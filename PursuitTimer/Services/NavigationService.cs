
namespace PursuitTimer.Services
{
    public class NavigationService : INavigationService
    {
        public Task GoBackAsync()
        {
            return Shell.Current.GoToAsync("..");
        }

        public Task InitializeAsync()
        {
            return NavgigateToAsync("//Timing/Timer");
        }

        public Task NavgigateToAsync(string route, IDictionary<string, object> routeParameters = null)
        {
            return 
                routeParameters is not null 
                    ? Shell.Current.GoToAsync(route, routeParameters)
                    : Shell.Current.GoToAsync(route);
        }

        public Task PopAsync()
        {
            return Shell.Current.Navigation.PopAsync();
        }
    }
}
