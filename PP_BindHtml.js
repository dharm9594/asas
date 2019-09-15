function gridView(response) {
    var html = '';
    var json = JSON.parse(response);
    for (var i = 0; i < json.length; i++) {
        var fileType = json[i].FileType;
        fileType = fileType.toLowerCase();

        html = html + '<div class="col-12 col-md-6 col-lg-4 col-xl-4 leftFloat mb-2">\n';
        html = html + '<div class="media p-3 listItem white_BG">\n';
        html = html + '<div class="leftSide" align="center">\n';
        if (fileType == ".indd" || fileType == ".ttf" || fileType == ".jpg" || fileType == ".jpeg" || fileType == ".png" || fileType == ".gif" || fileType == ".otf" || fileType == ".idml" || fileType == ".ai") {
            html = html + '<div class="mr-3 ml-2 height6 width6 coverImg">\n';
            html = html + '<img src="https://us145k12gcaspp1.nac.sitel-world.net/GrowthCenter/' + json[i].FilePath_Thumb + '" class="img-fluid max6"/>'
            html = html + '</div>\n';
        }
        else if (fileType == ".mp4" || fileType == ".mkv") {
            html = html + '<div class="mr-3 ml-2 height6 width6 coverImg">\n';
            html = html + '<img src="https://us145k12gcaspp1.nac.sitel-world.net/GrowthCenter/' + json[i].FilePath_Thumb + '" class="img-fluid max6"/>'
            html = html + '</div>\n';
        }
        else if (fileType == ".xlsx" || fileType == ".xls") {
            html = html + '<div class="greyIcon mr-3 ml-2 height6 width6 pt-3 pb-3 pr-4 pl-4">\n';
            html = html + '<svg class="fileIcon" width="57" height="62.656" viewBox="0 0 57 62.656">\n';
            html = html + '<use href="../../Content/custom_icons/excel.svg#fileIcon"></use>\n';
            html = html + '</svg>\n';
            html = html + '</div>\n';
        }
        else if (fileType == ".doc" || fileType == ".docx") {
            html = html + '<div class="greyIcon mr-3 ml-2 height6 width6 pt-3 pb-3 pr-4 pl-4">\n';
            html = html + '<svg class="fileIcon" width="57" height="62.656" viewBox="0 0 57 62.656">\n';
            html = html + '<use href="../../Content/custom_icons/word.svg#fileIcon"></use>\n';
            html = html + '</svg>\n';
            html = html + '</div>\n';
        }
        else if (fileType == ".pdf") {
            html = html + '<div class="greyIcon mr-3 ml-2 height6 width6 pt-3 pb-3 pr-4 pl-4">\n';
            html = html + '<svg class="fileIcon" width="57" height="62.656" viewBox="0 0 57 62.656">\n';
            html = html + '<use href="../../Content/custom_icons/pdf.svg#fileIcon"></use>\n';
            html = html + '</svg>\n';
            html = html + '</div>\n';
        }
        else if (fileType == ".txt") {
            html = html + '<div class="greyIcon mr-3 ml-2 height6 width6 pt-3 pb-3 pr-4 pl-4">\n';
            html = html + '<svg class="fileIcon" width="57" height="62.656" viewBox="0 0 57 62.656">\n';
            html = html + '<use href="../../Content/custom_icons/txt.svg#fileIcon"></use>\n';
            html = html + '</svg>\n';
            html = html + '</div>\n';
        }
        else if (fileType == ".ppt" || fileType == ".pptx") {
            html = html + '<div class="greyIcon mr-3 ml-2 height6 width6 pt-3 pb-3 pr-4 pl-4">\n';
            html = html + '<svg class="fileIcon" width="57" height="62.656" viewBox="0 0 57 62.656">\n';
            html = html + '<use href="../../Content/custom_icons/ppt.svg#fileIcon"></use>\n';
            html = html + '</svg>\n';
            html = html + '</div>\n';
        }
        else {
            html = html + '<div class="greyIcon mr-3 ml-2 height6 width6 pt-3 pb-3 pr-4 pl-4">\n';
            html = html + '<svg class="fileIcon" width="57" height="62.656" viewBox="0 0 57 62.656">\n';
            html = html + '<use href="../../Content/custom_icons/fileIcons.svg#fileIcon"></use>\n';
            html = html + '</svg>\n';
            html = html + '</div>\n';
        }
        html = html + '<div class="media-footer mt-1">\n';
        if (json[i].FavouritesCnt != null) {
            html = html + '<a class="mt-1 mr-2' + (json[i].FavouritesCnt != null ? '' : ' ml-4') + '" title="Add To Favourites" href="javascript:void(0);" onclick="ArticleFavorite(this)" data-id=' + json[i].ID + ' data-fileid=' + json[i].FileID + ' data-filename=\'' + json[i].FileName + '\'  data-filepath=\'' + json[i].FilePath + '\'>\n';
            html = html + '<svg class="star' + (parseInt(json[i].FavouritesCnt) == 0 ? '' : ' active') + '" width="22" height="20" viewBox="0 0 22 25">\n';
            html = html + '<use href="../../Content/custom_icons/starIcon.svg#star"></use>\n';
            html = html + '</svg>\n';
            html = html + '</a>\n';
        }
        html = html + '<a class="mt-1 mr-2" title="Download" href="fileDownloader.ashx?filePath=' + json[i].FilePath + '&fileName=' + json[i].FileName + json[i].FileType + '">\n';
        html = html + '<svg width="21" height="17" viewBox="0 0 21 21">\n';
        html = html + '<use class="downloadIcon" href="../../Content/custom_icons/downloadIcon.svg#downloadIcon"></use>\n';
        html = html + '</svg>\n';
        html = html + '</a>\n';
        html = html + '<a class="mr-2 genevent" title="Add To Cart" href="javascript:void(0);" data-id=' + json[i].ID + ' data-fileid=' + json[i].FileID + ' data-filename=\'' + json[i].FileName + '\'  data-filepath=\'' + json[i].FilePath + '\' >\n';
        html = html + '<svg xmlns="http://www.w3.org/2000/svg" class="cartAdd" width="23" height="21" viewBox="0 0 55 53">\n';
        html = html + '<g transform="translate(-1700.373 -27)">\n';
        html = html + '<g transform="translate(1700.373 40)">\n';
        html = html + '<path d="M46.921,18.868a1.832,1.832,0,0,0-1.5-.912L15.785,16.565l-1.032-4.126A1.868,1.868,0,0,0,12.925,11H6.876a1.92,1.92,0,0,0,0,3.838h4.6l5.158,20.535-1.266,5.374a1.981,1.981,0,0,0,.328,1.631,1.8,1.8,0,0,0,1.454.72H40.122a1.92,1.92,0,0,0,0-3.838H19.583l.516-2.207,18.757-.912a1.89,1.89,0,0,0,1.594-1.1l6.565-14.393A1.85,1.85,0,0,0,46.921,18.868Z" transform="translate(-5 -11)">\n';
        html = html + '</path>\n';
        html = html + '<circle cx="4.267" cy="4.267" r="4.267" transform="translate(9.648 31.467)">\n';
        html = html + '</circle>\n';
        html = html + '<circle cx="4.267" cy="4.267" r="4.267" transform="translate(28.39 31.467)">\n';
        html = html + '</circle>\n';
        html = html + '</g>\n';
        html = html + '<g transform="translate(-11.627)">\n';
        html = html + '<rect style="fill: #92da46;" width="26" height="26" rx="13" transform="translate(1741 27)">\n';
        html = html + '</rect>\n';
        html = html + '<text fill="white" font-family="Calibri" font-size="1.5rem" transform="translate(1754 46)">\n';
        html = html + '<tspan x="-6" y="0.5">+</tspan>\n';
        html = html + '</text>\n';
        html = html + '</g>\n';
        html = html + '</g>\n';
        html = html + '</svg>\n';
        html = html + '</a>\n';
        html = html + '</div>\n';
        html = html + '</div>\n';
        html = html + '<div class="media-body">\n';
        html = html + '<h5 class="mt-0 display-4 only1" title="' + isNull(json[i].FileName) + '">' + isNull(json[i].FileName) + '</h5>\n';
        html = html + '<h5 class="mediumFont mr-4 only3" title="' + isNull(json[i].Description) + '">' + isNull(json[i].Description) + '</h5>\n';
        html = html + '<h5 class="mt-0 smallFont">Uploaded : ' + (json[i].CreatedDate != null ? convertDate(json[i].CreatedDate.match(/\d+/)[0] * 1) : '') + '</h5>\n';
        var MoreFilter = $('#hdnMoreFilter').val();
        if (MoreFilter == "Image" || MoreFilter == "Video") {
            html = html + '<button class="btn redIcon_BG m-0 pt-1 pb-1 pr-4 pl-4 smallFont" onclick="popupImage_Video(' + json[i].FileID + '); return false;">View</button>'
        }
        else {
            html = html + '<button class="btn redIcon_BG m-0 pt-1 pb-1 pr-4 pl-4 smallFont" onclick="popupDoc(' + json[i].FileID + '); return false;">View</button>'
        }
        html = html + '</div>\n';
        html = html + '</div>\n';
        html = html + '</div>\n';
    }

    return html;
}

function listView(response) {
    var html = '';
    var json = JSON.parse(response);
    for (var i = 0; i < json.length; i++) {
        var fileType = json[i].FileType;
        fileType = fileType.toLowerCase();

        html = html + '<div class="col-12">\n';
        html = html + '<div class="media p-3 listItem white_BG">\n';
        html = html + '<div class="leftSide" align="center">\n';
        if (fileType == ".indd" || fileType == ".ttf" || fileType == ".jpg" || fileType == ".jpeg" || fileType == ".png" || fileType == ".gif" || fileType == ".otf" || fileType == ".idml" || fileType == ".ai") {
            html = html + '<div class="mr-3 ml-2 height6 width6 coverImg">\n';
            html = html + '<img src="https://us145k12gcaspp1.nac.sitel-world.net/GrowthCenter/' + json[i].FilePath_Thumb + '" class="img-fluid max6"/>'
            html = html + '</div>\n';
        }
        else if (fileType == ".mp4" || fileType == ".mkv") {
            html = html + '<div class="mr-3 ml-2 height6 width6 coverImg">\n';
            html = html + '<img src="https://us145k12gcaspp1.nac.sitel-world.net/GrowthCenter/' + json[i].FilePath_Thumb + '" class="img-fluid max6"/>'
            html = html + '</div>\n';
        }
        else if (fileType == ".xlsx" || fileType == ".xls") {
            html = html + '<div class="greyIcon mr-3 ml-2 height6 width6 pt-3 pb-3 pr-4 pl-4">\n';
            html = html + '<svg class="fileIcon" width="57" height="62.656" viewBox="0 0 57 62.656">\n';
            html = html + '<use href="../../Content/custom_icons/excel.svg#fileIcon"></use>\n';
            html = html + '</svg>\n';
            html = html + '</div>\n';
        }
        else if (fileType == ".doc" || fileType == ".docx") {
            html = html + '<div class="greyIcon mr-3 ml-2 height6 width6 pt-3 pb-3 pr-4 pl-4">\n';
            html = html + '<svg class="fileIcon" width="57" height="62.656" viewBox="0 0 57 62.656">\n';
            html = html + '<use href="../../Content/custom_icons/word.svg#fileIcon"></use>\n';
            html = html + '</svg>\n';
            html = html + '</div>\n';
        }
        else if (fileType == ".pdf") {
            html = html + '<div class="greyIcon mr-3 ml-2 height6 width6 pt-3 pb-3 pr-4 pl-4">\n';
            html = html + '<svg class="fileIcon" width="57" height="62.656" viewBox="0 0 57 62.656">\n';
            html = html + '<use href="../../Content/custom_icons/pdf.svg#fileIcon"></use>\n';
            html = html + '</svg>\n';
            html = html + '</div>\n';
        }
        else if (fileType == ".txt") {
            html = html + '<div class="greyIcon mr-3 ml-2 height6 width6 pt-3 pb-3 pr-4 pl-4">\n';
            html = html + '<svg class="fileIcon" width="57" height="62.656" viewBox="0 0 57 62.656">\n';
            html = html + '<use href="../../Content/custom_icons/txt.svg#fileIcon"></use>\n';
            html = html + '</svg>\n';
            html = html + '</div>\n';
        }
        else if (fileType == ".ppt" || fileType == ".pptx") {
            html = html + '<div class="greyIcon mr-3 ml-2 height6 width6 pt-3 pb-3 pr-4 pl-4">\n';
            html = html + '<svg class="fileIcon" width="57" height="62.656" viewBox="0 0 57 62.656">\n';
            html = html + '<use href="../../Content/custom_icons/ppt.svg#fileIcon"></use>\n';
            html = html + '</svg>\n';
            html = html + '</div>\n';
        }
        else {
            html = html + '<div class="greyIcon mr-3 ml-2 height6 width6 pt-3 pb-3 pr-4 pl-4">\n';
            html = html + '<svg class="fileIcon" width="57" height="62.656" viewBox="0 0 57 62.656">\n';
            html = html + '<use href="../../Content/custom_icons/fileIcons.svg#fileIcon"></use>\n';
            html = html + '</svg>\n';
            html = html + '</div>\n';
        }
        html = html + '<div class="media-footer mt-1">\n';
        if (json[i].FavouritesCnt != null) {
            html = html + '<a class="mt-1 mr-2" title="Add To Favourites" href="javascript:void(0);" onclick="ArticleFavorite(this)" data-id=' + json[i].ID + ' data-fileid=' + json[i].FileID + ' data-filename=\'' + json[i].FileName + '\'  data-filepath=\'' + json[i].FilePath + '\'>\n';
            html = html + '<svg class="star' + (parseInt(json[i].FavouritesCnt) == 0 ? '' : ' active') + '" width="22" height="20" viewBox="0 0 22 25">\n';
            html = html + '<use href="../../Content/custom_icons/starIcon.svg#star"></use>\n';
            html = html + '</svg>\n';
            html = html + '</a>\n';
        }
        html = html + '<a class="mt-1 mr-2' + (json[i].FavouritesCnt != null ? '' : ' ml-4') + '" title="Download" href="fileDownloader.ashx?filePath=' + json[i].FilePath + '&fileName=' + json[i].FileName + json[i].FileType + '">\n';
        html = html + '<svg width="21" height="17" viewBox="0 0 21 21">\n';
        html = html + '<use class="downloadIcon" href="../../Content/custom_icons/downloadIcon.svg#downloadIcon"></use>\n';
        html = html + '</svg>\n';
        html = html + '</a>\n';
        html = html + '<a class="mr-2 genevent" title="Add To Cart" href="javascript:void(0);" data-id=' + json[i].ID + ' data-fileid=' + json[i].FileID + ' data-filename=\'' + json[i].FileName + '\'  data-filepath=\'' + json[i].FilePath + '\'>\n';
        html = html + '<svg xmlns="http://www.w3.org/2000/svg" class="cartAdd" width="23" height="21" viewBox="0 0 55 53">\n';
        html = html + '<g transform="translate(-1700.373 -27)">\n';
        html = html + '<g transform="translate(1700.373 40)">\n';
        html = html + '<path d="M46.921,18.868a1.832,1.832,0,0,0-1.5-.912L15.785,16.565l-1.032-4.126A1.868,1.868,0,0,0,12.925,11H6.876a1.92,1.92,0,0,0,0,3.838h4.6l5.158,20.535-1.266,5.374a1.981,1.981,0,0,0,.328,1.631,1.8,1.8,0,0,0,1.454.72H40.122a1.92,1.92,0,0,0,0-3.838H19.583l.516-2.207,18.757-.912a1.89,1.89,0,0,0,1.594-1.1l6.565-14.393A1.85,1.85,0,0,0,46.921,18.868Z" transform="translate(-5 -11)">\n';
        html = html + '</path>\n';
        html = html + '<circle cx="4.267" cy="4.267" r="4.267" transform="translate(9.648 31.467)">\n';
        html = html + '</circle>\n';
        html = html + '<circle cx="4.267" cy="4.267" r="4.267" transform="translate(28.39 31.467)">\n';
        html = html + '</circle>\n';
        html = html + '</g>\n';
        html = html + '<g transform="translate(-11.627)">\n';
        html = html + '<rect style="fill: #92da46;" width="26" height="26" rx="13" transform="translate(1741 27)">\n';
        html = html + '</rect>\n';
        html = html + '<text fill="white" font-family="Calibri" font-size="1.5rem" transform="translate(1754 46)">\n';
        html = html + '<tspan x="-6" y="0.5">+</tspan>\n';
        html = html + '</text>\n';
        html = html + '</g>\n';
        html = html + '</g>\n';
        html = html + '</svg>\n';
        html = html + '</a>\n';
        html = html + '</div>\n';
        html = html + '</div>\n';
        html = html + '<div class="media-body">\n';
        html = html + '<h5 class="mt-0 display-4 only1" title="' + isNull(json[i].FileName) + '">' + isNull(json[i].FileName) + '</h5>\n';
        html = html + '<h5 class="mediumFont mr-4 only3" title="' + isNull(json[i].Description) + '">' + isNull(json[i].Description) + '</h5>\n';
        html = html + '<h5 class="mt-0 smallFont">Uploaded : ' + (json[i].CreatedDate != null ? convertDate(json[i].CreatedDate.match(/\d+/)[0] * 1) : '') + '</h5>\n';
        var MoreFilter = $('#hdnMoreFilter').val();
        if (MoreFilter == "Image" || MoreFilter == "Video") {
            html = html + '<button class="btn redIcon_BG m-0 pt-1 pb-1 pr-4 pl-4 smallFont" onclick="popupImage_Video(' + json[i].FileID + '); return false;">View</button>'
        }
        else {
            html = html + '<button class="btn redIcon_BG m-0 pt-1 pb-1 pr-4 pl-4 smallFont" onclick="popupDoc(' + json[i].FileID + '); return false;">View</button>'
        }
        html = html + '</div>\n';
        html = html + '</div>\n';
        html = html + '</div>\n';
    }

    return html;
}

function gridView_top6(response, id) {
    var html = '';
    var json = JSON.parse(response);
    debugger;
    if (id != '') {
        if (json.length <= 6) {
            $('#' + id).css('display', 'none');
        }
        else {
            $('#' + id).css('display', 'block');
        }
    }

    if (json.length == 0) {
        html = html + '<div class="col-12">\n';
        html = html + '<h2 class="mb-0 display-6 textCenter white pt-5 pb-5">No record found!</h2>'
        html = html + '</div>'
    }
    else {
        for (var i = 0; i < json.length; i++) {
            var fileType = json[i].FileType;
            fileType = fileType.toLowerCase();

            html = html + '<div class="col-12 col-md-6 col-lg-4 col-xl-4 leftFloat mb-2 top6"' + (i < 6 ? '' : ' style="display: none;"') + '>\n';
            html = html + '<div class="media p-3 listItem white_BG">\n';
            html = html + '<div class="leftSide" align="center">\n';
            if (fileType == ".indd" || fileType == ".ttf" || fileType == ".jpg" || fileType == ".jpeg" || fileType == ".png" || fileType == ".gif" || fileType == ".otf" || fileType == ".idml" || fileType == ".ai") {
                html = html + '<div class="mr-3 ml-2 height6 width6 coverImg">\n';
                html = html + '<img src="https://us145k12gcaspp1.nac.sitel-world.net/GrowthCenter/' + json[i].FilePath_Thumb + '" class="img-fluid max6"/>'
                html = html + '</div>\n';
            }
            else if (fileType == ".mp4" || fileType == ".mkv") {
                html = html + '<div class="mr-3 ml-2 height6 width6 coverImg">\n';
                html = html + '<img src="https://us145k12gcaspp1.nac.sitel-world.net/GrowthCenter/' + json[i].FilePath_Thumb + '" class="img-fluid max6"/>'
                html = html + '</div>\n';
            }
            else if (fileType == ".xlsx" || fileType == ".xls") {
                html = html + '<div class="greyIcon mr-3 ml-2 height6 width6 pt-3 pb-3 pr-4 pl-4">\n';
                html = html + '<svg class="fileIcon" width="57" height="62.656" viewBox="0 0 57 62.656">\n';
                html = html + '<use href="../../Content/custom_icons/excel.svg#fileIcon"></use>\n';
                html = html + '</svg>\n';
                html = html + '</div>\n';
            }
            else if (fileType == ".doc" || fileType == ".docx") {
                html = html + '<div class="greyIcon mr-3 ml-2 height6 width6 pt-3 pb-3 pr-4 pl-4">\n';
                html = html + '<svg class="fileIcon" width="57" height="62.656" viewBox="0 0 57 62.656">\n';
                html = html + '<use href="../../Content/custom_icons/word.svg#fileIcon"></use>\n';
                html = html + '</svg>\n';
                html = html + '</div>\n';
            }
            else if (fileType == ".pdf") {
                html = html + '<div class="greyIcon mr-3 ml-2 height6 width6 pt-3 pb-3 pr-4 pl-4">\n';
                html = html + '<svg class="fileIcon" width="57" height="62.656" viewBox="0 0 57 62.656">\n';
                html = html + '<use href="../../Content/custom_icons/pdf.svg#fileIcon"></use>\n';
                html = html + '</svg>\n';
                html = html + '</div>\n';
            }
            else if (fileType == ".txt") {
                html = html + '<div class="greyIcon mr-3 ml-2 height6 width6 pt-3 pb-3 pr-4 pl-4">\n';
                html = html + '<svg class="fileIcon" width="57" height="62.656" viewBox="0 0 57 62.656">\n';
                html = html + '<use href="../../Content/custom_icons/txt.svg#fileIcon"></use>\n';
                html = html + '</svg>\n';
                html = html + '</div>\n';
            }
            else if (fileType == ".ppt" || fileType == ".pptx") {
                html = html + '<div class="greyIcon mr-3 ml-2 height6 width6 pt-3 pb-3 pr-4 pl-4">\n';
                html = html + '<svg class="fileIcon" width="57" height="62.656" viewBox="0 0 57 62.656">\n';
                html = html + '<use href="../../Content/custom_icons/ppt.svg#fileIcon"></use>\n';
                html = html + '</svg>\n';
                html = html + '</div>\n';
            }
            else {
                html = html + '<div class="greyIcon mr-3 ml-2 height6 width6 pt-3 pb-3 pr-4 pl-4">\n';
                html = html + '<svg class="fileIcon" width="57" height="62.656" viewBox="0 0 57 62.656">\n';
                html = html + '<use href="../../Content/custom_icons/fileIcons.svg#fileIcon"></use>\n';
                html = html + '</svg>\n';
                html = html + '</div>\n';
            }
            html = html + '<div class="media-footer mt-1">\n';
            if (json[i].FavouritesCnt != null) {
                html = html + '<a class="mt-1 mr-2" title="Add To Favourites" href="javascript:void(0);" onclick="ArticleFavorite(this)" data-id=' + json[i].ID + ' data-fileid=' + json[i].FileID + ' data-filename=\'' + json[i].FileName + '\'  data-filepath=\'' + json[i].FilePath + '\'>\n';
                html = html + '<svg class="star' + (parseInt(json[i].FavouritesCnt) == 0 ? '' : ' active') + '" width="22" height="20" viewBox="0 0 22 25">\n';
                html = html + '<use href="../../Content/custom_icons/starIcon.svg#star"></use>\n';
                html = html + '</svg>\n';
                html = html + '</a>\n';
            }
            html = html + '<a class="mt-1 mr-2' + (json[i].FavouritesCnt != null ? '' : ' ml-4') + '" title="Download" href="fileDownloader.ashx?filePath=' + json[i].FilePath + '&fileName=' + json[i].FileName + json[i].FileType + '">\n';
            html = html + '<svg width="21" height="17" viewBox="0 0 21 21">\n';
            html = html + '<use class="downloadIcon" href="../../Content/custom_icons/downloadIcon.svg#downloadIcon"></use>\n';
            html = html + '</svg>\n';
            html = html + '</a>\n';
            html = html + '<a class="mr-2 genevent" title="Add To Cart" href="javascript:void(0);" data-id=' + json[i].ID + ' data-fileid=' + json[i].FileID + ' data-filename=\'' + json[i].FileName + '\'  data-filepath=\'' + json[i].FilePath + '\'>\n';
            html = html + '<svg xmlns="http://www.w3.org/2000/svg" class="cartAdd" width="23" height="21" viewBox="0 0 55 53">\n';
            html = html + '<g transform="translate(-1700.373 -27)">\n';
            html = html + '<g transform="translate(1700.373 40)">\n';
            html = html + '<path d="M46.921,18.868a1.832,1.832,0,0,0-1.5-.912L15.785,16.565l-1.032-4.126A1.868,1.868,0,0,0,12.925,11H6.876a1.92,1.92,0,0,0,0,3.838h4.6l5.158,20.535-1.266,5.374a1.981,1.981,0,0,0,.328,1.631,1.8,1.8,0,0,0,1.454.72H40.122a1.92,1.92,0,0,0,0-3.838H19.583l.516-2.207,18.757-.912a1.89,1.89,0,0,0,1.594-1.1l6.565-14.393A1.85,1.85,0,0,0,46.921,18.868Z" transform="translate(-5 -11)">\n';
            html = html + '</path>\n';
            html = html + '<circle cx="4.267" cy="4.267" r="4.267" transform="translate(9.648 31.467)">\n';
            html = html + '</circle>\n';
            html = html + '<circle cx="4.267" cy="4.267" r="4.267" transform="translate(28.39 31.467)">\n';
            html = html + '</circle>\n';
            html = html + '</g>\n';
            html = html + '<g transform="translate(-11.627)">\n';
            html = html + '<rect style="fill: #92da46;" width="26" height="26" rx="13" transform="translate(1741 27)">\n';
            html = html + '</rect>\n';
            html = html + '<text fill="white" font-family="Calibri" font-size="1.5rem" transform="translate(1754 46)">\n';
            html = html + '<tspan x="-6" y="0.5">+</tspan>\n';
            html = html + '</text>\n';
            html = html + '</g>\n';
            html = html + '</g>\n';
            html = html + '</svg>\n';
            html = html + '</a>\n';
            html = html + '</div>\n';
            html = html + '</div>\n';
            html = html + '<div class="media-body">\n';
            html = html + '<h5 class="mt-0 display-4 only1" title="' + isNull(json[i].FileName) + '">' + isNull(json[i].FileName) + '</h5>\n';
            html = html + '<h5 class="mediumFont mr-4 only3"  title="' + isNull(json[i].Description) + '">' + isNull(json[i].Description) + '</h5>\n';
            html = html + '<h5 class="mt-0 smallFont">Uploaded : ' + (json[i].CreatedDate != null ? convertDate(json[i].CreatedDate.match(/\d+/)[0] * 1) : '') + '</h5>\n';
            var MoreFilter = $('#hdnMoreFilter').val();
            if (MoreFilter == "Image" || MoreFilter == "Video") {
                html = html + '<button class="btn redIcon_BG m-0 pt-1 pb-1 pr-4 pl-4 smallFont" onclick="popupImage_Video(' + json[i].FileID + '); return false;">View</button>'
            }
            else {
                html = html + '<button class="btn redIcon_BG m-0 pt-1 pb-1 pr-4 pl-4 smallFont" onclick="popupDoc(' + json[i].FileID + '); return false;">View</button>'
            }
            html = html + '</div>\n';
            html = html + '</div>\n';
            html = html + '</div>\n';
        }
    }
    return html;
}

function resultresponce(response) {
    var rptRcntSearchedWord = '';
    var json = JSON.parse(response.d);
    var docsLength = json.length;
    for (var i = 0; i < docsLength; i++) {
        rptRcntSearchedWord = rptRcntSearchedWord + '<span class="badge badge-light chip customRound mediumFont mr-2 mb-2">' +
                                '<span class="pr-3 pl-2 pt-2 pb-2" onclick="SearchedWordClicked(\'' + json[i].WordSearched + '\');">' + json[i].WordSearched + '</span>' +
                                '<span aria-hidden="true"><i class=" pointer close fa-lg" onclick="removeWordSearch(\'' + json[i].WordSearched + '\');" aria-hidden="true">×</i></span>' +
                                '</span>';
    }
    $('#rptRcntSearchedWord').html(rptRcntSearchedWord);
}

function onSuccessApi(response) {
    var json = JSON.parse(response);
    var fileID = "";
    var docsLength = json.response.docs.length;
    numFound = json.response.numFound;
    $('#spnTotalNumFound').text(numFound);
    for (var i = 0; i < docsLength; i++) {
        fileID = fileID + json.response.docs[i].FileID + (i == (docsLength - 1) ? "" : ",");
    }
    if (fileID.trim() != "") {
        getByID(fileID);
    }
    else {
        $('#forGrid').append('<div class="col-12 leftFloat  text-center"><a  class="btn btn-custom btn-xs">No record Found! Kindly refine your search</a></div>');
        $('#forList').append('<div class="col-12  text-center"><a  class="btn btn-custom btn-xs">No record found! Kindly refine your search</a></div>');
        $("#progress").hide();
    }
}


function onsuccessFav_NewUpld(response, id) {
    var html = gridView_top6(response, (id == 'divFavourites' ? 'showMore' : ''));
    $('#' + id).html(html);
}


function GetResultbyID(response) {
    startIndex = startIndex + maxRow;
    $('#forGrid').append(gridView(response.d));
    $('#forList').append(listView(response.d));
    if (parseInt(startIndex) <= parseInt(numFound)) {
        $('#forGrid').append('<div class="col-12 leftFloat lnkloadmore text-center"><a onclick="scrollDiv(this);" class="btn btn-custom btn-xs">Load More</a></div>');
        $('#forList').append('<div class="col-12 lnkloadmore text-center"><a onclick="scrollDiv(this);" class="btn btn-custom btn-xs">Load More</a></div>');
    }
    //$('#containerDiv').prepend('<div class="row">');
    //$('#containerDiv').append('</div>');
};


function GetresultBCFileDetailsByID(response) {
    debugger;
    // var xmlDoc = $.parseXML(response.d);
    // var xml = $(xmlDoc);
    var json = JSON.parse(response.d);
    var PopupImgVid = '';
    var Filetype = '';
    Filetype = json[0][0].FileType;
    Filetype = Filetype.toLowerCase();
    PopupImgVid = PopupImgVid + ' <div id="popup_IV" class="popup">' +
            '<div class="container popupArea">' +
    '<div class="row fullHeighVH fullWidthVH p-3 listItem white_BG">' +
                      '<div class="col-12 col-md-12 col-lg-12 col-xl-12">' +
                         ' <span class="closeBtn" onclick="clearNav(\'popup_IV\');">&times;</span>' +
                          '<div>' +
                              '<div class="leftSide col-12 col-sm-12 col-md-12 col-lg-6 col-xl-6 leftFloat mb-3" align="center">' +
                                 ' <div class="mr-3 ml-2 coverImg">';
    if (Filetype != '.mp4') {
        PopupImgVid = PopupImgVid + '<a href="https://us145k12gcaspp1.nac.sitel-world.net/GrowthCenter/' + json[0][0].FilePath + '" target="_blank">';
        PopupImgVid = PopupImgVid + '<img class="img-fluid max85" src="https://us145k12gcaspp1.nac.sitel-world.net/GrowthCenter/' + json[0][0].FilePath + '"/>'
        PopupImgVid = PopupImgVid + '</a>'
    }
    else {
        PopupImgVid = PopupImgVid + '<video class="img-fluid max85" controls>';
        PopupImgVid = PopupImgVid + '<source src="https://us145k12gcaspp1.nac.sitel-world.net/GrowthCenter/' + json[0][0].FilePath + '" type="video/mp4"/>';
        PopupImgVid = PopupImgVid + '</video>';
    }
    PopupImgVid = PopupImgVid + ' </div>' +
                                 ' <div class="media-footer mt-1">' +
                                 ' </div>' +
                             ' </div>' +
                             ' <div class="col-12 col-sm-12 col-md-12 col-lg-6 col-xl-6 overflow_S">' +
                                 ' <h5 class="mt-0 display-4 largeFont_1x"  title="' + isNull(json[0][0].FileName) + '">' + limitChar(isNull(json[0][0].FileName), 40) + '</h5>' +
                                  '<div class="table-responsive">' +
                                     ' <table class="table">';
    for (var i = 0; i < json[1].length; i++) {
        PopupImgVid = PopupImgVid + ' <tr>' +
             '<td>' +
                ' <h5 class="mt-0 mediumFont">' + isNull(json[1][i].ThesaurusCategory) + '</h5>' +
           '  </td>' +
             '<td>' +
                ' <h5 class="mt-0 mediumFont">' + isNull(json[1][i].ThesaurusNames) + '</h5>' +
           '  </td>' +
        ' </tr>'
    }
    PopupImgVid = PopupImgVid + '</table>' +
                                  '</div>' +
                                 ' <div class="rightFloat">' +
                                     ' <a class="btn redIcon_BG m-0 pt-1 pb-1 pr-4 pl-4 mediumFont mr-2" href="fileDownloader.ashx?filePath=' + json[0][0].FilePath + '&fileName=' + json[0][0].FileName + json[0][0].FileType + '">' +
                                         ' <svg class="mr-2" width="17" height="13" viewBox="0 0 21 21">' +
                                             ' <use class="whiteSVG" href="../../Content/custom_icons/downloadIcon.svg#downloadIcon"></use>' +
                                         ' </svg>' +
  ' Download' +
'</a>' +
'<a class="mr-2 genevent" title="Add To Cart" href="javascript:void(0);" data-id=' + json[0][0].ID + ' data-fileid=' + json[0][0].FileID + ' data-filename=\'' + json[0][0].FileName + '\'  data-filepath=\'' + json[0][0].FilePath + '\'>' +
   '<svg xmlns="http://www.w3.org/2000/svg" class="cartAdd" width="27" height="25" viewBox="0 0 55 53">' +
      ' <g transform="translate(-1700.373 -27)">' +
           '<g transform="translate(1700.373 40)">' +
               '<path d="M46.921,18.868a1.832,1.832,0,0,0-1.5-.912L15.785,16.565l-1.032-4.126A1.868,1.868,0,0,0,12.925,11H6.876a1.92,1.92,0,0,0,0,3.838h4.6l5.158,20.535-1.266,5.374a1.981,1.981,0,0,0,.328,1.631,1.8,1.8,0,0,0,1.454.72H40.122a1.92,1.92,0,0,0,0-3.838H19.583l.516-2.207,18.757-.912a1.89,1.89,0,0,0,1.594-1.1l6.565-14.393A1.85,1.85,0,0,0,46.921,18.868Z" transform="translate(-5 -11)">' +
              ' </path>' +
              ' <circle cx="4.267" cy="4.267" r="4.267" transform="translate(9.648 31.467)">' +
              ' </circle>' +
              ' <circle cx="4.267" cy="4.267" r="4.267" transform="translate(28.39 31.467)">' +
               '</circle>' +
           '</g>' +
   '<g transform="translate(-11.627)">' +
   '<rect style="fill: #92da46;" width="26" height="26" rx="13" transform="translate(1741 27)">' +
   ' </rect>' +
   ' <text fill="white" font-family="Calibri" font-size="1.5rem" transform="translate(1754 46)">' +
   '    <tspan x="-6" y="0.5">+</tspan>' +
   '</text>' +
   ' </g>' +
   '</g>' +
   '</svg>' +
   '</a>' +
   '</div>' +
   '</div>' +
   '</div>' +
   '</div>' +
'</div>' +
'</div> </div>'
    $('body').append(PopupImgVid);
}

function GetresultGCFileDetailsByID(response) {
    debugger;
    var json = JSON.parse(response.d);
    var popupDoc = '';
    var Filetype = '';
    Filetype = json[0].FileType;
    Filetype = Filetype.toLowerCase();
    popupDoc = popupDoc + '  <div id="popup_D" class="popup">' +
            '<div class="container popupArea">' +
                 ' <div class="row fullHeighVH fullWidthVH p-3 listItem white_BG">' +
                     ' <div class="col-12 col-md-12 col-lg-12 col-xl-12">' +
                         ' <span class="closeBtn" onclick="clearNav(\'popup_D\');">&times;</span>' +
                          '<div>' +
                         '<div class="leftSide col-12" align="center">' +
                                  '<div class="leftFloat">' +
                                     ' <a class="mt-1 mr-2" title="Add To Favourites" href="javascript:void(0);">' +
                                          '<svg class="star" width="28" height="26" viewBox="0 0 22 25">' +
                                             ' <use href="../../Content/custom_icons/starIcon.svg#star"></use>' +
                                          '</svg>' +
                                     ' </a>' +
                                 ' </div>' +
                                 ' <span class="mt-0 display-4 largeFont_1x pr-4 mr-4 only1" title="' + isNull(json[0].FileName) + '">' + limitChar(isNull(json[0].FileName), 50) + '</span>' +
                                 ' <div class="rightFloat">' +
                                     ' <button class="btn transparentBtn m-0 pt-1 pb-2 pr-4 pl-4 mediumFont mr-2" onclick="return toggleDetails(\'filePreview\',\'fileDetails\', this);">' +
                                         ' View Details' +
                                     ' </button>' +
                                     ' <a class="btn redIcon_BG m-0 pt-1 pb-1 pr-4 pl-4 mediumFont mr-2" href="fileDownloader.ashx?filePath=' + json[0].FilePath + '&fileName=' + json[0].FileName + json[0].FileType + '">' +
                                         ' <svg class="mr-2" width="17" height="13" viewBox="0 0 21 21">' +
                                             ' <use class="whiteSVG" href="../../Content/custom_icons/downloadIcon.svg#downloadIcon"></use>' +
                                         ' </svg>' +
    'Download' +
'</a>' +
'<a class="mr-2 genevent" title="Add To Cart" href="javascript:void(0);" data-id=' + json[0].ID + ' data-fileid=' + json[0].FileID + ' data-filename=\'' + json[0].FileName + '\'  data-filepath=\'' + json[0].FilePath + '\'>' +
    '<svg xmlns="http://www.w3.org/2000/svg" class="cartAdd" width="27" height="25" viewBox="0 0 55 53">' +
        '<g transform="translate(-1700.373 -27)">' +
           ' <g transform="translate(1700.373 40)">' +
                '<path d="M46.921,18.868a1.832,1.832,0,0,0-1.5-.912L15.785,16.565l-1.032-4.126A1.868,1.868,0,0,0,12.925,11H6.876a1.92,1.92,0,0,0,0,3.838h4.6l5.158,20.535-1.266,5.374a1.981,1.981,0,0,0,.328,1.631,1.8,1.8,0,0,0,1.454.72H40.122a1.92,1.92,0,0,0,0-3.838H19.583l.516-2.207,18.757-.912a1.89,1.89,0,0,0,1.594-1.1l6.565-14.393A1.85,1.85,0,0,0,46.921,18.868Z" transform="translate(-5 -11)">' +
                '</path>' +
               ' <circle cx="4.267" cy="4.267" r="4.267" transform="translate(9.648 31.467)">' +
                '</circle>' +
                '<circle cx="4.267" cy="4.267" r="4.267" transform="translate(28.39 31.467)">' +
                '</circle>' +
            '</g>' +
           ' <g transform="translate(-11.627)">' +
              '  <rect style="fill: #92da46;" width="26" height="26" rx="13" transform="translate(1741 27)">' +
               ' </rect>' +
               ' <text fill="white" font-family="Calibri" font-size="1.5rem" transform="translate(1754 46)">' +
               '  <tspan x="-6" y="0.5">+</tspan>' +
               ' </text>' +
           ' </g>' +
        '</g>' +
    '</svg>' +
'</a>' +
'</div>' +
'</div>' +
'<div class="col-12 greyIcon mt-4 max35 p-0 overflow_S" id="filePreview">' +
   ' <div class="embed-responsive embed-responsive-16by9">';
    if (Filetype == '.ppt' || Filetype == '.pptx') {
        popupDoc = popupDoc + '<iframe class="embed-responsive-item" src="DocViewer.aspx?FileID=' + json[0].FileID + '"></iframe>';
    }
    else {
        popupDoc = popupDoc + '<iframe class="embed-responsive-item" src="https://us145k12gcaspp1.nac.sitel-world.net/GrowthCenter/' + json[0].IFrameFilePath + '"></iframe>';
    }
    popupDoc = popupDoc + '</div>' +
 '</div>' +
 '<div class="col-12 mt-4 max35 white_BG overflow_S" id="fileDetails" style="display: none;">' +
     '<div class="table-responsive lightgreyIcon">' +
        ' <table class="table">' +
            ' <tr>' +
                ' <td class="width10">' +
                   '  <h5 class="mt-0 mediumFont">Description</h5>' +
                                      '  </td>' +
                 '<td>' +
                     '<h5 class="mt-0 mediumFont"  title="' + isNull(json[0].Description) + '">' + isNull(json[0].Description) + '</h5>' +
                ' </td>' +
             '</tr>' +
            ' <tr>' +
                ' <td class="width10">' +
                    ' <h5 class="mt-0 mediumFont">Main Category</h5>' +
               '  </td>' +
                 '<td>' +
                    ' <h5 class="mt-0 mediumFont">' + isNull(json[0].Categories) + '</h5>' +
                ' </td>' +
            ' </tr>' +
             '<tr>' +
               '  <td class="width10">' +
                    ' <h5 class="mt-0 mediumFont">File Type</h5>' +
               '  </td>' +
                ' <td>' +
                  '   <h5 class="mt-0 mediumFont">' + isNull(json[0].FileType) + '</h5>' +
                ' </td>' +
                                      '</tr>' +
             '<tr>' +
                 '<td class="width10">' +
                    ' <h5 class="mt-0 mediumFont">File Format</h5>' +
                ' </td>' +
                ' <td>' +
                    ' <h5 class="mt-0 mediumFont">' + isNull(json[0].FileFormat) + '</h5>' +
                 '</td>' +
                                      '</tr>' +
             '<tr>' +
                ' <td class="width10">' +
                    ' <h5 class="mt-0 mediumFont">Updated Date</h5>' +
                 '</td>' +
                 '<td>' +
                   '  <h5 class="mt-0 mediumFont">' + (json[0].UpdatedDate != null ? convertDate(json[0].UpdatedDate.match(/\d+/)[0] * 1) : '') + '</h5>' +
                ' </td>' +
             '</tr>' +
            ' <tr>' +
                ' <td class="width10">' +
                  '   <h5 class="mt-0 mediumFont">Tag List</h5>' +
               '  </td>' +
               '  <td>'
    var Tags = json[0].Tags.split(',');

    for (var i = 0; i < Tags.length; i++) {
        if (Tags[i].trim() != "") {
            popupDoc = popupDoc + '  <span class="badge badge-light transparentBtn chip rounded mediumFont p-2 mt-1 mr-1">' +
          '     <span class="pl-2 pr-2">' + Tags[i] + '</span>' +
         '  </span>'
        }
    }
    popupDoc = popupDoc + '  </td>' +
             '</tr>' +
         '</table>' +
   '  </div>' +
     '<div class="mt-3">' +
        ' <h5 class="display-4"><b>Feedback</b></h5>' +
        ' <textarea id="txtFeedback" class="form-control resizeNone feedback" rows="6"></textarea>' +
    ' </div>' +
    ' <div class="mt-3">' +
     '    <button class="btn redIcon_BG m-0 pt-1 pb-1 pr-4 pl-4 mediumFont mr-2" data-fileid='+ json[0].FileID+' onclick="SendFeedback(this)">Send Feedback</button>' +
    ' </div>' +
 '</div>' +
 '</div>' +
 '</div>' +
 '</div>' +
 '</div></div>'
    $('body').append(popupDoc);
}


function listViewCart(response) {
    var html = ''
    var json = JSON.parse(response);
    if (json.length == 0) {
        html = html + '<div class="col-12">\n';
        html = html + '<h2 class="mb-0 display-6 textCenter pt-5 pb-5">No record found!</h2>'
        html = html + '</div>'
    }
    else {
        for (var i = 0; i < json.length; i++) {
            var fileType = json[i].FileType;
            fileType = fileType.toLowerCase();

            html = html + '<div class="col-12">';
            html = html + '\n<div class="media p-3 listItem white_BG">';
            html = html + '\n<div class="leftSide">';
            html = html + '\n<div class="leftFloat">';
            html = html + '\n<input type="checkbox" class="checkBox cartCheck pointer" />';
            html = html + '\n</div>';
            if (fileType == ".indd" || fileType == ".ttf" || fileType == ".jpg" || fileType == ".jpeg" || fileType == ".png" || fileType == ".gif" || fileType == ".otf" || fileType == ".idml" || fileType == ".ai") {
                html = html + '<div class="mr-3 ml-4 height6 width6 coverImg">\n';
                html = html + '<img src="https://us145k12gcaspp1.nac.sitel-world.net/GrowthCenter/' + json[i].FilePath_Original + '" class="img-fluid max6"/>'
                html = html + '</div>\n';
            }
            else if (fileType == ".mp4" || fileType == ".mkv") {
                html = html + '<div class="mr-3 ml-4 height6 width6 coverImg">\n';
                html = html + '<img src="https://us145k12gcaspp1.nac.sitel-world.net/GrowthCenter/' + json[i].FilePath_Original + '" class="img-fluid max6"/>'
                html = html + '</div>\n';
            }
            else if (fileType == ".xlsx" || fileType == ".xls") {
                html = html + '<div class="greyIcon mr-3 ml-4 height6 width6 pt-3 pb-3 pr-4 pl-4">\n';
                html = html + '<svg class="fileIcon" width="57" height="62.656" viewBox="0 0 57 62.656">\n';
                html = html + '<use href="../../Content/custom_icons/excel.svg#fileIcon"></use>\n';
                html = html + '</svg>\n';
                html = html + '</div>\n';
            }
            else if (fileType == ".doc" || fileType == ".docx") {
                html = html + '<div class="greyIcon mr-3 ml-4 height6 width6 pt-3 pb-3 pr-4 pl-4">\n';
                html = html + '<svg class="fileIcon" width="57" height="62.656" viewBox="0 0 57 62.656">\n';
                html = html + '<use href="../../Content/custom_icons/word.svg#fileIcon"></use>\n';
                html = html + '</svg>\n';
                html = html + '</div>\n';
            }
            else if (fileType == ".pdf") {
                html = html + '<div class="greyIcon mr-3 ml-4 height6 width6 pt-3 pb-3 pr-4 pl-4">\n';
                html = html + '<svg class="fileIcon" width="57" height="62.656" viewBox="0 0 57 62.656">\n';
                html = html + '<use href="../../Content/custom_icons/pdf.svg#fileIcon"></use>\n';
                html = html + '</svg>\n';
                html = html + '</div>\n';
            }
            else if (fileType == ".txt") {
                html = html + '<div class="greyIcon mr-3 ml-4 height6 width6 pt-3 pb-3 pr-4 pl-4">\n';
                html = html + '<svg class="fileIcon" width="57" height="62.656" viewBox="0 0 57 62.656">\n';
                html = html + '<use href="../../Content/custom_icons/txt.svg#fileIcon"></use>\n';
                html = html + '</svg>\n';
                html = html + '</div>\n';
            }
            else if (fileType == ".ppt" || fileType == ".pptx") {
                html = html + '<div class="greyIcon mr-3 ml-4 height6 width6 pt-3 pb-3 pr-4 pl-4">\n';
                html = html + '<svg class="fileIcon" width="57" height="62.656" viewBox="0 0 57 62.656">\n';
                html = html + '<use href="../../Content/custom_icons/ppt.svg#fileIcon"></use>\n';
                html = html + '</svg>\n';
                html = html + '</div>\n';
            }
            else {
                html = html + '<div class="greyIcon mr-3 ml-4 height6 width6 pt-3 pb-3 pr-4 pl-4">\n';
                html = html + '<svg class="fileIcon" width="57" height="62.656" viewBox="0 0 57 62.656">\n';
                html = html + '<use href="../../Content/custom_icons/fileIcons.svg#fileIcon"></use>\n';
                html = html + '</svg>\n';
                html = html + '</div>\n';
            }
            html = html + '\n</div>';
            html = html + '\n<div class="media-body">';
            html = html + '\n<h5 class="mt-0 display-4 only1" title="' + isNull(json[i].FileName) + '">';
            html = html + '\n<span class="leftFloat">' + isNull(json[i].FileName) + '</span>';
            html = html + '\n<span class="rightFloat">';
            html = html + '\n<a class="blackIcon"  onclick="DeleteFileFromCart(this)" data-type=' + json[i].Type + ' data-fileid=' + json[i].FileID + '>Remove <span class="largeFont">x</span></a>';
            html = html + '\n</span>';
            html = html + '\n</h5>';
            html = html + '\n<h5 class="mt-0 smallFont only2">Uploaded : ' + (json[i].CreatedDate != null ? convertDate(json[i].CreatedDate.match(/\d+/)[0] * 1) : '') + '</h5>';
            html = html + '\n<a class="mt-1 mr-2 leftFloat singleDownload" title="Download" href="fileDownloader.ashx?filePath=' + json[i].FilePath_Original + '&fileName=' + json[i].FileName + '">';
            html = html + '\n<svg width="21" height="17" viewBox="0 0 21 21">';
            html = html + '\n<use class="downloadIcon" href="../../Content/custom_icons/downloadIcon.svg#downloadIcon"></use>';
            html = html + '\n</svg>';
            html = html + '\n</a>';
            html = html + '\n<a class="mt-1 mr-2 leftFloat blackIcon" title="Email" href="javascript:void(0);">';
            html = html + '\n<i class="fa fa-envelope-o fa-lg" aria-hidden="true"></i>';
            html = html + '\n</a>';
            if (fileType == ".indd" || fileType == ".ttf" || fileType == ".jpg" || fileType == ".jpeg" || fileType == ".png" || fileType == ".gif" || fileType == ".otf" || fileType == ".idml" || fileType == ".ai" || fileType == ".mp4" || fileType == ".mkv") {
                html = html + '<button class="btn redIcon_BG m-0 pt-1 pb-1 pr-4 pl-4 smallFont rightFloat white" onclick="popupImage_Video(' + json[i].FileID + '); return false;">Preview</button>'
            }
            else {
                html = html + '<button class="btn redIcon_BG m-0 pt-1 pb-1 pr-4 pl-4 smallFont rightFloat white" onclick="popupDoc(' + json[i].FileID + '); return false;">Preview</button>'
            }
            html = html + '\n</div>';
            html = html + '\n</div>';
            html = html + '\n</div>';
        }
    }
    return html;
}

function gridViewCart(response) {
    var html = ''
    var json = JSON.parse(response);
    if (json.length == 0) {
        html = html + '<div class="col-12">\n';
        html = html + '<h2 class="mb-0 display-6 textCenter pt-5 pb-5">No record found!</h2>'
        html = html + '</div>'
    }
    else {
        for (var i = 0; i < json.length; i++) {
            var fileType = json[i].FileType;
            fileType = fileType.toLowerCase();

            html = html + '<div class="col-12 col-md-12 col-lg-6 col-xl-6 leftFloat mb-2">\n';
            html = html + '\n<div class="media p-3 listItem white_BG">';
            html = html + '\n<div class="leftSide">';
            html = html + '\n<div class="leftFloat">';
            html = html + '\n<input type="checkbox" class="checkBox cartCheck pointer" />';
            html = html + '\n</div>';
            if (fileType == ".indd" || fileType == ".ttf" || fileType == ".jpg" || fileType == ".jpeg" || fileType == ".png" || fileType == ".gif" || fileType == ".otf" || fileType == ".idml" || fileType == ".ai") {
                html = html + '<div class="mr-3 ml-4 height6 width6 coverImg">\n';
                html = html + '<img src="https://us145k12gcaspp1.nac.sitel-world.net/GrowthCenter/' + json[i].FilePath_Original + '" class="img-fluid max6"/>'
                html = html + '</div>\n';
            }
            else if (fileType == ".mp4" || fileType == ".mkv") {
                html = html + '<div class="mr-3 ml-4 height6 width6 coverImg">\n';
                html = html + '<img src="https://us145k12gcaspp1.nac.sitel-world.net/GrowthCenter/' + json[i].FilePath_Original + '" class="img-fluid max6"/>'
                html = html + '</div>\n';
            }
            else if (fileType == ".xlsx" || fileType == ".xls") {
                html = html + '<div class="greyIcon mr-3 ml-4 height6 width6 pt-3 pb-3 pr-4 pl-4">\n';
                html = html + '<svg class="fileIcon" width="57" height="62.656" viewBox="0 0 57 62.656">\n';
                html = html + '<use href="../../Content/custom_icons/excel.svg#fileIcon"></use>\n';
                html = html + '</svg>\n';
                html = html + '</div>\n';
            }
            else if (fileType == ".doc" || fileType == ".docx") {
                html = html + '<div class="greyIcon mr-3 ml-4 height6 width6 pt-3 pb-3 pr-4 pl-4">\n';
                html = html + '<svg class="fileIcon" width="57" height="62.656" viewBox="0 0 57 62.656">\n';
                html = html + '<use href="../../Content/custom_icons/word.svg#fileIcon"></use>\n';
                html = html + '</svg>\n';
                html = html + '</div>\n';
            }
            else if (fileType == ".pdf") {
                html = html + '<div class="greyIcon mr-3 ml-4 height6 width6 pt-3 pb-3 pr-4 pl-4">\n';
                html = html + '<svg class="fileIcon" width="57" height="62.656" viewBox="0 0 57 62.656">\n';
                html = html + '<use href="../../Content/custom_icons/pdf.svg#fileIcon"></use>\n';
                html = html + '</svg>\n';
                html = html + '</div>\n';
            }
            else if (fileType == ".txt") {
                html = html + '<div class="greyIcon mr-3 ml-4 height6 width6 pt-3 pb-3 pr-4 pl-4">\n';
                html = html + '<svg class="fileIcon" width="57" height="62.656" viewBox="0 0 57 62.656">\n';
                html = html + '<use href="../../Content/custom_icons/txt.svg#fileIcon"></use>\n';
                html = html + '</svg>\n';
                html = html + '</div>\n';
            }
            else if (fileType == ".ppt" || fileType == ".pptx") {
                html = html + '<div class="greyIcon mr-3 ml-4 height6 width6 pt-3 pb-3 pr-4 pl-4">\n';
                html = html + '<svg class="fileIcon" width="57" height="62.656" viewBox="0 0 57 62.656">\n';
                html = html + '<use href="../../Content/custom_icons/ppt.svg#fileIcon"></use>\n';
                html = html + '</svg>\n';
                html = html + '</div>\n';
            }
            else {
                html = html + '<div class="greyIcon mr-3 ml-4 height6 width6 pt-3 pb-3 pr-4 pl-4">\n';
                html = html + '<svg class="fileIcon" width="57" height="62.656" viewBox="0 0 57 62.656">\n';
                html = html + '<use href="../../Content/custom_icons/fileIcons.svg#fileIcon"></use>\n';
                html = html + '</svg>\n';
                html = html + '</div>\n';
            }
            html = html + '\n</div>';
            html = html + '\n<div class="media-body">';
            html = html + '\n<h5 class="mt-0 display-4 only1" title="' + isNull(json[i].FileName) + '">' + isNull(json[i].FileName) + '</h5>';
            html = html + '\n<h5 class="mt-0 smallFont only2">Uploaded : ' + (json[i].CreatedDate != null ? convertDate(json[i].CreatedDate.match(/\d+/)[0] * 1) : '') + '</h5>';
            html = html + '\n<a class="mt-1 mr-2 leftFloat singleDownload" title="Download" href="fileDownloader.ashx?filePath=' + json[i].FilePath_Original + '&fileName=' + json[i].FileName + '">';
            html = html + '\n<svg width="21" height="17" viewBox="0 0 21 21">';
            html = html + '\n<use class="downloadIcon" href="../../Content/custom_icons/downloadIcon.svg#downloadIcon"></use>';
            html = html + '\n</svg>';
            html = html + '\n</a>';
            html = html + '\n<a class="mt-1 mr-2 leftFloat blackIcon" title="Email" href="javascript:void(0);">';
            html = html + '\n<i class="fa fa-envelope-o fa-lg" aria-hidden="true"></i>';
            html = html + '\n</a>';
            html = html + '\n<a class="mt-1 mr-2 leftFloat blackIcon" title="Remove"  onclick="DeleteFileFromCart(this)" data-type=' + json[i].Type + ' data-fileid=' + json[i].FileID + ' >';
            html = html + '\n<i class="fa fa-times fa-lg" aria-hidden="true"></i>';
            html = html + '\n</a>';
            if (fileType == ".indd" || fileType == ".ttf" || fileType == ".jpg" || fileType == ".jpeg" || fileType == ".png" || fileType == ".gif" || fileType == ".otf" || fileType == ".idml" || fileType == ".ai" || fileType == ".mp4" || fileType == ".mkv") {
                html = html + '<button class="btn redIcon_BG m-0 pt-1 pb-1 pr-4 pl-4 smallFont rightFloat white" onclick="popupImage_Video(' + json[i].FileID + '); return false;">Preview</button>'
            }
            else {
                html = html + '<button class="btn redIcon_BG m-0 pt-1 pb-1 pr-4 pl-4 smallFont rightFloat white" onclick="popupDoc(' + json[i].FileID + '); return false;">Preview</button>'
            }
            html = html + '\n</div>';
            html = html + '\n</div>';
            html = html + '\n</div>';
        }
    }
    return html;
}

function onSuccessCart(response) {
    var json = JSON.parse(response);
    document.getElementById('cartNum').innerHTML = json.length;
    $('#forGridCart').html(gridViewCart(response));
    $('#forListCart').html(listViewCart(response));
}

