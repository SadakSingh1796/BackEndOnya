using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnyaServices;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static OnyaModels.BankDetailsModel;
using static OnyaModels.CommonModel;

namespace Onyo_v1._0.Controllers
{
    [ApiController]
    [Route("api/[controller]/{action}/")]
    public class BankDetailsController : Controller
    {
        private readonly ILogger<BankDetailsController> _logger;
        private BankDetailsService bankDetailsService = new BankDetailsService();

        public BankDetailsController(ILogger<BankDetailsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ApiResult AddBankDetails(AddBankDetailsRequestModel model)
        {
            try
            {
                if (model.userid == null || model.userid == 0)
                {
                    return new ApiResult() { isSuccess = false, message = "User Id is required!" };
                }

                if (model.holdername == null || model.holdername == "")
                {
                    return new ApiResult() { isSuccess = false, message = "Account Holder Name is required!" };
                }

                if (model.accounttype == null || model.accounttype == "")
                {
                    return new ApiResult() { isSuccess = false, message = "Account Type is required!" };
                }

                if (model.bankname == null || model.bankname == "" )
                {
                    return new ApiResult() { isSuccess = false, message = "Bank Name is required!" };
                }

                if (model.bsbnumber == null || model.bsbnumber == "")
                {
                    return new ApiResult() { isSuccess = false, message = "BSB Number is required!" };
                }

                if (model.accountnumber == null || model.accountnumber == "")
                {
                    return new ApiResult() { isSuccess = false, message = "Account Number is required!" };
                }

                int id = bankDetailsService.InsertBankDetails(model.userid,model.holdername,model.accounttype,model.bankname,model.bsbnumber,model.accountnumber,model.isprimary);

                if (id != null && id > 0)
                {
                    return new ApiResult() { isSuccess = true, data = "Bank Details added succesfully" };
                }
                else
                {
                    return new ApiResult() { isSuccess = true, data = "Something went wrong. Please try again!" };
                }
            }
            catch (Exception ex)
            {
                return new ApiResult() { isSuccess = false, message = ex.Message };
            }
        }

        [HttpGet]
        public ApiResult GetUserBankDetails(int userId)
        {
            try
            {
                if (userId == null || userId == 0)
                {
                    return new ApiResult() { isSuccess = false, message = "User Id is required!" };
                }

                List<BankDetailModel> bankDetails = bankDetailsService.GetUserBankDetails(userId);

                return new ApiResult() { isSuccess = true, data = bankDetails };
            }
            catch (Exception ex)
            {
                return new ApiResult() { isSuccess = false, message = ex.Message };
            }
        }

        #region Stripe Payment
        [HttpPost]
        public ApiResult CreatePaymentIntent([FromBody] PaymentIntentModel stripePayment)
        {
            // Set your secret key. Remember to switch to your live secret key in production.
            // See your keys here: https://dashboard.stripe.com/apikeys
            //StripeConfiguration.ApiKey = "sk_test_tR3PYbcVNZZ796tH88S4VQ2u";

            if (stripePayment == null)
            {
                return new ApiResult() { isSuccess = false, message = "Bad Request" };
            }
            else if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                var errorList = errors.Select(x => x.ErrorMessage).Where(x => !string.IsNullOrEmpty(x)).ToList();
                var excepList = errors.Where(x => x.Exception != null).Select(x => x.Exception.Message).ToList();
                errorList.AddRange(excepList);
                return new ApiResult() { isSuccess = false, message = string.Join(",", errorList) };
            }
            var custCreate = new CustomerService();
            var paymentIntent = new PaymentIntent();
            var setupIntent = new SetupIntent();

            if (string.IsNullOrEmpty(stripePayment.Stripe_Customer_Id))
            {

                if (string.IsNullOrEmpty(stripePayment.Name))
                {
                    return new ApiResult() { isSuccess = false, message = "Customer Name is required" };
                }
                var customeroption = new CustomerCreateOptions();

                customeroption.Name = stripePayment.Name;
                if (stripePayment.Currency.ToLower() != "inr")
                {
                    customeroption.Address = new AddressOptions
                    {
                        Line1 = "sdad",
                        City = "NewYork",
                        Country = "US",
                        PostalCode = "11002",
                    };

                };

                var customerservice = new CustomerService();
                var customer = customerservice.Create(customeroption);
                stripePayment.Stripe_Customer_Id = customer.Id;
            }

            //bool isSuccess = siteUserService.UpdateUserStripeId(stripePayment.UserId, stripePayment.Stripe_Customer_Id);
            //if (!isSuccess)
            //{
            //    return new ApiResult() { IsSuccess = false, Message = "Intent failed" };
            //}


            var options1 = new PaymentIntentCreateOptions
            {
                PaymentMethodTypes = new List<string>
                      {
                         "card",
                        },
              
                Amount = stripePayment.Amount,
                Currency = stripePayment.Currency,
                Customer = stripePayment.Stripe_Customer_Id,
                Description = "Onya Package Charges",
            };

            var service1 = new PaymentIntentService();
            var _paymentIntent = service1.Create(options1);


            
            var clientSecret = _paymentIntent.ClientSecret;

            return new ApiResult() { isSuccess = true, message = "Payment intent created", data = new { ClientSecret = clientSecret, StripeCustomerId = stripePayment.Stripe_Customer_Id } };

            // return Json(new { clientSecret = paymentIntent.ClientSecret });
        }

        #endregion
    }
}
