window.navigatorInterop = {
    getCurrentPosition: function (dotNetObjectReference) {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(function (position) {
                dotNetObjectReference.invokeMethodAsync('HandlePosition', position.coords.latitude, position.coords.longitude);
            }, function (error) {
                dotNetObjectReference.invokeMethodAsync('HandleError', error.message);
            });
        } else {
            dotNetObjectReference.invokeMethodAsync('HandleError', 'La geolocalización no es compatible en este navegador.');
        }
    }
};
