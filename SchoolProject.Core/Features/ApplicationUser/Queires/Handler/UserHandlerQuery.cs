using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Core.BasicsStatus;
using SchoolProject.Core.Features.ApplicationUser.Queires.Model;
using SchoolProject.Core.Features.ApplicationUser.Queires.NewFolder.Model;
using SchoolProject.Core.Features.ApplicationUser.Queires.ViewModel;
using SchoolProject.Core.Localization;
using SchoolProject.Core.Wrappings;
using SchoolProject.Data.Entities.Identity;
using System.Linq.Expressions;

namespace SchoolProject.Core.Features.ApplicationUser.Queires.Handler
{
    public class UserHandlerQuery : ResponseHandler, IRequestHandler<GetUsersPaginatedQuery, PaginatedResult<GetUserPaginatedResponse>>,
                                    IRequestHandler<GetUserByIdQuery, Response<GetUserByIdResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly UserManager<User> _userManager;

        public UserHandlerQuery(IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer, UserManager<User> userManager) : base(stringLocalizer)
        {
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
        }

        public Task<PaginatedResult<GetUserPaginatedResponse>> Handle(GetUsersPaginatedQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.AsQueryable();
            //sol
            //var PaginatedUsers = users.Select(x => new GetUserPaginatedResponse(x.FullName, x.Email, x.Country, x.Address))
            //.ToPaginateListAsync(request.PageSize, request.PageNumber);
            //return PaginatedUsers;

            Expression<Func<User, GetUserPaginatedResponse>> expression = e => new GetUserPaginatedResponse(e.FullName, e.Email, e.Country, e.Address);
            var userspaginated = users.Select(expression).ToPaginateListAsync(request.PageSize, request.PageNumber);
            return userspaginated;
        }

        public async Task<Response<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (user == null)
            {
                return NotFound<GetUserByIdResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            }
            var result = _mapper.Map<GetUserByIdResponse>(user);
            return Success(result);
        }
    }
}
