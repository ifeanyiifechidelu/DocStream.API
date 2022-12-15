using AutoMapper;
using DocStream.Core.Interfaces;
using DocStream.Core.Token;
using DocStream.Dtos.ShareholderDtos;
using DocStream.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocStream.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShareholderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ShareholderController> _logger;
        private readonly IMapper _mapper;

        public ShareholderController(IUnitOfWork unitOfWork, ILogger<ShareholderController> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetShareholders([FromQuery] RequestParams requestParams)
        {
            var shareholders = await _unitOfWork.Shareholders.GetPagedList(requestParams);
            var results = _mapper.Map<IList<ShareholderDto>>(shareholders);
            return Ok(results);
        }

        [HttpGet("{id:int}", Name = "GetShareholder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetShareholder(int id)
        {

            var shareholder = await _unitOfWork.Shareholders.Get(q => q.Id == id);
            var result = _mapper.Map<ShareholderDto>(shareholder);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateShareholder([FromBody] CreateShareholderDto shareholderDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateShareholder)}");
                return BadRequest(ModelState);
            }

            var shareholder = _mapper.Map<Shareholder>(shareholderDto);
            await _unitOfWork.Shareholders.Insert(shareholder);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetShareholder", new { id = shareholder.Id }, shareholder);

        }

        [Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateShareholder(int id, [FromBody] UpdateShareholderDto shareholderDto)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateShareholder)}");
                return BadRequest(ModelState);
            }

            var shareholder = await _unitOfWork.Shareholders.Get(q => q.Id == id);
            if (shareholder == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateShareholder)}");
                return BadRequest("Submitted data is invalid");
            }

            _mapper.Map(shareholderDto, shareholder);
            _unitOfWork.Shareholders.Update(shareholder);
            await _unitOfWork.Save();

            return NoContent();

        }

        [Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteShareholder(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteShareholder)}");
                return BadRequest();
            }

            var shareholder = await _unitOfWork.Shareholders.Get(q => q.Id == id);
            if (shareholder == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteShareholder)}");
                return BadRequest("Submitted data is invalid");
            }

            await _unitOfWork.Shareholders.Delete(id);
            await _unitOfWork.Save();

            return NoContent();
        }
    }
}
