using AutoMapper;
using DocStream.Core.Interfaces;
using DocStream.Dtos.ApplicantLegalStatusDtos;
using DocStream.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocStream.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantLegalStatusController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ApplicantLegalStatusController> _logger;
        private readonly IMapper _mapper;

        public ApplicantLegalStatusController(IUnitOfWork unitOfWork, ILogger<ApplicantLegalStatusController> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("{id:int}", Name = "GetApplicantLegalStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetApplicantLegalStatus(int id)
        {

            var applicantLegalStatus = await _unitOfWork.Applicants.Get(q => q.Id == id);
            var result = _mapper.Map<ApplicantLegalStatusDto>(applicantLegalStatus);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateApplicantLegalStatus([FromBody] ApplicantLegalStatusDto applicantLegalStatusDto, int optionSelected)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateApplicantLegalStatus)}");
                return BadRequest(ModelState);
            }

            if (optionSelected < 1 || optionSelected > 1)
            {
                _logger.LogError($"None of the options in {nameof(CreateApplicantLegalStatus)} was selected");
                return BadRequest("No options selected");
            }


            await _unitOfWork.ApplicantLegalStatuses.Get(q => q.Id == optionSelected);
            var applicantLegalStatus = _mapper.Map<ApplicantLegalStatus>(applicantLegalStatusDto);
            await _unitOfWork.ApplicantLegalStatuses.Insert(applicantLegalStatus);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetApplicantLegalStatus", new { id = applicantLegalStatus.Id }, applicantLegalStatus);

        }

        [Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteApplicantLegalStatus(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteApplicantLegalStatus)}");
                return BadRequest();
            }

            var applicantLegalStatus = await _unitOfWork.ApplicantLegalStatuses.Get(q => q.Id == id);
            if (applicantLegalStatus == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteApplicantLegalStatus)}");
                return BadRequest("Submitted data is invalid");
            }

            await _unitOfWork.ApplicantLegalStatuses.Delete(id);
            await _unitOfWork.Save();

            return NoContent();
        }
    }
}
