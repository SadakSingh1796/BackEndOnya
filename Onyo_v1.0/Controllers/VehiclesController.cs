using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnyaServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static OnyaModels.CommonModel;
using static OnyaModels.VehiclesModel;

namespace Onyo_v1._0.Controllers
{
    [ApiController]
    [Route("api/[controller]/{action}/")]
    public class VehiclesController : Controller
    {
        private readonly ILogger<VehiclesController> _logger;
        private VehiclesService vehiclesService = new VehiclesService();

        public VehiclesController(ILogger<VehiclesController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ApiResult AddVehicle(AddVehicleRequestModel model)
        {
            try
            {
                if (model.userid == null || model.userid == 0)
                {
                    return new ApiResult() { isSuccess = false, message = "User Id is required!" };
                }

                if (model.registrationnumber == null || model.registrationnumber == "")
                {
                    return new ApiResult() { isSuccess = false, message = "Vehicle Registration Number is required!" };
                }

                if (model.vehicletype == null || model.vehicletype == "")
                {
                    return new ApiResult() { isSuccess = false, message = "Vehicle Type is required!" };
                }

                if (model.photofront == null || model.photofront == "" )
                {
                    return new ApiResult() { isSuccess = false, message = "Vehicle Picture is required!" };
                }

                int vehicleId = vehiclesService.InsertVehicle(model.userid, model.registrationnumber,model.vehicletype,model.photofront,model.photoback,model.isregisteredentity,model.registeredownername,model.owntrailer,model.regonumber);

                if (vehicleId != null && vehicleId > 0)
                {
                    return new ApiResult() { isSuccess = true, data = "Vehicle added succesfully" };
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
        public ApiResult GetUserVehicles(int userId)
        {
            try
            {
                if (userId == null || userId == 0)
                {
                    return new ApiResult() { isSuccess = false, message = "User Id is required!" };
                }

                List<VehicleModel> vehicles = vehiclesService.GetUserVehicles(userId);

                return new ApiResult() { isSuccess = true, data = vehicles };
            }
            catch (Exception ex)
            {
                return new ApiResult() { isSuccess = false, message = ex.Message };
            }
        }

    }
}
