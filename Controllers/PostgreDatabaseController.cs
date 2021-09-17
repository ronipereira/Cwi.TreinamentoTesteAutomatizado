using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Cwi.TreinamentoTesteAutomatizado.Controllers
{
    public class PostgreDatabaseController
    {
        private readonly NpgsqlConnection Connection;

        public PostgreDatabaseController(NpgsqlConnection connection)
        {
            try 
            {
                Connection = connection;

                if(Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Não foi possível abrir a conexão com a base de dados", ex);
            }
        }

        public async Task ClearDatabase(string schema = "public")
        {
            var query = $@"DO
                          $$
                          DECLARE
                              l_stmt VARCHAR;
                              databaseschema VARCHAR:= '{schema}';
                          BEGIN
                              SELECT 'truncate ' || string_agg(format('%I.%I', schemaname, tablename), ',')
                              INTO l_stmt
                              FROM pg_tables
                              WHERE schemaname = databaseschema;
                             
                              EXECUTE l_stmt || ' RESTART IDENTITY';
                          END;
                          $$";
            await Connection.ExecuteAsync(query);
        }

        public async Task<IEnumerable<object>> SelectFrom(string tableName, Table table)
        {
            var selectColumns = string.Join(",", GetColumnsForSelect(table));
            var filterConditions = string.Join(" OR ", GetFilterConditions(table));

            var query = $"SELECT {selectColumns} from {tableName} WHERE {filterConditions}";

            return await Connection.QueryAsync(query);
        }

        public async Task<IEnumerable<object>> InsertDatabase(string tableName, Table table)
        {
            var insertColumns = string.Join(", ", GetColumnsForInsert(table));
            var values = string.Join(", ", GetFilterValues(table));

            var query = $"INSERT INTO {tableName}({insertColumns}) VALUES {values}";

            return await Connection.QueryAsync(query);
        }

        private string[] GetColumnsForSelect(Table table)
        {
            return table.Header.Select(x => $"{x} AS \"{x}\"").ToArray();
        }

        private string[] GetColumnsForInsert(Table table)
        {
            return table.Header.ToArray();
        }

        private string[] GetFilterConditions(Table table)
        {
            List<string> filters = new List<string>();

            for(int row = 0; row < table.Rows.Count; row++)
            {
                var rowConditions = new List<string>();

                for(int header = 0; header < table.Header.Count; header++)
                {
                    string column = table.Header.ElementAt(header);
                    string value = table.Rows[row][header];

                    rowConditions.Add($"{column} = {value}");
                }

                filters.Add($"({string.Join(" AND ", rowConditions)})");
            }

            return filters.ToArray();
        }

        private string[] GetFilterValues(Table table)
        {
            List<string> values = new List<string>();

            for (int row = 0; row < table.Rows.Count; row++)
            {
                var rowValues = new List<string>();

                for (int header = 0; header < table.Header.Count; header++)
                {
                    string value = table.Rows[row][header];

                    rowValues.Add(value);
                }

                values.Add($"({string.Join(", ", rowValues)})");
            }

            return values.ToArray();
        }
    }
}
