using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorSearchableVirtualScroll
{
    public class VirtualSearchBase<TItem> : ComponentBase
    {
        public bool IsOpen = false;
        public bool IsLoading = true;
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

        [Parameter]
        public bool OverrideOpen { get; set; }

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

        public void HandleKeyUp()
        {
            _timer.Stop();
            _timer.Start();
        }

        public async Task OnClose()
        {
            await Task.Delay(100);
            IsOpen = false;
            StateHasChanged();
        }

        protected override void OnParametersSet()
        {
            IsLoading = false;
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
