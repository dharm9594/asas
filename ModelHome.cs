using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GCBC_NextGen.model
{
    public class ModelHome
    {
        string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnStr"].ConnectionString;

        public DataTable GetUserfavoriteArticleList(string CreatedNTID, string CreatedDomain)
        {
            SqlConnection con = new SqlConnection(strCon);
            try
            {
                string query = " SELECT isnull(FavouritesCnt,0)FavouritesCnt,a.*, ISNULL(d.FileID,'0') favouriteFileID, b.Categories,e.category, \n" +
                               " convert(varchar,a.CreatedDate,105) + ' ' + convert(nvarchar,CAST(a.CreatedDate as time),100) UploadedDate, ISNULL(h.FileID,'0')AddToCartFileID from portal_file_details a \n" +
                               " INNER JOIN ( \n" +
                               " SELECT  t.FileID  \n" +
                               "        ,STUFF((SELECT ' > ' + CAST(CategoryName AS VARCHAR(100)) [text()]  \n" +
                               "          FROM Portal_File_Details \n" +
                               "          WHERE FileID = t.FileID  \n" +
                               "          FOR XML PATH(''), TYPE)  \n" +
                               "         .value('.','NVARCHAR(MAX)'),1,2,' ') Categories  \n" +
                               " FROM Portal_File_Details t  \n" +
                               " GROUP BY t.FileID ) b on a.FileID = b.FileID \n" +
                               " INNER JOIN (select * from Portal_master where Active='Y') c on a.Portal_ID = c.ID  \n" +
                               " INNER JOIN Favorite_Article d on  a.FileID = d.FileID \n" +
                               " INNER Join Base_Category_Master e on a.Top_Category_ID = e.ID \n" +
                               " left outer join (select count(*) as FavouritesCnt,FileID from  Favorite_Article group by FileID) g on a.FileID = g.FileID \n" +
                               " LEFT JOIN (select * from AddToCartFiles where NTID = '" + CreatedNTID + "' and Domain = '" + CreatedDomain + "') h on a.FileID = h.FileID \n" +
                               " WHERE ISFile = 1 and d.CreatedNTID = '" + CreatedNTID + "' and d.CreatedDomain = '" + CreatedDomain + "'";
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

        public DataTable getLatest()
        {
            SqlConnection con = new SqlConnection(strCon);
            try
            {
                string query = " SELECT TOP 6 isnull(FavouritesCnt,0)FavouritesCnt,a.*, ISNULL(d.FileID,'0') favouriteFileID, b.Categories,e.category,  \n" +
                               " convert(varchar,a.CreatedDate,105) + ' ' + convert(nvarchar,CAST(a.CreatedDate as time),100) UploadedDate, ISNULL(h.FileID,'0')AddToCartFileID from portal_file_details a \n" +
                               " INNER JOIN ( \n" +
                               " SELECT  t.FileID  \n" +
                               "        ,STUFF((SELECT ' > ' + CAST(CategoryName AS VARCHAR(100)) [text()]  \n" +
                               "          FROM Portal_File_Details \n" +
                               "          WHERE FileID = t.FileID  \n" +
                               "          FOR XML PATH(''), TYPE)  \n" +
                               "         .value('.','NVARCHAR(MAX)'),1,2,' ') Categories  \n" +
                               " FROM Portal_File_Details t  \n" +
                               " GROUP BY t.FileID ) b on a.FileID = b.FileID \n" +
                               " INNER JOIN (select * from Portal_master where Active='Y') c on a.Portal_ID = c.ID  \n" +
                               " LEFT JOIN Favorite_Article d on  a.FileID = d.FileID \n" +
                               " INNER Join Base_Category_Master e on a.Top_Category_ID = e.ID \n" +
                               " left outer join (select count(*) as FavouritesCnt,FileID from  Favorite_Article group by FileID) g on a.FileID = g.FileID \n" +
                               " LEFT JOIN (select * from AddToCartFiles ) h on a.FileID = h.FileID \n" +
                               " WHERE ISFile = 1  AND IsPublished = 1 order by UpdatedDate desc";
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

        public DataTable getCartDetails(string CreatedNTID, string CreatedDomain)
        {
            SqlConnection con = new SqlConnection(strCon);
            try
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = con;
                Cmd.Connection.Open();
                Cmd.CommandText = "sp_GetCartDetails";
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@NTID", CreatedNTID);
                Cmd.Parameters.AddWithValue("@Domain", CreatedDomain);
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
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


        public Int32 DeleteCartFile(Int32 FileID)
        {
            string SqlQuery = "delete from AddToCartFiles where FileID=@FileID and NTID=@NTID";
            Int32 iRetVal = 0;
            SqlConnection SqlCon = new SqlConnection(strCon);
            try
            {
                using (SqlCommand SqlCmd = new SqlCommand(SqlQuery, SqlCon))
                {
                    SqlCon.Open();
                    SqlCmd.CommandType = System.Data.CommandType.Text;
                    SqlCmd.Parameters.AddWithValue("@FileID", FileID);
                    SqlCmd.Parameters.AddWithValue("@NTID", Convert.ToString(HttpContext.Current.Session["PP_NTID"]));

                    iRetVal = Convert.ToInt32(SqlCmd.ExecuteNonQuery());
                }
            }
            catch (Exception Ex) { throw Ex; }
            finally { SqlCon.Close(); }
            return iRetVal;
        }

        public Int32 BCDeleteCartFile(Int32 FileID)
        {
            string SqlQuery = "delete from BC_AddToCartFiles where FileID=@FileID and NTID=@NTID";
            Int32 iRetVal = 0;
            SqlConnection SqlCon = new SqlConnection(strCon);
            try
            {
                using (SqlCommand SqlCmd = new SqlCommand(SqlQuery, SqlCon))
                {
                    SqlCon.Open();
                    SqlCmd.CommandType = System.Data.CommandType.Text;
                    SqlCmd.Parameters.AddWithValue("@FileID", FileID);
                    SqlCmd.Parameters.AddWithValue("@NTID", Convert.ToString(HttpContext.Current.Session["PP_NTID"]));

                    iRetVal = Convert.ToInt32(SqlCmd.ExecuteNonQuery());
                }
            }
            catch (Exception Ex) { throw Ex; }
            finally { SqlCon.Close(); }
            return iRetVal;
        }
    }
}