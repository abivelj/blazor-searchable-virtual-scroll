using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace BlazorSearchableVirtualScroll
{
    public class VirtualSearchBase<TItem> : ComponentBase
    {
        public ElementReference AutoCompleteRef { get; set; }
        public bool IsLoading = true;
        public bool IsOpen = false;
        public string SearchString { get; set; }

        private System.Timers.Timer _timer;
        public List<VirtualItem<TItem>> FilteredItems = new List<VirtualItem<TItem>>();

        private void HandleFilter(Object source, System.Timers.ElapsedEventArgs e)
        {
            DoFilter();
        }

        private void DoFilter()
        {
            InvokeAsync(() =>
            {
                var culture = CultureInfo.CurrentCulture;
                FilteredItems = !string.IsNullOrEmpty(SearchString) ? Items.Where(x => culture.CompareInfo.IndexOf(x.SearchableString, SearchString, CompareOptions.IgnoreCase) > -1).ToList() : Items;
                StateHasChanged();
            });
        }

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> Attributes { get; set; }

        [Parameter]
        public bool OverrideOpen { get; set; }

        [Parameter]
        public string Placeholder { get; set; }

        [Parameter]
        public string DefaultSearch { get; set; }

        [Parameter]
        public List<VirtualItem<TItem>> Items { get; set; }

        [Parameter]
        public int ItemHeight { get; set; }

        [Parameter]
        public int ItemsToShow { get; set; }

        [Parameter]
        public int ItemsToPreRender { get; set; }

        [Parameter]
        public EventCallback<VirtualItem<TItem>> OnClick { get; set; }

        [Inject]
        public IJSRuntime Jsr { get; set; }

        public void HandleKeyUp(KeyboardEventArgs keys)
        {
            if (keys.Key == "Escape")
            {
                IsOpen = false;
                OnClick.InvokeAsync(null);
            }
            _timer.Stop();
            _timer.Start();
        }

        public void OnInnerClick(VirtualItem<TItem> item)
        {
            SearchString = item.SearchableString;
            OnClick.InvokeAsync(item);
            IsOpen = false;
            StateHasChanged();
        }

        [JSInvokable]
        public void OnCloseVirtualScroll()
        {
            if (!IsOpen) return;
            IsOpen = false;
            if (FilteredItems.Count == 1 && !string.IsNullOrEmpty(SearchString))
            {
                OnClick.InvokeAsync(FilteredItems[0]);
            }
            else
            {
                OnClick.InvokeAsync(null);
            }
            StateHasChanged();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                BlazorSearchableVirtualScrollJs.SetupOutsideClick(Jsr, AutoCompleteRef, DotNetObjectReference.Create(this));
            }
            base.OnAfterRender(firstRender);
        }

        protected override void OnParametersSet()
        {
            IsLoading = false;
            if (!string.IsNullOrEmpty(DefaultSearch))
                SearchString = DefaultSearch;

            DoFilter();
            base.OnParametersSet();
        }

        protected override void OnInitialized()
        {
            _timer = new System.Timers.Timer(100);
            _timer.Elapsed += HandleFilter;
            _timer.AutoReset = false;

            base.OnInitialized();
        }
    }
}
