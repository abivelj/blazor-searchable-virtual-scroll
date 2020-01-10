# blazor-searchable-virtual-scroll

A searchable virtual scroll component

## How to use

### On your `index.html` or `index.cshtml` page

Add in the following css and javascript links

```html
<link
  href="_content/BlazorSearchableVirtualScroll/blazorSearchableVirtualScroll.css"
  rel="stylesheet"
/>

<script
  src="_content/BlazorSearchableVirtualScroll/blazorSearchableVirtualScroll.js"
  type="text/javascript"
></script>
```

### On your `Component.razor` page

Add the component

```html
<VirtualSearch
  TItem="string"
  Items="Items"
  ItemsToShow="8"
  ItemsToPreRender="3"
  ItemHeight="37"
  OnClick="OnClick"
/>
```

Load the items and handle onclick

```csharp
    private List<VirtualItem<string>> Items = new List<VirtualItem<string>>();

    private string selectedItem = "";

    private void OnClick(VirtualItem<string> selected)
    {
        selectedItem = selected.SearchableString;
    }

    protected override void OnInitialized()
    {
        for (var i = 0; i < 1000; i++)
        {
            Items.Add(new VirtualItem<string> { SearchableString = i.ToString(), Item = i.ToString() });
        }
    }

```
