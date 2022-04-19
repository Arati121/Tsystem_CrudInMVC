using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CrudInMVC.Models;

namespace CrudInMVC.Models
{
    public class Student1DAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Student1DAL()
        {
            con = new SqlConnection(Startup.ConnectionString);
        }
        public List<Student1> Student1s { get; private set; }
        public List<Student1> GetAllStudent1s()
        {
            List<Student1> list = new List<Student1>();
            cmd = new SqlCommand("select * from Student1", con);
            con.Open();
            dr = cmd.ExecuteReader();
            list = ArrageList(dr);
            con.Close();
            return list;
        }
        public int Save(Student1 s)
        {
            cmd = new SqlCommand("insert into Student1 values(@name,@percentage)", con);
            cmd.Parameters.AddWithValue("@name", s.Name);
            cmd.Parameters.AddWithValue("@percentage", s.Percentage);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
        public List<Student1> ArrageList(SqlDataReader dr)
        {
            List<Student1> list = new List<Student1>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Student1 s = new Student1();
                    s.Id = Convert.ToInt32(dr["Id"]);
                    s.Name = dr["Name"].ToString();
                    s.Percentage = Convert.ToDecimal(dr["Percentage"]);
                    list.Add(s);
                }
                return list;
            }
            else
            {
                return null;
            }
        }
        public Student1 GetStudent1Byid(int Id)
        {
            Student1 stud = new Student1();
            cmd = new SqlCommand("select * from Student1 where id=@id", con);
            cmd.Parameters.AddWithValue("@id", Id);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                   stud.Id = Convert.ToInt32(dr["Id"]);
                   stud.Name = dr["Name"].ToString();
                   stud.Percentage = Convert.ToDecimal(dr["Percentage"]);
                }

            }
            con.Close();
            return stud;
        }

        public int Upate(Student1 s)
        {
            cmd = new SqlCommand("update Student set name=@name,percentage=@percentage where id=@id", con);
            cmd.Parameters.AddWithValue("@name", s.Name);
            cmd.Parameters.AddWithValue("@percentage", s.Percentage);
            cmd.Parameters.AddWithValue("@id", s.Id);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
        public int Delete(int id)
        {
            cmd = new SqlCommand("delete from student1 where id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
    }
}
