namespace HunMmdEndpoints.Exceptions
{
    public class SchemaDownloadFailureException: Exception
    {
        public SchemaDownloadFailureException(string? message, Exception? e) : base(message, e)
        {

        }
    }
}
