using Banking.Model;
using Banking.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Repository
{
    public class CustomerRepository
    {
        public static List<Customer> customers;
        public static bool intialized = false;

        public void Initialize(string filePath)
        {
            if (intialized)
                throw new InvalidOperationException("El sistema ya ha sido inicializado.");

            try
            {
                customers = CsvLoader.Initialize(filePath);
                intialized = true;
            }
            catch(Exception ex) 
            {
                throw new InvalidOperationException("Hubo un error al cargar la informacion. | " + ex.Message);
            }
            
        }

        public List<Customer> GetCustomers() => customers;

        public Customer GetCustomerById(int id) => customers.FirstOrDefault(x => x.Id == id);

        public List<Customer> SearchCustomerByName(string name)
        {
            return customers.Where(c =>
                c.FirstName.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0 ||
                c.LastName.IndexOf(name,StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        public int CountCustomer() => customers.Count;

    }
}
