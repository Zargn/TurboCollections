using TurboCollections;

namespace ShuntingYardAlgorithm
{
    public class ShuntingYardAlgorithm
    {
        private readonly char[] validCharacters = {'+', '-', '*', '/', '(', ')'};
        
        
        
        /// <summary>
        /// Main algorithm method. Calls necessary conversion methods to process the input.
        /// </summary>
        /// <param name="input">string to process</param>
        /// <returns>Token queue in reverse polish format, or empty if the input was invalid.</returns>
        public TurboQueue<Token> ConvertToReversePolish(string input)
        {
            var tokenQueue = GatherTokens(input);
            return tokenQueue.Count == 0 ? new TurboQueue<Token>() : ConvertFromTokenQueue(tokenQueue);
        }
        
        

        /// <summary>
        /// Does the processing of the algorithm. 
        /// </summary>
        /// <param name="tokenQueue">Queue of tokens to process.</param>
        /// <returns>Processed token queue in reverse polish format</returns>
        public TurboQueue<Token> ConvertFromTokenQueue(TurboQueue<Token> tokenQueue)
        {
            TurboQueue<Token> result = new();
            TurboStack<Token> stack = new();

            while (tokenQueue.Count != 0)
            {
                var currentToken = tokenQueue.Dequeue();
                Console.WriteLine($"Token: text:{currentToken.Text} type:{currentToken.Type}");
                
                if (currentToken.Type == TokenType.Number)
                    result.Enqueue(currentToken);

                if (currentToken.Type is >= TokenType.Add and <= TokenType.Multiply)
                {
                    ProcessOperator(stack, currentToken, result);
                }
                
                if (currentToken.Type is TokenType.LeftBracket)
                {
                    stack.Push(currentToken);
                    Console.WriteLine($"Pushing {currentToken.Text} to stack");
                }

                if (currentToken.Type is TokenType.RightBracket)
                {
                    ProcessRightBracket(stack, result);
                }
            }

            // Move any final operators to the result queue.
            while (stack.Count != 0)
            {
                result.Enqueue(stack.Pop());
            }

            return result;
        }

        
        
        /// <summary>
        /// Processes mathematical operators. 
        /// </summary>
        /// <param name="stack">Current stack</param>
        /// <param name="currentToken">Token being processed</param>
        /// <param name="result">Current result queue</param>
        private static void ProcessOperator(TurboStack<Token> stack, Token currentToken, TurboQueue<Token> result)
        {
            while (stack.Count != 0)
            {
                if (stack.Peek().Type > currentToken.Type && stack.Peek().Type is >= TokenType.Add and <= TokenType.Multiply)
                {
                    Console.WriteLine($"moving {stack.Peek().Text} to queue.");
                    result.Enqueue(stack.Pop());
                }
                else
                    break;
            }

            Console.WriteLine($"Adding {currentToken.Text} to stack");
            stack.Push(currentToken);
        }

        
        
        /// <summary>
        /// Processes right brackets. 
        /// </summary>
        /// <param name="stack">Current stack</param>
        /// <param name="result">Current result queue</param>
        private static void ProcessRightBracket(TurboStack<Token> stack, TurboQueue<Token> result)
        {
            // TODO: This could create a error if the user inputs a ) without a ( before it.
            while (stack.Count != 0)
            {
                if (stack.Peek().Type == TokenType.LeftBracket)
                {
                    break;
                }

                Console.WriteLine($"moving {stack.Peek().Text} to queue.");
                result.Enqueue(stack.Pop());
            }

            Console.WriteLine($"Popping: {stack.Peek().Text}");
            stack.Pop();
        }

        

        /// <summary>
        /// Converts a string into a queue of math tokens.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Returns a queue of tokens, or a empty queue if input is invalid.</returns>
        public TurboQueue<Token> GatherTokens(string input)
        {
            TurboQueue<Token> result = new();

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
                    result.Enqueue(new Token(number, TokenType.Number));
                    number = null;
                }

                if (!CharIsValidCheck(character))
                {
                    return new TurboQueue<Token>();
                }
                result.Enqueue(ConvertOperatorToToken(character));
            }

            return result;
        }



        /// <summary>
        /// Converts a operator char into a Token containing the type and actual operator.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        Token ConvertOperatorToToken(Char character)
        {
            TokenType type = TokenType.Number;
            switch (character)
            {
                case '+':
                    type = TokenType.Add;
                    break;
                case '-':
                    type = TokenType.Subtract;
                    break;
                case '/':
                    type = TokenType.Divide;
                    break;
                case '*':
                    type = TokenType.Multiply;
                    break;
                case '(':
                    type = TokenType.LeftBracket;
                    break;
                case ')':
                    type = TokenType.RightBracket;
                    break;
            }

            return new Token(character.ToString(), type);
        }

        
        
        /// <summary>
        /// Checks whether the supplied character is a valid character.
        /// </summary>
        /// <param name="character">Character to check</param>
        /// <returns>Boolean representing if the character supplied is valid or not.</returns>
        private bool CharIsValidCheck(char character)
        {
            foreach (var validCharacter in validCharacters)
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