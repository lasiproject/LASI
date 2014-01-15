var __extends = this.__extends || function (d, b) {
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
// Type definitions for eLang.edit 0.5.1
// Project: https://github.com/sumegizoltan/ELang/
// Definitions by: Zoltan Sumegi <https://github.com/sumegizoltan/>
// Definitions:
/// <reference path="../jquery/jquery.d.ts"/>
/// <reference path="./jquery.elang.d.ts"/>
/// <reference path="./jquery.elang.common.ts"/>
/// <reference path="./jquery.elang.db.ts"/>
/// <reference path="./jquery.elang.search.ts"/>
var ELang;
(function (ELang) {
    var ELangEditEvents = (function () {
        function ELangEditEvents() {
            this.insert = new jQuery.Deferred();
            this.modify = new jQuery.Deferred();
            this.remove = new jQuery.Deferred();
            this.select = new jQuery.Deferred();
        }
        return ELangEditEvents;
    })();    
    var ELangEditDelegates = (function () {
        function ELangEditDelegates() {
            this.insertCallback = null;
            this.insertHandler = null;
            this.modifyCallback = null;
            this.modifyHandler = null;
            this.removeCallback = null;
            this.removeHandler = null;
            this.selectHandler = null;
            this.selectCallback = null;
            this.btnAddHandler = null;
            this.btnAddClickHandler = null;
        }
        return ELangEditDelegates;
    })();    
    var ELangEditDefaults = (function (_super) {
        __extends(ELangEditDefaults, _super);
        function ELangEditDefaults() {
                _super.call(this);
            this.headLabel = "lblEditHead";
            this.resultHeadLabel = "lblEditedExpressionsHead";
            this.editFormHtml = '<form class="form-search"><div class="controls controls-row"></div></form>';
            this.editFieldHtml = '<input class="input-large span2" type="text" placeholder="-" />';
            this.addButtonHtml = '<button type="submit" class="btn btn-primary span0"><span></span></button>';
            this.addButtonLabel = "lblAdd";
            this.editKeyLabel = "lblEditKeyField";
            this.editValueLabel = "lblEditValueField";
        }
        return ELangEditDefaults;
    })(ELang.ELangBaseDefaults);
    ELang.ELangEditDefaults = ELangEditDefaults;    
    var ELangEdit = (function (_super) {
        __extends(ELangEdit, _super);
        function ELangEdit() {
                _super.call(this);
            this.name = "eLang-Edit";
            this.description = "eLang - Language Learning edit functionality.";
            this.defaults = new ELangEditDefaults();
            this.delegates = new ELangEditDelegates();
            this.events = new ELangEditEvents();
        }
        ELangEdit.prototype.initialize = function (target, options) {
            _super.prototype.initialize.call(this, target, options);
            this.delegates.insertHandler = jQuery.proxy(this._onInsert, this);
            this.delegates.modifyHandler = jQuery.proxy(this._onModify, this);
            this.delegates.removeHandler = jQuery.proxy(this._onRemove, this);
            this.delegates.selectHandler = jQuery.proxy(this._onSelect, this);
            this.delegates.btnAddHandler = jQuery.proxy(this._onAddClick, this);
            this.delegates.insertCallback = jQuery.proxy(this._onInsertCallback, this);
            this.delegates.modifyCallback = jQuery.proxy(this._onModifyCallback, this);
            this.delegates.removeCallback = jQuery.proxy(this._onRemoveCallback, this);
            this.delegates.selectCallback = jQuery.proxy(this._onSelectCallback, this);
            this.events.insert.done(this.delegates.insertHandler);
            this.events.modify.done(this.delegates.modifyHandler);
            this.events.remove.done(this.delegates.removeHandler);
            this.events.select.done(this.delegates.selectHandler);
            var handlerAdd = this.delegates.btnAddHandler;
            this.delegates.btnAddClickHandler = function () {
                var srcE = this;
                var fields = srcE.parentNode["getElementsByTagName"]("input");
                var key = fields[0];
                var value = fields[1];
                handlerAdd(key, value);
            };
            this.createContent();
            this.element.data("elang-edit", jQuery.proxy(this.processCommand, this));
        };
        ELangEdit.prototype.createContent = function () {
            _super.prototype.createContent.call(this);
            var contentDiv = this.element.next("div");
            var resultSelector = "." + this.defaults.resultCSS.split(" ")[0];
            var result = contentDiv.find("*").filter(resultSelector);
            // edit panel
            var form = jQuery(this.defaults.editFormHtml);
            var keyField = jQuery(this.defaults.editFieldHtml);
            var valueField = jQuery(this.defaults.editFieldHtml);
            var add = jQuery(this.defaults.addButtonHtml);
            var langid = ELang.ELangCommon.resource.selectedLang;
            var labelKey = ELang.ELangCommon.getLabel(this.defaults.editKeyLabel);
            var labelValue = ELang.ELangCommon.getLabel(this.defaults.editValueLabel);
            var formIn = this.getLastChild(form);
            keyField.add(keyField.find("*")).filter("input[placeholder]").attr("id", this.defaults.editKeyLabel).attr("placeholder", labelKey);
            valueField.add(valueField.find("*")).filter("input[placeholder]").attr("id", this.defaults.editValueLabel).attr("placeholder", labelValue);
            add.add(add.find("*")).filter("span").attr("id", this.defaults.addButtonLabel);
            add.click(this.delegates.btnAddClickHandler);
            formIn.append(keyField);
            formIn.append(valueField);
            formIn.append(add);
            result.before(form);
            // set labels
            ELang.ELangCommon.setLang(langid, contentDiv);
        };
        ELangEdit.prototype._onAddClick = function (key, value) {
            //TODO validate fields
            //TODO update database
            //TODO refresh list if required
                    };
        ELangEdit.prototype._onInsert = function () {
        };
        ELangEdit.prototype._onModify = function () {
        };
        ELangEdit.prototype._onRemove = function () {
        };
        ELangEdit.prototype._onSelect = function () {
        };
        ELangEdit.prototype._onInsertCallback = function () {
        };
        ELangEdit.prototype._onModifyCallback = function () {
        };
        ELangEdit.prototype._onRemoveCallback = function () {
        };
        ELangEdit.prototype._onSelectCallback = function () {
        };
        ELangEdit.prototype._insert = function () {
            this.events.insert.resolve();
        };
        ELangEdit.prototype._modify = function () {
            this.events.modify.resolve();
        };
        ELangEdit.prototype._remove = function () {
            this.events.remove.resolve();
        };
        ELangEdit.prototype._select = function () {
            this.events.select.resolve();
        };
        return ELangEdit;
    })(ELang.ELangBase);
    ELang.ELangEdit = ELangEdit;    
})(ELang || (ELang = {}));
(function (jQuery) {
    jQuery.fn.elangEdit = function (options, command) {
        var result = new ELang.FnJQuery(this, options, command, "ELangEdit", "elang-edit");
        return result;
    };
})(jQuery);
//@ sourceMappingURL=jquery.elang.edit.js.map
