using System.Data.SqlClient;

using Ridavei.Settings.Base;

using Ridavei.Settings.SqlServer.Settings;

namespace Ridavei.Settings.SqlServer.Manager
{
    /// <summary>
    /// In memory manager class used to retrieve settings using <see cref="SqlServerSettings"/>.
    /// </summary>
    internal sealed class SqlServerManager : AManager
    {
        private readonly string _connectionString;
        private readonly SqlCredential _credential;

        /// <summary>
        /// The default constructor for <see cref="SqlServerManager"/> class.
        /// </summary>
        public SqlServerManager(string connectionString, SqlCredential credential = null) : base()
        {
            _connectionString = connectionString;
            _credential = credential;
        }

        /// <inheritdoc/>
        protected override ASettings CreateSettingsObject(string dictionaryName)
        {
            return new SqlServerSettings(dictionaryName, _connectionString, _credential);
        }

        /// <inheritdoc/>
        protected override bool TryGetSettingsObject(string dictionaryName, out ASettings settings)
        {
            settings = CreateSettingsObject(dictionaryName);
            return true;
        }
    }
}
