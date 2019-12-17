using jqueryBO;
using jqueryDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jqueryBLL
{
    public class TestBLL
    {
        public DataTable GetUsers()
        {
            try
            {
                TestDAL objTestDAL = new TestDAL();
                return objTestDAL.Read();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataSet GetEmp()
        {
            try
            {
                TestDAL objTestDAL = new TestDAL();
                return objTestDAL.GetDsEmp();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int SaveEmp(TestBO obj) // passing Bussiness object Here  
        {
            try
            {
                TestDAL objEmpDAL = new TestDAL(); // Creating object of Dataccess  
                return objEmpDAL.SaveEmp(obj); // calling Method of DataAccess  
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetEmpById(TestBO obj) // passing Bussiness object Here  
        {
            try
            {
                TestDAL objEmpDAL = new TestDAL(); // Creating object of Dataccess  
                return objEmpDAL.GetEmpById(obj); // calling Method of DataAccess  
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
