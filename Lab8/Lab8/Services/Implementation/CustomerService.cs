using Lab8.Models;
using Lab8.Repositories.Interface;
using Lab8.Repositories.Interfaces;
using Lab8.Services.Interfaces;
using System.Collections.Generic;

namespace Lab8.Services.Implementation
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public List<Customer> GetAll()
        {
            return _customerRepository.GetAll();
        }

        public Customer? GetById(int id)
        {
            return _customerRepository.GetById(id);
        }

        public void Add(Customer customer)
        {
            _customerRepository.Add(customer);
        }

        public void Update(Customer customer)
        {
            _customerRepository.Update(customer);
        }

        public void Delete(int id)
        {
            _customerRepository.Delete(id);
        }
    }
}
