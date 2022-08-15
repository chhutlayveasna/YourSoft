using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YourSoft.BLL.Contracts;
using YourSoft.BLL.Models;
using YourSoft.BLL.Models.Sample;
using YourSoft.DAL.Data;

namespace YourSoft.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Manage Sample")]
    public class SamplesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISampleRepository _sampleRepository;

        public SamplesController(IMapper mapper, ISampleRepository sampleRepository)
        {
            _mapper = mapper;
            _sampleRepository = sampleRepository;
        }

        // GET: api/Samples/GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<Response<SampleDto>>> GetSamples()
        {
            var samples = await _sampleRepository.GetAllAsync();
            var records = _mapper.Map<List<SampleDto>>(samples);
            return Ok(new Response<SampleDto> { Data = records, Message = "success", StatusCode = StatusCodes.Status200OK });
        }

        // GET: api/Samples/?StartIndex=0&pagesize=25&PageNumber=1
        [HttpGet]
        public async Task<ActionResult<PagedResult<SampleDto>>> GetPagedSamples([FromQuery] QueryParameters queryParameters)
        {
            var pagedSamplesResult = await _sampleRepository.GetAllAsync<SampleDto>(queryParameters);
            return Ok(pagedSamplesResult);
        }

        // GET: api/Samples/5
        [HttpGet("{id}")]
        //[Authorize(Roles = "MANAGE SAMPLE")]
        public async Task<ActionResult<Response<SampleDto>>> GetSample(int id)
        {
            var sample = await _sampleRepository.GetAsync(id);

            if (sample == null)
                return NotFound();

            var sampleDto = _mapper.Map<SampleDto>(sample);

            return Ok(new ResponseResult<SampleDto> { Data = sampleDto, Message = "success", StatusCode = StatusCodes.Status200OK });
        }

        // PUT: api/Samples/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSample(int id, SampleDto sampleDto)
        {
            if (id != sampleDto.Id)
                return BadRequest();

            var sample = await _sampleRepository.GetAsync(id);

            if (sample == null)
                return NotFound();

            _mapper.Map(sampleDto, sample);

            try
            {
                await _sampleRepository.UpdateAsync(sample);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await SampleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Samples
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sample>> PostSample(CreateSampleDto createSampleDto)
        {
            var sample = _mapper.Map<Sample>(createSampleDto);
            await _sampleRepository.AddAsync(sample);
            return CreatedAtAction("GetSample", new { id = sample.Id }, sample);
        }

        // DELETE: api/Samples/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSample(int id)
        {
            var sample = await _sampleRepository.GetAsync(id);
            if (sample == null)
                return NotFound();

            await _sampleRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> SampleExists(int id)
        {
            return await _sampleRepository.Exists(id);
        }
    }
}
