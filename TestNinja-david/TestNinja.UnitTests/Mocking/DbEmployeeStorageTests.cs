using NUnit.Framework;
using TestNinja.Mocking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace TestNinja.Mocking.Tests
{
    [TestFixture()]
    public class DbEmployeeStorageTests
    {
        [TestCase]
        //[Ignore("To save time")]
        public void DeleteEmployee_WhenCalled_ReturnExpectedResult()
        {
            // Arrange
            var mock = new Mock<IEmployeeRepository>();
            var storage = new DbEmployeeStorage(mock.Object);
            var employee = new Employee();
            mock.Setup(m => m.Find(123)).Returns(employee);
            // Act
            storage.DeleteEmployee(123);
            // Assert
            mock.Verify( m => m.Find(123));
            mock.Verify(m => m.Remove(employee));
            mock.Verify(m => m.SaveChanges());
        }

        [TestCase]
        //[Ignore("To save time")]
        public void DeleteEmployee_WhenEmployeesIsEmpty_ReturnExpectedResult()
        {
            // Arrange
            var mock = new Mock<IEmployeeRepository>();
            var storage = new DbEmployeeStorage(mock.Object);
            mock.Setup(m => m.Find(It.IsAny<int>())).Returns((Employee) null);
            // Act
            storage.DeleteEmployee(123);
            // Assert
            mock.Verify(m => m.Find(123), Times.Once);
            mock.Verify(m => m.Remove(null), Times.Never);
            mock.Verify(m => m.SaveChanges(), Times.Never);
        }
    }
}