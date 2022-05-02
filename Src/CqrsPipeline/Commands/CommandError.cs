namespace CqrsPipeline.Commands
{
    /// <summary>
    /// An envelope class for command errors
    /// </summary>
    public class CommandError
    {
        /// <summary>
        /// Error code
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// Error message that you want return back;
        /// </summary>
        public string? Message { get; set; }
        /// <summary>
        /// Name of command property that caused the error 
        /// </summary>
        public string? PropertyName { get; set; }
        /// <summary>
        /// Attempted value of command property that caused the error 
        /// </summary>
        public string? AttemptedValue { get; set; }


        /// <summary>
        /// Create new uninitialized instance of command error
        /// </summary>
        public CommandError()
        {

        }

        /// <summary>
        /// Create new instance of command error with initializing all of it`s property
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="propertyName"></param>
        /// <param name="attemptedValue"></param>
        public CommandError(string? code, string? message = null, string? propertyName = null,
            string? attemptedValue = null)
        {
            Code = code;
            Message = message;
            PropertyName = propertyName;
            AttemptedValue = attemptedValue;
        }

        /// <summary>
        /// Create new instance of command error with initializing error code and error message of it`s property
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public CommandError(string? code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}