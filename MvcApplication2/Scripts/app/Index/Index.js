/*This file will contain all the javascript and jquery functions for Index.cshtml. 
Keeping the functions will help with organization and will allow us to create classes with collections of javascript functions and that way we can load those classes, 
which will optimize page load time. 
*/

//This function disables submit button 
$(document).ready(
                function () {
                    $('input:submit').attr('disabled', true);
                    $('input:file').change(
                        function () {
                            if ($(this).val()) {
                                $('input:submit').removeAttr('disabled');
                            }
                            else {
                                $('input:submit').attr('disabled', true);
                            }
                        });
                });
$(document).ready(
                function () {
                    var tId = set(function (event) {
                        //event.preventDefault();
                        $.getJSON('home/partialpage/').complete(function (evt) {

                            var data = evt.responseJSON; var $progress = $(".progress-bar");
                            //$progress.parent().width(data.PercentComplete);
                            $progress.width(data.PercentComplete);
                            $progress.text(data.StatusMessage);
                        });

                    }, 1000);
                });