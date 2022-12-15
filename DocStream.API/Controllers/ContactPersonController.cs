using AutoMapper;
using DocStream.Core.Interfaces;
using DocStream.Core.Token;
using DocStream.Dtos.ContactPersonDtos;
using DocStream.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocStream.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactPersonController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ContactPersonController> _logger;
        private readonly IMapper _mapper;

        public ContactPersonController(IUnitOfWork unitOfWork, ILogger<ContactPersonController> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetContactPeople([FromQuery] RequestParams requestParams)
        {
            var contactPeople = await _unitOfWork.ContactPeople.GetPagedList(requestParams);
            var results = _mapper.Map<IList<ContactPersonDto>>(contactPeople);
            return Ok(results);
        }

        [HttpGet("{id:int}", Name = "GetContactPerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetContactPerson(int id)
        {
            
            var contactPerson = await _unitOfWork.ContactPeople.Get(q => q.Id == id);
            var result = _mapper.Map<ContactPersonDto>(contactPerson);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateContactPerson([FromBody] CreateContactPersonDto contactPersonDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateContactPerson)}");
                return BadRequest(ModelState);
            }

            var contactPerson = _mapper.Map<ContactPerson>(contactPersonDto);
            await _unitOfWork.ContactPeople.Insert(contactPerson);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetContactPerson", new { id = contactPerson.Id }, contactPerson);

        }

        [Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateContactPerson(int id, [FromBody] UpdateContactPersonDto applicantDto)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateContactPerson)}");
                return BadRequest(ModelState);
            }

            var contactPerson = await _unitOfWork.ContactPeople.Get(q => q.Id == id);
            if (contactPerson == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateContactPerson)}");
                return BadRequest("Submitted data is invalid");
            }

            _mapper.Map(applicantDto, contactPerson);
            _unitOfWork.ContactPeople.Update(contactPerson);
            await _unitOfWork.Save();

            return NoContent();

        }

        [Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteContactPeople(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteContactPeople)}");
                return BadRequest();
            }

            var contactPerson = await _unitOfWork.ContactPeople.Get(q => q.Id == id);
            if (contactPerson == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteContactPeople)}");
                return BadRequest("Submitted data is invalid");
            }

            await _unitOfWork.ContactPeople.Delete(id);
            await _unitOfWork.Save();

            return NoContent();
        }
    }
}
