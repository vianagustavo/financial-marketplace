namespace FinancialMarketplace.Application.Exceptions;

public class MissingEnvironmentVariableException : Exception
{
    public MissingEnvironmentVariableException()
    {
    }

    public MissingEnvironmentVariableException(string variableName) : base($"Environment variable {variableName} is missing")
    {
    }

    public MissingEnvironmentVariableException(string message, Exception innerException) : base(message, innerException)
    {
    }
}