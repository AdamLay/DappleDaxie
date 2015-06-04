var Admin = (function () {
    function Admin() {
    }
    Admin.Login = function () {
        var user = $("#txtUsername").val();
        var pass = $("#txtPassword").val();
        var remember = $("#chkRemember").get(0).checked;
        Settings.RememberMe = remember;
        Settings.Username = user;
        Data.Authenticate(user, pass);
    };
    return Admin;
})();
//#region Interfaces
//#endregion
var Data = (function () {
    function Data() {
    }
    Data.Call = function (handler, data, callback) {
        Data._lastCall = {
            Handler: handler,
            Data: data,
            Callback: callback
        };
        var input = {
            Handler: handler,
            Data: JSON.stringify(data)
        };
        console.log(Data._lastCall.Handler + " input: ", input);
        $.post("/handler.dap", input, Data.Callback);
    };
    Data.Callback = function (json) {
        var msg;
        try {
            msg = JSON.parse(json);
        }
        catch (ex) {
            console.error("Error parsing response from " + Data._lastCall.Handler, json);
        }
        if (!msg.Success)
            Data.Error(msg.ErrorMessage);
        console.log(Data._lastCall.Handler + " response: ", msg);
        Data._lastCall.Callback(msg);
    };
    Data.Error = function (message) {
        console.error(Data._lastCall.Handler + ": " + message);
    };
    //#endregion
    Data.Authenticate = function (username, password) {
        Data.Call("Authenticate", { Username: username, Password: password }, function (msg) {
            if (msg.Result.IsValid) {
                Settings.AuthToken = msg.Result.AuthToken;
                window.location.replace("/admin/index.html");
            }
            else {
            }
        });
    };
    Data.GetSql = function () {
        var sql = $("#cmdGet").val();
        Data.Call("GetSql", { Sql: sql }, function (msg) {
        });
    };
    Data.ExecSql = function () {
        var sql = $("#cmdExec").val();
        Data.Call("ExecSql", { Sql: sql }, function (msg) {
        });
    };
    return Data;
})();
//$(document).ready(function ()
//{
//  Data.Call("Images", { Foo: "bar" }, function (msg)
//  {
//  });
//});
var Settings = (function () {
    function Settings() {
    }
    Settings.GetValue = function (property, session) {
        var prop = "Settings." + property;
        var obj = null;
        if (Settings[prop])
            obj = Settings[prop];
        if (session && sessionStorage[prop])
            obj = sessionStorage[prop];
        if (localStorage[prop])
            obj = localStorage[prop];
        return obj;
    };
    Settings.SetValue = function (property, value, session) {
        var prop = "Settings." + property;
        Settings[prop] = value;
        (session ? sessionStorage : localStorage)[prop] = (typeof (value) == "object" ? JSON.stringify(value) : value);
    };
    Object.defineProperty(Settings, "AuthToken", {
        get: function () {
            return Settings.GetValue("AuthToken", true);
        },
        set: function (token) {
            Settings.SetValue("AuthToken", token, true);
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(Settings, "Username", {
        get: function () {
            return Settings.GetValue("Username", false);
        },
        set: function (usr) {
            Settings.SetValue("Username", usr, false);
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(Settings, "RememberMe", {
        get: function () {
            return Settings.GetValue("RememberMe", false);
        },
        set: function (remember) {
            Settings.SetValue("RememberMe", remember, false);
        },
        enumerable: true,
        configurable: true
    });
    return Settings;
})();
