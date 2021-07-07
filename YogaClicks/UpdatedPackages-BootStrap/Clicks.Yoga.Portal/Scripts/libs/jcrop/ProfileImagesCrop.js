$(document).ready(function () {
    var jcrop_api;

    function showCoordsBanner(c) {
        $('#ProfileBanner_ImageCrop_XCoord').val(c.x);
        $('#ProfileBanner_ImageCrop_YCoord').val(c.y);
        $('#ProfileBanner_ImageCrop_Width').val(c.w);
        $('#ProfileBanner_ImageCrop_Height').val(c.h);
    };

    function showCoordsProfile(c) {
        $('#Image_ImageCrop_XCoord').val(c.x);
        $('#Image_ImageCrop_YCoord').val(c.y);
        $('#Image_ImageCrop_Width').val(c.w);
        $('#Image_ImageCrop_Height').val(c.h);
    };

    function bindPreview(e, coordFunction, aspectRatio, setSelector, saveMessage, formId) {
        $('#preview').Jcrop({
        onChange: coordFunction,
            onSelect: coordFunction,
            boxWidth: 1000,
            aspectRatio: aspectRatio
        }, function () {
            jcrop_api = this;
        });
        jcrop_api.setImage(e.target.result);

        $.fancybox(
        {
            href: "#cropfancybox",
            scrolling: 'none',
            autoSize: false, //Fix for FireFox Load
            autoDimensions: false,
            height: 'auto',
            width: 'auto',
            afterShow: function () {
                $('.fancybox-outer').append(
                    $('<div>Drag the corners of the above box to crop your photo</div>', { 'style': 'display:inline-block; width:100%; padding:10px 0 0 0; margin-top: 5px' }).append(
                        $('<input/>', { 'class': 'button', 'type': 'button', 'value': 'Finished Cropping', 'onClick': '$.fancybox.close(); $(\'' + formId + '\').submit(); $(\'#PleaseWait\').show();', 'style': 'float:right;' }))
                );
                jcrop_api.setSelect(setSelector);
            }
        });
    }

    $('#File').change(function (evt) {
        if (window.FileReader) {
            var f = evt.target.files[0];
            var reader = new FileReader();
            reader.onload = function (e) {
                bindPreview(e, showCoordsBanner, 3 / 1, [0, 0, 200, 200], 'profilebannerSave', '#update-banner-form');
            }
            reader.readAsDataURL(f);
        }
    });

    $('#profileImage').change(function (evt) {
        if (window.FileReader) {
            var f = evt.target.files[0];
            var reader = new FileReader();
            reader.onload = function (e) {
                bindPreview(e, showCoordsProfile, 1 / 1, [0, 0, 200, 200], 'profileImageSave', '#update-profile-image-form');
            }
            reader.readAsDataURL(f);
        }
    });
});
