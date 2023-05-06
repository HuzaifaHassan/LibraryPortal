using NUnit.Framework;
using System.Threading.Tasks;
using DbHandler.Repositories;
using DbHandler.Data;
using DbHandler.Model;
using LibraryPortal.Controllers;
using static LibraryPortal.DTO.DTO;
using FluentValidation;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace FunctionTest
{
    [TestFixture]
    public class AddStudentTests
    {
        [TestMethod]
        public void Registration_InputFields_Validation()
        {
            // Arrange
            var registration = new StudentDetails
            {
                Id = "123456",
                stId = "student123",
                cstID = "college123",
                CreatedOn = DateTime.Now,
                IsActive = true,
                Name = "John",
                LastName = "Doe",
                Email = "johndoe@example.com",
                Password = "password123",
                MobileNo = "1234567890",
                IsGraduated = "Yes"
            };

            var validator = new RegistrationValidator();

            // Act
            var result = validator.Validate(registration);

            // Assert
            Assert.IsTrue(result.IsValid);
        }
    }
    public class RegistrationValidator : AbstractValidator<StudentDetails>
    {
        public RegistrationValidator()
        {
            RuleFor(reg => reg.Id);

            RuleFor(reg => reg.stId);

            RuleFor(reg => reg.cstID);

            RuleFor(reg => reg.CreatedOn);

            RuleFor(reg => reg.Name);

            RuleFor(reg => reg.LastName);


            RuleFor(reg => reg.Email);

            RuleFor(reg => reg.Password);

            RuleFor(reg => reg.MobileNo);

            RuleFor(reg => reg.IsGraduated);
        }
    }
}