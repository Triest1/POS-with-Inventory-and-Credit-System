﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace POSInventoryCreditSystem
{
    internal class AddProductsData
    {
        public int ID { set; get; } //0
        public string Description { set; get; } //1
        public string ProdName { set; get; } //2
        public string Category { set; get; } //3
        public string Price { set; get; } //4
        public int Stock { set; get; } //5
        public string ImagePath { set; get; } //6
        public string Status { set; get; } //7
        public string Date { set; get; } //8

        public List<AddProductsData> AllProductsData()
        {
            List<AddProductsData> listData = new List<AddProductsData>();

            using (SqlConnection connect
                = new SqlConnection(@"Data Source=LAPTOP-DS3FBCLH\SQLEXPRESS01;Initial Catalog=posinventorycredit;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"))
            {
                connect.Open();

                string selectData = "SELECT p.id, p.prod_id, p.prod_name, p.category, p.price, s.quantity AS stock, p.image_path, p.status, p.date_insert " +
                                    "FROM products p " +
                                    "INNER JOIN stock s ON p.prod_id = s.prod_id";

                using (SqlCommand cmd = new SqlCommand(selectData, connect))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        AddProductsData apData = new AddProductsData();
                        apData.ID = (int)reader["id"];
                        apData.Description = reader["prod_id"].ToString();
                        apData.ProdName = reader["prod_name"].ToString();
                        apData.Category = reader["category"].ToString();
                        apData.Price = reader["price"].ToString();
                        apData.Stock = (int)reader["stock"];
                        apData.ImagePath = reader["image_path"].ToString();
                        apData.Status = reader["status"].ToString();
                        apData.Date = reader["date_insert"].ToString();

                        listData.Add(apData);
                    }
                }
            }

            return listData;
        }


        public List<AddProductsData> allAvailableProducts()
        {
            List<AddProductsData> listData = new List<AddProductsData>();

            using (SqlConnection connect
               = new SqlConnection(@"Data Source=LAPTOP-DS3FBCLH\SQLEXPRESS01;Initial Catalog=posinventorycredit;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"))
            {
                connect.Open();

                string selectData = "SELECT p.id, p.prod_id, p.prod_name, p.category, p.price, s.quantity AS stock, p.image_path, p.status, p.date_insert " +
                                    "FROM products p " +
                                    "INNER JOIN stock s ON p.prod_id = s.prod_id " +
                                    "WHERE p.status = @status";

                using (SqlCommand cmd = new SqlCommand(selectData, connect))
                {
                    cmd.Parameters.AddWithValue("@status", "Available");
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        AddProductsData apData = new AddProductsData();
                        apData.ID = (int)reader["id"];
                        apData.Description = reader["prod_id"].ToString();
                        apData.ProdName = reader["prod_name"].ToString();
                        apData.Category = reader["category"].ToString();
                        apData.Price = reader["price"].ToString();
                        apData.Stock = (int)reader["stock"];
                        apData.ImagePath = reader["image_path"].ToString();
                        apData.Status = reader["status"].ToString();
                        apData.Date = reader["date_insert"].ToString();

                        listData.Add(apData);
                    }
                }
            }

            return listData;
        }
    }
}