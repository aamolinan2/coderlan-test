using Banking.Repository;
using Banking.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking
{
    class Program
    {
        static void Main(string[] args) 
        { 
            var repository = new CustomerRepository();
            var service = new BankingService(repository);

            bool fileLoaded = false;

            while (!fileLoaded)
            {
                Console.WriteLine("Ingrese la ruta del archivo csv:");
                string filePath = Console.ReadLine();

                try
                {
                    service.Initialize(filePath);
                    Console.WriteLine("Sistema Bancario Iniciado");
                    fileLoaded = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Hubo un error al cargar el archivo");
                    Console.WriteLine("Por favor intente de nuevo");
                }
            }

            bool running = true;

            while (running)
            {
                Console.WriteLine("\n Seleccione una opcion:");
                Console.WriteLine("1. Transferir dinero");
                Console.WriteLine("2. Calcular comision");
                Console.WriteLine("3. Contar clientes");
                Console.WriteLine("4. Buscar cliente por nombre");
                Console.WriteLine("5. Buscar cliente por Id");
                Console.WriteLine("6. Salir");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        TransferMoney(service);
                        break;
                    case "2":
                        CalculateFees(service);
                        break;
                    case "3":
                        CountCustomer(service);
                        break;
                    case "4":
                        SearchByName(service);
                        break;
                    case "5":
                        SearchById(service);
                        break;
                    case "6":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Opcion no valida");
                        break;

                }
            }
        }

        static void TransferMoney(BankingService service)
        {
            Console.WriteLine("Ingrese el Id del Cliente que da el dinero:");
            int giverId = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese el Id del Cliente que recibira el dinero:");
            int reciverId = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese el monto a transferir:");
            decimal amount = decimal.Parse(Console.ReadLine());

            try
            {
                service.TransferMoney(giverId, reciverId, amount);
                Console.WriteLine("El dinero se ha transferido con exito");
            }catch (Exception ex) 
            { 
                Console.WriteLine($"Hubo un error al transferir el dinero: {ex.Message}"); 
            }
              
        }

        static void CalculateFees(BankingService service)
        {
            Console.WriteLine("Ingrese el Id del Cliente que da el dinero:");
            int giverId = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese el Id del Cliente que recibira el dinero:");
            int reciverId = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese el monto a transferir:");
            decimal amount = decimal.Parse(Console.ReadLine());

            try
            {
                var fee=service.CalculateFees(giverId, reciverId, amount);
                Console.WriteLine($"Comision calculada con exito: {fee} ");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hubo un error al realizar el calculo de la comision: {ex.Message}");
            }

        }

        static void CountCustomer(BankingService service)
        {
            var count = service.CountCustomer();
            Console.WriteLine($"Total de clientes: {count} ");
        }

        static void SearchByName(BankingService service)
        {
            var count = service.CountCustomer();
            Console.WriteLine("Ingrese el nombre del cliente:");
            var name = Console.ReadLine();

            var result= service.SearchCustomerByName(name);
            if (result.Count == 0)
            {
                Console.WriteLine("No se encontraron clientes con ese nombre");
            }
            else
            {
                foreach (var item in result)
                {
                    Console.WriteLine($"Se encontro al cliente con ID: {item.Id} Nombre: {item.FirstName} Apellido: {item.LastName} Balance: {item.Balance}");
                }

                Console.WriteLine($"Total de clientes con ese nombre: {result.Count}");
            }
        }

        static void SearchById(BankingService service)
        {
            var count = service.CountCustomer();
            Console.WriteLine("Ingrese el Id del cliente:");
            var idCliente = int.Parse(Console.ReadLine());

            var result = service.GetCustomerById(idCliente);
            if (result == null)
            {
                Console.WriteLine("No se encontraron clientes con ese nombre");
            }
            else
            {
                Console.WriteLine($"Se encontro al cliente con ID: {result.Id} Nombre: {result.FirstName} Apellido: {result.LastName} Balance: {result.Balance}");
            }
        }
    }
}
