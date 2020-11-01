using NUnit.Framework;
using NunitTest.ParallelTest.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NunitTest.ParallelTest.NewFolder
{
    [TestFixture, Parallelizable(ParallelScope.Fixtures)]
    public class TestFixture1 : BaseFixture
    {
        [Parallelizable(ParallelScope.Self)]
        [Test]
        public void TestMethod1()
        {
            Thread.Sleep(2000);
            Assert.IsTrue(true);
        }
        [Parallelizable(ParallelScope.Self)]
        [Test]
        public void TestMethod2()
        {
            Thread.Sleep(500);
            Assert.IsTrue(true);
        }
        [Parallelizable(ParallelScope.Self)]
        [Test]
        public void TestMethod3()
        {
            Assert.IsTrue(true);
        }
        [Parallelizable(ParallelScope.Self)]
        [Test]
        public void TestMethod4()
        {
            Assert.IsTrue(true);
        }
        [Parallelizable(ParallelScope.Self)]
        [Test]
        public void TestMethod5()
        {
            Assert.IsTrue(true);
        }
        [Parallelizable(ParallelScope.Self)]
        [Test]
        public void TestMethod6()
        {
            Assert.IsTrue(true);
        }
    }
}
