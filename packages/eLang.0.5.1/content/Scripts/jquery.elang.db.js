var ELang;
(function (ELang) {
    var ELangDBEvents = (function () {
        function ELangDBEvents() {
            this.select = new jQuery.Deferred();
            this.insert = new jQuery.Deferred();
            this.modify = new jQuery.Deferred();
            this.remove = new jQuery.Deferred();
        }
        return ELangDBEvents;
    })();    
    var ELangDBDelegates = (function () {
        function ELangDBDelegates() {
        }
        return ELangDBDelegates;
    })();    
    var ELangDBOptions = (function () {
        function ELangDBOptions() {
            this.autocompleteRows = 5;
        }
        return ELangDBOptions;
    })();    
    var ELangDB = (function () {
        function ELangDB() {
            this.cache = {
            };
            this.isInitialized = false;
            this.name = "elang-DB";
            this.description = "Html5 localstorage based instance.";
        }
        ELangDB._instance = null;
        ELangDB._syncLock = {
            isLocked: false
        };
        ELangDB.prototype.initialize = function (options) {
            if (typeof options === "undefined") { options = null; }
            this.options = new ELangDBOptions();
            this.setOptions(options);
            this.events = new ELangDBEvents();
            this.delegates = new ELangDBDelegates();
            this.delegates.selectHandler = jQuery.proxy(this._onSelect, this);
            this.delegates.insertHandler = jQuery.proxy(this._onInsert, this);
            this.delegates.modifyHandler = jQuery.proxy(this._onModify, this);
            this.delegates.removeHandler = jQuery.proxy(this._onRemove, this);
            this.events.select.done(this.delegates.selectHandler);
            this.events.insert.done(this.delegates.insertHandler);
            this.events.modify.done(this.delegates.modifyHandler);
            this.events.remove.done(this.delegates.removeHandler);
            this.isInitialized = true;
        };
        ELangDB.prototype._onSelect = function (id, callback) {
            if(jQuery.isFunction(callback)) {
                callback();
            }
        };
        ELangDB.prototype._onInsert = function (id, value, callback) {
            if(jQuery.isFunction(callback)) {
                callback();
            }
        };
        ELangDB.prototype._onModify = function (id, value, callback) {
            if(jQuery.isFunction(callback)) {
                callback();
            }
        };
        ELangDB.prototype._onRemove = function (id, callback) {
            if(jQuery.isFunction(callback)) {
                callback();
            }
        };
        ELangDB.prototype.select = function (id, callback) {
            this.events.select.resolve(id, callback);
        };
        ELangDB.prototype.insert = function (id, value, callback) {
            this.events.insert.resolve(id, value, callback);
        };
        ELangDB.prototype.modify = function (id, value, callback) {
            this.events.modify.resolve(id, value, callback);
        };
        ELangDB.prototype.remove = function (id, callback) {
            this.events.remove.resolve(id, callback);
        };
        ELangDB.prototype.sort = function () {
        };
        ELangDB.prototype.setOptions = function (options) {
            if (typeof options === "undefined") { options = null; }
            if(options) {
                jQuery.extend(true, this.options, options);
            }
        };
        ELangDB.prototype.getIndexHash = function (id) {
            return "";
        };
        ELangDB.getInstance = function getInstance(options) {
            if (typeof options === "undefined") { options = null; }
            if(!ELangDB._instance) {
                if(ELangDB._syncLock.isLocked) {
                    var i = 0;
                    while(ELangDB._syncLock.isLocked) {
                        i = 1;
                    }
                }
                if(!ELangDB._instance) {
                    ELangDB._syncLock.isLocked = true;
                    ELangDB._instance = new ELangDB();
                    ELangDB._instance.initialize(options);
                    ELangDB._syncLock.isLocked = false;
                } else {
                    ELangDB._instance.setOptions(options);
                }
            } else {
                ELangDB._instance.setOptions(options);
            }
            return ELangDB._instance;
        };
        ELangDB.prototype.getOptions = function () {
            return this.options;
        };
        return ELangDB;
    })();
    ELang.ELangDB = ELangDB;    
})(ELang || (ELang = {}));
