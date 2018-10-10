using Lab1A.Controllers;
using Lab1A.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab1UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private static CarContext dealershipContext = null;

        [ClassInitialize]
        public static void Initialize(TestContext tc)
        {
            //Initialize our context
            var options = new DbContextOptionsBuilder<CarContext>().UseInMemoryDatabase(databaseName: "DealershipTestDB").Options;

            dealershipContext = new CarContext(options);


            //Add records to context and save
            Dealership dealership1 = new Dealership { ID = 1, Name = "Toyota Dealership", Email = "Toyota.Dealership@hotmail.com", PhoneNumber = "9057899456" };
            Dealership dealership2 = new Dealership { ID = 2, Name = "Honda Dealership", Email = "Honda.Dealership@hotmail.com", PhoneNumber = "1234567891" };

            dealershipContext.Dealership.AddRange(dealership1, dealership2);
            dealershipContext.SaveChanges();

        }


        [TestMethod]
        public async Task DealershipTestToPass()
        {
            //Arrange
            DealershipApiController dealership = new DealershipApiController(dealershipContext);

            //Act 
            IEnumerable<Dealership> theDealerships = dealership.GetDealership();


            //Assert
            Assert.IsInstanceOfType(theDealerships, typeof(DbSet<Dealership>));
            DbSet<Dealership> dbSetDealerships = theDealerships as DbSet<Dealership>;

            int count = await dbSetDealerships.CountAsync();
            Assert.AreEqual(2, count);

            Dealership dealership1 = dbSetDealerships.Find(1);
            Assert.AreEqual("Toyota Dealership", dealership1.Name);
            Assert.AreEqual("Toyota.Dealership@hotmail.com", dealership1.Email);
        }

        [TestMethod]
        public async Task DealershipTestToPass2()
        {
            //Arrange
            DealershipApiController dealership = new DealershipApiController(dealershipContext);

            //Act 
            IEnumerable<Dealership> theDealerships = dealership.GetDealership();


            //Assert
            Assert.IsInstanceOfType(theDealerships, typeof(DbSet<Dealership>));
            DbSet<Dealership> dbSetDealerships = theDealerships as DbSet<Dealership>;

            int count = await dbSetDealerships.CountAsync();
            Assert.AreEqual(2, count);

            Dealership dealership2 = dbSetDealerships.Find(2);
            Assert.AreEqual(2, dealership2.ID);
            Assert.AreEqual("1234567891", dealership2.PhoneNumber);
        }

        [TestMethod]
        public async Task DealershipTestToPass3()
        {
            // Arrange
            DealershipController dealership = new DealershipController(dealershipContext);

            // Act
            IActionResult result = await dealership.Details(1);
            ViewResult viewResult = result as ViewResult;
            Dealership dealershipResult = viewResult.Model as Dealership;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "Details should return ViewResult");
            Assert.IsNotNull(viewResult, "Details should return non-null ViewResult");
            Assert.IsInstanceOfType(viewResult.Model, typeof(Dealership));

            Dealership detailsValue = viewResult.Model as Dealership;
            Assert.AreEqual("Toyota Dealership", detailsValue.Name);


        }

        [TestMethod]
        public async Task DealershipTestToFail()
        {
            //Arrange
            DealershipApiController dealership = new DealershipApiController(dealershipContext);

            //Act 
            IEnumerable<Dealership> theDealerships = dealership.GetDealership();


            //Assert
            Assert.IsInstanceOfType(theDealerships, typeof(DbSet<Dealership>));
            DbSet<Dealership> dbSetDealerships = theDealerships as DbSet<Dealership>;

            int count = await dbSetDealerships.CountAsync();
            Assert.AreNotEqual("2", count);

            Dealership dealership1 = dbSetDealerships.Find(1);
            Assert.AreNotEqual("Toyota Dealerships", dealership1.Name);
            Assert.AreNotEqual("Toyota.Dealership@gmail.com", dealership1.Email);

            Dealership dealership2 = dbSetDealerships.Find(2);
            Assert.AreNotEqual(1, dealership2.ID);
            Assert.AreNotEqual(1234567891, dealership2.PhoneNumber);
        }


        [TestMethod]
        public async Task DealershipTestToFail2()
        {
            // Arrange
            DealershipController dealership = new DealershipController(dealershipContext);

            // Act
            IActionResult result = await dealership.Details(1);
            ViewResult viewResult = result as ViewResult;
            Dealership dealershipResult = viewResult.Model as Dealership;

            // Assert
            Assert.IsNotInstanceOfType(viewResult.Model, typeof(Member));

            Dealership detailsValue = viewResult.Model as Dealership;
            Assert.AreNotEqual("Honda Dealership", detailsValue.Name);


        }
    }
}
