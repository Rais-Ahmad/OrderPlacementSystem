using System;
using System.Data;
using System.Data.SqlClient;
using Models;
using System.Collections.Generic;

namespace ClassLibraryDAL
{
    public class DAL
    {
        string connectionString = @"Data Source=RAIS-AHMAD\SQLEXPRESS;Initial Catalog=NORTHWND;User ID=sa;Password=4Islamabad";

        // Function to view all Customers
        public List<Customer1> viewCustomers()
        {
            List<Customer1> customers = new List<Customer1>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select CustomerID, ContactName, CompanyName from Customers", con);

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Customer1 customer = new Customer1();
                    customer.CustomerID = rdr["CustomerID"].ToString();
                    customer.ContactName = rdr["ContactName"].ToString();
                    customer.CompanyName = rdr["CompanyName"].ToString();

                    customers.Add(customer);
                }
                rdr.Close();
                cmd.Dispose();
                con.Close();
            }
            return customers;
        }
      
        // Function to View a particular customer

        public List<Customer1> viewById(string id)
        {
            List<Customer1> customers = new List<Customer1>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

            SqlCommand cmd = new SqlCommand("Select * from Customers", con);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Customer1 customer = new Customer1();

                customer.CustomerID = rdr["CustomerID"].ToString();
                customer.ContactName = rdr["ContactName"].ToString();
                customer.CompanyName = rdr["CompanyName"].ToString();
                customer.ContactTitle = rdr["ContactTitle"].ToString();
                customer.Address = rdr["Address"].ToString();
                customer.City = rdr["City"].ToString();
                customer.Region = rdr["Region"].ToString();
                if (customer.CustomerID == id)
                {
                    customers.Add(customer);
                }
            }
            rdr.Close();
            cmd.Dispose();
            con.Close();
        }

            return customers;
    }

        // Function to view all Orders 
        public List<Order> viewOrder(string id)
        {
            List<Order> orders = new List<Order>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("Select * from Orders", con);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Order order = new Order();

                    order.CustomerID = rdr["CustomerID"].ToString();
                    order.OrderID = Convert.ToInt32(rdr["OrderID"].ToString());
                    order.ShipName = rdr["ShipName"].ToString();
                    order.ShipAddress = rdr["ShipAddress"].ToString();
                    order.ShipCity = rdr["ShipCity"].ToString();
                    order.ShipRegion = rdr["ShipRegion"].ToString();
                    order.ShipPostalCode = rdr["ShipPostalCode"].ToString();
                    order.ShipCountry = rdr["ShipCountry"].ToString();

                    if (order.CustomerID == id)
                    {
                        orders.Add(order);
                        
                    }
                }
                rdr.Close();
                cmd.Dispose();
                con.Close();
            }
            return orders;
        }

        // Function to Create Customer
        public int createCustomer(Customer1 customerModel)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "INSERT INTO Customers VALUES (@CustomerID, @CompanyName, @ContactName , @ContactTitle, @Address, @City, @Region, @PostalCode, @Country, @Phone, @Fax  )";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@CustomerID", customerModel.CustomerID);
                    sqlCmd.Parameters.AddWithValue("@CompanyName", customerModel.CompanyName);
                    sqlCmd.Parameters.AddWithValue("@ContactName", customerModel.ContactName);
                    sqlCmd.Parameters.AddWithValue("@ContactTitle", customerModel.ContactTitle);
                    sqlCmd.Parameters.AddWithValue("@Address", customerModel.Address);
                    sqlCmd.Parameters.AddWithValue("@City", customerModel.City);
                    sqlCmd.Parameters.AddWithValue("@Region", customerModel.Region);
                    sqlCmd.Parameters.AddWithValue("@PostalCode", customerModel.PostalCode);
                    sqlCmd.Parameters.AddWithValue("@Country", customerModel.Country);
                    sqlCmd.Parameters.AddWithValue("@Phone", customerModel.Phone);
                    sqlCmd.Parameters.AddWithValue("@Fax", customerModel.Fax);

                    sqlCmd.ExecuteNonQuery();
                }

                return 1;
            }
            catch
            {
                return 2;
            }
        }

        
        // Function to add ProductOrder
        public int ProductOrder(Products orderDet, int oId)
        {
            try
            {           
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    int id = 0;
                    con.Open();
                 
                    string query = "Select ProductID from Products where ProductName = @ProductName";
                    SqlCommand sql = new SqlCommand(query, con);
                    sql.Parameters.AddWithValue("@ProductName", orderDet.ProductName);
                    SqlDataReader rdr = sql.ExecuteReader();
                    while (rdr.Read())
                    {
                        Products p = new Products();
                         id = p.ProductID = Convert.ToInt32(rdr["ProductID"].ToString());
                    }
                    rdr.Close();
                    sql.Dispose();

                    string query1 = "INSERT INTO OrderDetails (OrderID, ProductID) VALUES (@OrderID, @ProductID)";
                    SqlCommand cmd = new SqlCommand(query1, con);
                    cmd.Parameters.AddWithValue("@OrderID", oId);
                    cmd.Parameters.AddWithValue("@ProductID", id);
                    cmd.ExecuteNonQuery();

                   
                    con.Close();
                }
                return 1;
            }catch(Exception e)
            {
                return 2;
            }
        }


        // Function to Edit Customer
        public Customer1 editCustomer(string id)
        {
            Customer1 cust = new Customer1();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("Select * from Customers", con);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Customer1 customer = new Customer1();

                    customer.CustomerID = rdr["CustomerID"].ToString();
                    customer.ContactName = rdr["ContactName"].ToString();
                    customer.CompanyName = rdr["CompanyName"].ToString();
                    customer.Address = rdr["Address"].ToString();
                    customer.City = rdr["City"].ToString();
                    customer.Region = rdr["Region"].ToString();
                    customer.PostalCode = rdr["PostalCode"].ToString();
                    customer.Country = rdr["Country"].ToString();
                    customer.Phone = rdr["Phone"].ToString();
                    customer.Fax = rdr["Fax"].ToString();

                    if (customer.CustomerID == id)
                    {
                        cust = customer;
                        return cust;
                    }
                }
                rdr.Close();
                cmd.Dispose();
                con.Close();
            }
            return cust;
        }

        // Function for saving Edited Customer
        public int saveEdit(Customer1 customerModel)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "UPDATE Customers SET CompanyName = @CompanyName, ContactName = @ContactName , ContactTitle = @ContactTitle, Address = @Address, City = @City, Region = @Region, PostalCode = @PostalCode, Country = @Country, Phone = @Phone, Fax = @Fax  Where CustomerId= @CustomerId ";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@CustomerID", customerModel.CustomerID);
                    sqlCmd.Parameters.AddWithValue("@CompanyName", customerModel.CompanyName);
                    sqlCmd.Parameters.AddWithValue("@ContactName", customerModel.ContactName);
                    sqlCmd.Parameters.AddWithValue("@ContactTitle", customerModel.ContactTitle);
                    sqlCmd.Parameters.AddWithValue("@Address", customerModel.Address);
                    sqlCmd.Parameters.AddWithValue("@City", customerModel.City);
                    sqlCmd.Parameters.AddWithValue("@Region", customerModel.Region);
                    sqlCmd.Parameters.AddWithValue("@PostalCode", customerModel.PostalCode);
                    sqlCmd.Parameters.AddWithValue("@Country", customerModel.Country);
                    sqlCmd.Parameters.AddWithValue("@Phone", customerModel.Phone);
                    sqlCmd.Parameters.AddWithValue("@Fax", customerModel.Fax);
                    sqlCmd.ExecuteNonQuery();
                }

                return 1;
            }
            catch (Exception e)
            {
                return 2;
            }
        }

        // Function to view next customer
        public List<Customer1> nextCustomer(string id)
        {

            List<Customer1> customers = new List<Customer1>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                int count = 0;
                con.Open();

                SqlCommand cmd = new SqlCommand("Select * from Customers", con);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Customer1 customer = new Customer1();

                    customer.CustomerID = rdr["CustomerID"].ToString();
                    customer.ContactName = rdr["ContactName"].ToString();
                    customer.CompanyName = rdr["CompanyName"].ToString();
                    customer.ContactTitle = rdr["ContactTitle"].ToString();
                    customer.Address = rdr["Address"].ToString();
                    customer.City = rdr["City"].ToString();
                    customer.Region = rdr["Region"].ToString();
                    if (count == 2)
                    {
                        customers.Add(customer);
                        return customers;
                    }
                    if (customer.CustomerID == id)
                    {
                        count = 1;
                        count = count + 1;
                    }
                }
                rdr.Close();
                cmd.Dispose();
                con.Close();
            }
            return customers;
        }

        // Function to view first Customer
        public List<Customer1> firstCustomer()
        {

            List<Customer1> firstCustomer = new List<Customer1>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from Customers", con);
                SqlDataReader rdr = cmd.ExecuteReader();

                Customer1 customer = new Customer1();
                int count = 0;
                while (rdr.Read())
                {
                    customer.CustomerID = rdr["CustomerID"].ToString();
                    customer.ContactName = rdr["ContactName"].ToString();
                    customer.CompanyName = rdr["CompanyName"].ToString();
                    customer.ContactTitle = rdr["ContactTitle"].ToString();
                    customer.Address = rdr["Address"].ToString();
                    customer.City = rdr["City"].ToString();
                    customer.Region = rdr["Region"].ToString();

                    count = count + 1;
                    firstCustomer.Add(customer);
                    if (count == 1)
                    {
                        return firstCustomer;
                    }
                }
                rdr.Close();
                cmd.Dispose();
                con.Close();
            }
            return firstCustomer;
        }


        // Function to Delete
        public void del(string id)
        {
            int temp;
            List<Order> orders = new List<Order>();
            List<int> num = new List<int>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("Select * from Orders", con);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Order order = new Order();

                    order.CustomerID = rdr["CustomerID"].ToString();
                    order.OrderID = Convert.ToInt32(rdr["OrderID"].ToString());
                    order.ShipName = rdr["ShipName"].ToString();
                    order.ShipAddress = rdr["ShipAddress"].ToString();
                    order.ShipCity = rdr["ShipCity"].ToString();
                    order.ShipRegion = rdr["ShipRegion"].ToString();
                    order.ShipPostalCode = rdr["ShipPostalCode"].ToString();
                    order.ShipCountry = rdr["ShipCountry"].ToString();

                    if (order.CustomerID == id)
                    {                          
                        temp = order.OrderID;
                        num.Add(temp);
                    }
                }
                rdr.Close();
                cmd.Dispose();

                foreach (int number in num)
                {

                SqlCommand cmd1 = new SqlCommand("Delete from OrderDetails where OrderID = @OrderID ", con);
                cmd1.Parameters.AddWithValue("@OrderID", number);                
                cmd1.ExecuteNonQuery();
                }
          
                SqlCommand cmd2 = new SqlCommand("Delete from Orders where CustomerID = @CustomerID ", con);
                cmd2.Parameters.AddWithValue("@CustomerID", id);
                cmd2.ExecuteNonQuery();

                SqlCommand cmd3 = new SqlCommand("Delete from Customers where CustomerID = @CustomerID ", con);
                cmd3.Parameters.AddWithValue("@CustomerID", id);
                cmd3.ExecuteNonQuery();

                con.Close();
            }

        }

        //Function to get all productIds
        public List<Products> ProductNames()
        {
            List<Products> list = new List<Products>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("Select ProductName from Products", con);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Products product = new Products();
                    string i;
                    product.ProductName = rdr["ProductName"].ToString();

                    list.Add(product);

                }
                rdr.Close();
                cmd.Dispose();
                con.Close();
            }
            return list;
        }

        //Function to place orders in 'Orders' Table
        public void orderPlacement(Order order , string id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string ship = "AlachiSoft";
                string address = "Dummy";
                con.Open();
                string query1 = "INSERT INTO Orders (CustomerID, ShipName , ShipAddress ,ShipCity , ShipRegion ,ShipPostalCode ,ShipCountry ) VALUES (@CustomerID, @ShipName ,@ShipAddress , @ShipCity , @ShipRegion, @ShipPostalCode ,@ShipCountry )";
                SqlCommand cmd = new SqlCommand(query1, con);
                cmd.Parameters.AddWithValue("@CustomerID", id);
                cmd.Parameters.AddWithValue("@ShipName", ship);
                cmd.Parameters.AddWithValue("@ShipAddress", address);
                cmd.Parameters.AddWithValue("@ShipCity", ship);
                cmd.Parameters.AddWithValue("@ShipRegion", address);
                cmd.Parameters.AddWithValue("@ShipPostalCode", ship);
                cmd.Parameters.AddWithValue("@ShipCountry", address);
                cmd.ExecuteNonQuery();

               con.Close();
            }
        }




    }
}
