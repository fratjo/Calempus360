using Calempus360.API.Controllers;
using Calempus360.Errors;

namespace Calempus360.Test.API.Handlers;

public class ExceptionHandlerTest
{
    public readonly TestController _controller = new();
    
    [Fact]
    public void Test_ExceptionHandler()
    {
        // Arrange
        var exception = new Exception("Test exception");
        
        // Act
        try
        {
            var result = _controller.Get();
        }
        catch (Exception ex)
        {
            // Assert
            Assert.NotNull(ex);
            Assert.IsType<TestException>(ex);
        }
    }
}