using CorePush.Google;
using Microsoft.Extensions.Configuration;
using OnyaModels;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static OnyaModels.DocumentsModel;
using static OnyaModels.GoogleNotification;
using static OnyaModels.VehiclesModel;

namespace OnyaServices
{
    public interface INotificationService
    {
        Task<ResponseModel> SendNotification(SendNotificationModel notificationModel);
    }

    public class NotificationService : INotificationService
    {
        private readonly FcmNotificationSetting _fcmNotificationSetting;
        private QueryHelper helper;
        public IConfiguration Configuration;

        public NotificationService()
        {
            helper = new QueryHelper();
            _fcmNotificationSetting = new FcmNotificationSetting();
        }

        public async Task<ResponseModel> SendNotification(SendNotificationModel notificationModel)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                if (notificationModel.IsAndroiodDevice)
                {
                    /* FCM Sender (Android Device) */
                    FcmSettings settings = new FcmSettings()
                    {
                        SenderId = "127365879570",
                        ServerKey = "AAAAHaeZFxI:APA91bGDXJ7IgT_G-BizvPYYLqiYwaKYIAGhqK3o15Ej00phZu_V0mwihFywZ8zvaBgil0L3Tv0uYrHcSLNsrVxACZ4_itsVKBSpJZSSOENpS-sGc-irvu6236SqVYP82-E9zPlKCtpe"
                    };
                    HttpClient httpClient = new HttpClient();

                    string authorizationKey = string.Format("keyy={0}", settings.ServerKey);
                    string deviceToken = notificationModel.DeviceId;

                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
                    httpClient.DefaultRequestHeaders.Accept
                            .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    DataPayload dataPayload = new DataPayload();
                    dataPayload.Title = notificationModel.Title;
                    dataPayload.Body = notificationModel.Body;

                    GoogleNotification notification = new GoogleNotification();
                    notification.Data = dataPayload;
                    //notification.Notification = dataPayload;

                    var fcm = new FcmSender(settings, httpClient);
                    var fcmSendResponse = await fcm.SendAsync(deviceToken, notification);

                    if (fcmSendResponse.IsSuccess())
                    {
                        response.IsSuccess = true;
                        response.Message = "Notification sent successfully";
                        return response;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = fcmSendResponse.Results[0].Error;
                        return response;
                    }
                }
                else
                {
                    /* Code here for APN Sender (iOS Device) */
                    //var apn = new ApnSender(apnSettings, httpClient);
                    //await apn.SendAsync(notification, deviceToken);
                }
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Something went wrong";
                return response;
            }
        }

        public int InsertNotification(int onyaid, int driverid, int userid, string title, string body, int type)
        {
            try
            {
                string query = string.Format(@" insert into dbo.tbl_notification(onyaid, driverid, userid, title, body, type)
                values(@onyaid, @driverid, @userid, @title, @body, @type) RETURNING notificationid;");
                return helper.InsertAndGetId(query, new { onyaid = onyaid, driverid = driverid, userid = userid, title = title, body = body, type = type });
            }
            catch (Exception ex)
            {

            }

            return 0;
        }

        public List<NotificationModel> GetUserNotifications(int userid)
        {
            try
            {
                string query = string.Format(@" select onyaid, driverid, userid, title, body, type from dbo.tbl_notification where userid = @userid");
                return helper.GetList<NotificationModel>(query, new { userid = userid });
            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }

}
