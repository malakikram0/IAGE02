using IAGE02.Apps.Operations;
using IAGE02.Entities.Lots;
using IAGE02.Entities.Operation;
using IAGE02.Entities.Operations;
using IAGE02.Models.Lots;
using IAGE02.Models.Operations;
using Microsoft.AspNetCore.Components;

namespace IAGE02.Components.Operations
{
    public partial class OperationPage
    {
        [Inject] private IOperationService operationService { get; set; }
        private OperationModal operationModal = new OperationModal();
        private List<Lot> Lots = new();

        private LotModal lotModal = new LotModal();
        private int counter = 1;
        String result = String.Empty;
        String resultcon = String.Empty;

        private async Task creerOperation()
        {
            Console.WriteLine("aaa");
            var operation = new Operation
            {
                TypeTravaux = (TypeTravaux)operationModal.TypeTravaux,
                TypeBudget = (TypeBudget)operationModal.TypeBudget,
                Numero = operationModal.Numero,
                ServicesContractant = operationModal.ServicesContractant,
                Objet = operationModal.Objet,
                ModeAttribuation = (TypeModeAttribuation)operationModal.ModeAttribuation,
                NumeroVisa = operationModal.NumeroVisa,
                DateVisa = operationModal.DateVisa,
                OpertionState = OpertionState.Active
            };


            result = await operationService.CreerOperation(operation, Lots);

            operationModal = new OperationModal();

            StateHasChanged();
        }


        private void creerLot()
        {
            if (!string.IsNullOrWhiteSpace(lotModal.Designation))
            {
                var lotinfo = new Lot
                {
                    NumeroLot = lotModal.Number,
                    Designation = lotModal.Designation
                };
                Lots.Add(lotinfo);
                lotModal = new LotModal();

            }
        }

    }
}