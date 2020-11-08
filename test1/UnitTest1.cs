using NUnit.Framework;
using LearnMe.Web.Controllers.Account;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using LearnMe.Core.DTO.Account;
using Microsoft.AspNetCore.Mvc;

namespace test1
{
    [TestFixture]
    public class Tests
    {
        RegisterByMentorController _RegisterByMentorController;
        RegisterFromMentorDto _RegisterFromMentor;

        [SetUp]
        public void SetUp()

        {
            _RegisterFromMentor = new RegisterFromMentorDto();
            _RegisterByMentorController = new RegisterByMentorController();
        }

        [Test]
        public void Test()
        {   
            _RegisterFromMentor.FirstName = "jan";
            _RegisterFromMentor.LastName = "Kowalski";

            var result = _RegisterByMentorController.OnPostAsync(_RegisterFromMentor);

            Assert.IsInstanceOf(typeof(NotFoundResult), result);

        }
    }
}