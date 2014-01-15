var __extends = this.__extends || function (d, b) {
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var ELang;
(function (ELang) {
    var ELangSearchEvents = (function () {
        function ELangSearchEvents() {
            this.select = new jQuery.Deferred();
        }
        return ELangSearchEvents;
    })();
    ELang.ELangSearchEvents = ELangSearchEvents;    
    var ELangSearchDelegates = (function () {
        function ELangSearchDelegates() {
            this.selectCallback = null;
            this.selectHandler = null;
            this.langDirectionHandler = null;
            this.langDirectionClickHandler = null;
            this.searchHandler = null;
            this.searchClickHandler = null;
        }
        return ELangSearchDelegates;
    })();
    ELang.ELangSearchDelegates = ELangSearchDelegates;    
    var ELangSearchDefaults = (function (_super) {
        __extends(ELangSearchDefaults, _super);
        function ELangSearchDefaults() {
                _super.call(this);
            this.headLabel = "lblFindHead";
            this.resultHeadLabel = "lblFindedExpressionsHead";
            this.expressionsLabel = "lblSearchInExpressions";
            this.meaningsLabel = "lblSearchInMeanings";
            this.expressionsTooltip = "lblSearchInExpressionsHlp";
            this.meaningsTooltip = "lblSearchInMeaningsHlp";
            this.searchFormHtml = '<form class="form-search"><div class="input-append"></div></form>';
            this.searchFieldHtml = '<input type="text" class="search-query" />';
            this.searchButtonLabel = "lblFind";
        }
        return ELangSearchDefaults;
    })(ELang.ELangBaseDefaults);
    ELang.ELangSearchDefaults = ELangSearchDefaults;    
    var ELangSearch = (function (_super) {
        __extends(ELangSearch, _super);
        function ELangSearch() {
                _super.call(this);
            this.name = "eLang-Search";
            this.description = "eLang - Language Learning search functionality.";
            this.defaults = new ELangSearchDefaults();
            this.delegates = new ELangSearchDelegates();
            this.events = new ELangSearchEvents();
            this.isSearchInExp = true;
        }
        ELangSearch.prototype.initialize = function (target, options) {
            _super.prototype.initialize.call(this, target, options);
            this.delegates.selectHandler = jQuery.proxy(this._onSelect, this);
            this.delegates.selectCallback = jQuery.proxy(this._onSelectCallback, this);
            this.delegates.langDirectionHandler = jQuery.proxy(this._onDirectionClick, this);
            this.delegates.searchHandler = jQuery.proxy(this._select, this);
            this.events.select.done(this.delegates.selectHandler);
            var handlerDirection = this.delegates.langDirectionHandler;
            var handlerSearch = this.delegates.searchHandler;
            this.delegates.searchClickHandler = function () {
                var srcE = this;
                var el = srcE.parentNode["getElementsByTagName"]("input")[0];
                handlerSearch(el);
            };
            this.delegates.langDirectionClickHandler = function () {
                var srcE = this;
                handlerDirection(srcE);
            };
            this.createContent();
            this.element.data("elang-search", jQuery.proxy(this.processCommand, this));
        };
        ELangSearch.prototype.createContent = function () {
            _super.prototype.createContent.call(this);
            var contentDiv = this.element.next("div");
            var resultSelector = "." + this.defaults.resultCSS.split(" ")[0];
            var result = contentDiv.find("*").filter(resultSelector);
            this.createRadioGroup(result, false, 2, 0, [
                this.defaults.expressionsLabel, 
                this.defaults.meaningsLabel
            ], this.delegates.langDirectionClickHandler, [
                this.defaults.expressionsTooltip, 
                this.defaults.meaningsTooltip
            ]);
            var form = jQuery(this.defaults.searchFormHtml);
            var input = jQuery(this.defaults.searchFieldHtml);
            var search = jQuery(this.defaults.submitButtonHtml);
            var formIn = this.getLastChild(form);
            search.add(search.find("*")).filter("span").attr("id", this.defaults.searchButtonLabel);
            search.click(this.delegates.searchClickHandler);
            formIn.append(input);
            formIn.append(search);
            result.before(form);
            ELang.ELangCommon.setLang(ELang.ELangCommon.resource.selectedLang, contentDiv);
        };
        ELangSearch.prototype._onDirectionClick = function (eSrc) {
            this.isSearchInExp = this.isRdoChecked(eSrc, this.defaults.expressionsLabel);
        };
        ELangSearch.prototype._onSelect = function (eSrc) {
            var id = eSrc.value;
            if(id) {
                var db = ELang.ELangDB.getInstance();
                db.select(id, this.delegates.selectCallback);
            } else {
                this._onSelectCallback();
            }
        };
        ELangSearch.prototype._onSelectCallback = function () {
        };
        ELangSearch.prototype._select = function (eSrc) {
            this.events.select.resolve(eSrc);
        };
        return ELangSearch;
    })(ELang.ELangBase);
    ELang.ELangSearch = ELangSearch;    
})(ELang || (ELang = {}));
(function (jQuery) {
    jQuery.fn.elangSearch = function (options, command) {
        var result = new ELang.FnJQuery(this, options, command, "ELangSearch", "elang-search");
        return result;
    };
})(jQuery);
