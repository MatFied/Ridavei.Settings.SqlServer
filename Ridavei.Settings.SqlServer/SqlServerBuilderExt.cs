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
        /// <returns>Builder</returns>
        public static SettingsBuilder UseSqlServerManager(this SettingsBuilder builder, string connectionString, SqlCredential credential = null)
        {
            return builder.SetManager(new SqlServerManager(connectionString, credential));
        }
    }
}