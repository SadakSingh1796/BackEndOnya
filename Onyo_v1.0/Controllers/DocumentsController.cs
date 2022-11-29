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

namespace Onyo_v1._0.Controllers
{
    [ApiController]
    [Route("api/[controller]/{action}/")]
    public class DocumentsController : Controller
    {
        private readonly ILogger<DocumentsController> _logger;
        private DocumentsService documentsService = new DocumentsService();
        private UserAuthService userAuthService = new UserAuthService();

        public DocumentsController(ILogger<DocumentsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ApiResult AddDocuments(AddDocumentRequestModel model)
        {
            try
            {
                if (model.userid == null || model.userid == 0)
                {
                    return new ApiResult() { isSuccess = false, message = "User Id is required!" };
                }

                if (model.passport == null || model.passport == "")
                {
                    return new ApiResult() { isSuccess = false, message = "Passport is required!" };
                }

                if (model.license == null || model.license == "")
                {
                    return new ApiResult() { isSuccess = false, message = "License is required!" };
                }

                if (model.governmentid == null || model.governmentid == "")
                {
                    return new ApiResult() { isSuccess = false, message = "Government Id is required!" };
                }

                int insertprofilepic = userAuthService.UpdateProfilePic(model.profile, model.userid);

                int insertprofile = documentsService.InsertDocument(model.userid, "Profile", model.profile);

                int insertlicense = documentsService.InsertDocument(model.userid, "License", model.license);

                int insertpassport = documentsService.InsertDocument(model.userid, "Passport", model.passport);

                int insertgovernmentid = documentsService.InsertDocument(model.userid, "Government Id", model.governmentid);

                if (insertgovernmentid != null && insertgovernmentid > 0)
                {
                    return new ApiResult() { isSuccess = true, data = "Documents added succesfully" };
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

    }
}
