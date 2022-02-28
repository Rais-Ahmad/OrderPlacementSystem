using ClassLibraryDAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WEB
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        DAL repo = new DAL();

        // Web Service to view all Customers

        [WebMethod]
        public List<Customer1> customerList()
        {
            var customers = repo.viewCustomers(); 
            return customers;
        }

        // Web Service to view Customer details 

        [WebMethod]
        public List<Customer1> CustomerDetail(string id)
        {
            var customerList = repo.viewById(id);
            return customerList;
        }

        // Web Service to Create Customer

        [WebMethod]
        public int createCustomer(Customer1 Customer)
        {
            var create = repo.createCustomer(Customer);
            return create;
        }

        // Web Service to edit Customer

        [WebMethod]
        public Customer1 EditView(string id)
        {
            var customer = repo.editCustomer(id);
            return customer;
        }

        [WebMethod]
        public int Edit(Customer1 customer)
        {
            var save = repo.saveEdit(customer);
            return save;
        }

        // Web Service to Delete Customer

        [WebMethod]
        public void Delete(string id)
        {
            repo.del(id);
        }

        // Web Service to View next Customer

        [WebMethod]
        public List<Customer1> Next(string id)
        {
            var nextCustomer = repo.nextCustomer(id);
            return nextCustomer;
        }

        // Web Service to View First Customer

        [WebMethod]
        public List<Customer1> First()
        {
            var firstCustomer = repo.firstCustomer();
            return firstCustomer;
        }

        // Web Service to view Order details

        [WebMethod]
        public List<Order> OrderDetails(string id)
        {
            var orders = repo.viewOrder(id);
            return orders;
        }

        // Web Service to place order in 'Order' Table

        [WebMethod]
        public void OrderAgainstCustomer(Order order, string id)
        {
            repo.orderPlacement(order, id);
        }

        // Web Service to place order in 'OrderDetails' Table

        [WebMethod]
        public List<string> AddingToOrderDetails()
        {
            List<string> prod = new List<string>();
            var list = repo.ProductNames();

            foreach (Products p in list)
            {
                prod.Add(p.ProductName);
            }
            return prod;
        }

        [WebMethod]
        public int postOrderDetails(Products orderDetails, int id)
        {
            var create = repo.ProductOrder(orderDetails, id);
            return create;
        }

     }

}
