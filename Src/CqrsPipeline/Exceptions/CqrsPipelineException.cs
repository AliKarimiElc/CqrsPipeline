namespace CqrsPipeline.Exceptions;

/// <summary>
/// Base class for all CqrsPipeline exceptions
/// </summary>
public class CqrsPipelineException:Exception
{
    /// <summary>
    /// Input parameters of exception
    /// </summary>
    public ExceptionParameter[]? ExceptionParameters { get; set; }


    /// <summary>
    /// Error code that indicate error
    /// </summary>
    public string? ErrorCode { get; }

    /// <summary>
    /// Create new instance
    /// </summary>
    /// <param name="message"></param>
    /// <param name="parameters"></param>
    public CqrsPipelineException(string message, params ExceptionParameter[] parameters) : base(message)
    {
        ExceptionParameters = parameters;
    }

    /// Create new instance
    public CqrsPipelineException(string? errorCode, string message, params ExceptionParameter[] parameters) : base(message)
    {
        ExceptionParameters = parameters;
        ErrorCode = errorCode;
    }

    /// <summary>
    /// Parameters
    /// </summary>
    public string[]? Parameters { get; set; }

    /// <summary>
    /// Create new instance
    /// </summary>
    /// <param name="message"></param>
    /// <param name="parameters"></param>
    public CqrsPipelineException(string message, params string[] parameters) : base(message)
    {
        Parameters = parameters;
    }
}