using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture()]
    public class DbEmployeeStorageTests
    {
        [TestCase]
        //[Ignore("To save time")]
        public void DeleteEmployee_WhenCalled_ReturnExpectedResult()
        {
            // Arrange
            var employeeRep = new Mock<IEmployeeRepository>();
            var storage = new DbEmployeeStorage(employeeRep.Object);
            var employee = new Employee();
            employeeRep.Setup(m => m.Find(123)).Returns(employee);
            // Act
            storage.DeleteEmployee(123);
            // Assert
            employeeRep.Verify(m => m.Find(123));
            employeeRep.Verify(m => m.Remove(employee));
            employeeRep.Verify(m => m.SaveChanges());
        }

        [TestCase]
        //[Ignore("To save time")]
        public void DeleteEmployee_WhenEmployeesIsEmpty_ReturnExpectedResult()
        {
            // Arrange
            var employeeRep = new Mock<IEmployeeRepository>();
            var storage = new DbEmployeeStorage(employeeRep.Object);
            employeeRep.Setup(m => m.Find(It.IsAny<int>())).Returns((Employee) null);
            // Act
            storage.DeleteEmployee(123);
            // Assert
            employeeRep.Verify(m => m.Find(123), Times.Once);
            employeeRep.Verify(m => m.Remove(null), Times.Never);
            employeeRep.Verify(m => m.SaveChanges(), Times.Never);
        }
    }
}