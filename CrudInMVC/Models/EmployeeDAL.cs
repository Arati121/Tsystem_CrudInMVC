using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CrudInMVC.Models;

namespace CrudInMVC.Models
{
    public class EmployeeDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public EmployeeDAL()
        {
            con = new SqlConnection(Startup.ConnectionString);
        }
        public List<Employee> Employees { get; private set; }
        public List<Employee> GetAllEmployees()
        {
            List<Employee> list = new List<Employee>();
            cmd = new SqlCommand("select * from Employee", con);
            con.Open();
            dr = cmd.ExecuteReader();
            list = ArrageList(dr);
            con.Close();
            return list;
        }
        public int Save(Employee e)
        {
            cmd = new SqlCommand("insert into Employee values(@name,@salary)", con);
            cmd.Parameters.AddWithValue("@name", e.EName);
            cmd.Parameters.AddWithValue("@salary", e.ESalary);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
        public List<Employee> ArrageList(SqlDataReader dr)
        {
            List<Employee> list = new List<Employee>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Employee e = new Employee();
                    e.EId = Convert.ToInt32(dr["EId"]);
                    e.EName = dr["EName"].ToString();
                    e.ESalary = Convert.ToInt32(dr["ESalary"]);
                    list.Add(e);
                }
                return list;
            }
            else
            {
                return null;
            }
        }
        public Employee GetEmployeeByid(int EId)
        {
            Employee e = new Employee();
            cmd = new SqlCommand("select * from Employee where EId=@EId", con);
            cmd.Parameters.AddWithValue("@EId", EId);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    e.EId = Convert.ToInt32(dr["EId"]);
                    e.EName = dr["EName"].ToString();
                    e.ESalary = Convert.ToInt32(dr["ESalary"]);
                }

            }
            con.Close();
            return e;
        }

        public int Upate(Employee e)
        {
            cmd = new SqlCommand("update Employee set name=@name,salary=@salary where EId=@EId", con);
            cmd.Parameters.AddWithValue("@name", e.EName);
            cmd.Parameters.AddWithValue("@salary", e.ESalary);
            cmd.Parameters.AddWithValue("@EId", e.EId);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
        public int Delete(int EId)
        {
            cmd = new SqlCommand("delete from Employee where EId=@EId", con);
            cmd.Parameters.AddWithValue("@EId", EId);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }

    }
}
