using IAGE02.Apps.Operations;
using IAGE02.Entities.Lots;
using IAGE02.Entities.Operation;
using IAGE02.Entities.Operations;
using IAGE02.Infrastructures.Storages.Lots;
using IAGE02.Models.Operations;
using Microsoft.AspNetCore.Components;

namespace IAGE02.Components.Operations
{
    public partial class OperationModifie
    {
        string result = string.Empty;
        [Inject] private IOperationService operationService { get; set; }
        [Inject] private ILotStorage lotStorage {get; set;}

        OperationModal operationModal = new OperationModal()
        {
            Id = Guid.Parse("D85EED99-B80F-44C6-85FA-0F4087D449F4"),
            Numero = "12345",
            ServicesContractant = "Service D",
            Objet = "Objet D",
            TypeTravaux = TypeTravaux.Travaux,
            TypeBudget = TypeBudget.Equipement,
            ModeAttribuation = TypeModeAttribuation.Consultation,
            NumeroVisa = "VISA-2025-001",
            DateVisa = new DateOnly(2025, 5, 20),
            OpertionState = OpertionState.Active
        };


        public async Task Modifier()
        {
            Operation operation = new Operation()
            {
                Id = operationModal.Id,
                TypeTravaux = operationModal.TypeTravaux,
                TypeBudget = operationModal.TypeBudget,
                Numero = operationModal.Numero,
                ServicesContractant = operationModal.ServicesContractant,
                Objet = operationModal.Objet,
                ModeAttribuation = operationModal.ModeAttribuation,
                NumeroVisa = operationModal.NumeroVisa,
                DateVisa = operationModal.DateVisa,
                OpertionState = operationModal.OpertionState
            };
            result = await operationService.ModifierOperation(operation);
        }
        public async Task ModifierLot()
        {
           
            Lot lot = new Lot
            {

                IdOperation = Guid.Parse("2F699C07-1776-4C11-B871-0632B2EE972A"),
                NumeroLot = "88",
                Designation = "Description 2",

            };

            int rs = await lotStorage.UpdateLot(lot);
            if (rs is 0)
            {
                result = "lot  modifier";
            }
            else
            {
                result = " lot err";
            }
        }
    }
}