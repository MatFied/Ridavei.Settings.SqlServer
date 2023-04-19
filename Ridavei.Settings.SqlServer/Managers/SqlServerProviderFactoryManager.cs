using System;
using System.Data.SqlClient;

using Ridavei.Settings.SqlServer.Settings;

using Ridavei.Settings.DbAbstractions.Managers;
using Ridavei.Settings.DbAbstractions.Settings;

namespace Ridavei.Settings.SqlServer.Managers
{
    /// <summary>
    /// SQL Server manager class used to retrieve settings from the database using <see cref="SqlClientFactory"/> and the ConnectionString.
    /// </summary>
    internal sealed class SqlServerProviderFactoryManager : ADbProviderFactoryManager
    {
        /// <summary>
        /// The default constructor for <see cref="SqlServerProviderFactoryManager"/> class.
        /// </summary>
		/// <param name="connectionString">Connection string to the database</param>
        /// <exception cref="ArgumentNullException">Throwed when the connection string is null, empty or whitespace.</exception>
        public SqlServerProviderFactoryManager(string connectionString) : base(SqlClientFactory.Instance, connectionString) { }

        /// <inheritdoc/>
        protected override ADbSettings CreateDbSettings(string dictionaryName)
        {
            return new SqlServerSettings(dictionaryName);
        }

        /// <inheritdoc/>
        protected override bool TryGetDbSettingsObject(string dictionaryName, out ADbSettings settings)
        {
            settings = CreateDbSettings(dictionaryName);
            return true;
        }
    }
}
