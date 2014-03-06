/*This file will contain all the javascript and jquery functions for Index.cshtml. 
Keeping the functions will help with organization and will allow us to create classes with collections of javascript functions and that way we can load those classes, 
which will optimize page load time. 
*/



(function () {
    'use strict';
    (function () {
        var flatMapArrayAugmentation = function (collectionSelector, elementProjection) {
            /// <signature>
            /// <summary> flattens an Array of collections into a single array.</summary>
            /// <param name='collectionSelector' type='Function'> A function to transform each element in the array into an array.</param> 
            /// <param name='elementProjection' type='Function'> An optional function to perform an additional transformation on each element in the flattened collection. </param>
            /// <returns type='Array'> All elements of each collection flattned onto a single array.</returns>
            /// </signature>
            var i, results = [];
            for (i = 0; i < this.length; i += 1) {
                var col = collectionSelector ? collectionSelector(this[i]) : this[i];
                col.forEach(function (element, index) {
                    results.push(element);
                });
            }
            return elementProjection ? results.map(elementProjection) : results;
        };

        var correlate = function (inner, outerKeySelector, innerKeySelector, resultSelector) {
            /// <signature>
            /// <summary>
            /// Correlates the elements from two sequences based on the strict (===) equality of the keys selected from each element
            /// by the provided selector functions. Models the semantics of a relational inner join.
            /// </summary>
            /// <param name= 'inner' type='Array'> An array whose elements will be correlated with the source array.</param> 
            /// <param name= 'outerKeySelector' type='Function'>
            /// <type> outerElement => key </type> 
            /// A function to produce a key value from each element in the outer array.</param>
            /// <param name= 'innerKeySelector' type='Function'>
            /// <type> innerElement => key </type>
            /// A function to produce a key value from each element in the inner array.</param>
            /// <param name = 'resultSelector' type = 'Function'>
            /// <type> (outerElement, innerElement) => resultElement</type> 
            /// An optional function to transform each pair.</param>
            /// <returns type='Array'>An array of tuple like elements containing all pairs of elements with equal keys.
            /// </returns>
            /// </signature>
            var outerKeyed, innerKeyed, i, j, results;
            outerKeyed = this.map(function (e) { return { element: e, key: outerKeySelector(e) }; });
            innerKeyed = inner.map(function (e) { return { element: e, key: innerKeySelector(e) }; });
            results = [];
            for (i = 0; i < outerKeyed.length; i += 1) {
                for (j = 0; j < innerKeyed.length; j += 1) {
                    if (outerKeyed[i].key === innerKeyed[j].key)
                        results.push({ first: outerKeyed[i].element, second: innerKeyed[j].element });
                }
            }
            // If a function was supplied, map it over the array of result pairs and return the result.
            // otherwise, just return the array of result pairs.
            return resultSelector ? results.map(function (e) { return resultSelector(e.first, e.second); }) : results;

        };
        /// adds the flatMap function to  Array.prototype, endowing all Arrays instances with it.
        /// flatMap takes an array of items, projects each one to an array,using the provided collectionSelector function,
        /// and flattens the resulting array of arrays onto a single flat array. 
        /// A second, optional function performs a projection on each element of the resulting flat array.
        /// This function is analagous to SelectMany in Linq, and flatMap in Scala. 

        Array.prototype.flatMap = Array.prototype.flatMap || flatMapArrayAugmentation;


        Array.prototype.correlate = Array.prototype.correlate || correlate;
    }());
}());


//This function disables submit button 
$(document).ready(function () {
    'use strict';
    $('input:submit').attr('disabled', true);
    $('input:file').change(function () {
        if ($(this).val()) {
            $('input:submit').removeAttr('disabled');
        } else {
            $('input:submit').attr('disabled', true);
        }
    });
});




$("input:submit").click(function (e) {
    'use strict';// All top level functions should start with this directive. nested functions inherit it.
    //e.preventDefault();

    $('input:file').each(function (index, element) {
        var Files = $('input:file').toArray().flatMap(function (item) {
            var result = [], i; for (i = 0; i < item.files.length; i += 1) {
                result.push(item.files[i]);
            } return result;
        });
        $.ajax({
            url: '/Home/Upload/',
            type: "POST",
            data: Files,
            content: Files,
        });

    });
});
var jobIds = (function () {
    var serverJobs = [];

    $(document).ready(function () {
        'use strict';
        $.ajaxSetup({ cache: false });
        (function () {
            'use strict';
            var jobId = setInterval(function (event) {
                $.getJSON('\\Home\\GetJobStatus?jobId=' + jobId).success(function (data) {

                    var $progress = $(".progress-bar");
                    $progress.width(data.Percent);
                    $progress.text(data.Message);
                });
            }, 1000);
            serverJobs.push(jobId);
        });
        return serverJobs;
    });
}());
