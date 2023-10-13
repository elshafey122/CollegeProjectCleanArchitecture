using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Core.BasicsStatus;
using SchoolProject.Core.Features.Departements.Commands.Models;
using SchoolProject.Core.Localization;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Departements.Commands.Hnadlers
{
    public class DepartementHandlerCommand : ResponseHandler, IRequestHandler<AddDepartementCommand, Response<string>>,
                                                              IRequestHandler<EditDepartementCommand, Response<string>>,
                                                              IRequestHandler<DeleteDepartementCommand, Response<string>>
    {
        private readonly IDeparetementService _deparetementService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        public DepartementHandlerCommand(IDeparetementService deparetementService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _deparetementService = deparetementService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }
        public async Task<Response<string>> Handle(AddDepartementCommand request, CancellationToken cancellationToken)
        {
            var departement = _mapper.Map<Departement>(request);
            var result = await _deparetementService.AddDepartement(departement);
            if (result == "Exist")
                return UnprocessableEntity<string>($"name: {_stringLocalizer[SharedResourcesKeys.IsExist]}");
            else if (result == "Success")
                return Created(result);
            return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(EditDepartementCommand request, CancellationToken cancellationToken)
        {
            var departement = await _deparetementService.GetDepartementById(request.DId);
            if (departement == null)
            {
                return NotFound<string>();
            }
            var mapper = _mapper.Map<Departement>(request);
            var result = await _deparetementService.EditDepartement(mapper);
            if (result != "Success")
            {
                return BadRequest<string>();
            }
            return Created<string>(_stringLocalizer[SharedResourcesKeys.Created]);
        }

        public async Task<Response<string>> Handle(DeleteDepartementCommand request, CancellationToken cancellationToken)
        {
            var result = await _deparetementService.DeleteDepartement(request.DId);
            if (result == "NotFound")
            {
                return NotFound<string>();
            }
            else if (result == "Success")
            {
                return Deleteed<string>();
            }
            else
            {
                return BadRequest<string>();
            }
        }
    }
}
