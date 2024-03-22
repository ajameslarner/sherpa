namespace Sherpa.Exceptions;

public class TestFailureException : Exception
{
    public TestFailureException() { }
    public TestFailureException(string message) : base(message) { }
    public TestFailureException(string message, Exception inner) : base(message, inner) { }
}
