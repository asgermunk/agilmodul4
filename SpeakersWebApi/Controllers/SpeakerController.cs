using SpeakersWebApi.Services;
using SpeakersWebApi.Models;
using Microsoft.AspNetCore.Mvc;
namespace SpeakersWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpeakerController : ControllerBase
    {
        private readonly IRepository _repository;

        public SpeakerController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
                    // var speakers = new List<SpeakerSummary>
                    // {
                    //     new SpeakerSummary { Name = "John Doe", ContactInfo = "john.doe@example.com", Availability = "Weekdays" },
                    //     new SpeakerSummary { Name = "Jane Smith", ContactInfo = "jane.smith@example.com", Availability = "Weekends" }
                    // };
            var speakers = _repository.GetAll();
            

            return Ok(speakers);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var speaker = _repository.GetById(id);
                return Ok(speaker);
            }
            catch (SpeakerNotFoundException)
            {
                return NotFound("speaker not found");
            }
        }
    }
}