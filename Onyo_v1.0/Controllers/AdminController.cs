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

                List<DocumentModel> documents = documentsService.GetUserDocuments(userId);

                List<UserDocument> userDocuments = new List<UserDocument>();

                foreach(DocumentModel document in documents)
                {
                    UserDocument doc = new UserDocument();
                    doc.documentType = document.type;
                    doc.documentValue = document.url;
                    doc.isVerified = document.isverified;
                    doc.comment = document.comment;

                    userDocuments.Add(doc);
                }

                return new ApiResult() { isSuccess = true, data = userDocuments };
            }
            catch (Exception ex)
            {
                return new ApiResult() { isSuccess = false, message = ex.Message };
            }
        }

        [HttpPost]
        public ApiResult UpdateUserStatus(UserAdminVerifyModel model)
        {
            try
            {
                if (model.userid == null || model.userid == 0)
                {
                    return new ApiResult() { isSuccess = false, message = "User Id is required!" };
                }

                if (model.isverified == null)
                {
                    return new ApiResult() { isSuccess = false, message = "Status is required!" };
                }

                int onyaId = userAuthService.UpdateUserStatus(model.isverified, model.userid);

                if (onyaId != null && onyaId > 0)
                {
                    return new ApiResult() { isSuccess = true, data = "User's status updated successfully" };
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
        public ApiResult UpdateDriverStatus(UserAdminVerifyModel model)
        {
            try
            {
                if (model.userid == null || model.userid == 0)
                {
                    return new ApiResult() { isSuccess = false, message = "User Id is required!" };
                }

                if (model.isverified == null)
                {
                    return new ApiResult() { isSuccess = false, message = "Status is required!" };
                }

                int onyaId = userAuthService.UpdateDriverStatus(model.isverified, model.userid);

                if (onyaId != null && onyaId > 0)
                {
                    return new ApiResult() { isSuccess = true, data = "User's status updated successfully" };
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
        public ApiResult VerifyUserDocuments(DocumentVerifyModel model)
        {
            try
            {
                if (model.documentid == null || model.documentid == 0)
                {
                    return new ApiResult() { isSuccess = false, message = "Document Id is required!" };
                }

                if (model.isverified == null)
                {
                    return new ApiResult() { isSuccess = false, message = "Status is required!" };
                }

                int onyaId = userAuthService.VerifyUserDocuments(model.documentid, model.isverified,model.comment);

                if (onyaId != null && onyaId > 0)
                {
                    return new ApiResult() { isSuccess = true, data = "Document verified successfully" };
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

                    DateTime dateTime = DateTime.ParseExact(model.pickupdate, "yyyy-MM-dd HH:mm:ss", null);

                    int onyaId = onyaService.CreateOnya(model.userid, model.packagesize, model.packageweight, model.packagetype, model.comments,
                        dateTime, model.pickuplat, model.pickuplong, model.pickupaddress, model.droplat, model.droplong, model.dropaddress,
                        model.pickuppoint, model.droppoint, model.pickupslot, model.dropslot, model.receivername, model.receiveremail, model.receiverphone, model.amount,
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

        [HttpGet]
        public ApiResult GetUserOnyas(int userId)
        {
            try
            {
                if (userId == null || userId == 0)
                {
                    return new ApiResult() { isSuccess = false, message = "User Id is required!" };
                }

                List<OnyaModel> sendingOnyas = onyaService.GetUserOnyas(userId);

                return new ApiResult() { isSuccess = true, data = sendingOnyas };
            }
            catch (Exception ex)
            {
                return new ApiResult() { isSuccess = false, message = ex.Message };
            }
        }

        [HttpGet]
        public ApiResult GetDeliverOnyas(int userId)
        {
            try
            {
                if (userId == null || userId == 0)
                {
                    return new ApiResult() { isSuccess = false, message = "User Id is required!" };
                }

                List<OnyaModel> deliveringOnyas = onyaService.GetDriverOnyas(userId);

                return new ApiResult() { isSuccess = true, data = deliveringOnyas };
            }
            catch (Exception ex)
            {
                return new ApiResult() { isSuccess = false, message = ex.Message };
            }
        }



    }
}
