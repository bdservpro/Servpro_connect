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
   public class SyncTasks
    {
       public static int InsertTaskLines(TaskMaster obj)
       {
           DBHelper DBConn = new DBHelper();
           DataTable dt;
           SqlParameter[] sqlparam;
           sqlparam = new SqlParameter[4];

           sqlparam[0] = new SqlParameter("@id", '0');
           sqlparam[1] = new SqlParameter("@JobNo", obj.JobNo);
           sqlparam[2] = new SqlParameter("@Project_Number", obj.Project_Number);
           sqlparam[3] = new SqlParameter("@TaskName", obj.TaskName);
           int a = DBConn.Save("InsertTaskLines", sqlparam, "");
           return a;

       }

    }

   public class TaskMaster 
   {
       public int id { get; set; }
       public string JobNo { get; set; }
       public string Project_Number { get; set; }
       public string TaskName { get; set; }
   }

}
