using IAGE02.Entities.Lots;
using IAGE02.Entities.Operation;
using IAGE02.Entities.Operations;

namespace IAGE02.Apps.Operations;

public class OperationInfo
{
    public Guid Id { get; set; }


    public TypeTravaux TypeTravaux { get; set; }


    public TypeBudget TypeBudget { get; set; }


    public string Numero { get; set; }


    public string ServicesContractant { get; set; }

    public string Objet { get; set; }


    public TypeModeAttribuation ModeAttribuation { get; set; }


    public string NumeroVisa { get; set; }


    public DateOnly DateVisa { get; set; }
    public OpertionState OpertionState { get; set; }
   // public List<Lot> Lots { get; set; } = new();
}