using FluentValidation.Results;
using System.Net;

namespace LeilaoCarro.Exceptions
{
    public class AppException : Exception
    {
        private const HttpStatusCode DefaultStatusCode = HttpStatusCode.BadRequest;
        public HttpStatusCode StatusCode { get; init; }
        public IDictionary<string, IEnumerable<string>> Erros { get; init; }

        public AppException(HttpStatusCode statusCode = DefaultStatusCode)
        {
            Erros = new Dictionary<string, IEnumerable<string>>();
            StatusCode = statusCode;
        }

        public AppException(string message, HttpStatusCode statusCode = DefaultStatusCode)
            : base(message)
        {
            Erros = new Dictionary<string, IEnumerable<string>>();
            StatusCode = statusCode;
        }

        public AppException(string message, Exception inner, HttpStatusCode statusCode = DefaultStatusCode)
            : base(message, inner)
        {
            Erros = new Dictionary<string, IEnumerable<string>>();
            StatusCode = statusCode;
        }

        public AppException(string message, IEnumerable<ValidationFailure> validationFailures, HttpStatusCode statusCode = DefaultStatusCode)
            : this(message, statusCode)
        {
            var errorsList = validationFailures.GroupBy(e => e.PropertyName);
            Erros = errorsList.ToDictionary(e => e.Key, e => e.Select(s => s.ErrorMessage));
        }
    }
}
