using NUnit.Framework;
using OutageManagementSystem.Common;
using OutageManagementSystem.Common.Enums;
using OutageManagementSystem.Common.Models;
using OutageManagementSystem.Service;
using OutageManagementSystem.Service.Enums;
using System;

namespace OutageManagementSystem.Tests
{
    [TestFixture]
    public class UpdateTests
    {
        private IOutageManagementSystem system = Factory.GenerateOMS(RepositoryType.TestValid, GeneratorType.TestInvalid);
        private IOutageManagementSystem invalidSystem = Factory.GenerateOMS(RepositoryType.TestInvalid, GeneratorType.TestInvalid);
        Outage outage;
        int id;

        [SetUp]
        public void Setup()
        {
            outage = new Outage();
            outage.Description = "Test";
            outage.ElementName = "Test";
            ExecutedAction action = new ExecutedAction();
            action.Description = "Test";
            outage.Actions.Add(action);

            id = system.Insert(outage).Data;
        }

        [Test]
        public void Returns_OK()
        {
            outage.Id = id;
            outage.Description = "Update";

            Response<bool> response = system.Update(outage);
            bool result = response.Data;
            ResponseStatus status = response.Status;

            Assert.IsTrue(result);
            Assert.AreEqual(ResponseStatus.OK, status);
        }

        [Test]
        public void Returns_ERROR_because_of_null_outage()
        {
            Response<bool> response = system.Update(null);
            bool result = response.Data;
            ResponseStatus status = response.Status;

            Assert.IsFalse(result);
            Assert.AreEqual(ResponseStatus.Error, status);
        }

        [Test]
        public void Returns_ERROR_because_of_id()
        {
            outage.Id = -1;
            outage.Description = "Update 2";
            outage.State = OutageState.Closed;

            Response<bool> response = system.Update(outage);
            bool result = response.Data;
            ResponseStatus status = response.Status;

            Assert.IsFalse(result);
            Assert.AreEqual(ResponseStatus.Error, status);
        }

        [Test]
        public void Returns_ERROR_because_state_is_closed()
        {
            outage.Description = "Update 2";
            outage.State = OutageState.Closed;

            Response<bool> response = system.Update(outage);
            bool result = response.Data;
            ResponseStatus status = response.Status;

            Assert.IsFalse(result);
            Assert.AreEqual(ResponseStatus.Error, status);
        }

        [Test]
        [TestCase("", "6/6/2006", "Test", 0, 0, "Test", "6/6/2006")]
        [TestCase("Test", "6/6/2066", "Test", 0, 0, "Test", "6/6/2006")]
        [TestCase("Test", "6/6/2006", "", 0, 0, "Test", "6/6/2006")]
        [TestCase("Test", "6/6/2006", "Test", -91, 0, "Test", "6/6/2006")]
        [TestCase("Test", "6/6/2006", "Test", 91, 0, "Test", "6/6/2006")]
        [TestCase("Test", "6/6/2006", "Test", 0, -181, "Test", "6/6/2006")]
        [TestCase("Test", "6/6/2006", "Test", 0, 181, "Test", "6/6/2006")]
        [TestCase("Test", "6/6/2006", "Test", 0, 0, "", "6/6/2006")]
        [TestCase("Test", "6/6/2006", "Test", 0, 0, "Test", "6/6/2066")]
        public void Return_ERROR_because_of_validation(string description, string creationDate, string elementName, double latitude, double longitude, string actionDescription, string actionExecutionDate)
        {
            Outage outage = new Outage();
            outage.Id = 1;
            outage.Description = description;
            outage.CreationDate = DateTime.Parse(creationDate);
            outage.ElementName = elementName;
            outage.Latitude = latitude;
            outage.Longitude = longitude;
            ExecutedAction action = new ExecutedAction();
            action.Description = actionDescription;
            action.ExecutionDate = DateTime.Parse(actionExecutionDate);
            outage.Actions.Add(action);

            Response<bool> response = invalidSystem.Update(outage);
            bool result = response.Data;
            ResponseStatus status = response.Status;

            Assert.IsFalse(result);
            Assert.AreEqual(ResponseStatus.Error, status);
        }

        [Test]
        public void Return_ERROR_because_of_repository_error()
        {
            outage.Description = "Update 3";

            Response<bool> response = invalidSystem.Update(outage);
            bool result = response.Data;
            ResponseStatus status = response.Status;

            Assert.IsFalse(result);
            Assert.AreEqual(ResponseStatus.Error, status);
        }
    }
}
