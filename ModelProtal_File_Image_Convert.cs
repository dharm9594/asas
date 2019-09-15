using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GCBC_NextGen.model
{
    public class ModelProtal_File_Image_Convert
    {
        string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnStr"].ConnectionString;
        public int ID { get; set; }
        public int FileID { get; set; }
        public string Image_FilePath { get; set; }
        public string Image_FileName { get; set; }

        public int Insert()
        {
            int Result;
            SqlCommand Cmd = new SqlCommand();
            try
            {
                Cmd.Connection = new SqlConnection(strCon);
                Cmd.Connection.Open();
                Cmd.CommandText = "SP_Insert_Portal_File_Image_Convert";
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.Parameters.Add("@FileID", SqlDbType.Int).Value = FileID;
                Cmd.Parameters.Add("@Image_FilePath", SqlDbType.VarChar).Value = Image_FilePath;
                Cmd.Parameters.Add("@Image_FileName", SqlDbType.VarChar).Value = Image_FileName;

                //Cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = CreatedBy;
                //Cmd.Parameters.Add("@CreatedDomain", SqlDbType.VarChar).Value = CreatedDomain;
                //Cmd.Parameters.Add("@CreatedNTID", SqlDbType.VarChar).Value = CreatedNTID;
                //Cmd.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Utility.GetIPAddress();
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

        public int deleteFileImages(int deleteFileID)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(strCon);
            try
            {
                string query = " delete from  Portal_File_Image_Convert where FileID = " + deleteFileID;
                con.Open();
                SqlCommand Cmd = new SqlCommand(query, con);
                Result = Cmd.ExecuteNonQuery();
                Cmd.Connection.Close();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                con.Close();
            }
            return Result;
        }


        public DataTable GetFileDataByFileID()
        {
            SqlConnection con = new SqlConnection(strCon);
            try
            {
                string NTID = Convert.ToString(HttpContext.Current.Session["PP_NTID"]) == "" ? Convert.ToString(HttpContext.Current.Session["NTID"]) : Convert.ToString(HttpContext.Current.Session["PP_NTID"]);
                string Domain = Convert.ToString(HttpContext.Current.Session["PP_Domain"]) == "" ? Convert.ToString(HttpContext.Current.Session["Domain"]) : Convert.ToString(HttpContext.Current.Session["PP_Domain"]);

                string query = " select * from Portal_File_Image_Convert where FileID = " + FileID;
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