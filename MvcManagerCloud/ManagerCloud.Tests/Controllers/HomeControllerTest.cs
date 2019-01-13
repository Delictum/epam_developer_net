using System.Collections.Generic;
using System.Web.Mvc;
using ManagerCloud.MVC.Controllers;
using ManagerCloud.MVC.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ManagerCloud.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            var controller = new HomeController();
            var result = controller.Index(1) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            var controller = new HomeController();
            var result = controller.About() as ViewResult;

            Assert.AreEqual("This site was developed for educational purposes on task 5 for EPAM Systems by a student Igor Olishkevich course of 2018/2019.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            var controller = new HomeController();
            var result = controller.Contact() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}
