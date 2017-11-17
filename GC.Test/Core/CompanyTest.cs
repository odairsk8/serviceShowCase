using GC.Core.Entities;
using GC.Core.Interfaces.Repositories;
using GC.Core.Interfaces.Services;
using GC.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace GC.Test.Core
{
    [TestClass]
    public class CompanyTest
    {
        Mock<ICompanyRepository> mockCompanyRepository = new Mock<ICompanyRepository>();
        ICompanyService companyService;

        [TestInitialize]
        public void TestInitialize()
        {
            companyService = new CompanyService(mockCompanyRepository.Object);
        }

        [TestMethod]
        public void ShouldReturnCompanies()
        {
            //arrange
            mockCompanyRepository.Setup(s => s.GetAll(It.IsAny<IEnumerable<string>>())).Returns(new List<Company>() {
                new Company(),
                new Company()
            });

            //act
            var companies = this.companyService.GetAll();

            //assert
            Assert.IsTrue(companies != null && companies.Count() > 0, "Service is not returning items");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.companyService.Dispose();
        }
    }
}
