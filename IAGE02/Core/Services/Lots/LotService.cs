using IAGE02.Apps.Lots;
using IAGE02.Entities.Lots;
using IAGE02.Infrastructures.Storages.Lots;
using Microsoft.AspNetCore.Components;

namespace IAGE02.Core.Services.Lots
{
    public class LotService : ILotService
    {
        [Inject] private ILotStorage lotStorage { get; set; }
        public async Task<string> updateLot(Lot lot)
        {
           int result= await lotStorage.UpdateLot(lot);
            if(result is 5000)
           
                return "modification error";
            if (result is 1004)
                return "pas lot";
            return "modification correct";
            
        }
    }
}
