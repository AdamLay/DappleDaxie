﻿class Admin
{
  public static Login(): void
  {
    var user = $("#txtUsername").val();
    var pass = $("#txtPassword").val();

    var remember = (<any>$("#chkRemember").get(0)).checked;

    Settings.RememberMe = remember;

    Settings.Username = user;

    Data.Authenticate(user, pass);
  }

  public static Logout(): void
  {
    if (!Settings.RememberMe)
      Settings.Username = "";

    Settings.AuthToken = "";

    window.location.replace("/admin/login.html");
  }
}