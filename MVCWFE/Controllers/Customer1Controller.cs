using System.Web.Mvc;
using MVCWFE.ServiceReference1;

namespace MVCWFE.Controllers
{
    public class Customer1Controller : Controller
    {
      WebService1SoapClient client = new WebService1SoapClient();

        // View all Customers

        [HttpGet]
        public ActionResult Index()
        {
             return View(client.customerList());  
        }

        // View Order details of a particular customer 

        public ActionResult OrderDetails(string id)
        {
            return View(client.OrderDetails(id));
        }

        // View details of Customers
            public ActionResult Details(string id)
        {
            return View(client.CustomerDetail(id));
        }

        // Create Customer
        public ActionResult Create()
        {
            return View(new Customer1());
        }
              
        [HttpPost]
        public ActionResult Create(Customer1 customer)
        {
            int create = client.createCustomer(customer);
            if (create == 1)
            {
                return RedirectToAction("Index");
            }
            else return View();
           }

        // Create Order by selecting product
       public ActionResult CreateProductOrder()
        {
            ViewBag.list = client.AddingToOrderDetails();
            return View(new Products());
        }

        [HttpPost]
        public ActionResult CreateProductOrder(Products orderDetails, int id, string str)
        {
            int create = client.postOrderDetails(orderDetails, id);
            if (create == 1)
            {
                return RedirectToAction("OrderDetails", new { id = str });
            }
            else return View();

        } 

        // Edit Customer details
        public ActionResult Edit(string id)
        {
           Customer1 customer = new Customer1();
            customer = client.EditView(id);
            return View(customer);
        }

        [HttpPost]
        public ActionResult Edit(Customer1 customer)
        {            
           int save = client.Edit(customer);
            if (save == 1) {
                return RedirectToAction("Index");
            }
            else return View();
        }

        // View next customer's details
        public ActionResult Next(string id)
        {
            return View(client.Next(id));
        }

        // View first customer's detail
        public ActionResult First()
        {
            return View(client.First());
        }

        // Place Order in 'Orders' table
        public ActionResult placeOrder()
        {
            return View(new Order());
        }

        [HttpPost]
        public ActionResult placeOrder(Order order, string id)
        {
            client.OrderAgainstCustomer(order, id);
            return RedirectToAction("OrderDetails", new { id = id });
        }

        // Delete Customer 
        public ActionResult Delete(string id)
        {
            client.Delete(id);
            return RedirectToAction("Index");
        }


    }
}




