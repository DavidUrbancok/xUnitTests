using System.Collections;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

//[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace xUnitPToject
{
    public class TestCaseDataClass : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 10, 1, 2, 3, 4 };
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }

    public class XUnitTests : IClassFixture<Calculator>
    {
        public static IEnumerable<object[]> TestData()
        {
            yield return new object[] { 10, new int[] { 1, 2, 3, 4 } };
            yield return new object[] { 5, new int[] { 2, 3 } };
        }

        private readonly Calculator _calculator;
        private readonly ITestOutputHelper _testOutputHelper;

        public XUnitTests(ITestOutputHelper testOutputHelper, Calculator calculator)
        {
            _calculator = calculator;
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void AddTest()
        {
            var sum = _calculator.Add(5, 5);

            Assert.Equal(10, sum);
        }

        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(1, 2, 3)]
        public void AddTests(int a, int b, int expected)
        {
            Assert.Equal(expected, _calculator.Add(a, b));
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void AddTestsFromData(int expected, params int[] numbers)
        {
            Assert.Equal(expected, _calculator.AddByParams(numbers));
        }

        [Theory]
        [ClassData(typeof(TestCaseDataClass))]
        public void AddTestsFromClass(int expected, params int[] numbers)
        {
            Assert.Equal(expected, _calculator.AddByParams(numbers));
        }
    }
}
