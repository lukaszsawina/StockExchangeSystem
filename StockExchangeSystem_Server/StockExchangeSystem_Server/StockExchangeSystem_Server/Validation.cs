namespace StockExchangeSystem_Server
{
    public static class Validation
    {
        public static bool ValidEmail(string email)
        {
            return email.Contains("@");
        }
        public static bool ValidPassword(string password)
        {
            return password.Length > 6 ? true : false;
        }
    }
}
