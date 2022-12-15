using AutoMapper;
using DocStream.Core.Interfaces;
using DocStream.Core.Token;
using DocStream.Dtos.DirectorDtos;
using DocStream.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocStream.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DirectorController> _logger;
        private readonly IMapper _mapper;

        public DirectorController(IUnitOfWork unitOfWork, ILogger<DirectorController> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDirectors([FromQuery] RequestParams requestParams)
        {
            var directors = await _unitOfWork.Directors.GetPagedList(requestParams);
            var results = _mapper.Map<IList<DirectorDto>>(directors);
            return Ok(results);
        }

        [HttpGet("{id:int}", Name = "GetDirector")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDirector(int id)
        {

            var director = await _unitOfWork.Directors.Get(q => q.Id == id);
            var result = _mapper.Map<DirectorDto>(director);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateDirector([FromBody] CreateDirectorDto directorDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateDirector)}");
                return BadRequest(ModelState);
            }

            var director = _mapper.Map<Director>(directorDto);
            await _unitOfWork.Directors.Insert(director);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetDirector", new { id = director.Id }, director);

        }

        [Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateDirector(int id, [FromBody] UpdateDirectorDto directorDto)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateDirector)}");
                return BadRequest(ModelState);
            }

            var director = await _unitOfWork.Directors.Get(q => q.Id == id);
            if (director == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateDirector)}");
                return BadRequest("Submitted data is invalid");
            }

            _mapper.Map(directorDto, director);
            _unitOfWork.Directors.Update(director);
            await _unitOfWork.Save();

            return NoContent();

        }

        [Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteDirector(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteDirector)}");
                return BadRequest();
            }

            var director = await _unitOfWork.Directors.Get(q => q.Id == id);
            if (director == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteDirector)}");
                return BadRequest("Submitted data is invalid");
            }

            await _unitOfWork.Directors.Delete(id);
            await _unitOfWork.Save();

            return NoContent();
        }
    }
}
