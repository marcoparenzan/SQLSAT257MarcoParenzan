using SQLSAT257DataModels;
using SQLSAT257DataViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLSAT257EntityFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            // Better saying nothing to SQL developers about this!
            //Database.SetInitializer<CRMDataModel>(null);

            //var crmDataModel = new CRMDataModel();
            //foreach (var customer in crmDataModel.Customers)
            //{
            //    Console.WriteLine("Customer {0} {1}", customer.Name, customer.FirstName);
            //}

            //var a_customer = crmDataModel.Customers.First();
            //var an_activityType = crmDataModel.ActivityTypes.First();
            //var a_user = crmDataModel.Users.First();

            //var new_activity = new Activity
            //{

            //    User = a_user
            //    ,
            //    ActivityType = an_activityType
            //    ,
            //    Customer = a_customer
            //    ,
            //    DateTime = DateTime.Today.AddHours(9)
            //    ,
            //    Duration = 10
            //    ,
            //    Details = "#sqlsat257!!!!!"
            //};
            //crmDataModel.Activities.Add(new_activity);
            //crmDataModel.SaveChanges();
           

            // Better saying nothing to SQL developers about this!
            Database.SetInitializer<CRMDataViewModel>(null);

            var crmDataViewModel = new CRMDataViewModel();
            var new_activity = new ActivityViewModel {
            
                CustomerName = "Azienda XXX"
                ,
                CustomerTaxCode = "10101010105"
                ,
                CustomerVatCode = "10101010105"
                ,
                ActivityTypeDescription = "Appointment"
                ,
                DateTime = DateTime.Today.AddDays(1).AddHours(15)
                ,
                Details = "Discuss...."
                ,
                Duration = 1
                ,
                Username = "marco"            
            
            };
            crmDataViewModel.Activities.Add(new_activity);
            crmDataViewModel.SaveChanges();
            Console.ReadLine();
        }
    }
}
