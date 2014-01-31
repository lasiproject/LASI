var __extends = this.__extends || function (d, b) {
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var ELang;
(function (ELang) {
    var ELangTestDelegates = (function () {
        function ELangTestDelegates() {
            this.startStopHandler = null;
            this.rdoVariantHandler = null;
            this.rdoModeHandler = null;
            this.rdoQuestionHandler = null;
            this.rdoVariantClickHandler = null;
            this.rdoModeClickHandler = null;
            this.rdoQuestionClickHandler = null;
        }
        return ELangTestDelegates;
    })();
    ELang.ELangTestDelegates = ELangTestDelegates;    
    var ELangTestDefaults = (function (_super) {
        __extends(ELangTestDefaults, _super);
        function ELangTestDefaults() {
                _super.call(this);
            this.formHtml = '<form class="form-search"><div class="input-append"></div></form>';
            this.headLabel = "lblTestHead";
            this.resultHeadLabel = "lblFindedExpressionsHead";
            this.startButtonLabel = "lblStartTest";
            this.stopButtonLabel = "lblStopTest";
            this.rdoTypedLabel = "lblTypedTest";
            this.rdoSelectedLabel = "lblSelectedTest";
            this.rdoOrderedLabel = "lblOrderedTest";
            this.rdoRandomlyLabel = "lblRandomlyTest";
            this.rdoWrittedLabel = "lblWrittedTest";
            this.rdoVoicedLabel = "lblVoicedTest";
            this.rdoTypedTooltip = "lblTypedTestHlp";
            this.rdoSelectedTooltip = "lblSelectedTestHlp";
            this.rdoOrderedTooltip = "lblOrderedTestHlp";
            this.rdoRandomlyTooltip = "lblRandomlyTestHlp";
            this.rdoWrittedTooltip = "lblWrittedTestHlp";
            this.rdoVoicedTooltip = "lblVoicedTestHlp";
        }
        return ELangTestDefaults;
    })(ELang.ELangBaseDefaults);
    ELang.ELangTestDefaults = ELangTestDefaults;    
    var ELangTest = (function (_super) {
        __extends(ELangTest, _super);
        function ELangTest() {
                _super.call(this);
            this.name = "eLang-Test";
            this.description = "eLang - Language Learning test functionality.";
            this.defaults = new ELangTestDefaults();
            this.delegates = new ELangTestDelegates();
        }
        ELangTest.prototype.initialize = function (target, options) {
            _super.prototype.initialize.call(this, target, options);
            this.delegates.rdoModeHandler = jQuery.proxy(this._onRdoModeClick, this);
            this.delegates.rdoQuestionHandler = jQuery.proxy(this._onRdoQuestionClick, this);
            this.delegates.rdoVariantHandler = jQuery.proxy(this._onRdoVariantClick, this);
            this.delegates.startStopHandler = jQuery.proxy(this._onStartStopClick, this);
            var handlerMode = this.delegates.rdoModeHandler;
            var handlerQuestion = this.delegates.rdoQuestionHandler;
            var handlerVariant = this.delegates.rdoVariantHandler;
            this.delegates.rdoModeClickHandler = function () {
                var srcE = this;
                handlerMode(srcE);
            };
            this.delegates.rdoQuestionClickHandler = function () {
                var srcE = this;
                handlerQuestion(srcE);
            };
            this.delegates.rdoVariantClickHandler = function () {
                var srcE = this;
                handlerVariant(srcE);
            };
            this.createContent();
            this.element.data("elang-test", jQuery.proxy(this.processCommand, this));
        };
        ELangTest.prototype.createContent = function () {
            _super.prototype.createContent.call(this);
            var contentDiv = this.element.next("div");
            var resultSelector = "." + this.defaults.resultCSS.split(" ")[0];
            var result = contentDiv.find("*").filter(resultSelector);
            var fluidRow = jQuery(this.defaults.fluidRowHtml);
            var radioCont = this.getLastChild(fluidRow);
            result.before(fluidRow);
            this.createRadioGroup(fluidRow, true, 2, 0, [
                this.defaults.rdoOrderedLabel, 
                this.defaults.rdoRandomlyLabel
            ], this.delegates.rdoVariantClickHandler, [
                this.defaults.rdoOrderedTooltip, 
                this.defaults.rdoRandomlyTooltip
            ]);
            this.createRadioGroup(fluidRow, true, 2, 1, [
                this.defaults.rdoTypedLabel, 
                this.defaults.rdoSelectedLabel
            ], this.delegates.rdoModeClickHandler, [
                this.defaults.rdoTypedTooltip, 
                this.defaults.rdoSelectedTooltip
            ]);
            this.createRadioGroup(fluidRow, true, 2, 0, [
                this.defaults.rdoWrittedLabel, 
                this.defaults.rdoVoicedLabel
            ], this.delegates.rdoQuestionClickHandler, [
                this.defaults.rdoWrittedTooltip, 
                this.defaults.rdoVoicedTooltip
            ]);
            fluidRow.children(".btn-group").addClass("span4");
        };
        ELangTest.prototype._onRdoVariantClick = function (eSrc) {
            this.isOrdered = this.isRdoChecked(eSrc, this.defaults.rdoOrderedLabel);
        };
        ELangTest.prototype._onRdoModeClick = function (eSrc) {
            this.isSelected = this.isRdoChecked(eSrc, this.defaults.rdoSelectedLabel);
        };
        ELangTest.prototype._onRdoQuestionClick = function (eSrc) {
            this.isWritted = this.isRdoChecked(eSrc, this.defaults.rdoWrittedLabel);
        };
        ELangTest.prototype._onStartStopClick = function () {
        };
        return ELangTest;
    })(ELang.ELangBase);
    ELang.ELangTest = ELangTest;    
})(ELang || (ELang = {}));
(function (jQuery) {
    jQuery.fn.elangTest = function (options, command) {
        var result = new ELang.FnJQuery(this, options, command, "ELangTest", "elang-test");
        return result;
    };
})(jQuery);
