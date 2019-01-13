using System.Web.Mvc;
using ManagerCloud.DAL.Contracts;
using ManagerCloud.MVC.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ManagerCloud.Tests.Controllers
{
    [TestClass]
    public class ClientsController
    {
        private MVC.Controllers.ClientsController controller;
        private ViewResult result;

        [TestInitialize]
        public void SetupContext()
        {
            controller = new MVC.Controllers.ClientsController();
            result = controller.Index() as ViewResult;
        }

        [TestMethod]
        public void IndexViewResultNotNull()
        {
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IndexViewNotEqualIndexCshtml()
        {
            Assert.AreNotEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void IndexStringInViewbag()
        {
            Assert.AreEqual(null, result.ViewBag.Message);
        }
    }
}
