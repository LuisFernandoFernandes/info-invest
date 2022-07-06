using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using trade.application.generics.models;

namespace trade.application.validations
{
    public class HandlerErrorExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private string GetBodyFromRequest(HttpActionExecutedContext context)
        {
            string data;
            using (var stream = context.Request.Content.ReadAsStreamAsync().Result)
            {
                if (stream.CanSeek) { stream.Position = 0; }
                data = context.Request.Content.ReadAsStringAsync().Result;
            }
            return data;
        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var metodo = $"{actionExecutedContext.Request.Method}";
            var url = $"{actionExecutedContext.Request.RequestUri}";
            var body = GetBodyFromRequest(actionExecutedContext);

            var tratarErro = !((actionExecutedContext.Exception.InnerException != null) && (actionExecutedContext.Exception.InnerException?.Message == "Inter_CRUDValidationException"));

            if (tratarErro)
            {
                var messageException = actionExecutedContext.Exception.Message;
                var messageFront = $@"Método: {metodo} Url: {url} Erro Técnico: {messageException}";

                var modelState = new InvalidResult<Exception>(messageFront) { message = messageException };
                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.InternalServerError, modelState);
            }
            else { actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.OK, new InvalidResult<Exception>(actionExecutedContext.Exception.Message) { message = actionExecutedContext.Exception.Message }); }
        }
    }
}