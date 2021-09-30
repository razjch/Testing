using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using RefillService.Controllers;
using RefillService.Model;
using RefillService.Repository;

namespace RefillService.Testing
{
    [TestFixture]
    public class Tests
    {
        RefillOrder refillorder = new RefillOrder();   
        
       
        [Test]
        [TestCase(201)]
        [TestCase(202)]
        public void ViewRefillStatus_WithValidId_ReturnSucess(int subscriptionId)
        {
            Mock<IRefillRepository> mock = new Mock<IRefillRepository>();
            mock.Setup(p => p.ViewRefillStatus(subscriptionId)).Returns(new RefillOrder() { });
            var pc = new RefillController(mock.Object);
            var result = pc.ViewRefillStatus(subscriptionId);
            Assert.IsNotNull(result);
        }


        [Test]
        [TestCase(-123)]
        [TestCase(4545)]
        public void ViewRefillStatus_WithInValidId_ReturnBadRequest(int subscriptionId)
        {
            Mock<IRefillRepository> mock = new Mock<IRefillRepository>();
            //mock.Setup(p => p.ViewRefillStatus(subscriptionId)).Returns(new RefillOrder() { });
            var pc = new RefillController(mock.Object);
            var result = (BadRequestObjectResult)pc.ViewRefillStatus(subscriptionId);
            Assert.AreEqual(result.StatusCode,400);
        }

        [Test]
        [TestCase(201,false)]
        [TestCase(202,false)]
        [TestCase(208,false)]
        [TestCase(213,true)]
        [TestCase(214,true)]
        [TestCase(6545, null)]
        [TestCase(-6545, null)]
        public void CheckPendingPaymentStatus_WithValidId_ReturnSucess(int subscriptionId, bool? expectedResult)
        {
            Mock<IRefillRepository> mock = new Mock<IRefillRepository>();
            if(subscriptionId == 201 || subscriptionId == 202 || subscriptionId == 208 )
            {
                mock.Setup(p => p.CheckPendingPaymentStatus(subscriptionId)).Returns(false);
                var pc = new RefillController(mock.Object);
                var result = (BadRequestObjectResult)pc.checkPendingPaymentStatus(subscriptionId);
                Assert.AreEqual(result.StatusCode, 400);
            }
            if (subscriptionId == 213 || subscriptionId == 214 || subscriptionId == 215)
            {
                mock.Setup(p => p.CheckPendingPaymentStatus(subscriptionId)).Returns(true);
                var pc = new RefillController(mock.Object);
                var result = (OkResult)pc.checkPendingPaymentStatus(subscriptionId);
                Assert.AreEqual(result.StatusCode, 200);
            }
            else 
            {
                //mock.Setup(p => p.CheckPendingPaymentStatus(subscriptionId)).Returns(true);
                var pc = new RefillController(mock.Object);
                var result = (BadRequestObjectResult)pc.checkPendingPaymentStatus(subscriptionId);
                Assert.AreEqual(result.StatusCode, 400);
            }
        }     



    }
}