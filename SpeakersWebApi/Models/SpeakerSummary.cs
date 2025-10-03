namespace SpeakersWebApi.Models
{
    public class SpeakerSummary
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string ContactInfo { get; set; }
        public required string Availability { get; set; }
    }
}

