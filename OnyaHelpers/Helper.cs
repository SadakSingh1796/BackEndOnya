using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnyaServices
{
    public class QueryHelper
    {
        private IDbConnection DbConnection
        {
            get
            {
                if (OnyaService.DatabaseType == DatabaseType.PostgreSQL)
                {
                    return new NpgsqlConnection(OnyaService.ConnectionString);
                }
                return null;
            }
        }
        public int ExecuteNonQuery(string query, object parameters = null)
        {
            using (IDbConnection db = DbConnection)
            {
                return db.Execute(query, parameters);
            }
        }

        public async Task<int> ExecuteNonQueryAsync(string query, object parameters = null)
        {
            using (IDbConnection db = DbConnection)
            {
                return await db.ExecuteAsync(query, parameters);
            }
        }
        public int InsertAndGetId(string query, object parameters = null)
        {
            using (IDbConnection db = DbConnection)
            {
                return db.ExecuteScalar<int>(query, parameters);
            }
        }
        public async Task<int> InsertAndGetIdAsync(string query, object parameters = null)
        {
            using (IDbConnection db = DbConnection)
            {
                return await db.ExecuteScalarAsync<int>(query, parameters);
            }
        }
        public T InsertAndGet<T>(string query, object parameters = null) where T : new()
        {
            using (IDbConnection db = DbConnection)
            {
                return db.ExecuteScalar<T>(query, parameters) == null ?
                     new T() : db.ExecuteScalar<T>(query, parameters);
            }
        }
        public T Get<T>(string query, object parameters = null) where T : new()
        {
            try
            {
                using (IDbConnection db = DbConnection)
                {
                    return db.QueryFirstOrDefault<T>(query, parameters, commandType: CommandType.Text) == null ?
                         new T() : db.QueryFirstOrDefault<T>(query, parameters, commandType: CommandType.Text);
                }
            }
            catch (Exception ex)
            {
                return new T();
            }


        }

        public async Task<T> GetAsync<T>(string query, object parameters = null) where T : new()
        {
            using (IDbConnection db = DbConnection)
            {
                return await db.QueryFirstOrDefaultAsync<T>(query, parameters, commandType: CommandType.Text) == null ?
                     new T() : await db.QueryFirstOrDefaultAsync<T>(query, parameters, commandType: CommandType.Text);
            }
        }
        public List<T> GetList<T>(string sqlQuery, DynamicParameters parameters)
        {
            using (IDbConnection db = DbConnection)
            {
                return db.Query<T>(sqlQuery, parameters,
                commandType: CommandType.Text).ToList();
            }
        }
        public List<T> GetList<T>(string sqlQuery, object parameters = null)
        {
            using (IDbConnection db = DbConnection)
            {
                return db.Query<T>(sqlQuery, parameters,
                commandType: CommandType.Text).ToList();
            }
        }

        public List<T> GetListProc<T>(string sqlQuery, object parameters = null)
        {
            using (IDbConnection db = DbConnection)
            {
                return db.Query<T>(sqlQuery, parameters,
                commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public List<T> GetList<T>(string tableName)
        {
            using (IDbConnection db = DbConnection)
            {
                return db.Query<T>(tableName,
                commandType: CommandType.TableDirect).ToList();
            }
        }
        public void ExecuteProcedure(string procedureName, ref DynamicParameters parameters)
        {
            #region DapperHelper
            //var procedure = "[Sales by Year]";
            //var values = new { Beginning_Date = "2017.1.1", Ending_Date = "2017.12.31" };

            //var p = new DynamicParameters();
            //p.Add("@SourceFilename", fileName);
            //p.Add("@Updated", dbType: DbType.Int32, direction: ParameterDirection.Output);
            //p.Add("@Inserted", dbType: DbType.Int32, direction: ParameterDirection.Output);

            //insertCount = p.Get<int>("@Inserted"),
            // updateCount = p.Get<int>("@Updated") 
            #endregion

            using (IDbConnection db = DbConnection)
            {
                db.Execute(procedureName, parameters,
               commandType: CommandType.Text);
            }
        }
        public int GetInt(string query, object parameters = null)
        {
            using (IDbConnection db = DbConnection)
            {
                return db.ExecuteScalar<int>(query, parameters);
            }
        }
    }
}
