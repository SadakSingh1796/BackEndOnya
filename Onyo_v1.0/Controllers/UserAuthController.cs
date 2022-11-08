using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnyaServices;
using System;
using static OnyaModels.CommonModel;
using static OnyaModels.UserAuthModel;

namespace Onyo_v1._0.Controllers
{
    [ApiController]
    [Route("api/[controller]/{action}/")]
    public class UserAuthController : Controller
    {
        private readonly ILogger<UserAuthController> _logger;
        private UserAuthService userAuthService = new UserAuthService();

        public UserAuthController(ILogger<UserAuthController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ApiResult Login(LoginRequestModel model)
        {
            try
            {
                if (model.type == null || model.type == "")
                {
                    return new ApiResult() { isSuccess = false, message = "Login type is mandatory!" };
                }

                if (model.value == null || model.value == "")
                {
                    return new ApiResult() { isSuccess = false, message = "Login value is mandatory!" };
                }

                UserModel user = userAuthService.GetUser(model);

                if (model.type == "google" && user != null && user.userId > 0)
                {
                    var insertUser = userAuthService.InsertSocialUser(model.name, "", model.value, DateTime.UtcNow, model.notificationToken, model.deviceType);
                    user = userAuthService.GetUser(model);
                }
                else if (model.type == "facebook" && user != null && user.userId > 0)
                {
                    var insertUser = userAuthService.InsertSocialUser(model.name, model.value, "", DateTime.UtcNow, model.notificationToken, model.deviceType);
                    user = userAuthService.GetUser(model);
                }

                if (user != null && user.userId > 0)
                {
                    return new ApiResult() { isSuccess = true, data = user };
                }
                else
                {
                    return new ApiResult() { isSuccess = false, message = "User not found!" };
                }
            }
            catch (Exception ex)
            {
                return new ApiResult() { isSuccess = false, message = ex.Message };
            }
        }

        [HttpPost]
        public ApiResult Signup(SignupRequestModel model)
        {
            try
            {
                if (model.name == null || model.name == "")
                {
                    return new ApiResult() { isSuccess = false, message = "Name is required!" };
                }

                if (model.email == null || model.email == "")
                {
                    return new ApiResult() { isSuccess = false, message = "Email is required!" };
                }

                if (model.phone == null || model.phone == "")
                {
                    return new ApiResult() { isSuccess = false, message = "Phone is required!" };
                }

                UserModel user = userAuthService.GetUserByEmailOrPhone(model.email,model.phone);

                if (user != null && user.userId > 0)
                {
                    return new ApiResult() { isSuccess = false, message = "User already exists!" };
                }

                int response = userAuthService.InsertUser(model.name, model.email, model.phone, DateTime.UtcNow, model.notificationToken, model.deviceType);

                if (response > 0)
                {
                    return new ApiResult() { isSuccess = true, message = "User created successfully!" };
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
