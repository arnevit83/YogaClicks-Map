var map, pruneCluster, marker, markers = new L.FeatureGroup(), storysStored, storysMarker = 0, storysMarkerSwp = 0 ,Facebookbool;
function mapleftswp() {


    for (var j = storysStored.length; j > 0; j--) {
        storysMarkerSwp--;

        if (storysMarkerSwp < 0) {
            storysMarkerSwp = storysStored.length;
        }
        console.warn(storysMarkerSwp);
        if ((typeof (storysStored[storysMarkerSwp]) != "undefined")) {

            break;
        };
    }


    if ((typeof (storysStored[storysMarkerSwp]) != "undefined")) {

        var template = '<a class="ClosePopup ClosePopupG" data-dismiss="modal" aria-label="Close"><i class="fa fa-times"></i></a><div class="panel "><div class="panel-body text-center"><div class="' + storysStored[storysMarkerSwp].data.typexy + '"><div id="CsV" style="width:100%;display:none;"><div class="embed-responsive embed-responsive-16by9">' + storysStored[storysMarkerSwp].data.video + '</div></div><img id="CsI" width="100%" class="img-responsive center-block"  src="' + storysStored[storysMarkerSwp].data.url + '"/><i class="fa fa-arrow-left arrmap" aria-hidden="true"> swipe</i> <i class="fa fa-arrow-right arrmap" aria-hidden="true"></i><p><span class="popupName">' + storysStored[storysMarkerSwp].data.Name + '</span><br><span class="popupcation">' + storysStored[storysMarkerSwp].data.TypeName + '</span><br><span class="popuppbydate">#PBY since ' + storysStored[storysMarkerSwp].data.PBYSince + '</span>' + storysStored[storysMarkerSwp].data.insert + '</p></div><hr><br /><div class="' + storysStored[storysMarkerSwp].data.typexy + '" id="MaxpopFolder"><span class="StoryTitlePopbig" id="StoryTitlePopbig"></span><span id="StoryTitlePopbigMenu"></span></div></div></div>';

        $("#popupmainview .modal-dialog").html(template);
        $("#popupmainview").attr('style', 'padding-right:0px!important;display:block');
        maxpopMobile(storysStored[storysMarkerSwp].data.Id);
    }

}
function maprightswp() {
    for (var j = 0; j < storysStored.length; j++) {
        storysMarkerSwp++;
        if (storysMarkerSwp >= storysStored.length) {
            storysMarkerSwp = 0;
        }

        if ((typeof (storysStored[storysMarkerSwp]) != "undefined")) {
            break;
        };
    }


    if ((typeof (storysStored[storysMarkerSwp]) != "undefined")) {

        var template = '<a class="ClosePopup ClosePopupG" data-dismiss="modal" aria-label="Close"><i class="fa fa-times"></i></a><div class="panel "><div class="panel-body text-center"><div class="' + storysStored[storysMarkerSwp].data.typexy + '"><div id="CsV" style="width:100%;display:none;"><div class="embed-responsive embed-responsive-16by9">' + storysStored[storysMarkerSwp].data.video + '</div></div><img id="CsI" width="100%" class="img-responsive center-block"  src="' + storysStored[storysMarkerSwp].data.url + '"/><i class="fa fa-arrow-left arrmap" aria-hidden="true"> swipe</i> <i class="fa fa-arrow-right arrmap" aria-hidden="true"></i><p><span class="popupName">' + storysStored[storysMarkerSwp].data.Name + '</span><br><span class="popupcation">' + storysStored[storysMarkerSwp].data.TypeName + '</span><br><span class="popuppbydate">#PBY since ' + storysStored[storysMarkerSwp].data.PBYSince + '</span>' + storysStored[storysMarkerSwp].data.insert + '</p></div><hr><br /><div class="' + storysStored[storysMarkerSwp].data.typexy + '" id="MaxpopFolder"><span class="StoryTitlePopbig" id="StoryTitlePopbig"></span><span id="StoryTitlePopbigMenu"></span></div></div></div>';

        $("#popupmainview .modal-dialog").html(template);
        $("#popupmainview").attr('style', 'padding-right:0px!important;display:block');
        maxpopMobile(storysStored[storysMarkerSwp].data.Id);
    }

}
function selectMarker(id) {

    var vph = $('#map').width();
    if (vph > 768) {
        map.fitWorld();
    

        $('html, body').animate({
            scrollTop: $("#MapTitle").offset().top
        }, 1000);
       
        var x = pruneCluster.GetMarkers();
        for (var i = 0; i < x.length; i++) {

            if (x[i].data.Id === storysStored[id].data.Id) {
                var y = x[i];
                pruneCluster.RemoveMarkers([y]);
                y.data.open = "true";
                pruneCluster.RegisterMarker(y);
   
            }
        }
     
        window.history.pushState("YogaMap", "Title", '/YogaMap/' + storysStored[id].data.Id);
    
        map.options.maxZoom = 18;
        map.setView(new L.LatLng(storysStored[id].data.lat, storysStored[id].data.lng), 18);
      //  pruneCluster.ProcessView();


    } else {


        var template = '<a class="ClosePopup ClosePopupG" data-dismiss="modal" aria-label="Close"><i class="fa fa-times"></i></a><div class="panel "><div class="panel-body text-center"><div class="' + storysStored[id].data.typexy + '"><div id="CsV" style="width:100%;display:none;"><div class="embed-responsive embed-responsive-16by9">' + storysStored[id].data.video + '</div></div><img id="CsI" width="100%" class="img-responsive center-block"  src="' + storysStored[id].data.url + '"/><i class="fa fa-arrow-left arrmap" aria-hidden="true"> swipe</i> <i class="fa fa-arrow-right arrmap" aria-hidden="true"></i><p><span class="popupName">' + storysStored[id].data.Name + '</span><br><span class="popupcation">' + storysStored[id].data.TypeName + '</span><br><span class="popuppbydate">#PBY since ' + storysStored[id].data.PBYSince + '</span>' + storysStored[id].data.insert + '</p></div><hr><br /><div class="' + storysStored[id].data.typexy + '" id="MaxpopFolder"><span class="StoryTitlePopbig" id="StoryTitlePopbig"></span><span id="StoryTitlePopbigMenu"></span></div></div></div>';

        $("#popupmainview .modal-dialog").html(template);

        $("#popupmainview").modal();
        $("#popupmainview").attr('style', 'padding-right:0px!important;display:block');

        storysMarkerSwp = id;



        $(".spip").unbind('swiperight');
        $(".spip").unbind('swipeleft');

        $(".spip").on("swiperight", function (event) {
            if (!jQuery.active > 0) {

                mapleftswp();

            } else {
                setTimeout(
              function () {
                  mapleftswp();
              }, 150);

            }
           

        });

        $(".spip").on("swipeleft", function (event) {
            if (!jQuery.active > 0) {

                maprightswp();

            } else {
                setTimeout(
              function () {
                  maprightswp();
              }, 150);

            }
        });


        maxpopMobile(storysStored[id].data.Id);
    }




}





function navpopulateStorys() {

    storysMarker = storysMarker + 12;
    for (var i = storysMarker - 12; i < storysMarker ; i++) {

        if (parseInt(i) === parseInt(storysStored.length)) {
            $('.no-more-styles').show();
            break;
        } else {
  
            if ((typeof (storysStored[i]) == "undefined")) {
         

                    storysMarker++;
                } else {
                //  Add Story





                if (storysStored[i].data.url.indexOf("svgicons") > -1) {
                 
                    $('#StorysContainer').append("<div onclick='javascript: selectMarker(" + i + ")' class='col-xs-6 col-sm-4 col-md-3  col-lg-3 NomapImage '><div class='" + storysStored[i].data.typexy + " editorspanblank'><img class='Mapquote img-responsive ' src='/Images/Stories/quote.svg' data-svgpng='/Images/Stories/quote.png'/><span class='editspa'>"
        + storysStored[i].data.story + "</span><span class='Edrm'>READ MORE</span></div></div>");
                } else {

                    var iconxy = "";
                    switch (storysStored[i].data.typexy) {
                    case "Teacher":
                        iconxy = "<img class='iconxy center-block' src='/images/svgicons/Icon_TeacherPurple.svg' data-svgpng='/images/SvgIcons/Icon_TeacherPurple.png'/>";
                        break;
                        case "Shop":
                            iconxy = "<img class='iconxy center-block' src='/images/svgicons/shopicon.svg' data-svgpng='/images/SvgIcons/shopicon.png'/>";
                            break;
                        case "Student":
                            iconxy = "<img class='iconxy center-block' src='/images/svgicons/Icon_ProfileBlue.svg' data-svgpng='/images/SvgIcons/Icon_ProfileBlue.png'/>";
                            break;
                    }

               

                    $('#StorysContainer').append("<div onclick='javascript: selectMarker(" + i + ")' class='col-xs-6 col-sm-4 col-md-3  col-lg-3'><div class='" + storysStored[i].data.typexy + " '><img class='img-responsive' src='" + storysStored[i].data.url + "'/>" + iconxy +"<span class='editorspan'>"
                            + storysStored[i].data.story + "</span><span class='Edrm'>READ MORE</span></div></div>");
                }

            }


        }
        if (!Modernizr.smil) {
            $('.Mapquote').attr("style", "height:100%;");
          
           
            $('.editorspan').attr("style", "height:98px!important");
          ////  $('.editspa').attr("style", "margin-top: -81px;!important");
          //  $('.editspa').attr("style", "margin-bottom: 84.81px;!important");
            
        }

    }
    }
function Videobuttonfunk(url) {

    $('#CsI').hide();
    $('#CsV').show();
    $('.Videobuttonspop').hide();

    
    
}
//function Movebuttonfunk(pbyStoryId) {



//    if (!$('#AddStory').hasClass("PinkG")) {
//        $("#popupmainview").modal('hide');
//        $('#map').toggleClass("curserB");

//        $('.PopoutleftG').attr("style", "background-image: url('/Images/Stories/PinClicked.png')!important");

//        $('#AddStory').toggleClass("PinkG");
//        $('html, body').animate({
//            scrollTop: $("#MapTitle").offset().top
//        }, 1000);
//        resizeDivmap();

//        map.closePopup();


    
//        map.on('click', function(e) {
//            map.off('click');
//              var droppin = L.Icon.extend({
//                      options: {

//                iconSize: [32, 46],
//                iconAnchor: [16, 46]
               
//                    }
//                        });

//                var droppinIc = new droppin({
//                    iconUrl: '/Images/SvgIcons/green-newv2.svg'
//                });


//                marker = L.marker(e.latlng, { icon: droppinIc, bounceOnAdd: true });


//                markers.addLayer(marker);



//            localStorage.setItem("Latpop", e.latlng.lat);
//            localStorage.setItem("Lanpop", e.latlng.lng);
//            $('#map').toggleClass("curserB");


//            setTimeout(function() {
//                var url = "/YogaMap/MoveStory/";

//                $.ajax({
//                    type: "POST",
//                    url: url,
//                    contentType: "application/json; charset=utf-8",
//                    data: JSON.stringify({
//                        Id: pbyStoryId,
//                        lng: e.latlng.lng,
//                        lat: e.latlng.lat
//                    }),
//                    dataType: "json",
//                    success: function(html) {
//                        location.href = '/YogaMap/';
//                    }

//                });

           
//                $('#AddStory').toggleClass("PinkG");
//                $('.PopoutleftG').attr("style", "background-image: url('/Images/Stories/PinClick.png')!important");


//            }, 1200);


//        });


//    }


//}
function Removebuttonfunk(pbyStoryId) {
  
        if (confirm("Are you sure you want to delete your story?")) {
        



        $.ajax({
            type: 'POST',

            url: "/YogaMap/RemoveStory/",

            data: JSON.stringify({ Id: pbyStoryId }),

            contentType: 'application/json; charset=utf-8',


            success: function (data) {

                location.href = '/YogaMap/';


            }
        });
        }





};
function Editbuttonfunk(pbyStoryId) {
    if (typeof $("#popupmain").data('bs.modal') == 'undefined' || typeof $('pop') !== 'undefined') {

      
            $.ajax({
                type: 'POST',

                url: "/YogaMap/AddStoryPopEdit/",

                data: JSON.stringify({ Id: pbyStoryId }),

                contentType: 'application/json; charset=utf-8',


                success: function(data) {
                  
                    $("#modellg").html(data);
                  
                    $("#popupmainview").modal('hide');

                      //  if ($(".bs-modal-lg").data('bs.modal').isShown !== true) {

                    setTimeout(function() {
                        $("#popupmain").modal({

                        });
                    }, 500);

                    //  } 
                }
            });
        }

    
};
function ShareShow() {
  
    $(".Sharebigmap").slideToggle({ direction: "up" }, 250);
    
};
function maxpop(pbyStoryId, zoom) {
    var url = "/YogaMap/GetBigPopup/";



    if ($('.Maparrows').hasClass('fa-plus-circle')) {

        $('.Maparrows').removeClass("fa-plus-circle");
        $('.Maparrows').addClass("fa-minus-circle");
      
        $.ajax({
            type: "POST",
            url: url,


            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ Id: pbyStoryId }),
            dataType: "json",


            success: function (html) {


                var titleBig = html[0].toString();
                var descriptionBig = html[1];
                var conditionsBig = "<span class='Conditionsbig'>";
                for (var i = 0; i < html[2].length; i++) {
                    conditionsBig += "<a href='/YogaMeds/" +  html[2][i].UrlSlug + "/" +  html[2][i].Id + "'>" +  html[2][i].Name + "</a>";
                }





                window.history.pushState("YogaMap", "Title", '/YogaMap/' + pbyStoryId);

                $('#MaxpopFolder').show();
                $('.leaflet-popup-close-button').toggleClass("popupclosebigtoggle");


        

                $('#StoryTitlePopbig').html("<b>" + titleBig + "</b><br /><br />" + descriptionBig + "<br /><br /><span class='BigpopupFav'>" + html[4] + "<br />" + html[5] + "<br />" + html[6] + " <hr/><div style=' margin-left: -9px;' class='fb-comments' data-href='" + window.location.href + "' data-numposts='15' data-width='100%'></div><div id='fb-root'></div>" + "</span><script> FB.XFBML.parse(document.getElementById('fb-comments'));  </script>");

              

                $('meta[name=image]').remove();
                $('head').append("<meta property='og:image' name='image' content='" + $("#CsI").attr("src") + ">");
                var fbLink = 'http://www.facebook.com/share.php?u=http://www.yogaclicks.com' + window.location.pathname + "?title=" + $(".popupName").html() + "'s joined The Yoga Map Project.&description=We’re sharing our stories to create a world that’s powered by yoga. Share your story. Together we can get the world on its mat.";
                var twitterLink = 'http://www.twitter.com/share?url=http://www.yogaclicks.com' + window.location.pathname + "?text=" + $(".popupName").html() + "'s joined The Yoga Map Project. ";
                var pintrestLink = 'http://pinterest.com/pin/create/button/?url=http://www.yogaclicks.com' + window.location.pathname + "?description=" + $(".popupName").html() + "'s joined The Yoga Map Project.| YogaClicks&amp;media=" + $("#CsI").attr("src");





                $('#StoryTitlePopbigMenu').html(conditionsBig + '</span>' + html[3] + '<a onclick="ShareShow()" title="Share" href="javascript:void(0);"><i id="ShareYC" class="fa fa-share-alt"></i></a><span class="Sharebigmap"><a href="' + fbLink + '" target="_blank" class="btn btn-social-icon btn-facebook btn-xs facebook-share-link footerIconssharebig"><i class="fa fa-facebook"></i></a><br /><a href="' + twitterLink + '" target="_blank" class="btn btn-social-icon btn-twitter btn-xs twitter-share-link footerIconssharebig"><i class="fa fa-twitter"></i></a><br /><a href="' + pintrestLink + '" target="_blank" class="btn btn-social-icon btn-pinterest btn-xs pintrest-share-link footerIconssharebig"><i class="fa fa-pinterest"></i></a></span>');



                map.panBy(new L.Point(zoom, 0));
               
            },
            error: function (html) {
                console.warn(html);
            }
        });
    }
    else {
        $('#MaxpopFolder').hide();
        $('.leaflet-popup-close-button').hide();
        $('.Maparrows').removeClass("fa-minus-circle");
        $('.Maparrows').addClass("fa-plus-circle");
        map.panBy(new L.Point(-250, 0));
    }

 
}
function maxpopMobile(pbyStoryId) {

    var url = "/YogaMap/GetBigPopup/";

        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ Id: pbyStoryId }),
            dataType: "json",
            success: function (html) {

                window.history.pushState("YogaMap", "Title", '/YogaMap/' + pbyStoryId);

                var titleBig = html[0].toString();
                var descriptionBig = html[1];
                var conditionsBig = "";
                for (var i = 0; i < html[2].length; i++) {
                    conditionsBig += "<a href='/YogaMeds/" + html[2][i].UrlSlug + "/" + html[2][i].Id + "'>" + html[2][i].Name + "</a>";
                }
                if (conditionsBig !== "") {conditionsBig += "<hr />";}
               
                $('#MaxpopFolder').show();
              

                $('#StoryTitlePopbig').html("<b>" + titleBig + "</b><br /><br />" + descriptionBig + "<br /><br /><span class='BigpopupFav'>" + html[4] + "<br />" + html[5] + "<hr/><div style=' margin-left: -9px;' class='fb-comments' data-href='" + window.location.href + "' data-numposts='15' data-width='100%'></div><div id='fb-root'></div>" + "</span><script> FB.XFBML.parse(document.getElementById('fb-comments'));  </script></span> ");



                $('meta[name=image]').remove();
                $('head').append("<meta property='og:image' name='image' content='" + $("#CsI").attr("src") + ">");
                var fbLink = 'http://www.facebook.com/share.php?u=http://www.yogaclicks.com' + window.location.pathname + "?title=" + $(".popupName").html() + "'s joined The Yoga Map Project.&description=We’re sharing our stories to create a world that’s powered by yoga. Share your story. Together we can get the world on its mat.";
                var twitterLink = 'http://www.twitter.com/share?url=http://www.yogaclicks.com' + window.location.pathname + "?text=" + $(".popupName").html() + "'s joined The Yoga Map Project.";
                var pintrestLink = 'http://pinterest.com/pin/create/button/?url=http://www.yogaclicks.com' + window.location.pathname + "?description=" + $(".popupName").html() + "'s joined The Yoga Map Project. | YogaClicks&amp;media=" + $("#CsI").attr("src");



                $('#StoryTitlePopbigMenu').html('<hr />' + html[3] + conditionsBig + '<a onclick="ShareShow()" href="javascript:void(0);"><i id="ShareYC" class="fa fa-share-alt"></i></a><span class="Sharebigmap"><a href="' + fbLink + '" target="_blank" class="btn btn-social-icon btn-facebook btn-xs facebook-share-link footerIconssharebig"><i class="fa fa-facebook"></i></a><br /><a href="' + twitterLink + '" target="_blank" class="btn btn-social-icon btn-twitter btn-xs twitter-share-link footerIconssharebig"><i class="fa fa-twitter"></i></a><br /><a href="' + pintrestLink + '" target="_blank" class="btn btn-social-icon btn-pinterest btn-xs pintrest-share-link footerIconssharebig"><i class="fa fa-pinterest"></i></a></span>');
        


            },
            error: function (html) {
                console.warn(html);
            }
        });
}
function createIcon(data, category) {

    var profilexy;

    switch (data.typexy) {
        case "Student":
            profilexy = '/Images/SvgIcons/blue-new.svg';
            break;
        case "Teacher":
            profilexy = '/Images/SvgIcons/purple-new.svg';
            break;
        case "Shop":
            profilexy = '/Images/SvgIcons/yellow-new.svg';
            break;
        default:
            profilexy = '/Images/SvgIcons/blue-new.svg';
    }

    return L.icon(L.extend({
        iconUrl: profilexy,
        iconSize: [32, 46],
        iconAnchor: [16, 46],

        shadowUrl: '/Images/SvgIcons/marker-shadow.png',

        shadowSize: [32, 46],
        shadowAnchor: [16, 40]

    }));
}
function resizeDivmap() {

    var vph = $('#map').width();


    if ($(window).width() > 751) {
        $('#map').height(550);
    } else {
        $('#map').height(vph);
    }

}

//$("#MapTitlelogo").click(function () {
//      //  map.fitWorld();
//        $('html, body').animate({
//            scrollTop: $("#MapTitle").offset().top
//        }, 1000);
//        map.closePopup();
//        pruneCluster.FitBounds();
//        window.history.pushState("YogaMap", "Title", '/YogaMap/Index');
//    });


window.onresize = function (event) {
   resizeDivmap();
 //   map.closePopup();
}
 $(document).ready(function() {


        resizeDivmap();

       

        var mapquestOSM = L.tileLayer("http://api.mapbox.com/styles/v1/arnevit/ciljkbz50004aazkrggjzl1l7/tiles/{z}/{x}/{y}?access_token={accessToken}", {
            maxZoom: 19,
            id: 'arnevit.nkehkk66',
            accessToken: 'pk.eyJ1IjoiYXJuZXZpdCIsImEiOiJjaWZkdzR0ZXMwMGVsdGpseTNyY2w2aTFiIn0.8negAtdqDN_UiAi1CrkaVQ'
        });
        /* Basemap Layers */
        var mapquestOAM = L.tileLayer("http://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token={accessToken}", {
            maxZoom: 19,
            id: 'arnevit.nkejg83e',
            accessToken: 'pk.eyJ1IjoiYXJuZXZpdCIsImEiOiJjaWZkdzR0ZXMwMGVsdGpseTNyY2w2aTFiIn0.8negAtdqDN_UiAi1CrkaVQ'
        });
        /* Basemap Layers */
        var mapquestHYB = L.tileLayer("http://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token={accessToken}", {
            maxZoom: 19,
            id: 'arnevit.nkejk7g9',
            accessToken: 'pk.eyJ1IjoiYXJuZXZpdCIsImEiOiJjaWZkdzR0ZXMwMGVsdGpseTNyY2w2aTFiIn0.8negAtdqDN_UiAi1CrkaVQ'
        });


        //var mapquestOSM = L.tileLayer("http://{s}.mqcdn.com/tiles/1.0.0/osm/{z}/{x}/{y}.png", {
        //    maxZoom: 19,
        //    subdomains: ["otile1", "otile2", "otile3", "otile4"],
        //    attribution: 'Tiles courtesy of <a href="http://www.mapquest.com/" target="_blank">MapQuest</a> <img src="http://developer.mapquest.com/content/osm/mq_logo.png">. Map data (c) <a href="http://www.openstreetmap.org/" target="_blank">OpenStreetMap</a> contributors, CC-BY-SA.'
        //});

        //var mapquestOAM = L.tileLayer("http://{s}.mqcdn.com/tiles/1.0.0/sat/{z}/{x}/{y}.jpg", {
        //    maxZoom: 18,
        //    subdomains: ["oatile1", "oatile2", "oatile3", "oatile4"],
        //    attribution: 'Tiles courtesy of <a href="http://www.mapquest.com/" target="_blank">MapQuest</a>. Portions Courtesy NASA/JPL-Caltech and U.S. Depart. of Agriculture, Farm Service Agency'
        //});

        //var mapquestHYB = L.layerGroup([L.tileLayer("http://{s}.mqcdn.com/tiles/1.0.0/sat/{z}/{x}/{y}.jpg", {
        //    maxZoom: 18,
        //    subdomains: ["oatile1", "oatile2", "oatile3", "oatile4"]
        //}), L.tileLayer("http://{s}.mqcdn.com/tiles/1.0.0/hyb/{z}/{x}/{y}.png", {
        //    maxZoom: 19,
        //    subdomains: ["oatile1", "oatile2", "oatile3", "oatile4"],
        //    attribution: 'Labels courtesy of <a href="http://www.mapquest.com/" target="_blank">MapQuest</a> <img src="http://developer.mapquest.com/content/osm/mq_logo.png">. Map data (c) <a href="http://www.openstreetmap.org/" target="_blank">OpenStreetMap</a> contributors, CC-BY-SA. Portions Courtesy NASA/JPL-Caltech and U.S. Depart. of Agriculture, Farm Service Agency'
        //})]);


        pruneCluster = new PruneClusterForLeaflet(160);
        pruneCluster.BuildLeafletClusterIcon = function(cluster) {
            var e = new L.Icon.MarkerCluster();
            e.stats = cluster.stats;
            e.population = cluster.population;
            return e;
        };


     


        var vph = $('#map').width();
        pruneCluster.PrepareLeafletMarker = function(leafletMarker, data) {
            leafletMarker.setIcon(createIcon(data));
            leafletMarker.on('add', function() {
        
                var template = '<div class="{typexy} mapPOPup"><div id="CsV" style="width:100%;display:none;"><div class="embed-responsive embed-responsive-4by3">{video}</div></div><img id="CsI"  src="{url}"/><p><span class="popupName">{Name}</span><br><span class="popupcation">{TypeName}</span><br><span class="popuppbydate">#PBY since {PBYSince}</span>{insert} <a  title="See the full story!" onclick="maxpop({Id},250)" href="javascript:void(0);"><i class="fa fa-plus-circle Maparrows"></i></a></p></div><div class="{typexy} MaxpopFolder" id="MaxpopFolder"><span class="StoryTitlePopbig" id="StoryTitlePopbig"></span><span id="StoryTitlePopbigMenu"></span></div>';

                    var doubleopensec = false;
                    if (data.open != null) {
                 
                        if (data.open.toString() === "true") {
                         
                      //  if (vph > 768) {
                        data.open = "false";
                     //        map.panTo(new L.LatLng(data.lat, data.lng), { animate: false });
                            localStorage.setItem("Latpop", data.lat);
                            localStorage.setItem("Lanpop", data.lng);
                            leafletMarker.bindPopup(L.Util.template(template, data), {
                                className: 'leaflet-popup-photo',
                                minWidth: 234,
                                maxWidth: 234,
                                autoPanPadding: new L.Point(0, 80),
                                fillColor: '#ffffff',
                                offset: new L.Point(1, -31)
                            }).openPopup();

                           
                            setTimeout(function () {
                                if (!doubleopensec) {
                                    doubleopensec = true;
                                    maxpop(data.Id,250);
                                    // map.panBy(new L.Point(250, 0), { animate: false });
                               


                                } else {
                                    $('.Maparrows').removeClass("fa-plus-circle");
                                    $('.Maparrows').addClass("fa-minus-circle");
                                    $('.leaflet-popup-close-button').show();
                                  
                                }


                            }, 170);
                           
                             
                              

                            $('#MaxpopFolder').on('shown', function (e) {
                                var modal = $(this);

                                modal.css('margin-top', (modal.outerHeight() / 2) * -1)
                                    .css('margin-left', (modal.outerWidth() / 2) * -1);

                                return this;
                            });


                        //} else {
                        //    template = '<a class="ClosePopup ClosePopupG" data-dismiss="modal" aria-label="Close"><i class="fa fa-times"></i></a><div class="panel"><div class="panel-body text-center"><div class="' + data.typexy + '"><div id="CsV" style="width:100%;display:none;"><div class="embed-responsive embed-responsive-16by9">' + data.video + '</div></div><img id="CsI" width="500px" class="img-responsive center-block"  src="' + data.url + '"/><p><span class="popupName">' + data.Name + '</span><br><span class="popupcation">' + data.typexy + '</span><br><span class="popuppbydate">#PBY since ' + data.PBYSince + '</span>' + data.insert + '</p></div><hr><br /><div class="' + data.typexy + '" id="MaxpopFolder"><span class="StoryTitlePopbig" id="StoryTitlePopbig"></span><span id="StoryTitlePopbigMenu"></span></div></div></div>';
                       

                        //    //map.panTo(
                        //    //    new L.LatLng(data.lat, data.lng), { animate: true });
                        //    $("#popupmainview .modal-dialog").html(template);
                        //    $("#popupmainview").modal();
                        //    $("#popupmainview").attr('style', 'padding-right:0px!important;display:block');
                          
                        //    maxpopMobile(data.Id);

                        //}
                    }
                
            }
            });
            leafletMarker.on('click', function() {


                var template = '<div class="{typexy} mapPOPup"><div id="CsV" style="width:100%;display:none;"><div class="embed-responsive embed-responsive-4by3">{video}</div></div><img id="CsI" src="{url}"/><p><span class="popupName">{Name}</span><br><span class="popupcation">{TypeName}</span><br><span class="popuppbydate">#PBY since {PBYSince}</span>{insert}<a title="See the full story!" onclick="maxpop({Id},250)" href="javascript:void(0);"><i class="fa fa-plus-circle Maparrows"></i></a></p></div><div class="{typexy} MaxpopFolder" id="MaxpopFolder"><span class="StoryTitlePopbig" id="StoryTitlePopbig"></span><span id="StoryTitlePopbigMenu"></span></div>';

                //if (data.video) {
                //    template = '<div class="{typexy} mapPOPup"></div><p><span class="popupName">{Name}</span><br><span class="popupcation">{TypeName}</span><br><span class="popuppbydate">#PBY since {PBYSince}</span>{insert}<a onclick="maxpop({Id})" href="javascript:void(0);"><span class="fa-stack fa-2x"><i class="fa fa-circle fa-stack-2x Maparrows"></i><i  id="Maparrows" class="fa fa-plus-circle fa-stack-1x fa-inverse"></i></span></a></p></div><div class="{typexy} MaxpopFolder" id="MaxpopFolder"><span class="StoryTitlePopbig" id="StoryTitlePopbig"></span><span id="StoryTitlePopbigMenu"></span></div>';

                //};
                if (vph > 768) {


                    map.panTo(
                        new L.LatLng(data.lat, data.lng), { animate: true });
                    localStorage.setItem("Latpop", data.lat);
                    localStorage.setItem("Lanpop", data.lng);

                    leafletMarker.bindPopup(L.Util.template(template, data), {
                        className: 'leaflet-popup-photo',
                        minWidth: 234,
                        maxWidth: 234,
                        autoPanPadding: new L.Point(0, 80),
                        fillColor: '#ffffff',
                        offset: new L.Point(1, -31)
                    }).openPopup();;
              
                        leafletMarker.data.forceIconRedraw = true;
                 
                 
                     pruneCluster.ProcessView();
                //    leafletMarker.openPopup();


                } else  {

                    template = '<a class="ClosePopup ClosePopupG" data-dismiss="modal" aria-label="Close"><i class="fa fa-times"></i></a><div class="panel"><div class="panel-body text-center"><div class="' + data.typexy + '"><div id="CsV" style="width:100%;display:none;"><div class="embed-responsive embed-responsive-16by9">' + data.video + '</div></div><img id="CsI" width="100%" class="img-responsive center-block"  src="' + data.url + '"/><i class="fa fa-arrow-left arrmap" aria-hidden="true"> swipe</i> <i class="fa fa-arrow-right arrmap" aria-hidden="true"></i><p><span class="popupName">' + data.Name + '</span><br><span class="popupcation">' + data.TypeName + '</span><br><span class="popuppbydate">#PBY since ' + data.PBYSince + '</span>' + data.insert + '</p></div><hr><br /><div class="' + data.typexy + '" id="MaxpopFolder"><span class="StoryTitlePopbig" id="StoryTitlePopbig"></span><span id="StoryTitlePopbigMenu"></span></div></div></div>';

              
                   // map.panTo(
                   //new L.LatLng(data.lat, data.lng), { animate: true });
                    $("#popupmainview .modal-dialog").html(template);
                    $("#popupmainview").modal();
                    $("#popupmainview").attr('style', 'padding-right:0px!important;display:block');


                    $(".spip").on("swiperight", function (event) {
                        if (!jQuery.active > 0) {

                            mapleftswp();

                        } else {
                            setTimeout(
                          function () {
                              mapleftswp();
                          }, 150);

                        }


                    });

                    $(".spip").on("swipeleft", function (event) {
                        if (!jQuery.active > 0) {

                            maprightswp();

                        } else {
                            setTimeout(
                          function () {
                              maprightswp();
                          }, 150);

                        }
                    });

                    maxpopMobile(data.Id);
                }
              


            });
        };


        var colors = ['#17B4E9', '#73669C', '#FDBA12'],
            pi2 = Math.PI * 2;

        L.Icon.MarkerCluster = L.Icon.extend({
            options: {
                iconSize: new L.Point(44, 44),
                className: 'prunecluster leaflet-markercluster-icon'
            },

            createIcon: function() {
                // based on L.Icon.Canvas from shramov/leaflet-plugins (BSD licence)
                var e = document.createElement('canvas');
                this._setIconStyles(e, 'icon');
                var s = this.options.iconSize;
                e.width = s.x;
                e.height = s.y;
                this.draw(e.getContext('2d'), s.x, s.y);
                return e;
            },

            createShadow: function() {
                return null;
            },

            draw: function(canvas, width, height) {

                var lol = 0;

                var start = 0;
                for (var i = 0, l = colors.length; i < l; ++i) {

                    var size = this.stats[i] / this.population;


                    if (size > 0) {
                        canvas.beginPath();
                        canvas.moveTo(22, 22);
                        canvas.fillStyle = colors[i];
                        var from = start + 0.14,
                            to = start + size * pi2;

                        if (to < from) {
                            from = start;
                        }
                        canvas.arc(22, 22, 22, from, to);

                        start = start + size * pi2;
                        canvas.lineTo(22, 22);
                        canvas.fill();
                        canvas.closePath();
                    }

                }

                canvas.beginPath();
                canvas.fillStyle = 'white';
                canvas.arc(22, 22, 18, 0, Math.PI * 2);
                canvas.fill();
                canvas.closePath();

                canvas.fillStyle = '#555';
                canvas.textAlign = 'center';
                canvas.textBaseline = 'middle';
                canvas.font = 'bold 12px sans-serif';

                canvas.fillText(this.population, 22, 22, 40);
            }
        });


        var southWest = L.latLng(84, 400),
            northEast = L.latLng(-90, -400),
            bounds = L.latLngBounds(southWest, northEast);

        var zoomZ = 2;
        var     minZoomZ = 2;

        if ($('#map').width() < 300) {
            zoomZ = 0;
            minZoomZ = 0;

        } else if ($('#map').width() < 500) {
            zoomZ = 1;
            minZoomZ = 1;
  
        } else if ($('#map').width() < 768) {
            zoomZ = 2;
            minZoomZ = 2;
        }
 

        map = L.map("map", {
            maxZoom: 18,
            zoom: zoomZ,
            minZoom: minZoomZ,
            center: [25, -5],
            layers: [mapquestOSM, pruneCluster],
            zoomControl: false,
            maxBounds: bounds,
            attributionControl: false
        });

        map.scrollWheelZoom.disable();
        map.on('focus', function() { map.scrollWheelZoom.enable(); });
        map.on('blur', function() { map.scrollWheelZoom.disable(); });
        map.on('zoomend', function () {
            pruneCluster.Cluster.Size = parseInt((map.getZoom() === 18) ? -1 : 160);
            pruneCluster.ProcessView();
                if (map.getZoom() < 18) {
                    map.closePopup();
                    map.options.maxZoom =17;
                }
        });




        map.on('popupclose', function(e) {
            $("#Id").val(null);
            window.history.pushState("YogaMap", "Title", '/YogaMap/');
        });

        L.control.zoom({
            position: 'topleft'
        }).addTo(map);

        resizeDivmap();
        var baseLayers = {
            "Street Map": mapquestOSM,
            "Aerial Imagery": mapquestOAM,
            "Imagery with Streets": mapquestHYB
        };

        if (document.body.clientWidth <= 767) {
            var isCollapsed = true;
        } else {
            var isCollapsed = false;
        }

        var layerControl = L.control.groupedLayers(baseLayers, {
            collapsed: isCollapsed
        }).addTo(map);


     var geocoder = L.Control.geocoder({
             defaultMarkGeocode: false
         });

     geocoder.markGeocode = function(e) {
         var bbox = e.bbox;
         var poly = L.polygon([
             bbox.getSouthEast(),
             bbox.getNorthEast(),
             bbox.getNorthWest(),
             bbox.getSouthWest()
         ]);
         map.fitBounds(poly.getBounds());
     };

        //var geocoder = L.Control.geocoder();

        geocoder.addTo(map);


       // geocoder.markGeocode = function(result) {
       //     this._map.fitBounds(result.bbox);
       ////     pruneCluster.FitBounds(result.bbox);
       // };

   
    //    map.addLayer(markers);
        L.Icon.Default.imagePath = '/images/svgIcons/';




        //$("#signupform").change(function () {

        //    var form = $(this);


        //    $.ajax({
        //        async: true,
        //        type: "post",
        //        url: '/YogaMap/Search',
        //        data: form.serialize(),
        //        success: function (data) {


        //            data = data.Stories;
        //            pruneCluster.RemoveMarkers();
        //            for (var i = 0; i < data.length; i++) {

        //                var photo = data[i];

        //                var x = new PruneCluster.Marker(photo.Lat, photo.Lng);
        //                x.data.lat = photo.Lat;
        //                x.data.lng = photo.Lng;
        //                x.data.url = photo.Image;
        //                x.data.PBYSince = photo.PBYSince;
        //                x.data.typexy = photo.typexy;
        //                x.data.Name = photo.Name;
        //                x.data.Id = photo.Id;
        //                x.data.video = photo.Video;
        //                x.data.insert = photo.Auth;

        //                switch (photo.typexy) {
        //                    case "Student":
        //                        x.category = 0;
        //                        break;
        //                    case "Teacher":
        //                        x.category = 1;
        //                        break;
        //                    case "Shop":
        //                        x.category = 2;
        //                        break;
        //                }

        //                pruneCluster.RegisterMarker(x);
        //            }

        //            pruneCluster.RedrawIcons();


        //            if (data.length === 0) {

        //                $('#Filererror').html("<div class='alert alert-success' role='alert'>No results found</div>");
        //            } else {
        //                //  map.fitBounds(pruneCluster.ComputeGlobalBounds());
        //                pruneCluster.FitBounds();

                    
        //                $("#right-nav").hide();
        //                $(".dark-overlay").hide();
        //                $('#Filererror').html("");
        //            }


        //        },
        //        Error: function (data) {

        //        }
        //    });
        //});


        //$('#signupform').submit(function(e) {
        //    e.preventDefault();

        //    var form = $(this);


        //    $.ajax({
        //        async: true,
        //        type: "post",
        //        url: '/YogaMap/Search',
        //        data: form.serialize(),
        //        success: function(data) {


        //            data = data.Stories;
        //            pruneCluster.RemoveMarkers();
        //            for (var i = 0; i < data.length; i++) {

        //                var photo = data[i];

        //                var x = new PruneCluster.Marker(photo.Lat, photo.Lng);
        //                x.data.lat = photo.Lat;
        //                x.data.lng = photo.Lng;
        //                x.data.url = photo.Image;
        //                x.data.PBYSince = photo.PBYSince;
        //                x.data.typexy = photo.typexy;
        //                x.data.Name = photo.Name;
        //                x.data.Id = photo.Id;
        //                x.data.video = photo.Video;
        //                x.data.insert = photo.Auth;
        //                switch (photo.typexy) {
        //                case "Student":
        //                    x.category = 0;
        //                    break;
        //                case "Teacher":
        //                    x.category = 1;
        //                    break;
        //                case "Shop":
        //                    x.category = 2;
        //                    break;
        //                }

        //                pruneCluster.RegisterMarker(x);
        //            }

        //            pruneCluster.RedrawIcons();


        //            if (data.length === 0) {

        //                $('#Filererror').html("<div class='alert alert-success' role='alert'>No results found</div>");
        //            } else {
        //                pruneCluster.FitBounds();

        //              //  map.fitBounds(pruneCluster.getBounds());
        //                $("#right-nav").hide();
        //                $(".dark-overlay").hide();
        //                $('#Filererror').html("");
        //            }


        //        },
        //        Error: function(data) {

        //        }
        //    });

        //});


        //$('.mapclear').click(function (e) {
           

        //        $.ajax({
        //            url: "/YogaMap/GetMapStories",
        //            type: 'post',
        //            async: true,
        //            success: function (data) {
                     

        //                    data = data.Stories;
        //                    pruneCluster.RemoveMarkers();
        //                    for (var i = 0; i < data.length; i++) {

        //                        var photo = data[i];

        //                        var x = new PruneCluster.Marker(photo.Lat, photo.Lng);
        //                        x.data.lat = photo.Lat;
        //                        x.data.lng = photo.Lng;
        //                        x.data.url = photo.Image;
        //                        x.data.PBYSince = photo.PBYSince;
        //                        x.data.typexy = photo.typexy;
        //                        x.data.Name = photo.Name;
        //                        x.data.Id = photo.Id;
        //                        x.data.video = photo.Video;
        //                        x.data.insert = photo.Auth;
        //                        switch (photo.typexy) {
        //                            case "Student":
        //                                x.category = 0;
        //                                break;
        //                            case "Teacher":
        //                                x.category = 1;
        //                                break;
        //                            case "Shop":
        //                                x.category = 2;
        //                                break;
        //                        }
        //                        pruneCluster.RegisterMarker(x);
        //                    }

        //                    pruneCluster.RedrawIcons();
        //                    pruneCluster.FitBounds();

   
        //                $('#Filererror').html("");
        //                $("#right-nav").hide();
        //                $(".dark-overlay").hide();
        //            },
        //            error: function () {

        //            }
        //        });
       
        //});

        //$('#EmailAddress').click(function (e) {
        //    $('#hiddenEmailB').slideDown();
        //    $('#GoemailB').removeClass("disabled");

        //});

   

    /* GPS enabled geolocation control set to follow the user's location */
    //var locateControl = L.control.locate({
    //    position: "topleft",
    //    drawCircle: true,

    //    setView: true,
    //    duration: 34,
    //    keepCurrentZoomLevel: false,
    //    markerStyle: {
    //        weight: 1,
    //        opacity: 0.9,
    //        fillOpacity: 0.9,
    //        color: '#C3225C',
    //        fillColor: '#F25EAA'

    //    },
    //    circleStyle: {
    //        weight: 1,
    //        clickable: false,
    //        color: '#C3225C',
    //        fillColor: '#F25EAA'
    //    },
    //    icon: "fa fa-location-arrow",
    //    metric: false,
    //    strings: {
    //        title: "My location",
    //        popup: "You are within {distance} {unit} from this point",
    //        outsideMapBoundsMsg: "You seem located outside the boundaries of the map"
    //    },
    //    locateOptions: {
    //        maxZoom: 18,
    //        watch: true,
    //        enableHighAccuracy: true,
    //        maximumAge: 10,
    //        timeout: 100000
    //    }
    //}).addTo(map);
      



    
   

     $.ajax({
         url: "/YogaMap/GetMapStories/",
         type: 'post',
     
         async: true,
         success: function(data) {


             storysStored = new Array(data.Stories.length);
             //   var lat;

             for (var i = 0; i < data.Stories.length; i++) {

                 var photo = data.Stories[i];

                 var x = new PruneCluster.Marker(photo.Lat, photo.Lng);
                 // 
                 x.data.lat = photo.Lat;
                 x.data.lng = photo.Lng;
                 x.data.url = photo.Image;
                 x.data.PBYSince = photo.PBYSince;
                 x.data.typexy = photo.typexy;
                 x.data.Name = photo.Name;
                 x.data.Id = photo.Id;
                 x.data.video = photo.Video;
                 x.data.insert = photo.Auth;
                 x.data.story = photo.SmallDesc;
                 switch (photo.typexy) {
                     case "Student":
                         x.category = 0;
                         x.data.TypeName = "Student";
                         break;
                     case "Teacher":
                         x.category = 1;
                         x.data.TypeName = "Teacher";
                         break;
                     case "Shop":
                         x.category = 2;
                         x.data.TypeName = "Designer/Creator";
                         break;
                 }

                 pruneCluster.RegisterMarker(x);
                 //if ($("#Lng").length < 1) {

                 //   

                 //} else {

                 //    if ($("#Id").val().toString() === x.data.Id.toString()) {
                 //        x.data.open = "true";
                 //          lat = new L.LatLng(x.data.lat, x.data.lng);
                 //    }


                 //    pruneCluster.RegisterMarker(x);

                 // //   map.options.maxZoom = 18;


                 // //   pruneCluster.ProcessView();


                 //}

                 if (x.data.story !== '...') {
                     storysStored[i] = x;

                 }
                 //    


             }

             //if ($("#Lng").length < 1) {
             //    map.setView(lat, 18);
             //}


             pruneCluster.RedrawIcons();

             $('#Map-latest').addClass("MapNavSelected");

             if ((typeof ($("#Id").val()) !== "undefined")) {


                 var vph = $('#map').width();
                 if (vph > 768) {
                     var latt;

                     $('html, body').animate({
                         scrollTop: $("#MapTitle").offset().top
                     }, 1000);

                     var z = pruneCluster.GetMarkers();
                     for (var c = 0; c < z.length; c++) {

                         if (z[c].data.Id.toString() === $("#Id").val().toString()) {

                             var y = z[c];
                             pruneCluster.RemoveMarkers([y]);
                             y.data.open = "true";
                             pruneCluster.RegisterMarker(y);
                             latt = new L.LatLng(y.data.lat, y.data.lng);


                         }
                     }


                     pruneCluster.ProcessView();
                     map.options.maxZoom = 18;
                     map.setView(latt, 18);

                 } else {

                     for (var j = 0; j < storysStored.length; j++) {

                         if ((typeof (storysStored[j]) !== "undefined")) {

                             if (storysStored[j].data.Id.toString() === $("#Id").val().toString()) {

                                 var template = '<a class="ClosePopup ClosePopupG" data-dismiss="modal" aria-label="Close"><i class="fa fa-times"></i></a><div class="panel"><div class="panel-body text-center"><div class="' + storysStored[j].data.typexy + '"><div id="CsV" style="width:100%;display:none;"><div class="embed-responsive embed-responsive-16by9">' + storysStored[j].data.video + '</div></div><img id="CsI" width="100%" class="img-responsive center-block"  src="' + storysStored[j].data.url + '"/><i class="fa fa-arrow-left arrmap" aria-hidden="true"> swipe</i> <i class="fa fa-arrow-right arrmap" aria-hidden="true"></i><p><span class="popupName">' + storysStored[j].data.Name + '</span><br><span class="popupcation">' + storysStored[j].data.TypeName+ '</span><br><span class="popuppbydate">#PBY since ' + storysStored[j].data.PBYSince + '</span>' + storysStored[j].data.insert + '</p></div><hr><br /><div class="' + storysStored[j].data.typexy + '" id="MaxpopFolder"><span class="StoryTitlePopbig" id="StoryTitlePopbig"></span><span id="StoryTitlePopbigMenu"></span></div></div></div>';

                                 $("#popupmainview .modal-dialog").html(template);
                                 $("#popupmainview").modal();
                                 $("#popupmainview").attr('style', 'padding-right:0px!important;display:block');

                                 storysMarkerSwp = j;
                                 $(".spip").on("swiperight", function (event) {
                                     if (!jQuery.active > 0) {

                                         mapleftswp();

                                     } else {
                                         setTimeout(
                                       function () {
                                           mapleftswp();
                                       }, 150);

                                     }


                                 });

                                 $(".spip").on("swipeleft", function (event) {
                                     if (!jQuery.active > 0) {

                                         maprightswp();

                                     } else {
                                         setTimeout(
                                       function () {
                                           maprightswp();
                                       }, 150);

                                     }
                                 });


                                 maxpopMobile(storysStored[j].data.Id);
                             }
                         }
                     }


                 }
                 $("#Id").remove();

             }
                 $(window).scroll(function() {
                     if ($('.no-more-styles:hidden').length > 0) {
                         if ($(window).scrollTop() + $(window).height() > $('#StorysContainer').height() - 1000) {
                             if ($('.loading-bar:hidden').length > 0) {
                                 $('.loading-bar').show();


                                 if (storysStored.length > storysMarker) {

                                 } else {
                                     $('.no-more-styles').show();
                                 }
                                 $('.loading-bar').hide();
                                 navpopulateStorys();
                             };
                         };
                     };
                 });
                 navpopulateStorys();
           

         }
     });




    $('#AddStoryMoreInfo').click(function (e) {

        $(".spip").unbind('swiperight');
        $(".spip").unbind('swipeleft');
        var url = "/YogaMap/AddStoryPopUp/";
        $.get(url, function (data) {
            $("#modellg").html(data);
            $("#popupmain").modal({ backdrop: 'static' });
            $("#popupmain").css("padding-right", "0");
        });
    });





  
    $('#AddStorymini').click(function (e) {

        $(".spip").unbind('swiperight');
        $(".spip").unbind('swipeleft');
        var url = "/YogaMap/AddStoryPopUp/";
        $.get(url, function (data) {
            $("#modellg").html(data);
            $("#popupmain").modal({ backdrop: 'static' });
            $("#popupmain").css("padding-right", "0");
        });
 
 

    });
  $('#MapToggle').click(function (e) {
      $('#map').toggleClass("MapTog");
      if ($('#map:hidden').length > 0) {
         
          $('#MapToggle').html("SHOW<br />MAP");
      } else {
          $('#MapToggle').html("HIDE<br />MAP");
      }
      $('html, body').animate({
          scrollTop: $("#LogoMain").offset().top

      }, 1000);
  });

   
  if ($( window ).width() < 751) {
      $('#map').toggleClass("MapTog");
     }

   

 });

function NavSelectedReset() {
    $('#Map-latest').removeClass("MapNavSelected");
    $('#Map-YogaMeds').removeClass("MapNavSelected");
    $('#Map-Locations').removeClass("MapNavSelected");
    $('#Map-Challenges').removeClass("MapNavSelected");
    

    $('#Map-Students').removeClass("MapNavSelected");
    $('#Map-Teachers').removeClass("MapNavSelected");
    $('#Map-Designers').removeClass("MapNavSelected");
    $('#Map-Designersmini').removeClass("MapNavSelected");
    $('#Map-latestmini').removeClass("MapNavSelected");
    $('#Map-YogaMedsmini').removeClass("MapNavSelected");
    $('#Map-Locationsmini').removeClass("MapNavSelected");
    $('#Map-Challengesmini').removeClass("MapNavSelected");
    
    $('#Map-Studentsmini').removeClass("MapNavSelected");
    $('#Map-Teachersmini').removeClass("MapNavSelected");
    $('#StorysContainer').html("");
    $('.no-more-styles').hide();
    $('#StoryLocations').hide();
    $('#StoryChallenges').hide();
    

}


$('#Map-latest').click(function(e) {
    e.preventDefault();
    NavSelectedReset();
    $(this).addClass("MapNavSelected");
    pruneCluster.RemoveMarkers();
    $.ajax({
        url: "/YogaMap/GetMapStories/",
        type: 'post',
        async: true,
        success: function (data) {


            storysStored = new Array(data.Stories.length);


            for (var i = 0; i < data.Stories.length; i++) {

                var photo = data.Stories[i];

                var x = new PruneCluster.Marker(photo.Lat, photo.Lng);
                x.data.lat = photo.Lat;
                x.data.lng = photo.Lng;
                x.data.url = photo.Image;
                x.data.PBYSince = photo.PBYSince;
                x.data.typexy = photo.typexy;
                x.data.Name = photo.Name;
                x.data.Id = photo.Id;
                x.data.video = photo.Video;
                x.data.insert = photo.Auth;
                x.data.story = photo.SmallDesc;
                switch (photo.typexy) {
                    case "Student":
                        x.category = 0;
                        x.data.TypeName= "Student";
                        break;
                    case "Teacher":
                        x.category = 1;
                        x.data.TypeName= "Teacher";
                        break;
                    case "Shop":
                        x.category = 2;
                        x.data.TypeName = "Designer/Creator";
                        break;
                }



                pruneCluster.RegisterMarker(x);


                if (x.data.story !== '...') {
                    storysStored[i] = x;
                }


            }



            pruneCluster.RedrawIcons();
            storysMarker = 0;
            map.closePopup();
            pruneCluster.FitBounds();
            navpopulateStorys();
        

        }

    });
  

});
$('#Map-YogaMeds').click(function(e) {
    e.preventDefault();
    NavSelectedReset();
    $(this).addClass("MapNavSelected");
    pruneCluster.RemoveMarkers();
    $.ajax({
        url: "/YogaMap/GetMapStoriesConditions/",
        type: 'post',
        async: true,
        success: function (data) {


            storysStored = new Array(data.Stories.length);


            for (var i = 0; i < data.Stories.length; i++) {

                var photo = data.Stories[i];

                var x = new PruneCluster.Marker(photo.Lat, photo.Lng);
                x.data.lat = photo.Lat;
                x.data.lng = photo.Lng;
                x.data.url = photo.Image;
                x.data.PBYSince = photo.PBYSince;
                x.data.typexy = photo.typexy;
                x.data.Name = photo.Name;
                x.data.Id = photo.Id;
                x.data.video = photo.Video;
                x.data.insert = photo.Auth;
                x.data.story = photo.SmallDesc;
                switch (photo.typexy) {
                    case "Student":
                        x.category = 0;
                        x.data.TypeName= "Student";
                        break;
                    case "Teacher":
                        x.category = 1;
                        x.data.TypeName= "Teacher";
                        break;
                    case "Shop":
                        x.category = 2;
                        x.data.TypeName = "Designer/Creator";
                        break;
                }



                pruneCluster.RegisterMarker(x);


                if (x.data.story !== '...') {
                    storysStored[i] = x;
                }


            }

            map.closePopup();

         //   pruneCluster.RedrawIcons();
            storysMarker = 0;
            pruneCluster.ProcessView();
           // pruneCluster.FitBounds({ margin: [500 ,500] });
            map.fitWorld();
            navpopulateStorys();
       

        }

    });
  
});
$('#Map-Locations').click(function(e) {
    e.preventDefault();
    NavSelectedReset();
    $(this).addClass("MapNavSelected"); $('[data-LocationMap]').each(function () {
        $(this).removeClass("Locationop");
    });
    $('#StorysContainer').html("");
    $('.no-more-styles').show();
    $('#StoryLocations').show();

});



$('#Map-Challenges').click(function (e) {
    e.preventDefault();
    NavSelectedReset();
    $(this).addClass("MapNavSelected"); $('[data-LocationMap]').each(function () {
        $(this).removeClass("Locationop");
    });
    $('#StorysContainer').html("");
    $('.no-more-styles').show();
    $('#StoryChallenges').show();

});


$('#Map-Students').click(function(e) {
    e.preventDefault();
    NavSelectedReset();
    $(this).addClass("MapNavSelected");
    pruneCluster.RemoveMarkers();
    $.ajax({
        url: "/YogaMap/Search/",
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify({ TSreturn: "Student" }),
        async: true,
        success: function (data) {


            storysStored = new Array(data.Stories.length);


            for (var i = 0; i < data.Stories.length; i++) {

                var photo = data.Stories[i];

                var x = new PruneCluster.Marker(photo.Lat, photo.Lng);
                x.data.lat = photo.Lat;
                x.data.lng = photo.Lng;
                x.data.url = photo.Image;
                x.data.PBYSince = photo.PBYSince;
                x.data.typexy = photo.typexy;
                x.data.Name = photo.Name;
                x.data.Id = photo.Id;
                x.data.video = photo.Video;
                x.data.insert = photo.Auth;
                x.data.story = photo.SmallDesc;
                switch (photo.typexy) {
                    case "Student":
                        x.category = 0;
                        x.data.TypeName= "Student";
                        break;
                    case "Teacher":
                        x.category = 1;
                        x.data.TypeName= "Teacher";
                        break;
                    case "Shop":
                        x.category = 2;
                        x.data.TypeName = "Designer/Creator";
                        break;
                }



                pruneCluster.RegisterMarker(x);


                if (x.data.story !== '...') {
                    storysStored[i] = x;
                }


            }



            pruneCluster.RedrawIcons();
            storysMarker = 0;
            map.closePopup();
            map.fitWorld();
            navpopulateStorys();


        }

    });
  
});
$('#Map-Teachers').click(function(e) {
    e.preventDefault();
    NavSelectedReset();
    $(this).addClass("MapNavSelected");
    pruneCluster.RemoveMarkers();
    $.ajax({
        url: "/YogaMap/Search/",
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify({ TSreturn: "Teacher" }),
        async: true,
        success: function (data) {


            storysStored = new Array(data.Stories.length);


            for (var i = 0; i < data.Stories.length; i++) {

                var photo = data.Stories[i];

                var x = new PruneCluster.Marker(photo.Lat, photo.Lng);
                x.data.lat = photo.Lat;
                x.data.lng = photo.Lng;
                x.data.url = photo.Image;
                x.data.PBYSince = photo.PBYSince;
                x.data.typexy = photo.typexy;
                x.data.Name = photo.Name;
                x.data.Id = photo.Id;
                x.data.video = photo.Video;
                x.data.insert = photo.Auth;
                x.data.story = photo.SmallDesc;
                switch (photo.typexy) {
                    case "Student":
                        x.category = 0;
                        x.data.TypeName= "Student";
                        break;
                    case "Teacher":
                        x.category = 1;
                        x.data.TypeName= "Teacher";
                        break;
                    case "Shop":
                        x.category = 2;
                        x.data.TypeName = "Designer/Creator";
                        break;
                }



                pruneCluster.RegisterMarker(x);


                if (x.data.story !== '...') {
                    storysStored[i] = x;
                }


            }


            pruneCluster.RedrawIcons();
            storysMarker = 0;
            map.closePopup();
            map.fitWorld();
            navpopulateStorys();


        }

    });
});
$('#Map-Designers').click(function (e) {
    e.preventDefault();
    NavSelectedReset();
    $(this).addClass("MapNavSelected");
    pruneCluster.RemoveMarkers();
    $.ajax({
        url: "/YogaMap/Search/",
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify({ TSreturn: "Shop" }),
        async: true,
        success: function (data) {


            storysStored = new Array(data.Stories.length);


            for (var i = 0; i < data.Stories.length; i++) {

                var photo = data.Stories[i];

                var x = new PruneCluster.Marker(photo.Lat, photo.Lng);
                x.data.lat = photo.Lat;
                x.data.lng = photo.Lng;
                x.data.url = photo.Image;
                x.data.PBYSince = photo.PBYSince;
                x.data.typexy = photo.typexy;
                x.data.Name = photo.Name;
                x.data.Id = photo.Id;
                x.data.video = photo.Video;
                x.data.insert = photo.Auth;
                x.data.story = photo.SmallDesc;
                switch (photo.typexy) {
                    case "Student":
                        x.category = 0;
                        x.data.TypeName= "Student";
                        break;
                    case "Teacher":
                        x.category = 1;
                        x.data.TypeName= "Teacher";
                        break;
                    case "Shop":
                        x.category = 2;
                        x.data.TypeName = "Designer/Creator";
                        break;
                }


                pruneCluster.RegisterMarker(x);


                if (x.data.story !== '...') {
                    storysStored[i] = x;
                }


            }


            pruneCluster.RedrawIcons();
            storysMarker = 0;
            map.closePopup();
            map.fitWorld();
            navpopulateStorys();


        }

    });
});
$('#right-nav').append("<div id='navwrap'><section id='filter-results' class='filters'><div id='filter-wrap'><div class='the-sub-menu'><span aria-label='Close' class='close filter-icon pull-right hidden-sm hidden-md hidden-lg' title='Close' id='right-menu-close'><span aria-hidden='true'>×</span></span><h1 class='pageTitle'>FILTER STORIES</h1><nav class='filter active'><a id='Map-latestmini' href='#'><span class='MapTab'>Latest Stories</span></a><a id='Map-YogaMedsmini' href='#'><span class='MapTab'>YogaMeds</span></a><a id='Map-Locationsmini' href='#'><span class='MapTab'>Featured Locations</span></a><a id='Map-Challengesmini' href='#'><span class='MapTab'>Life Challenges</span></a><a id='Map-Studentsmini' href='#'><span class='MapTab'>Students</span></a><a id='Map-Teachersmini' href='#'><span class='MapTab'>Teachers</span></a><a id='Map-Designersmini' href='#'><span class='MapTab'>Designers/Creators</span></a></nav></div></div></section><br><br></div>");
$('#Map-latestmini').click(function (e) {
    e.preventDefault();
    NavSelectedReset();
    $(this).addClass("MapNavSelected");
    $('#MenuMapMin').html("");
    pruneCluster.RemoveMarkers();
    $.ajax({
        url: "/YogaMap/GetMapStories/",
        type: 'post',
        async: true,
        success: function (data) {


            storysStored = new Array(data.Stories.length);


            for (var i = 0; i < data.Stories.length; i++) {

                var photo = data.Stories[i];

                var x = new PruneCluster.Marker(photo.Lat, photo.Lng);
                x.data.lat = photo.Lat;
                x.data.lng = photo.Lng;
                x.data.url = photo.Image;
                x.data.PBYSince = photo.PBYSince;
                x.data.typexy = photo.typexy;
                x.data.Name = photo.Name;
                x.data.Id = photo.Id;
                x.data.video = photo.Video;
                x.data.insert = photo.Auth;
                x.data.story = photo.SmallDesc;
                switch (photo.typexy) {
                    case "Student":
                        x.category = 0;
                        x.data.TypeName= "Student";
                        break;
                    case "Teacher":
                        x.category = 1;
                        x.data.TypeName= "Teacher";
                        break;
                    case "Shop":
                        x.category = 2;
                        x.data.TypeName = "Designer/Creator";
                        break;
                }



                pruneCluster.RegisterMarker(x);


                if (x.data.story !== '...') {
                    storysStored[i] = x;
                }


            }



            pruneCluster.RedrawIcons();
            storysMarker = 0;
            map.closePopup();
            pruneCluster.FitBounds();
            navpopulateStorys();
            $("#right-nav").fadeToggle("fast");
            $(".dark-overlay").fadeToggle("slow");

        }

    });


});
$('#Map-YogaMedsmini').click(function (e) {
    e.preventDefault();
    NavSelectedReset();
    $(this).addClass("MapNavSelected");
    $('#MenuMapMin').html("<a id='miniresetfilet' href='#'>YogaMeds&nbsp;&nbsp;x</a>");
    pruneCluster.RemoveMarkers();
    $.ajax({
        url: "/YogaMap/GetMapStoriesConditions/",
        type: 'post',
        async: true,
        success: function (data) {


            storysStored = new Array(data.Stories.length);


            for (var i = 0; i < data.Stories.length; i++) {

                var photo = data.Stories[i];

                var x = new PruneCluster.Marker(photo.Lat, photo.Lng);
                x.data.lat = photo.Lat;
                x.data.lng = photo.Lng;
                x.data.url = photo.Image;
                x.data.PBYSince = photo.PBYSince;
                x.data.typexy = photo.typexy;
                x.data.Name = photo.Name;
                x.data.Id = photo.Id;
                x.data.video = photo.Video;
                x.data.insert = photo.Auth;
                x.data.story = photo.SmallDesc;
                switch (photo.typexy) {
                    case "Student":
                        x.category = 0;
                        x.data.TypeName= "Student";
                        break;
                    case "Teacher":
                        x.category = 1;
                        x.data.TypeName= "Teacher";
                        break;
                    case "Shop":
                        x.category = 2;
                        x.data.TypeName = "Designer/Creator";
                        break;
                }


                pruneCluster.RegisterMarker(x);


                if (x.data.story !== '...') {
                    storysStored[i] = x;
                }


            }



            pruneCluster.RedrawIcons();
            storysMarker = 0;
            map.closePopup();
            pruneCluster.FitBounds();
            navpopulateStorys();
            $("#right-nav").fadeToggle("fast");
            $(".dark-overlay").fadeToggle("slow");

        }

    });

});
$('#Map-Locationsmini').click(function (e) {
    $('#MenuMapMin').html("<a id='miniresetfilet' href='#'>Featured Locations&nbsp;&nbsp;x</a>");
    e.preventDefault();
    NavSelectedReset();
    $(this).addClass("MapNavSelected");
    $('#StorysContainer').html("");
    $('.no-more-styles').show();
    $('#StoryLocations').show();
    $('[data-LocationMap]').each(function () {
        $(this).removeClass("Locationop");
    });
    $("#right-nav").fadeToggle("fast");
    $(".dark-overlay").fadeToggle("slow");



});

$('#Map-Challengesmini').click(function (e) {
    $('#MenuMapMin').html("<a id='miniresetfilet' href='#'>Life Challenges&nbsp;&nbsp;x</a>");
    e.preventDefault();
    NavSelectedReset();
    $(this).addClass("MapNavSelected");
    $('#StorysContainer').html("");
    $('.no-more-styles').show();
    $('#StoryChallenges').show();
    $('[data-LocationMap]').each(function () {
        $(this).removeClass("Locationop");
    });
    $("#right-nav").fadeToggle("fast");
    $(".dark-overlay").fadeToggle("slow");



});
$('#Map-Studentsmini').click(function (e) {
    e.preventDefault();
    NavSelectedReset();
    $(this).addClass("MapNavSelected");
    $('#MenuMapMin').html("<a id='miniresetfilet' href='#'>Students&nbsp;&nbsp;x</a>");
    pruneCluster.RemoveMarkers();
    $.ajax({
        url: "/YogaMap/Search/",
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify({ TSreturn: "Student" }),
        async: true,
        success: function (data) {


            storysStored = new Array(data.Stories.length);


            for (var i = 0; i < data.Stories.length; i++) {

                var photo = data.Stories[i];

                var x = new PruneCluster.Marker(photo.Lat, photo.Lng);
                x.data.lat = photo.Lat;
                x.data.lng = photo.Lng;
                x.data.url = photo.Image;
                x.data.PBYSince = photo.PBYSince;
                x.data.typexy = photo.typexy;
                x.data.Name = photo.Name;
                x.data.Id = photo.Id;
                x.data.video = photo.Video;
                x.data.insert = photo.Auth;
                x.data.story = photo.SmallDesc;
                switch (photo.typexy) {
                    case "Student":
                        x.category = 0;
                        x.data.TypeName= "Student";
                        break;
                    case "Teacher":
                        x.category = 1;
                        x.data.TypeName= "Teacher";
                        break;
                    case "Shop":
                        x.category = 2;
                        x.data.TypeName = "Designer/Creator";
                        break;
                }



                pruneCluster.RegisterMarker(x);


                if (x.data.story !== '...') {
                    storysStored[i] = x;
                }


            }



            pruneCluster.RedrawIcons();
            storysMarker = 0;
            map.closePopup();
            pruneCluster.FitBounds();
            navpopulateStorys();
            $("#right-nav").fadeToggle("fast");
            $(".dark-overlay").fadeToggle("slow");

        }

    });

});
$('#Map-Teachersmini').click(function (e) {
    e.preventDefault();
    NavSelectedReset();
    $(this).addClass("MapNavSelected");
    $('#MenuMapMin').html("<a id='miniresetfilet' href='#'>Teachers&nbsp;&nbsp;x</a>");
    pruneCluster.RemoveMarkers();
    $.ajax({
        url: "/YogaMap/Search/",
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify({ TSreturn: "Teacher" }),
        async: true,
        success: function (data) {


            storysStored = new Array(data.Stories.length);


            for (var i = 0; i < data.Stories.length; i++) {

                var photo = data.Stories[i];

                var x = new PruneCluster.Marker(photo.Lat, photo.Lng);
                x.data.lat = photo.Lat;
                x.data.lng = photo.Lng;
                x.data.url = photo.Image;
                x.data.PBYSince = photo.PBYSince;
                x.data.typexy = photo.typexy;
                x.data.Name = photo.Name;
                x.data.Id = photo.Id;
                x.data.video = photo.Video;
                x.data.insert = photo.Auth;
                x.data.story = photo.SmallDesc;
                switch (photo.typexy) {
                    case "Student":
                        x.category = 0;
                        x.data.TypeName= "Student";
                        break;
                    case "Teacher":
                        x.category = 1;
                        x.data.TypeName= "Teacher";
                        break;
                    case "Shop":
                        x.category = 2;
                        x.data.TypeName = "Designer/Creator";
                        break;
                }

                pruneCluster.RegisterMarker(x);


                if (x.data.story !== '...') {
                    storysStored[i] = x;
                }


            }


            pruneCluster.RedrawIcons();
            storysMarker = 0;
            map.closePopup();
            pruneCluster.FitBounds();
            navpopulateStorys();
            $("#right-nav").fadeToggle("fast");
            $(".dark-overlay").fadeToggle("slow");

        }

    });
});
$('#Map-Designersmini').click(function (e) {
    e.preventDefault();
    NavSelectedReset();
    $(this).addClass("MapNavSelected");
    $('#MenuMapMin').html("<a id='miniresetfilet' href='#'>Designers/Creators&nbsp;&nbsp;x</a>");
    pruneCluster.RemoveMarkers();
    $.ajax({
        url: "/YogaMap/Search/",
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify({ TSreturn: "Shop" }),
        async: true,
        success: function (data) {


            storysStored = new Array(data.Stories.length);


            for (var i = 0; i < data.Stories.length; i++) {

                var photo = data.Stories[i];

                var x = new PruneCluster.Marker(photo.Lat, photo.Lng);
                x.data.lat = photo.Lat;
                x.data.lng = photo.Lng;
                x.data.url = photo.Image;
                x.data.PBYSince = photo.PBYSince;
                x.data.typexy = photo.typexy;
                x.data.Name = photo.Name;
                x.data.Id = photo.Id;
                x.data.video = photo.Video;
                x.data.insert = photo.Auth;
                x.data.story = photo.SmallDesc;
                switch (photo.typexy) {
                    case "Student":
                        x.category = 0;
                        x.data.TypeName= "Student";
                        break;
                    case "Teacher":
                        x.category = 1;
                        x.data.TypeName= "Teacher";
                        break;
                    case "Shop":
                        x.category = 2;
                        x.data.TypeName = "Designer/Creator";
                        break;
                }


                pruneCluster.RegisterMarker(x);


                if (x.data.story !== '...') {
                    storysStored[i] = x;
                }


            }


            pruneCluster.RedrawIcons();
            storysMarker = 0;
            map.closePopup();
            map.fitWorld();
            navpopulateStorys();
            $("#right-nav").fadeToggle("fast");
            $(".dark-overlay").fadeToggle("slow");


        }

    });
});
$('[data-LocationMap]').each(function () {

    $(this).click(function () {
        $('[data-LocationMap]').each(function () {
            $(this).removeClass("Locationop");
        });
        var location;
        var distance;
        $(this).addClass("Locationop");
        switch ($(this).attr("data-LocationMap")) {
            case "MA":
                location = new L.LatLng("53.599025","-1.867676");
                distance = 51000;
            break;
            case "CL":
                location = new L.LatLng("34.994004","-118.674316");
                distance = 681000;
                break;
            case "NO":
                location = new L.LatLng("52.745991","1.089020");
                distance = 45000;
                break;
            case "LO":
                location = new L.LatLng("51.512161","-0.134583");
                distance = 32000;
                break;
                }

        pruneCluster.RemoveMarkers();
        $('#StorysContainer').html("");
        $('.no-more-styles').hide();
        $.ajax({
            url: "/YogaMap/GetMapStoriesByArea/",
            type: 'post',

            async: true,
            success: function (data) {


                storysStored = new Array(data.Stories.length);


                for (var i = 0; i < data.Stories.length; i++) {

                    var photo = data.Stories[i];

                    var x = new PruneCluster.Marker(photo.Lat, photo.Lng);
                    x.data.lat = photo.Lat;
                    x.data.lng = photo.Lng;
                    x.data.url = photo.Image;
                    x.data.PBYSince = photo.PBYSince;
                    x.data.typexy = photo.typexy;
                    x.data.Name = photo.Name;
                    x.data.Id = photo.Id;
                    x.data.video = photo.Video;
                    x.data.insert = photo.Auth;
                    x.data.story = photo.SmallDesc;
                    switch (photo.typexy) {
                        case "Student":
                            x.category = 0;
                            x.data.TypeName= "Student";
                            break;
                        case "Teacher":
                            x.category = 1;
                            x.data.TypeName= "Teacher";
                            break;
                        case "Shop":
                            x.category = 2;
                            x.data.TypeName = "Designer/Creator";
                            break;
                    }



                    if (new L.LatLng(x.data.lat, x.data.lng).distanceTo(location) < distance) {
                        pruneCluster.RegisterMarker(x);
                        if (x.data.story !== '...') {
                            storysStored[i] = x;
                        }
                    }


                    


                }

                pruneCluster.RedrawIcons();
                storysMarker = 0;
                map.closePopup();
          
                pruneCluster.FitBounds();
                navpopulateStorys();

               // map.fitWorld();
           

            }

        });
    });
});
$('[data-challengesmap]').each(function () {

    $(this).click(function () {
        $('[data-challengesmap]').each(function () {
            $(this).removeClass("Locationop");
        });
  
        $(this).addClass("Locationop");

        pruneCluster.RemoveMarkers();
        $('#StorysContainer').html("");
        $('.no-more-styles').hide();
        $.ajax({
            url: "/YogaMap/GetMapStoriesByBucket/",
            type: 'post',

            contentType: 'application/json',
            data: JSON.stringify({ Bucket: $(this).attr("data-challengesmap") }),
            async: true,
     
            success: function (data) {


                storysStored = new Array(data.Stories.length);


                for (var i = 0; i < data.Stories.length; i++) {

                    var photo = data.Stories[i];

                    var x = new PruneCluster.Marker(photo.Lat, photo.Lng);
                    x.data.lat = photo.Lat;
                    x.data.lng = photo.Lng;
                    x.data.url = photo.Image;
                    x.data.PBYSince = photo.PBYSince;
                    x.data.typexy = photo.typexy;
                    x.data.Name = photo.Name;
                    x.data.Id = photo.Id;
                    x.data.video = photo.Video;
                    x.data.insert = photo.Auth;
                    x.data.story = photo.SmallDesc;
                    switch (photo.typexy) {
                        case "Student":
                            x.category = 0;
                            x.data.TypeName = "Student";
                            break;
                        case "Teacher":
                            x.category = 1;
                            x.data.TypeName = "Teacher";
                            break;
                        case "Shop":
                            x.category = 2;
                            x.data.TypeName = "Designer/Creator";
                            break;
                    }



                

                    pruneCluster.RegisterMarker(x);


                    if (x.data.story !== '...') {
                        storysStored[i] = x;
                    }



                }

                pruneCluster.RedrawIcons();
                storysMarker = 0;

                map.closePopup();
                map.fitWorld();
                navpopulateStorys();



    
                // map.fitWorld();


            }

        });
    });
});

$('#MenuMapMin').click(function (e) {
    e.preventDefault();
    NavSelectedReset();
    pruneCluster.RemoveMarkers();
    $("#MenuMapMin").html("");
    $('#Map-latestmini').addClass("MapNavSelected");
    $.ajax({
        url: "/YogaMap/GetMapStories/",
        type: 'post',
        async: true,
        success: function (data) {


            storysStored = new Array(data.Stories.length);


            for (var i = 0; i < data.Stories.length; i++) {

                var photo = data.Stories[i];

                var x = new PruneCluster.Marker(photo.Lat, photo.Lng);
                x.data.lat = photo.Lat;
                x.data.lng = photo.Lng;
                x.data.url = photo.Image;
                x.data.PBYSince = photo.PBYSince;
                x.data.typexy = photo.typexy;
                x.data.Name = photo.Name;
                x.data.Id = photo.Id;
                x.data.video = photo.Video;
                x.data.insert = photo.Auth;
                x.data.story = photo.SmallDesc;
                switch (photo.typexy) {
                    case "Student":
                        x.category = 0;
                        x.data.TypeName= "Student";
                        break;
                    case "Teacher":
                        x.category = 1;
                        x.data.TypeName= "Teacher";
                        break;
                    case "Shop":
                        x.category = 2;
                        x.data.TypeName = "Designer/Creator";
                        break;
                }


                pruneCluster.RegisterMarker(x);


                if (x.data.story !== '...') {
                    storysStored[i] = x;
                }


            }



            pruneCluster.RedrawIcons();
            storysMarker = 0;
            map.closePopup();
            pruneCluster.FitBounds();
            navpopulateStorys();


        }

    });


});

