using System.Net;

namespace SchoolProject.Api
{
    public record ErrorValidationResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool Success { get; set; } = false;
        public IEnumerable<string> ErrorMessages { get; set; }
    }
}
