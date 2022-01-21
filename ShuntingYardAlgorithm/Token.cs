namespace ShuntingYardAlgorithm
{
    /// <summary>
    /// Hold's a text, and type to be used by the ShuntingYardAlgorithm.
    /// </summary>
    public struct Token
    {
        public Token(string text, TokenType type)
        {
            Text = text;
            Type = type;
        }

        public readonly string Text;
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