


function requestInput(DatasInput) {

    var _data = {};
    $(DatasInput).each(function () {

        var objInfo = $(this);

        if (objInfo.prop("tagName") == "TEXTAREA") {
            _data[objInfo.attr("id")] = objInfo.val();
            
        } else if (objInfo.attr("type") == "text" || objInfo.attr("type") == "hidden" || objInfo.attr("type") == "textarea") {
            _data[objInfo.attr("id")] = objInfo.val();
        }

    });

    return _data;
}