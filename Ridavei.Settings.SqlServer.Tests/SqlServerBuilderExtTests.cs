using System;
using System.Data.SqlClient;

using NUnit.Framework;
using Shouldly;

namespace Ridavei.Settings.SqlServer.Tests
{
    [TestFixture]
    public sealed class SqlServerBuilderExtTests
    {
        private SettingsBuilder _builder;

        [SetUp]
        public void SetUp()
        {
            _builder = SettingsBuilder
                .CreateBuilder();
        }

        [Test]
        public void UseSqlServerManager_NullConnectionString__RaisesException()
        {
            Should.Throw<ArgumentNullException>(() =>
            {
                _builder.UseSqlServerManager((string)null);
            });
        }

        [Test]
        public void UseSqlServerManager_NullSqlConnection__RaisesException()
        {
            Should.Throw<ArgumentNullException>(() =>
            {
                _builder.UseSqlServerManager((SqlConnection)null);
            });
        }

        [Test]
        public void UseSqlServerManager_ConnectionString__NoException()
        {
            Should.NotThrow(() =>
            {
                _builder.UseSqlServerManager("Server=localhost;Database=Ridavei;Trusted_Connection=True;");
            });
        }

        [Test]
        public void UseSqlServerManager_SqlConnection__NoException()
        {
            Should.NotThrow(() =>
            {
                using (var connection = new SqlConnection("Server=localhost;Database=Ridavei;Trusted_Connection=True;"))
                    _builder.UseSqlServerManager(connection);
            });
        }
    }
}