using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GCBC_NextGen.model
{
    public class ModelMasters
    {
        string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnStr"].ConnectionString;
        // string strcon1 = System.Configuration.ConfigurationManager.ConnectionStrings["GlobalMappingStr1"].ConnectionString;

        public DataTable getPortalMaster()
        {
            SqlConnection con = new SqlConnection(strCon);
            try
            {
                string query = " SELECT a.*,b.ID RoleID,b.RoleName  from Portal_master a \n" +
                               " INNER JOIN Portal_Admin b on a.ID = b.ID  where a.active = 'Y' Order by OrderByID";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                return null;
                throw Ex;
            }
            finally
            {
                con.Close();
            }
        }

        public DataTable getadminAccess()
        {
            SqlConnection con = new SqlConnection(strCon);
            try
            {
                string query = " SELECT * from adminAccess";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                return null;
                throw Ex;
            }
            finally
            {
                con.Close();
            }
        }

        public DataTable getPortalAccessByName_User(string NTID, string PortalName)
        {
            SqlConnection con = new SqlConnection(strCon);
            try
            {
                string query = " select * from Portal_Access where NTID = '" + NTID + "' AND Portal_Name = '" + PortalName + "'";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                return null;
                throw Ex;
            }
            finally
            {
                con.Close();
            }
        }

        public DataTable getTagMaster(string whereClause)
        {
            SqlConnection con = new SqlConnection(strCon);
            try
            {
                string query = " SELECT * from Tags_Master where 1=1 " + whereClause + " order by Alphabets,Keywords";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                return null;
                throw Ex;
            }
            finally
            {
                con.Close();
            }
        }

        public int UpdateTags(string Alphabet, string Keyword, int ID)
        {
            int Result;
            SqlConnection con = new SqlConnection(strCon);
            con.Open();
            SqlCommand Cmd = new SqlCommand();
            try
            {
                string UpdateQuery = " Update Tags_Master set Alphabets = '" + Alphabet + "' , Keywords = '" + Keyword + "', " +
                " UpdatedBy = '" + HttpContext.Current.Session["FullName"].ToString() + "'," +
                " UpdatedDomain = '" + HttpContext.Current.Session["Domain"].ToString() + "'," +
                " UpdatedNTID = '" + HttpContext.Current.Session["NTID"].ToString() + "'," +
                " UpdatedDate = GETDATE()," +
                " UpdatedIPAddress = '" + Utility.GetIPAddress() + "'" +
                " WHERE ID" + " = " + ID;
                Cmd.Connection = con;
                Cmd.CommandText = UpdateQuery;
                Result = Cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                if (Cmd.Connection.State == ConnectionState.Open)
                    Cmd.Connection.Close();
                throw ex;
            }
            return Result;
        }

        public DataTable getTagMasterDistAlphbets()
        {
            SqlConnection con = new SqlConnection(strCon);
            try
            {
                string query = " SELECT distinct Alphabets from Tags_Master order by Alphabets";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                return null;
                throw Ex;
            }
            finally
            {
                con.Close();
            }
        }

        public DataTable getThesaurusName(string ThesaurusType)
        {
            SqlConnection con = new SqlConnection(strCon);
            try
            {

                string query = " SELECT DISTINCT ID, Level_4 FROM KnowledgeBase..PortalFiles_Thesaurus_Master WHERE Level_1 = '" + ThesaurusType + "'";

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


        public DataTable getThesaurusfilecount(string Thesaurusid)
        {
            SqlConnection con = new SqlConnection(strCon);
            try
            {

                string query = "select count(*) as filecount from  KnowledgeBase..PortalFiles_Thesaurus where thesaurusid = '" + Thesaurusid + "'";

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

        public DataTable getCategoryByTeam(string TeamName)
        {
            SqlConnection con = new SqlConnection(strCon);
            try
            {
                string query = " SELECT a.*,b.Portal_Name,c.RoleName from Base_Category_Master a \n" +
                               " INNER JOIN  Portal_master b on a.Portal_ID  = b.ID \n" +
                               " INNER JOIN  Portal_Admin c on a.PortalAdmin_ID = c.ID \n" +
                               " WHERE RoleName = '" + TeamName + "'";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                return null;
                throw Ex;
            }
            finally
            {
                con.Close();
            }
        }

        public DataTable getSubCategoryByTeam_Category(string TeamName, string categoryID)
        {
            SqlConnection con = new SqlConnection(strCon);
            try
            {
                string query = " SELECT a.*,b.Portal_Name,c.RoleName,d.level,d.SubType_Name from Base_Category_Master a \n" +
                               " INNER JOIN  Portal_master b on a.Portal_ID  = b.ID  \n" +
                               " INNER JOIN  Portal_Admin c on a.PortalAdmin_ID = c.ID  \n" +
                               " INNER JOIN  Base_Category_SubCategory_Master d on a.ID = d.Category_ID  \n" +
                               " WHERE RoleName = '" + TeamName + "' AND d.Category_ID = '" + categoryID + "'";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                return null;
                throw Ex;
            }
            finally
            {
                con.Close();
            }
        }

        public DataTable getCategoryByID(int CategoryID)
        {
            SqlConnection con = new SqlConnection(strCon);
            try
            {
                string query = " SELECT a.*,b.Portal_Name,c.RoleName from Base_Category_Master a \n" +
                               " INNER JOIN  Portal_master b on a.Portal_ID  = b.ID \n" +
                               " INNER JOIN  Portal_Admin c on a.PortalAdmin_ID = c.ID \n" +
                               " WHERE a.ID = '" + CategoryID + "'";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                return null;
                throw Ex;
            }
            finally
            {
                con.Close();
            }
        }
        public DataTable getCategorysByPortal_ID(int Portal_ID)
        {
            SqlConnection con = new SqlConnection(strCon);
            try
            {
                string query = " SELECT a.*,b.Portal_Name,c.RoleName from Base_Category_Master a \n" +
                               " INNER JOIN  Portal_master b on a.Portal_ID  = b.ID \n" +
                               " INNER JOIN  Portal_Admin c on a.PortalAdmin_ID = c.ID \n" +
                               " WHERE a.Portal_ID = '" + Portal_ID + "'";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                return null;
                throw Ex;
            }
            finally
            {
                con.Close();
            }
        }

        public DataTable getBaseCategoryByWhereClause(string whereClause)
        {
            SqlConnection con = new SqlConnection(strCon);
            try
            {
                string query = " SELECT b.Portal_Name,c.RoleName,a.Category,ISNULL(TotalFiles,0)Total,PositionId FROM Base_Category_Master a \n" +
                               " INNER JOIN Portal_master b on a.Portal_ID = b.ID \n" +
                               " Inner JOIN Portal_Admin c on a.PortalAdmin_ID = c.ID \n" +
                               " LEFT JOIN (Select Top_Category_ID,count(*)TotalFiles from Portal_File_Details where ISFile = 1 AND IsPublished = 1 group by Top_Category_ID)  d on a.ID  = d.Top_Category_ID  \n" +
                               " WHERE 1 = 1 " + whereClause + "\n" +
                               " GROUP BY b.Portal_Name,c.RoleName,a.Category,TotalFiles,PositionId order by PositionId";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                return null;
                throw Ex;
            }
            finally
            {
                con.Close();
            }
        }


        public DataTable Get_FileCount()
        {
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand Cmd = new SqlCommand();
            try
            {
                Cmd.Connection = new SqlConnection(strCon);
                Cmd.Connection.Open();
                Cmd.CommandText = "Get_BCcount";
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                int Result = Cmd.ExecuteNonQuery();
                Cmd.Connection.Close();
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                return null;
                throw Ex;
            }
            finally
            {
                con.Close();
            }
        }


        public DataTable Get_Filename_count(string fileid)
        {
            DataTable dt = new DataTable();
            SqlConnection SqlCon = new SqlConnection(strCon);
            SqlCommand SqlCmd = new SqlCommand("SP_Thesaurus_FileCount", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.CommandTimeout = 10000;
            SqlCmd.Parameters.Add("@Thesaurus_id", SqlDbType.VarChar).Value = fileid;
            SqlDataAdapter SqlAdpt = new SqlDataAdapter(SqlCmd);
            SqlAdpt.Fill(dt);
            return dt;
        }

        public DataTable Get_Filename_datacount(string fileid)
        {
            DataTable dt = new DataTable();
            SqlConnection SqlCon = new SqlConnection(strCon);
            SqlCommand SqlCmd = new SqlCommand("sp_fileidcheck", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@fileid", SqlDbType.VarChar).Value = fileid;
            SqlDataAdapter SqlAdpt = new SqlDataAdapter(SqlCmd);
            SqlAdpt.Fill(dt);
            return dt;
        }

       

        public DataTable getBaseCategoryByWhereClauseNew(string whereClause)
        {
            SqlConnection con = new SqlConnection(strCon);
            try
            {
                string query = " SELECT b.Portal_Name,c.RoleName,a.Category,ISNULL(TotalFiles,0)Total,PositionId FROM Base_Category_Master a \n" +
                               " INNER JOIN Portal_master b on a.Portal_ID = b.ID \n" +
                               " Inner JOIN Portal_Admin c on a.PortalAdmin_ID = c.ID \n" +
                               " LEFT JOIN (Select Top_Category_ID,count(*)TotalFiles from Portal_File_Details where ISFile = 1 AND IsPublished = 1 AND Level='1' group by Top_Category_ID)  d on a.ID  = d.Top_Category_ID  \n" +
                               " WHERE 1 = 1 " + whereClause + "\n" +
                               " GROUP BY b.Portal_Name,c.RoleName,a.Category,TotalFiles,PositionId order by PositionId";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                return null;
                throw Ex;
            }
            finally
            {
                con.Close();
            }
        }

        public DataTable getBaseCategoryDetailsByPortal(string whereClause)
        {
            SqlConnection con = new SqlConnection(strCon);
            try
            {
                string query = " SELECT b.Portal_Name,c.RoleName,a.Category,ISNULL(TotalFiles,0)Total FROM Base_Category_Master a \n" +
                               " INNER JOIN Portal_master b on a.Portal_ID = b.ID \n" +
                               " Inner JOIN Portal_Admin c on a.PortalAdmin_ID = c.ID \n" +
                               " LEFT JOIN (Select Top_Category_ID,count(*)TotalFiles from Portal_File_Details where ISFile = 1 AND IsPublished = 1 group by Top_Category_ID)  d on a.ID  = d.Top_Category_ID  \n" +
                               " WHERE 1 = 1 " + whereClause;

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                return null;
                throw Ex;
            }
            finally
            {
                con.Close();
            }
        }
        public DataTable getPortalCategoryID(string Portal_Name, string Category)
        {
            SqlConnection con = new SqlConnection(strCon);
            try
            {
                string query = "select pm.ID as portal_id,pm.Portal_Name,bcm.ID as Top_Category_ID,bcm.Category from Portal_master pm,Base_Category_Master bcm where pm.ID=bcm.Portal_ID and pm.Portal_Name='" + Portal_Name + "' and bcm.Category='" + Category + "';";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                return null;
                throw Ex;
            }
            finally
            {
                con.Close();
            }
        }

        public DataTable getmenu_photobank()
        {
            SqlConnection con = new SqlConnection(strCon);
            //  SqlCommand Cmd = new SqlCommand();
            try
            {
                // string query = "select distinct CategoryName ,level, Top_Category_ID from Portal_File_Details  where Portal_ID=12 and Top_Category_ID=86 and level=2";

                string query = "Select  SubType_Name CategoryName, level, ID   from Base_Category_SubCategory_Master WHERE Previous_Level_ID = 86 AND level = 2";
                //Cmd.Connection = new SqlConnection(strcon1);
                //Cmd.Connection.Open();
                //Cmd.CommandText = "getmenudata_photobank";
                //Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //Cmd.Connection.Close();
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                return null;
            }


        }

        public DataTable getmenu_videolibrary()
        {
            SqlConnection con = new SqlConnection(strCon);

            try
            {
                //  string query = "select distinct CategoryName ,level from Portal_File_Details where Portal_ID=12 and Top_Category_ID=87 and level=2";
                string query = "select  SubType_Name CategoryName, level, ID   from Base_Category_SubCategory_Master WHERE Previous_Level_ID = 87 AND level = 2";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                return null;
            }


        }
        public DataTable getmenu_logotype()
        {
            SqlConnection con = new SqlConnection(strCon);

            try
            {
                // string query = "select distinct CategoryName ,level from Portal_File_Details where Portal_ID=12 and Top_Category_ID=88 and level=2";
                string query = "select  SubType_Name CategoryName, level, ID   from Base_Category_SubCategory_Master WHERE Previous_Level_ID = 88 AND level = 2";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                return null;
            }


        }       

        public DataTable getLevelMaster(int Portal_ID, int Category_ID, int level_cnt)
        {
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand Cmd = new SqlCommand();
            try
            {
                Cmd.Connection = new SqlConnection(strCon);
                Cmd.Connection.Open();
                Cmd.CommandText = "SP_getLevelDetails";
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;

                Cmd.Parameters.Add("@Portal_ID", SqlDbType.VarChar).Value = Portal_ID;
                Cmd.Parameters.Add("@Category_ID", SqlDbType.VarChar).Value = Category_ID;
                Cmd.Parameters.Add("@Level", SqlDbType.VarChar).Value = level_cnt;
                //string query = "select distinct(CategoryName),count(*) as cnt from Portal_File_Details where Portal_ID='"+Portal_ID+"' and Top_Category_ID='"+Category_ID+"' and IsPublished='1' and ISFile='1' group by CategoryName;";
                //string query = "select distinct(a.CategoryName) as Level_Name,count(*) as cnt,b.Portal_Name,c.Category from Portal_File_Details a,Portal_master b,Base_Category_Master c where a.Portal_ID=b.ID and a.Top_Category_ID=c.ID and a.Portal_ID='" + Portal_ID + "' and a.Top_Category_ID='" + Category_ID + "' and a.IsPublished='1' and a.ISFile='1' group by a.CategoryName,b.Portal_Name,c.Category";
                int Result = Cmd.ExecuteNonQuery();
                Cmd.Connection.Close();
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                return null;
                throw Ex;
            }
            finally
            {
                con.Close();
            }
        }

        public DataTable getLevelMasterDynamic(int Portal_ID, int Category_ID, int level_cnt, string Level)
        {
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand Cmd = new SqlCommand();
            try
            {
                Cmd.Connection = new SqlConnection(strCon);
                Cmd.Connection.Open();
                Cmd.CommandText = "SP_getLevelDetails_dynamic";
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;

                Cmd.Parameters.Add("@Portal_ID", SqlDbType.VarChar).Value = Portal_ID;
                Cmd.Parameters.Add("@Category_ID", SqlDbType.VarChar).Value = Category_ID;
                Cmd.Parameters.Add("@Level", SqlDbType.VarChar).Value = level_cnt;
                Cmd.Parameters.Add("@CategoryName", SqlDbType.VarChar).Value = Level;
                //string query = "select distinct(CategoryName),count(*) as cnt from Portal_File_Details where Portal_ID='"+Portal_ID+"' and Top_Category_ID='"+Category_ID+"' and IsPublished='1' and ISFile='1' group by CategoryName;";
                //string query = "select distinct(a.CategoryName) as Level_Name,count(*) as cnt,b.Portal_Name,c.Category from Portal_File_Details a,Portal_master b,Base_Category_Master c where a.Portal_ID=b.ID and a.Top_Category_ID=c.ID and a.Portal_ID='" + Portal_ID + "' and a.Top_Category_ID='" + Category_ID + "' and a.IsPublished='1' and a.ISFile='1' group by a.CategoryName,b.Portal_Name,c.Category";
                int Result = Cmd.ExecuteNonQuery();
                Cmd.Connection.Close();
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                return null;
                throw Ex;
            }
            finally
            {
                con.Close();
            }
        }


        //public DataTable getSubCategoryBylevel_Category_vs(string categoryID)
        //{
        //    SqlConnection con = new SqlConnection(strCon);
        //    try
        //    {
        //        //string query = " SELECT a.*,b.Portal_Name,c.RoleName,d.level,d.SubType_Name,d.ID SubCategoryID from Base_Category_Master a \n" +
        //        //               " INNER JOIN  Portal_master b on a.Portal_ID  = b.ID  \n" +
        //        //               " INNER JOIN  Portal_Admin c on a.PortalAdmin_ID = c.ID  \n" +
        //        //               " INNER JOIN  Base_Category_SubCategory_Master d on a.ID = d.Category_ID  \n" +
        //        //               " WHERE level = '" + level + "' AND d.Category_ID = '" + categoryID + "'";
        //        string query = " select * from Base_Category_SubCategory_Master where previous_level_id ='" + categoryID + "' ";
        //        SqlDataAdapter da = new SqlDataAdapter(query, con);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        return dt;
        //    }
        //    catch (Exception Ex)
        //    {
        //        return null;
        //        throw Ex;
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        public DataTable getSubCategoryBylevel_Category(string level, string categoryID)
        {
            SqlConnection con = new SqlConnection(strCon);
            try
            {
                string query = " SELECT a.*,b.Portal_Name,c.RoleName,d.level,d.SubType_Name,d.ID SubCategoryID from Base_Category_Master a \n" +
                               " INNER JOIN  Portal_master b on a.Portal_ID  = b.ID  \n" +
                               " INNER JOIN  Portal_Admin c on a.PortalAdmin_ID = c.ID  \n" +
                               " INNER JOIN  Base_Category_SubCategory_Master d on a.ID = d.Category_ID  \n" +
                               " WHERE level = '" + level + "' AND d.Category_ID = '" + categoryID + "'";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                return null;
                throw Ex;
            }
            finally
            {
                con.Close();
            }
        }


        public DataTable getSubCategoryBylevel_Category_vs_AP(string categoryID)
        {
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand Cmd = new SqlCommand();
            try
            {
                Cmd.Connection = new SqlConnection(strCon);
                Cmd.Connection.Open();
                Cmd.CommandText = "Sp_FileDetails_pp";
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.Parameters.Add("@Category_id", SqlDbType.VarChar).Value = categoryID;
                int Result = Cmd.ExecuteNonQuery();
                Cmd.Connection.Close();
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                return null;
                throw Ex;
            }
            finally
            {
                con.Close();
            }
        }


        public DataTable getSubCategoryBylevel_Category_vs(string categoryID)
        {
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand Cmd = new SqlCommand();
            try
            {
                Cmd.Connection = new SqlConnection(strCon);
                Cmd.Connection.Open();
                Cmd.CommandText = "Sp_FileDetails";
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.Parameters.Add("@Category_id", SqlDbType.VarChar).Value = categoryID;
                int Result = Cmd.ExecuteNonQuery();
                Cmd.Connection.Close();
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                return null;
                throw Ex;
            }
            finally
            {
                con.Close();
            }
        }


        public DataTable getSubCategoryBylevel_Category_CreateFolder(string categoryID)
        {
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand Cmd = new SqlCommand();
            try
            {
                Cmd.Connection = new SqlConnection(strCon);
                Cmd.Connection.Open();
                Cmd.CommandText = "Sp_FileDetails_createFolder";
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.Parameters.Add("@Category_id", SqlDbType.VarChar).Value = categoryID;
                int Result = Cmd.ExecuteNonQuery();
                Cmd.Connection.Close();
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                return null;
                throw Ex;
            }
            finally
            {
                con.Close();
            }
        }

        public DataTable getSubCategoryBylevel_Category_vs_breadCrumb(string categoryID)
        {
            SqlConnection con = new SqlConnection(strCon);

            try
            {
                // string query = "select distinct CategoryName ,level from Portal_File_Details where Portal_ID=12 and Top_Category_ID=88 and level=2";
                string query = "select * from Base_Category_SubCategory_Master where ID=" + categoryID;

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                return null;
            }


        }


        public DataTable getSubCategoryBylevel_CategoryTEST(string categoryID, String Sort_Order)
        {
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand Cmd = new SqlCommand();
            try
            {
                Cmd.Connection = new SqlConnection(strCon);
                Cmd.Connection.Open();
                Cmd.CommandText = "SP_GetFileDetailsByOrder";
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.Parameters.Add("@Fileid", SqlDbType.VarChar).Value = categoryID;
                Cmd.Parameters.Add("@Order_Type", SqlDbType.VarChar).Value = Sort_Order;
                int Result = Cmd.ExecuteNonQuery();
                Cmd.Connection.Close();
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                return null;
                throw Ex;
            }
            finally
            {
                con.Close();
            }
        }

        public DataTable getSubCategoryBylevel_CategoryTEST_AP(string categoryID, String Sort_Order)
        {
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand Cmd = new SqlCommand();
            try
            {
                Cmd.Connection = new SqlConnection(strCon);
                Cmd.Connection.Open();
                Cmd.CommandText = "SP_GetFileDetailsByOrder_AP";
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.Parameters.Add("@Fileid", SqlDbType.VarChar).Value = categoryID;
                Cmd.Parameters.Add("@Order_Type", SqlDbType.VarChar).Value = Sort_Order;
                int Result = Cmd.ExecuteNonQuery();
                Cmd.Connection.Close();
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                return null;
                throw Ex;
            }
            finally
            {
                con.Close();
            }
        }


        public DataTable Check_HideFolder(int FolderID)
        {
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand Cmd = new SqlCommand();
            try
            {
                Cmd.Connection = new SqlConnection(strCon);
                Cmd.Connection.Open();
                Cmd.CommandText = "Sp_Check_folderid";
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.Parameters.Add("@Folderid", FolderID);
                int Result = Cmd.ExecuteNonQuery();
                Cmd.Connection.Close();
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                return null;
                throw Ex;
            }
            finally
            {
                con.Close();
            }
        }

        public DataTable getSubCategoryBylevel_Bread(string categoryID)
        {
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand Cmd = new SqlCommand();
            try
            {
                Cmd.Connection = new SqlConnection(strCon);
                Cmd.Connection.Open();
                Cmd.CommandText = "SP_get_Board";
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.Parameters.Add("@Fileid", SqlDbType.VarChar).Value = categoryID;
                // Cmd.Parameters.Add("@Order_Type", SqlDbType.VarChar).Value = Sort_Order;
                int Result = Cmd.ExecuteNonQuery();
                Cmd.Connection.Close();
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                return null;
                throw Ex;
            }
            finally
            {
                con.Close();
            }
        }



        // Edit Functionality

        public DataTable get_fileidEdit(string categoryID)
        {
            SqlConnection con = new SqlConnection(strCon);
            try
            {

                string query = "SELECT DISTINCT Portal_File_Details_BC.FileID, CategoryName, FileType, Portal_File_Details_BC.FileName, FilePath, FileFormat ,privacyType, Description, level, ISFile, Top_Category_ID,Portal_ID, Portal_File_Details_BC.CreatedBy, Portal_File_Details_BC.CreatedDomain, Portal_File_Details_BC.CreatedNTID  ,Portal_File_Details_BC.CreatedDate,Portal_File_Details_BC.IPAddress, IsPublished, Tags, FileID_BrandCenter, SubCategory_MasterID, IsPhysicalFilePresent , FilePath_Thumb, media_orientation , (CASE WHEN Portal_File_Details_BC.FileID IN(Select Fileid FROM Release_Waiver_Mapping_Master) THEN 1 ELSE 0 END) AS IsWaiver ,Portal_File_Details_BC.OrderNo   from Portal_File_Details_BC LEFT JOIN Portal_File_Details_BC_Release_Waivers  ON Portal_File_Details_BC_Release_Waivers.FileID = Portal_File_Details_BC.FileID  where Portal_File_Details_BC.FileID='" + categoryID + "'";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                return null;
                throw Ex;
            }
            finally
            {
                con.Close();
            }
        }

        

        // Tree Load

        public DataTable GetTreeData(string query)
        {

            SqlConnection con = new SqlConnection(strCon);
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand(query))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                }
            }
            return dt;
        }


        public DataTable getAP_CategoryImageDetail(string categoryID)
        {
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand Cmd = new SqlCommand();
            try
            {
                Cmd.Connection = new SqlConnection(strCon);
                Cmd.Connection.Open();
                Cmd.CommandText = "Sp_AP_CategoryImageDetail";
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.Parameters.Add("@Category_id", SqlDbType.VarChar).Value = categoryID;
                int Result = Cmd.ExecuteNonQuery();
                Cmd.Connection.Close();
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                return null;
                throw Ex;
            }
            finally
            {
                con.Close();
            }
        }


        public int Update_AP_CategoryFolderImage(int ID, string FileName)
        {
            int Result;
            SqlCommand Cmd = new SqlCommand();
            try
            {
                Cmd.Connection = new SqlConnection(strCon);
                Cmd.Connection.Open();
                Cmd.CommandText = "SP_Update_AP_CategoryFolderImage";
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;

                Cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                Cmd.Parameters.Add("@FileName", SqlDbType.VarChar).Value = FileName;
                Cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar).Value = Convert.ToString(HttpContext.Current.Session["PP_NTID"]);
                Cmd.Parameters.Add("@UpdatedIpAddress", SqlDbType.VarChar).Value = Utility.GetIPAddress();

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

        public int Update_AP_CategoryFolderName(int ID,string FolderName)
        {
            int Result;
            SqlCommand Cmd = new SqlCommand();
            try
            {
                Cmd.Connection = new SqlConnection(strCon);
                Cmd.Connection.Open();
                Cmd.CommandText = "SP_Update_AP_CategoryFolderName";
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;

                Cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                Cmd.Parameters.Add("@FolderName", SqlDbType.VarChar).Value = FolderName;
                Cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar).Value = Convert.ToString(HttpContext.Current.Session["PP_NTID"]);
                Cmd.Parameters.Add("@UpdatedIpAddress", SqlDbType.VarChar).Value = Utility.GetIPAddress();

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

        public int Update_AP_CategoryFolderOrderNo(int ID, string NewOrderNo, string OldOrderNp, string Previous_level_id)
        {
            int Result;
            SqlCommand Cmd = new SqlCommand();
            try
            {
                Cmd.Connection = new SqlConnection(strCon);
                Cmd.Connection.Open();
                Cmd.CommandText = "Update_AP_CategoryFolderOrderNo";
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;

                Cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                Cmd.Parameters.Add("@previous_level_id", SqlDbType.VarChar).Value = Previous_level_id;
                Cmd.Parameters.Add("@NewOrderNo", SqlDbType.VarChar).Value = NewOrderNo;
                Cmd.Parameters.Add("@OldOrderNo", SqlDbType.VarChar).Value = OldOrderNp;
                Cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar).Value = Convert.ToString(HttpContext.Current.Session["PP_NTID"]);
                Cmd.Parameters.Add("@UpdatedIpAddress", SqlDbType.VarChar).Value = Utility.GetIPAddress();

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

        public DataTable getFolderDetailtById(int ID)
        {
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand Cmd = new SqlCommand();
            try
            {
                Cmd.Connection = new SqlConnection(strCon);
                Cmd.Connection.Open();
                Cmd.CommandText = "getFolderDetailtById";
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                DataTable dt = new DataTable();
                Cmd.Connection.Close();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                return null;
                throw Ex;
            }
            finally
            {
                con.Close();
            }
        }

        public DataTable getSubCategoryBylevel_Category_vs2(string categoryID)
        {
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand Cmd = new SqlCommand();
            try
            {
                Cmd.Connection = new SqlConnection(strCon);
                Cmd.Connection.Open();
                Cmd.CommandText = "Sp_FileDetails2";
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.Parameters.Add("@Category_id", SqlDbType.VarChar).Value = categoryID;
                int Result = Cmd.ExecuteNonQuery();
                Cmd.Connection.Close();
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                return null;
                throw Ex;
            }
            finally
            {
                con.Close();
            }
        }

        public DataTable get_waiversdetails(string categoryID)
        {
            SqlConnection con = new SqlConnection(strCon);
            try
            {

                string query = " select * from Portal_File_Details_BC_Release_Waivers where fileid='" + categoryID + "' and isactive=1";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                return null;
                throw Ex;
            }
            finally
            {
                con.Close();
            }
        }



        public DataTable GetReleaseWaiverDetails_Edit(int FileID)
        {
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand Cmd = new SqlCommand();
            try
            {
                Cmd.Connection = new SqlConnection(strCon);
                Cmd.Connection.Open();
                Cmd.CommandText = "SP_GetReleaseWaiverDetails_Edit";
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.Parameters.Add("@FileID", SqlDbType.Int).Value = FileID;
                int Result = Cmd.ExecuteNonQuery();
                Cmd.Connection.Close();
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                return null;
                throw Ex;
            }
            finally
            {
                con.Close();
            }
        }

        public DataTable GetReleaseWaiverDetails_ForDownload(int ID)
        {
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand Cmd = new SqlCommand();
            try
            {
                Cmd.Connection = new SqlConnection(strCon);
                Cmd.Connection.Open();
                Cmd.CommandText = "SP_GetReleaseWaiverData_forDownload";
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                int Result = Cmd.ExecuteNonQuery();
                Cmd.Connection.Close();
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                return null;
                throw Ex;
            }
            finally
            {
                con.Close();
            }
        }

    }
}
