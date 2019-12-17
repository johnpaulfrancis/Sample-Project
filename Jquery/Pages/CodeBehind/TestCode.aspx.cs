using jqueryBLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using jqueryBO;

namespace Jquery.Pages.CodeBehind
{
    public partial class TestCode1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string GetData()
        {
            return "This string is from Code behind";
        }

        [WebMethod]
        public static string SaveData(int EmpId, string Name, int Designation, string Salary,bool Status)
        {
            string returnValue = "-1";
            try
            {
                TestBO Obj = new TestBO();
                Obj.EmpID = EmpId;
                Obj.Empname = Name;
                Obj.Designation = Designation;
                Obj.EmpSalary = Salary;
                Obj.Status = Status;
                TestBLL DBHelper = new TestBLL();
                int result = DBHelper.SaveEmp(Obj);
                returnValue= JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {

            }
            return returnValue;
        }

        [WebMethod]
        public static string GetActiveCategoryNames()
        {
            string returnValue = "-1";
            //DataTable dt = new DataTable();
            try
            {
                TestBLL objTestBLL = new TestBLL();
                DataTable dt = objTestBLL.GetUsers();

                string JSONdata = null;
                if (dt.Rows.Count > 0)
                {
                    JSONdata = JsonConvert.SerializeObject(dt);
                    returnValue = JSONdata;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnValue;
        }

        [WebMethod]
        public static string GetEmp()
        {
            string returnValue = "-1";
            try
            {
                TestBLL objTestBLL = new TestBLL();
                DataSet ds = objTestBLL.GetEmp();

                string JSONdata = null;
                if (ds.Tables.Count > 0)
                {
                    JSONdata = JsonConvert.SerializeObject(ds);
                    returnValue = JSONdata;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnValue;
        }

        [WebMethod]
        public static string GetEmpById(int EmpId)
        {
            string returnValue = "-1";
            try
            {
                TestBO obj = new TestBO();
                obj.EmpID = EmpId;
                TestBLL objTestBLL = new TestBLL();
                DataTable dt = objTestBLL.GetEmpById(obj);

                string JSONdata = null;
                if (dt.Rows.Count > 0)
                {
                    JSONdata = JsonConvert.SerializeObject(dt);
                    returnValue = JSONdata;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnValue;
        }

    }
}