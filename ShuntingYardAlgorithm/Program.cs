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
        public MathOperation(decimal value, Math calculationMode)
        {
            this.value = value;
            this.calculationMode = calculationMode;
        }
        
        public decimal value;
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

