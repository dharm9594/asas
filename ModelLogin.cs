using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GCBC_NextGen.model
{
    public class ModelLogin
    {
        public ModelLogin() { }

        string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnStr"].ConnectionString;
        string GlobalMappingCon = System.Configuration.ConfigurationManager.ConnectionStrings["GlobalMappingStr"].ConnectionString;

        public bool GetUserDetails(string UserName, string DomainName)
        {
            SqlConnection SqlCon = new SqlConnection(strCon);
            try
            {
                DataTable DT = new DataTable();
                string Query = "SELECT UserID,LawsonID,NTID,Name,UserStatus,RoleID FROM UserMaster WHERE UserStatus=1 AND NTID='" + UserName.Trim() + "' AND Domain='" + DomainName.Trim() + "'";
                SqlCon.Open();
                SqlCommand SqlCmd = new SqlCommand(Query, SqlCon);
                SqlDataAdapter SqlAdpt = new SqlDataAdapter(SqlCmd);
                SqlAdpt.Fill(DT);

                if (Utility.HasRowsDT(DT))
                {
                    HttpContext.Current.Session["UserID"] = Convert.ToString(DT.Rows[0]["UserID"]);
                    HttpContext.Current.Session["LawsonID"] = Convert.ToString(DT.Rows[0]["LawsonID"]);
                    HttpContext.Current.Session["RollID"] = Convert.ToString(DT.Rows[0]["RoleID"]);
                    HttpContext.Current.Session["NTID"] = Convert.ToString(DT.Rows[0]["NTID"]);
                    //HttpContext.Current.Session["FullName"] = Convert.ToString(DT.Rows[0]["FullName"]);
                    
                    if (Convert.ToString(DT.Rows[0]["RoleID"]) == "")
                    {
                        HttpContext.Current.Session["NTID"] = UserName;
                        //HttpContext.Current.Session["RollID"] = "1";
                        return false;
                    }
                    
                    HttpContext.Current.Session["RollID"] = Convert.ToString(DT.Rows[0]["RoleID"]);
                    return true;
                }
                else
                {
                    HttpContext.Current.Session["NTID"] = UserName;
                    //HttpContext.Current.Session["RollID"] = "1";
                    return false;
                }
            }
            catch (Exception Ex) { throw Ex; }
            finally { SqlCon.Close(); }
        }

        public void InsertLoginLog(string FullName, string NTID, string Domain, string Login_Type)
        {
            SqlConnection SqlCon = new SqlConnection(strCon);
            try
            {
                using (SqlCommand SqlCmd = new SqlCommand("Insert_Login_Log", SqlCon))
                {
                    SqlCon.Open();
                    SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlCmd.Parameters.Add("@NTID", System.Data.SqlDbType.VarChar).Value = Convert.ToString(NTID).Trim();
                    SqlCmd.Parameters.Add("@Logged_Date", System.Data.SqlDbType.DateTime).Value = DateTime.Now;
                    SqlCmd.Parameters.Add("@Logged_IPAddress", System.Data.SqlDbType.VarChar).Value = Utility.GetIPAddress();
                    SqlCmd.Parameters.Add("@EmpFullName", System.Data.SqlDbType.VarChar).Value = Convert.ToString(FullName).Trim();
                    SqlCmd.Parameters.Add("@Domain", System.Data.SqlDbType.VarChar).Value = Convert.ToString(Domain).Trim();
                    SqlCmd.Parameters.Add("@Login_Type", System.Data.SqlDbType.VarChar).Value = Convert.ToString(Login_Type).Trim();
                    SqlCmd.ExecuteNonQuery();
                }
            }
            catch (Exception Ex) { throw Ex; }
            finally { SqlCon.Close(); }
        }


        public void InsertBCAccessFailureLogs(string Domain, string NTID, string Description)
        {
            SqlConnection SqlCon = new SqlConnection(strCon);
            try
            {
                using (SqlCommand SqlCmd = new SqlCommand("SP_InsertBCAccessFailureLogs", SqlCon))
                {
                    SqlCon.Open();
                    SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlCmd.Parameters.Add("@Domain", System.Data.SqlDbType.VarChar).Value = Convert.ToString(Domain).Trim();
                    SqlCmd.Parameters.Add("@NTID", System.Data.SqlDbType.VarChar).Value = Convert.ToString(NTID).Trim();
                    SqlCmd.Parameters.Add("@CreatedIpAddress", System.Data.SqlDbType.VarChar).Value = Utility.GetIPAddress();
                    SqlCmd.Parameters.Add("@Description", System.Data.SqlDbType.VarChar).Value = Convert.ToString(Description).Trim();
                    SqlCmd.ExecuteNonQuery();
                }
            }
            catch (Exception Ex) { throw Ex; }
            finally { SqlCon.Close(); }
        
        }


        public DataTable FunGetUserDetail(string username)
        {

            DataTable table = new DataTable();
            string strCon = System.Configuration.ConfigurationManager.ConnectionStrings
             ["DbConnStr"].ConnectionString;
            SqlConnection conn = new SqlConnection(strCon);
            string sql = "select * from usermaster where UserStatus =1 and NTID='" + username + "'";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(table);
            conn.Close();
            return table;
        }
        public string GetGosPositionCode(string LawsonId)
        {
            string GosCode = "";
            DataTable table = new DataTable();
            SqlConnection conn = new SqlConnection(GlobalMappingCon);
            string sql = "select GOSPOSITIONCODE from MappingMasterNew where EMPLOYEEID = '" + LawsonId + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(table);
            if (table.Rows.Count > 0)
            {
                GosCode = table.Rows[0]["GOSPOSITIONCODE"].ToString();
            }
            else
            {
                GosCode = "";
            }
            return GosCode;
        }

        public DataTable GetRoles(string Goscode)
        {
            SqlConnection con = new SqlConnection(strCon);
            try
            {
                SqlCommand cmd = new SqlCommand("select * from GosPositionCode_Master where GOSPOSITIONCODE = '" + Goscode + "' ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                con.Close();
            }
        }

        public string GetLawsonIDfromGlobalNew(string username, string Domain)
        {
            string LawsonID = "";

            DataTable table = new DataTable();
            string strCon = System.Configuration.ConfigurationManager.ConnectionStrings
             ["GlobalMappingStr"].ConnectionString;
            SqlConnection conn = new SqlConnection(strCon);
            string sql = "select * from Global_Mapping..[Employee_DomainNTID_Master]  where LOGINID ='" + username + "'" + " and LOGINDOMAIN='" + Domain + "'";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(table);
            conn.Close();
            if (table.Rows.Count > 0)
            {
                LawsonID = table.Rows[0]["EMPLOYEEID"].ToString();
                return LawsonID;
            }
            else
            {
                return "";
            }
        }

        public int SetLoginStatus(string Domain,string NTID,string Emp_Name,int isLogin)
        {
            int Result;
            SqlCommand Cmd = new SqlCommand();
            try
            {
                Cmd.Connection = new SqlConnection(strCon);
                Cmd.Connection.Open();
                Cmd.CommandText = "SP_Login_Status";
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.Parameters.Add("@Domain", SqlDbType.VarChar).Value = Domain;
                Cmd.Parameters.Add("@NTID", SqlDbType.VarChar).Value = NTID;
                Cmd.Parameters.Add("@Emp_Name", SqlDbType.VarChar).Value = Emp_Name;
                Cmd.Parameters.Add("@isLogin", SqlDbType.VarChar).Value = isLogin;
                Result = Cmd.ExecuteNonQuery();
                Cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                if (Cmd.Connection.State == ConnectionState.Open)
                    Cmd.Connection.Close();
                throw ex;
            }
            return Result;
        }

        public DataTable GetAccess(string Domain, string NTID)
        {
            DataTable table = new DataTable();
            SqlConnection conn = new SqlConnection(strCon);
            string sql = "select * from Login_Exception_Master where domain = '" + Domain + "' and ntid = '" + NTID + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(table);
            return table;
        }

        // Terms and Conditions Brand Center

        public string GetEmpname(string LawsonId)
        {
            string Empanme = "";
            DataTable table = new DataTable();
            SqlConnection conn = new SqlConnection(GlobalMappingCon);
            string sql = "select EMP_NAME from MappingMasterNew where EMPLOYEEID = '" + LawsonId + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(table);
            if (table.Rows.Count > 0)
            {
                Empanme = table.Rows[0]["EMP_NAME"].ToString();
            }
            else
            {
                Empanme = "";
            }
            return Empanme;
        }
    }
}