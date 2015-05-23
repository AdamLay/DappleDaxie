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
    private string _connection { get { return "Data Source=db575146881.db.1and1.com;Database=db575146881;User ID=dbo575146881;Password=DappleDaxie)DLFhg65;"; } }
    private SqlConnection Connection { get { return new SqlConnection(_connection); } }

    public bool AuthenticateUser(string username, string password)
    {
      User user = Get<User>("Users_Get", new Parameter() { Name = "Username", type = DbType.String, Value = username });

      return user != null && BCrypt.Net.BCrypt.Verify(password, user.Password);
    }

    public List<T> List<T>(string cmd, params Parameter[] parameters) where T : class, IBuildable<T>
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
            command.AddParam(p.Name, p.type, p.Value);

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

    public List<T> List<T>(string cmd) where T : class, IBuildable<T>
    {
      return List<T>(cmd, new Parameter[0]);
    }

    public T Get<T>(string cmd, params Parameter[] parameters) where T : class, IBuildable<T>
    {
      var results = List<T>(cmd, parameters);

      if (results.Count >= 1)
        return results[0];

      return null;
    }
  }
}