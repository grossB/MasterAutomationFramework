using NUnit.Framework;
using NunitTest.ParallelTest.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NunitTest.ParallelTest.NewFolder
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class TestFixture2 : BaseFixture
    {
        [Parallelizable(ParallelScope.Self)]
        [Test]
        public void TestMethod1()
        {
            Thread.Sleep(1000);
            Assert.IsTrue(true);
        }
        [Parallelizable(ParallelScope.Self)]
        [Test]
        public void TestMethod2()
        {
            Thread.Sleep(2200);
            Assert.IsTrue(true);
        }
        [Parallelizable(ParallelScope.Self)]
        [Test]
        public void TestMethod3()
        {
            Thread.Sleep(300);
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
