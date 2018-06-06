using NUnit.Framework;
using OutageManagementSystem.Common;
using OutageManagementSystem.Common.Enums;
using OutageManagementSystem.Common.Models;
using OutageManagementSystem.Service;
using OutageManagementSystem.Service.Enums;
using System.Linq;

namespace OutageManagementSystem.Tests
{
    [TestFixture]
    public class GenerateReportTests
    {
        private IOutageManagementSystem system = Factory.GenerateOMS(RepositoryType.TestValid, GeneratorType.TestValid);
        private IOutageManagementSystem invalidGeneratorSystem = Factory.GenerateOMS(RepositoryType.TestValid, GeneratorType.TestInvalid);
        private IOutageManagementSystem invalidRepositorySystem = Factory.GenerateOMS(RepositoryType.TestInvalid, GeneratorType.TestValid);
        Outage outage;

        [SetUp]
        public void Setup()
        {
            outage = new Outage();
            outage.Description = "Test";
            outage.ElementName = "Test";
            ExecutedAction action = new ExecutedAction();
            action.Description = "Test";
            outage.Actions.Add(action);

            system.Insert(outage);
            outage.Id = 0;
            invalidGeneratorSystem.Insert(outage);
        }

        [Test]
        public void Returns_OK()
        {
            Response<PDFReport> response = system.GenerateReport(outage.Id);
            PDFReport result = response.Data;
            ResponseStatus status = response.Status;

            Assert.IsNotNull(result);
            Assert.IsNotNullOrEmpty(result.FileName);
            Assert.IsNotNull(result.BinaryData);
            Assert.IsTrue(result.BinaryData.Any());
            Assert.AreEqual(ResponseStatus.OK, status);
        }

        [Test]
        [TestCase(99)]
        [TestCase(0)]
        [TestCase(-1)]
        public void Returns_ERROR_because_of_invalid_id(int id)
        {
            Response<PDFReport> response = system.GenerateReport(id);
            PDFReport result = response.Data;
            ResponseStatus status = response.Status;

            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseStatus.Error, status);
        }

        [Test]
        public void Returns_ERROR_because_of_generator_error()
        {
            Response<PDFReport> response = invalidGeneratorSystem.GenerateReport(outage.Id);
            PDFReport result = response.Data;
            ResponseStatus status = response.Status;

            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseStatus.Error, status);
        }

        [Test]
        public void Returns_ERROR_because_of_repository_error()
        {
            Response<PDFReport> response = invalidRepositorySystem.GenerateReport(outage.Id);
            PDFReport result = response.Data;
            ResponseStatus status = response.Status;

            Assert.IsNotNull(result);
            Assert.AreEqual(ResponseStatus.Error, status);
        }
    }
}
