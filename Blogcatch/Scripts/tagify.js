/*
* Tagify jQuery Blog tagging plugin
* 
*       Created by: Rawa Jalal
*       Date : August 23 / 2017
*
*/
(function ($) {
    /*
    * Tagify methods
    */
    var methods = {
        /*
        * Initialization
        */
        Init: function () {
            var input = $(
                '<input type="text" class="form-control" id="rj-tag-input">'
            );
            var tagBox = $('<div id="rj-tag-box"></div>');
            $(this).append(input);
            $(this).append(tagBox);
        },
        /*
        * Returns all of the tags as a JsonArray and appends the values as a hidden field in a form
        */
        InsertJsonArrayToForm: function (formID, hiddenInputName, debugMode = false) {
            $("#" + formID).submit(function () {
                var spanTags = $("#rj-tag-box span.tag");
                var tagText = [];

                for (var i = 0; i < spanTags.length; i++) {
                    tagText.push($(spanTags).eq(i).text());
                }
                var JSONTags = JSON.stringify(tagText);

                if (document.getElementById(hiddenInputName)) {
                    $("#" + hiddenInputName).val(JSONTags);
                } else {
                    var hiddenInput = $('<input />')
                        .attr('type', 'hidden')
                        .attr('name', hiddenInputName)
                        .attr('id', hiddenInputName)
                        .val(JSONTags);
                    $("#" + formID).append(hiddenInput);
                }
                if (debugMode === true) {
                    alert("The values in the hidden field: " + hiddenInputName + " is: " + $("#" + hiddenInputName).val());
                }
            })
        }
    }

    $.fn.tagify = function (methodOrOptions) {

        /*
        * Method call logic
        */
        if (methods[methodOrOptions]) {
            methods[methodOrOptions].apply(this, Array.prototype.slice.call(arguments, 1));
        } else if (typeof methodOrOptions === 'object' || !methodOrOptions) {
            // Default to "init"
            methods.Init.apply(this);
        } else {
            $.error('Method ' + methodOrOptions + ' does not exist on jQuery.tagify');
        }
        /*
        * When user presses enter on the input, it will trigger 'CreateTag' function and send the value of the
        * input as a parameter
        */
        document.getElementById("rj-tag-input").onkeypress = function (e) {
            if (!e) e = window.event;
            var keyCode = e.keyCode || e.which;
            if (keyCode == '13') {
                var tagText = this.value.trim();
                if (tagText.length > 0) {
                    CreateTag(tagText);
                    $("#rj-tag-input").val("");
                }
            }
        }

        /*
        * Create tag and append to the tag-box div
        */
        function CreateTag(text) {
            var newTag = $('<span />').addClass("label label-primary tag").append($(
                '<span />').html(text));
            var removeButton = $(
                '<a class="rj-js-tag-delete"><i class="fa fa-times fa-1" aria-hidden="true"></i></a>'
            );
            $(newTag).append(removeButton);
            $("#rj-tag-box").append(newTag);
        }

        /*
        * Delete tag
        */
        $(this).on("click", "a.rj-js-tag-delete", function () {
            $(this).parents(".tag").first().remove();
        })
    }

})(jQuery);