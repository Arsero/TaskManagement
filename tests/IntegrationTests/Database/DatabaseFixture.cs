namespace IntegrationTests.Database
{
    public class DatabaseFixture : IDisposable
    {
        public ITestDatabase Database { get; private set; }

        public DatabaseFixture()
        {
            Database = TestDatabaseFactory.CreateAsync().Result;
        }

        public void Dispose()
        {
            Database.DisposeAsync();
        }
    }
}
