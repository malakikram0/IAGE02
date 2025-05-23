using IAGE02.Entities.Lots;

namespace IAGE02.Apps.Lots
{
    public interface ILotService
    {
        Task<string> updateLot(Lot lot);
    }
}
