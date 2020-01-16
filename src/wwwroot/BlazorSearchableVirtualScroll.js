window.blazorSearchableVirtualScroll = {
    getScrollTop: function (element) {
        return parseInt(element.scrollTop);
    },
    setupOnOutsideClick: function (element, instance) {
        window.addEventListener("click", function (e) {
            var path = e.path;
            var hide = true;
            for (var i = 0; i < path.length; i++) {
                if (path[i] === element)
                    hide = false;
            }
            if (hide) {
                instance.invokeMethodAsync("OnCloseVirtualScroll");
            }
        });
    }
};
