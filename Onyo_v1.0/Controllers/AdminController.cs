using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnyaServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static OnyaModels.CommonModel;
using static OnyaModels.DocumentsModel;
using static OnyaModels.UserAuthModel;
using static OnyaModels.UserOnyaModel;
using static OnyaModels.VehiclesModel;

namespace Onyo_v1._0.Controllers
{
    [ApiController]
    [Route("api/[controller]/{action}/")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;

        private UserAuthService userAuthService = new UserAuthService();
        private DocumentsService documentsService = new DocumentsService();
        private UserOnyaService onyaService = new UserOnyaService();

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ApiResult GetAllUsers()
        {
            try
            {
                List<AllUserModel> users = userAuthService.GetAllUsers();
                return new ApiResult() { isSuccess = true, data = users };
            }
            catch (Exception ex)
            {
                return new ApiResult() { isSuccess = false, message = ex.Message };
            }
        }

        [HttpGet]
        public ApiResult GetUserDocuments(int userId)
        {
            try
            {
                if (userId == null || userId == 0)
                {
                    return new ApiResult() { isSuccess = false, message = "User Id is required!" };
                }

                DocumentModel document = documentsService.GetUserDocument(userId);

                List<UserDocument> userDocuments = new List<UserDocument>();

                UserDocument profile = new UserDocument();
                profile.documentType = "Profile Picture";
                profile.documentValue = document.profile;

                UserDocument passport = new UserDocument();
                passport.documentType = "Passport";
                passport.documentValue = document.passport;

                UserDocument license = new UserDocument();
                license.documentType = "License";
                license.documentValue = document.license;

                UserDocument governmentId = new UserDocument();
                governmentId.documentType = "Government Id";
                governmentId.documentValue = document.governmentid;

                userDocuments.Add(profile);
                userDocuments.Add(passport);
                userDocuments.Add(license);
                userDocuments.Add(governmentId);

                return new ApiResult() { isSuccess = true, data = userDocuments };
            }
            catch (Exception ex)
            {
                return new ApiResult() { isSuccess = false, message = ex.Message };
            }
        }

        [HttpGet]
        public ApiResult GetAllOnyas()
        {
            try
            {
                List<OnyaModel> onyas = onyaService.GetAllOnyas();

                return new ApiResult() { isSuccess = true, data = onyas };
            }
            catch (Exception ex)
            {
                return new ApiResult() { isSuccess = false, message = ex.Message };
            }
        }

    }
}
