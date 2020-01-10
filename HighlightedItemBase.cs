using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Components;

namespace BlazorSearchableVirtualScroll
{
    public class HighlightedItemBase : ComponentBase
    {
        private string _value = string.Empty;
        private string _highlightValue = string.Empty;

        [Parameter]
        public string Value
        {
            get => _value ?? string.Empty;
            set => _value = value;
        }

        [Parameter]
        public string HighlightValue
        {
            get => _highlightValue ?? string.Empty;
            set => _highlightValue = value;
        }

        private int StartIndex
        {
            get
            {
                var startIndex = Value.IndexOf(HighlightValue, StringComparison.OrdinalIgnoreCase);
                return startIndex < 0 ? 0 : startIndex;
            }
        }

        public string Starting => HighlightValue.Length > Value.Length ? Value : Value.Substring(0, StartIndex);
        public string Middle => HighlightValue.Length <= Value.Length ? Value.Substring(StartIndex, Math.Min(Value.Length, HighlightValue.Length)) : string.Empty;
        public string Ending => HighlightValue.Length < Value.Length ? Value.Substring(StartIndex + HighlightValue.Length) : string.Empty;

    }
}
