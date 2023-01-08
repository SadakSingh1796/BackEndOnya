using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnyaServices;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using static OnyaModels.CommonModel;
using static OnyaModels.HomeModel;
using static OnyaModels.UserOnyaModel;

namespace Onyo_v1._0.Controllers
{
    [ApiController]
    [Route("api/[controller]/{action}/")]
    public class OnyaController : Controller
    {
        private readonly ILogger<OnyaController> _logger;
        private UserOnyaService onyaService = new UserOnyaService();
        private UserAuthService userAuthService = new UserAuthService();

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

                foreach(OnyaModel onya in onyas)
                {
                    List<String> onyaImages = onyaService.GetOnyaImages(onya.onyaid);
                    onya.images = onyaImages;
                }

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

        [HttpPost]
        public ApiResult SendOnyaRequest(OnyaRequestModel model)
        {
            try
            {
                if (model.driverid == null || model.driverid == 0)
                {
                    return new ApiResult() { isSuccess = false, message = "Driver Id is required!" };
                }

                if (model.onyaid == null || model.onyaid == 0)
                {
                    return new ApiResult() { isSuccess = false, message = "Onya Id is required!" };
                }

                int requestid = onyaService.InsertOnyaRequests(model.onyaid, model.driverid);



                if (requestid != null && requestid > 0)
                {
                    return new ApiResult() { isSuccess = true, data = "Request sent succesfully" };
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

        [HttpPost]
        public ApiResult RespondOnyaRequest(OnyaRespondModel model)
        {
            try
            {
                if (model.driverid == null || model.driverid == 0)
                {
                    return new ApiResult() { isSuccess = false, message = "Driver Id is required!" };
                }

                if (model.onyaid == null || model.onyaid == 0)
                {
                    return new ApiResult() { isSuccess = false, message = "Onya Id is required!" };
                }

                if (model.isaccepted)
                {
                    int id = onyaService.UpdateDriverId(model.onyaid, model.driverid);

                    if (id != null && id > 0)
                    {
                        return new ApiResult() { isSuccess = true, data = "Request accepted succesfully" };
                    }
                    else
                    {
                        return new ApiResult() { isSuccess = true, data = "Something went wrong. Please try again!" };
                    }
                }
                else
                {
                    int id = onyaService.DeleteDriverRequest(model.onyaid, model.driverid);

                    if (id != null && id > 0)
                    {
                        return new ApiResult() { isSuccess = true, data = "Request denied succesfully" };
                    }
                    else
                    {
                        return new ApiResult() { isSuccess = true, data = "Something went wrong. Please try again!" };
                    }
                }

                return new ApiResult() { isSuccess = true, data = "Something went wrong. Please try again!" };
            }
            catch (Exception ex)
            {
                return new ApiResult() { isSuccess = false, message = ex.Message };
            }
        }

        [HttpGet]
        public ApiResult GetOnyaDetails(int onyaid)
        {
            try
            {
                if (onyaid == null || onyaid == 0)
                {
                    return new ApiResult() { isSuccess = false, message = "Onya Id is required!" };
                }

                OnyaModel onya = onyaService.GetOnyaDetails(onyaid);

                onya.images = onyaService.GetOnyaImages(onyaid);

                return new ApiResult() { isSuccess = true, data = onya };
            }
            catch (Exception ex)
            {
                return new ApiResult() { isSuccess = false, message = ex.Message };
            }
        }
    }
}
