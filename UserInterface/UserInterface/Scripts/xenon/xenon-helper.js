var appHelper = {
    // Vars (paths without trailing slash)
    templatesDir: 'views',
    assetsDir: '/content',
    appDir: '/apps/JourneyDesigner',
    
    shellDir: '/apps/shell',

    journeyDesignerDir: '/apps/JourneyDesigner',
    templateDir: 'views',

    adminDir: '/apps/Admin',

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
    
    journeyDesignerTemplatePath: function (view_name) {
        return this.journeyDesignerDir + '/' + this.templateDir + '/' + view_name + '.html';
    },

    shellTemplatePath: function (view_name) {
        return this.shellDir + '/' + this.templateDir + '/' + view_name + '.html';
    }
};