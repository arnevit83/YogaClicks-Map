(function ($) {
    $.fn.yogaPoseGallery = function (options) {

        var $gallery = this;


        var $errors = $gallery.find('[data-role=Errors]');
        //var $entries = $gallery.find('[data-role=Entries]');
        var $uploadButtons = $gallery.find('[data-role=UploadButton]');
        //var $uploadTemplate = $entries.find('[data-role=UploadingTemplate]');
        //var $updateForm = $gallery.find('[data-role=UpdateForm]');
        //var $updateButton = $updateForm.find('[data-role=UpdateButton]');
        var $saveButton = $gallery.find('[data-role=SaveGallery]');
        //var $galleryPublicRadioBtn = $gallery.find('[name=Public]');

        var uploads = {};

        //$uploadTemplate.detach();

        options = $.extend({
            beforeUpload: function () { },
            afterUpload: function () { }
        }, options);

        $uploadButtons.each(function () {
            var $button = $(this);

            var uploader = new plupload.Uploader({
                runtimes: 'html5,flash',
                browse_button: $button.attr('id'),
                max_file_size: '40mb',
                url: $button.attr('data-url'),
                file_data_name: 'PostedFile',
                flash_swf_url: '/scripts/libs/plupload/plupload.flash.swf',
                filters: [
                    { title: "Image files", extensions: "jpg,jpeg,gif,png" }
                ]
            });

            uploader.init();
            uploader.refresh();

            uploader.bind('FilesAdded', function (sender, files) {
                //$errors.hide();

                //for (var i in files) {
                //    var file = files[i];
                //    var $upload = $uploadTemplate.clone();
                //    $upload.appendTo($entries);
                //    $upload.show();
                //    uploads[file.id] = {
                //        control: $upload,
                //        complete: false
                //    };
                //}
                plupload.each(files, function (file) {
                    document.getElementById('filelist').innerHTML += '<div id="' + file.id + '">' + file.name + ' (' + plupload.formatSize(file.size) + ') <b></b></div>';
                });
                //setTimeout(function () {
                //    options['beforeUpload']();
                //    uploader.start();
                //}, 100);
            });

            uploader.bind('FileUploaded', function (sender, file, response) {
                var $upload = uploads[file.id];

                $upload.complete = true;

                $.ajax({
                    type: 'GET',
                    url: response.response,
                    success: function (html) {
                        $upload.control.replaceWith(html);
                        options['afterUpload']();
                    },
                    error: function () {
                        $upload.control.remove();
                        $errors.show();
                    }
                });
            });

            uploader.bind('FilesRemoved', function (sender, files) {
                for (var i in files) {
                    var file = files[i];
                    var $upload = uploads[file.id];

                    if ($upload) $upload.remove();
                }
            });

            uploader.bind('Error', function (sender, error) {
                $errors.show();
            });

            $button.data('uploader', uploader);

            $saveButton.on('click', function (e) {
                var $btn = $(this).button('Saving');
                uploader.files.forEach(function (file) {
                    if (!file.loaded) e.preventDefault();
                });
                $btn.button('reset');

            });
        });

        //$updateButton.on('click', function () {
        //    $.ajax({
        //        type: 'POST',
        //        url: $updateButton.attr('data-url'),
        //        data: $updateForm.find('input').serialize(),
        //        success: function () {

        //            openModal($updateButton.data("success"), false, 400, 150);
        //        }
        //    });
        //});


     
    };
})(jQuery);