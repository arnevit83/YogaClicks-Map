
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

    function showCoordsConditionBanner(c) {
        $('#ConditionBanner_ImageCrop_XCoord').val(c.x);
        $('#ConditionBanner_ImageCrop_YCoord').val(c.y);
        $('#ConditionBanner_ImageCrop_Width').val(c.w);
        $('#ConditionBanner_ImageCrop_Height').val(c.h);
    };

    function showCoordsConditionDirectoryImage(c) {
        $('#ConditionDirectoryImage_ImageCrop_XCoord').val(c.x);
        $('#ConditionDirectoryImage_ImageCrop_YCoord').val(c.y);
        $('#ConditionDirectoryImage_ImageCrop_Width').val(c.w);
        $('#ConditionDirectoryImage_ImageCrop_Height').val(c.h);
    };

    function showCoordsUpdateUserPoseImages(c) {
        $('#UpdateUserPoseImages_ImageCrop_XCoord').val(c.x);
        $('#UpdateUserPoseImages_ImageCrop_YCoord').val(c.y);
        $('#UpdateUserPoseImages_ImageCrop_Width').val(c.w);
        $('#UpdateUserPoseImages_ImageCrop_Height').val(c.h);
    };
    
    function drawCroppedImage(prefix) {
        var widthCrop = $("#" + prefix + "ImageCrop_Width").val();
        var heightcrop = $("#" + prefix + "ImageCrop_Height").val();
        var xcrop = $("#" + prefix + "ImageCrop_XCoord").val();
        var ycrop = $("#" + prefix + "ImageCrop_YCoord").val();

        var canvas = document.getElementById(prefix + "signupcanvascanvas");
        var context = canvas.getContext('2d');

        canvas.width = widthCrop;
        canvas.height = heightcrop;

        // Get the src of the image from the div
        var newimage = $('.jcrop-holder div div img').attr("src");

        var imageObj = new Image();
        imageObj.src = newimage;

        // draw the image with offset
        context.drawImage(imageObj, xcrop, ycrop, widthCrop, heightcrop, 0, 0, widthCrop, heightcrop);
    };

    function swapToInPageImage(prefix) {
        $("#" + prefix + "signupcontainer").parent().children("div").hide();
        $("#" + prefix + "signupcanvas").show();
    };

    function bindPreviewSignup(e, coordFunction, aspectRatio, setSelector, prefix) {
        var screenWidth = $("body").width();
     
        //Extends the pre filled box if its a ipad or smaller device.
        if (screenWidth < 1100) {
            var tempimgUploaded = new Image();
            tempimgUploaded.src = e.srcElement.result;

            if (setSelector[2] == setSelector[3]) {
                if (tempimgUploaded.width > 1000) {
                    setSelector[2] = setSelector[2] * 2;
                    setSelector[3] = setSelector[3] * 2;
                }

                if (tempimgUploaded.width > 2000) {
                    setSelector[2] = setSelector[2] * 1.5;
                    setSelector[3] = setSelector[3] * 1.5;
                }

                if (tempimgUploaded.width > 3000) {
                    setSelector[2] = setSelector[2] * 1.5;
                    setSelector[3] = setSelector[3] * 1.5;
                }
            } else {

                if (tempimgUploaded.width > 2000) {
                    setSelector[2] = setSelector[2] * 2;
                    setSelector[3] = setSelector[3] * 2;
                }

                if (tempimgUploaded.width > 3000) {
                    setSelector[2] = setSelector[2] * 1.5;
                    setSelector[3] = setSelector[3] * 1.5;
                }

                if (tempimgUploaded.width > 4000) {
                    setSelector[2] = setSelector[2] * 1.5;
                    setSelector[3] = setSelector[3] * 1.5;
                }
            }
        }

        $('#preview').Jcrop({
            onChange: coordFunction,
            onSelect: coordFunction,
            boxWidth: screenWidth,
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
                            $('<input/>', { 'class': 'button', 'type': 'button', 'value': 'Finished Cropping', 'onClick': '$.fancybox.close(); $(\'#PleaseWait\').hide();', 'style': 'float:right;' }))
                    );
                    jcrop_api.setSelect(setSelector);
                },
                afterClose: function () {
                    drawCroppedImage(prefix);
                    swapToInPageImage(prefix);
                }
            });
    };

    function bindPreview(e, coordFunction, aspectRatio, setSelector, saveMessage, formId) {
        var screenWidth = $("body").width();

        $('#preview').Jcrop({
        onChange: coordFunction,
            onSelect: coordFunction,
            boxWidth: screenWidth,
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
    };

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



    $("#ProfileBanner_signupcontainer").change(function (evt) {
        if (window.FileReader) {
            if (evt.target.files[0].name.match(/\.(jpg|jpeg|png|gif)$/i)) {
                var f = evt.target.files[0];
                var reader = new FileReader();
                reader.onload = function (e) {
                    bindPreviewSignup(e, showCoordsBanner, 3 / 1, [0, 0, 710, 240], "ProfileBanner_");
                }
                reader.readAsDataURL(f);
            } else {
                //can place alerts here
            }
        }
    });

    //Click event for the editing of a image
    $(".signupbannerimagechange").click(function () {
        $("#ProfileBanner_NewImage_PostedFile").click();
    });

    $("#Image_signupcontainer").change(function (evt) {
        if (window.FileReader) {
            if (evt.target.files[0].name.match(/\.(jpg|jpeg|png|gif)$/i)) {
              
                var f = evt.target.files[0];
                var reader = new FileReader();
                reader.onload = function (e) {

                    bindPreviewSignup(e, showCoordsProfile, 1 / 1, [0, 0, 240, 240], "Image_");
                }
                reader.readAsDataURL(f);
            } else {
                //can place alerts here
            }
        }
    });

    //Click event for the editing of a image
    $(".signupprofileimagechange").click(function () {
        $("#Image_NewImage_PostedFile").click();
    });

    $('#ConditionBanner').change(function (evt) {
        if (window.FileReader) {
            var f = evt.target.files[0];
            var reader = new FileReader();
            reader.onload = function (e) {
                bindPreview(e, showCoordsConditionBanner, 179 / 94, [0, 0, 716, 376], 'conditionbannerSave', '#update-condition-banner-form');
            }
            reader.readAsDataURL(f);
        }
    });

    $('#ConditionDirectoryImage').change(function (evt) {
        if (window.FileReader) {
            var f = evt.target.files[0];
            var reader = new FileReader();
            reader.onload = function (e) {
                bindPreview(e, showCoordsConditionDirectoryImage, 133 / 59, [0, 0, 266, 118], 'conditiondirectoryimageSave', '#update-condition-directory-image-form');
            }
            reader.readAsDataURL(f);
        }
    });
    $('#UserPoseImages').change(function (evt) {
        if (window.FileReader) {
            if (evt.target.files[0].name.match(/\.(jpg|jpeg|png|gif)$/i)) {
            var f = evt.target.files[0];
            var reader = new FileReader();
            reader.onload = function (e) {
                bindPreview(e, showCoordsUpdateUserPoseImages, 1 / 1, [0, 0, 600, 600], 'conditiondirectoryimageSave', '#update-condition-directory-image-form');
            }
            reader.readAsDataURL(f);
            } else {
                //Error file type

            }
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



