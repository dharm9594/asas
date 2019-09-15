using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GCBC_NextGen.model
{
    public class ModelFavorite_Article
    {
        string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnStr"].ConnectionString;
        public int ID { get; set; }
        public int FileID { get; set; }
        public string FileName { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDomain { get; set; }
        public string CreatedNTID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string IPAddress { get; set; }

        public string Insert()
        {
            string Result = "";
            SqlCommand Cmd = new SqlCommand();
            try
            {
                Cmd.Connection = new SqlConnection(strCon);
                Cmd.Connection.Open();
                Cmd.CommandText = "SP_Insert_Favorite_Article";
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.Parameters.Add("@FileID", SqlDbType.Int).Value = FileID;
                Cmd.Parameters.Add("@FileName", SqlDbType.VarChar).Value = FileName;
                Cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = CreatedBy;
                Cmd.Parameters.Add("@CreatedDomain", SqlDbType.VarChar).Value = CreatedDomain;
                Cmd.Parameters.Add("@CreatedNTID", SqlDbType.VarChar).Value = CreatedNTID;
                Cmd.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Utility.GetIPAddress();
                Cmd.Parameters.Add("@Message", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                Cmd.ExecuteNonQuery();
                Result = Cmd.Parameters["@Message"].Value.ToString();
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

        public DataTable GetUserfavoriteArticleList()
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
    }
}