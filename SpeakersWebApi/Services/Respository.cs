using SpeakersWebApi.Services;
using SpeakersWebApi.Models;

namespace SpeakersWebApi.Services
{
    public class Repository : IRepository
    {
        private readonly List<SpeakerSummary> _speakers = new List<SpeakerSummary>
        {
            new SpeakerSummary { Id = 1, Name = "John Doe", ContactInfo = "john.doe@example.com", Availability = "Weekdays" },
            new SpeakerSummary { Id = 2, Name = "Jane Smith", ContactInfo = "jane.smith@example.com", Availability = "Weekends" }
        };

        public List<SpeakerSummary> GetAll()
        {
            return _speakers;
        }

        public SpeakerSummary GetById(int id)
        {
            var speaker = _speakers.FirstOrDefault(s => s.Id == id);
            if (speaker == null)
            {
                throw new SpeakerNotFoundException();
            }
            return speaker;
        }
    }
}