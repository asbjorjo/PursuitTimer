namespace PursuitTimer.Services
{
    public interface INavigationService
    {
        Task InitializeAsync();
        Task GoBackAsync();
        Task NavgigateToAsync(string route, IDictionary<string, object> routeParameters = null);
        Task PopAsync();
    }
}
