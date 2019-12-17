using jqueryBO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jqueryDAL
{
    public class TestDAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());

        public DataTable Read()
        {
            DataTable dt = new DataTable();

            //if (ConnectionState.Closed == con.State)
            con.Open();

            string quer = "select DesignationID,Designation from Designation";
            SqlCommand cmd = new SqlCommand(quer, con);
            try
            {
                SqlDataReader rd = cmd.ExecuteReader();
                dt.Load(rd);
                return dt;
            }
            catch
            {
                throw;
            }
        }


        public DataSet GetDsEmp()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adptr = new SqlDataAdapter();
                string query = "Select * from Emptable";
                adptr = new SqlDataAdapter(query, con);
                adptr.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetEmpById(TestBO obj)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter adptr = new SqlDataAdapter();
                string query = "Select Empname,EmpSalary,Designation,Status from Emptable Where EmpID="+obj.EmpID;
                adptr = new SqlDataAdapter(query, con);
                adptr.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveEmp(TestBO obj)
        {
            try
            {
                int ReturnValue = 0;
                SqlCommand cmd = new SqlCommand("USP_insertupdate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpID", obj.EmpID);
                cmd.Parameters.AddWithValue("@Name", obj.Empname);
                cmd.Parameters.AddWithValue("@SeignationId", obj.Designation);
                cmd.Parameters.AddWithValue("@Salary", obj.EmpSalary);
                cmd.Parameters.AddWithValue("@Status", obj.Status);
                cmd.Parameters.AddWithValue("@Return", 1);
                cmd.Parameters["@Return"].Direction = ParameterDirection.Output;
                con.Open();
                cmd.ExecuteNonQuery();
                //cmd.Dispose();
                con.Close();
                ReturnValue= Convert.ToInt32(cmd.Parameters["@Return"].Value);
                return ReturnValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
