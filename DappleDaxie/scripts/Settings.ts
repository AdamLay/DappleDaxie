class Settings
{
  private static GetValue<T>(property: string, session: boolean): T
  {
    var prop: string = "Settings." + property;
    var obj = null;

    if (Settings[prop])
      obj = <T>Settings[prop];

    if (session && sessionStorage[prop])
      obj = sessionStorage[prop];

    if (localStorage[prop])
      obj = localStorage[prop];

    return obj;
  }

  private static SetValue<T>(property: string, value: T, session: boolean): void
  {
    var prop: string = "Settings." + property;

    Settings[prop] = value;

    (session ? sessionStorage : localStorage)[prop] = (typeof (value) == "object" ? JSON.stringify(value) : value);
  }

  public static get AuthToken(): string { return Settings.GetValue<string>("AuthToken", true); }
  public static set AuthToken(token: string) { Settings.SetValue<string>("AuthToken", token, true); }

  public static get Username(): string { return Settings.GetValue<string>("Username", false); }
  public static set Username(usr: string) { Settings.SetValue<string>("Username", usr, false); }

  public static get RememberMe(): boolean { return Settings.GetValue<boolean>("RememberMe", false); }
  public static set RememberMe(remember: boolean) { Settings.SetValue<boolean>("RememberMe", remember, false); }
}