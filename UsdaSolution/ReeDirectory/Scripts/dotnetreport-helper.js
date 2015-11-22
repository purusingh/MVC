/// .Net Report Builder helper methods

// Ajax call wrapper function
function ajaxcall(options) {
    if ($.blockUI) {
        $.blockUI({ baseZ: 500 });
    }

    return $.ajax({
        url: options.url,
        type: options.type || "GET",
        data: options.data,
        cache: options.cache || false,
        dataType: options.dataType || "json",
        contentType: options.contentType || "application/json; charset=utf-8",
        headers: options.headers || {}
    }).success(function (data) {
        if ($.unblockUI) {
            $.unblockUI();
        }
        delete options;
    }).fail(function (jqxhr, status, error) {
        if ($.unblockUI) {
            $.unblockUI();
        }
        delete options;
        var msg = jqxhr.responseJSON && jqxhr.responseJSON.Message ? "\n" + jqxhr.responseJSON.Message : "";

        if (error == "Conflict") {
            alertify.error("Conflict detected. Please ensure the record is not a duplicate and that it has no related records." + msg);
        } else if (error == "Bad Request") {
            alertify.error("Validation failed for your request. Please make sure the data provided is correct." + msg);
        } else if (error == "Unauthorized") {
            alertify.error("You are not authorized to make that request." + msg);
        } else if (error == "Forbidden") {
            location.reload(true);
        } else if (error == "Not Found") {
            alertify.error("Record not found." + msg);
        } else if (error == "Internal Server Error") {
            alertify.error("The system was unable to complete your request." + msg);
        } else {
            alertify.error(status + ": " + msg);
        }
    });
}

// knockout binding extenders
ko.bindingHandlers.datepicker = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        //initialize datepicker with some optional options
        var options = allBindingsAccessor().datepickerOptions || {};
        $(element).datepicker(options);

        //handle the field changing
        ko.utils.registerEventHandler(element, "change", function () {
            var observable = valueAccessor();
            observable($(element).datepicker("getDate"));
        });

        //handle disposal (if KO removes by the template binding)
        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $(element).datepicker("destroy");
        });

    },
    //update the control when the view model changes
    update: function (element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor()),
            current = $(element).datepicker("getDate");

        if (value - current !== 0) {
            $(element).datepicker("setDate", value);
        }
    }
};


ko.bindingHandlers.fadeVisible = {
    init: function (element, valueAccessor) {
        // Initially set the element to be instantly visible/hidden depending on the value
        var value = valueAccessor();
        $(element).toggle(ko.utils.unwrapObservable(value)); // Use "unwrapObservable" so we can handle values that may or may not be observable
    },
    update: function (element, valueAccessor) {
        // Whenever the value subsequently changes, slowly fade the element in or out
        var value = valueAccessor();
        ko.utils.unwrapObservable(value) ? $(element).fadeIn("slow") : $(element).hide();
    }
};