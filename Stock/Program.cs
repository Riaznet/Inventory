using Common;
using Stock.Login;
using Stock.Stock.Product;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
             Connection.DbConnectionSql= new SqlConnectionStringBuilder { DataSource = "(local)", InitialCatalog = "InventoryDb", IntegratedSecurity = true, MultipleActiveResultSets = true }.ToString();
            Application.Run(new Background());
        }
        private static bool TestConnection()
        {
            try
            {

                string connStr = Properties.Settings.Default.ConnectionSql;
                if (connStr == "")
                    return false;
                else
                {

                    SqlConnection con = new SqlConnection(connStr);
                    con.Open();
                    Connection.DbConnectionSql = connStr;
                    con.Close();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
