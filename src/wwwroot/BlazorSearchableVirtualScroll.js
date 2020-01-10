window.blazorSearchableVirtualScroll = {
    getScrollTop: function (element) {
        return parseInt(element.scrollTop);
    },
    setupOnOutsideClick: function (element, instance) {
        window.addEventListener("click", function (e) {
            console.log(element, instance, e);
            var path = e.path;
            var hide = true;
            for (var i = 0; i < path.length; i++) {
                this.console.log(path[i], element, path[i] === element);
                if (path[i] === element)
                    hide = false;
            }
            if (hide) {
                instance.invokeMethodAsync("OnCloseVirtualScroll");
            }
        });
    }
};
