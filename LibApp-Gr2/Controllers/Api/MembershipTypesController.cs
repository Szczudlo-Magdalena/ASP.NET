using AutoMapper;
using LibApp.Dtos;
using LibApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace LibApp.Controllers.Api
{
    // kontroler przetwarzający informacje o rodzajach członkostwa (API)
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipTypesController : ControllerBase
    {
        private readonly MembershipTypeRepository repository;
        private IMapper mapper;

        public MembershipTypesController(MembershipTypeRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<MembershipTypeDto> Get()
        {
            var types = repository.GetAll().Select(mapper.Map<MembershipTypeDto>);

            return types;
        }
    }
}
