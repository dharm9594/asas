using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GCBC_NextGen.model
{
    public class ModelFeedback
    {
        string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnStr"].ConnectionString;
        public int ID { get; set; }
        //public string Title { get; set; }
        public int FileID { get; set; }
        public string EmailID { get; set; }
        public string Subject { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Message { get; set; }
        public int Active { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDomain { get; set; }
        public string CreatedNTID { get; set; }
        public string CreatedDate { get; set; }
        public string IPAddress { get; set; }
        public DateTime? Created_Date { get; set; }

        public string UpdatedBy { get; set; }
        public string UpdatedDomain { get; set; }
        public string UpdatedNTID { get; set; }
        public string UpdatedDate { get; set; }
        public DateTime? UpdatedIPAddress { get; set; }
        public int IsActionTaken { get; set; }
        public string FeedBack_Action_Comment { get; set; }

        public string Con_Email { get; set; }
        public string Con_Domain { get; set; }
        public string Con_NTID { get; set; }
        public string Con_Empid { get; set; }
        public string Con_Fname { get; set; }
        public string Con_lname { get; set; }
        public string Con_Company { get; set; }
        public string Con_Position { get; set; }
        public string Con_Phone { get; set; }
        public string Con_subject { get; set; }
        public string Con_Createdby { get; set; }
        public DateTime Con_Created_date { get; set; }

        public int Insert()
        {
            int Result;
            SqlCommand Cmd = new SqlCommand();
            try
            {
                Cmd.Connection = new SqlConnection(strCon);
                Cmd.Connection.Open();
                Cmd.CommandText = "SP_Insert_Feedback";
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;

                Cmd.Parameters.Add("@FileID", SqlDbType.VarChar).Value = FileID;
                Cmd.Parameters.Add("@EmailID", SqlDbType.VarChar).Value = EmailID;
                Cmd.Parameters.Add("@Subject", SqlDbType.VarChar).Value = Subject;
                Cmd.Parameters.Add("@FileName", SqlDbType.VarChar).Value = FileName;
                Cmd.Parameters.Add("@FilePath", SqlDbType.VarChar).Value = FilePath;
                Cmd.Parameters.Add("@Message", SqlDbType.VarChar).Value = Message;
                Cmd.Parameters.Add("@Active", SqlDbType.VarChar).Value = Active;

                Cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = Convert.ToString(HttpContext.Current.Session["PP_FullName"]);
                Cmd.Parameters.Add("@CreatedDomain", SqlDbType.VarChar).Value = Convert.ToString(HttpContext.Current.Session["PP_Domain"]);
                Cmd.Parameters.Add("@CreatedNTID", SqlDbType.VarChar).Value = Convert.ToString(HttpContext.Current.Session["PP_NTID"]);
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

       
    }
}