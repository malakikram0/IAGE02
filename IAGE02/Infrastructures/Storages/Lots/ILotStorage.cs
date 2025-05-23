using IAGE02.Entities.Lots;

namespace IAGE02.Infrastructures.Storages.Lots;

public interface ILotStorage
{
    Task<bool> InsertLot(Lot lot);
    Task<List<Lot>> SelectLotsByOperationId(Guid idOperation);
    Task<int> UpdateLot(Lot lot);
}