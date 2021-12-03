using Android.App;
using Android.Content.PM;
using Android.OS;
using AndroidX.AppCompat.App;
using P41.Navigation;

namespace ;

[Activity(Theme = "@style/Theme.App", ConfigurationChanges = ConfigChanges.Orientation, MainLauncher = true)]
//[GenerateBindings("shell.xml")]
public partial class Shell : AppCompatActivity
{
    private CompositeDisposable disposables = null!;

    private NavigationHost Host { get; set; } = null!;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        SetContentView(R.Layout.shell);

        Host = (NavigationHost)App.Resolve<INavigationHost>();
        //ThemeService.FirstRun(); todo: Move theme service here!

        disposables = new()
        {
            Host.WhenRootPopped.Subscribe(_ => Finish()),
            Host.ShouldPopRoot.RegisterHandler(static c => c.SetOutput(true))
        };

        Host.SetFragmentManager(SupportFragmentManager)
            .SetFragmentContainerId(R.Id.Container)
            .Navigate("home");
    }

    public override void OnBackPressed()
    {
        Host.GoBack();
    }

    protected override void OnDestroy()
    {
        disposables.Dispose();
        base.OnDestroy();
    }
}
