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
                result = shuntingYardAlgorithm.Calculate("INPUTVALUEHERE");
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

    public class ShuntingYardAlgorithm
    {
        private char[] ValidCharacters = new char[] {'+', '-', '*', '/', '(', ')'};
        
        public decimal? Calculate(string input)
        {
            if (!EnsureInputValidity(input))
                return null;
                // Calculation failed so null is returned.
            return 5;
        }

        private bool EnsureInputValidity(string input)
        {
            foreach (var character in input)
            {
                
            }
            return false;
        }

        public TurboQueue<string> GatherTokens(string input)
        {
            TurboQueue<string> result = new();

            string? number = "";
            foreach (var character in input)
            {
                if (Char.IsDigit(character))
                {
                    number = character + number;
                    continue;
                }
                else if (number != null)
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

