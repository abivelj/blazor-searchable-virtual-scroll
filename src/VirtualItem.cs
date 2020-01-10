namespace BlazorSearchableVirtualScroll
{
    public class VirtualItem<TItem>
    {
        public string SearchableString { get; set; }
        public TItem Item { get; set; }
    }
}
