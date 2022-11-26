using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnyaServices;
using System;
using static OnyaModels.BankDetailsModel;
using System.Collections.Generic;
using static OnyaModels.CommonModel;
using static OnyaModels.DocumentsModel;
using static OnyaModels.UserAuthModel;
using static OnyaModels.VehiclesModel;
using static OnyaModels.HomeModel;
using static OnyaModels.UserOnyaModel;

namespace Onyo_v1._0.Controllers
{
    [ApiController]
    [Route("api/[controller]/{action}/")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserAuthService userAuthService = new UserAuthService();
        private VehiclesService vehiclesService = new VehiclesService();
        private DocumentsService documentsService = new DocumentsService();
        private BankDetailsService bankDetailsService = new BankDetailsService();
        private UserOnyaService onyaService = new UserOnyaService();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ApiResult HomeCollection(int userid)
        {
            try
            {
                if (userid == 0)
                {
                    return new ApiResult() { isSuccess = false, message = "User id is required!" };
                }

                HomeUserModel userModel = userAuthService.GetUserById(userid);
                List<VehicleModel> vehicleModels = vehiclesService.GetUserVehicles(userid);
                List<DocumentModel> documentModel = documentsService.GetUserDocuments(userid);
                List<BankDetailModel> bankDetailModels = bankDetailsService.GetUserBankDetails(userid);
                List<OnyaModel> sendingOnyas = onyaService.GetUserOnyas(userid);
                List<OnyaModel> deliveringOnyas = onyaService.GetDriverOnyas(userid);

                HomeCollection homeCollection = new HomeCollection();
                homeCollection.userModel = userModel;
                homeCollection.vehicleModels = vehicleModels;
                homeCollection.documentModel = documentModel;
                homeCollection.bankDetailModels = bankDetailModels;
                homeCollection.sendingOnyas = sendingOnyas;
                homeCollection.deliveringOnyas = deliveringOnyas;

                if (userid > 0)
                {
                    return new ApiResult() { isSuccess = true, message = "Success!",data = homeCollection };
                }
                else
                {
                    return new ApiResult() { isSuccess = false, message = "Something went wrong. Please try again!" };
                }
            }
            catch (Exception ex)
            {
                return new ApiResult() { isSuccess = false, message = ex.Message };
            }
        }

    }
}
