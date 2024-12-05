using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace POSInventoryCreditSystem
{
    internal class StockData
    {
        public int ID {  get; set; }
        public string Description {  get; set; }
        public int Quantity { get; set; }
        public string Date { get; set; }
        public List<StockData> AllStockData(string prod_id = null)
        {
            List<StockData> listData = new List<StockData>();

            using (SqlConnection connect = new SqlConnection(@"Data Source=LAPTOP-DS3FBCLH\SQLEXPRESS01;Initial Catalog=posinventorycredit;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"))
            {
                connect.Open();

                string selectData = "SELECT * FROM stock";
                if (!string.IsNullOrEmpty(prod_id))
                {
                    selectData += " WHERE prod_id = @prod_id";
                }

                using (SqlCommand cmd = new SqlCommand(selectData, connect))
                {
                    if (!string.IsNullOrEmpty(prod_id))
                    {
                        cmd.Parameters.AddWithValue("@prod_id", prod_id);
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        StockData cData = new StockData();
                        cData.ID = (int)reader["stock_id"];
                        cData.Description = reader["prod_id"].ToString();
                        cData.Quantity = (int)reader["quantity"];
                        cData.Date = reader["stock_date"].ToString();
                        listData.Add(cData);
                    }
                }
            }

            return listData;
        }
      // New method to get all product IDs
        public List<string> GetAllProductIDs()
        {
            List<string> productIDs = new List<string>();

            using (SqlConnection connect = new SqlConnection(@"Data Source=LAPTOP-DS3FBCLH\SQLEXPRESS01;Initial Catalog=posinventorycredit;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"))
            {
                connect.Open();

                string query = "SELECT DISTINCT prod_id FROM stock";
                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        productIDs.Add(reader["prod_id"].ToString());
                    }
                }
            }

            return productIDs;
        }
    }
}
