(function () {
    "use strict";

    angular.module("runniac").controller("PreviewCourseCtrl", ["$scope", "$rootScope", "olData", "$upload", "MapUtils",
        function ($scope, $rootScope, olData, $upload, MapUtils) {

            $scope.madrid = MapUtils.madrid;
            $scope.bing = MapUtils.bing;
            $scope.bingBaseLayers = MapUtils.bingBaseLayers;

            $rootScope.$on("showCourseOnMap", function (event, fileName, mapId) {
                var source;

                var extension = MapUtils.getFileExtension(fileName);
                var url = MapUtils.courseFilesUrl + fileName;

                olData.getMap(mapId).then(function (map) {
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
                            MapUtils.addAndZoomToLayer(map, layer);
                        }
                    });
                });
            });

            $scope.changeImagery = function (e, imagerySet) {
                MapUtils.changeImagery(e, imagerySet);
            };

            $scope.onFileSelect = function ($files, $event) {
                $scope.file = $files[0];
                var reader = new FileReader();

                reader.onloadend = function () {
                    $scope.content = reader.result;

                    olData.getMap().then(function (map) {
                        var extension = MapUtils.getFileExtension($scope.file.name);
                        var source;

                        if (extension.toLowerCase() == "kml") {
                            source = new ol.source.KML({ "projection": MapUtils.projection });
                        } else if (extension.toLowerCase() == "gpx") {
                            source = new ol.source.GPX({ "projection": MapUtils.projection });
                        } else {
                            toastr.warning("Tipo de archivo no soportado. Las extensiones aceptadas son KML y GPX");
                        }

                        if (source != null) {
                            var features = source.readFeatures($scope.content);
                            var layer = new ol.layer.Vector({
                                source: new ol.source.Vector({ features: features }),
                                name: "Course"
                            });
                            MapUtils.addAndZoomToLayer(map, layer);
                        }
                    });
                }
                reader.readAsText($scope.file);
            };

            $scope.resetCourse = function () {
                olData.getMap().then(function (map) {
                    map.removeLayer(MapUtils.findLayerByName(map, "Course"));
                });
            };

            $rootScope.$on("uploadCourse", function (event, eventId) {
                if ($scope.file != null) {
                    $scope.upload = $upload.upload({
                        url: '/api/events/uploadCourse',
                        method: 'POST',
                        data: { eventId: eventId },
                        file: $scope.file,
                    }).success(function (data, status, headers, config) {
                        $scope.file = null;
                    }).error(function (errorResult) {
                        toastr.error(errorResult);
                    });
                }
            });
        }]);
})();