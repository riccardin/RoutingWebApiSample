using Microsoft.VisualStudio.TestTools.UnitTesting;
using DB.Routing.Api.Helpers;

namespace Projects
{
    [TestClass]
    public class ExerciseUnitTest
    {
        [TestMethod]
        public void TestSwapNumber() {

            var result = Excercise.swapNumbers();
            Assert.AreEqual(200, result);
        }

        [TestMethod]
        public void TestIsArmstrongNumber() {

            var result = Excercise.IsArmstrongNumber();
            Assert.AreEqual(result, true);

        }

        [TestMethod]
        public void TestGetMajorityElement() {
            var result = Excercise.GetMajorityElement();
            Assert.AreEqual(result, 2);

        }

     
           
    }
}
