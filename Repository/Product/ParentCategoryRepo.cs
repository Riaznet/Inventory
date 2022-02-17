using Common;
using Domain.Product;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Product
{
    public class ParentCategoryRepo
    {
        SqlConnection con;
        SqlCommand cmd;
        public ParentCategoryRepo()
        {
            con = new SqlConnection(Connection.DbConnectionSql);
            cmd = new SqlCommand("",con);
        }
        public Int64 InsertParentCategory(ParentCategory model )
        {
            Int64 result = 0;
            SqlTransaction transaction = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                transaction = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"Insert Into ParentCategory (Name,CreatedBy,CreatedAt)  VALUES (@Name,@CreatedBy,@CreatedAt);SELECT SCOPE_IDENTITY() ";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@CreatedBy", model.CreatedBy);
                cmd.Parameters.AddWithValue("@CreatedAt", model.CreatedAt);

                cmd.Transaction = transaction; 
                Int64 newId = Convert.ToInt64(cmd.ExecuteScalar()); 
                    transaction.Commit();
                result = newId;
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            return result;
        }
        public bool UpdateParentCategory(ParentCategory model)
        {
            bool result = false;
            SqlTransaction transaction = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                transaction = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"Update ParentCategory set Name=@Name,UpdatedBy=@UpdatedBy,UpdatedAt=@UpdatedAt Where Id=@Id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@UpdatedBy", model.UpdatedBy);
                cmd.Parameters.AddWithValue("@UpdatedAt", model.UpdatedAt);
                cmd.Parameters.AddWithValue("@Id", model.Id);

                cmd.Transaction = transaction;
                cmd.ExecuteNonQuery();
                transaction.Commit();
                result = true;
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            return result;
        }
        public bool DeleteParentCategory(ParentCategory model)
        {
            bool result = false;
            SqlTransaction transaction = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                transaction = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"Delete From ParentCategory Where Id=@Id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Id", model.Id);

                cmd.Transaction = transaction;
                cmd.ExecuteNonQuery();
                transaction.Commit();
                result = true;
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            return result;
        }
        public DataTable SelectParentCategory(ParentCategory model)
        {
            var dataTable = new DataTable();
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new SqlCommand(@"Select Name,Id From ParentCategory Where Name=@Name", con);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Name", model.Name);

                var dataAdepter = new SqlDataAdapter(cmd);
                dataAdepter.Fill(dataTable);
                return dataTable;
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ex)
            {
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            return null;
        }
    }
}
