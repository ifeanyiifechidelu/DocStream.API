using AutoMapper;
using DocStream.Core.Interfaces;
using DocStream.Core.Token;
using DocStream.Dtos.ApplicantDtos;
using DocStream.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocStream.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ApplicantController> _logger;
        private readonly IMapper _mapper;

        public ApplicantController(IUnitOfWork unitOfWork, ILogger<ApplicantController> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetApplicants([FromQuery] RequestParams requestParams)
        {
            var applicants = await _unitOfWork.Applicants.GetPagedList(requestParams);
            var results = _mapper.Map<IList<ApplicantDto>>(applicants);
            return Ok(results);
        }

        [HttpGet("{id:int}", Name = "GetApplicant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetApplicant(int id)
        {
            
            var applicant = await _unitOfWork.Applicants.Get(q => q.Id == id);
            var result = _mapper.Map<ApplicantDto>(applicant);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateApplicant([FromBody] CreateApplicantDto applicantDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateApplicant)}");
                return BadRequest(ModelState);
            }

            var applicant = _mapper.Map<Applicant>(applicantDto);
            await _unitOfWork.Applicants.Insert(applicant);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetApplicant", new { id = applicant.Id }, applicant);

        }

        [Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateApplicant(int id, [FromBody] UpdateApplicantDto applicantDto)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateApplicant)}");
                return BadRequest(ModelState);
            }

            var applicant = await _unitOfWork.Applicants.Get(q => q.Id == id);
            if (applicant == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateApplicant)}");
                return BadRequest("Submitted data is invalid");
            }

            _mapper.Map(applicantDto, applicant);
            _unitOfWork.Applicants.Update(applicant);
            await _unitOfWork.Save();

            return NoContent();

        }

        [Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteApplicant(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteApplicant)}");
                return BadRequest();
            }

            var applicant = await _unitOfWork.Applicants.Get(q => q.Id == id);
            if (applicant == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteApplicant)}");
                return BadRequest("Submitted data is invalid");
            }

            await _unitOfWork.Applicants.Delete(id);
            await _unitOfWork.Save();

            return NoContent();
        }
    }
}
