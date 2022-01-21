using System;
using NUnit.Framework;

namespace ShuntingYardAlgorithm.Test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void GatherTokensReturnsFalseByIncorrectInput()
    {
        ShuntingYardAlgorithm shuntingYardAlgorithm = new();
        var result = shuntingYardAlgorithm.GatherTokens("Hello there i am not math");
        Assert.AreEqual(0, result.Count);
    }
    
    [Test]
    public void GatherTokensReturnsCorrectQueue()
    {
        ShuntingYardAlgorithm shuntingYardAlgorithm = new();
        var result = shuntingYardAlgorithm.GatherTokens("5+32/(4*4)");
        

        Token[] comparison = {
            new(5, TokenType.Number), new(0, TokenType.Add), new(32, TokenType.Number),
            new(0, TokenType.Divide), new(0, TokenType.LeftBracket), new(4, TokenType.Number),
            new(0, TokenType.Multiply), new(4, TokenType.Number), new(0, TokenType.RightBracket)
        };
        
        

        int index = 0;
        foreach (var VARIABLE in result)
        {
            Assert.AreEqual(comparison[index], VARIABLE);
            index++;
        }
    }
    
    [Test]
    public void ConvertFromTokenQueue()
    {
        ShuntingYardAlgorithm shuntingYardAlgorithm = new();

        var result = shuntingYardAlgorithm.ConvertToReversePolish("4+18/(9-3)");

        
        // {"4", "18", "9", "3", "-", "/", "+"};
        Token[] comparison = {
            new(4, TokenType.Number), new(18, TokenType.Number), new(9, TokenType.Number),
            new(3, TokenType.Number), new(0, TokenType.Subtract), new(0, TokenType.Divide),
            new(0, TokenType.Add)
        };

        foreach (var VARIABLE in result)
        {
            Console.WriteLine(VARIABLE.Value);
        }

        int index = 0;
        foreach (var token in result)
        {
            Assert.AreEqual(comparison[index].Value, token.Value);
            Assert.AreEqual(comparison[index].Type, token.Type);
            index++;
        }
        //
        // for (int i = 0; result.Count != 0; i++)
        // {
        //     Assert.AreEqual(comparison[i], result.Dequeue());
        // }
    }
    
    [Test]
    public void FullCalculationReturnsCorrectValue()
    {
        ShuntingYardAlgorithm shuntingYardAlgorithm = new();
        var result = ReversePolishCalculator.CalculateFromQueue(shuntingYardAlgorithm.ConvertToReversePolish("(3+5)*(7-2)"));
        Assert.AreEqual(40m, result);
    }
    
    // [Test]
    // public void ()
    // {
    //     ShuntingYardAlgorithm shuntingYardAlgorithm = new();
    // }
}