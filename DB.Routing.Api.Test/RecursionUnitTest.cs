using DB.Routing.Api.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Projects
{
    [TestClass]
    public class RecursionUnitTest
    {
        [TestMethod]
        public void TestRecursion()
        {
            var result=RecursionExercise.recursiveSum(5);
            Assert.AreEqual(result, 15);
        }

        [TestMethod]
        public void TestNonRecursion()
        {
            var result = RecursionExercise.nonRecursiveSume(5);
            Assert.AreEqual(result, 6);
        }

        [TestMethod]
        public void TestFactorial()
        {
            var result = RecursionExercise.getFactorial(3);
            Assert.AreEqual(result, 6);
        }

        [TestMethod]
        public void TestFibonnaci()
        {
            var result = RecursionExercise.getFib(3);
            Assert.AreEqual(result, 2);
        }


        [TestMethod]
        public void TestCombosRecursive()
        {
            var result = RecursionExercise.CombosRecursive(10);
            Assert.AreEqual(result, 274);
        }
    }
}
