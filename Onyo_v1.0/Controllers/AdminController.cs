using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnyaServices;
using System;
using System.Collections.Generic;
using static OnyaModels.BusinessAccountModel;
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
        private BusinessAccountService businessAccountService = new BusinessAccountService();

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

                return new ApiResult() { isSuccess = true, data = documents };
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

                int onyaId = userAuthService.VerifyUserDocuments(model.documentid, model.isverified, model.comment);

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

        [HttpPost]
        public ApiResult CreateBusinessAccount(BusinessAccountRequestModel model)
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

                if (model.phonenumber == null || model.phonenumber == "")
                {
                    return new ApiResult() { isSuccess = false, message = "Phone number is required!" };
                }

                if (model.password == null || model.password == "")
                {
                    return new ApiResult() { isSuccess = false, message = "Password is required!" };
                }

                BusinessAccount existingaccount = businessAccountService.GetBusinessAccount(model.email, model.phonenumber);

                if (existingaccount != null && existingaccount.accountid > 0)
                {
                    return new ApiResult() { isSuccess = true, data = "Account already exists" };
                }

                int accountid = businessAccountService.AddBusinessAccount(model.name,model.email,model.phonenumber,model.password,DateTime.Now);

                if (accountid != null && accountid > 0)
                {
                    return new ApiResult() { isSuccess = true, data = "Account created succesfully" };
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
        public ApiResult GetBusinessAccounts()
        {
            try
            {
                List<BusinessAccount> businessAccounts = businessAccountService.GetBusinessAccounts();

                return new ApiResult() { isSuccess = true, data = businessAccounts };
            }
            catch (Exception ex)
            {
                return new ApiResult() { isSuccess = false, message = ex.Message };
            }
        }

        [HttpPost]
        public ApiResult ToggleBusinessAccountRequestModel(ToggleBusinessAccountRequestModel model)
        {
            try
            {
                int accountid = businessAccountService.ToggleBusinessAccount(model.isactive,model.accountid);

                if (accountid != null && accountid > 0)
                {
                    return new ApiResult() { isSuccess = true, data = "Account updated succesfully" };
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
    }
}
