using Microsoft.JSInterop;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorSearchableVirtualScroll
{
    public class BlazorSearchableVirtualScrollJs
    {
        public static ValueTask<int> GetScrollTop(IJSRuntime jsRuntime, ElementReference element)
        {
            return jsRuntime.InvokeAsync<int>("blazorSearchableVirtualScroll.getScrollTop", element);
        }

        public static void SetupOutsideClick<TItem>(IJSRuntime jsRuntime, ElementReference element, DotNetObjectReference<VirtualSearchBase<TItem>> instance)
        {
            jsRuntime.InvokeVoidAsync("blazorSearchableVirtualScroll.setupOnOutsideClick", element, instance);
        }
    }
}
