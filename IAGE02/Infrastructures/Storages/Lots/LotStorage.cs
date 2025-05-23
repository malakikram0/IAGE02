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
}