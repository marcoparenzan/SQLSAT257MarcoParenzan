using Aspose.Words;
using Aspose.Words.Saving;
using Aspose.Words.Tables;
using SQLSAT257DataViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLSAT257Reports
{
    public class Reports
    {
        public void Words(IEnumerable<ActivityViewModel> items, Stream stream)
        {
            var db = new CRMDataViewModel();

            var doc = new Document();
            var builder = new DocumentBuilder(doc);

            var table = builder.StartTable();
            // We must insert at least one row first before setting any table formatting.
            builder.InsertCell();
            builder.Writeln("Customer");
            builder.CellFormat.RightPadding = 40;
            builder.InsertCell();
            builder.Writeln("Activity");
            builder.InsertCell();
            builder.Writeln("Date");
            builder.EndRow();

            foreach (var activity in items)
            {
                builder.InsertCell();
                builder.Writeln(activity.CustomerName);
                builder.InsertCell();
                builder.Writeln(activity.ActivityTypeDescription);
                builder.InsertCell();
                builder.Writeln(activity.DateTime.ToShortDateString());
                builder.EndRow();
            }

            // Set the table style used based of the unique style identifier.
            // Note that not all table styles are available when saving as .doc format.
            table.StyleIdentifier = StyleIdentifier.MediumList2Accent2;
            // Apply which features should be formatted by the style.
            table.StyleOptions = TableStyleOptions.FirstColumn | TableStyleOptions.RowBands | TableStyleOptions.FirstRow;
            table.AutoFit(AutoFitBehavior.AutoFitToContents);

            doc.Save(stream, SaveFormat.Pdf);
        }
    }
}
