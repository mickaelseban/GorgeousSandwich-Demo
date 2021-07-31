namespace Infrastructure.CrossCutting
{
    public class DbConnectionString
    {
        public DbConnectionString(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}