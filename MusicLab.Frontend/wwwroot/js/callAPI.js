﻿var APIManager = {
    GetAPI: function (serviceUrl, successCallback, errorCallback) {
        $.ajax({
            type: "GET",
            url: serviceUrl,
            dataType: "json",
            success: successCallback,
            error: errorCallback
        });
    },
    PostAPI: function (serviceUrl, data, successCallback, errorCallback) {
        $.ajax({
            type: "POST",
            url: serviceUrl,
            contentType: "application/json",
            data: JSON.stringify(data),
            success: successCallback,
            error: errorCallback
        });
    },
    PutAPI: function (serviceUrl, data, successCallback, errorCallback) {
        $.ajax({
            type: "PUT",
            url: serviceUrl,
            contentType: "application/json",
            data: JSON.stringify(data),
            success: successCallback,
            error: errorCallback
        });
    },
    DeleteAPI: function (serviceUrl, successCallback, errorCallback) {
        $.ajax({
            type: "DELETE",
            url: serviceUrl,
            success: successCallback,
            error: errorCallback
        });
    }
}

var APIManagerSecurity = {
    GetAPISecurity: function (serviceUrl, authToken, successCallback, errorCallback) {
        $.ajax({
            type: "GET",
            url: serviceUrl,
            headers: {
                "Authorization": "Bearer " + authToken
            },
            dataType: "json",
            success: successCallback,
            error: errorCallback
        });
    },
    PostAPISecurity: function (serviceUrl, authToken, data, successCallback, errorCallback) {
        $.ajax({
            type: "POST",
            url: serviceUrl,
            headers: {
                Authorization: 'Bearer ' + authToken
            },
            contentType: "application/json",
            data: JSON.stringify(data),
            success: successCallback,
            error: errorCallback
        });
    },
    PutAPISecurity: function (serviceUrl, authToken, data, successCallback, errorCallback) {
        $.ajax({
            type: "PUT",
            url: serviceUrl,
            headers: {
                Authorization: 'Bearer ' + authToken
            },
            contentType: "application/json",
            data: JSON.stringify(data),
            success: successCallback,
            error: errorCallback
        });
    },
    DeleteAPISecurity: function (serviceUrl, authToken, successCallback, errorCallback) {
        $.ajax({
            type: "DELETE",
            url: serviceUrl,
            headers: {
                Authorization: 'Bearer ' + authToken
            },
            success: successCallback,
            error: errorCallback
        });
    }
};