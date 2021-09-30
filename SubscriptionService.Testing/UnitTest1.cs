using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SubscriptionService.Controllers;
using SubscriptionService.DTO;
using SubscriptionService.Models;
using SubscriptionService.Repository;
using SubscriptionService.TempData;
using System;

namespace SubscriptionService.Testing
{
    [TestFixture]
    public class Tests
    {
        private Mock<ISubscriptionRepository> _prod;
        private Mock<SubscriptionData> subscriptionData;
        private SubscriptionController sub;

        MemberPrescription pre = new MemberPrescription()
        {
            DoctorDetails="abc",           
            DrugId = 1,
            PrescriptionId = 101,
            InsurancePolicyNumber = "I123",
            InsuranceProvider = "xyz",
            PrescriptionDate = DateTime.Now,
            DosagePerDay = 2,
            PrescriptionCourse  = 90,
            MemberID = 1          

         };
        SubscriptionDTO subDTO = new SubscriptionDTO
        {
            PrescriptionId = 101,
            Token = "token"
        };

        [SetUp]
        public void Setup()
        {
            _prod = new Mock<ISubscriptionRepository>();
            subscriptionData = new Mock<SubscriptionData>();
            sub = new SubscriptionController(_prod.Object, subscriptionData.Object);

        }
        [Test]
        public void PostSubscribe_WhenCalled_returnobject()
        {
            _prod.Setup(p => p.Subscribe(subDTO)).Returns(new MemberSubscription()
            {
                SubscriptionId = 1,
                SubscriptionStatus = true,
                MemberId = 101,
                MemberLocation = "bangalore",
                PrescriptionId = 21,
                RefillOccurrence = "Weekly",
                SubscriptionDate = DateTime.Now
            });
            var res = sub.Subscription(subDTO);
            var check = res as OkObjectResult;
            Assert.AreEqual(check.StatusCode, 200);

        }

        [Test]
        public void PostUnSubscribe_WhenCalled_returnobject()
        {
            _prod.Setup(p => p.UnSubscribe(subDTO)).Returns(new MemberSubscription()
            {
                SubscriptionId = 1,
                SubscriptionStatus = true,
                MemberId = 101,
                MemberLocation = "bangalore",
                PrescriptionId = 21,
                RefillOccurrence = "Weekly",
                SubscriptionDate = DateTime.Now
            });
            var result = sub.UnSubscription(subDTO);
            var check = result as OkObjectResult;
            Assert.AreEqual(check.StatusCode, 200);


        }                

    }
}