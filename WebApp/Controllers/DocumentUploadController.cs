using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace LASI.WebApp.Controllers
{
    [Route("DocumentUpload")]
    public class DocumentUploadController : ApiController
    {
        [HttpPost]
        public async Task<HttpResponseMessage> Post() {
            if (Request.Content.IsMimeMultipartContent())
                return await ProcessDocumentUploadRequest();
            return Failure;
        }

        private async Task<HttpResponseMessage> ProcessDocumentUploadRequest() {
            var serverPath = System.Web.HttpContext.Current.Server.MapPath(USER_UPLOADED_DOCUMENTS_DIR);
            var outputStream = new DocumentUploadFormDataStreamProvider(serverPath);
            var streamedData = await Request.Content.ReadAsMultipartAsync(outputStream);
            var responseMessage = Request.CreateResponse(
               statusCode: HttpStatusCode.Created,
               value: JArray.FromObject(
                   streamedData.FileData.Select(datum => new
                   {
                       Uri = serverPath + datum.LocalFileName,
                       Name = datum.LocalFileName
                   })));
            return responseMessage;
        }

        private class DocumentUploadFormDataStreamProvider : MultipartFormDataStreamProvider
        {
            public DocumentUploadFormDataStreamProvider(string directory) : base(directory) { }
            public override string GetLocalFileName(HttpContentHeaders headers) => headers.ContentDisposition.FileName.RemoveSubstrings("\"");
        }

        private HttpResponseMessage Failure => Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "The request was not in the correct format.");

        private const string USER_UPLOADED_DOCUMENTS_DIR = "~/App_Data/UserDocuments/";

    }
}