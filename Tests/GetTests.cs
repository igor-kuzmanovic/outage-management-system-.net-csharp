using NUnit.Framework;
using OutageManagementSystem.Common;
using OutageManagementSystem.Common.Enums;
using OutageManagementSystem.Common.Models;
using OutageManagementSystem.Service;
using OutageManagementSystem.Service.Enums;

namespace OutageManagementSystem.Tests
{
    [TestFixture]
    public class GetTests
    {
        private IOutageManagementSystem system = Factory.GenerateOMS(RepositoryType.TestValid, GeneratorType.TestInvalid);
        private IOutageManagementSystem invalidSystem = Factory.GenerateOMS(RepositoryType.TestInvalid, GeneratorType.TestInvalid);
        private Outage outage;

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
        }

        [Test]
        public void Returns_OK()
        {
            Response<Outage> response = system.Get(outage.Id);
            Outage outageResponse = response.Data;
            ResponseStatus status = response.Status;

            Assert.IsTrue(outage.Description == outageResponse.Description);
            Assert.AreEqual(ResponseStatus.OK, status);
        }

        [Test]
        public void Returns_ERROR_because_of_id()
        {
            Response<Outage> response = system.Get(-1);
            Outage outageResponse = response.Data;
            ResponseStatus status = response.Status;

            Assert.IsNotNull(outageResponse);
            Assert.AreEqual(ResponseStatus.Error, status);
        }

        [Test]
        public void Returns_ERROR_because_of_repository_error()
        {
            Response<Outage> response = invalidSystem.Get(outage.Id);
            Outage outageResponse = response.Data;
            ResponseStatus status = response.Status;

            Assert.IsNotNull(outageResponse);
            Assert.AreEqual(ResponseStatus.Error, status);
        }
    }
}
