using Banking.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Utilities
{
    public static class CsvLoader
    {
        public static List<Customer> Initialize (string filePath)
        {
            var customers = new List<Customer>();

            using (var reader = new StreamReader(filePath))
            {
                var header = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    var customer = new Customer
                    {
                        Id = int.Parse(values[0]),
                        FirstName = values[1],
                        LastName = values[2],
                        Phone1 = values[9],
                        Phone2 = values[10],
                        TypeCustomer = values[13],
                        Balance = decimal.Parse(values[14])
                    };

                    customers.Add(customer);
                }
            }

            return customers;
        }
        
    }
}
