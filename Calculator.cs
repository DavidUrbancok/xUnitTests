using System.Linq;

namespace xUnitPToject
{
    public class Calculator
    {
        public int Add(int a, int b)
            => a + b;

        public int AddByParams(params int[] numbers)
            => numbers.Sum();
    }
}
