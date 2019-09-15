//using Spire.Presentation;
//using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;

namespace GCBC_NextGen.model
{
    public class Utility
    {
        public static string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnStr"].ConnectionString;
       public  Utility() { }

        #region "Check DataTable"
        public static bool HasRowsDS(DataSet DS, int iTableIndex)
        {
            if (DS == null) return false;
            if (DS.Tables[iTableIndex] == null) return false;
            if (DS.Tables[iTableIndex].Rows.Count <= 0) return false;
            return true;
        }

        public static bool HasRowsDT(DataTable DT)
        {
            if (DT == null) return false;
            if (DT.Rows.Count <= 0) return false;
            return true;
        }
        #endregion

        #region "MessageBox"


       

        public static void MessageBox(string Msg)
        {
            string script = "alert('" + Msg + "');";
            Guid guidKey = Guid.NewGuid();
            Page page = HttpContext.Current.Handler as Page;
            ScriptManager.RegisterStartupScript(page, page.GetType(), guidKey.ToString(), script, true);
        }

        public static void MessageBox(string Msg, string RedirectURL)
        {
            string script = "alert('" + Msg + "'); window.parent.location='" + RedirectURL + "';";
            Guid guidKey = Guid.NewGuid();
            Page page = HttpContext.Current.Handler as Page;
            ScriptManager.RegisterStartupScript(page, page.GetType(), guidKey.ToString(), script, true);
        }

        public static void MessageBoxAjax(string Msg, UpdatePanel UpPnl)
        {
            string script = "alert('" + Msg + "');";
            Guid guidKey = Guid.NewGuid();
            ScriptManager.RegisterStartupScript(UpPnl, UpPnl.GetType(), "script", script, true);
        }

        public static void MessageBoxError(String ex, Page pg, Object obj)
        {
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }
        #endregion

        #region "GET IP Address"
        public static string GetIPAddress()
        {
            string IPAddress = "";
            try
            {
                IPAddress = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
                IPAddress = string.IsNullOrEmpty(IPAddress) ? "127.0.0.1" : IPAddress;
            }
            catch (Exception Ex) { }
            return IPAddress;
        }
        #endregion

        #region "Read Excel File"
        public static DataTable ReadExcelToDataTable(string FilePath, string Extension, string SheetName)
        {
            DataTable DT = new DataTable();
            string ExcelConStr = "";

            try
            {
                switch (Extension)
                {
                    case ".xls": //Excel 97-03
                        ExcelConStr = @"Provider=Microsoft.Jet.OLEDB.4.0;" +
                                 @"Data Source=" + FilePath + ";" +
                                 @"Extended Properties=" + Convert.ToChar(34).ToString() +
                                 @"Excel 8.0;HDR=YES" + Convert.ToChar(34).ToString();
                        break;
                    case ".xlsx": //Excel 07
                        ExcelConStr = @"Provider=Microsoft.ACE.OLEDB.12.0;" +
                                 @"Data Source=" + FilePath + ";" +
                                 @"Extended Properties=" + Convert.ToChar(34).ToString() +
                                 @"Excel 12.0;HDR=YES" + Convert.ToChar(34).ToString();
                        //conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=Excel 14.0;HDR=YES";
                        break;
                }

                OleDbConnection connExcel = new OleDbConnection(ExcelConStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter OleDbAdpt = new OleDbDataAdapter();

                cmdExcel.Connection = connExcel;

                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "$]";
                OleDbAdpt.SelectCommand = cmdExcel;
                OleDbAdpt.Fill(DT);
                connExcel.Close();
            }
            catch (Exception Ex) { throw Ex; }
            return DT;
        }
        #endregion

        #region "Get WeekNo & Year"
        public static DataTable GetWeekNoYear()
        {
            SqlConnection SqlCon = new SqlConnection(strConn);
            DataTable DT = new DataTable();
            try
            {
                SqlCon.Open();
                string Query = "SELECT DATEPART(WK,GETDATE()) AS WeekNo, YEAR(GETDATE()) AS [Year]";
                using (SqlCommand SqlCmd = new SqlCommand(Query, SqlCon))
                {
                    SqlCmd.CommandType = System.Data.CommandType.Text;
                    SqlDataAdapter SqlAdpt = new SqlDataAdapter(SqlCmd);
                    SqlAdpt.Fill(DT);
                }
            }
            catch (Exception Ex) { SqlCon.Close(); throw Ex; }
            finally { SqlCon.Close(); }
            return DT;
        }
        #endregion

        #region "Send Email"
        public static void SendEmail(string strFrom, string strTo, string strCC, string strBcc, string strSubject, string strBody)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();

                string[] TO = strTo.Split(';');
                if (TO[0] != "")
                {
                    for (int i = 0; i < TO.Length; i++)
                    {
                        if (TO[i].Trim() != "")
                        {
                            mailMessage.To.Add(TO[i].Trim());
                        }
                    }
                }

                if (!string.IsNullOrEmpty(strCC))
                {
                    string[] CC = strBcc.Split(';');
                    if (CC[0] != "")
                    {
                        for (int i = 0; i < CC.Length; i++)
                        {
                            if (CC[i].Trim() != "")
                            {
                                mailMessage.CC.Add(CC[i].Trim());
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(strBcc))
                {
                    string[] BCC = strBcc.Split(';');
                    if (BCC[0] != "")
                    {
                        for (int i = 0; i < BCC.Length; i++)
                        {
                            if (BCC[i].Trim() != "")
                            {
                                mailMessage.Bcc.Add(BCC[i].Trim());
                            }
                        }
                    }
                }

                mailMessage.From = new MailAddress(Convert.ToString(strFrom));
                mailMessage.Subject = strSubject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = strBody;
                SmtpClient smtpClient = new SmtpClient("10.252.252.135", 25);
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex) { throw; }
        }

        public static void SendEmail(string strFrom, string strTo, string strCC, string strBcc, string strSubject, string strBody, string Attachement)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                Attachment Attmnt = new Attachment(Attachement);
                string[] TO = strTo.Split(';');
                if (TO[0] != "")
                {
                    for (int i = 0; i < TO.Length; i++)
                    {
                        if (TO[i].Trim() != "")
                        {
                            mailMessage.To.Add(TO[i].Trim());
                        }
                    }
                }

                if (!string.IsNullOrEmpty(strCC))
                {
                    string[] CC = strBcc.Split(';');
                    if (CC[0] != "")
                    {
                        for (int i = 0; i < CC.Length; i++)
                        {
                            if (CC[i].Trim() != "")
                            {
                                mailMessage.CC.Add(CC[i].Trim());
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(strBcc))
                {
                    string[] BCC = strBcc.Split(';');
                    if (BCC[0] != "")
                    {
                        for (int i = 0; i < BCC.Length; i++)
                        {
                            if (BCC[i].Trim() != "")
                            {
                                mailMessage.Bcc.Add(BCC[i].Trim());
                            }
                        }
                    }
                }

                mailMessage.From = new MailAddress(Convert.ToString(strFrom));
                mailMessage.Subject = strSubject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Attachments.Add(Attmnt);
                mailMessage.Body = strBody;
                SmtpClient smtpClient = new SmtpClient("10.252.252.135", 25);
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex) { throw; }
        }
        public static void SendEmail(string strFrom, string strTo, string strCC, string strBcc, string strSubject, string strBody, System.Net.Mail.Attachment Attachement)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                //Attachment Attmnt = new Attachment(Attachement);
                string[] TO = strTo.Split(';');
                if (TO[0] != "")
                {
                    for (int i = 0; i < TO.Length; i++)
                    {
                        if (TO[i].Trim() != "")
                        {
                            mailMessage.To.Add(TO[i].Trim());
                        }
                    }
                }

                if (!string.IsNullOrEmpty(strCC))
                {
                    string[] CC = strBcc.Split(';');
                    if (CC[0] != "")
                    {
                        for (int i = 0; i < CC.Length; i++)
                        {
                            if (CC[i].Trim() != "")
                            {
                                mailMessage.CC.Add(CC[i].Trim());
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(strBcc))
                {
                    string[] BCC = strBcc.Split(';');
                    if (BCC[0] != "")
                    {
                        for (int i = 0; i < BCC.Length; i++)
                        {
                            if (BCC[i].Trim() != "")
                            {
                                mailMessage.Bcc.Add(BCC[i].Trim());
                            }
                        }
                    }
                }

                mailMessage.From = new MailAddress(Convert.ToString(strFrom));
                mailMessage.Subject = strSubject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Attachments.Add(Attachement);
                mailMessage.Body = strBody;
                SmtpClient smtpClient = new SmtpClient("10.252.252.135", 25);
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex) { throw; }
        }
        #endregion

        #region "Encrypt/Decrypt"
        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        #endregion

        #region "Error Log"
        public static void CreateErrorLog(string ErrorMsg, string PageName, string EventName)
        {
            string strFileName = "ErrorLog.txt";
            string sDirectory = HttpContext.Current.Server.MapPath("~/ErrorLog/");
            if (!Directory.Exists(sDirectory))
                Directory.CreateDirectory(sDirectory);

            StreamWriter sw;
            if (!File.Exists(sDirectory + strFileName))
                sw = new StreamWriter(sDirectory + strFileName, true);
            else
                sw = File.AppendText(sDirectory + strFileName);

            sw.WriteLine("Data Time ==> " + DateTime.Now.ToString("dd MMM yyyy HH:mm:ss"));
            sw.WriteLine("Error ==> " + ErrorMsg);
            sw.WriteLine("Page Name ==> " + PageName);
            sw.WriteLine("Event Name ==> " + EventName);
            //sw.WriteLine("Error Line No.:" + Ex.StackTrace.Substring(Ex.StackTrace.LastIndexOf(":line") + 5).ToString());
            sw.WriteLine("");
            sw.Close();
        }

        public static void CreateErrorLog(string ErrorMsg, string PageName, string EventName, string DirPath)
        {
            string strFileName = "ErrorLog.txt";
            string sDirectory = HttpContext.Current.Server.MapPath("~/ErrorLog/");
            if (!Directory.Exists(sDirectory))
                Directory.CreateDirectory(sDirectory);

            StreamWriter sw;
            if (!File.Exists(sDirectory + strFileName))
                sw = new StreamWriter(sDirectory + strFileName, true);
            else
                sw = File.AppendText(sDirectory + strFileName);

            sw.WriteLine("Data Time ==> " + DateTime.Now.ToString("dd MMM yyyy HH:mm:ss"));
            sw.WriteLine("Error ==> " + ErrorMsg);
            sw.WriteLine("Page Name ==> " + PageName);
            sw.WriteLine("Event Name ==> " + EventName);
            //sw.WriteLine("Error Line No.:" + Ex.StackTrace.Substring(Ex.StackTrace.LastIndexOf(":line") + 5).ToString());
            sw.WriteLine("");
            sw.Close();
        }
        #endregion

        #region "Get Name of NTID Domain"
        public string GetFullName(string Domain, string NTID)
        {
            string strFullName = string.Empty;
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, Domain))
            {
                UserPrincipal user = new UserPrincipal(pc);
                user = UserPrincipal.FindByIdentity(pc, NTID);
                if (user != null)
                    strFullName = user.GivenName + " " + user.Surname;
            }
            return strFullName;
        }
        #endregion

        #region "Generate Excel File using ExcelPackage class (Assembly Location : \\10.223.2.59\Software\Projects\Dev\Assemblies\ExcelPackage.dll)"
        //public static string GetGenerateExcelFile(string strAppendText, DataTable DT)
        //{
        //    string FullPath = "", strFileName = "";
        //    try
        //    {
        //        strFileName = strAppendText + DateTime.Now.ToString("ddMMMyyyyHHmmssfff") + ".xlsx";
        //        string FilePath = HttpContext.Current.Server.MapPath("ExcelDownloads/");
        //        DirectoryInfo DirInfo = new DirectoryInfo(FilePath);
        //        if (!DirInfo.Exists)
        //            Directory.CreateDirectory(FilePath);

        //        FullPath = FilePath + strFileName;
        //        FileInfo FInfo = new FileInfo(FullPath);
        //        using (ExcelPackage xlPackage = new ExcelPackage(FInfo))
        //        {
        //            ExcelWorksheet workSheet = xlPackage.Workbook.Worksheets.Add(strAppendText);

        //            if (Utility.HasRowsDT(DT))
        //            {
        //                for (int j = 0; j <= DT.Rows.Count - 1; j++)
        //                {
        //                    for (int k = 0; k <= DT.Columns.Count - 1; k++)
        //                    {
        //                        if (j == 0)
        //                            workSheet.Cell(1, k + 1).Value = Convert.ToString(DT.Columns[k]); //For Column Header

        //                        workSheet.Cell(2 + j, k + 1).Value = Convert.ToString(DT.Rows[j][k]); //For Column Cell Value
        //                    }
        //                }
        //                xlPackage.Save();
        //            }
        //        }
        //    }
        //    catch (Exception Ex) { }
        //    return strFileName;
        //}
        #endregion

        #region "Generate Excel File using EPP class (Assembly Location : \\10.223.2.59\Software\Projects\Dev\Assemblies\EPPlus.dll)"
        //public static string GetGenerateExcelFile(string strAppendText, DataTable DT)
        //{
        //    string FullPath = "", strFileName = "";
        //    try
        //    {
        //        strFileName = strAppendText + DateTime.Now.ToString("ddMMMyyyyHHmmssfff") + ".xlsx";
        //        string FilePath = HttpContext.Current.Server.MapPath("ExcelDownloads/");
        //        DirectoryInfo DirInfo = new DirectoryInfo(FilePath);
        //        if (!DirInfo.Exists)
        //            Directory.CreateDirectory(FilePath);

        //        FullPath = FilePath + strFileName;
        //        FileInfo FInfo = new FileInfo(FullPath);
        //        using (ExcelPackage xlPackage = new ExcelPackage(FInfo))
        //        {
        //            ExcelWorksheet workSheet = xlPackage.Workbook.Worksheets.Add(strAppendText);

        //            if (Utility.HasRowsDT(DT))
        //            {
        //                for (int j = 0; j <= DT.Rows.Count - 1; j++)
        //                {
        //                    for (int k = 0; k <= DT.Columns.Count - 1; k++)
        //                    {
        //                        if (j == 0)
        //                        {
        //                            workSheet.Cells[1, k + 1].Value = Convert.ToString(DT.Columns[k]); //For Column Header
        //                            workSheet.Cells[1, k + 1].Style.Font.Bold = true;
        //                        }

        //                        workSheet.Cells[2 + j, k + 1].Value = Convert.ToString(DT.Rows[j][k]); //For Column Cell Value
        //                        //workSheet.Cells[2 + j, k + 1].Style.Font.Bold = true;
        //                        //if (Convert.ToString(DT.Rows[j][k]) == "Pending")
        //                        //    workSheet.Cells[2 + j, k + 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(1, 255, 85, 0));
        //                        //else if (Convert.ToString(DT.Rows[j][k]) == "Completed")
        //                        //    workSheet.Cells[2 + j, k + 1].Style.Fill.BackgroundColor.SetColor(Color.Green);
        //                        //else if (Convert.ToString(DT.Rows[j][k]) == "Rejected")
        //                        //    workSheet.Cells[2 + j, k + 1].Style.Fill.BackgroundColor.SetColor(Color.Red);
        //                    }
        //                }
        //                xlPackage.Save();
        //            }
        //        }
        //    }
        //    catch (Exception Ex) { }
        //    return strFileName;
        //}
        #endregion

        #region "Find Matching Values In 2 Arrays"
        public static bool IsMachingArray(string[] fixedColumns, string[] matchColumns)
        {
            bool bMatchedColums = fixedColumns.Any(fxC => !matchColumns.Contains(fxC)) ? false : true;
            return bMatchedColums;
        }
        #endregion

        #region "File Upload"

        public static string GetUploadMultipleFileName(HttpPostedFile FlUpld, string Prefix)
        {
            string sOriginalFileName = "", sFileName = "", sDirectory = "", sFullPath = "";
            try
            {
                if (FlUpld.ContentLength > 0)
                {
                    sOriginalFileName = System.IO.Path.GetFileName(FlUpld.FileName).Replace(",", "");
                    sDirectory = HttpContext.Current.Server.MapPath("../UploadedFiles/");

                    if (!System.IO.Directory.Exists(sDirectory))
                        System.IO.Directory.CreateDirectory(sDirectory);

                    string strRandon = System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetRandomFileName().ToUpper()) + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    sFileName = Prefix + System.IO.Path.GetFileNameWithoutExtension(sOriginalFileName).ToString() + "_" + strRandon + System.IO.Path.GetExtension(sOriginalFileName);
                    sFullPath = sDirectory + sFileName;
                    FlUpld.SaveAs(sFullPath);
                    sFileName = "UploadedFiles/" + sFileName;
                }
            }
            catch (Exception Ex) { Utility.CreateErrorLog(Ex.Message.ToString(), "Utility", "GetUploadMultipleFileName", "../ErrorLog/"); throw Ex; }
            return sFileName;
        }
        #endregion

        public static string GetSearchData(string prefix, string whereClause)
        {
            List<string> result = new List<string>();
            string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnStr"].ConnectionString;
            string query = " SELECT distinct a.FileID,a.FileName,b.Portal_Name,c.Category,a.FileType,Description,a.Tags  \n" +
                           " FROM Portal_File_Details  a \n" +
                           " INNER JOIN Portal_master b on a.Portal_ID = b.ID \n" +
                           " INNER JOIN Base_Category_Master c on a.Top_Category_ID = c.ID \n" +
                           " WHERE ISFile = 1 AND IsPublished = 1 AND (FileName like '%" + prefix + "%'  OR Description like '%" + prefix + "%' OR a.Tags like '%" + prefix + "%') " + whereClause + " order by a.FileID desc";

            SqlDataAdapter da = new SqlDataAdapter(query, strCon);
            DataTable dt = new DataTable();
            da.Fill(dt);

            string demo = "[";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string description = Regex.Replace(dt.Rows[i]["Description"].ToString(), @"[^0-9a-zA-Z]+", ",");
                demo += "{\"FileID\": " + dt.Rows[i]["FileID"].ToString() + ",\"FileName\": \"" + dt.Rows[i]["FileName"].ToString() + "\",\"Portal_Name\": \"" + dt.Rows[i]["Portal_Name"].ToString() + "\",\"FileType\": \"" + dt.Rows[i]["FileType"].ToString() + "\",\"SearchBy\":\"\",\"FileDescription\": \"" + description + "\",\"Tags\": \"" + dt.Rows[i]["Tags"].ToString() + "\"},";
            }
            demo = demo.TrimEnd(',');
            demo += "]";
            Guid guidKey = Guid.NewGuid();
            Page page = HttpContext.Current.Handler as Page;
            ScriptManager.RegisterStartupScript(page, page.GetType(), guidKey.ToString(), "setFileData('" + demo + "');", true);
            return demo;
        }

        public static string RemoveSpecialChars(string str)
        {
            string[] chars = new string[] { "/", "@", "#", "$", "%", "^", "*", "'", "\"", "(", ")", "|", "[", "]", "~", "<", ">", "`", "{", "}" };
            for (int i = 0; i < chars.Length; i++)
            {
                // check if given containt special charter
                if (str.Contains(chars[i]))
                {
                    str = str.Replace(chars[i], "");
                }
            }
            return str;
        }

        public static string[] mediaExtensions = {                                            
                                            ".WAV", ".MID", ".MIDI", ".WMA", ".MP3", ".OGG", ".RMA", //etc
                                            ".AVI", ".MP4", ".DIVX", ".WMV", //etc
                                          };

        public static bool IsMediaFile(string path)
        {
            return -1 != Array.IndexOf(mediaExtensions, Path.GetExtension(path).ToUpperInvariant());
        }

        public static bool isAdmin(string NTID)
        {
            ModelMasters masters = new ModelMasters();
            DataTable dt = masters.getadminAccess();
            bool isAdmin = false;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (NTID == Convert.ToString(dt.Rows[i]["NTID"]))
                {
                    isAdmin = true;
                    break;
                }
            }
            return isAdmin;
        }

        public static bool isPortalAccess(string NTID, string PortalName)
        {
            ModelMasters masters = new ModelMasters();
            DataTable dt = masters.getPortalAccessByName_User(NTID, PortalName);
            bool isAccess = false;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (NTID == Convert.ToString(dt.Rows[i]["NTID"]))
                {
                    isAccess = true;
                    break;
                }
            }
            return isAccess;
        }

        public static string MailDocumentBody(string FilePath, string message)
        {
            StringBuilder sbMailBody = new StringBuilder();
            string Linkpath = "";
            FilePath = FilePath.ToLower().Replace(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ProjectLink"]).ToLower(), "");
            if (GCBC_NextGen.model.Utility.IsMediaFile(FilePath))
                Linkpath = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ProjectLink"]) + "view/PP/FileViewer.aspx?src=" + GCBC_NextGen.model.Utility.Encrypt(FilePath);
            else
                //Linkpath = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ProjectLink"]) + FilePath;
                Linkpath = FilePath;


            sbMailBody.Append("<body style='width: 100% !important; height: 100%; line-height: 1.6; background-color: #f6f6f6;'>" +
               "<table style='background-color: #f6f6f6; width: 100%;'>" +
               "<tr>" +
               "<td></td>" +
               "<td style='display: block !important; max-width: 600px !important; margin: 0 auto !important; clear: both !important;' width='600'>" +
               "<div style='max-width: 600px; margin: 0 auto; display: block; padding: 20px;'>" +
               "<table style='background: #fff; border: 1px solid #e9e9e9; border-radius: 3px;' width='100%' cellpadding='0' cellspacing='0'>" +
               "<tr>" +
               "<td style='background: #004a74; font-size: 18px; color: #fff; font-weight: 600; padding: 20px; text-align: center; border-radius: 3px 3px 0 0;'>Growth Center" +
               "</td>" +
               "</tr>" +
               "<tr>" +
               "<td style='padding: 20px;'>" +
               "<table width='100%' cellpadding='0' cellspacing='0' border='1'>" +
               "<tr>" +
               "<td class='content-block' style='padding: 10px; width:200px;'>Link For Download" +
               "</td>" +
               "<td class='content-block' style='padding: 10px;'>" +
               "<span style='margin-left:2%;'><a href='" + Linkpath + "'>" + Path.GetFileName(FilePath) + "</a></span>" +
               "</td>" +
               "</tr>" +
               "<tr>" +
               "<td class='content-block' style='padding: 10px; width:200px;'>Expiry Date" +
               "</td>" +
               "<td class='content-block' style='padding: 10px;'>" +
               "<span style='margin-left:2%;'>" + DateTime.Now.AddDays(2).ToShortDateString() + "</span>" +
               "</td>" +
               "</tr>" +
               "<tr>" +
               "<td class='content-block' colspan='2'>" + message + "" +
               "</td>" +
               "</tr>" +
               "</table>" +
               "</td>" +
               "</tr>" +
               "</table>" +
               "</div>" +
               "</td><td></td>" +
               "</tr></table>" +
               "</body>");

            return sbMailBody.ToString();
        }

        public static string MailDocumentBody_BC(string FilePath, string message)
        {
            StringBuilder sbMailBody = new StringBuilder();
            string Linkpath = "";
            FilePath = FilePath.ToLower().Replace(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ProjectLink"]).ToLower(), "");
            if (GCBC_NextGen.model.Utility.IsMediaFile(FilePath))
                Linkpath = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ProjectLink"]) + "view/PP/FileViewer.aspx?src=" + GCBC_NextGen.model.Utility.Encrypt(FilePath);
            else
                Linkpath = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ProjectLink"]) + FilePath;
            //Linkpath = FilePath;


            sbMailBody.Append("<body style='width: 100% !important; height: 100%; line-height: 1.6; background-color: #f6f6f6;'>" +
               "<table style='background-color: #f6f6f6; width: 100%;'>" +
               "<tr>" +
               "<td></td>" +
               "<td style='display: block !important; max-width: 600px !important; margin: 0 auto !important; clear: both !important;' width='600'>" +
               "<div style='max-width: 600px; margin: 0 auto; display: block; padding: 20px;'>" +
               "<table style='background: #fff; border: 1px solid #e9e9e9; border-radius: 3px;' width='100%' cellpadding='0' cellspacing='0'>" +
               "<tr>" +
               "<td style='background: #004a74; font-size: 18px; color: #fff; font-weight: 600; padding: 20px; text-align: center; border-radius: 3px 3px 0 0;'>Growth Center" +
               "</td>" +
               "</tr>" +
               "<tr>" +
               "<td style='padding: 20px;'>" +
               "<table width='100%' cellpadding='0' cellspacing='0' border='1'>" +
               "<tr>" +
               "<td class='content-block' style='padding: 10px; width:200px;'>Link For Download" +
               "</td>" +
               "<td class='content-block' style='padding: 10px;'>" +
               "<span style='margin-left:2%;'><a href='" + Linkpath + "'>" + Path.GetFileName(FilePath) + "</a></span>" +
               "</td>" +
               "</tr>" +
               "<tr>" +
               "<td class='content-block' style='padding: 10px; width:200px;'>Expiry Date" +
               "</td>" +
               "<td class='content-block' style='padding: 10px;'>" +
               "<span style='margin-left:2%;'>" + DateTime.Now.AddDays(2).ToShortDateString() + "</span>" +
               "</td>" +
               "</tr>" +
               "<tr>" +
               "<td class='content-block' colspan='2'>" + message + "" +
               "</td>" +
               "</tr>" +
               "</table>" +
               "</td>" +
               "</tr>" +
               "</table>" +
               "</div>" +
               "</td><td></td>" +
               "</tr></table>" +
               "</body>");

            return sbMailBody.ToString();
        }


       

    }
}