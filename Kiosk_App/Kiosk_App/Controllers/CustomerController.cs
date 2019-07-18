using Kiosk_App.Models;
using System;
using System.Linq;
using System.Web.Http;

namespace Kiosk_App.Controllers
{
    [RoutePrefix("api/customer")]
    public class CustomerController : ApiController
    {
        private Priyanka_DBEntities _context;

        public CustomerController()
        {
            if (_context == null)
                _context = new Priyanka_DBEntities();
        }

        // GET api/values
        [HttpGet]
        [Route("Customer/{customerId}")]
        public Customer Customer(int customerId)
        {
            try
            {
                Customer customer = new Customer();
                if (customerId > default(int))
                {
                    CUSTOMER cust = _context.CUSTOMERS.FirstOrDefault(x => x.CUST_REG_ID == customerId);
                    if (cust != null)
                    {
                        customer.CustomerId = cust.CUST_REG_ID;
                        customer.Name = cust.CUST_NAME;
                        customer.Email = cust.CUST_EMAIL;
                        customer.Mobile = cust.CUST_MOBILE;
                    }
                }

                return customer;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET api/values/5
        [HttpGet]
        [Route("BillPayments/{customerId}")]
        public Customer BillPayments(int customerId)
        {
            try
            {
                Customer customer = new Customer();
                IQueryable<BILL_PAYMENTS> billPayments = _context.BILL_PAYMENTS.Where(x => x.CUST_REG_ID == customerId).OrderByDescending(x => x.DATE_TIME).Take(5);
                if (billPayments != null && billPayments.Count() > 0)
                {
                    customer.Name = billPayments.FirstOrDefault().CUSTOMER.CUST_NAME;
                    customer.CustomerId = billPayments.FirstOrDefault().CUSTOMER.CUST_REG_ID;
                    foreach (BILL_PAYMENTS payment in billPayments)
                    {
                        customer.BillPayments.Add(new BillPayment()
                        {
                            Biller = payment.BILLER,
                            PaymentId = payment.PAYMENT_ID,
                            Amount = payment.AMOUNT.ToString("c"),
                            DateTime = payment.DATE_TIME.ToString("MM/dd/yyyy")
                        });
                    }
                }

                return customer;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // POST api/values
        [HttpPost]
        public int Post([FromBody]CUSTOMER customer)
        {
            try
            {
                if (customer != null)
                {
                    _context.CUSTOMERS.Add(customer);
                    _context.SaveChanges();
                    return customer.CUST_REG_ID;
                }

                return default(int);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT api/values/5
        [HttpPut]
        public void Put(int customerId, [FromBody]CUSTOMER customer)
        {
            try
            {
                if (customerId > default(int))
                {
                    CUSTOMER existingCustomer = _context.CUSTOMERS.FirstOrDefault(x => x.CUST_REG_ID == customerId);
                    existingCustomer.CUST_NAME = customer.CUST_NAME;
                    existingCustomer.CUST_EMAIL = customer.CUST_EMAIL;
                    existingCustomer.CUST_MOBILE = customer.CUST_MOBILE;

                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
