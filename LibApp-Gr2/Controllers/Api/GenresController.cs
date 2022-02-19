using AutoMapper;
using LibApp.Dtos;
using LibApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LibApp.Controllers.Api
{
    // kontroler przetwarzający informacje o kategoriach (API)
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly GenreRepository repository;
        private IMapper mapper;

        public GenresController(GenreRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var genres = repository.GetAll().Select(g => mapper.Map<GenreDto>(g));

            return Ok(genres);
        }
    }
}
