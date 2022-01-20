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
    
    // [Test]
    // public void ()
    // {
    //     ShuntingYardAlgorithm shuntingYardAlgorithm = new();
    // }
}