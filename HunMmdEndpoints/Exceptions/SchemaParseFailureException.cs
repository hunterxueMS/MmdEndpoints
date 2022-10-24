namespace HunMmdEndpoints.Exceptions
{
    public class SchemaParseFailureException : Exception
    {
        public SchemaParseFailureException(string message, Exception e) : base(message, e)
        {

        }
    }
}
