using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Management.Automation;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SQLSAT257Commands
{
    [Cmdlet(VerbsCommon.Get, "json")]
    public class GetJsonCommand : Cmdlet
    {
        [Parameter()]
        [ValidateNotNull]
        public string Url { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            var pending_json = (new HttpClient()).GetStringAsync(this.Url);
            pending_json.Wait();
            var json = pending_json.Result;
            Array.ForEach(JsonConvert.DeserializeObject<object[]>(json), WriteObject);
        }
    }
}