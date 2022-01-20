using TurboCollections;

namespace ShuntingYardAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            ShuntingYardAlgorithm shuntingYardAlgorithm = new();

            TurboQueue<Token> result = new TurboQueue<Token>();
            while (result.Count == 0)
            {
                result = shuntingYardAlgorithm.ConvertToReversePolish("I AM NOT MATH");
            }
        }
    }
    
    
    
    public struct Token
    {
        public Token(string text, TokenType type)
        {
            Text = text;
            Type = type;
        }

        public string Text;
        public TokenType Type;
    }


    public enum TokenType
    {
        Number = 0,
        Add = 1,
        Subtract = 2,
        Divide = 3,
        Multiply = 4,
        LeftBracket = 5,
        RightBracket = 6
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
        
        public TurboQueue<Token> ConvertToReversePolish(string input)
        {
            var tokenQueue = GatherTokens(input);
            if (tokenQueue.Count == 0)
                return new TurboQueue<Token>();

            return ConvertFromTokenQueue(tokenQueue);
        }
        
        /*
         
         Parentheses, Exponents, Multiplication and Division, Addition and Subtraction (from left to right). 
        1.  While there are tokens to be read:
        2.        Read a token
        3.        If it's a number add it to queue
        4.        If it's an operator
        5.               While there's an operator on the top of the stack with greater precedence:
        6.                       Pop operators from the stack onto the output queue
        7.               Push the current operator onto the stack
        8.        If it's a left bracket push it onto the stack
        9.        If it's a right bracket 
        10.              While there's not a left bracket at the top of the stack:
        11.                      Pop operators from the stack onto the output queue.
        12.              Pop the left bracket from the stack and discard it
        13. While there are operators on the stack, pop them to the queue */

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
                    while (stack.Count != 0)
                    {
                        if (stack.Peek().Type > currentToken.Type)
                        {
                            result.Enqueue(stack.Pop());
                        }
                        else
                            break;
                    }

                    Console.WriteLine($"Adding {currentToken.Text} to stack");
                    stack.Push(currentToken);
                }

                if (currentToken.Type is TokenType.LeftBracket)
                {
                    stack.Push(currentToken);
                    Console.WriteLine($"Pushing {currentToken.Text} to stack");
                }

                if (currentToken.Type is TokenType.RightBracket)
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
            }

            while (stack.Count != 0)
            {
                result.Enqueue(stack.Pop());
            }

            // TODO: TEMPORARY
            return result;
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

