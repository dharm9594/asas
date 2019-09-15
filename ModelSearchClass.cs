using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GCBC_NextGen.model
{
    public class ModelSearchClass 
    {
        string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnStr"].ConnectionString;
        string GlobalMappingCon = System.Configuration.ConfigurationManager.ConnectionStrings["GlobalMappingStr"].ConnectionString;

        public DataTable DeleteRecentWordSearched(string WordSearched,string Domain, string NTID)
        {
            DataTable table=new DataTable();
            SqlCommand Cmd = new SqlCommand();
            try
            {
                Cmd.Connection = new SqlConnection(strCon);
                Cmd.Connection.Open();
                Cmd.CommandText = "USP_InsertGCBCSearchedWordLog";
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.Parameters.Add("@Flag", SqlDbType.VarChar).Value = "Delete";
                Cmd.Parameters.Add("@SearchedWord", SqlDbType.VarChar).Value = WordSearched;
                Cmd.Parameters.Add("@SearchedByDomain", SqlDbType.VarChar).Value = Domain;
                Cmd.Parameters.Add("@SearchedByNTID", SqlDbType.VarChar).Value = NTID;
               // Cmd.Parameters.Add("@Emp_Name", SqlDbType.VarChar).Value = Emp_Name;
                //Cmd.Parameters.Add("@isLogin", SqlDbType.VarChar).Value = isLogin;
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                da.Fill(table);
                Cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                if (Cmd.Connection.State == ConnectionState.Open)
                    Cmd.Connection.Close();
                throw ex;
            }
            return table;
        }

        public DataTable SelectRecentWordSearched(string Domain, string NTID)
        {
            DataTable table = new DataTable();
            SqlCommand Cmd = new SqlCommand();
            try
            {
                Cmd.Connection = new SqlConnection(strCon);
                Cmd.Connection.Open();
                Cmd.CommandText = "USP_InsertGCBCSearchedWordLog";
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.Parameters.Add("@Flag", SqlDbType.VarChar).Value = "Select";
                Cmd.Parameters.Add("@SearchedByDomain", SqlDbType.VarChar).Value = Domain;
                Cmd.Parameters.Add("@SearchedByNTID", SqlDbType.VarChar).Value = NTID;
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                da.Fill(table);
                Cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                if (Cmd.Connection.State == ConnectionState.Open)
                    Cmd.Connection.Close();
                throw ex;
            }
            return table;
        }

        public DataSet getBCFile_Detailsdata_ByID(string fileid,string Domain, string NTID)
        {
            DataSet table = new DataSet();
            SqlCommand Cmd = new SqlCommand();
            try
            {
                Cmd.Connection = new SqlConnection(strCon);
                Cmd.Connection.Open();
                Cmd.CommandText = "USP_getBCFile_Detailsdata_ByID";
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.Parameters.Add("@FileID", SqlDbType.VarChar).Value = fileid;
                Cmd.Parameters.Add("@Domain", SqlDbType.VarChar).Value = Domain;
                Cmd.Parameters.Add("@NTID", SqlDbType.VarChar).Value = NTID;
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                da.Fill(table);
                Cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                if (Cmd.Connection.State == ConnectionState.Open)
                    Cmd.Connection.Close();
                throw ex;
            }
            return table;
        }
        public DataTable getGCFile_Detailsdata_ByID(string fileid,string Domain, string NTID)
        {
            DataTable table = new DataTable();
            SqlCommand Cmd = new SqlCommand();
            try
            {
                Cmd.Connection = new SqlConnection(strCon);
                Cmd.Connection.Open();
                Cmd.CommandText = "USP_getGCFile_Detailsdata_ByID";
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.Parameters.Add("@FileID", SqlDbType.VarChar).Value = fileid;
                Cmd.Parameters.Add("@Doamin", SqlDbType.VarChar).Value = Domain;
                Cmd.Parameters.Add("@NTID", SqlDbType.VarChar).Value = NTID;
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                da.Fill(table);
                Cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                if (Cmd.Connection.State == ConnectionState.Open)
                    Cmd.Connection.Close();
                throw ex;
            }
            return table;
        }
        public  DataTable InsertRecentWordSearched(string searchedData,string Domain, string NTID,string createdby)
        {
            DataTable Result=new DataTable();
            SqlCommand Cmd = new SqlCommand();
            try
            {
                Cmd.Connection = new SqlConnection(strCon);
                Cmd.Connection.Open();
                Cmd.CommandText = "USP_InsertGCBCSearchedWordLog";
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.Parameters.Add("@Flag", SqlDbType.VarChar).Value = "Insert";
                Cmd.Parameters.Add("@SearchedWord", SqlDbType.VarChar).Value = searchedData;
                Cmd.Parameters.Add("@SearchedByDomain", SqlDbType.VarChar).Value = Domain;
                Cmd.Parameters.Add("@SearchedByNTID", SqlDbType.VarChar).Value = NTID;
                Cmd.Parameters.Add("@SearchedBy", SqlDbType.VarChar).Value = createdby;
                SqlDataAdapter sda = new SqlDataAdapter(Cmd);
                sda.Fill(Result);
                //Result = Cmd.ExecuteNonQuery();
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

        public static DataTable GetFileDataByFileID_json(string id,string sortBy,string arrange)
        {
            string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnStr"].ConnectionString;
            SqlConnection con = new SqlConnection(strCon);
            try
            {
                string NTID = Convert.ToString(HttpContext.Current.Session["PP_NTID"]) == "" ? Convert.ToString(HttpContext.Current.Session["NTID"]) : Convert.ToString(HttpContext.Current.Session["PP_NTID"]);
                string Domain = Convert.ToString(HttpContext.Current.Session["PP_Domain"]) == "" ? Convert.ToString(HttpContext.Current.Session["Domain"]) : Convert.ToString(HttpContext.Current.Session["PP_Domain"]);

                string query = " Select f.Portal_Name, g.Category ,a.*, f.Portal_Name + ' > ' + b.Categories Categories,ISNULL(e.FileID,'0') favouriteFileID, ISNULL(c.AverageRating,'0')AverageRating, \n" +
                              " ISNULL(d.TotalComment,0) as TotalComment , \n" +
                              " CASE WHEN  ISNULL(a.UpdatedDate,'') = '' THEN \n" +
                              " convert(varchar,a.CreatedDate,105) + ' ' + convert(nvarchar,CAST(a.CreatedDate as time),100)  \n" +
                              " ELSE convert(varchar,a.UpdatedDate,105) + ' ' + convert(nvarchar,CAST(a.UpdatedDate as time),100) END UploadedDate \n" +
                              " ,ISNULL(i.FavouritesCnt,'0') FavouritesCnt ,ISNULL(j.FileID,'0')AddToCartFileID from portal_file_details a  \n" +
                              " INNER JOIN ( \n" +
                              " SELECT  t.FileID  \n" +
                              "        ,STUFF((SELECT ' > ' + CAST(CategoryName AS VARCHAR(100)) [text()]  \n" +
                              "          FROM Portal_File_Details \n" +
                              "          WHERE FileID = t.FileID  \n" +
                              "          FOR XML PATH(''), TYPE)  \n" +
                              "         .value('.','NVARCHAR(MAX)'),1,2,' ') Categories  \n" +
                              " FROM Portal_File_Details t  \n" +
                              " GROUP BY t.FileID ) b on a.FileID = b.FileID \n" +
                              " LEFT JOIN (SELECT cast(AVG(CONVERT(float,Rating))AS DECIMAL(8,2)) as AverageRating, \n" +
                              " FileID as AverageFileID FROM Article_Feedback_Rating group by FileID ) c on a.FileID = c.AverageFileID \n" +
                              " LEFT JOIN (select FileID CommentFileID ,count(*)TotalComment from Article_Feedback_Rating  where (Comments <> '' or Comments is not null) group by FileID) d on a.FileID = d.CommentFileID \n" +
                              " LEFT JOIN (select * from Favorite_Article where CreatedNTID = '" + NTID + "' and CreatedDomain = '" + Domain + "') e on a.FileID = e.FileID \n" +
                              " INNER JOIN Portal_master f on a.Portal_ID = f.ID \n" +
                              " INNER JOIN Base_Category_Master g on a.Top_Category_ID = g.ID \n" +
                              " left outer join (select count(*) as FavouritesCnt,FileID from  Favorite_Article group by FileID) i on a.FileID = i.FileID \n" +
                              " LEFT JOIN (select * from AddToCartFiles where NTID = '" + NTID + "' and Domain = '" + Domain + "') j on a.FileID = j.FileID \n" +
                              " where ISFile = 1 and IsPublished = 1 and a.FileID in" + '(' + id + ')' + "order by a."+sortBy +" "+ arrange;

                //string query = "  select * from Portal_File_Details_BC where FILEID=" + id;
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

        public static DataTable BC_GetFileDataByFileID(string id,string sortBy,string arrange)
        {
            string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnStr"].ConnectionString;
            SqlConnection con = new SqlConnection(strCon);
            try
            {
                string NTID = Convert.ToString(HttpContext.Current.Session["PP_NTID"]) == "" ? Convert.ToString(HttpContext.Current.Session["NTID"]) : Convert.ToString(HttpContext.Current.Session["PP_NTID"]);
                string Domain = Convert.ToString(HttpContext.Current.Session["PP_Domain"]) == "" ? Convert.ToString(HttpContext.Current.Session["Domain"]) : Convert.ToString(HttpContext.Current.Session["PP_Domain"]);

                string query = "  select * from Portal_File_Details_BC where FILEID in (" + id + ") order by " + sortBy + " " + arrange + " ";
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
