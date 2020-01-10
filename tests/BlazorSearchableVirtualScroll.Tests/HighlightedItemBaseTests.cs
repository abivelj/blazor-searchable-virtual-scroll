using System;
using Xunit;

namespace BlazorSearchableVirtualScroll.Tests
{
    public class HighlightedItemBaseTests
    {
        private HighlightedItemBase _sut;

        public HighlightedItemBaseTests() => _sut = new HighlightedItemBase();

        [Fact]
        public void it_should_highlight_1()
        {
            _sut.Value = "abc";
            _sut.HighlightValue = "a";

            Assert.Equal("", _sut.Starting);
            Assert.Equal("a", _sut.Middle);
            Assert.Equal("bc", _sut.Ending);
        }

        [Fact]
        public void it_should_highlight_2()
        {
            _sut.Value = "abc";
            _sut.HighlightValue = "b";

            Assert.Equal("a", _sut.Starting);
            Assert.Equal("b", _sut.Middle);
            Assert.Equal("c", _sut.Ending);
        }

        [Fact]
        public void it_should_highlight_3()
        {
            _sut.Value = "abc";
            _sut.HighlightValue = "bc";

            Assert.Equal("a", _sut.Starting);
            Assert.Equal("bc", _sut.Middle);
            Assert.Equal("", _sut.Ending);
        }

        [Fact]
        public void it_should_highlight_4()
        {
            _sut.Value = "abc";
            _sut.HighlightValue = "ab";

            Assert.Equal("", _sut.Starting);
            Assert.Equal("ab", _sut.Middle);
            Assert.Equal("c", _sut.Ending);
        }

        [Fact]
        public void it_should_highlight_5()
        {
            _sut.Value = "a";
            _sut.HighlightValue = "ab";

            Assert.Equal("a", _sut.Starting);
            Assert.Equal("", _sut.Middle);
            Assert.Equal("", _sut.Ending);
        }

        [Fact]
        public void it_should_highlight_6()
        {
            _sut.Value = "abc";
            _sut.HighlightValue = "abc";

            Assert.Equal("", _sut.Starting);
            Assert.Equal("abc", _sut.Middle);
            Assert.Equal("", _sut.Ending);
        }

    }
}
