﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fb-login.aspx.cs" Inherits="sdk_facebook_fb_login" %>

<!DOCTYPE html>

<div id="fb-root"></div>
<script>
    window.fbAsyncInit = function () {
        FB.init({
            appId: '270137673136672', // App ID
            status: true, // check login status
            cookie: true, // enable cookies to allow the server to access the session
            xfbml: true  // parse XFBML
        });

        FB.Event.subscribe('auth.authResponseChange', function (response) {
            if (response.status === 'connected') {
                // the user is logged in and has authenticated your
                // app, and response.authResponse supplies
                // the user's ID, a valid access token, a signed
                // request, and the time the access token 
                // and signed request each expire
                var uid = response.authResponse.userID;
                var accessToken = response.authResponse.accessToken;

                // Do a post to the server to finish the logon
                // This is a form post since we don't want to use AJAX
                var form = document.createElement("form");
                form.setAttribute("method", 'post');
                form.setAttribute("action", '/FacebookLogin.ashx');

                var field = document.createElement("input");
                field.setAttribute("type", "hidden");
                field.setAttribute("name", 'accessToken');
                field.setAttribute("value", accessToken);
                form.appendChild(field);

                document.body.appendChild(form);
                form.submit();

            } else if (response.status === 'not_authorized') {
                // the user is logged in to Facebook, 
                // but has not authenticated your app
            } else {
                // the user isn't logged in to Facebook.
            }
        });
    };

    // Load the SDK Asynchronously
    (function (d) {
        var js, id = 'facebook-jssdk', ref = d.getElementsByTagName('script')[0];
        if (d.getElementById(id)) { return; }
        js = d.createElement('script'); js.id = id; js.async = true;
        js.src = "//connect.facebook.net/en_US/all.js";
        ref.parentNode.insertBefore(js, ref);
    }(document));
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
