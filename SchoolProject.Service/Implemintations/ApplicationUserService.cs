using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrustructure.Data;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Service.Implemintations
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationService _authenticationService;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHttpContextAccessor _httpContextAccesor;
        private readonly IEmailService _emailService;
        private readonly IUrlHelper _urlHelper;
        public ApplicationUserService(UserManager<User> userManager,
            IAuthenticationService authenticationService, ApplicationDbContext applicationDbContext,
            IEmailService emailService, IHttpContextAccessor httpContextAccesor, IUrlHelper urlHelper)
        {
            _userManager = userManager;
            _authenticationService = authenticationService;
            _applicationDbContext = applicationDbContext;
            _emailService = emailService;
            _httpContextAccesor = httpContextAccesor;
            _urlHelper = urlHelper;
        }

        public async Task<string> AddUserAsync(User user, string password)
        {
            var trans = await _applicationDbContext.Database.BeginTransactionAsync();
            try
            {
                //check exist email 
                var Existuser = await _userManager.FindByEmailAsync(user.Email);
                if (Existuser != null)
                    return "EmailIsExist";

                //check exist username
                var Userbyusername = await _userManager.FindByNameAsync(user.UserName);
                if (Userbyusername != null)
                    return "UserNameIsExist";

                // create user
                var createdresult = await _userManager.CreateAsync(user, password);
                if (!createdresult.Succeeded)
                {
                    return String.Join(",", createdresult.Errors.Select(x => x.Description).ToList());
                }

                // add role to user
                await _userManager.AddToRoleAsync(user, "User");

                // generate confirm email
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var requestAccessor = _httpContextAccesor.HttpContext?.Request;

                var requestUrl = requestAccessor?.Scheme + "://" + requestAccessor.Host + _urlHelper.Action(new UrlActionContext { Action = "ConfirmEmail", Controller = "Authentication", Values = new { userId = user.Id, code = code } });

                var requestMessage = $"to confirm email click link: {requestUrl}";

                var sendEmailResult = await _emailService.SendEmail(user.Email, requestMessage, "Confirm Email");

                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "FailedToAddUser";
            }
        }
    }
}
