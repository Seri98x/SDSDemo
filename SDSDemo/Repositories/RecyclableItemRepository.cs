using SDSDemo.Interfaces;
using SDSDemo.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SDSDemo.Repositories
{
    public class RecyclableItemRepository : IRecyclableItemRepository
    {
        private readonly string _connectionString;

        public RecyclableItemRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<RecyclableItem> GetAll()
        {
            var items = new List<RecyclableItem>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SP_GetAllRecyclableItems", connection) { CommandType = CommandType.StoredProcedure };
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var recyclableType = new RecyclableType
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("RecyclableTypeId")),
                            Type = reader.IsDBNull(reader.GetOrdinal("RecyclableTypeType"))
                                ? "Unknown"
                                : reader.GetString(reader.GetOrdinal("RecyclableTypeType")),
                            Rate = reader.GetDecimal(reader.GetOrdinal("RecyclableTypeRate")),
                            MinKg = reader.GetDecimal(reader.GetOrdinal("RecyclableTypeMinKg")),
                            MaxKg = reader.GetDecimal(reader.GetOrdinal("RecyclableTypeMaxKg"))
                        };

                        items.Add(new RecyclableItem
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            RecyclableTypeId = reader.GetInt32(reader.GetOrdinal("RecyclableTypeId")),
                            Weight = reader.GetDecimal(reader.GetOrdinal("Weight")),
                            ComputedRate = reader.GetDecimal(reader.GetOrdinal("ComputedRate")),
                            ItemDescription = reader.GetString(reader.GetOrdinal("ItemDescription")),
                            RecyclableType = recyclableType // Assign RecyclableType object
                        });
                    }
                }
            }

            return items;
        }

        public void Add(RecyclableItem recyclableItem)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SP_AddRecyclableItem", connection) { CommandType = CommandType.StoredProcedure };
                command.Parameters.AddWithValue("@RecyclableTypeId", recyclableItem.RecyclableTypeId);
                command.Parameters.AddWithValue("@Weight", recyclableItem.Weight);
                command.Parameters.AddWithValue("@ItemDescription", recyclableItem.ItemDescription);
                command.Parameters.AddWithValue("@ComputedRate", recyclableItem.ComputedRate);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public RecyclableItem GetById(int id)
        {
            RecyclableItem recyclableItem = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SP_GetRecyclableItemById", connection) { CommandType = CommandType.StoredProcedure };
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var recyclableType = new RecyclableType
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("RecyclableTypeId")),
                            Type = reader.IsDBNull(reader.GetOrdinal("RecyclableTypeType"))
                                ? "Unknown"
                                : reader.GetString(reader.GetOrdinal("RecyclableTypeType")),
                            Rate = reader.GetDecimal(reader.GetOrdinal("RecyclableTypeRate")),
                            MinKg = reader.GetDecimal(reader.GetOrdinal("RecyclableTypeMinKg")),
                            MaxKg = reader.GetDecimal(reader.GetOrdinal("RecyclableTypeMaxKg"))
                        };

                        recyclableItem = new RecyclableItem
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            RecyclableTypeId = reader.GetInt32(reader.GetOrdinal("RecyclableTypeId")),
                            Weight = reader.GetDecimal(reader.GetOrdinal("Weight")),
                            ComputedRate = reader.GetDecimal(reader.GetOrdinal("ComputedRate")),
                            ItemDescription = reader.GetString(reader.GetOrdinal("ItemDescription")),
                            RecyclableType = recyclableType // Assign RecyclableType object
                        };
                    }
                }
            }

            return recyclableItem;
        }

        public void Update(RecyclableItem recyclableItem)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SP_UpdateRecyclableItem", connection) { CommandType = CommandType.StoredProcedure };
                command.Parameters.AddWithValue("@Id", recyclableItem.Id);
                command.Parameters.AddWithValue("@RecyclableTypeId", recyclableItem.RecyclableTypeId);
                command.Parameters.AddWithValue("@Weight", recyclableItem.Weight);
                command.Parameters.AddWithValue("@ItemDescription", recyclableItem.ItemDescription);
                command.Parameters.AddWithValue("@ComputedRate", recyclableItem.ComputedRate);


                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SP_DeleteRecyclableItem", connection) { CommandType = CommandType.StoredProcedure };
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<RecyclableType> GetRecyclableTypes()
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
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Type = reader.GetString(reader.GetOrdinal("Type")),
                            Rate = reader.GetDecimal(reader.GetOrdinal("Rate")),
                            MinKg = reader.GetDecimal(reader.GetOrdinal("MinKg")),
                            MaxKg = reader.GetDecimal(reader.GetOrdinal("MaxKg"))
                        });
                    }
                }
            }

            return types;
        }
    }
}
