using TurboCollections;

namespace ShuntingYardAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            ShuntingYardAlgorithm shuntingYardAlgorithm = new();

            decimal? result = null;
            while (result == null)
            {
                result = shuntingYardAlgorithm.Calculate("INPUTVALUEHERE")
            }
        }
    }

    public struct MathOperation
    {
        public MathOperation(decimal value1, decimal value2, Math calculationMode)
        {
            this.value1 = value1;
            this.value2 = value2;
            this.calculationMode = calculationMode;
        }
        
        public decimal value1;
        public decimal value2;
        public Math calculationMode;
    }

    public enum Math
    {
        Add,
        Subtract,
        Divide,
        Multiply
    }

    class ShuntingYardAlgorithm
    {
        public decimal? Calculate(string input)
        {
            
            // Calculation failed so null is returned.
            return null;
        }
    }
}

