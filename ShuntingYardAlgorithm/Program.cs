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
            /*
                While there are tokens in the input queue:
                Dequeue a token
                If token is a number
                                Add it to the result stack
                If token is a operator
                                Pop the two top numbers in the stack and apply the operator to them.
                                Push the result back to the stack.
             */
            

            // TurboStack<int> result = new();
            //
            // while (inputQueue.Count != 0)
            // {
            //     var currentToken = inputQueue.Dequeue();
            //     if (currentToken.Type == TokenType.Number)
            //         result.Push(result);
            // }

            return 0;
        }
    }
}

