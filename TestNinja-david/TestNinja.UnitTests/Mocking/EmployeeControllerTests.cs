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