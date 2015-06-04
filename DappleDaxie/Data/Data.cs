using DappleDaxie.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DappleDaxie.Data
{
  public class Data
  {
    private static string _connection { get { return "Data Source=db575146881.db.1and1.com;Database=db575146881;User ID=dbo575146881;Password=DappleDaxie)DLFhg65;"; } }
    private static SqlConnection Connection { get { return new SqlConnection(_connection); } }

    public static bool AuthenticateUser(string username, string password)
    {
      User user = Get<User>("Users_Get", new Parameter() { Name = "Name", Type = DbType.String, Value = username });

      return user != null && BCrypt.Net.BCrypt.Verify(password, user.Password);
    }

    public static void CreateAuthToken(Guid token)
    {
      Set("AuthTokens_Create", new Parameter("@Token", DbType.Guid, token));
    }

    public static AuthToken GetAuthToken(Guid token)
    {
      AuthToken authToken = Get<AuthToken>("AuthTokens_Get", new Parameter("@Token", DbType.Guid, token));

      return authToken;
    }

    #region Framework

    public static void Set(string cmd, params Parameter[] parameters)
    {
      using (SqlConnection conn = Connection)
      using (SqlCommand command = conn.CreateCommand())
      {
        command.CommandText = cmd;
        command.CommandType = CommandType.StoredProcedure;

        if (parameters != null)
          foreach (var p in parameters)
            command.AddParam(p.Name, p.Type, p.Value);

        conn.Open();

        command.ExecuteNonQuery();

        conn.Close();
      }
    }

    public static List<T> List<T>(string cmd, params Parameter[] parameters) where T : class, IBuildable<T>
    {
      T buildable = (T)Activator.CreateInstance(typeof(T));

      List<T> results = new List<T>();

      using (SqlConnection conn = Connection)
      using (SqlCommand command = conn.CreateCommand())
      {
        command.CommandText = cmd;
        command.CommandType = CommandType.StoredProcedure;

        if (parameters != null)
          foreach (var p in parameters)
            command.AddParam(p.Name, p.Type, p.Value);

        conn.Open();

        using (SqlDataReader dr = command.ExecuteReader())
        {
          while (dr.Read())
          {
            results.Add(buildable.Build(dr));
          }
        }

        conn.Close();
      }

      return results;
    }

    public static List<T> List<T>(string cmd) where T : class, IBuildable<T>
    {
      return List<T>(cmd, new Parameter[0]);
    }

    public static T Get<T>(string cmd, params Parameter[] parameters) where T : class, IBuildable<T>
    {
      var results = List<T>(cmd, parameters);

      if (results.Count >= 1)
        return results[0];

      return null;
    }

    public static List<List<KeyValuePair<int, string>>> GetFromText(string commandText)
    {
      List<List<KeyValuePair<int, string>>> results = new List<List<KeyValuePair<int, string>>>();

      using (SqlConnection conn = Connection)
      using (SqlCommand command = conn.CreateCommand())
      {
        command.CommandText = commandText;
        command.CommandType = CommandType.Text;

        conn.Open();

        using (SqlDataReader dr = command.ExecuteReader())
        {
          while (dr.Read())
          {
            List<KeyValuePair<int, string>> obj = new List<KeyValuePair<int, string>>();

            for (int i = 0; i < dr.FieldCount; i++)
            {
              obj.Add(new KeyValuePair<int, string>(i, dr[i].ToString()));
            }

            results.Add(obj);
          }
        }

        conn.Close();
      }

      return results;
    }

    public static void ExecuteText(string commandText)
    {
      using (SqlConnection conn = Connection)
      using (SqlCommand command = conn.CreateCommand())
      {
        command.CommandText = commandText;
        command.CommandType = CommandType.Text;

        conn.Open();

        command.ExecuteNonQuery();

        conn.Close();
      }
    }

    #endregion
  }
}