using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using POS.DataAccess;
using POS.Models;

namespace POS.BusinessLogics
{
    public class CustomerLogics
    {
        private CustomerAccess _customerAccess=new CustomerAccess();

        public List<Customer> GetAllCustomers()
        {
            try
            {
                return _customerAccess.GetAll().ToList();
            }
            catch (Exception exception)
            {
                return new List<Customer>();
            }
        }

        public Customer FindCustomerById(int id)
        {
            try
            {
                return _customerAccess.GetSingle(customer => customer.Id == id);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string AddCustomer(Customer customer)
        {
            try
            {
                return _customerAccess.Add(customer) ? "Customer Added Successfully." : "Customer Not Added.";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string UpdateCustomer(Customer customer)
        {
            try
            {
                var aCustomer = _customerAccess.GetSingle(c => c.Id == customer.Id);
                aCustomer.Name = customer.Name;
                aCustomer.Address = customer.Address;
                aCustomer.ContactNo = customer.ContactNo;
                return _customerAccess.Update(aCustomer) ? "Customer update Successfully." : "Customer Not updated.";
            }
            catch (Exception e)
            {
                return e.Message;
            }
            
        }

        public void AddPoints(Customer customer, int point)
        {
            customer.Points += point;
            _customerAccess.Update(customer);
        }
    }
}
