using TurboCollections;

namespace ShuntingYardAlgorithm
{
    /// <summary>
    /// Static class that contains methods of calculating a resulting decimal number from a Reverse Polish input.
    /// </summary>
    public static class ReversePolishCalculator
    {
        /// <summary>
        /// Calculate the result of the Reverse Polish Notation inside the inputQueue.
        /// </summary>
        /// <param name="inputQueue">Sequence to use for calculation</param>
        /// <returns>Result of calculation.</returns>
        public static decimal CalculateFromQueue(TurboQueue<Token> inputQueue)
        {
            TurboStack<decimal> result = new();
            
            while (inputQueue.Count != 0)
            {
                var currentToken = inputQueue.Dequeue();
                if (currentToken.Type == TokenType.Number)
                {
                    result.Push(currentToken.Value);
                    continue;
                }
                
                // The reason for the reverse order of first and second value in this method call is that the stack is
                // from right to left, so if we want to do 3-1, that would in the stack be 1, 3. Popping the first
                // number would return 1, and the second time 3.
                result.Push(ApplyOperator(currentToken, result.Pop(), result.Pop()));
            }

            return result.Peek();
        }

        
        
        /// <summary>
        /// Applies the operator to the two most recent values.
        /// </summary>
        /// <param name="currentToken">Current token taken from queue.</param>
        /// <param name="secondValue">Second value</param>
        /// <param name="firstValue">First value</param>
        /// <returns>result of operator application to the two numbers.</returns>
        static decimal ApplyOperator(Token currentToken, decimal secondValue, decimal firstValue)
        {
            switch(currentToken.Type)
            {
                case TokenType.Add:
                    return firstValue + secondValue;
                case TokenType.Subtract:
                    return firstValue - secondValue;
                case TokenType.Divide:
                    return firstValue / secondValue;
                case TokenType.Multiply:
                    return firstValue * secondValue;
            }

            throw new Exception("Token doesn't match any implemented math operator! Your options are Add, Subtract, Divide, Multiply.");
        }
    }
}