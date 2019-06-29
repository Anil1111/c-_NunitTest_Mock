using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture()]
    public class EmployeeControllerTests
    {
        private readonly EmployeeController _controller;
        private readonly Mock<IEmployeeStorage> _storage;

        public EmployeeControllerTests()
        {
            _storage = new Mock<IEmployeeStorage>();
            _controller = new EmployeeController(_storage.Object) ;
        }

        [TestCase]
        //[Ignore("To save time")]
        public void DeleteEmployee_WhenCalled_ReturnExpectedResult()
        {
            // Arrange
            // Act
            var result = _controller.DeleteEmployee(111);
            // Assert
            _storage.Verify( s => s.DeleteEmployee(111));
        }

    }
}