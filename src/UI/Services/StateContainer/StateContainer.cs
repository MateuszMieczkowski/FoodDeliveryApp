namespace UI.Services.StateContainer;

public class StateContainer
{
    public event Action? OnChange;

    public void NotifyStateChanged() => OnChange?.Invoke();
}
