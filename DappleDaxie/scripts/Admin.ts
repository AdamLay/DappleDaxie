class Admin
{
  public static Login(): void
  {
    var user = $("#txtUsername").val();
    var pass = $("#txtPassword").val();

    if ((<any>$("#chkRemember").get(0)).checked)
    {
      Settings.Username = user;
    }

    Data.Authenticate(user, pass);
  }
}