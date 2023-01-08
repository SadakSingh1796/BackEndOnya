using Microsoft.AspNetCore.Mvc;
using OnyaModels;
using OnyaServices;
using System.Threading.Tasks;

namespace Onyo_v1._0.Controllers
{
    [Route("api/notification")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [Route("send")]
        [HttpPost]
        public async Task<IActionResult> SendNotification(NotificationModel notificationModel)
        {
            ResponseModel result = await _notificationService.SendNotification(notificationModel);
            return Ok(result);
        }
    }
}
