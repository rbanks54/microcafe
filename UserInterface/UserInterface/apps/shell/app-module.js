"use strict";

var app = angular.module("journeyDesigner-app", [
    "common",
    "ngCookies",
    "ngRoute",

    "ui.router",
    "ui.bootstrap",

    "oc.lazyLoad",

    // Added in v1.3
    "FBAngular",

    "dndLists"
]);

app.run(function () {
    // Page Loading Overlay
    public_vars.$pageLoadingOverlay = jQuery(".page-loading-overlay");

    jQuery(window).load(function () {
        public_vars.$pageLoadingOverlay.addClass("loaded");
    })
});