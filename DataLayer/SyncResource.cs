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
  public  class SyncResource
    {
    public static int InsertResourceLines(ResourceMaster obj)
    {
        DBHelper DBConn = new DBHelper();
        DataTable dt;
        SqlParameter[] sqlparam;
        sqlparam = new SqlParameter[4];

        sqlparam[0] = new SqlParameter("@id", '0');
        sqlparam[1] = new SqlParameter("@ResourceNo", obj.ResourceNo);
        sqlparam[2] = new SqlParameter("@UserName", obj.UserName);
        sqlparam[3] = new SqlParameter("@Name", obj.Name);
        int a = DBConn.Save("InsertResourceLines", sqlparam, "");
        return a;

    }
    }

public class ResourceMaster {
    public int id { get; set; }
    public string ResourceNo { get; set; }
    public string UserName { get; set; }
    public string Name { get; set; }
}
}
