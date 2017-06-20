(function () {
    "use strict";

    angular.module("runniacFilters", []).filter('dateFromStringShort', function () {
        return function (dateString) {
            if (dateString != null)
                return moment(new Date(dateString.replace(/(\d{2})\/(\d{2})\/(\d{4})/, "$2/$1/$3"))).format("DD/MM/YYYY")
            else
                return "";
        };
    }).filter('defaultLogoIfNotFound', function () {
        return function (imageUrl) {
            if (imageUrl == null)
                return "/Imgs/running-icon.png";
            else
                return imageUrl;
        };
    }).filter('showLinkIfAvailable', function () {
        return function (data) {
            if (data == null)
                return "No disponible";
            else
                return "<a href=\"" + data + "\">" + data + "</a>";
        };
    }).filter('showOrAnonymous', function () {
        return function (data) {
            if (data == null)
                return "Anónimo";
            else
                return data;
        };
    }).filter('zeroIfNull', function () {
        return function (data) {
            if (data == null || isNaN(data))
                return 0;
            else
                return data;
        };
    }).filter('cleanHash', function () {
        return function (text) {
            return text.replace(/#/g, 'hash');
        }
    });

})();