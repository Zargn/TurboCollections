using TurboCollections;

namespace ShuntingYardAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            ShuntingYardAlgorithm shuntingYardAlgorithm = new();

            TurboQueue<string> result = new TurboQueue<string>();
            while (result.Count == 0)
            {
                result = shuntingYardAlgorithm.ConvertToReversePolish("I AM NOT MATH");
            }
        }
    }

    // public struct MathOperation
    // {
    //     public MathOperation(decimal value, Math calculationMode)
    //     {
    //         this.value = value;
    //         this.calculationMode = calculationMode;
    //     }
    //     
    //     public decimal value;
    //     public Math calculationMode;
    // }

    // public enum Math
    // {
    //     Add,
    //     Subtract,
    //     Divide,
    //     Multiply
    // }

    public class ShuntingYardAlgorithm
    {
        private char[] ValidCharacters = new char[] {'+', '-', '*', '/', '(', ')'};
        
        public TurboQueue<string> ConvertToReversePolish(string input)
        {
            var tokenQueue = GatherTokens(input);
            if (tokenQueue.Count == 0)
                return new TurboQueue<string>();

            return ConvertFromTokenQueue(tokenQueue);
        }
        
        /*
        1.  While there are tokens to be read:
        2.        Read a token
        3.        If it's a number add it to queue
        4.        If it's an operator
        5.               While there's an operator on the top of the stack with greater precedence:
        6.                       Pop operators from the stack onto the output queue
        7.               Push the current operator onto the stack
        8.        If it's a left bracket push it onto the stack
        9.        If it's a right bracket 
        10.            While there's not a left bracket at the top of the stack:
        11.                     Pop operators from the stack onto the output queue.
        12.             Pop the left bracket from the stack and discard it
        13. While there are operators on the stack, pop them to the queue */

        public TurboQueue<string> ConvertFromTokenQueue(TurboQueue<string> tokenQueue)
        {
            
            
            
            
            // TODO: TEMPORARY
            return new TurboQueue<string>();
        }



        /// <summary>
        /// Converts a string into a queue of math tokens.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Returns a queue of tokens, or a empty queue if input is invalid.</returns>
        public TurboQueue<string> GatherTokens(string input)
        {
            TurboQueue<string> result = new();

            string? number = "";
            foreach (var character in input)
            {
                if (Char.IsDigit(character))
                {
                    number += character;
                    continue;
                }
                if (number != null)
                {
                    result.Enqueue(number);
                    number = null;
                }

                if (!CharIsValidCheck(character))
                {
                    return new TurboQueue<string>();
                }
                result.Enqueue(character.ToString());
            }

            return result;
        }

        
        
        /// <summary>
        /// Checks whether the supplied character is a valid character.
        /// </summary>
        /// <param name="character">Character to check</param>
        /// <returns>Boolean representing if the character supplied is valid or not.</returns>
        private bool CharIsValidCheck(char character)
        {
            foreach (var validCharacter in ValidCharacters)
            {
                if (character == validCharacter)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

