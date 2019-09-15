using GCBC_NextGen.model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GCBC_NextGen.View.PP
{
    public partial class SITE2 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["PP_NTID"])))
                {
                    lblLoginName.Text = Convert.ToString(Session["PP_FullName"]);
                }
            }
        }

        [WebMethod]
        public static string getApi(string api)
        {
            HttpWebRequest requestFl = (HttpWebRequest)WebRequest.Create(api);
            requestFl.Method = "Get";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            requestFl.KeepAlive = true;
            requestFl.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            requestFl.UseDefaultCredentials = true;
            requestFl.Credentials = new NetworkCredential("gcsquery", "WovRong4graf");
            requestFl.ContentType = "application/json";

            HttpWebResponse responseFl = (HttpWebResponse)requestFl.GetResponse();

            string myResponseFl = "";
            using (System.IO.StreamReader srFl = new System.IO.StreamReader(responseFl.GetResponseStream()))
            {
                myResponseFl = srFl.ReadToEnd();
            }
            return myResponseFl;
        }

        [WebMethod]
        public static string getImageVideoByID(string fileID, string sortBy, string arrange)
        {

            DataTable table = ModelSearchClass.BC_GetFileDataByFileID(fileID,sortBy,arrange);
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            var lst = table.AsEnumerable().Select(r => r.Table.Columns.Cast<DataColumn>().Select(c => new KeyValuePair<string, object>(c.ColumnName, r[c.Ordinal])).ToDictionary(z => z.Key, z => z.Value)).ToList();
            return jsSerializer.Serialize(lst);
        }

        [WebMethod]
        public static string getGCFiledataByID(string fileID, string sortBy, string arrange)
        {

            DataTable table = ModelSearchClass.GetFileDataByFileID_json(fileID,sortBy,arrange);
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            var lst = table.AsEnumerable().Select(r => r.Table.Columns.Cast<DataColumn>().Select(c => new KeyValuePair<string, object>(c.ColumnName, r[c.Ordinal])).ToDictionary(z => z.Key, z => z.Value)).ToList();
            return jsSerializer.Serialize(lst);
        }

        [WebMethod]
        public static string saveSearchedword(string Data)
        {
            ModelSearchClass searchClass = new ModelSearchClass();
            int _EmployeeId = Convert.ToInt32(HttpContext.Current.Session["PP_EmployeeID"]);
            try
            {
                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                DataTable dt = searchClass.InsertRecentWordSearched(Data, Convert.ToString(HttpContext.Current.Session["PP_Domain"]), Convert.ToString(HttpContext.Current.Session["PP_NTID"]), Convert.ToString(HttpContext.Current.Session["PP_FullName"]));
                var lst = dt.AsEnumerable().Select(r => r.Table.Columns.Cast<DataColumn>().Select(c => new KeyValuePair<string, object>(c.ColumnName, r[c.Ordinal])).ToDictionary(z => z.Key, z => z.Value)).ToList();
                return jsSerializer.Serialize(lst);
            }
            catch (Exception Ex)
            {
                Utility.MessageBox("Error Occurred !!! Please contact system administrator.");
                Utility.CreateErrorLog(Ex.Message.ToString(), "Home.aspx", "Save", "../ErrorLog/");
            }
            return "";
        }

        [WebMethod]
        public static string DeleteSearchedword(string Data)
        {
            ModelSearchClass searchClass = new ModelSearchClass();
            int _EmployeeId = Convert.ToInt32(HttpContext.Current.Session["PP_EmployeeID"]);
            try
            {
                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                DataTable dt = searchClass.DeleteRecentWordSearched(Data, Convert.ToString(HttpContext.Current.Session["PP_Domain"]), Convert.ToString(HttpContext.Current.Session["PP_NTID"]));
                var lst = dt.AsEnumerable().Select(r => r.Table.Columns.Cast<DataColumn>().Select(c => new KeyValuePair<string, object>(c.ColumnName, r[c.Ordinal])).ToDictionary(z => z.Key, z => z.Value)).ToList();
                return jsSerializer.Serialize(lst);
            }
            catch (Exception Ex)
            {
                Utility.MessageBox("Error Occurred !!! Please contact system administrator.");
                Utility.CreateErrorLog(Ex.Message.ToString(), "Home.aspx", "Save", "../ErrorLog/");
            }
            return "";
        }

        [WebMethod]
        public static string SelectSearchedword()
        {
            ModelSearchClass searchClass = new ModelSearchClass();
            int _EmployeeId = Convert.ToInt32(HttpContext.Current.Session["PP_EmployeeID"]);
            try
            {
                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                DataTable dt = searchClass.SelectRecentWordSearched(Convert.ToString(HttpContext.Current.Session["PP_Domain"]), Convert.ToString(HttpContext.Current.Session["PP_NTID"]));
                var lst = dt.AsEnumerable().Select(r => r.Table.Columns.Cast<DataColumn>().Select(c => new KeyValuePair<string, object>(c.ColumnName, r[c.Ordinal])).ToDictionary(z => z.Key, z => z.Value)).ToList();
                return jsSerializer.Serialize(lst);
            }
            catch (Exception Ex)
            {
                Utility.MessageBox("Error Occurred !!! Please contact system administrator.");
                Utility.CreateErrorLog(Ex.Message.ToString(), "Home.aspx", "Save", "../ErrorLog/");
            }
            return "";
        }

        [WebMethod]
        public static string getFavorites()
        {
            ModelHome modelHome = new ModelHome();
            DataTable table = modelHome.GetUserfavoriteArticleList(Convert.ToString(HttpContext.Current.Session["PP_NTID"].ToString()), Convert.ToString(HttpContext.Current.Session["PP_Domain"].ToString()));
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            var lst = table.AsEnumerable().Select(r => r.Table.Columns.Cast<DataColumn>().Select(c => new KeyValuePair<string, object>(c.ColumnName, r[c.Ordinal])).ToDictionary(z => z.Key, z => z.Value)).ToList();
            return jsSerializer.Serialize(lst);
        }

        [WebMethod]
        public static string getNew()
        {
            ModelHome modelHome = new ModelHome();
            DataTable table = modelHome.getLatest();
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            var lst = table.AsEnumerable().Select(r => r.Table.Columns.Cast<DataColumn>().Select(c => new KeyValuePair<string, object>(c.ColumnName, r[c.Ordinal])).ToDictionary(z => z.Key, z => z.Value)).ToList();
            return jsSerializer.Serialize(lst);
        }

        [WebMethod]
        public static string getCartDetails()
        {
            ModelHome modelHome = new ModelHome();
            DataTable table = modelHome.getCartDetails(Convert.ToString(HttpContext.Current.Session["PP_NTID"].ToString()), Convert.ToString(HttpContext.Current.Session["PP_Domain"].ToString()));
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            var lst = table.AsEnumerable().Select(r => r.Table.Columns.Cast<DataColumn>().Select(c => new KeyValuePair<string, object>(c.ColumnName, r[c.Ordinal])).ToDictionary(z => z.Key, z => z.Value)).ToList();
            return jsSerializer.Serialize(lst);
        }

        [WebMethod]
        public static int AddToCart(AjaxFileData AddToCart)
        {
            int val = 0;
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DbConnStr"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("IF NOT EXISTS (SELECT * FROM AddToCartFiles where FileID=@FileID and NTID=@NTID) " +
                        "BEGIN INSERT INTO AddToCartFiles VALUES(@FileID, @FileName,@FilePath,@Status,@CreatedDate,@NTID,@Domain) END"))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@FileID", AddToCart.FileID);
                        cmd.Parameters.AddWithValue("@FileName", AddToCart.FileName);
                        cmd.Parameters.AddWithValue("@FilePath", AddToCart.FilePath);
                        cmd.Parameters.AddWithValue("@Status", 1);
                        cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@NTID", HttpContext.Current.Session["PP_NTID"].ToString());
                        cmd.Parameters.AddWithValue("@Domain", HttpContext.Current.Session["PP_Domain"].ToString());

                        cmd.Connection = con;
                        con.Open();
                        val = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch (Exception Ex)
            {
                GCBC_NextGen.model.Utility.MessageBox("Error Occurred !!! Please contact system administrator.");
                GCBC_NextGen.model.Utility.CreateErrorLog(Ex.Message.ToString(), "Main,Master.aspx", "AddToCart", "../ErrorLog/");
                throw;
            }
            return val;
        }


        [WebMethod]
        public static string ArticleFavoriteLogs(ModelFavorite_Article FavoriteLogs)
        {
            string result = "";
            try
            {
                FavoriteLogs.CreatedBy = Convert.ToString(HttpContext.Current.Session["PP_FullName"]);
                FavoriteLogs.CreatedNTID = Convert.ToString(HttpContext.Current.Session["PP_NTID"].ToString());
                FavoriteLogs.CreatedDomain = Convert.ToString(HttpContext.Current.Session["PP_Domain"].ToString());
                result = FavoriteLogs.Insert();
            }
            catch (Exception Ex)
            {
                GCBC_NextGen.model.Utility.MessageBox("Error Occurred !!! Please contact system administrator.");
                GCBC_NextGen.model.Utility.CreateErrorLog(Ex.Message.ToString(), "Home.aspx", "ArticleFavoriteLogs", "../ErrorLog/");
                throw;
            }
            return result;
        }

        [WebMethod]
        public static string getGCFile_Detailsdata_ByID(string fileID)
        {
            ModelSearchClass searchClass = new ModelSearchClass();
            int _EmployeeId = Convert.ToInt32(HttpContext.Current.Session["PP_EmployeeID"]);
            try
            {
                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                DataTable dt = searchClass.getGCFile_Detailsdata_ByID(fileID, Convert.ToString(HttpContext.Current.Session["PP_Domain"]), Convert.ToString(HttpContext.Current.Session["PP_NTID"]));
                var lst = dt.AsEnumerable().Select(r => r.Table.Columns.Cast<DataColumn>().Select(c => new KeyValuePair<string, object>(c.ColumnName, r[c.Ordinal])).ToDictionary(z => z.Key, z => z.Value)).ToList();
                return jsSerializer.Serialize(lst);
            }
            catch (Exception Ex)
            {
                Utility.MessageBox("Error Occurred !!! Please contact system administrator.");
                Utility.CreateErrorLog(Ex.Message.ToString(), "Home.aspx", "Save", "../ErrorLog/");
            }
            return "";
        }

        [WebMethod]
        public static string SendFeedback(string FileID, string FeedbackVal)
        {
            try
            {
                ModelFeedback feedback = new ModelFeedback();
                feedback.FileID = Convert.ToInt16(FileID);
                feedback.Message = FeedbackVal;
                feedback.Active = 1;
                feedback.Insert();

            }
            catch (Exception Ex)
            {
                Utility.MessageBox("Error Occurred !!! Please contact system administrator.");
                Utility.CreateErrorLog(Ex.Message.ToString(), "Home.aspx", "Save", "../ErrorLog/");
            }

            return "";
        }


        [WebMethod]
        public static string getBCFile_Detailsdata_ByID(string fileID)
        {
            ModelSearchClass searchClass = new ModelSearchClass();
            int _EmployeeId = Convert.ToInt32(HttpContext.Current.Session["PP_EmployeeID"]);
            try
            {
                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                DataSet dt = searchClass.getBCFile_Detailsdata_ByID(fileID, Convert.ToString(HttpContext.Current.Session["PP_Domain"]), Convert.ToString(HttpContext.Current.Session["PP_NTID"]));
                // var lst = dt.Tables[0].AsEnumerable().Select(r => r.Table.Columns.Cast<DataColumn>().Select(c => new KeyValuePair<string, object>(c.ColumnName, r[c.Ordinal])).ToDictionary(z => z.Key, z => z.Value)).ToList();
                //return jsSerializer.Serialize(dt);
                //return dt.GetXml();
                return DataSetExt.GetJSON(dt);
            }
            catch (Exception Ex)
            {
                Utility.MessageBox("Error Occurred !!! Please contact system administrator.");
                Utility.CreateErrorLog(Ex.Message.ToString(), "Home.aspx", "Save", "../ErrorLog/");
            }
            return "";
        }

        [System.Web.Services.WebMethod]
        public static int BC_AddToCart(AjaxFileData BC_AddToCart)
        {
            int val = 0;
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DbConnStr"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    //using (SqlCommand cmd = new SqlCommand("IF NOT EXISTS (SELECT * FROM BC_AddToCartFiles where FileID=@FileID and NTID=@NTID) " +
                    //    "BEGIN INSERT INTO BC_AddToCartFiles VALUES(@FileID, @FileName,@FilePath,@Status,@CreatedDate,@NTID,@Domain) END"))

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "SP_Insert_BC_AddToCart_Files";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@FileID", BC_AddToCart.FileID);
                        cmd.Parameters.AddWithValue("@FileName", BC_AddToCart.FileName);
                        cmd.Parameters.AddWithValue("@FilePath", BC_AddToCart.FilePath);
                        cmd.Parameters.AddWithValue("@Status", 1);
                        // cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@NTID", HttpContext.Current.Session["PP_NTID"].ToString());
                        cmd.Parameters.AddWithValue("@Domain", HttpContext.Current.Session["PP_Domain"].ToString());

                        cmd.Connection = con;
                        con.Open();
                        val = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch (Exception Ex)
            {
                GCBC_NextGen.model.Utility.MessageBox("Error Occurred !!! Please contact system administrator.");
                GCBC_NextGen.model.Utility.CreateErrorLog(Ex.Message.ToString(), "BC_SearchReport.aspx", "AddToCart", "../ErrorLog/");
                throw;
            }
            return val;
        }


        [WebMethod]
        public static string DeleteCartFile(string fileID)
        {
            try
            {
                ModelHome articleDetails = new ModelHome();
                articleDetails.DeleteCartFile(Convert.ToInt32(fileID));
            }
            catch (Exception Ex)
            {
                Utility.MessageBox("Error Occurred !!! Please contact system administrator.");
                Utility.CreateErrorLog(Ex.Message.ToString(), "Home.aspx", "Save", "../ErrorLog/");
            }

            return "";

        }


        [WebMethod]
        public static string BCDeleteCartFile(string fileID)
        {
            try
            {
                ModelHome articleDetails = new ModelHome();
                articleDetails.BCDeleteCartFile(Convert.ToInt32(fileID));
            }
            catch (Exception Ex)
            {
                Utility.MessageBox("Error Occurred !!! Please contact system administrator.");
                Utility.CreateErrorLog(Ex.Message.ToString(), "Home.aspx", "Save", "../ErrorLog/");
            }

            return "";

        }

    }

    public static class DataSetExt
    {
        public static string GetJSON(this DataSet ds)
        {

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            ArrayList root = new ArrayList();
            List<Dictionary<string, object>> table;
            Dictionary<string, object> data;

            foreach (DataTable dt in ds.Tables)
            {
                table = new List<Dictionary<string, object>>();
                foreach (DataRow dr in dt.Rows)
                {
                    data = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        data.Add(col.ColumnName, dr[col]);
                    }
                    table.Add(data);
                }
                root.Add(table);
            }

            return serializer.Serialize(root);
        }
    }
}