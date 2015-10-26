"use strict";

//angular.module('journeyDesigner.services', []).
app.
    service("$menuItems", function () {
        this.menuItems = [];

        var $menuItemsRef = this;

        var menuItemObj = {
            parent: null,

            title: "",
            link: "", // starting with "./" will refer to parent link concatenation
            state: "", // will be generated from link automatically where "/" (forward slashes) are replaced with "."
            icon: "",

            isActive: false,
            label: null,

            menuItems: [],

            setLabel: function (label, color, hideWhenCollapsed) {
                if (typeof hideWhenCollapsed == "undefined")
                    hideWhenCollapsed = true;

                this.label = {
                    text: label,
                    classname: color,
                    collapsedHide: hideWhenCollapsed
                };

                return this;
            },

            addItem: function (title, link, icon) {
                var parent = this,
                    item = angular.extend(angular.copy(menuItemObj), {
                        parent: parent,

                        title: title,
                        link: link,
                        icon: icon
                    });

                if (item.link) {
                    if (item.link.match(/^\./))
                        item.link = parent.link + item.link.substring(1, link.length);

                    if (item.link.match(/^-/))
                        item.link = parent.link + "-" + item.link.substring(2, link.length);

                    item.state = $menuItemsRef.toStatePath(item.link);
                }

                this.menuItems.push(item);

                return item;
            }
        };

        this.addItem = function (title, link, icon) {
            var item = angular.extend(angular.copy(menuItemObj), {
                title: title,
                link: link,
                state: this.toStatePath(link),
                icon: icon
            });

            this.menuItems.push(item);

            return item;
        };

        this.getAll = function () {
            return this.menuItems;
        };

        this.prepareSidebarMenu = function () {
            var landing = this.addItem("Home", "/app/dashboard-welcome", "linecons-desktop");
            var product = this.addItem("Products", "/app/journeydesigner-products", "fa-globe");
            var itineraries = this.addItem("Itineraries", "/app/itineraries-index", "fa-calendar");
            var masterData = this.addItem("Master Data", "/app/masterdata", "fa-book");
        
            // Subitems of Master Data
            masterData.addItem("Brands", "-/brands"); // "-/" will append parents link
            masterData.addItem("Operators", "-/operators"); // "-/" will append parents link

            return this;
        };

        this.prepareHorizontalMenu = function () {
            var landing = this.addItem("Home", "/app/dashboard-welcome", "linecons-desktop");
            var product = this.addItem("Products", "/app/journeydesigner", "fa-globe");
            var itineraries = this.addItem("Itineraries", "/app/itineraries", "fa-calendar");
            var masterData = this.addItem("Master Data", "/app/masterdata", "fa-book");
        
            // Subitems of Products
            product.addItem("Products", "-/index"); // "-/" will append parents link
            product.addItem("Add Product", "-/new");

            // Subitems of Itineraries
            itineraries.addItem("Itineraries", "-/index"); // "-/" will append parents link
            itineraries.addItem("Add Itinerary", "-/new");

            // Subitems of Master Data
            masterData.addItem("Brands", "-/brands"); // "-/" will append parents link
            masterData.addItem("Operators", "-/operators"); // "-/" will append parents link
            
            return this;
        }

        this.instantiate = function () {
            return angular.copy(this);
        }

        this.toStatePath = function (path) {
            return path.replace(/\//g, ".").replace(/^\./, "");
        };

        this.setActive = function (path) {
            this.iterateCheck(this.menuItems, this.toStatePath(path));
        };

        this.setActiveParent = function (item) {
            item.isActive = true;
            item.isOpen = true;

            if (item.parent)
                this.setActiveParent(item.parent);
        };

        this.iterateCheck = function (menuItems, currentState) {
            angular.forEach(menuItems, function (item) {
                if (item.state == currentState) {
                    item.isActive = true;

                    if (item.parent != null)
                        $menuItemsRef.setActiveParent(item.parent);
                }
                else {
                    item.isActive = false;
                    item.isOpen = false;

                    if (item.menuItems.length) {
                        $menuItemsRef.iterateCheck(item.menuItems, currentState);
                    }
                }
            });
        }
    });
