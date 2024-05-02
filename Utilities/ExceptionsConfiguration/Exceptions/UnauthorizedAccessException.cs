namespace E_Commerece.API.ExceptionsConfiguration.Exceptions
{
    public class UnauthorizedAccessException : Exception
    {
        public UnauthorizedAccessException(string msg) : base(msg)
        {
        }
    }
}
