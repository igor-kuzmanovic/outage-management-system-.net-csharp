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
    public class InsertTests
    {
        private IOutageManagementSystem system = Factory.GenerateOMS(RepositoryType.TestValid, GeneratorType.TestInvalid);
        private IOutageManagementSystem invalidSystem = Factory.GenerateOMS(RepositoryType.TestInvalid, GeneratorType.TestInvalid);

        [Test]
        public void Returns_OK()
        {
            Outage outage = new Outage();
            outage.Description = "Test";
            outage.ElementName = "Test";
            ExecutedAction action = new ExecutedAction();
            action.Description = "Test";
            outage.Actions.Add(action);

            Response<int> response = system.Insert(outage);
            int id = response.Data;
            ResponseStatus status = response.Status;

            Assert.IsTrue(id > 0);
            Assert.AreEqual(ResponseStatus.OK, status);
        }

        [Test]
        public void Returns_ERROR_because_of_null_outage()
        {
            Response<int> response = system.Insert(null);
            int id = response.Data;
            ResponseStatus status = response.Status;

            Assert.AreEqual(id, 0);
            Assert.AreEqual(ResponseStatus.Error, status);
        }

        [Test]
        public void Returns_ERROR_because_of_id()
        {
            Outage outage = new Outage();
            outage.Description = "Test";
            outage.ElementName = "Test";
            ExecutedAction action = new ExecutedAction();
            action.Description = "Test";
            outage.Actions.Add(action);
            outage.Id = 1;

            Response<int> response = system.Insert(outage);
            int id = response.Data;
            ResponseStatus status = response.Status;

            Assert.AreEqual(id, 0);
            Assert.AreEqual(ResponseStatus.Error, status);
        }

        [Test]
        [TestCase("", "6/6/2006", "Test", 0, 0, "Test", "6/6/2006")]
        [TestCase("Test", "6/6/2066", "Test", 0, 0, "Test", "6/6/2006")]
        [TestCase("Test", "6/6/2006", "", 0, 0, "Test", "6/6/2006")]
        [TestCase("Test", "6/6/2006", "Test", -999, 0, "Test", "6/6/2006")]
        [TestCase("Test", "6/6/2006", "Test", 999, 0, "Test", "6/6/2006")]
        [TestCase("Test", "6/6/2006", "Test", 0, -999, "Test", "6/6/2006")]
        [TestCase("Test", "6/6/2006", "Test", 0, 999, "Test", "6/6/2006")]
        [TestCase("Test", "6/6/2006", "Test", 0, 0, "", "6/6/2006")]
        [TestCase("Test", "6/6/2006", "Test", 0, 0, "Test", "6/6/2066")]
        public void Returns_ERROR_because_of_validation(string description, string creationDate, string elementName, double latitude, double longitude, string actionDescription, string actionExecutionDate)
        {
            Outage outage = new Outage();
            outage.Description = description;
            outage.CreationDate = DateTime.Parse(creationDate);
            outage.ElementName = elementName;
            outage.Latitude = latitude;
            outage.Longitude = longitude;
            ExecutedAction action = new ExecutedAction();
            action.Description = actionDescription;
            action.ExecutionDate = DateTime.Parse(actionExecutionDate);
            outage.Actions.Add(action);

            Response<int> response = system.Insert(outage);
            int id = response.Data;
            ResponseStatus status = response.Status;

            Assert.AreEqual(outage.Id, 0);
            Assert.AreEqual(ResponseStatus.Error, status);
        }

        [Test]
        public void Returns_ERROR_because_of_repository_error()
        {
            Outage outage = new Outage();
            outage.Description = "Test";
            outage.ElementName = "Test";
            ExecutedAction action = new ExecutedAction();
            action.Description = "Test";
            outage.Actions.Add(action);

            Response<int> response = invalidSystem.Insert(outage);
            int id = response.Data;
            ResponseStatus status = response.Status;

            Assert.AreEqual(outage.Id, 0);
            Assert.AreEqual(ResponseStatus.Error, status);
        }
    }
}
