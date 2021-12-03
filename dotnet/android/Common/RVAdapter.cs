using Android.Views;
using AndroidX.RecyclerView.Widget;
using DynamicData;

namespace ;

public class RVAdapter<TViewModel> : ReactiveUI.AndroidX.ReactiveRecyclerViewAdapter<TViewModel>
    where TViewModel : class, IReactiveObject
{
    private readonly int resource;

    public Action<View, TViewModel>? OnBind { get; init; }

    public Action<int>? Selected { get; init; }

    public Action<TViewModel?>? SelectedWithViewModel { get; init; }

    public Action<int>? LongClicked { get; init; }

    public Action<TViewModel?>? LongClickedWithViewModel { get; init; }

    public RVAdapter(IObservable<IChangeSet<TViewModel>> backingList, int resource) : base(backingList)
    {
        this.resource = resource;
    }

    public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
    {
        base.OnBindViewHolder(holder, position);
        var vh = (ViewHolder)holder;

        OnBind?.Invoke(vh.View, vh.ViewModel!);
    }

    public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
    {
        var view = LayoutInflater.From(parent.Context)!.Inflate(resource, parent, false);
        var vh = new ViewHolder(view!);

        if (Selected is not null)
        {
            vh.Selected.Subscribe(Selected);
        }
        if (SelectedWithViewModel is not null)
        {
            vh.SelectedWithViewModel.Subscribe(SelectedWithViewModel);
        }
        if (LongClicked is not null)
        {
            vh.LongClicked.Subscribe(LongClicked);
        }
        if (LongClickedWithViewModel is not null)
        {
            vh.LongClickedWithViewModel.Subscribe(LongClickedWithViewModel);
        }
        return vh;
    }

    public class ViewHolder : ReactiveUI.AndroidX.ReactiveRecyclerViewViewHolder<TViewModel>
    {
        public ViewHolder(View view) : base(view)
        {
        }
    }
}
