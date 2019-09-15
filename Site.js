
function SearchedWordClicked(value) {
    debugger;
    $("#txtSearch").val(value);
    $('#forGrid').empty();
    $('#forList').empty();
    startIndex = 0
    saveSearchedword(value);
    bindAPI(value);

}

function fireChange() {
    if (valdateSearch()) {

        $("#progress").show();
        var value = $("#txtSearch").val();
        saveSearchedword(value);
        startIndex = 0;
        bindAPI(value);
        $('#forGrid').empty();
        $('#forList').empty();
        $("#progress").hide();
    }
}

function valdateSearch() {
    var value = $("#txtSearch").val();
    if (value == "") {
        alert('Kindly refine your search!');
        $("#progress").hide();
        $('#forGrid').empty();
        $('#forList').empty();
        $('#spnTotalNumFound').html('0');

        return false;

    }
    return true;
}

function togglePreview(id, elementId) {
    $('.preview').each(function () {
        $(this).css("display", "none");
    });
    $('.previewIcon').each(function () {
        $(this).attr("class", "redIcon_SVG previewIcon notSelect");
    });
    var element = document.getElementById(elementId);
    element.classList.remove('notSelect');
    document.getElementById(id).style.display = "block";
}

function addSelectedClass(element) {
    if (valdateSearch()) {
        $("#progress").show();
        var selectedtype = element.innerHTML;
        $('#hdnMoreFilter').val(selectedtype);
        $('.chipButtonInner').each(function () {
            $(this).removeClass("selectBlack");
        });
        $('.autoHide').each(function () {
            $(this).css("display", "none");
        });
        element.classList.add('selectBlack');
        $('#forGrid').empty();
        $('#forList').empty();
        startIndex = 0;
        var value = $("#txtSearch").val();
        bindAPI(value);
    }
}

function toggleDiv(id, element) {
    if (id.trim().toLowerCase() != 'morefilters') {
        if (valdateSearch()) {
            $("#progress").show();
            if (element.classList.contains('selectBlack')) {
                $('#hdnMoreFilter').val('');
                document.getElementById(id).style.display = "none";
                element.classList.remove('selectBlack');
                $("input[name='docFilterchecked']:checked").prop('checked', false);
            }
            else {
                var selectedtype = element.innerHTML;
                $('#hdnMoreFilter').val(selectedtype);
                $('.autoHide').each(function () {
                    $(this).css("display", "none");
                });
                $('.chipButton').each(function () {
                    $(this).removeClass("selectBlack");
                });
                $('.chipButtonInner').each(function () {
                    $(this).removeClass("selectBlack");
                });
                if (document.getElementById(id).style.display == "block") {
                    document.getElementById(id).style.display = "none";
                    element.classList.remove('selectBlack');
                }
                else {
                    document.getElementById(id).style.display = "block";
                    element.classList.add('selectBlack');
                }
            }

            startIndex = 0;
            $('#forGrid').empty();
            $('#forList').empty();
            var value = $("#txtSearch").val();
            saveSearchedword(value);
            bindAPI(value);
        }
    }
    else {
        if (document.getElementById(id).style.display == "block") {
            document.getElementById(id).style.display = "none";
            element.innerHTML = "<i class='fa fa-eye pl-2 pr-2'></i>Show Filters";
            element.classList.remove("selectBlack");
            $('.scrollContainer').css('max-height', '32rem');
        }
        else {
            document.getElementById(id).style.display = "block";
            element.classList.add("selectBlack");
            element.innerHTML = "<i class='fa fa-eye-slash pl-2 pr-2'></i>Hide Filters";
            $('.scrollContainer').css('max-height', '24.5rem');
        }
        $("#progress").hide();
    }

}

function hideNav(event) {
    if (event.keyCode == 27) {
        $('.overlayMenu').each(function () {
            $(this).css("height", "0");
        });
        $('body').css('overflow-y', 'auto');
        $('#txtSearch').val('');
        $('#forList').html('');
        $('#forGrid').html('');
        $('#txtSearch').blur();
        document.getElementById('moreFilters').style.display = "none";
        document.getElementById('badgeMoreFilters').innerHTML = "<i class='fa fa-eye pl-2 pr-2'></i>Show Filters";
        document.getElementById('badgeMoreFilters').classList.remove("selectBlack");
        $('.scrollContainer').css('max-height', '32rem');
    }
}

function closeNav(id) {
    document.getElementById(id).style.height = "0%";
    $('body').css('overflow-y', 'auto');
    if (id.trim().toLowerCase() == "searchbar") {
        $('#txtSearch').val('');
        $('#forList').html('');
        $('#forGrid').html('');
        $('#txtSearch').blur();
        document.getElementById('moreFilters').style.display = "none";
        document.getElementById('badgeMoreFilters').innerHTML = "<i class='fa fa-eye pl-2 pr-2'></i>Show Filters";
        document.getElementById('badgeMoreFilters').classList.remove("selectBlack");
        $('.scrollContainer').css('max-height', '32rem');
    }
}

function clearNav(id) {
    $('#' + id).remove();
}

function openNav(id) {
    $('.isOpen').each(function () {
        $(this).css("height", "0");
        $(this).removeClass("isOpen");
    });
    $('body').css('overflow-y', 'hidden');
    document.getElementById(id).style.height = "100%";
    document.getElementById(id).classList.add("isOpen");
    if (id.trim().toLowerCase() == "searchbar") {
        document.getElementById('txtSearch').focus();
    }
}

function convertDate(inputFormat) {
    function pad(s) { return (s < 10) ? '0' + s : s; }
    var d = new Date(inputFormat);
    return [pad(d.getDate()), pad(d.getMonth() + 1), d.getFullYear()].join('/');
}

function switchTabs(id, element) {
    $('.portalPreview').each(function () {
        $(this).css("display", "none");
    });
    $('.tabButtons').each(function () {
        $(this).removeClass("customRed");
        $(this).removeClass("setBold");
    });
    $('.liInline').each(function () {
        $(this).removeClass("active");
    });
    (element.parentElement).classList.add('active');
    element.classList.add('customRed');
    element.classList.add('setBold');
    document.getElementById(id).style.display = "block";
}

function showMore(id) {
    document.getElementById(id).style.display = "none";
    $('.top6').each(function () {
        $(this).css('display', 'block');
    });
}


function toggleDetails(preview, details, element) {
    if (document.getElementById(preview).style.display == "none") {
        document.getElementById(preview).style.display = "block";
        document.getElementById(details).style.display = "none";
        element.innerHTML = "View Details";
    }
    else {
        document.getElementById(preview).style.display = "none";
        document.getElementById(details).style.display = "block";
        element.innerHTML = "View Preview";
    }
    return false;
}

function limitChar(chartext,limit)
{
    var textLength = chartext.length;
    var text = "";
    if (textLength > limit) {
        text = chartext.substring(0, limit) + "...";
    }
    else {
        text = chartext;
    }
    return text;
}


function ClearAll()
{
    $('#txtSearch').val('');
    $('#forList').html('');
    $('#forGrid').html('');
    $('#txtSearch').blur();
    document.getElementById('moreFilters').style.display = "none";
    document.getElementById('badgeMoreFilters').innerHTML = "<i class='fa fa-eye pl-2 pr-2'></i>Show Filters";
    document.getElementById('badgeMoreFilters').classList.remove("selectBlack");
    $('.autoHide').each(function () {
        $(this).css("display", "none");
    });
    $('.scrollContainer').css('max-height', '32rem');
    $('#hdnMoreFilter').val('');
    $('.chipButtonInner').each(function () {
        $(this).removeClass("selectBlack");
    });
    $("input[name='optradio']:first").prop('checked', true);
    $("input[name='docFilterchecked']:checked").prop('checked', false);
}

function downloadAll() {
    $('.singleDownload').each(function () {
        window.open($(this).href);
    });
    return false;
}

function isNull(text) {
    return (text == null || text.toLowerCase() == 'null' ? '' : text);
}

function navigate(id) {
    window.location.href = "GCBCFolder.aspx?Type=" + id;
}

function sideNav(id, element) {
    debugger;
    var html = "";
    if (document.getElementById(id).style.display == "none") {
        document.getElementById(id).style.display = "block";
        html = '<span class="p-0 display-4 fullWidth ml-2 rounded-left" style="top: 0vh; left: 1vw;"><b>Portal</b></span><span href="javascript:void(0);" class="closePortal">&times;</span>';
        document.getElementById('smallCol').classList.remove('smallCol');
        $('#smallCol').parent().attr('class', 'col-4 col-sm-3 col-md-4 col-lg-2 col-xl-2 p-0');
        $('#smallCol').parent().siblings().closest('div').attr('class', 'col-8 col-sm-9 col-md-8 col-lg-10 col-xl-10 pt-2 pb-2 scrollDiv');
    }
    else {
        document.getElementById(id).style.display = "none";
        html = '<svg width="20" height="20" viewBox="0 0 30 27"><use class="hamburger_Lines" href="./custom_icons/hamburgerIcon.svg#hamburger_Lines"></use></svg>';
        document.getElementById('smallCol').classList.add('smallCol');
        $('#smallCol').parent().attr('class', 'col-1 col-sm-1 col-md-1 col-lg-1 col-xl-1 p-0')
        $('#smallCol').parent().siblings().closest('div').attr('class', 'col-11 col-sm-11 col-md-11 col-lg-11 col-xl-11 pt-2 pb-2 scrollDiv');
    }
    element.innerHTML = html;
}