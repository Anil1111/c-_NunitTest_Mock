using NUnit.Framework;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture()]
    public class StackTests
    {
        private TestNinja.Fundamentals.Stack<string> _stack;

        [SetUp]
        public void Setup()
        {
            _stack = new TestNinja.Fundamentals.Stack<string>();
        }

        [TestCase]
        //[Ignore("To save time")]
        public void Push_WhenParameterIsNotCorrect_ThrowsException()
        {
            // Arrange

            // Act
            // Assert
            Assert.That(() => _stack.Push(null), Throws.Exception);
            Assert.That(() => _stack.Pop(), Throws.Exception);
            Assert.That(() => _stack.Peek(), Throws.Exception);
        }

        [TestCase]
        //[Ignore("To save time")]
        public void Push_EmptyStatck_ReturnExpectedResult()
        {
            // Arrange
            // Act
            // Assert
            Assert.That(_stack.Count == 0, Is.True);
        }

        [TestCase]
        //[Ignore("To save time")]
        public void Push_WhenPushedTwoObject_ReturnExpectedResult()
        {
            // Arrange
            // Act
            _stack.Push("aaa");
            _stack.Push("bbb");
            // Assert
            Assert.That(_stack.Count == 2, Is.True);
        }

        [TestCase]
        //[Ignore("To save time")]
        public void Pop_WhenCalled_ReturnExpectedResult()
        {
            // Arrange
            // Act
            _stack.Push("aaa");
            _stack.Push("aaa");
            _stack.Push("ccc");
            var result = _stack.Pop();
            // Assert
            Assert.That(result.Equals("ccc"), Is.True);
            Assert.That(_stack.Count == 2, Is.True);
        }

        [TestCase]
        //[Ignore("To save time")]
        public void Peek_WhenCalled_ReturnExpectedResult()
        {
            // Arrange
            // Act
            _stack.Push("aaa");
            _stack.Push("aaa");
            _stack.Push("ccc");
            var result = _stack.Peek();
            // Assert
            Assert.That(result.Equals("ccc"), Is.True);
            Assert.That(_stack.Count == 3, Is.True);
        }
    }
}