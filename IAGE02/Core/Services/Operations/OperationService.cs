using System.Transactions;
using IAGE02.Apps.Operations;
using IAGE02.Entities.Lots;
using IAGE02.Entities.Operations;
using IAGE02.Infrastructures.Storages.Lots;
using IAGE02.Infrastructures.Storages.Operations;
using IAGE02.Models.Operations;

namespace IAGE02.Core.Services.Operations;

public class OperationService(IOperationStorage _operationStorage, ILotStorage _lotStorage) : IOperationService
{
    private readonly IOperationStorage operationStorage = _operationStorage;
    private readonly ILotStorage lotStorage = _lotStorage;


    public async Task<string> CreerOperation(Operation operation, List<Lot> lots)
    {
        /*
        using var scop = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        */
        try
        {
            var id = Guid.NewGuid();
            operation.Id = id;
            var result=await operationStorage.InsertOperation(operation);
            if (result is 1002)
                return "Operation déjà existante";
            if(result is 5000)
                return "Operation non créée";
         
            foreach (var lot in lots)
            {
                lot.IdOperation = id;
                await lotStorage.InsertLot(lot);
            }
            return "opération créée";
          
        }
        catch (Exception e)
        {
          //  scop.Dispose();
          Console.WriteLine(e);
          throw;
        }

  
    }

    
    
       public async Task<List<Operation>> GetOperations()
    {
        try
        {
            var result = await operationStorage.SelectOperation();
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    

    public async Task<string> ModifierOperation(Operation operation)
    {
        try
        {
            int resultModification = await operationStorage.UpdateOperation(operation);
            if (resultModification is 1004)
                return "Operation pas exist";
            if (resultModification is 5000)
                return "Operation non créée";
            return "opération Modifier";
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            throw;

        }
        
    }
}