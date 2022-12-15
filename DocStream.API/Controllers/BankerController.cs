using AutoMapper;
using DocStream.Core.Interfaces;
using DocStream.Core.Token;
using DocStream.Dtos.BankerDtos;
using DocStream.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocStream.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BankerController> _logger;
        private readonly IMapper _mapper;

        public BankerController(IUnitOfWork unitOfWork, ILogger<BankerController> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBankers([FromQuery] RequestParams requestParams)
        {
            var bankers = await _unitOfWork.Bankers.GetPagedList(requestParams);
            var results = _mapper.Map<IList<BankerDto>>(bankers);
            return Ok(results);
        }

        [HttpGet("{id:int}", Name = "GetBanker")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBanker(int id)
        {
            
            var banker = await _unitOfWork.Bankers.Get(q => q.Id == id);
            var result = _mapper.Map<BankerDto>(banker);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateBanker([FromBody] CreateBankerDto bankerDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateBanker)}");
                return BadRequest(ModelState);
            }

            var banker = _mapper.Map<Banker>(bankerDto);
            await _unitOfWork.Bankers.Insert(banker);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetBanker", new { id = banker.Id }, banker);

        }

        [Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBanker(int id, [FromBody] UpdateBankerDto bankerDto)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateBanker)}");
                return BadRequest(ModelState);
            }

            var banker = await _unitOfWork.Bankers.Get(q => q.Id == id);
            if (banker == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateBanker)}");
                return BadRequest("Submitted data is invalid");
            }

            _mapper.Map(bankerDto, banker);
            _unitOfWork.Bankers.Update(banker);
            await _unitOfWork.Save();

            return NoContent();

        }

        [Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteBanker(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteBanker)}");
                return BadRequest();
            }

            var banker = await _unitOfWork.Bankers.Get(q => q.Id == id);
            if (banker == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteBanker)}");
                return BadRequest("Submitted data is invalid");
            }

            await _unitOfWork.Bankers.Delete(id);
            await _unitOfWork.Save();

            return NoContent();
        }
    }
}
