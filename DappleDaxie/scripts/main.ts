$(document).ready(function ()
{
  

  Data.Call("Images", { Foo: "bar" }, function (msg)
  {
    
  });

});

class Data
{
  private static _callback: Function;
  private static _lastCall: string;

  public static Call(handler: string, data: Object, callback: (msg: any) => void): void
  {
    var input = {
      Handler: handler,
      Data: JSON.stringify(data)
    };

    Data._callback = callback;
    Data._lastCall = handler;

    console.log(Data._lastCall + " input: ", input);

    $.post("handler.dap", input, Data.Callback);
  }

  public static Callback(json: string): void
  {
    var msg = JSON.parse(json);

    console.log(Data._lastCall + " response: ", msg);

    Data._callback(msg);
  }
}