//#region Interfaces

interface IRequest
{
  Handler: string;
  Data: Object;
  Callback: (msg: IResponse) => void;
}

interface IResponse
{
  Success: boolean;
  ErrorMessage?: string
}

interface IAuthenticateResponse extends IResponse
{
  IsValid: boolean;
  AuthToken?: string;
}

//#endregion



class Data
{
  //#region Framework

  private static _lastCall: IRequest;

  public static Call(handler: string, data: Object, callback: (msg: IResponse) => void): void
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

    $.post("handler.dap", input, Data.Callback);
  }

  public static Callback(json: string): void
  {
    var msg: IResponse = JSON.parse(json);

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
    Data.Call("Authenticate", {},(msg: IAuthenticateResponse) =>
    {
      if (msg.IsValid)
      {
        Settings.AuthToken = msg.AuthToken;

        window.location.replace("/admin/index.html");
      }
      else
      {
        // Try again
      }
    });
  }
}