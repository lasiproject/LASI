var LASI = require('app/LASI');
LASI.enableActiveHighlighting = function () {
    'use strict';
    var enableActiveHighlighting = function () {
        var phrasalTextSpans = $('span.phrase');
        var highlightClass = 'active-phrase-highlight';
        var recolor = function () {
            phrasalTextSpans.each(function () {
                $(this).removeClass(highlightClass);
            });
            $(this).addClass(highlightClass);
        };
        phrasalTextSpans.click(recolor);
        phrasalTextSpans.on('contextmenu', recolor);
        // bootstrap requires that tooltips be manually enabled. The data-toggle="tooltip" attributes set on each element
        // have no effect without this or an equivalent call. By setting container to 'body', we allow the contents of the 
        // tooltip to overflow its container. This is generally close to the desired behavior as it is difficult to predict width
        // and this gives good flexibility. There is probably a cleaner and more precise/obvious way of doing this, change to that if discovered.
        $('[data-toggle="tooltip"]').tooltip({
            delay: 250,
            container: 'body'
        });
        // TODO: look into fixing tooltips on elements containing a line break or remove such breaks.
    };
    $(enableActiveHighlighting);
    return enableActiveHighlighting;
};
