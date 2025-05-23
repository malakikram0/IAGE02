using System.ComponentModel.DataAnnotations;
using IAGE02.Entities.Operation;
using IAGE02.Entities.Operations;

namespace IAGE02.Models.Operations
{
    public class OperationModal
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Type de travaux est requis")]
        public TypeTravaux TypeTravaux { get; set; }

        [Required(ErrorMessage = "Type de budget est requis")]
        public TypeBudget TypeBudget { get; set; }

        [Required(ErrorMessage = "Numéro de l'opération requis")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "Service contractant requis")]
        public string ServicesContractant { get; set; }

        [Required(ErrorMessage = "Objet requis")]
        public string Objet { get; set; }

        [Required(ErrorMessage = "Nature (mode d'attribution) requise")]
        public TypeModeAttribuation ModeAttribuation { get; set; }

        [Required(ErrorMessage = "Numéro de visa requis")]
        public string NumeroVisa { get; set; }

        [Required(ErrorMessage = "Date du visa requise")]

        [DataType(DataType.Date)]
        public DateOnly DateVisa { get; set; }
        public OpertionState OpertionState { get; set; }
        //public List<LotModal> Lots { get; set; } = new();
    }

}
