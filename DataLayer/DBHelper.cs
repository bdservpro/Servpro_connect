using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Data.SqlClient;



namespace DataLayer
{
    public class DBHelper
    {

        public SqlConnection objConnection4Trans4ANDAUDIT1_DEV = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);

        public DataTable FillDropDown(String procedureName, String errorMsg)
        {
            try
            {
                DataTable st = new DataTable();
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                cmd = new SqlCommand(procedureName, objConnection4Trans4ANDAUDIT1_DEV);
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                da.Fill(st);
                return st;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message.ToString();
                DateTime dateEx = DateTime.Now;
                return null;
            }

        }


        public DataTable Selection(String procedureName, SqlParameter[] sqlparam, String errorMsg)
        {
            try
            {
                DataTable st = new DataTable();
                SqlCommand cmd = new SqlCommand();
                objConnection4Trans4ANDAUDIT1_DEV.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                cmd = new SqlCommand(procedureName, objConnection4Trans4ANDAUDIT1_DEV);
                cmd.CommandType = CommandType.StoredProcedure;
                if (sqlparam != null)
                {
                    cmd.Parameters.AddRange(sqlparam);
                }
                da.SelectCommand = cmd;
                da.Fill(st);
                // cmd = null;
                return st;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message.ToString();
                DateTime dateEx = DateTime.Now;
                return null;
            }
            finally
            {
                objConnection4Trans4ANDAUDIT1_DEV.Close();
            }

        }

        public int Save(String procedureName, SqlParameter[] sqlparam, String errorMsg)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                objConnection4Trans4ANDAUDIT1_DEV.Open();
                cmd = new SqlCommand(procedureName, objConnection4Trans4ANDAUDIT1_DEV);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(sqlparam);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message.ToString();
                DateTime dateEx = DateTime.Now;
                return 0;
            }
            finally
            {
                objConnection4Trans4ANDAUDIT1_DEV.Close();
            }

        }
        public int DeleteById(String procedureName, SqlParameter[] sqlparam, String errorMsg)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                objConnection4Trans4ANDAUDIT1_DEV.Open();
                cmd = new SqlCommand(procedureName, objConnection4Trans4ANDAUDIT1_DEV);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(sqlparam);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message.ToString();
                DateTime dateEx = DateTime.Now;
                return 0;
            }
            finally
            {
                objConnection4Trans4ANDAUDIT1_DEV.Close();
            }

        }


        public int Save_AddWithValue(SqlCommand cmd, string procedureName, String errorMsg)
        {
            try
            {
                objConnection4Trans4ANDAUDIT1_DEV.Open();
                cmd = new SqlCommand(procedureName, objConnection4Trans4ANDAUDIT1_DEV);
                cmd.CommandType = CommandType.StoredProcedure;
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message.ToString();
                DateTime dateEx = DateTime.Now;
                return 0;
            }
            finally
            {
                objConnection4Trans4ANDAUDIT1_DEV.Close();
            }

        }

        public int Delete(String procedureName, String errorMsg)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                objConnection4Trans4ANDAUDIT1_DEV.Open();

                cmd = new SqlCommand(procedureName, objConnection4Trans4ANDAUDIT1_DEV);
                cmd.CommandType = CommandType.Text;

                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message.ToString();
                DateTime dateEx = DateTime.Now;
                return 0;
            }
            finally
            {
                objConnection4Trans4ANDAUDIT1_DEV.Close();
            }

        }

        public int update(String procedureName, String errorMsg)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                objConnection4Trans4ANDAUDIT1_DEV.Open();
                cmd = new SqlCommand(procedureName, objConnection4Trans4ANDAUDIT1_DEV);
                cmd.CommandType = CommandType.Text;
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message.ToString();
                DateTime dateEx = DateTime.Now;
                return 0;
            }
            finally
            {
                objConnection4Trans4ANDAUDIT1_DEV.Close();
            }

        }

        public int Scaler(string procedureName, string errorMsg)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                objConnection4Trans4ANDAUDIT1_DEV.Open();

                cmd = new SqlCommand(procedureName, objConnection4Trans4ANDAUDIT1_DEV);
                cmd.CommandType = CommandType.Text;

                int i = (int)cmd.ExecuteScalar();
                return i;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message.ToString();
                DateTime dateEx = DateTime.Now;
                return 0;
            }
            finally
            {
                objConnection4Trans4ANDAUDIT1_DEV.Close();
            }
        }

        public string ScalerValue(string procedureName, string errorMsg)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                objConnection4Trans4ANDAUDIT1_DEV.Open();

                cmd = new SqlCommand(procedureName, objConnection4Trans4ANDAUDIT1_DEV);
                cmd.CommandType = CommandType.Text;

                string i = (string)cmd.ExecuteScalar();
                return i;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message.ToString();
                DateTime dateEx = DateTime.Now;
                return "";
            }
            finally
            {
                objConnection4Trans4ANDAUDIT1_DEV.Close();
            }
        }

        public DataTable getDataTable4QueryNew(string argSQL, string argstatus, DBHelper DBConn = null, SqlTransaction argTransaction = null)
        {
            if ((argTransaction != null))
            {
                goto USE_TRANSACTION;
            }



            DataTable tDataTable = new DataTable();

            SqlCommand cmd = new SqlCommand(argSQL, objConnection4Trans4ANDAUDIT1_DEV);
            objConnection4Trans4ANDAUDIT1_DEV.Open();
            SqlDataAdapter tDataAdapter = new SqlDataAdapter();
            tDataAdapter.SelectCommand = cmd;
            cmd.CommandTimeout = 0;
            tDataAdapter.Fill(tDataTable);
            objConnection4Trans4ANDAUDIT1_DEV.Close();


            return tDataTable;
        USE_TRANSACTION:
            DataTable tDataTable1 = new DataTable();
            SqlCommand tObjCommand4TRAN = new SqlCommand(argSQL, objConnection4Trans4ANDAUDIT1_DEV);
            SqlCommand cmd1 = new SqlCommand(argSQL, objConnection4Trans4ANDAUDIT1_DEV);

            //tObjCommand4TRAN.ExecuteNonQuery()



            SqlDataAdapter tDataAdapter1 = new SqlDataAdapter();
            tDataAdapter1.SelectCommand = tObjCommand4TRAN;
            cmd1.CommandTimeout = 0;
            tDataAdapter1.Fill(tDataTable1);
            tObjCommand4TRAN = null;
            return tDataTable1;

        }



    }
}
