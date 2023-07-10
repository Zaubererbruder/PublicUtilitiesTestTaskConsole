namespace PublicUtilitiesData
{
    public class PublicUtilities
    {
        internal PublicUtilities(ICalculatorsLoader calculatorsLoader)
        {
            Calculations = new Calculations(calculatorsLoader.GetCalculators());
        }

        public Calculations Calculations { get; private set; }

        public static PublicUtilitiesFactory Create(string connectionString)
        {
            return new PublicUtilitiesFactory(connectionString);
        }
    }
}
