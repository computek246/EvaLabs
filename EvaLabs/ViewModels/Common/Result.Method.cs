using System;
using System.Collections.Generic;
using System.Linq;

namespace EvaLabs.ViewModels.Common
{
    public partial class Result<TResult>
    {
        public static Result<TResult> Success(TResult data)
        {
            return new()
            {
                Data = data,
                IsSucceeded = true
            };
        }

        public static Result<TResult> Success(TResult data, List<string> messages)
        {
            return new()
            {
                Data = data,
                IsSucceeded = true,
                Messages = messages
            };
        }

        public static Result<TResult> Success(TResult data, params string[] messages)
        {
            return new()
            {
                Data = data,
                Messages = messages.Any() ? messages.ToList() : null,
                IsSucceeded = true
            };
        }

        public static Result<TResult> Failed(params string[] messages)
        {
            return new()
            {
                Messages = messages.Any() ? messages.ToList() : null,
                IsSucceeded = false
            };
        }

        public static Result<TResult> Failed(Exception exception)
        {
            var messages = new List<string> {exception.Message};
            if (exception.InnerException != null)
            {
                messages.AddRange(exception.InnerException.Message.Split(Environment.NewLine));
            }

            return new Result<TResult>
            {
                Messages = messages,
                IsSucceeded = false
            };
        }

        public static Result<TResult> Failed(List<string> messages)
        {
            return new()
            {
                Messages = messages,
                IsSucceeded = false
            };
        }

        public static Result<TResult> Failed(TResult data, List<string> messages)
        {
            return new()
            {
                Data = data,
                Messages = messages,
                IsSucceeded = false
            };
        }
    }
}