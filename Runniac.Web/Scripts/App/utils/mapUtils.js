(function () {
    "use strict";

    angular.module("runniac").factory("MapUtils", ['$timeout', 'olData',
        function ($timeout, olData) {
            var factory = {};

            factory.KML_MIME_TYPE = "application/vnd.google-earth.kml+xml";
            factory.GPX_MIME_TYPE = "application/gpx+xml";

            factory.projection = "EPSG:3857";
            factory.courseFilesUrl = "/FileStorage/Courses/";
            factory.madrid = {
                lat: 40.4165,
                lon: -3.70256,
                zoom: 5
            };
            factory.bing = {
                source: {
                    name: 'Bing Maps',
                    type: 'BingMaps',
                    key: 'Aj6XtE1Q1rIvehmjn2Rh1LR2qvMGZ-8vPS9Hn3jCeUiToM77JFnf-kFRzyMELDol',
                    imagerySet: 'Road'
                }
            };
            factory.bingBaseLayers = ['Road', 'Aerial', 'AerialWithLabels'];

            factory.getFileExtension = function (fileName) {
                var fileNameChunks = fileName.split('.');
                var extension = fileNameChunks[fileNameChunks.length - 1];

                return extension;
            };

            factory.addAndZoomToLayer = function (map, layer) {
                map.addLayer(layer);
                var extent = layer.getSource().getExtent();
                map.getView().fitExtent(extent, map.getSize());
            };

            factory.findLayerByName = function (map, name) {
                var layers = map.getLayers();
                var length = layers.getLength();
                for (var i = 0; i < length; i++) {
                    if (name === layers.item(i).get('name')) {
                        return layers.item(i);
                    }
                }
                return null;
            };

            factory.changeImagery = function (e, imagerySet) {
                factory.bing.source.imagerySet = imagerySet;
                e.preventDefault();
            };

            factory.getFeaturesFromLayer = function (map) {
                var vector = factory.findLayerByName(map, "Course");
                var features = [];
                vector.getSource().forEachFeature(function (feature) {
                    var clone = feature.clone();
                    clone.getGeometry().transform(factory.projection, 'EPSG:4326');
                    features.push(clone);
                });
                return features;
            };

            factory.zoomToMarkers = function () {
                olData.getMap().then(function (map) {
                    $timeout(function () {
                        var markersLayer;
                        var layers = map.getLayers();
                        var length = layers.getLength();
                        for (var i = 0; i < length; i++) {
                            if (layers.item(i).get('markers')) {
                                markersLayer = layers.item(i);
                            }
                        }

                        if (markersLayer) {
                            var extent = markersLayer.getSource().getExtent();

                            if (Math.abs(extent[0] - extent[2]) <= 2500)
                                extent = ol.extent.buffer(extent, 5000);
                            else
                                extent = ol.extent.buffer(extent, 15000);

                            map.getView().fitExtent(extent, map.getSize());
                        }
                    }, 500);
                });
            };

            factory.getMarkerInThisPosition = function (markers, lat, lon) {
                var found;

                angular.forEach(markers, function (value, key) {
                    if (value.lat == lat && value.lon == lon) {
                        found = value;
                        return;
                    }
                });

                return found;
            };

            factory.getPopupContent = function (value) {
                return '<img class="overlay-img" src="' + value.ImageUrl +
                            '" /><a href="#/eventDetail/' + value.Id + '">' + value.Name + '</a>';
            };

            factory.addPopupContent = function (marker, value) {
                marker.label.message += this.getPopupContent(value);
            };

            factory.createMarker = function (value) {
                return {
                    name: value.Id,
                    lat: value.Town.Latitude,
                    lon: value.Town.Longitude,
                    label: {
                        message: this.getPopupContent(value),
                        show: false,
                        keepOneOverlayVisible: true
                    }
                };
            };

            return factory;
        }]);
})();