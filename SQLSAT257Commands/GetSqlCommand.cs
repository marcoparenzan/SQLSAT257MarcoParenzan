using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace SQLSAT257Commands
{
    [Cmdlet(VerbsCommon.Get, "sql")]
    public class GetSqlCommand : Cmdlet
    {
        [Parameter()]
        [ValidateNotNull]
        public string DataSource { get; set; }

        [Parameter()]
        [ValidateNotNull]
        public string InitialCatalog { get; set; }

        [Parameter()]
        [ValidateNotNull]
        public string Query { get; set; }

        private SqlConnection _connection;
        private SqlDataReader _dataReader;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            var csb = new SqlConnectionStringBuilder();
            csb.DataSource = DataSource;
            csb.InitialCatalog = InitialCatalog;
            csb.IntegratedSecurity = true;

            _connection = new SqlConnection(csb.ConnectionString);
            _connection.Open();

            var cmd = _connection.CreateCommand();
            cmd.CommandText = Query;

            _dataReader = cmd.ExecuteReader();
            if (!_dataReader.Read())
                this.StopProcessing();
        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            while (_dataReader.Read())
            {
                var item = new PSObject();
                for (var i = 0; i < _dataReader.FieldCount; i++)
                    item.Properties.Add(new PSNoteProperty(_dataReader.GetName(i), _dataReader.GetValue(i)));

                this.WriteObject(item);
            }
        } 

        protected override void EndProcessing()
        {
            _dataReader.Close();
            _connection.Close();

            base.EndProcessing();
        }
    }
}
