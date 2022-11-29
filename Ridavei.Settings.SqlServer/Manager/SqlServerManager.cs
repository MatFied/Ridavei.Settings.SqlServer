using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using Ridavei.Settings.Base;
using Ridavei.Settings.DbAbstractions.Manager;
using Ridavei.Settings.DbAbstractions.Settings;

using Ridavei.Settings.SqlServer.Settings;

namespace Ridavei.Settings.SqlServer.Manager
{
    internal sealed class SqlServerManager : ADbManager
    {
        public SqlServerManager(string connectionString) : base(SqlClientFactory.Instance, connectionString) { }

        public SqlServerManager(IDbConnection connection) : base(connection) { }

        /// <inheritdoc/>
        protected override ADbSettings CreateDbSettings(string dictionaryName, DbProviderFactory dbFactory, string connectionString)
        {
            return new SqlServerSettings(dictionaryName, dbFactory, connectionString);
        }

        /// <inheritdoc/>
        protected override ADbSettings CreateDbSettings(string dictionaryName, IDbConnection connection)
        {
            return new SqlServerSettings(dictionaryName, connection);
        }

        /// <inheritdoc/>
        protected override bool TryGetDbSettingsObject(string dictionaryName, DbProviderFactory dbFactory, string connectionString, out ASettings settings)
        {
            settings = CreateDbSettings(dictionaryName, dbFactory, connectionString);
            return true;
        }

        /// <inheritdoc/>
        protected override bool TryGetDbSettingsObject(string dictionaryName, IDbConnection connection, out ASettings settings)
        {
            settings = CreateDbSettings(dictionaryName, connection);
            return true;
        }
    }
}
