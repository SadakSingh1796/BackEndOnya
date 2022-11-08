using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnyaServices;
using System;
using System.Collections.Generic;
using static OnyaModels.CommonModel;
using static OnyaModels.DocumentsModel;
using static OnyaModels.UserAuthModel;
using static OnyaModels.UserOnyaModel;

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

        [HttpPost]
        public ApiResult CreateOnyas(List<CreateOnyaRequestModel> modelArray)
        {
            try
            {
                foreach (CreateOnyaRequestModel model in modelArray)
                {
                    if (model.userid == null || model.userid == 0)
                    {
                        return new ApiResult() { isSuccess = false, message = "User Id is required!" };
                    }

                    int onyaId = onyaService.CreateOnya(model.userid, model.packagesize, model.packageweight, model.packagetype, model.comments,
                        model.pickupdate, model.pickuplat, model.pickuplong, model.pickupaddress, model.droplat, model.droplong, model.dropaddress,
                        model.pickuppoint, model.droppoint, model.pickupslot, model.dropslot, model.receiveremail, model.receiverphone, model.amount,
                        model.cancounter);

                    if (onyaId != null && onyaId > 0)
                    {
                        //do nothing
                    }
                    else
                    {
                        return new ApiResult() { isSuccess = true, data = "Something went wrong. Please try again!" };
                    }
                }

                return new ApiResult() { isSuccess = true, data = "Onya created succesfully" };

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
