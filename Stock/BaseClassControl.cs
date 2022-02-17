using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;

namespace Stock
{
    public static class BaseClassControl
    {
        public static void Complition_txt(TextBox txt, string str)
        {
            SqlConnection Conn = new SqlConnection(Connection.DbConnectionSql);
            txt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txt.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
            Conn.Open();
            SqlCommand cmnd = new SqlCommand(str, Conn);
            SqlDataReader dReader;
            dReader = cmnd.ExecuteReader();
            while (dReader.Read())
                namesCollection.Add(dReader["txt"].ToString());
            //dReader.Close();
            Conn.Close();
            txt.AutoCompleteCustomSource = namesCollection;
        }
        public static void GridViewAdd(DataGridView ob, String sql)
        {
            DataTable table = new DataTable();
            try
            {
                SqlConnection con = new SqlConnection(Connection.DbConnectionSql);
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ad.Fill(table);
                ob.DataSource = table;
                //ob.DataBind();
                con.Close();
            }
            catch { }
            //return List;
        }
    }
}
