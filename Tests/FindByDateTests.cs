using NUnit.Framework;
using OutageManagementSystem.Common;
using OutageManagementSystem.Common.Enums;
using OutageManagementSystem.Common.Models;
using OutageManagementSystem.Service;
using OutageManagementSystem.Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OutageManagementSystem.Tests
{
    [TestFixture]
    public class FindByDateTests
    {
        private IOutageManagementSystem system = Factory.GenerateOMS(RepositoryType.TestValid, GeneratorType.TestInvalid);
        private IOutageManagementSystem invalidSystem = Factory.GenerateOMS(RepositoryType.TestInvalid, GeneratorType.TestInvalid);

        [SetUp]
        public void Setup()
        {
            Outage outage = new Outage();
            outage.CreationDate = new DateTime(2006, 6, 6);
            outage.Description = "Test";
            outage.ElementName = "Test";
            ExecutedAction action = new ExecutedAction();
            action.Description = "Test";
            outage.Actions.Add(action);

            system.Insert(outage);
        }

        [Test]
        [TestCase("", "")]
        [TestCase("", "7/7/2007")]
        [TestCase("5/5/2005", "")]
        [TestCase("5/5/2005", "7/7/2007")]
        public void Returns_OK(string start, string end)
        {
            Response<IEnumerable<Outage>> response = system.FindByDate(start, end);
            IEnumerable<Outage> outages = response.Data.ToList();
            ResponseStatus status = response.Status;

            Assert.IsTrue(outages.Count() != 0);
            Assert.AreEqual(ResponseStatus.OK, status);
        }

        [Test]
        public void Returns_NO_CONTENT()
        {
            Response<IEnumerable<Outage>> response = system.FindByDate("7/7/2007", "8/8/2008");
            IEnumerable<Outage> outages = response.Data.ToList();
            ResponseStatus status = response.Status;

            Assert.IsTrue(outages.Count() == 0);
            Assert.AreEqual(status, ResponseStatus.NoContent);
        }

        [Test]
        public void Returns_NO_CONTENT_because_of_repository_error()
        {
            Response<IEnumerable<Outage>> response = invalidSystem.FindByDate(null, null);
            IEnumerable<Outage> outages = response.Data.ToList();
            ResponseStatus status = response.Status;

            Assert.IsTrue(outages.Count() == 0);
            Assert.AreEqual(status, ResponseStatus.NoContent);
        }
    }
}
