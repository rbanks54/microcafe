"use strict";

app.
    controller("LoginCtrl", function ($scope, $rootScope, oauth, currentUser, alerting, loginRedirect) {
        //----------------------------------------------------------
        // From the Common Application
        //----------------------------------------------------------
        // oauth - oauth service
        // currentUser - current user information
        // alerting - messaging 
        // loginRedirect - redirect information
        //----------------------------------------------------------
        var model = this;

        model.username = "";
        model.password = "";

        // Rest the currentUser
        //currentUser.resetProfile();
        oauth.logout();
        model.user = currentUser.profile;

        $scope.login = function (form) {
            if (form.$valid) {

                oauth.login(model.username, model.password)
                    .then(loginRedirect.redirectPreLogin)
                    .catch(function () {
                        alerting.addDanger("You have entered wrong password, please try again. User and password is <strong>demo/demo</strong> :)", "Invalid Login!");
                        alerting.showAsToaster();
                    }
                    );
                model.password = model.username = "";
                form.$setUntouched();
            }
        };
        model.signOut = function () {
            oauth.logout();
        };

        // Bind the model to the scope...
        $scope.model = model;

        // These are for navigation purposes
        $rootScope.isLoginPage = true;
        $rootScope.isLightLoginPage = false;
        $rootScope.isLockscreenPage = false;
        $rootScope.isMainPage = false;

    }).
    controller("LoginLightCtrl", function ($scope, $rootScope) {
        $rootScope.isLoginPage = true;
        $rootScope.isLightLoginPage = true;
        $rootScope.isLockscreenPage = false;
        $rootScope.isMainPage = false;
    }).
    controller("LockscreenCtrl", function ($scope, $rootScope) {
        $rootScope.isLoginPage = false;
        $rootScope.isLightLoginPage = false;
        $rootScope.isLockscreenPage = true;
        $rootScope.isMainPage = false;
    }).
    controller("MainCtrl", function ($scope, $rootScope, $location, $layout, $layoutToggles, $pageLoadingBar, Fullscreen) {
        $rootScope.isLoginPage = false;
        $rootScope.isLightLoginPage = false;
        $rootScope.isLockscreenPage = false;
        $rootScope.isMainPage = true;

        $rootScope.layoutOptions = {
            horizontalMenu: {
                isVisible: false,
                isFixed: true,
                minimal: false,
                clickToExpand: false,

                isMenuOpenMobile: false
            },
            sidebar: {
                isVisible: true,
                isCollapsed: false,
                toggleOthers: true,
                isFixed: true,
                isRight: false,

                isMenuOpenMobile: false,

                // Added in v1.3
                userProfile: true
            },
            chat: {
                isOpen: false
            },
            settingsPane: {
                isOpen: false,
                useAnimation: true
            },
            container: {
                isBoxed: false
            },
            skins: {
                sidebarMenu: "",
                horizontalMenu: "",
                userInfoNavbar: ""
            },
            pageTitles: true,
            userInfoNavVisible: false
        };

        $layout.loadOptionsFromCookies(); // remove this line if you don't want to support cookies that remember layout changes


        $scope.updatePsScrollbars = function () {
            var $scrollbars = jQuery(".ps-scrollbar:visible");

            $scrollbars.each(function (i, el) {
                if (typeof jQuery(el).data("perfectScrollbar") == "undefined") {
                    jQuery(el).perfectScrollbar();
                }
                else {
                    jQuery(el).perfectScrollbar("update");
                }
            });
        };


        // Define Public Vars
        public_vars.$body = jQuery("body");


        // Init Layout Toggles
        $layoutToggles.initToggles();


        // Other methods
        $scope.setFocusOnSearchField = function () {
            public_vars.$body.find(".search-form input[name=\"s\"]").focus();

            setTimeout(function () { public_vars.$body.find(".search-form input[name=\"s\"]").focus() }, 100);
        };


        // Watch changes to replace checkboxes
        $scope.$watch(function () {
            cbr_replace();
        });

        // Watch sidebar status to remove the psScrollbar
        $rootScope.$watch("layoutOptions.sidebar.isCollapsed", function (newValue, oldValue) {
            if (newValue !== oldValue) {
                if (newValue === true) {
                    public_vars.$sidebarMenu.find(".sidebar-menu-inner").perfectScrollbar("destroy");
                }
                else {
                    public_vars.$sidebarMenu.find(".sidebar-menu-inner").perfectScrollbar({ wheelPropagation: public_vars.wheelPropagation });
                }
            }
        });


        // Page Loading Progress (remove/comment this line to disable it)
        $pageLoadingBar.init();

        $scope.showLoadingBar = showLoadingBar;
        $scope.hideLoadingBar = hideLoadingBar;


        // Set Scroll to 0 When page is changed
        $rootScope.$on("$stateChangeStart", function () {
            var obj = { pos: jQuery(window).scrollTop() };

            TweenLite.to(obj, .25, {
                pos: 0, ease: Power4.easeOut, onUpdate: function () {
                    $(window).scrollTop(obj.pos);
                }
            });
        });


        // Full screen feature added in v1.3
        $scope.isFullscreenSupported = Fullscreen.isSupported();
        $scope.isFullscreen = Fullscreen.isEnabled() ? true : false;

        $scope.goFullscreen = function () {
            if (Fullscreen.isEnabled())
                Fullscreen.cancel();
            else
                Fullscreen.all();

            $scope.isFullscreen = Fullscreen.isEnabled() ? true : false;
        };
    }).
    controller("SidebarMenuCtrl", function ($scope, $rootScope, $menuItems, $timeout, $location, $state, $layout) {

        // Menu Items
        var $sidebarMenuItems = $menuItems.instantiate();

        $scope.menuItems = $sidebarMenuItems.prepareSidebarMenu().getAll();

        // Set Active Menu Item
        $sidebarMenuItems.setActive($location.path());

        $rootScope.$on("$stateChangeSuccess", function () {
            $sidebarMenuItems.setActive($state.current.name);
        });

        // Trigger menu setup
        public_vars.$sidebarMenu = public_vars.$body.find(".sidebar-menu");
        $timeout(setup_sidebar_menu, 1);

        ps_init(); // perfect scrollbar for sidebar
    }).
    controller("HorizontalMenuCtrl", function ($scope, $rootScope, $menuItems, $timeout, $location, $state) {
        var $horizontalMenuItems = $menuItems.instantiate();

        $scope.menuItems = $horizontalMenuItems.prepareHorizontalMenu().getAll();

        // Set Active Menu Item
        $horizontalMenuItems.setActive($location.path());

        $rootScope.$on("$stateChangeSuccess", function () {
            $horizontalMenuItems.setActive($state.current.name);

            $(".navbar.horizontal-menu .navbar-nav .hover").removeClass("hover"); // Close Submenus when item is selected
        });

        // Trigger menu setup
        $timeout(setup_horizontal_menu, 1);
    }).
    controller("SettingsPaneCtrl", function ($rootScope) {
        // Define Settings Pane Public Variable
        public_vars.$settingsPane = public_vars.$body.find(".settings-pane");
        public_vars.$settingsPaneIn = public_vars.$settingsPane.find(".settings-pane-inner");
    }).
    controller("ChatCtrl", function ($scope, $element) {
        var $chat = jQuery($element),
            $chat_conv = $chat.find(".chat-conversation");

        $chat.find(".chat-inner").perfectScrollbar(); // perfect scrollbar for chat container


        // Chat Conversation Window (sample)
        $chat.on("click", ".chat-group a", function (ev) {
            ev.preventDefault();

            $chat_conv.toggleClass("is-open");

            if ($chat_conv.is(":visible")) {
                $chat.find(".chat-inner").perfectScrollbar("update");
                $chat_conv.find("textarea").autosize();
            }
        });

        $chat_conv.on("click", ".conversation-close", function (ev) {
            ev.preventDefault();

            $chat_conv.removeClass("is-open");
        });
    }).
    controller("UIModalsCtrl", function ($scope, $rootScope, $modal, $sce) {
        // Open Simple Modal
        $scope.openModal = function (modal_id, modal_size, modal_backdrop) {
            $rootScope.currentModal = $modal.open({
                templateUrl: modal_id,
                size: modal_size,
                backdrop: typeof modal_backdrop == "undefined" ? true : modal_backdrop
            });
        };

        // Loading AJAX Content
        $scope.openAjaxModal = function (modal_id, url_location) {
            $rootScope.currentModal = $modal.open({
                templateUrl: modal_id,
                resolve: {
                    ajaxContent: function ($http) {
                        return $http.get(url_location).then(function (response) {
                            $rootScope.modalContent = $sce.trustAsHtml(response.data);
                        }, function (response) {
                            $rootScope.modalContent = $sce.trustAsHtml("<div class=\"label label-danger\">Cannot load ajax content! Please check the given url.</div>");
                        });
                    }
                }
            });

            $rootScope.modalContent = $sce.trustAsHtml("Modal content is loading...");
        };
    }).

    // Added in v1.3
    controller("FooterChatCtrl", function ($scope, $element) {
        $scope.isConversationVisible = false;

        $scope.toggleChatConversation = function () {
            $scope.isConversationVisible = !$scope.isConversationVisible;

            if ($scope.isConversationVisible) {
                setTimeout(function () {
                    var $el = $element.find(".ps-scrollbar");

                    if ($el.hasClass("ps-scroll-down")) {
                        $el.scrollTop($el.prop("scrollHeight"));
                    }

                    $el.perfectScrollbar({
                        wheelPropagation: false
                    });

                    $element.find(".form-control").focus();

                }, 300);
            }
        };
    });