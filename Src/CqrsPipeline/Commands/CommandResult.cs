namespace CqrsPipeline.Commands
{
    /// <summary>
    /// Envelope class for Command result without any data and result and only indicate Successfully
    /// command execution or not
    /// </summary>
    public class CommandResult
    {
        /// <summary>
        /// Indicate command successfully executed
        /// </summary>
        public bool Success { get; protected set; }
        /// <summary>
        /// If command is successfully and you want return message fill this property
        /// </summary>
        public string? SuccessMessage { get; protected set; }
        /// <summary>
        /// If command is unsuccessfully this contains errors 
        /// </summary>
        public IEnumerable<CommandError>? Errors { get; protected set; }

        /// <summary>
        /// Create successful command result that not have any message
        /// </summary>
        public CommandResult()
        {
            Success = true;
            Errors = null;
        }

        /// <summary>
        /// Create successful command result whit message
        /// </summary>
        /// <param name="successMessage"></param>
        public CommandResult(string successMessage)
        {
            SuccessMessage = successMessage;
            Success = true;
            Errors = null;
        }

        /// <summary>
        /// Create Unsuccessful command with errors
        /// </summary>
        /// <param name="errors"></param>
        public CommandResult(params CommandError[] errors)
        {
            Success = false;
            SuccessMessage = null;
            Errors = errors;
        }
        /// <summary>
        /// Create Unsuccessful command with errors
        /// </summary>
        /// <param name="errors"></param>
        public CommandResult(IEnumerable<CommandError> errors)
        {
            Success = false;
            SuccessMessage = null;
            Errors = errors;
        }


        /// <summary>
        /// Add errors to command result
        /// </summary>
        /// <param name="errors"></param>
        public void AddError(params CommandError[] errors)
        {
            Success = false;
            SuccessMessage = null;
            Errors = errors;
        }

        /// <summary>
        /// Add errors to command result
        /// </summary>
        /// <param name="errors"></param>
        public void AddError(IEnumerable<CommandError> errors)
        {
            Success = false;
            SuccessMessage = null;
            Errors = errors;
        }
    }

    /// <summary>
    /// Envelope class for Command result that returns you some data for command execution
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class CommandResult<TData> : CommandResult
    {
        TData? Data { get; }

        /// <summary>
        /// Create successful command result that not have any message
        /// </summary>
        protected CommandResult() : base()
        {
        }

        /// <summary>
        /// Create successful command result whit data
        /// </summary>
        /// <param name="data"></param>
        public CommandResult(TData data) : base()
        {
            Data = data;
        }

        /// <summary>
        /// Create successful command result with data and message
        /// </summary>
        /// <param name="data"></param>
        /// <param name="successMessage"></param>
        public CommandResult(TData data, string successMessage) : base(successMessage)
        {
            Data = data;
        }

        /// <summary>
        /// Create Unsuccessful command with errors
        /// </summary>
        /// <param name="errors"></param>
        public CommandResult(params CommandError[] errors):base(errors)
        {
            Data = default;
        }

        /// <summary>
        /// Create Unsuccessful command with errors
        /// </summary>
        /// <param name="errors"></param>
        public CommandResult(IEnumerable<CommandError> errors):base(errors)
        {
            Data = default;
        }
    }
}
