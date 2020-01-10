using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorSearchableVirtualScroll
{
    public class VirtualScrollBase<TItem> : ComponentBase
    {
        public VirtualScrollBase()
        {
            Items = new List<VirtualItem<TItem>>();
            SearchString = string.Empty;
        }

        public ElementReference ElemRef = new ElementReference();
        public int TotalHeight => IsLoading ? 0 : Items.Count * ItemHeight;
        public int TotalVisibleHeight => Math.Min(Items.Count == 0 ? 1 : Items.Count, ItemsToShow) * ItemHeight;
        public int ItemsToSkip = 0;
        public int ItemsToTake => ItemsToShow + ItemsToPreRender;
        public int ScrollTop = 0;

        [Inject]
        public IJSRuntime Jsr { get; set; }

        [Parameter]
        public bool IsOpen { get; set; }
        [Parameter]
        public string SearchString { get; set; }

        [Parameter]
        public bool IsLoading { get; set; }

        [Parameter]
        public int ItemHeight { get; set; }

        [Parameter]
        public List<VirtualItem<TItem>> Items { get; set; }

        [Parameter]
        public int ItemsToShow { get; set; }

        [Parameter]
        public int ItemsToPreRender { get; set; }

        [Parameter]
        public EventCallback<VirtualItem<TItem>> OnClick { get; set; }

        public void HandleClick(VirtualItem<TItem> itm)
        {
            OnClick.InvokeAsync(itm);
        }

        public async Task OnScrollEvent()
        {
            ScrollTop = await BlazorSearchableVirtualScrollJs.GetScrollTop(Jsr, ElemRef);
            CalculateScrollView();
        }

        private void CalculateScrollView()
        {
            ItemsToSkip = (ScrollTop - ItemsToPreRender) / ItemHeight;
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                CalculateScrollView();
            }
            base.OnAfterRender(firstRender);
        }
    }
}
