using Aspose.Words;
using Aspose.Words.Tables;
using Newtonsoft.Json.Linq;
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
    [Cmdlet(VerbsCommon.Format, "Pdf")]
    public class FormatPdfCommand : Cmdlet
    {
        [Parameter(ValueFromPipeline=true, ValueFromPipelineByPropertyName=true)]
        [ValidateNotNull]
        public JObject Source { get; set; }
        
        [Parameter]
        [ValidateNotNull]
        public string PdfName { get; set; }

        private Document _document;
        private DocumentBuilder _documentBuilder;
        private Aspose.Words.Tables.Table _table;
        private bool _header;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            _document = new Document();
            _documentBuilder = new DocumentBuilder(_document);
            _documentBuilder.PageSetup.PaperSize = PaperSize.A4;
            _documentBuilder.PageSetup.Orientation = Orientation.Landscape;
            _table = _documentBuilder.StartTable();
            _header = true;
        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            if (_header)
            {
                foreach (var property in Source.Properties())
                {
                    _documentBuilder.InsertCell();
                    _documentBuilder.Writeln(property.Name);
                }
                _documentBuilder.EndRow();
                _header = false;
            }
            foreach (var property in Source.Properties())
            {
                _documentBuilder.InsertCell();
                _documentBuilder.Writeln(property.Value.ToString());
            }
            _documentBuilder.EndRow();
        } 

        protected override void EndProcessing()
        {
            base.EndProcessing();

            // Set the table style used based of the unique style identifier.
            // Note that not all table styles are available when saving as .doc format.
            _table.StyleIdentifier = StyleIdentifier.MediumList2Accent2;
            // Apply which features should be formatted by the style.
            _table.StyleOptions = TableStyleOptions.FirstColumn | TableStyleOptions.RowBands | TableStyleOptions.FirstRow;
            _table.AutoFit(AutoFitBehavior.AutoFitToContents);

            _document.Save(PdfName, SaveFormat.Pdf);
        }
    }
}
