using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLSAT257LoadFromExcel
{
    class Program
    {
        static void Main()
        {
            var workbook = new Workbook("SQLSAT257InitialData.xlsx");

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLSAT257CRM"].ConnectionString))
            {
                connection.Open();

                //
                // customers
                //
                Console.WriteLine("Inserting Customers...");
                var insertCustomerCommand = connection.CreateCommand();
                insertCustomerCommand.CommandText = "INSERT INTO crm.Customers(Name, FirstName, TaxCode, VatCode) VALUES (@Name, @FirstName, @TaxCode, @VatCode)";
                var nameParam = insertCustomerCommand.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar, 64);
                var firstNameParam = insertCustomerCommand.Parameters.Add("@FirstName", System.Data.SqlDbType.NVarChar, 64);
                firstNameParam.IsNullable = true;
                var taxCodeParam = insertCustomerCommand.Parameters.Add("@TaxCode", System.Data.SqlDbType.NVarChar, 16);
                var vatCodeParam = insertCustomerCommand.Parameters.Add("@VatCode", System.Data.SqlDbType.NChar, 11);
                vatCodeParam.IsNullable = true;

                var customers = workbook.Worksheets["customers"];
                for (var r = 1; customers.Cells[r, 0].Value != null; r++)
                {
                    nameParam.Value = customers.Cells[r, 0].Value;
                    firstNameParam.Value = customers.Cells[r, 1].Value ?? System.DBNull.Value;
                    taxCodeParam.Value = customers.Cells[r, 2].Value;
                    vatCodeParam.Value = customers.Cells[r, 3].Value ?? System.DBNull.Value;
                    insertCustomerCommand.ExecuteNonQuery();
                    Console.WriteLine("Customer {0} inserted", nameParam.Value);
                }

                //
                // activityTypes
                //
                Console.WriteLine("Inserting ActivityTypes...");
                var insertActivityTypeCommand = connection.CreateCommand();
                insertActivityTypeCommand.CommandText = "INSERT INTO crm.ActivityTypes(Description) VALUES (@Description)";
                var descriptionParam = insertActivityTypeCommand.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar, 64);

                var activityTypes = workbook.Worksheets["ActivityTypes"];
                for (var r = 1; activityTypes.Cells[r, 0].Value != null; r++)
                {
                    descriptionParam.Value = activityTypes.Cells[r, 0].Value;
                    insertActivityTypeCommand.ExecuteNonQuery();
                    Console.WriteLine("ActivityType {0} inserted", descriptionParam.Value);
                }
                    
                //
                // users
                //
                Console.WriteLine("Inserting Users...");
                var insertUserCommand = connection.CreateCommand();
                insertUserCommand.CommandText = "INSERT INTO dbo.Users(Username, DisplayName) VALUES (@Username, @DisplayName)";
                var usernameParam = insertUserCommand.Parameters.Add("@Username", System.Data.SqlDbType.NVarChar, 64);
                var displayNameParam = insertUserCommand.Parameters.Add("@DisplayName", System.Data.SqlDbType.NVarChar, 64);

                var users = workbook.Worksheets["Users"];
                for (var r = 1; activityTypes.Cells[r, 0].Value != null; r++)
                {
                    usernameParam.Value = users.Cells[r, 0].Value;
                    displayNameParam.Value = users.Cells[r, 1].Value;
                    insertUserCommand.ExecuteNonQuery();
                    Console.WriteLine("User {0} inserted", usernameParam.Value);
                }

                connection.Close();
            }

            Console.ReadLine();
        }
    }
}
