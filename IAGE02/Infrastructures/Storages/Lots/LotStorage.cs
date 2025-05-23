using System.Data;
using System.Data.SqlClient;
using IAGE02.Components.Pages;
using IAGE02.Entities.Lots;
using IAGE02.Entities.Operation;

namespace IAGE02.Infrastructures.Storages.Lots;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;


public class LotStorage(IConfiguration configuration) : ILotStorage

{
    private string connectionString = configuration.GetConnectionString("IAGE02");
   

    private const string insertLotCommand = "insertLot";
    private const string updateLotCommand = "updateLot";
    private const string selectLotCommand = @"
    SELECT * 
    FROM dbo.GettAllLotNonSupprimer(@aId_Operation);";
    public async Task<bool> InsertLot(Lot lot)
    {
        await using SqlConnection con = new SqlConnection(connectionString);
        await using SqlCommand cmd = new SqlCommand(insertLotCommand, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@aOperationId", lot.IdOperation);
        cmd.Parameters.AddWithValue("@aNumeroLot", lot.NumeroLot);
        cmd.Parameters.AddWithValue("@aDesignation", lot.Designation);

        cmd.Parameters.Add("@aReturn", SqlDbType.Int).Direction = ParameterDirection.Output;
        con.Open();

        await cmd.ExecuteNonQueryAsync();

        int result = (int)cmd.Parameters["@aReturn"].Value;
        return result == 0;


    }

    public async  Task<int> UpdateLot(Lot lot)
    {

        await using SqlConnection con = new SqlConnection(connectionString);
        await using SqlCommand cmd = new SqlCommand(updateLotCommand, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@aOperationId", lot.IdOperation);
        cmd.Parameters.AddWithValue("@aNumeroLot", lot.NumeroLot);
        cmd.Parameters.AddWithValue("@aDesignation", lot.Designation);

        cmd.Parameters.Add("@aReturn", SqlDbType.Int).Direction = ParameterDirection.Output;
        con.Open();

        await cmd.ExecuteNonQueryAsync();

        return (int)cmd.Parameters["@aReturn"].Value;
    }
    private static Lot mapLot(SqlDataReader reader)
    {
        return new Lot
        {
           IdOperation= (Guid)reader["OperationId"],
            NumeroLot = (string )reader["Numero"],
            Designation = (string)reader["Designation"]

        };
    }
    public async Task<List<Lot>> SelectLotsByOperationId(Guid idOperation)
    {
        var lots = new List<Lot>();
        await using var con = new SqlConnection(connectionString);

        using var cmd = new SqlCommand(selectLotCommand, con);
        cmd.Parameters.AddWithValue("@aId_Operation", idOperation);
        
        await con.OpenAsync();

        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            lots.Add(mapLot(reader)); 
        }
        return lots;
    }
}