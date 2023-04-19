using System.Data.SqlClient;

namespace Ridavei.Settings.SqlServer.Tests.Managers
{
    public sealed class SqlServerProviderFactoryManagerTests : AManagersTests
    {
        private SqlConnection _connection;

        protected override void SetUp()
        {
            base.SetUp();
            _connection = new SqlConnection("Server=localhost;Database=Ridavei;Trusted_Connection=True;");
        }

        protected override void TearDown()
        {
            _connection.Dispose();
        }

        protected override void SetManager()
        {
            Builder.UseSqlServerManager(_connection);
        }
    }
}
