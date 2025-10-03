using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpeakersWebApi.Controllers;
using SpeakersWebApi.Services;
using SpeakersWebApi.Models;

namespace SpeakersTestProject
{
    [TestClass]
    public class GetAlltests
    {
        private Mock<IRepository> _speakerServiceMock = null!;
        private SpeakerController _controller = null!;
        
        [TestInitialize]
        public void Initialize()
        {
            _speakerServiceMock = new Mock<IRepository>();
            _controller = new SpeakerController(_speakerServiceMock.Object);
        }

        [TestMethod]
        public void ItExists()
        {
            // Arrange

            // Act
            _controller.GetAll();
            // Assert
            // No assertions yet; the test ensures the method can be called without errors
        }

        [TestMethod]
        public void ItReturnsOkObjectResult()
        {
            // Arrange

            // Act
            var result = _controller.GetAll();
            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }
        
        [TestMethod]
        public void ItReturnsCollectionOfSpeakerSummary()
        {
            // Arrange
            var speakerList = new List<SpeakerSummary>
            {
                new SpeakerSummary { Id = 1, Name = "John Doe", ContactInfo = "john.doe@example.com", Availability = "Weekdays" },
                new SpeakerSummary { Id = 2, Name = "Jane Smith", ContactInfo = "jane.smith@example.com", Availability = "Weekends" }
            };
            // Act
            
            _speakerServiceMock.Setup(r => r.GetAll())
            .Returns(speakerList);
               
            var result = _controller.GetAll() as OkObjectResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(IEnumerable<SpeakerSummary>));

        }
        
        [TestMethod]
        public void EnsureRepositoryIsCalled()
        {
            // Arrange
            // var speakerServiceMock = new Mock<IRepository>();
            //var controller = new SpeakerController(speakerServiceMock.Object);
            // Act
            _speakerServiceMock.Setup(r => r.GetAll())
            .Returns(new List<SpeakerSummary>());
            var result = _controller.GetAll() as OkObjectResult;
            _speakerServiceMock.Verify(r => r.GetAll(), Times.Once());
            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(IEnumerable<SpeakerSummary>));
        }
        
        [TestMethod]
        public void GivenSpeakerNotFoundThenReturnsNotFound()
        {
            // Setup mock to throw when id = -1
            _speakerServiceMock.Setup(r => r.GetById(-1))
            .Throws(new SpeakerNotFoundException());
            // Act
            var result = _controller.Get(-1);
            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }
        
        [TestMethod]
        public void GivenSpeakerNotFoundThenReturnsNotFoundFriendlyMessage()
        {
            // Setup mock to throw when id = -1
            _speakerServiceMock.Setup(r => r.GetById(-1))
            .Throws(new SpeakerNotFoundException());
            // Act
            var result = _controller.Get(-1) as NotFoundObjectResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("speaker not found", result.Value);
        }
    }
}
