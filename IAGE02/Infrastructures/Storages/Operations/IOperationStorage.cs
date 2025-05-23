using System.Threading.Tasks;
using IAGE02.Apps.Operations;
using IAGE02.Entities.Lots;
using IAGE02.Entities.Operations;
using IAGE02.Models.Operations;

namespace IAGE02.Infrastructures.Storages.Operations;

public interface IOperationStorage
{
    Task<int> InsertOperation(Operation operation);
    Task<List<Operation>> SelectOperation();
    Task<int> UpdateOperation(Operation operation);
    
}
