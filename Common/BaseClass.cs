using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks; 

namespace Common
{
    public static class BaseClass
    {
         
        public static string encryptHash(string clearText)
        {
            try
            {
                byte[] hashBytes = computeHash(clearText + "asl");
                byte[] hashWithSaltBytes = new byte[hashBytes.Length];
                for (int i = 0; i < hashBytes.Length; i++)
                    hashWithSaltBytes[i] = hashBytes[i];
                string hashValue = Convert.ToBase64String(hashWithSaltBytes);

                return hashValue;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static byte[] computeHash(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            HashAlgorithm hash = new SHA256Managed();
            return hash.ComputeHash(plainTextBytes);
        }

        public static string GetDataParemeter(String sql, string value)
        {
            string s = "";
            try
            {
                SqlConnection conn = new SqlConnection(Connection.DbConnectionSql);
                conn.Open();
                SqlCommand cmd1 = new SqlCommand(sql, conn);
                cmd1.Parameters.Clear();
                cmd1.Parameters.Add("@TXT", SqlDbType.NVarChar).Value = value;
                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    s = dr[0].ToString();
                }
                dr.Close();
                conn.Close();
            }
            catch { }
            return s;
        }
        public static string GetData(string str)
        {
            string Result = "";
            try
            {

                SqlConnection Conn = new SqlConnection(Connection.DbConnectionSql);
                Conn.Open();
                SqlCommand cmd = new SqlCommand(str, Conn);
                SqlDataReader DR = cmd.ExecuteReader();
                while (DR.Read())
                    Result = DR[0].ToString();
                Conn.Close();
                // DR.Close();
            }
            catch (Exception ex) { }
            return Result;
        }
         

        public static string GetData(string str, string Con)
        {
            string Result = "";
            try
            {

                SqlConnection Conn = new SqlConnection(Con);
                Conn.Open();
                SqlCommand cmd = new SqlCommand(str, Conn);
                SqlDataReader DR = cmd.ExecuteReader();
                while (DR.Read())
                    Result = DR[0].ToString();
                Conn.Close();
                // DR.Close();
            }
            catch { }
            return Result;
        }
        public static DataSet DataSetData(string Script)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(Connection.DbConnectionSql))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(Script, con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(ds);
                        con.Close();
                        return ds;
                    }
                }
            }
        }
    }
}
