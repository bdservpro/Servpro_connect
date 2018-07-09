using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Security;
using System.Data.OleDb;
using System.Text;
using DataLayer;

namespace DataLayer
{
  public class SyncSalesCycle
    {
      public static int InsertSalecylceLines(SalesCyleMaster obj)
      {
          DBHelper DBConn = new DBHelper();
          DataTable dt;
          SqlParameter[] sqlparam;
          sqlparam = new SqlParameter[3];

          sqlparam[0] = new SqlParameter("@id", '0');
          sqlparam[1] = new SqlParameter("@Code", obj.Code);
          sqlparam[2] = new SqlParameter("@Description", obj.Description);

          int a = DBConn.Save("InsertSalesCylceLines", sqlparam, "");
          return a;

      }

    }

  public class SalesCyleMaster {
      public int id { get; set; }
      public string Code { get; set; }
      public string Description { get; set; }
  }
}
