namespace ShuntingYardAlgorithm
{
    /// <summary>
    /// Hold's a text, and type to be used by the ShuntingYardAlgorithm.
    /// </summary>
    public struct Token
    {
        public Token(decimal value, TokenType type)
        {
            Value = value;
            Type = type;
        }

        public readonly decimal Value;
        public readonly TokenType Type;
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
}