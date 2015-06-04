//#region Interfaces

interface IRequest
{
  Handler: string;
  Data: Object;
  Callback: (msg: IResponse<any>) => void;
}

interface IResponse<T>
{
  Success: boolean;
  ErrorMessage?: string;
  Result: T;
}

interface IAuthenticateResult
{
  IsValid: boolean;
  AuthToken?: string;
}

//#endregion

class Data
{
  //#region Framework

  private static _lastCall: IRequest;

  public static Call(handler: string, data: Object, callback: (msg: IResponse<any>) => void, hideLoading?: boolean): void
  {
    if (!hideLoading)
      Data.ShowLoading();

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
  }

  public static Callback(json: string): void
  {
    var msg: IResponse<any>;
    var parseError = false;

    try
    {
      msg = JSON.parse(json);
    }
    catch (ex)
    {
      msg = { Success: false, ErrorMessage: "Error parsing response from " + Data._lastCall.Handler, Result: null };

      console.error("Error parsing response from " + Data._lastCall.Handler, json);

      parseError = true;
    }

    if (!msg.Success)
      Data.Error(msg.ErrorMessage);

    if (parseError)
      return;

    console.log(Data._lastCall.Handler + " response: ", msg);

    Data._lastCall.Callback(msg);

    Data.RemoveLoading();
  }

  public static Error(message: string)
  {
    console.error(Data._lastCall.Handler + ": " + message);

    Data.RemoveLoading();
  }

  public static ShowLoading(): void
  {
    var loader = $("<div>", { "id": "divLoader" });
    var loaderText = $("<div>", { "id": "divLoadingText", "text": "Loading..." });
    var loaderWheel = $("<div>", { "id": "divLoadingWheel", "class": "icon" });

    loader.append(loaderWheel);
    loader.append(loaderText);

    $("body").append(loader);

    $("#divLoader").animate({ "top": "1em" });
  }

  public static RemoveLoading(): void
  {
    $("#divLoader").animate({ "top": "-60px" }, 500, function ()
    {
      $("#divLoader").remove();
    });
  }

  //#endregion

  public static Authenticate(username: string, password: string): void
  {
    Data.Call("Authenticate", { Username: username, Password: password },(msg: IResponse<IAuthenticateResult>) =>
    {
      if (msg.Result.IsValid)
      {
        Settings.AuthToken = msg.Result.AuthToken;

        window.location.replace("/admin/index.html");
      }
      else
      {
        // Try again
      }
    });
  }

  public static GetSql()
  {
    var sql = $("#cmdGet").val();

    Data.Call("GetSql", { Sql: sql },(msg: IResponse<any>) => { });
  }

  public static ExecSql()
  {
    var sql = $("#cmdExec").val();

    Data.Call("ExecSql", { Sql: sql },(msg: IResponse<any>) => { });
  }
}