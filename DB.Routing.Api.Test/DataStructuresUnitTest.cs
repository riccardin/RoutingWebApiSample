using Microsoft.VisualStudio.TestTools.UnitTesting;
using DB.Routing.Api.Helpers;

namespace Projects
{
    [TestClass]
    class DataStructuresUnitTest
    {
        [TestMethod]
        public void TestPrintList()
        {

            var result = DataStructures.createNodeList();
            Assert.AreEqual(result, "3,7,10,");

        }
    }
}
