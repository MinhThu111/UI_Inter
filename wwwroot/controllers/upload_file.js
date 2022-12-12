
var dataTable;
var $tableMain = $('#table_main');
$(document).ready(function () {
    LoadDataTable();
});

const buttonActionHtmls = function (id, status, timer) {
    let html = ``;
    //html += `<a href="#deleteFileModal" class="delete" onclick="DeleteFile(${fName})" title="${_buttonResource.Delete}"><i class="material-icons" data-toggle="tooltip" title="Delete">&#xE872;</i></a>`;
    return html;
}
//get data form controller
const dataParamsTables = function (method = 'GET') {
    return {
        type: method,
        url: '/UploadFile/GetList',
        data: function (d) {
           
        },
        dataType: 'json',
        beforeSend: function () {
            //laddaSearch.start();
        },
        dataSrc: function (response) {
            //laddaSearch.stop();
            if (CheckResponseIsSuccess(response) && response.data != null)
                return response.data;
            return response;
        },
        error: function (err) {
            //laddaSearch.stop();
            CheckResponseIsSuccess({ result: -1, error: { code: err.status } });
            return [];
        }
    };
}


const columnTable = function () {
    return [
        {
            title: "STT",
            data: null,
            render: (data, type, row, meta) => ++meta.row,
            className: "text-center"
        },
        {
            data:"fName",
            className: "text-center text-nowrap", 
        },
        {
            data: "fName",
            render: (data, type, row, meta) => buttonActionHtmls(data, row.status, row.timer),
            orderable: false,
            searchable: false,
            className: "text-center text-nowrap"
        }
        
    ];
}
//Load table
function LoadDataTable(method = 'GET') {
    console.log("hello");
    if (dataTable) dataTable.ajax.reload(null, true);
    dataTable = $tableMain.DataTable({
        lengthChange: true,
        lengthMenu: _lengthMenuResource,
        colReorder: { allowReorder: false },
        select: false,
        scrollX: true,
        stateSave: false,
        processing: true,
        responsive: { details: true },
        //get data
        ajax: dataParamsTables(method),
        rowId: "id",
        //column name
        columns: columnTable(),
        language: _languageDataTalbeObj,
        drawCallback: _dataTablePaginationStyle,
        initComplete: function () { jQuery(jQuery.fn.dataTable.tables(true)).DataTable().columns.adjust().draw(); }
    });
}

//Show panel when done
function ShowPanelWhenDone(html) {
    $(window).scrollTop();
    $('#div_view_panel').html(html);
    ShowHidePanel("#div_view_panel", "#div_main_table");
}

//Show add single file
function ShowAddSingleFileModal(elm) {
    
    let text = $(elm).html();
    $(elm).attr('disabled', true); $(elm).html(_loadAnimationSmallHtml);
    $.get(`/UploadFile/P_UploadSingleFile`).done(function (response) {
        $(elm).attr('disabled', false); $(elm).html(text);
        if (response.result === 0 || response.result === -1) {
            CheckResponseIsSuccess(response); return false;
        }
        ShowPanelWhenDone(response);
        //Dropzone.autoDiscover = false;
        //dropzoneFile = new Dropzone('#my-dropzone', {
        //    autoProcessQueue: false,
        //    url: "UploadFile/P_UploadSingleFile",
        //    paramName: 'file',
        //    uploadMultiple: false
        //})
        InitSubmitUploadSingleFile();
        $('.dropify').dropify({
            messages: {
                'default': 'Drag and drop a file here or click',
                'replace': 'Drag and drop or click to replace',
                'remove': 'Remove',
                'error': 'Ooops, something wrong happended.'
            },
            error: {
                'fileSize': 'The file size is too big ({{ value }} max).',
                'minWidth': 'The image width is too small ({{ value }}}px min).',
                'maxWidth': 'The image width is too big ({{ value }}}px max).',
                'minHeight': 'The image height is too small ({{ value }}}px min).',
                'maxHeight': 'The image height is too big ({{ value }}px max).',
                'imageFormat': 'The image format is not allowed ({{ value }} only).'
            }
        });
        

    }).fail(function (err) {
        $(elm).attr('disabled', false); $(elm).html(text);
        CheckResponseIsSuccess({ result: -1, error: { code: err.status } });
    });
}
//Show add mutiple file
function ShowAddMutipleFileModal(elm) {
    let text = $(elm).html();
    $(elm).attr('disabled', true); $(elm).html(_loadAnimationSmallHtml);
    $.get(`/UploadFile/P_UploadMutipleFile`).done(function (response) {
        $(elm).attr('disabled', false); $(elm).html(text);
        if (response.result === 0 || response.result === -1) {
            CheckResponseIsSuccess(response); return false;
        }
        ShowPanelWhenDone(response);
        InitSubmitUploadMutipleFile();
        $('.dropify').dropify({
            messages: {
                'default': 'Drag and drop a file here or click',
                'replace': 'Drag and drop or click to replace',
                'remove': 'Remove',
                'error': 'Ooops, something wrong happended.'
            },
            error: {
                'fileSize': 'The file size is too big ({{ value }} max).',
                'minWidth': 'The image width is too small ({{ value }}}px min).',
                'maxWidth': 'The image width is too big ({{ value }}}px max).',
                'minHeight': 'The image height is too small ({{ value }}}px min).',
                'maxHeight': 'The image height is too big ({{ value }}px max).',
                'imageFormat': 'The image format is not allowed ({{ value }} only).'
            }
        });
    }).fail(function (err) {
        $(elm).attr('disabled', false); $(elm).html(text);
        CheckResponseIsSuccess({ result: -1, error: { code: err.status } });
    });
}

//Init submit add form
function InitSubmitUploadSingleFile() {
    $('#form_data').on('submit', function (e) {
        e.preventDefault();
        e.stopImmediatePropagation();
        let $formElm = $('#form_data');
        //let isvalidate = $formElm[0].checkValidity();
        let isvalidate = CheckValidationUnobtrusive($formElm);
        if (!isvalidate) { ShowToastNoti('warning', '', _resultActionResource.PleaseWrite); return false; }
        let formData = new FormData($formElm[0]);
        laddaSubmitForm = Ladda.create($formElm.find('[type="submit"]')[0]);
        laddaSubmitForm.start();
        $.ajax({
            url: '/UploadFile/P_UploadSingleFile',
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                laddaSubmitForm.stop();
                if (!CheckResponseIsSuccess(response)) return false;
                ShowToastNoti('success', '', _resultActionResource.AddSuccess);
                BackToTable('#div_main_table', '#div_view_panel');
            }, error: function (err) {
                laddaSubmitForm.stop();
                CheckResponseIsSuccess({ result: -1, error: { code: err.status } });
            }
        });
    });
}
//Init submit add form                             
function InitSubmitUploadMutipleFile() {
    $('#form_data').on('submit', function (e) {
        e.preventDefault();
        e.stopImmediatePropagation();
        let $formElm = $('#form_data');
        //let isvalidate = $formElm[0].checkValidity();
        let isvalidate = CheckValidationUnobtrusive($formElm);
        if (!isvalidate) { ShowToastNoti('warning', '', _resultActionResource.PleaseWrite); return false; }
        let formData = new FormData($formElm[0]);
        laddaSubmitForm = Ladda.create($formElm.find('[type="submit"]')[0]);
        laddaSubmitForm.start();
        $.ajax({
            url: '/UploadFile/P_UploadMutipleFile',
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                laddaSubmitForm.stop();
                if (!CheckResponseIsSuccess(response)) return false;
                ShowToastNoti('success', '', _resultActionResource.AddSuccess);
                BackToTable('#div_main_table', '#div_view_panel');
            }, error: function (err) {
                laddaSubmitForm.stop();
                CheckResponseIsSuccess({ result: -1, error: { code: err.status } });
            }
        });
    });
}

//delete
function DeleteFile(fName) {
    swal.fire({
        title: 'Xác nhận xóa?',
        text: '',
        type: 'warning',
        showCancelButton: !0,
        confirmButtonText: "Xóa",
        cancelButtonText: "Đóng",
        confirmButtonClass: "btn btn-danger mx-1 mt-2",
        cancelButtonClass: "btn btn-outline-secondary mx-1 mt-2",
        reverseButtons: true,
        buttonsStyling: !1,
        showLoaderOnConfirm: true,
        preConfirm: function () {
            return new Promise(function (resolve, reject) {
                $.ajax({
                    type: 'DELETE',
                    url: '/UploadFile/Delete',
                    data: { fName: fName},
                    dataType: 'json',
                    success: function (response) {
                        if (!CheckResponseIsSuccess(response)) {
                            resolve(); return false;
                        }
                        ShowToastNoti('success', '', _resultActionResource.DeleteSuccess);
                        ChangeUIDelete(dataTable, id);
                        resolve();
                    },
                    error: function (err) {
                        CheckResponseIsSuccess({ result: -1, error: { code: err.status } });
                        resolve();
                    }
                });
            });
        }
    });
}