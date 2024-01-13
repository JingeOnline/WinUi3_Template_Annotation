namespace WinUI3_SelfLearning.Contracts.Services;

public interface IActivationService
{
    Task ActivateAsync(object activationArgs);
}
