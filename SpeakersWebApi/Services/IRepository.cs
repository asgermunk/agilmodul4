using SpeakersWebApi.Models;
namespace SpeakersWebApi.Services
{

    public interface IRepository
    {
        List<SpeakerSummary> GetAll();
        SpeakerSummary GetById(int id);
    }


}