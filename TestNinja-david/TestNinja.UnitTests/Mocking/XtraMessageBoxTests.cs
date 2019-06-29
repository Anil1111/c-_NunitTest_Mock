using NUnit.Framework;
using TestNinja.Mocking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking.Tests
{
    [TestFixture()]
    public class XtraMessageBoxTests
    {
        [Test()]
        public void ShowTest()
        {
            // Arrange
            var box = new XtraMessageBox();
            // Act
            // Assert
            box.Show(null, null, MessageBoxButtons.OK);
        }
    }
}