using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Core.BasicsStatus;
using SchoolProject.Core.Features.Email.Commands.Models;
using SchoolProject.Core.Localization;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Email.Commands.Handler
{
    public class EmailCommandHandler : ResponseHandler, IRequestHandler<SendEmaiCommand, Response<string>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IEmailService _emailService;
        public EmailCommandHandler(IStringLocalizer<SharedResources> stringLocalizer, IEmailService emailService) : base(stringLocalizer)
        {
            _emailService = emailService;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<string>> Handle(SendEmaiCommand request, CancellationToken cancellationToken)
        {
            var result = await _emailService.SendEmail(request.Email, request.Message, null);
            if (result == "Success")
                return Success<string>("");
            return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.EmailSendFailed]);
        }
    }
}
