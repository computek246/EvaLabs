using System.Collections.Generic;
using System.Net;

namespace EvaLabs.ViewModels.Common.Response
{
    public partial class ResponseVm<TResult>
    {
        public static ResponseVm<TResult> Ok(Result<TResult> result)
        {
            return new()
            {
                Data = result.Data,
                Messages = result.Messages,
                StatusCode = HttpStatusCode.OK,
            };
        }

        public static ResponseVm<TResult> Ok(TResult data, params string[] messages)
        {
            return new()
            {
                Data = data,
                Messages = messages,
                StatusCode = HttpStatusCode.OK,
            };
        }

        public static ResponseVm<TResult> Ok(TResult data, IList<string> messages)
        {
            return new()
            {
                Data = data,
                Messages = messages,
                StatusCode = HttpStatusCode.OK
            };
        }

        public static ResponseVm<TResult> BadRequest(Result<TResult> result)
        {
            return new()
            {
                Messages = result.Messages,
                StatusCode = HttpStatusCode.BadRequest
            };
        }

        public static ResponseVm<TResult> BadRequest(IList<string> errors)
        {
            return new()
            {
                Messages = errors,
                StatusCode = HttpStatusCode.BadRequest
            };
        }

        public static ResponseVm<TResult> BadRequest(TResult data, IList<string> errors)
        {
            return new()
            {
                Data = data,
                Messages = errors,
                StatusCode = HttpStatusCode.BadRequest
            };
        }

        public static ResponseVm<TResult> UnAuthorized(IList<string> errors = null)
        {
            return new()
            {
                Messages = errors,
                StatusCode = HttpStatusCode.Unauthorized
            };
        }
    }
}