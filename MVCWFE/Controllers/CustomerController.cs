using MVCWFE.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;

namespace MVCWFE.Controllers
{
       public class CustomerController : Controller
    {
        string connectionString = @"Data Source=RAIS-AHMAD\SQLEXPRESS;Initial Catalog=db_test;User ID=sa;Password=4Islamabad";

        [HttpGet]
        public ActionResult Index()
        {
            DataTable dt = new DataTable();
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "Select * from customer";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                sda.Fill(dt);
            }
            return View(dt);
        }

        // GET: Customer/Details/5
        public ActionResult Details(string id)
        {
            try
            {
                CustomerModel customer = new CustomerModel();
                DataTable d = new DataTable();
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "SELECT * FROM customer Where Id= @Id ";
                    SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                    sqlDa.SelectCommand.Parameters.AddWithValue("@Id", id);
                    sqlDa.Fill(d);

                }
                if (d.Rows.Count == 1)
                {
                    customer.Id = Convert.ToInt32(d.Rows[0][0].ToString());
                    customer.Name = d.Rows[0][1].ToString();
                    customer.Address = d.Rows[0][2].ToString();
                    return View(customer);
                }
                else
                    return RedirectToAction("Index");
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        
        [HttpGet]
        public ActionResult Create()
        {
            return View(new CustomerModel());
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(CustomerModel customerModel)
        {
            try
            {
                using(SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "INSERT INTO customer VALUES (@Name, @Address)";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@Name", customerModel.Name);
                    sqlCmd.Parameters.AddWithValue("@Address", customerModel.Address);
                    sqlCmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            CustomerModel customer = new CustomerModel();
            DataTable d = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "SELECT * FROM customer Where Id = @Id";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@Id", id);
                sqlDa.Fill(d);

            }
            if(d.Rows.Count == 1)
            {
                customer.Id = Convert.ToInt32(d.Rows[0][0].ToString());
                customer.Name = d.Rows[0][1].ToString();
                customer.Address = d.Rows[0][2].ToString();
                return View(customer);
            }
            else
                return RedirectToAction("Index");
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(CustomerModel customerModel)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "UPDATE customer SET Name = @Name, Address = @Address Where Id= @Id ";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@Id", customerModel.Id);
                    sqlCmd.Parameters.AddWithValue("@Name", customerModel.Name);
                    sqlCmd.Parameters.AddWithValue("@Address", customerModel.Address);
                    sqlCmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "DELETE customer Where Id= @Id ";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@Id", id);
                sqlCmd.ExecuteNonQuery();
            }
                return RedirectToAction("Index");
        }
        // GET: Customer/Delete/5
        public ActionResult DeleteTest(int id)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "DELETE test Where id= @id ";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@id", id);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        /*     // POST: Customer/Delete/5
             [HttpPost]
             public ActionResult Delete(int id, FormCollection collection)
             {
                 try
                 {
                     // TODO: Add delete logic here

                     return RedirectToAction("Index");
                 }
                 catch
                 {
                     return View();
                 }
             }*/
    }
}
