using System;
using System.Data;

using Ridavei.Settings.SqlServer.Settings;

using Ridavei.Settings.DbAbstractions.Managers;
using Ridavei.Settings.DbAbstractions.Settings;

namespace Ridavei.Settings.SqlServer.Managers
{
    /// <summary>
    /// SQL Server manager class used to retrieve settings from the database using <see cref="IDbConnection"/>.
    /// </summary>
    internal sealed class SqlServerConnectionManager : ADbConnectionManager
    {
        /// <summary>
        /// The default constructor for <see cref="SqlServerConnectionManager"/> class.
        /// </summary>
        /// <param name="connection">Database connection object</param>
        /// <exception cref="ArgumentNullException">Throwed when the connection is null.</exception>
        public SqlServerConnectionManager(IDbConnection connection) : base(connection) { }

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
