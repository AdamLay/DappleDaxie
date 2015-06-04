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

  public static Call(handler: string, data: Object, callback: (msg: IResponse<any>) => void): void
  {
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
    try
    {
      msg = JSON.parse(json);
    }
    catch (ex)
    {
      console.error("Error parsing response from " + Data._lastCall.Handler, json);
    }

    if (!msg.Success)
      Data.Error(msg.ErrorMessage);

    console.log(Data._lastCall.Handler + " response: ", msg);

    Data._lastCall.Callback(msg);
  }

  public static Error(message: string)
  {
    console.error(Data._lastCall.Handler + ": " + message);
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