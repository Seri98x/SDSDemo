using SDSDemo.Interfaces;
using SDSDemo.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SDSDemo.Repositories
{
    public class RecyclableTypeRepository : IRecyclableTypeRepository
    {
        private readonly string _connectionString;

        public RecyclableTypeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<RecyclableType> GetAll()
        {
            var types = new List<RecyclableType>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SP_GetAllRecyclableTypes", connection) { CommandType = CommandType.StoredProcedure };
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        types.Add(new RecyclableType
                        {
                            Id = reader.GetInt32(0),
                            Type = reader.GetString(1),
                            Rate = reader.GetDecimal(2),
                            MinKg = reader.GetDecimal(3),
                            MaxKg = reader.GetDecimal(4)
                        });
                    }
                }
            }

            return types;
        }

        public void Add(RecyclableType recyclableType)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SP_AddRecyclableType", connection) { CommandType = CommandType.StoredProcedure };
                command.Parameters.AddWithValue("@Type", recyclableType.Type);
                command.Parameters.AddWithValue("@Rate", recyclableType.Rate);
                command.Parameters.AddWithValue("@MinKg", recyclableType.MinKg);
                command.Parameters.AddWithValue("@MaxKg", recyclableType.MaxKg);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public RecyclableType GetById(int id)
        {
            RecyclableType recyclableType = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SP_GetRecyclableTypeById", connection) { CommandType = CommandType.StoredProcedure };
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        recyclableType = new RecyclableType
                        {
                            Id = reader.GetInt32(0),
                            Type = reader.GetString(1),
                            Rate = reader.GetDecimal(2),
                            MinKg = reader.GetDecimal(3),
                            MaxKg = reader.GetDecimal(4)
                        };
                    }
                }
            }

            return recyclableType;
        }

        public void Update(RecyclableType recyclableType)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SP_UpdateRecyclableType", connection) { CommandType = CommandType.StoredProcedure };
                command.Parameters.AddWithValue("@Id", recyclableType.Id);
                command.Parameters.AddWithValue("@Type", recyclableType.Type);
                command.Parameters.AddWithValue("@Rate", recyclableType.Rate);
                command.Parameters.AddWithValue("@MinKg", recyclableType.MinKg);
                command.Parameters.AddWithValue("@MaxKg", recyclableType.MaxKg);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SP_DeleteRecyclableType", connection) { CommandType = CommandType.StoredProcedure };
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}