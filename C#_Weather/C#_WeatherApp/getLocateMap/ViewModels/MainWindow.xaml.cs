﻿using CefSharp;
using MahApps.Metro.Controls;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using getLocateMap.Logics;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using RestSharp;
using System.Net.Http;
using System.Threading.Tasks;

namespace getLocateMap
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        #region < 카카오 관련 API > 
        //public async Task<string> GetKakaoMapScriptAsync()
        //{
        //    string url = "https://dapi.kakao.com/v2/maps/sdk.js?appkey=a71fd4e07d867d250dec31c7fa8e7742";

        //    using (HttpClient client = new HttpClient())
        //    {
        //        HttpResponseMessage response = await client.GetAsync(url);
        //        response.EnsureSuccessStatusCode();

        //        string script = await response.Content.ReadAsStringAsync();
        //        return script;
        //    }
        //}
        // 일반 카카오 지도
        string KakaoAPI = $@"<!DOCTYPE html>
                                <html>
                                <head>
	                                <meta charset=""utf-8""/>
	                                <title>Kakao 지도 시작하기</title>
                                </head>
                                <script>
                                    window.kakao=window.kakao||{{}},window.kakao.maps=window.kakao.maps||{{}},window.daum&&window.daum.maps?window.kakao.maps=window.daum.maps:(window.daum=window.daum||{{}},window.daum.maps=window.kakao.maps),function(){{function a(){{if(E.length){{t(I[E.shift()],a).start()}}else e()}}function t(a,t){{var e=document.createElement(""script"");return e.charset=""utf-8"",e.onload=t,e.onreadystatechange=function(){{/loaded|complete/.test(this.readyState)&&t()}},{{start:function(){{e.src=a||"""",document.getElementsByTagName(""head"")[0].appendChild(e),e=null}}}}}}function e(){{for(;c[0];)c.shift()();o.readyState=2}}var o=kakao.maps=kakao.maps||{{}};if(void 0===o.readyState)o.onloadcallbacks=[],o.readyState=0;else if(2===o.readyState)return;o.VERSION={{ROADMAP:""2303ksn"",ROADMAP_SUFFIX:"""",HYBRID:""2303ksn"",SR:""3.00"",ROADVIEW:""7.00"",ROADVIEW_FLASH:""200402"",BICYCLE:""6.00"",USE_DISTRICT:""2303ksn"",SKYVIEW_VERSION:""160114"",SKYVIEW_HD_VERSION:""160107""}},o.RESOURCE_PATH={{ROADVIEW_AJAX:""//t1.daumcdn.net/roadviewjscore/core/css3d/200204/standard/1580795088957/roadview.js"",ROADVIEW_CSS:""//t1.daumcdn.net/roadviewjscore/core/openapi/standard/230112/roadview.js""}};for(var n,r=""https:""==location.protocol?""https:"":""http:"",s="""",i=document.getElementsByTagName(""script""),d=i.length;n=i[--d];)if(/\/(beta-)?dapi\.kakao\.com\/v2\/maps\/sdk\.js\b/.test(n.src)){{s=n.src;break}}i=null;var c=o.onloadcallbacks,E=[""v3""],S="""",I={{v3:r+""//t1.daumcdn.net/mapjsapi/js/main/4.4.8/kakao.js"",services:r+""//t1.daumcdn.net/mapjsapi/js/libs/services/1.0.2/services.js"",drawing:r+""//t1.daumcdn.net/mapjsapi/js/libs/drawing/1.2.6/drawing.js"",clusterer:r+""//t1.daumcdn.net/mapjsapi/js/libs/clusterer/1.0.9/clusterer.js""}},_=function(a){{var t={{}};return a.replace(/[?&]+([^=&]+)=([^&]*)/gi,function(a,e,o){{t[e]=o}}),t}}(s);S=_.appkey,S&&(o.apikey=S),o.version=""4.4.8"";var R=_.libraries;if(R&&(E=E.concat(R.split("",""))),""false""!==_.autoload){{for(var d=0,l=E.length;d<l;d++)!function(a){{a&&document.write('<script charset=""UTF-8"" src=""'+a+'""><\/script>')}}(I[E[d]]);o.readyState=2}}o.load=function(t){{switch(c.push(t),o.readyState){{case 0:o.readyState=1,a();break;case 2:e()}}}}}}();
                                </script>
                                <body>
	                                <div id=""map"" style=""width:380;height:193px;""></div>
	                                <script type=""text/javascript"" src=""//dapi.kakao.com/v2/maps/sdk.js?appkey=a71fd4e07d867d250dec31c7fa8e7742""></script>
	                                <script>
		                                var container = document.getElementById('map');
		                                var options = {{
			                                center: new kakao.maps.LatLng(33.450701, 126.570667),
			                                level: 3
		                                }};
		                                var map = new kakao.maps.Map(container, options);
	                                </script>
                                </body>
                                </html>";
        // 로드뷰 가능한 카카오 지도
        string KakaoAPIRoadView = @"<!DOCTYPE html>
                                    <html>
                                    <head>
                                    <meta charset=""utf-8"">
                                    <title>지도 위 버튼으로 로드뷰 표시하기</title>
                                    <style>
                                    #container {overflow:hidden;height:193px;position:relative;}
                                    #mapWrapper {width:100%;height:193px;z-index:1;}
                                    #rvWrapper {width:50%;height:193px;top:0;right:0;position:absolute;z-index:0;}
                                    #container.view_roadview #mapWrapper {width: 50%;}
                                    #roadviewControl {position:absolute;top:5px;left:5px;width:42px;height:42px;z-index: 1;cursor: pointer; background: url(https://t1.daumcdn.net/localimg/localimages/07/2018/pc/common/img_search.png) 0 -450px no-repeat;}
                                    #roadviewControl.active {background-position:0 -350px;}
                                    #close {position: absolute;padding: 4px;top: 5px;left: 5px;cursor: pointer;background: #fff;border-radius: 4px;border: 1px solid #c8c8c8;box-shadow: 0px 1px #888;}
                                    #close .img {display: block;background: url(https://t1.daumcdn.net/localimg/localimages/07/mapapidoc/rv_close.png) no-repeat;width: 14px;height: 14px;}
                                    </style>
                                    </head>
                                    <script>
                                        window.kakao=window.kakao||{},window.kakao.maps=window.kakao.maps||{},window.daum&&window.daum.maps?window.kakao.maps=window.daum.maps:(window.daum=window.daum||{},window.daum.maps=window.kakao.maps),function(){function a(){if(E.length){t(I[E.shift()],a).start()}else e()}function t(a,t){var e=document.createElement(""script"");return e.charset=""utf-8"",e.onload=t,e.onreadystatechange=function(){/loaded|complete/.test(this.readyState)&&t()},{start:function(){e.src=a||"""",document.getElementsByTagName(""head"")[0].appendChild(e),e=null}}}function e(){for(;c[0];)c.shift()();o.readyState=2}var o=kakao.maps=kakao.maps||{};if(void 0===o.readyState)o.onloadcallbacks=[],o.readyState=0;else if(2===o.readyState)return;o.VERSION={ROADMAP:""2303ksn"",ROADMAP_SUFFIX:"""",HYBRID:""2303ksn"",SR:""3.00"",ROADVIEW:""7.00"",ROADVIEW_FLASH:""200402"",BICYCLE:""6.00"",USE_DISTRICT:""2303ksn"",SKYVIEW_VERSION:""160114"",SKYVIEW_HD_VERSION:""160107""},o.RESOURCE_PATH={ROADVIEW_AJAX:""//t1.daumcdn.net/roadviewjscore/core/css3d/200204/standard/1580795088957/roadview.js"",ROADVIEW_CSS:""//t1.daumcdn.net/roadviewjscore/core/openapi/standard/230112/roadview.js""};for(var n,r=""https:""==location.protocol?""https:"":""http:"",s="""",i=document.getElementsByTagName(""script""),d=i.length;n=i[--d];)if(/\/(beta-)?dapi\.kakao\.com\/v2\/maps\/sdk\.js\b/.test(n.src)){s=n.src;break}i=null;var c=o.onloadcallbacks,E=[""v3""],S="""",I={v3:r+""//t1.daumcdn.net/mapjsapi/js/main/4.4.8/kakao.js"",services:r+""//t1.daumcdn.net/mapjsapi/js/libs/services/1.0.2/services.js"",drawing:r+""//t1.daumcdn.net/mapjsapi/js/libs/drawing/1.2.6/drawing.js"",clusterer:r+""//t1.daumcdn.net/mapjsapi/js/libs/clusterer/1.0.9/clusterer.js""},_=function(a){var t={};return a.replace(/[?&]+([^=&]+)=([^&]*)/gi,function(a,e,o){t[e]=o}),t}(s);S=_.appkey,S&&(o.apikey=S),o.version=""4.4.8"";var R=_.libraries;if(R&&(E=E.concat(R.split("",""))),""false""!==_.autoload){for(var d=0,l=E.length;d<l;d++)!function(a){a&&document.write('<script charset=""UTF-8"" src=""'+a+'""><\/script>')}(I[E[d]]);o.readyState=2}o.load=function(t){switch(c.push(t),o.readyState){case 0:o.readyState=1,a();break;case 2:e()}}}();
                                    </script>
                                    <body>
                                    <div id=""container"">
                                        <div id=""rvWrapper"">
                                            <div id=""roadview"" style=""width:100%;height:100%;""></div> <!-- 로드뷰를 표시할 div 입니다 -->
                                            <div id=""close"" title=""로드뷰닫기"" onclick=""closeRoadview()""><span class=""img""></span></div>
                                        </div>
                                        <div id=""mapWrapper"">
                                            <div id=""map"" style=""width:100%;height:100%""></div> <!-- 지도를 표시할 div 입니다 -->
                                            <div id=""roadviewControl"" onclick=""setRoadviewRoad()""></div>
                                        </div>
                                    </div>

                                    <script type=""text/javascript"" src=""//dapi.kakao.com/v2/maps/sdk.js?appkey=a71fd4e07d867d250dec31c7fa8e7742""></script>
                                    <script>
                                    var overlayOn = false, // 지도 위에 로드뷰 오버레이가 추가된 상태를 가지고 있을 변수
                                        container = document.getElementById('container'), // 지도와 로드뷰를 감싸고 있는 div 입니다
                                        mapWrapper = document.getElementById('mapWrapper'), // 지도를 감싸고 있는 div 입니다
                                        mapContainer = document.getElementById('map'), // 지도를 표시할 div 입니다 
                                        rvContainer = document.getElementById('roadview'); //로드뷰를 표시할 div 입니다

                                    var mapCenter = new kakao.maps.LatLng(35.1919 , 129.0946), // 지도의 중심좌표
                                        mapOption = {
                                            center: mapCenter, // 지도의 중심좌표
                                            level: 3 // 지도의 확대 레벨
                                        };

                                    // 지도를 표시할 div와 지도 옵션으로 지도를 생성합니다
                                    var map = new kakao.maps.Map(mapContainer, mapOption);

                                    // 로드뷰 객체를 생성합니다 
                                    var rv = new kakao.maps.Roadview(rvContainer); 

                                    // 좌표로부터 로드뷰 파노라마 ID를 가져올 로드뷰 클라이언트 객체를 생성합니다 
                                    var rvClient = new kakao.maps.RoadviewClient(); 

                                    // 로드뷰에 좌표가 바뀌었을 때 발생하는 이벤트를 등록합니다 
                                    kakao.maps.event.addListener(rv, 'position_changed', function() {

                                        // 현재 로드뷰의 위치 좌표를 얻어옵니다 
                                        var rvPosition = rv.getPosition();

                                        // 지도의 중심을 현재 로드뷰의 위치로 설정합니다
                                        map.setCenter(rvPosition);

                                        // 지도 위에 로드뷰 도로 오버레이가 추가된 상태이면
                                        if(overlayOn) {
                                            // 마커의 위치를 현재 로드뷰의 위치로 설정합니다
                                            marker.setPosition(rvPosition);
                                        }
                                    });

                                    // 마커 이미지를 생성합니다
                                    var markImage = new kakao.maps.MarkerImage(
                                        'https://t1.daumcdn.net/localimg/localimages/07/2018/pc/roadview_minimap_wk_2018.png',
                                        new kakao.maps.Size(26, 46),
                                        {
                                            // 스프라이트 이미지를 사용합니다.
                                            // 스프라이트 이미지 전체의 크기를 지정하고
                                            spriteSize: new kakao.maps.Size(1666, 168),
                                            // 사용하고 싶은 영역의 좌상단 좌표를 입력합니다.
                                            // background-position으로 지정하는 값이며 부호는 반대입니다.
                                            spriteOrigin: new kakao.maps.Point(705, 114),
                                            offset: new kakao.maps.Point(13, 46)
                                        }
                                    );

                                    // 드래그가 가능한 마커를 생성합니다
                                    var marker = new kakao.maps.Marker({
                                        image : markImage,
                                        position: mapCenter,
                                        draggable: true
                                    });

                                    // 마커에 dragend 이벤트를 등록합니다
                                    kakao.maps.event.addListener(marker, 'dragend', function(mouseEvent) {

                                        // 현재 마커가 놓인 자리의 좌표입니다 
                                        var position = marker.getPosition();

                                        // 마커가 놓인 위치를 기준으로 로드뷰를 설정합니다
                                        toggleRoadview(position);
                                    });

                                    //지도에 클릭 이벤트를 등록합니다
                                    kakao.maps.event.addListener(map, 'click', function(mouseEvent){
    
                                        // 지도 위에 로드뷰 도로 오버레이가 추가된 상태가 아니면 클릭이벤트를 무시합니다 
                                        if(!overlayOn) {
                                            return;
                                        }

                                        // 클릭한 위치의 좌표입니다 
                                        var position = mouseEvent.latLng;

                                        // 마커를 클릭한 위치로 옮깁니다
                                        marker.setPosition(position);

                                        // 클락한 위치를 기준으로 로드뷰를 설정합니다
                                        toggleRoadview(position);
                                    });

                                    // 전달받은 좌표(position)에 가까운 로드뷰의 파노라마 ID를 추출하여
                                    // 로드뷰를 설정하는 함수입니다
                                    function toggleRoadview(position){
                                        rvClient.getNearestPanoId(position, 50, function(panoId) {
                                            // 파노라마 ID가 null 이면 로드뷰를 숨깁니다
                                            if (panoId === null) {
                                                toggleMapWrapper(true, position);
                                            } else {
                                                toggleMapWrapper(false, position);

                                                // panoId로 로드뷰를 설정합니다
                                                rv.setPanoId(panoId, position);
                                            }
                                        });
                                    }

                                    // 지도를 감싸고 있는 div의 크기를 조정하는 함수입니다
                                    function toggleMapWrapper(active, position) {
                                        if (active) {

                                            // 지도를 감싸고 있는 div의 너비가 100%가 되도록 class를 변경합니다 
                                            container.className = '';

                                            // 지도의 크기가 변경되었기 때문에 relayout 함수를 호출합니다
                                            map.relayout();

                                            // 지도의 너비가 변경될 때 지도중심을 입력받은 위치(position)로 설정합니다
                                            map.setCenter(position);
                                        } else {

                                            // 지도만 보여지고 있는 상태이면 지도의 너비가 50%가 되도록 class를 변경하여
                                            // 로드뷰가 함께 표시되게 합니다
                                            if (container.className.indexOf('view_roadview') === -1) {
                                                container.className = 'view_roadview';

                                                // 지도의 크기가 변경되었기 때문에 relayout 함수를 호출합니다
                                                map.relayout();

                                                // 지도의 너비가 변경될 때 지도중심을 입력받은 위치(position)로 설정합니다
                                                map.setCenter(position);
                                            }
                                        }
                                    }

                                    // 지도 위의 로드뷰 도로 오버레이를 추가,제거하는 함수입니다
                                    function toggleOverlay(active) {
                                        if (active) {
                                            overlayOn = true;

                                            // 지도 위에 로드뷰 도로 오버레이를 추가합니다
                                            map.addOverlayMapTypeId(kakao.maps.MapTypeId.ROADVIEW);

                                            // 지도 위에 마커를 표시합니다
                                            marker.setMap(map);

                                            // 마커의 위치를 지도 중심으로 설정합니다 
                                            marker.setPosition(map.getCenter());

                                            // 로드뷰의 위치를 지도 중심으로 설정합니다
                                            toggleRoadview(map.getCenter());
                                        } else {
                                            overlayOn = false;

                                            // 지도 위의 로드뷰 도로 오버레이를 제거합니다
                                            map.removeOverlayMapTypeId(kakao.maps.MapTypeId.ROADVIEW);

                                            // 지도 위의 마커를 제거합니다
                                            marker.setMap(null);
                                        }
                                    }

                                    // 지도 위의 로드뷰 버튼을 눌렀을 때 호출되는 함수입니다
                                    function setRoadviewRoad() {
                                        var control = document.getElementById('roadviewControl');

                                        // 버튼이 눌린 상태가 아니면
                                        if (control.className.indexOf('active') === -1) {
                                            control.className = 'active';

                                            // 로드뷰 도로 오버레이가 보이게 합니다
                                            toggleOverlay(true);
                                        } else {
                                            control.className = '';

                                            // 로드뷰 도로 오버레이를 제거합니다
                                            toggleOverlay(false);
                                        }
                                    }

                                    // 로드뷰에서 X버튼을 눌렀을 때 로드뷰를 지도 뒤로 숨기는 함수입니다
                                    function closeRoadview() {
                                        var position = marker.getPosition();
                                        toggleMapWrapper(true, position);
                                    }
                                    </script>
                                    </body>
                                    </html>";
        #endregion
        private void browser_IsBrowserInitializedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // var map_url = $@"{AppDomain.CurrentDomain.BaseDirectory}api2.html";
            // string strHtml = File.ReadAllText(map_url);
            // Debug.WriteLine(strHtml);

            browser.LoadHtml(KakaoAPIRoadView, "http://www.team-one.com/");
        }
    }
}

