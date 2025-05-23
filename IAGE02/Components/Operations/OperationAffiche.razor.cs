using IAGE02.Apps.Operations;
using IAGE02.Entities.Lots;
using IAGE02.Entities.Operation;
using IAGE02.Entities.Operations;
using IAGE02.Infrastructures.Storages.Lots;
using Microsoft.AspNetCore.Components;

namespace IAGE02.Components.Operations
{
    public partial class OperationAffiche
    {
        [Inject] private IOperationService operationService { get; set; }
        [Inject] private ILotStorage lotStorage { get; set; }

        private List<OperationInfo> operationsinfo = new();
        private List<Lot> lotInfo = new();

        private async Task afficherOperations()
        {
            List<Operation> operations = await operationService.GetOperations();
            changerVersTypInfoOperation(operations);
        }

        private void changerVersTypInfoOperation(List<Operation> operations)
        {
            operationsinfo = operations.Select(o => new OperationInfo
            {
                Id = o.Id,
                TypeTravaux = (TypeTravaux)o.TypeTravaux,
                TypeBudget = (TypeBudget)o.TypeBudget,
                Numero = o.Numero,
                ServicesContractant = o.ServicesContractant,
                Objet = o.Objet,
                ModeAttribuation = (TypeModeAttribuation)o.ModeAttribuation,
                NumeroVisa = o.NumeroVisa,
                DateVisa = o.DateVisa,

            }).ToList();
        }
        private async Task afficherLots()
        {
            
            lotInfo = await lotStorage.SelectLotsByOperationId(Guid.Parse("785583D7-96B8-4050-BB19-B2C813D96F60"));
        }
        protected override async Task OnInitializedAsync()
        {
            await afficherLots();
            await afficherOperations();
        }



    }
}