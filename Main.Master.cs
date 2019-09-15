using GCBC_NextGen.model;
using System;
using System.Collections.Generic;
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
    public partial class Sites : System.Web.UI.MasterPage
    {
        ModelSearchClass searchClass = new ModelSearchClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            // if (!IsPostBack)
            // {
            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["PP_NTID"])))
                {
                }
                else 
                {
                    Response.Redirect("LoginSSO.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("LoginSSO.aspx", false);
            }
            //  }
        }
    }
}