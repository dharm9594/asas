using GCBC_NextGen.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GCBC_NextGen.View.PP
{
    public partial class DocViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["FileID"])))
                    {
                        int FIleID = Convert.ToInt16(Convert.ToString(Request.QueryString["FileID"]));
                        ModelProtal_File_Image_Convert file = new ModelProtal_File_Image_Convert();
                        file.FileID = FIleID;
                        DataTable dt = file.GetFileDataByFileID();
                        Repeater1.DataSource = dt;
                        Repeater1.DataBind();
                        Repeater2.DataSource = dt;
                        Repeater2.DataBind();
                    }
                    //else if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["PricingCartID"])))
                    //{
                    //    int PricingCartID = Convert.ToInt16(Convert.ToString(Request.QueryString["PricingCartID"]));
                    //    ModelPricing pricingCart = new ModelPricing();
                    //    string NTID = Convert.ToString(Session["PP_NTID"]);
                    //    DataTable dtPricingCart = pricingCart.getPricingCart(NTID);

                    //    Response.Clear();
                    //    Response.Buffer = true;
                    //    Response.AddHeader("content-disposition", "attachment;filename=I_PricingData.xls");
                    //    Response.Charset = "";
                    //    Response.ContentType = "application/vnd.ms-excel";
                    //    using (StringWriter sw = new StringWriter())
                    //    {
                    //        HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //        //To Export all pages
                    //        GridView gv = new GridView();
                    //        gv.DataSource = dtPricingCart;
                    //        gv.DataBind();

                    //        gv.AllowPaging = false;


                    //        gv.HeaderRow.BackColor = Color.White;
                    //        foreach (TableCell cell in gv.HeaderRow.Cells)
                    //        {
                    //            cell.BackColor = gv.HeaderStyle.BackColor;
                    //        }
                    //        foreach (GridViewRow row in gv.Rows)
                    //        {
                    //            row.BackColor = Color.White;
                    //            foreach (TableCell cell in row.Cells)
                    //            {
                    //                if (row.RowIndex % 2 == 0)
                    //                {
                    //                    cell.BackColor = gv.AlternatingRowStyle.BackColor;
                    //                }
                    //                else
                    //                {
                    //                    cell.BackColor = gv.RowStyle.BackColor;
                    //                }
                    //                cell.CssClass = "textmode";
                    //            }
                    //        }

                    //        gv.RenderControl(hw);

                    //        //style to format numbers to string
                    //        string style = @"<style> .textmode { } </style>";
                    //        Response.Write(style);
                    //        Response.Output.Write(sw.ToString());
                    //        Response.Flush();
                    //        Response.End();
                    //    }
                    //}
                }
            }
            catch (Exception Ex)
            {
                Utility.MessageBox("Error Occurred !!! Please contact system administrator.");
                Utility.CreateErrorLog(Ex.Message.ToString(), "DocViewer.aspx", "Page_Load", "../ErrorLog/");
            }
        }
    }
}