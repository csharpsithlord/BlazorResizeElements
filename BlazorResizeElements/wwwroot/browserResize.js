
window.browserResize = (() => {

    let dotnetRef = null;

    function getSize() {
        return {
            width: window.innerWidth,
            height: window.innerHeight
        };
    }

    function onResize() {
        if (!dotnetRef) return;

        dotnetRef.invokeMethodAsync(
            "NotifyResize",
            window.innerWidth,
            window.innerHeight
        );
    }

    function register(ref) {
        dotnetRef = ref;
        window.addEventListener("resize", onResize);
    }

    function unregister() {
        window.removeEventListener("resize", onResize);
        dotnetRef = null;
    }

    return {
        getSize,
        register,
        unregister
    };
})();
