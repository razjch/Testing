using DrugsAPI;
using DrugsAPI.Controllers;
using DrugsAPI.Models;
using DrugsAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace DrugAPI.Testing
{
    [TestFixture]
    public class Tests
    {
        private Mock<IDrugRepository> config;
        private DrugController TokenObj;
        [SetUp]
        public void Setup()
        {
            config = new Mock<IDrugRepository>();
            TokenObj = new DrugController(config.Object);
        }

        [Test]
        public void GetDetailsWhenNotNullDrugId()
        {
            config.Setup(p => p.GetById(1)).Returns(new Drug(){});
            var result = TokenObj.SearchDrugsByID(1);
            var rs = result as OkObjectResult;
            Assert.That(rs, Is.Not.Null);
            //  Assert.That(result,Is.InstanceOf<OkObjectResult>());
        }
        [Test]
        public void GetDetailsWhenNullDrugId()
        {
            //  config.Setup(p => p.searchDrugsByID(0)).Returns(new List<LocationWiseDrug> { });
            var result = (NotFoundObjectResult)TokenObj.SearchDrugsByID(0);
            Assert.AreEqual(result.StatusCode, 404);
        }
        [Test]
        public void GetDetailsWhenNotNullDrugName()
        {
            config.Setup(p => p.GetByName("corocin")).Returns(new Drug(){ });
            var result = TokenObj.SearchDrugsByName("corocin");
            var rs = result as OkObjectResult;
            Assert.That(rs, Is.Not.Null);
        }
        [Test]
        public void GetDetailsWhenNullDrugName()
        {
            var result = (NotFoundObjectResult)TokenObj.SearchDrugsByName(null);
            Assert.AreEqual(result.StatusCode, 404);
        }
        [Test]
        public void GetResponseWhenIdAndLocationIsNotNull()
        {
            config.Setup(p => p.GetDispatchableDrugStock(1, "bangalore")).Returns(new DispatchableDrug() { });
            var result = TokenObj.getDispatchableDrugStock(1, "bangalore");
            var rs = result as OkObjectResult;
            Assert.That(rs, Is.Not.Null);
        }
        //[Test]
        //public void GetResponseWhenIdAndLocationIsNull()
        //{
        //    var result = (NotFoundObjectResult)TokenObj.getDispatchableDrugStock(0, null);
        //    Assert.AreEqual(result.StatusCode, 404);
        //}
    }
}