[TestClass]
public class CalculatorFunctionTests
{
    [TestMethod]
    public void TestAdd()
    {
        // Arrange
        var request = new DefaultHttpContext().Request;
        request.Query = new QueryCollection(new Dictionary<string, StringValues>
        {
            { "operation", "add" },
            { "num1", "2" },
            { "num2", "3" }
        });
        var logger = Mock.Of<ILogger>();

        // Act
        var response = (OkObjectResult)CalculatorFunction.Run(request, logger).Result;

        // Assert
        Assert.AreEqual(5, response.Value);
    }

    // Similar tests for subtract, multiply, divide...

    [TestMethod]
    public void TestDivisionByZero()
    {
        // Arrange
        var request = new DefaultHttpContext().Request;
        request.Query = new QueryCollection(new Dictionary<string, StringValues>
        {
            { "operation", "divide" },
            { "num1", "5" },
            { "num2", "0" }
        });
        var logger = Mock.Of<ILogger>();

        // Act
        var response = (BadRequestObjectResult)CalculatorFunction.Run(request, logger).Result;

        // Assert
        Assert.AreEqual("Error: Division by zero.", response.Value);
    }

    // Additional tests for error handling, invalid operations, missing parameters...
}
