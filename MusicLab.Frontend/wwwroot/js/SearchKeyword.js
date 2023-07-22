//$(document).ready(function () {
//    $("#search-all").on("change", function () {
//        var keyword = $(this).val();
//        Manager.GetSongs(keyword);
//    });
//});

//var Manager = {
//    GetSongs: function (keyword) {
//        var serviceUrl = "https://localhost:7054/api/get-songs-by-keyword?keyword=" + keyword;
//        Manager.GetAPI(serviceUrl, onSuccess, onFailed);
//        function onSuccess(jsonData) {
//            $('#Table').empty();
//            $.each(jsonData, function (i, item) {
//                var rows = "<tr data-id='" + item.id + "'>" +
//                    "<td>" + item.name + "</td>" +
//                    "<td>" + item.birthDate + "</td>" +
//                    "<td>" +
//                    "<button class='updateBtn'>Update</button>" +
//                    "<button class='deleteBtn'>Delete</button>" +
//                    "</td>" +
//                    "</tr>";
//                $('#Table').append(rows);
//            });
//        }
//        function onFailed(xhr, status, error) {
//            window.alert(error);
//        }
//    },
//    GetAPI: function (serviceUrl, successCallback, errorCallback) {
//        $.ajax({
//            type: "GET",
//            url: serviceUrl,
//            dataType: "json",
//            success: successCallback,
//            error: errorCallback
//        });
//    },
//    PostAPI: function (serviceUrl, data, successCallback, errorCallback) {
//        $.ajax({
//            type: "POST",
//            url: serviceUrl,
//            contentType: "application/json",
//            data: JSON.stringify(data),
//            success: successCallback,
//            error: errorCallback
//        });
//    },
//    PutAPI: function (serviceUrl, data, successCallback, errorCallback) {
//        $.ajax({
//            type: "PUT",
//            url: serviceUrl,
//            contentType: "application/json",
//            data: JSON.stringify(data),
//            success: successCallback,
//            error: errorCallback
//        });
//    },
//    DeleteAPI: function (serviceUrl, successCallback, errorCallback) {
//        $.ajax({
//            type: "DELETE",
//            url: serviceUrl,
//            success: successCallback,
//            error: errorCallback
//        });
//    }
//};
