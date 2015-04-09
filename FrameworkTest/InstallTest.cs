﻿using Dover.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dover.Framework.DAO;

namespace FrameworkTest
{
    [TestClass]
    public class InstallTests
    {
        private Application app;

        [TestInitialize]
        public void Initialize()
        {
            DoverSetup.CleanDover(false);
            app = DoverSetup.bootDover();
        }

        [TestCleanup]
        public void Cleanup()
        {
            DoverSetup.shutdownDover();
        }

        [TestMethod]
        public void InstallDover()
        {
            BusinessOneDAO b1dao = app.Resolve<BusinessOneDAO>();
            Assert.AreEqual( b1dao.ExecuteSqlForObject<int>("select count(*) from \"@DOVER_MODULES\""), 11);
            Assert.IsTrue( b1dao.ExecuteSqlForObject<int>("select count(*) from \"@DOVER_MODULES_BIN\"") > 0);
            Assert.IsTrue( b1dao.ExecuteSqlForObject<int>("select count(*) from \"@DOVER_MODULES_DEP\"") > 0);
            Assert.AreEqual( b1dao.ExecuteSqlForObject<int>("select count(*) from \"@DOVER_MODULES_USER\""), 0);
            Assert.AreEqual( b1dao.ExecuteSqlForObject<int>("select count(*) from \"@DOVER_LOGS\""), 0);
            Assert.AreEqual( b1dao.ExecuteSqlForObject<int>("select count(*) from \"@DOVER_LICENSE_BIN\""), 0);
        }

        [TestMethod]
        public void InstallI18NAddin()
        {
            SAPbouiCOM.Application b1App = app.Resolve<SAPbouiCOM.Application>();
            b1App.Menus.Item("doverAdmin").Activate();
        }
    }
}
