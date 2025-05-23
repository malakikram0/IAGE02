using IAGE02.Entities.Lots;
using IAGE02.Entities.Operations;
using IAGE02.Models.Operations;

namespace IAGE02.Apps.Operations;

public interface IOperationService
{
    Task<string> CreerOperation(Operation operation, List<Lot> lots);
    Task<string> ModifierOperation(Operation operation);
    Task<List<Operation>> GetOperations();
}
