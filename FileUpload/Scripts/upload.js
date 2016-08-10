function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}



/* SHOW MODAL DIALOG */
function showModal(modalid, title, message) {
    $('#dm-modal-title').html(title);
    $('#dm-modal-body').html(message);
    $('#dm-modal-button').hide();
    $(modalid).modal('show');
}


//get file path from client system
function getNameFromPath(strFilepath) {
    var objRE = new RegExp(/([^\/\\]+)$/);
    var strName = objRE.exec(strFilepath);

    if (strName == null) {
        return null;
    }
    else {
        return strName[0];
    }
}
/* CHECK FILE SIZE */
function checkFileSize(size, maxsize, message) {
    if (size >= maxsize) {
        showModal('#modal1', 'Alert! Maximum file exceeded', '<h3>Maximum file size ' + message + 'mb </h3>');
        return false;
    } else {
        return true;
    }
}
/* CHECK FILE FORMAT */
function checkFileFormat(file, arrayExtensions) {
    if (!arrayExtensions.length == 0) {
        var ext = file.substr((file.lastIndexOf('.') + 1));
        //console.log(ext);
        $.each(arrayExtensions, function (i, v) {
            // console.log(i);
            flag = (v == ext);
            if (flag == true) {
                return false;
            }
        });
        // console.log(flag);

        if (flag == false) { // not allowed
            showModal('#modal1', 'Alert! Wrong File Format', 'You can upload only ' + arrayExtensions.join(',') + ' extension file');
            return false;
        }
        else { // extension allowed
            return true;
        }

    }
}


$(function () {

  
    /* FORM VALIDATION*/

    var appSettings = $(".sl").html();


    var maxSize = 0;
    var fileSize = 0;
    var mbSize = 5;
    var message = "";
    var arrayExtension = ['pdf', 'doc', 'docx'];

    if (appSettings != null && appSettings != 'undefined') {
        maxSize = (appSettings * 1024) * 1024;
        message = appSettings;
    } else {
        maxSize = (mbSize * 1024) * 1024;
        message = mbSize;
    }



    $("#uploadFile").bind('change', function () {
        fileSize = this.files[0].size;
        checkFileSize(fileSize, maxSize, message);
        checkFileFormat(this.files[0].name, arrayExtension);
    });


    $("#Send").click(function () {

        var fileName = $('#uploadFile').val();

        if (fileName == "") {
            //            $(".spanfile").html("Please select a file to upload");
            //            return false;
        }
        else {
            $(".spanfile").html(' ');
            //            // return checkfile();

            if (checkFileSize(fileSize, maxSize, message) == false) {
                return false;

            } else {
                return checkFileFormat(fileName, arrayExtension);
            }


        }
    });


});