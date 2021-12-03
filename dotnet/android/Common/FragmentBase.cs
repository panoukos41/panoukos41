namespace ;

public abstract class FragmentBase<TViewModel> : ReactiveUI.AndroidX.ReactiveFragment<TViewModel>
    where TViewModel : class
{
    protected FragmentBase()
    {
        Activated.Subscribe(ActivatedAction);
        Deactivated.Subscribe(DeactivatedAction);
    }

    private void ActivatedAction(Unit obj)
    {
        if (ViewModel is IActivatableViewModel vm)
        {
            vm.Activator.Activate();
        }
    }

    private void DeactivatedAction(Unit obj)
    {
        if (ViewModel is IActivatableViewModel vm)
        {
            vm.Activator.Deactivate();
        }
    }
}