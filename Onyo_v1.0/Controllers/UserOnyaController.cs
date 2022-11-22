using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnyaServices;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using static OnyaModels.CommonModel;
using static OnyaModels.UserOnyaModel;

namespace Onyo_v1._0.Controllers
{
    [ApiController]
    [Route("api/[controller]/{action}/")]
    public class OnyaController : Controller
    {
        private readonly ILogger<OnyaController> _logger;
        private UserOnyaService onyaService = new UserOnyaService();

        public OnyaController(ILogger<OnyaController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ApiResult CreateOnya(CreateOnyaRequestModel model)
        {
            try
            {
                if (model.userid == null || model.userid == 0)
                {
                    return new ApiResult() { isSuccess = false, message = "User Id is required!" };
                }

                DateTime dateTime = DateTime.ParseExact(model.pickupdate, "yyyy-MM-dd HH:mm:ss", null);

                int onyaId = onyaService.CreateOnya(model.userid,model.packagesize,model.packageweight,model.packagetype,model.comments,
                    dateTime,model.pickuplat,model.pickuplong,model.pickupaddress,model.droplat,model.droplong,model.dropaddress,
                    model.pickuppoint,model.droppoint,model.pickupslot,model.dropslot,model.receivername, model.receiveremail,model.receiverphone,model.amount,model.cancounter);

                if (onyaId != null && onyaId > 0)
                {
                    foreach(String image in model.images)
                    {
                        onyaService.InsertOnyaImages(onyaId, image, 1);
                    }

                    return new ApiResult() { isSuccess = true, data = "Onya created succesfully" };
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
        public ApiResult GetUserOnyas(int userId)
        {
            try
            {
                if (userId == null || userId == 0)
                {
                    return new ApiResult() { isSuccess = false, message = "User Id is required!" };
                }

                List<OnyaModel> onyas = onyaService.GetUserOnyas(userId);

                return new ApiResult() { isSuccess = true, data = onyas };
            }
            catch (Exception ex)
            {
                return new ApiResult() { isSuccess = false, message = ex.Message };
            }
        }

        [HttpGet]
        public ApiResult GetAllOtherOnyas(int userId)
        {
            try
            {
                if (userId == null || userId == 0)
                {
                    return new ApiResult() { isSuccess = false, message = "User Id is required!" };
                }

                List<OnyaModel> onyas = onyaService.AllOtherOnyas(userId);

                return new ApiResult() { isSuccess = true, data = onyas };
            }
            catch (Exception ex)
            {
                return new ApiResult() { isSuccess = false, message = ex.Message };
            }
        }

        [HttpGet]
        public ApiResult GetDriverOnyas(int userId)
        {
            try
            {
                if (userId == null || userId == 0)
                {
                    return new ApiResult() { isSuccess = false, message = "User Id is required!" };
                }

                List<OnyaModel> vehicles = onyaService.GetDriverOnyas(userId);

                return new ApiResult() { isSuccess = true, data = vehicles };
            }
            catch (Exception ex)
            {
                return new ApiResult() { isSuccess = false, message = ex.Message };
            }
        }

    }
}
