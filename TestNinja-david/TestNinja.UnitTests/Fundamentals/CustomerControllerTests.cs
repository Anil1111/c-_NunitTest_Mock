using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture()]
    public class CustomerControllerTests
    {
        private CustomerController _customerController;

        public CustomerControllerTests()
        {
            _customerController = new CustomerController();
        }

        [Test()]
        public void GetCustomer_IdIsCorrect_ReturnOK()
        {
            // Arrange
            // Action
            var result = _customerController.GetCustomer(123);
            // Assert
            Assert.That(result, Is.TypeOf<Ok>());
        }


        [Test()]
        public void GetCustomer_IdIs0_ReturnNotFound()
        {
            // Arrange
            // Action
            var result = _customerController.GetCustomer(0);
            // Assert
            Assert.That(result, Is.TypeOf<NotFound>());
        }
    }
}