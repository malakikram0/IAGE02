using IAGE02.Entities.Lots;

namespace IAGE02.Infrastructures.Storages.Lots;

public interface ILotStorage
{
    Task<bool> InsertLot(Lot lot);
    Task<int> UpdateLot(Lot lot);
}