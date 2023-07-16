namespace CqrsPipeline.Exceptions
{
    /// <summary>
    /// Input parameters for exceptions 
    /// </summary>
    public class ExceptionParameter
    {
        /// <summary>
        /// Name of input parameter
        /// </summary>
        public string? PropertyName { get; set; }
        /// <summary>
        /// Attempted value of input parameter
        /// </summary>
        public string? AttemptedValue { get; set; }
    }
}