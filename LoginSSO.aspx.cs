using GCBC_NextGen.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GCBC_NextGen.view.PP
{
    public partial class LoginSSO : System.Web.UI.Page
    {
        ModelLogin ml = new ModelLogin();
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] userstrarr;
            string domainntidstr, domain = "apc", ntid = "dyada011";

            //Response.Write(User.Identity.Name);
          //  domainntidstr = "APC\\aaddu002";
            domainntidstr = User.Identity.Name;
            if (domainntidstr.Contains('\\'))
            {
                userstrarr = domainntidstr.Split('\\');
                domain = userstrarr[0];
                ntid = userstrarr[1];
            }

            if (Request.QueryString["error"] != null)
            {

                lblError.Text = "User Does Not exists. - " + domain + "\\" + ntid + " Kindly contact Administrator" ;


                ml.InsertBCAccessFailureLogs(domain, ntid, "User Does Not exists");

            }
            else
            {
                ModelLogin objMdlLogin = new ModelLogin();
                try
                {
     
                    string Domain = domain;
                    string NTID = ntid;
                    IntPtr token = IntPtr.Zero;

                    bool bLogonUser;
                    DataTable dtAcccess = objMdlLogin.GetAccess(Domain, NTID);

                    //if (dtAcccess.Rows.Count > 0)
                    bLogonUser = true;
                    //else
                    //    bLogonUser = LogonUser(NTID, Domain, TxtPassword.Text.ToString(), 2, 0, ref token);


                    int bGetUserDetail = Get_User_Detail(NTID, Domain);
                    if (bLogonUser)
                    {
                        if (bGetUserDetail != 0)
                        {
                            if (string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                            {
                                HttpContext.Current.Session["NTID_Val"] = ntid;
                                HttpContext.Current.Session["PP_NTID"] = NTID;
                                HttpContext.Current.Session["PP_Domain"] = Domain;

                                Response.Cookies["userid"].Expires = DateTime.Now.AddDays(-1);
                                Response.Cookies["pwd"].Expires = DateTime.Now.AddDays(-1);
                                FormsAuthentication.RedirectFromLoginPage(NTID, false);

                                //TODO: added by janmesh to store gosposotioncode
                                string LawsonNew = objMdlLogin.GetLawsonIDfromGlobalNew(NTID, Domain);
                                string GosCode = objMdlLogin.GetGosPositionCode(LawsonNew);
                                DataTable DtRoles = new DataTable();
                                DtRoles = objMdlLogin.GetRoles(GosCode);
                                //HttpContext.Current.Session["PP_RollID"] = DtRoles.Rows[0]["RoleId"].ToString();
                                HttpContext.Current.Session["PP_RoleName"] = GosCode;
                                HttpContext.Current.Session["PP_LawsonID"] = LawsonNew;
                                DataTable dt = objMdlLogin.FunGetUserDetail(NTID);
                                InsertLoginLog(domain, ntid);
                                if (dt.Rows.Count != 0)
                                {
                                    string AdminID = Convert.ToString(dt.Rows[0]["NTID"].ToString());
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        if (NTID == AdminID)
                                        {
                                            Session["PP_RoleID"] = dt.Rows[0]["RoleID"].ToString();
                                            //Session["RoleName"] = dt.Rows[0]["RoleName"].ToString();
                                        }
                                    }
                                }
                                else
                                {
                                    Session["PP_RoleID"] = null;
                                }

                                //if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Text"])))
                                //{
                                //    int value = Convert.ToUInt16(Utility.Decrypt(Request.QueryString["Text"]));
                                //    Response.Redirect("Notifications.aspx?id=" + value, false);
                                //}
                                //else
                                //{
                                Response.Redirect("Home.aspx", false);
                                //}
                                Context.ApplicationInstance.CompleteRequest();
                            }
                            else
                            {
                                FormsAuthentication.SetAuthCookie(NTID, false);
                                Response.Redirect("Login.aspx");
                            }
                        }
                        else
                        {
                            lblError.Text = "No Role Assigned To User "+domain+"\\"+ntid;

                            ml.InsertBCAccessFailureLogs(domain, ntid, "No Role Assigned To User");


                            lblError.Visible = true;
                        }

                    }
                    else
                    {
                        //LblError.Text = "Invalid User Name or Password.";
                        //LblError.Visible = true;
                    }
                }

                catch (ThreadAbortException)
                {
                }
                catch (Exception Ex)
                {
                    Utility.CreateErrorLog(Ex.Message.ToString(), "Login.aspx", "btnLogin_Click");
                    //LblError.Text = "Invalid User Name or Password.";
                    Response.Redirect("LoginSSO.aspx?error=1", false);
                }
            }


        }

        public static string GetUsername(string usernameDomain)
        {
            if (string.IsNullOrEmpty(usernameDomain))
            {
                throw (new ArgumentException("Argument can't be null.", "usernameDomain"));
            }

            if (usernameDomain.Contains("\\"))
            {
                int index = usernameDomain.IndexOf("\\");
                return usernameDomain.Substring(index + 1);
            }
            else if (usernameDomain.Contains("@"))
            {
                int index = usernameDomain.IndexOf("@");
                return usernameDomain.Substring(0, index);
            }
            else
                return usernameDomain;
        }

        private int Get_User_Detail(string UserName, string DomainName)
        {
            ModelLogin objMdlLogin = new ModelLogin();
            string LawsonNew = objMdlLogin.GetLawsonIDfromGlobalNew(UserName, DomainName);
            string GosCode = objMdlLogin.GetGosPositionCode(LawsonNew);
            DataTable DtRoles = new DataTable();
            DtRoles = objMdlLogin.GetRoles(GosCode);
            int accesstoGC = Convert.ToInt32(DtRoles.Rows[0]["accessToGC"].ToString());
            return accesstoGC;
            //return objMdlLogin.GetUserDetails(UserName, DomainName);
        }

        private void InsertLoginLog(string domain, string ntid)
        {
            ModelLogin objMdlLogin = new ModelLogin();
            string EmployeeID = string.Empty, userName = string.Empty, EmailAddress = string.Empty, telephone = string.Empty, firstname = string.Empty, lastName = string.Empty, GosCode = string.Empty;
            PrincipalContext pc;
            if (domain == "ACTICALL")
                pc = new PrincipalContext(ContextType.Domain, "ACTICALL", "DC=acticall,DC=com");
            else
                pc = new PrincipalContext(ContextType.Domain, Convert.ToString(domain));
            UserPrincipal user = new UserPrincipal(pc);
            user = UserPrincipal.FindByIdentity(pc, Convert.ToString(ntid));
            if (user != null)
            {
                userName =  user.GivenName + " " + user.Surname;
                EmailAddress = user.EmailAddress;
                telephone = user.VoiceTelephoneNumber;
                firstname = user.GivenName;
                lastName = user.Surname;
                EmployeeID = user.EmployeeId;
                GosCode = user.Description;
            }
            else
                userName = Convert.ToString(ntid);

            Session["PP_FullName"] = userName;
            Session["EmailAddress"] = EmailAddress;
            Session["PP_EmployeeID"] = Session["PP_LawsonID"];
            if (domain == "ACTICALL")
            {
                HttpContext.Current.Session["PP_RoleName"] = GosCode;
                Session["PP_EmployeeID"] = EmployeeID;
            }

            objMdlLogin.InsertLoginLog(Convert.ToString(userName), Convert.ToString(ntid), Convert.ToString(domain), "PP");
        }
    }
}