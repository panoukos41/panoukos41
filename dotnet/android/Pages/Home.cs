using Android.OS;
using Android.Views;
using P41.Navigation;
using P41.ViewBindingsGenerator;

namespace ;

[GenerateBindings("home.xml")]
public partial class Home : ReactiveUI.AndroidX.ReactiveFragment<object>, IRoute
{
    private readonly INavigationHost host;

    public static string Route { get; } = "home";

    public Home(INavigationHost host)
    {
        this.host = host;
    }

    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
    {
        return inflater.Inflate(R.Layout.home, container, false)!;
    }

    public override void OnViewCreated(View view, Bundle savedInstanceState)
    {
        Text.Text = $"Hello from route: {Route}";
        Oasa.Click += (s, e) => host.Navigate("oasa");

        //base.OnViewCreated(view, savedInstanceState);
    }
}
