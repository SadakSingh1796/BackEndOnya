using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnyaServices;
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

    }
}
