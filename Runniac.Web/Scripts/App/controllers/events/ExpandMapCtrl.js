angular.module('runniac').controller('ExpandMapController', ['$scope', '$rootScope', '$modalInstance', '$timeout', '$window',
    '$location', '$http', 'event', 'olData', 'MapUtils', 'ConversionUtils', 'BrowserUtils',
    function ($scope, $rootScope, $modalInstance, $timeout, $window, $location, $http, event, olData, MapUtils, ConversionUtils,
        BrowserUtils) {

        $scope.event = event;

        $scope.madrid = MapUtils.madrid;
        $scope.bing = MapUtils.bing;
        $scope.bingBaseLayers = MapUtils.bingBaseLayers;


        $timeout(function () {
            olData.getMap("expandedMap").then(function (map) {
                var source;
                var extension = MapUtils.getFileExtension($scope.event.CourseFileName);
                var url = MapUtils.courseFilesUrl + $scope.event.CourseFileName;

                if (extension.toLowerCase() == "kml") {
                    source = new ol.source.KML({ "projection": MapUtils.projection, url: url });
                } else if (extension.toLowerCase() == "gpx") {
                    source = new ol.source.GPX({ "projection": MapUtils.projection, url: url });
                } else {
                    toastr.warning("Tipo de archivo no soportado. Las extensiones aceptadas son KML y GPX");
                }

                //Esperar a que se descargue el archivo KML o GPX para hacer zoom sobre la capa
                var key = source.on('change', function () {
                    if (source.getState() == 'ready') {
                        source.unByKey(key);
                        var layer = new ol.layer.Vector({ source: source, name: "Course" });
                        map.updateSize();
                        MapUtils.addAndZoomToLayer(map, layer);
                    }
                });
            });
        }, 500);

        $scope.changeImagery = function (e, imagerySet) {
            MapUtils.changeImagery(e, imagerySet);
        };

        $scope.isIeAndKml = (BrowserUtils.detectIE() || BrowserUtils.isSafari())
            && MapUtils.getFileExtension($scope.event.CourseFileName) === "kml";

        $scope.isIeAndGpx = (BrowserUtils.detectIE() || BrowserUtils.isSafari())
            && MapUtils.getFileExtension($scope.event.CourseFileName) === "gpx";

        $scope.kmlDownload = "javascript:void(0)";
        $scope.exportAsKml = function () {
            olData.getMap("expandedMap").then(function (map) {
                var features = MapUtils.getFeaturesFromLayer(map);
                var string = new ol.format.KML().writeFeatures(features);

                if (BrowserUtils.detectIE() || BrowserUtils.isSafari()) {
                    window.open(MapUtils.courseFilesUrl + $scope.event.CourseFileName);
                } else {
                    var base64 = ConversionUtils.strToBase64(string);
                    $scope.kmlDownload = 'data:text/kml+xml;base64,' + base64;
                }
            });
        };

        $scope.gpxDownload = "javascript:void(0)";
        $scope.exportAsGpx = function () {
            olData.getMap("expandedMap").then(function (map) {
                var features = MapUtils.getFeaturesFromLayer(map);
                var string = new ol.format.GPX().writeFeatures(features);
                if (BrowserUtils.detectIE() || BrowserUtils.isSafari()) {
                    window.open(MapUtils.courseFilesUrl + $scope.event.CourseFileName);
                } else {
                    var base64 = ConversionUtils.strToBase64(string);
                    $scope.gpxDownload = 'data:text/gpx+xml;base64,' + base64;
                }
            });
        };

        $scope.ok = function () {
            $modalInstance.close();
        };

        $scope.close = function () {
            $modalInstance.close();
        };

        $scope.cancel = function () {
            $modalInstance.dismiss();
        };
    }
]);