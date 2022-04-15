using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CrudInMVC.Models;
using System.Linq;
using System.Threading.Tasks;


namespace CrudInMVC.Models
{
    public class ProductDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public ProductDAL()
        {
            con = new SqlConnection(Startup.ConnectionString);
        }
        public List<Product> Products { get; private set; }
        public List<Product> GetAllProducts()
        {
            List<Product> list = new List<Product>();
            cmd = new SqlCommand("select * from Product", con);
            con.Open();
            dr = cmd.ExecuteReader();
            list = ArrageList(dr);
            con.Close();
            return list;
        }
        public int Save(Product p)
        {
            cmd = new SqlCommand("insert into Product values(@name,@price)", con);
            cmd.Parameters.AddWithValue("@name", p.Name);
            cmd.Parameters.AddWithValue("@price", p.Price);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
        public List<Product> ArrageList(SqlDataReader dr)
        {
            List<Product> list = new List<Product>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Product p = new Product();
                    p.Id = Convert.ToInt32(dr["Id"]);
                    p.Name = dr["Name"].ToString();
                    p.Price = Convert.ToDecimal(dr["Price"]);
                    list.Add(p);
                }
                return list;
            }
            else
            {
                return null;
            }
        }
        public Product GetProductByid(int Id)
        {
            Product prod = new Product();
            cmd = new SqlCommand("select * from Product where id=@id", con);
            cmd.Parameters.AddWithValue("@id",Id);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    prod.Id = Convert.ToInt32(dr["Id"]);
                    prod.Name = dr["Name"].ToString();
                    prod.Price = Convert.ToDecimal(dr["Price"]);
                }

            }
            con.Close();
            return prod;
        }

        public int Upate(Product p)
        {
            cmd = new SqlCommand("update Product set name=@name,price=@price where id=@id", con);
            cmd.Parameters.AddWithValue("@name", p.Name);
            cmd.Parameters.AddWithValue("@price", p.Price);
            cmd.Parameters.AddWithValue("@id", p.Id);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
        public int Delete(int id)
        {
            cmd = new SqlCommand("delete from Product where id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }


    }
}
