using System.Buffers;
using System.Data;
using System.Data.SqlClient;
using IAGE02.Apps.Operations;
using IAGE02.Components.Pages;
using IAGE02.Entities.Lots;
using IAGE02.Entities.Operation;
using IAGE02.Entities.Operations;
using IAGE02.Models.Operations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IAGE02.Infrastructures.Storages.Operations;

public class OperationStorage(IConfiguration configuration) : IOperationStorage
{
    private string connectionString = configuration.GetConnectionString("IAGE02");
    private const string insertOperationCommand = "insertNewOperation";
    private const string updateOperationCommand = "updateExistingOperation";
    private const string selectOperation = @"select * from OPERATIONS";
    public async Task<int> InsertOperation(Operation operation)
    {
        await using SqlConnection con = new SqlConnection(connectionString);


        SqlCommand cmd = new SqlCommand(insertOperationCommand, con);
        cmd.CommandType = CommandType.StoredProcedure;


        cmd.Parameters.AddWithValue("@aId_Operation", operation.Id);
        cmd.Parameters.AddWithValue("@aNumero", operation.Numero);
        cmd.Parameters.AddWithValue("@aService_contractant", operation.ServicesContractant);
        cmd.Parameters.AddWithValue("@aTypeBudget", operation.TypeBudget);
        cmd.Parameters.AddWithValue("@aModeAttribuation", operation.ModeAttribuation);
        cmd.Parameters.AddWithValue("@aObjet", operation.Objet);
        cmd.Parameters.AddWithValue("@aTypeTravaux", operation.TypeTravaux);
        cmd.Parameters.AddWithValue("@aNumeroVisa", operation.NumeroVisa);
        cmd.Parameters.AddWithValue("@aDateVisa", operation.DateVisa.ToDateTime(TimeOnly.MinValue));
        cmd.Parameters.AddWithValue("@aState", operation.OpertionState);


        cmd.Parameters.Add("@aReturn", SqlDbType.Int).Direction = ParameterDirection.Output;
        con.Open();

        await cmd.ExecuteNonQueryAsync();

        return (int)cmd.Parameters["@aReturn"].Value;

       
    }

    public async Task<int> UpdateOperation(Operation operation)
    {
        await using SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(updateOperationCommand, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@aId_Operation", operation.Id);
        cmd.Parameters.AddWithValue("@aNumero", operation.Numero);
        cmd.Parameters.AddWithValue("@aService_contractant", operation.ServicesContractant);
        cmd.Parameters.AddWithValue("@aTypeBudget", operation.TypeBudget);
        cmd.Parameters.AddWithValue("@aModeAttribuation", operation.ModeAttribuation);
        cmd.Parameters.AddWithValue("@aObjet", operation.Objet);
        cmd.Parameters.AddWithValue("@aTypeTravaux", operation.TypeTravaux);
        cmd.Parameters.AddWithValue("@aNumeroVisa", operation.NumeroVisa);
        cmd.Parameters.AddWithValue("@aDateVisa", operation.DateVisa.ToDateTime(TimeOnly.MinValue));

        cmd.Parameters.Add("@aReturn", SqlDbType.Int).Direction = ParameterDirection.Output;
        await con.OpenAsync();

        await cmd.ExecuteNonQueryAsync();

        return (int)cmd.Parameters["@aReturn"].Value;

    }
    private static Operation mapOperation(SqlDataReader reader)
    {
        return new Operation
        {
            Id = (Guid)reader["Id"],
            TypeTravaux = (TypeTravaux)reader["TypeTravaux"],
            TypeBudget = (TypeBudget)reader["TypeBudget"],
            Numero = (string)reader["Numero"],
            ServicesContractant = (string)reader["Service_Contractant"],
            Objet = (string)reader["Objet"],
            ModeAttribuation = (TypeModeAttribuation)reader["ModeAttribuation"],
            NumeroVisa = (string)reader["NumeroVisa"],
            DateVisa = DateOnly.FromDateTime((DateTime)reader["DateVisa"]),
            OpertionState = (OpertionState)reader["State"]


        };
    }
    public async Task<List<Operation>> SelectOperation()
    {
        var operation = new List<Operation>();
        await using var con = new SqlConnection(connectionString);

        var cmd = new SqlCommand(selectOperation, con);
        con.Open();
        var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            operation.Add(mapOperation(reader));
        }

        return operation;

    }
}