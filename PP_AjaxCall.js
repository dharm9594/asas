function saveSearchedword(Data) {
    var params = '{"Data":"' + Data.toString() + '"}';
    $.ajax({
        url: "Home.aspx/saveSearchedword",
        data: params,
        type: "POST",
        cache: false,
        contentType: "application/json;charset=utf-8",
        success: resultresponce,
        error: function (errormessage) {
            // $("#progress").hide();
        }
    });
}

function removeWordSearch(Data) {
    debugger;
    var params = '{"Data":"' + Data.toString() + '"}';
    $.ajax({
        url: "Home.aspx/DeleteSearchedword",
        data: params,
        type: "POST",
        cache: false,
        contentType: "application/json;charset=utf-8",
        success: resultresponce,
        error: function (errormessage) {
        }
    });
}
function selectWordSearch() {
    $.ajax({
        url: "Home.aspx/SelectSearchedword",
        type: "POST",
        cache: false,
        contentType: "application/json;charset=utf-8",
        success: resultresponce,
        error: function (errormessage) {
        }
    });
}
function getFavorites() {
    $.ajax({
        url: "Home.aspx/getFavorites",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (response) {
            onsuccessFav_NewUpld(response.d, 'divFavourites');
        },
        error: function (errormessage) {
            console.log(errormessage);
            $("#progress").hide();
        }
    });
}

function getNew() {
    $.ajax({
        url: "Home.aspx/getNew",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (response) {
            onsuccessFav_NewUpld(response.d, 'newPortal');
        },
        error: function (errormessage) {
            console.log(errormessage);
            $("#progress").hide();
        }
    });
}

function getByID(id) {
    debugger;
    $("#progress").show();
    var sortBy = $("#ddlsortBy").val();
    var arrange = $("#ddlArrange").val();
    var params = '{"fileID":"' + id.toString() + '" , "sortBy":"' + sortBy.toString() + '" , "arrange":"' + arrange.toString() + '"}';
    var MoreFilter = $('#hdnMoreFilter').val();
    var urls = '';
    if (MoreFilter == "Image" || MoreFilter == "Video") {
        urls = "Home.aspx/getImageVideoByID";
    }
    else {
        urls = "Home.aspx/getGCFiledataByID";
    }
    $.ajax({
        url: urls,
        data: params,
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: GetResultbyID,
        error: function (errormessage) {
            console.log(errormessage);
            $("#progress").hide();
        }
    });
    $("#progress").hide();
}

function popupImage_Video(fileid) {

    var params = '{"fileID":"' + fileid.toString() + '"}';
    $.ajax({
        url: "Home.aspx/getBCFile_Detailsdata_ByID",
        data: params,
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: GetresultBCFileDetailsByID,
        error: function (errormessage) {
            console.log(errormessage);
            $("#progress").hide();
        }
    });
}

function popupDoc(fileid) {

    var params = '{"fileID":"' + fileid.toString() + '"}';
    $.ajax({
        url: "Home.aspx/getGCFile_Detailsdata_ByID",
        data: params,
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: GetresultGCFileDetailsByID,
        error: function (errormessage) {
            console.log(errormessage);
            $("#progress").hide();
        }
    });
}


function DeleteFileFromCart(delfile)
{
    debugger;
    var params = '{"fileID":"' + delfile.dataset.fileid + '"}';

    var Type = delfile.dataset.type;

    var Url = "";
    if (Type == "Media")
    {
        Url = "Home.aspx/BCDeleteCartFile";
    }
    else
    {
        Url = "Home.aspx/DeleteCartFile";
    }

    $.ajax({
        url: Url,
        data: params,
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != -1) {

                alert("File Removed from the cart");

            }

        },
        error: function (response) {
            alert(response.responseText);
        },
        failure: function (response) {
            alert(response.responseText);
        }

    });
}

function SendFeedback(feedbck)
{
    debugger;
    var FeedbackVal = $('#txtFeedback').val();

    var params = '{"FileID":"' + feedbck.dataset.fileid + '", "FeedbackVal":"' + FeedbackVal + '"}';

    $.ajax({
        url: "Home.aspx/SendFeedback",
        data: params,
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != -1) {
                $("#txtFeedback").val('');

                alert("Thank you for your feedback, we will deal with the matter you raise and revert for clarification if necessary.");

            }

        },
        error: function (response) {
            alert(response.responseText);
            hideLoading();
        },
        failure: function (response) {
            alert(response.responseText);
        }
      
    });

}


//Documents Add to cart
var anchorHref;
$(document).on('click', ".genevent", function (e) {
    anchorHref = e.currentTarget.href;
    var AddToCartDetails = {
        ID: e.currentTarget.dataset.id,
        FileID: e.currentTarget.dataset.fileid,
        FileName: e.currentTarget.dataset.filename,
        FilePath: e.currentTarget.dataset.filepath,
    }

    $.ajax({

        type: "POST",
        url: "Home.aspx/AddToCart",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ 'AddToCart': AddToCartDetails }), // Check this call.
        success: function (data) {
            if (data.d == -1) {
                alert("This file is already in your cart..");
            }
            else {
                alert("File successfully added to your cart ");
                AddtoCartData();
            }

        },
        error: function (response) {
            alert(response.responseText);
            hideLoading();
        },
        failure: function (response) {
            alert(response.responseText);
        }
    });
    getCartDetails();
});

//Documents Add to favourites
function ArticleFavorite(favorite, IsPreview) {
    var ArticleFavorite = {
        FileID: favorite.dataset.fileid,
        FileName: favorite.dataset.filename,
        FilePath: favorite.dataset.filepath,
    }
    var actionUrl = "";
    if (favorite.title == 'Added to wishlist') {
        actionUrl = "Home.aspx/ArticleFavoriteLogs";
    }
    else {
        actionUrl = "Home.aspx/ArticleFavoriteLogs";
    }

    $.ajax({
        type: "POST",
        url: "Home.aspx/ArticleFavoriteLogs",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ 'FavoriteLogs': ArticleFavorite }), // Check this call.
        success: function (data) {
            getFavorites();
            getNew();
            var html = '';
            html = html + '<svg class="star' + (data.d == "Added" ? (IsPreview ? '' : ' active') : (IsPreview ? ' active' : '')) + '" width="22" height="20" viewBox="0 0 22 25">\n';
            html = html + '<use href="../../Content/custom_icons/starIcon.svg#star"></use>\n';
            html = html + '</svg>\n';
            favorite.title = (data.d == "Added" ? 'Remove From Favourites' : 'Add To Favourites');
            favorite.innerHTML = html;
            alert((data.d == "Added" ? 'File added to favourites' : 'File remove from favourites'));
        },
        error: function (response) {
            alert(response.responseText);
        },
        failure: function (response) {
            alert(response.responseText);
        }
    });
}

function getCartDetails() {
    $.ajax({
        url: "Home.aspx/getCartDetails",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (response) {
            onSuccessCart(response.d);
        },
        error: function (errormessage) {
            console.log(errormessage);
            $("#progress").hide();
        }
    });
}