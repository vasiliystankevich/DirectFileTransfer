using CommonClasses;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServerSTUNLibrary.Controllers
{
    public class FileController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Download(string id)
        {
            try
            {
                if (String.IsNullOrEmpty(id)) return Request.CreateResponse(HttpStatusCode.BadRequest);
                var appDataDirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");
                var localFilePath = Path.Combine(appDataDirectoryPath, id);
                if (!File.Exists(localFilePath)) return Request.CreateResponse(HttpStatusCode.Gone);
                var result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new StreamContent(new FileStream(localFilePath, FileMode.Open, FileAccess.Read));
                result.Content.Headers.ContentDisposition =
                    new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                    {
                        FileName = id
                    };
                return result;
            }
            catch (Exception exception)
            {
               Globalton<Logger<FileController>>.Instance.Log.Error(exception);
               return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    } 
}
