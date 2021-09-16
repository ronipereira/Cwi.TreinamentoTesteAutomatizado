using Dapper;
using Npgsql;
using System;
using System.Data;
using System.Threading.Tasks;

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

    }
}
