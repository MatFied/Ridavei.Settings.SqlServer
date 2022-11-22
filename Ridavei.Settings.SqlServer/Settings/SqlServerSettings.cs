using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using Ridavei.Settings.Base;
using Ridavei.Settings.SqlServer.Constants;

namespace Ridavei.Settings.SqlServer.Settings
{
    /// <summary>
    /// In memory settings class that uses <see cref="Dictionary{TKey, TValue}"/> for storing keys and values.
    /// </summary>
    internal sealed class SqlServerSettings : ASettings
    {
        private static readonly string KeyExistsQuery = $@"
IF EXISTS (SELECT 1 FROM TABLENAME WHERE [Dictionary] = @{Const.DictionaryParamName} AND [Key] = @{Const.KeyParamName})
SELECT 1
ELSE
SELECT 0";
        private readonly SqlConnection _connection;
        private readonly SqlParameter _dictionaryParam;

        /// <summary>
        /// The default constructor for <see cref="SqlServerSettings"/> class.
        /// </summary>
        /// <param name="dictionaryName">Name of the dictionary</param>
        /// <exception cref="ArgumentNullException">Throwed when the name of the dictionary is null, empty or whitespace.</exception>
        public SqlServerSettings(string dictionaryName, string connectionString, SqlCredential credential = null) : base(dictionaryName)
        {
            _connection = new SqlConnection(connectionString, credential);
            _connection.Open();

            _dictionaryParam = new SqlParameter(DictionaryParamName, dictionaryName);
        }

        /// <inheritdoc/>
        protected override void SetValue(string key, string value)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "";

                cmd.ExecuteNonQuery();
            }
        }

        /// <inheritdoc/>
        protected override bool TryGetValue(string key, out string value)
        {
            value = null;
            return true;
        }

        /// <inheritdoc/>
        protected override IReadOnlyDictionary<string, string> GetAllValues()
        {
            return null;
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _connection?.Dispose();
        }

        private bool CheckIfKeyExists(string key)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = KeyExistsQuery;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(_dictionaryParam);

                return Convert.ToInt32(cmd.ExecuteScalar()) == 1;
            }
        }
    }
}
