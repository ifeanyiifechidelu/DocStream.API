using AutoMapper;
using DocStream.Core.Interfaces;
using DocStream.Core.Token;
using DocStream.Dtos.ProposedBusinessNameDtos;
using DocStream.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocStream.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProposedBusinessNameController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProposedBusinessNameController> _logger;
        private readonly IMapper _mapper;

        public ProposedBusinessNameController(IUnitOfWork unitOfWork, ILogger<ProposedBusinessNameController> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProposedBusinessNames([FromQuery] RequestParams requestParams)
        {
            var proposedBusinessNames = await _unitOfWork.ProposedBusinessNames.GetPagedList(requestParams);
            var results = _mapper.Map<IList<ProposedBusinessNameDto>>(proposedBusinessNames);
            return Ok(results);
        }

        [HttpGet("{id:int}", Name = "GetProposedBusinessName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProposedBusinessName(int id)
        {

            var proposedBusinessNames = await _unitOfWork.ProposedBusinessNames.Get(q => q.Id == id);
            var result = _mapper.Map<ProposedBusinessNameDto>(proposedBusinessNames);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProposedBusinessName([FromBody] CreateProposedBusinessNameDto proposedBusinessNamesDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateProposedBusinessName)}");
                return BadRequest(ModelState);
            }

            var proposedBusinessName = _mapper.Map<ProposedBusinessName>(proposedBusinessNamesDto);
            await _unitOfWork.ProposedBusinessNames.Insert(proposedBusinessName);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetProposedBusinessName", new { id = proposedBusinessName.Id }, proposedBusinessName);

        }

        [Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProposedBusinessName(int id, [FromBody] UpdateProposedBusinessNameDto proposedBusinessNameDto)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateProposedBusinessName)}");
                return BadRequest(ModelState);
            }

            var proposedBusinessName = await _unitOfWork.ProposedBusinessNames.Get(q => q.Id == id);
            if (proposedBusinessName == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateProposedBusinessName)}");
                return BadRequest("Submitted data is invalid");
            }

            _mapper.Map(proposedBusinessNameDto, proposedBusinessName);
            _unitOfWork.ProposedBusinessNames.Update(proposedBusinessName);
            await _unitOfWork.Save();

            return NoContent();

        }

        [Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProposedBusinessName(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteProposedBusinessName)}");
                return BadRequest();
            }

            var proposedBusinessName = await _unitOfWork.ProposedBusinessNames.Get(q => q.Id == id);
            if (proposedBusinessName == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteProposedBusinessName)}");
                return BadRequest("Submitted data is invalid");
            }

            await _unitOfWork.ProposedBusinessNames.Delete(id);
            await _unitOfWork.Save();

            return NoContent();
        }
    }
}
