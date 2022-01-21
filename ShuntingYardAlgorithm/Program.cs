using TurboCollections;

namespace ShuntingYardAlgorithm
{
    class Program
    {
        private static void Main()
        {
            ShuntingYardAlgorithm shuntingYardAlgorithm = new();

            TurboQueue<Token> result = new TurboQueue<Token>();
            while (result.Count == 0)
            {
                // TODO: Add proper method for input here.
                result = shuntingYardAlgorithm.ConvertToReversePolish("I AM NOT MATH");
                
                // TODO: Add call to math calculation method here.
            }
        }
    }

    public static class ReversePolishCalculator
    {
        public static decimal CalculateFromQueue(TurboQueue<Token> inputQueue)
        {


            return 0;
        }
    }
}

