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

        string[] comparison = new string[] {"5", "+", "32", "/", "(", "4", "*", "4", ")"};
        int index = 0;
        foreach (var VARIABLE in result)
        {
            Assert.AreEqual(comparison[index], VARIABLE.Text);
            index++;
        }
    }
    
    [Test]
    public void ConvertFromTokenQueue()
    {
        ShuntingYardAlgorithm shuntingYardAlgorithm = new();

        var result = shuntingYardAlgorithm.ConvertToReversePolish("4+18/(9-3)");

        string[] comparison = new string[] {"4", "18", "9", "3", "-", "/", "+"};

        foreach (var VARIABLE in result)
        {
            Console.WriteLine(VARIABLE.Text);
        }
        
        int i = 0;
        while (result.Count != 0)
        {
            Assert.AreEqual(comparison[i], result.Dequeue().Text);
            i++;
        }
    }
    
    [Test]
    public void FullCalculationReturnsCorrectValue()
    {
        ShuntingYardAlgorithm shuntingYardAlgorithm = new();
        var result = ReversePolishCalculator.CalculateFromQueue(shuntingYardAlgorithm.ConvertToReversePolish("(3+5)*(7-2)"));
        Assert.AreEqual(40, result);
    }
    
    // [Test]
    // public void ()
    // {
    //     ShuntingYardAlgorithm shuntingYardAlgorithm = new();
    // }
}