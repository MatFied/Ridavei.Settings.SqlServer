using System;
using System.Collections.Generic;
using System.Data;

using Ridavei.Settings.DbAbstractions.Settings;

namespace Ridavei.Settings.SqlServer.Settings
{
    /// <summary>
    /// SQL Server settings class that uses database table to store settings.
    /// </summary>
    internal sealed class SqlServerSettings : ADbSettings
    {
        const string DictionaryNamePar = "@DictionaryNamePar";
        const string KeyNamePar = "@KeyNamePar";
        const string ValueNamePar = "@ValuePar";

        private readonly string _addOrUpdateValueQuery = "[dbo].[p_ridaveiSettings_AddOrUpdate]";
        private readonly string _getAllValuesQuery = $"SELECT [KeyName], [Value] FROM f_ridaveiSettings_GetAllValues ({DictionaryNamePar})";
        private readonly string _tryGetValueQuery = $"SELECT dbo.f_ridaveiSettings_GetValue ({DictionaryNamePar}, {KeyNamePar})";

        /// <summary>
        /// The default constructor for <see cref="SqlServerSettings"/> class.
        /// </summary>
        /// <param name="dictionaryName">Name of the dictionary</param>
        public SqlServerSettings(string dictionaryName) : base(dictionaryName) { }

        /// <inheritdoc/>
        protected override int AddOrUpdateValueInDb(IDbConnection connection, string key, string value)
        {
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = _addOrUpdateValueQuery;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(CreateParam(cmd, DictionaryNamePar, DictionaryName));
                cmd.Parameters.Add(CreateParam(cmd, KeyNamePar, key));
                cmd.Parameters.Add(CreateParam(cmd, ValueNamePar, value));

                return cmd.ExecuteNonQuery();
            }
        }

        /// <inheritdoc/>
        protected override IReadOnlyDictionary<string, string> GetAllValuesFromDb(IDbConnection connection)
        {
            var res = new Dictionary<string, string>();
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = _getAllValuesQuery;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(CreateParam(cmd, DictionaryNamePar, DictionaryName));

                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        res.Add(reader.GetString(0), reader.IsDBNull(1) ? string.Empty : reader.GetString(1));
            }

            return res;
        }

        /// <inheritdoc/>
        protected override bool TryGetValueFromDb(IDbConnection connection, string key, out string value)
        {
            value = string.Empty;
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = _tryGetValueQuery;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(CreateParam(cmd, DictionaryNamePar, DictionaryName));
                cmd.Parameters.Add(CreateParam(cmd, KeyNamePar, key));

                var obj = cmd.ExecuteScalar();
                if (obj == null || obj == DBNull.Value)
                    return false;

                value = obj.ToString();
                return true;
            }
        }

        /// <summary>
        /// Creates parameter for the command.
        /// </summary>
        /// <param name="command">Database command</param>
        /// <param name="paramName">Name of the parameter</param>
        /// <param name="paramValue">Value of the parameter</param>
        /// <returns>Parameter</returns>
        private IDbDataParameter CreateParam(IDbCommand command, string paramName, string paramValue)
        {
            var res = command.CreateParameter();
            res.ParameterName = paramName;
            res.Value = paramValue;

            return res;
        }
    }
}
