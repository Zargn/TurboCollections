using NUnit.Framework;
using ShuntingYardAlgorithm;


public class Tests
{
    [Test]
    public void GatherTokensReturnsFalseByIncorrectInput()
    {
        var result = ShuntingYardAlgorithm.ShuntingYardAlgorithm.GatherTokens("Hello there i am not math and should return a 0 lenght queue.");
        Assert.AreEqual(0, result.Count);
    }
    
    [Test]
    public void GatherTokensReturnsCorrectQueue()
    {
        var result = ShuntingYardAlgorithm.ShuntingYardAlgorithm.GatherTokens("5+32/(4*4)");


        Token[] comparison = {
            new(5, TokenType.Number), new(0, TokenType.Add), new(32, TokenType.Number),
            new(0, TokenType.Divide), new(0, TokenType.LeftBracket), new(4, TokenType.Number),
            new(0, TokenType.Multiply), new(4, TokenType.Number), new(0, TokenType.RightBracket)
        };
        
        

        int index = 0;
        foreach (var token in result)
        {
            Assert.AreEqual(comparison[index], token);
            index++;
        }
    }
    
    [Test]
    public void ConvertToReversePolishReturnsCorrectQueue()
    {
        var result = ShuntingYardAlgorithm.ShuntingYardAlgorithm.ConvertToReversePolish("4+18/(9-3)");
        
        Token[] comparison = {
            new(4, TokenType.Number), new(18, TokenType.Number), new(9, TokenType.Number),
            new(3, TokenType.Number), new(0, TokenType.Subtract), new(0, TokenType.Divide),
            new(0, TokenType.Add)
        };

        int index = 0;
        foreach (var token in result)
        {
            Assert.AreEqual(comparison[index].Value, token.Value);
            Assert.AreEqual(comparison[index].Type, token.Type);
            index++;
        }
    }
    
    [Test]
    public void FullCalculationReturnsCorrectValue()
    {
        var result = ReversePolishCalculator.CalculateFromQueue(ShuntingYardAlgorithm.ShuntingYardAlgorithm.ConvertToReversePolish("(3+5)*(7-2)"));
        Assert.AreEqual(40m, result);
    }
}