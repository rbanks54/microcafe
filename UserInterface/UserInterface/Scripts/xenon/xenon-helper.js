var appHelper = {
    // Vars (paths without trailing slash)
    templatesDir: 'views',
    assetsDir: '/content',
    appDir: '/apps/JourneyDesigner',
    
    shellTemplateDir: 'views',
    shellDir: '/apps/shell',

    journeyDesignerDir: '/apps/JourneyDesigner',
    journeyDesignerTemplateDir: 'views',

    masterDataDir: '/apps/MasterData',
    masterDataTemplateDir: 'views',

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

    masterDataTemplatePath: function (view_name) {
        return this.masterDataDir + '/' + this.masterDataTemplateDir + '/' + view_name + '.html';
    },
    
    journeyDesignerTemplatePath: function (view_name) {
        return this.journeyDesignerDir + '/' + this.journeyDesignerTemplateDir + '/' + view_name + '.html';
    },

    shellTemplatePath: function (view_name) {
        return this.shellDir + '/' + this.shellTemplateDir + '/' + view_name + '.html';
    }
};