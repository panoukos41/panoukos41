global using ReactiveUI;
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Linq.Expressions;
global using System.Reactive;
global using System.Reactive.Disposables;
global using System.Reactive.Linq;
global using System.Reactive.Threading.Tasks;
global using System.Threading;

using Android.App;
using Android.Runtime;

namespace ;

[Application]
public class AndroidApp : Application
{
    public AndroidApp(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
    {
    }

    public override void OnCreate()
    {
        base.OnCreate();
        App.Initialize(new AndroidServices());
        Xamarin.Essentials.Platform.Init(this);
    }
}
