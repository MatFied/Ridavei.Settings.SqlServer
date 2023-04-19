namespace Ridavei.Settings.SqlServer.Tests.Managers
{
    public sealed class SqlServerConnectionManagerTests : AManagersTests
    {
        protected override void SetManager()
        {
            Builder.UseSqlServerManager("Server=localhost;Database=Ridavei;Trusted_Connection=True;");
        }
    }
}
