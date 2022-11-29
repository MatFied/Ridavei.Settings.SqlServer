using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

using Ridavei.Settings.DbAbstractions.Settings;

namespace Ridavei.Settings.SqlServer.Settings
{
    internal sealed class SqlServerSettings : ADbSettings
    {
        private const string DictionaryParam = "@DictionaryPar";
        private const string KeyParam = "@KeyPar";
        private const string ValueParam = "@ValuePar";

        private static readonly string AddOrUpdateValueQuery = $@"
MERGE [{TableName}] AS trg
USING (SELECT {DictionaryParam} AS [{DictionaryColumnName}], {KeyParam} AS [{SettingsKeyColumnName}], {ValueParam} AS [{SettingsValueColumnName}]) AS src
ON (trg.[{DictionaryColumnName}] = src.[{DictionaryColumnName}] AND trg.[{SettingsKeyColumnName}] = src.[{SettingsKeyColumnName}])
WHEN MATCHED THEN
UPDATE SET [{SettingsValueColumnName}] = src.[{SettingsValueColumnName}]
WHEN NOT MATCHED THEN
INSERT ([{DictionaryColumnName}], [{SettingsKeyColumnName}], [{SettingsValueColumnName}])
VALUES (src.[{DictionaryColumnName}], src.[{SettingsKeyColumnName}], src.[{SettingsValueColumnName}]);";
        private static readonly string GetAllValuesFromDbQuery = $"SELECT [{SettingsKeyColumnName}], [{SettingsValueColumnName}] FROM [{TableName}] WHERE [{DictionaryColumnName}] = {DictionaryParam}";
        private static readonly string GetValueQuery = $"SELECT [{SettingsValueColumnName}] FROM [{TableName}] WHERE [{DictionaryColumnName}] = {DictionaryParam} AND [{SettingsKeyColumnName}] = {KeyParam}";

        public SqlServerSettings(string dictionaryName, DbProviderFactory dbFactory, string connectionString) : base(dictionaryName, dbFactory, connectionString) { }

        public SqlServerSettings(string dictionaryName, IDbConnection connection) : base(dictionaryName, connection) { }

        /// <inheritdoc/>
        protected override int AddOrUpdateValueInDb(IDbConnection connection, string key, string value)
        {
            using (IDbCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = AddOrUpdateValueQuery;
                cmd.Parameters.Add(CreateParameter(cmd, DictionaryParam, DictionaryName));
                cmd.Parameters.Add(CreateParameter(cmd, KeyParam, key));
                cmd.Parameters.Add(CreateParameter(cmd, ValueParam, value));

                return cmd.ExecuteNonQuery();
            }
        }

        /// <inheritdoc/>
        protected override IReadOnlyDictionary<string, string> GetAllValuesFromDb(IDbConnection connection)
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            using (IDbCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = GetAllValuesFromDbQuery;
                cmd.Parameters.Add(CreateParameter(cmd, DictionaryParam, DictionaryName));

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        res.Add(reader.GetString(0), reader.IsDBNull(1) ? string.Empty : reader.GetString(1));
                }
            }

            return res;
        }

        /// <inheritdoc/>
        protected override bool TryGetValueFromDb(IDbConnection connection, string key, out string value)
        {
            value = string.Empty;
            using (IDbCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = GetValueQuery;
                cmd.Parameters.Add(CreateParameter(cmd, DictionaryParam, DictionaryName));
                cmd.Parameters.Add(CreateParameter(cmd, KeyParam, key));

                var obj = cmd.ExecuteScalar();
                if (obj != null && obj != DBNull.Value)
                {
                    value = obj.ToString();
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Creates parameter for the command.
        /// </summary>
        /// <param name="cmd">Database command</param>
        /// <param name="parameterName">Name of the parameter</param>
        /// <param name="value">Value of the parameter</param>
        /// <returns>Parameter</returns>
        private static IDbDataParameter CreateParameter(IDbCommand cmd, string parameterName, object value)
        {
            var res = cmd.CreateParameter();
            res.ParameterName = parameterName;
            res.Value = value;

            return res;
        }
    }
}
