using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Lab5.EPAM.EFData;
using Lab5.EPAM.EFData.EFContext;
using Lab5.EPAM.Services.Services;
using NUnit.Framework;

namespace Lab5.EPAM.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test()
        {
            var context = new SiteContext("SiteTest");
            var unitOfWork = new UnitOfWork(context);
            var membershipService = new MembershipService(unitOfWork, unitOfWork);
            var user = membershipService.RegisterUser("Artyom", "123456", "123@gmail.com", true);
            var id = user.Id;
            var email = user.Email;
            unitOfWork.Dispose();

            var context1 = new SiteContext("SiteTest");
            var unitOfWork1 = new UnitOfWork(context1);
            var membershipService1 = new MembershipService(unitOfWork1, unitOfWork1);
            var userById = membershipService1.GetUserByEmail(email);
            userById.Roles.Should().NotBeNull();
        }
    }
}
