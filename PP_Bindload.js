

window.onload = function () {
    selectWordSearch();
    getFavorites();
    getNew();
    getCartDetails();
};

$(document).ready(function () {
    $("#txtSearch").on('keyup', function (e) {

        if (e.keyCode === 13) {
            if (valdateSearch()) {
                $("#progress").show();
                $('#forGrid').empty();
                $('#forList').empty();
                var value = $("#txtSearch").val();
                startIndex = 0
                saveSearchedword(value);
                bindAPI(value);
            }
        }
    });
});