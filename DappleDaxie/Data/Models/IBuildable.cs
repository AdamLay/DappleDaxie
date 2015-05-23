using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DappleDaxie.Data.Models
{
  public interface IBuildable<T>
  {
    T Build(SqlDataReader dr);
  }
}
