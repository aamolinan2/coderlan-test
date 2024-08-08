using Banking.Model;
using Banking.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Services
{
    public class BankingService
    {
        public readonly CustomerRepository _repository;

        public BankingService(CustomerRepository repository)
        {
            _repository = repository;
        }

        public void Initialize(string filePath)
        {
            _repository.Initialize(filePath);
        }

        public decimal CalculateFees(int giverId, int recieverId, decimal amount)
        {
            var giver = _repository.GetCustomerById(giverId);
            var reciever = _repository.GetCustomerById(recieverId);

            if (!giver.TypeCustomer.Equals("internal"))
            {
                throw new InvalidOperationException("Debe ser un cliente interno.");
            }

            if (giver.TypeCustomer.Equals("internal") && reciever.TypeCustomer.Equals("internal"))
            {
                return amount >= 100 ? 5 : 0;

            }else if (giver.TypeCustomer.Equals("internal") && !reciever.TypeCustomer.Equals("internal"))
            {
                return 10;
            }

            throw new InvalidOperationException("Condiciones de transferencia invalidas.");
        }

        public void TransferMoney(int giverId, int recieverId, decimal amount)
        {
            var giver = _repository.GetCustomerById(giverId);
            var reciever = _repository.GetCustomerById(recieverId);

            if (amount <= 0)
            {
                throw new InvalidOperationException("El monto debe ser mayor a cero");
            }

            if (giver == null || reciever == null)
            {
                throw new InvalidOperationException("No se encontro a estos clientes.");
            }

            if (!giver.TypeCustomer.Equals("internal"))
            {
                throw new InvalidOperationException("El cliente debe ser interno");
            }

            var fees = CalculateFees(giverId, recieverId, amount);

            if (giver.Balance < amount + fees) 
            {
                throw new InvalidOperationException("El cliente no posee sufienciente saldo");
            }

            giver.Balance -=  (amount + fees);
            reciever.Balance += amount;

        }
        public int CountCustomer() => _repository.CountCustomer();

        public Customer GetCustomerById(int id) => _repository.GetCustomerById(id);

        public List<Customer> SearchCustomerByName(string name) => _repository.SearchCustomerByName(name);

    }
}
