namespace E_Commerece.API.ExceptionsConfiguration.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string msg) : base(msg)
        {

        }
    }
}
