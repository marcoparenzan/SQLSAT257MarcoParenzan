using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLSAT257DataViewModels
{
    public class ActivityViewModel
    {
        public int ActivityId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerTaxCode { get; set; }
        public string CustomerVatCode { get; set; }
        public System.DateTime DateTime { get; set; }
        public decimal Duration { get; set; }
        public string ActivityTypeDescription { get; set; }
        public string Details { get; set; }
        public string Username { get; set; }
    }
}
