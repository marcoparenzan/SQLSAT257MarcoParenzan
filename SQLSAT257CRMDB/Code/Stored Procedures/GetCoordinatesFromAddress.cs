//------------------------------------------------------------------------------
// <copyright file="CSSqlStoredProcedure.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Net.Http;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure(Name = "GetCoordinatesFromAddress")]
    public static void GetCoordinatesFromAddress(string address)
    {
        // https://developers.google.com/maps/documentation/geocoding/
        // http://maps.googleapis.com/maps/api/geocode/json?address={0}&sensor=false
        // Via+Kennedy+38+Fiume+Veneto+Pordenone+Italy
        var url = 
            string.Format(
                "http://maps.googleapis.com/maps/api/geocode/json?address={0}&sensor=false"
                , address.Replace(" ", "+")
            );
        var pending_json = (new HttpClient()).GetStringAsync(url);
        pending_json.Wait();
        var json = pending_json.Result;

        var response = (JObject)JsonConvert.DeserializeObject(json);
        var results = (JArray)response["results"];
        var first_result = results[0];
        var geometry = (JObject)first_result["geometry"];
        var location = (JObject)geometry["location"];

        double lat = 0.0;
        double lng = 0.0;
        lat = location["lat"].Value<double>();
        lng = location["lng"].Value<double>();

        var record = new SqlDataRecord(
            new SqlMetaData("json", SqlDbType.NVarChar, 2048)
            , new SqlMetaData("lat", SqlDbType.Float)
            , new SqlMetaData("lng", SqlDbType.Float)
        );
        record.SetString(0, json);
        record.SetDouble(1, lat);
        record.SetDouble(2, lng);

        SqlContext.Pipe.SendResultsStart(record);
        SqlContext.Pipe.SendResultsRow(record);
        SqlContext.Pipe.SendResultsEnd();
        
        using (var conn = new SqlConnection("context connection=true"))
        {
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO [dbo].[AddressSearchs] (Address, Timestamp, Provider, Response, Coordinates) VALUES (@Address, @Timestamp, @Provider, @Response, @Coordinates)";
        
            cmd.Parameters.AddWithValue("@Address", address);
            cmd.Parameters.AddWithValue("@Timestamp", DateTime.Now);
            cmd.Parameters.AddWithValue("@Provider", "google");
            cmd.Parameters.AddWithValue("@Response", json);
            cmd.Parameters.AddWithValue("@Coordinates", string.Format("Point({0} {1} {2})", lat, lng, 4444));

            cmd.ExecuteNonQuery();
        
            conn.Close();
        }
    }
}
