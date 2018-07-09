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
 public class SyncJobs
    {

     public static int InsertJobLines(ProjectMaster obj)
     {
         DBHelper DBConn = new DBHelper();
         DataTable dt;
         SqlParameter[] sqlparam;
         sqlparam = new SqlParameter[5];

         sqlparam[0] = new SqlParameter("@id", '0');
         sqlparam[1] = new SqlParameter("@project_name",obj.Project_Name);
         sqlparam[2] = new SqlParameter("@project_number",obj.Project_Number);
         sqlparam[3] = new SqlParameter("@contact_person", obj.Contact_Person);
         sqlparam[4] = new SqlParameter("@blocked", obj.Blocked);
         int a= DBConn.Save("InsertJobLines", sqlparam, "");
         return a;

     }

    }
 public class ProjectMaster
 {
     public int id { get; set; }
     public string Project_Name { get; set; }
     public string Project_Number { get; set; }
     public string Contact_Person { get; set; }
     public string Blocked { get; set; }
 }
}
