using System.Data.SqlClient;

using Ridavei.Settings.SqlServer.Manager;

namespace Ridavei.Settings.SqlServer
{
    /// <summary>
    /// Class used to extend <see cref="SettingsBuilder"/>.
    /// </summary>
    public static class SqlServerBuilderExt
    {
        /// <summary>
        /// Allows to use <see cref="SqlServerManager"/> as the manager class.
        /// </summary>
        /// <param name="builder">Builder</param>
		/// <param name="connectionString">Connection string to the database</param>
        /// <returns>Builder</returns>
        public static SettingsBuilder UseSqlServerManager(this SettingsBuilder builder, string connectionString)
        {
            return builder.SetManager(new SqlServerManager(connectionString));
        }

        /// <summary>
        /// Allows to use <see cref="SqlServerManager"/> as the manager class.
        /// </summary>
        /// <param name="builder">Builder</param>
        /// <param name="connection">Database connection object</param>
        /// <returns>Builder</returns>
        public static SettingsBuilder UseSqlServerManager(this SettingsBuilder builder, SqlConnection connection)
        {
            return builder.SetManager(new SqlServerManager(connection));
        }
    }
}