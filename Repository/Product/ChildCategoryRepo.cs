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
    public class ChildCategoryRepo
    {
        SqlConnection con;
        SqlCommand cmd;
        public ChildCategoryRepo()
        {
            con = new SqlConnection(Connection.DbConnectionSql);
            cmd = new SqlCommand("",con);
        }
        public bool InsertChildCategory(ChildCategory model)
        {
            bool result = false;
            SqlTransaction transaction = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                transaction = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"Insert Into ChildCategory (ParentId,Name,CreatedBy,CreatedAt) Values (@ParentId,@Name,@CreatedBy,@CreatedAt)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ParentId", model.ParentId);
                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@CreatedBy", model.CreatedBy);
                cmd.Parameters.AddWithValue("@CreatedAt", model.CreatedAt);

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
        public bool UpdateChildCategory(ChildCategory model)
        {
            bool result = false;
            SqlTransaction transaction = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                transaction = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"Update ChildCategory set  Name=@Name,UpdatedBy=@UpdatedBy,UpdatedAt=@UpdatedAt Where Id=@Id";
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
        public bool DeleteChildCategory(ChildCategory model)
        {
            bool result = false;
            SqlTransaction transaction = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                transaction = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"Delete From ChildCategory Where Id=@Id";
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
    }
}
