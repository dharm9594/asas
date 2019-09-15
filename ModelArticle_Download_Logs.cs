using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GCBC_NextGen.model
{
    public class ModelArticle_Download_Logs
    {
        string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnStr"].ConnectionString;
        public int ID { get; set; }
        public int Download_MasterID { get; set; }
        public int FileID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int LawsonID { get; set; }
        public string EmpName { get; set; }
        public string NTID { get; set; }
        public string Domain { get; set; }
        public string EmailID { get; set; }
        public int IsRating { get; set; }
        public DateTime? Rating_Date { get; set; }
        public string IPAddress { get; set; }
        public string CreatedDate { get; set; }

        public int Insert()
        {
            int Result;
            SqlCommand Cmd = new SqlCommand();
            try
            {
                Cmd.Connection = new SqlConnection(strCon);
                Cmd.Connection.Open();
                Cmd.CommandText = "SP_Insert_Article_Download_Logs";
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.Parameters.Add("@Download_MasterID", SqlDbType.Int).Value = Download_MasterID;
                Cmd.Parameters.Add("@FileID", SqlDbType.Int).Value = FileID;
                Cmd.Parameters.Add("@FileName", SqlDbType.VarChar).Value = FileName;
                Cmd.Parameters.Add("@LawsonID", SqlDbType.Int).Value = null;
                Cmd.Parameters.Add("@EmpName", SqlDbType.VarChar).Value = Convert.ToString(HttpContext.Current.Session["PP_FullName"]);
                Cmd.Parameters.Add("@NTID", SqlDbType.VarChar).Value = Convert.ToString(HttpContext.Current.Session["PP_NTID"]);
                Cmd.Parameters.Add("@Domain", SqlDbType.VarChar).Value = Convert.ToString(HttpContext.Current.Session["PP_Domain"]);
                Cmd.Parameters.Add("@EmailID", SqlDbType.VarChar).Value = Convert.ToString(HttpContext.Current.Session["EmailAddress"]);
                Cmd.Parameters.Add("@IsRating", SqlDbType.Int).Value = IsRating;
                Cmd.Parameters.Add("@Rating_Date", SqlDbType.VarChar).Value = Rating_Date;
                Cmd.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Utility.GetIPAddress();

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

        // Brand Center Download Logs Insertion

        public int BC_Insert()
        {
            int Result;
            SqlCommand Cmd = new SqlCommand();
            try
            {
                Cmd.Connection = new SqlConnection(strCon);
                Cmd.Connection.Open();
                Cmd.CommandText = "SP_Insert_BC_Article_Download_Logs";
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.Parameters.Add("@Download_MasterID", SqlDbType.Int).Value = Download_MasterID;
                Cmd.Parameters.Add("@FileID", SqlDbType.Int).Value = FileID;
                Cmd.Parameters.Add("@FileName", SqlDbType.VarChar).Value = FileName;
                Cmd.Parameters.Add("@LawsonID", SqlDbType.Int).Value = null;
                Cmd.Parameters.Add("@EmpName", SqlDbType.VarChar).Value = Convert.ToString(HttpContext.Current.Session["PP_FullName"]);
                Cmd.Parameters.Add("@NTID", SqlDbType.VarChar).Value = Convert.ToString(HttpContext.Current.Session["PP_NTID"]);
                Cmd.Parameters.Add("@Domain", SqlDbType.VarChar).Value = Convert.ToString(HttpContext.Current.Session["PP_Domain"]);
                Cmd.Parameters.Add("@EmailID", SqlDbType.VarChar).Value = Convert.ToString(HttpContext.Current.Session["EmailAddress"]);

                Cmd.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Utility.GetIPAddress();

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

        public int BC_FilePreview_Insert_Logs()
        {
            int Result;
            SqlCommand Cmd = new SqlCommand();
            try
            {
                Cmd.Connection = new SqlConnection(strCon);
                Cmd.Connection.Open();
                Cmd.CommandText = "SP_Insert_BC_FilePreview_Logs";
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;

                Cmd.Parameters.Add("@FileID", SqlDbType.Int).Value = FileID;
                Cmd.Parameters.Add("@FileName", SqlDbType.VarChar).Value = FileName;
                Cmd.Parameters.Add("@LawsonID", SqlDbType.Int).Value = null;
                Cmd.Parameters.Add("@EmpName", SqlDbType.VarChar).Value = Convert.ToString(HttpContext.Current.Session["PP_FullName"]);
                Cmd.Parameters.Add("@NTID", SqlDbType.VarChar).Value = Convert.ToString(HttpContext.Current.Session["PP_NTID"]);
                Cmd.Parameters.Add("@Domain", SqlDbType.VarChar).Value = Convert.ToString(HttpContext.Current.Session["PP_Domain"]);
                Cmd.Parameters.Add("@EmailID", SqlDbType.VarChar).Value = Convert.ToString(HttpContext.Current.Session["EmailAddress"]);

                Cmd.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Utility.GetIPAddress();

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

        public int BC_AddToCart_Details()
        {
            int Result;
            SqlCommand Cmd = new SqlCommand();
            try
            {
                Cmd.Connection = new SqlConnection(strCon);
                Cmd.Connection.Open();
                Cmd.CommandText = "SP_Insert_BC_AddToCart_Logs";
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;

                Cmd.Parameters.Add("@FileID", SqlDbType.Int).Value = FileID;
                Cmd.Parameters.Add("@FileName", SqlDbType.VarChar).Value = FileName;
                Cmd.Parameters.Add("@LawsonID", SqlDbType.Int).Value = null;
                Cmd.Parameters.Add("@EmpName", SqlDbType.VarChar).Value = Convert.ToString(HttpContext.Current.Session["PP_FullName"]);
                Cmd.Parameters.Add("@NTID", SqlDbType.VarChar).Value = Convert.ToString(HttpContext.Current.Session["PP_NTID"]);
                Cmd.Parameters.Add("@Domain", SqlDbType.VarChar).Value = Convert.ToString(HttpContext.Current.Session["PP_Domain"]);
                Cmd.Parameters.Add("@EmailID", SqlDbType.VarChar).Value = Convert.ToString(HttpContext.Current.Session["EmailAddress"]);

                Cmd.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Utility.GetIPAddress();

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

        public int BC_FileHitCounter(string Flag)
        {
            int Result;
            SqlCommand Cmd = new SqlCommand();
            try
            {
                Cmd.Connection = new SqlConnection(strCon);
                Cmd.Connection.Open();
                Cmd.CommandText = "SP_Insert_BC_FileHitCounter";
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;

                Cmd.Parameters.Add("@FileID", SqlDbType.Int).Value = FileID;
                Cmd.Parameters.Add("@FileName", SqlDbType.VarChar).Value = FileName;
                Cmd.Parameters.Add("@LawsonID", SqlDbType.Int).Value = null;
                Cmd.Parameters.Add("@EmpName", SqlDbType.VarChar).Value = Convert.ToString(HttpContext.Current.Session["PP_FullName"]);
                Cmd.Parameters.Add("@NTID", SqlDbType.VarChar).Value = Convert.ToString(HttpContext.Current.Session["PP_NTID"]);
                Cmd.Parameters.Add("@Domain", SqlDbType.VarChar).Value = Convert.ToString(HttpContext.Current.Session["PP_Domain"]);
                Cmd.Parameters.Add("@EmailID", SqlDbType.VarChar).Value = Convert.ToString(HttpContext.Current.Session["EmailAddress"]);

                Cmd.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Utility.GetIPAddress();
                Cmd.Parameters.Add("@Flag", SqlDbType.VarChar).Value = Flag;

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


        public int getMaxFileDownloadID()
        {
            int maxID = 0;
            SqlConnection con = new SqlConnection(strCon);
            try
            {
                string query = " SELECT (ISNULL(MAX(Download_MasterID),0)+1) from  Article_Download_Logs";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    maxID = Convert.ToInt16(dt.Rows[0][0]);
                }
                return maxID;
            }
            catch (Exception Ex)
            {
                return 0;
                throw Ex;
            }
            finally
            {
                con.Close();
            }
        }

        public int BC_getMaxFileDownloadID()
        {
            int maxID = 0;
            SqlConnection con = new SqlConnection(strCon);
            try
            {
                string query = " SELECT (ISNULL(MAX(DownloadMasterID),0)+1) from  BC_ArticleDownloadLogs";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    maxID = Convert.ToInt16(dt.Rows[0][0]);
                }
                return maxID;
            }
            catch (Exception Ex)
            {
                return 0;
                throw Ex;
            }
            finally
            {
                con.Close();
            }
        }

        public DataTable GetFildIDsForFeedBack(Int16 Downloadid)
        {
            SqlConnection con = new SqlConnection(strCon);
            try
            {
                string query = " SELECT d.Portal_Name,e.Category,b.Download_MasterID,b.ExpiryDate, a.* from Portal_File_Details a \n" +
                               " INNER JOIN Article_Download_Logs b on a.FileID = b.FileID \n" +
                               " INNER JOIN Portal_master d on a.Portal_ID = d.ID \n" +
                               " INNER JOIN Base_Category_Master e on a.Top_Category_ID = e.ID  \n" +
                               " LEFT JOIN Article_Feedback_Rating c on c.FileID = b.FileID \n" +
                               " WHERE a.ISFile = 1  AND b.Download_MasterID = " + Downloadid;
                SqlDataAdapter da = new SqlDataAdapter(query, con);
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

        public DataTable GetDataForFeedBack(string NTID, string Domain)
        {
            SqlConnection con = new SqlConnection(strCon);
            try
            {
                string query = " SELECT a.*,b.IsRating,e.Category from Portal_File_Details a  \n" +
                               " INNER JOIN (select distinct FileID,IsRating,NTID,Domain,max(CreatedDate) DownloadDate from Article_Download_Logs where NTID='" + NTID + "' \n" +
                               " and Domain='" + Domain + "' and IsRating = 0 group by FileID,IsRating,NTID,Domain) b on a.FileID = b.FileID \n" +
                               " INNER JOIN Portal_master d on a.Portal_ID = d.ID \n" +
                               " INNER JOIN Base_Category_Master e on a.Top_Category_ID = e.ID  \n" +
                    //" LEFT JOIN Article_Feedback_Rating c on c.FileID = b.FileID \n" +
                               " WHERE a.ISFile = 1  and  NTID='" + NTID + "' \n" +
                               " and Domain='" + Domain + "'";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
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

        public DataTable GetDataForFeedBackByFileID(Int16 fileid)
        {
            SqlConnection con = new SqlConnection(strCon);
            try
            {
                string query = " SELECT a.*,b.IsRating,e.Category from Portal_File_Details a  \n" +
                               " INNER JOIN (select distinct FileID,IsRating,NTID,Domain from Article_Download_Logs where NTID='" + Convert.ToString(HttpContext.Current.Session["NTID"]) + "' \n" +
                               " and Domain='" + Convert.ToString(HttpContext.Current.Session["Domain"]) + "' and IsRating = 0) b on a.FileID = b.FileID \n" +
                               " INNER JOIN Portal_master d on a.Portal_ID = d.ID \n" +
                               " INNER JOIN Base_Category_Master e on a.Top_Category_ID = e.ID  \n" +
                               " LEFT JOIN Article_Feedback_Rating c on c.FileID = b.FileID \n" +
                               " WHERE a.ISFile = 1 and a.FileID=" + fileid + "  and  NTID='" + Convert.ToString(HttpContext.Current.Session["PP_NTID"]) + "' \n" +
                               " and Domain='" + Convert.ToString(HttpContext.Current.Session["PP_Domain"]) + "'";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
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

        public DataTable GetDownloadFileDataByID(Int16 Downloadid)
        {
            SqlConnection con = new SqlConnection(strCon);
            try
            {
                string query = "select b.Download_MasterID,b.FileID, a.FilePath,a.FileName, b.ExpiryDate from Portal_File_Details as a inner join Article_Download_Logs as b " +
                                "on a.FileID = b.FileID" +
                                " where a.ISFile=1 and b.Download_MasterID=" + Downloadid;
                SqlDataAdapter da = new SqlDataAdapter(query, con);
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
    }
}