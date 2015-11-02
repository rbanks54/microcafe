var appHelper = {
    // Vars (paths without trailing slash)
    templatesDir: 'views',
    assetsDir: '/content',
    appDir: '/apps/cashier',
    
    shellDir: '/apps/shell',

    cashierDir: '/apps/cashier',
    baristaDir: '/apps/barista',
    templateDir: 'views',

    adminDir: '/apps/admin',

    // Methods
    applicationPath: function (view_name) {
        return this.appDir + '/' + + view_name + '.html';
    },

    templatePath: function (view_name) {
        return this.templatesDir + '/' + view_name + '.html';
    },

    assetPath: function (file_path) {
        return this.assetsDir + '/' + file_path;
    },

    adminTemplatePath: function (view_name) {
        return this.adminDir + '/' + this.templateDir + '/' + view_name + '.html';
    },
    
    cashierTemplatePath: function (view_name) {
        return this.cashierDir + '/' + this.templateDir + '/' + view_name + '.html';
    },

    baristaTemplatePath: function (view_name) {
        return this.basristaDir + '/' + this.templateDir + '/' + view_name + '.html';
    },

    shellTemplatePath: function (view_name) {
        return this.shellDir + '/' + this.templateDir + '/' + view_name + '.html';
    }
};