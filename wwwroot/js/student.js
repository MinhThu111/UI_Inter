//var filesArrData = [];
//Dropzone.autoDiscover = false;
//const MAX_FILE_SIZE_IMAGE = 5;
//const ACCEPT_EXTENSION_IMAGE_FILE = ".png,.jpg,.jpeg";
//const MAX_IMAGE_FILE = 5;
//ACCEPT_EXTENSION_IMAGE_FILE.split(",").forEach((item) => $('.accept_extension_files').append(" " + item));
//$('.max_file_size_multiple_upload').text(MAX_FILE_SIZE_IMAGE);
//$('.max_files_multiple_upload').text(MAX_IMAGE_FILE);
//dropzoneListImageFomrAdd = new Dropzone('#div_multi_upload_image_add', {
//    autoProcessQueue: false,
//    url: "#",
//    paramName: "listImageFile",
//    uploadMultiple: true,
//    maxFilesize: MAX_FILE_SIZE_IMAGE,
//    maxFiles: MAX_IMAGE_FILE,
//    acceptedFiles: ACCEPT_EXTENSION_IMAGE_FILE,
//    init: function () {

//    },
//    previewsContainer: "#div_file_previews_add",
//    previewTemplate: $("#div_upload_preview_template_add").html()
//});

// Dropzone has been added as a global variable.




// If you are using JavaScript/ECMAScript modules:

Dropzone.autoDiscover = false;
dropzoneFile = new Dropzone('#my-dropzone', {
    autoProcessQueue: false,
    url: "#",
    paramName: 'file',
    uploadMultiple:false
})


