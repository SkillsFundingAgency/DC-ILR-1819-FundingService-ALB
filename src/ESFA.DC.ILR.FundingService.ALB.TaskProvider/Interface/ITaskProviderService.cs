using ESFA.DC.ILR.Model;

namespace ESFA.DC.ILR.FundingService.ALB.TaskProvider.Interface
{
    public interface ITaskProviderService
    {
       void ExecuteTasks(Message message);
    }
}
