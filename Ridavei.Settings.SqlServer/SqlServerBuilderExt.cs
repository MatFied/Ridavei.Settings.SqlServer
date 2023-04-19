using System;
using System.Data.SqlClient;

using Ridavei.Settings.SqlServer.Managers;

namespace Ridavei.Settings.SqlServer
{
    /// <summary>
    /// Class used to extend <see cref="SettingsBuilder"/>.
    /// </summary>
    public static class SqlServerBuilderExt
    {
        /// <summary>
        /// Allows to use <see cref="SqlServerProviderFactoryManager"/> as the manager class.
        /// </summary>
        /// <param name="builder">Builder</param>
		/// <param name="connectionString">Connection string to the database</param>
        /// <returns>Builder</returns>
        /// <exception cref="ArgumentNullException">Throwed when the connection string is null, empty or whitespace.</exception>
        public static SettingsBuilder UseSqlServerManager(this SettingsBuilder builder, string connectionString)
        {
            return builder.SetManager(new SqlServerProviderFactoryManager(connectionString));
        }

        /// <summary>
        /// Allows to use <see cref="SqlServerConnectionManager"/> as the manager class.
        /// </summary>
        /// <param name="builder">Builder</param>
        /// <param name="connection">Database connection object</param>
        /// <returns>Builder</returns>
        /// <exception cref="ArgumentNullException">Throwed when the connection is null.</exception>
        public static SettingsBuilder UseSqlServerManager(this SettingsBuilder builder, SqlConnection connection)
        {
            return builder.SetManager(new SqlServerConnectionManager(connection));
        }
    }
}