﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace POSInventoryCreditSystem
{
    public partial class AdminAddUsers : Form
    {
        SqlConnection
            connect = new SqlConnection(@"Data Source=LAPTOP-DS3FBCLH\SQLEXPRESS01;Initial Catalog=posinventorycredit;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");

        public AdminAddUsers()
        {
            InitializeComponent();
        }

        private void addUsers_addBtn_Click(object sender, EventArgs e)
        {
            if (addUsers_username.Text == "" || addUsers_password.Text == ""
                || addUsers_role.SelectedIndex == -1 || addUsers_status.SelectedIndex == -1)
            { 
                MessageBox.Show("Empty fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if(checkConnection())
                {
                    try 
                    {
                        connect.Open();

                        string checkUsername = "SELECT * FROM users WHERE username = @usern";

                        using(SqlCommand cmd = new SqlCommand(checkUsername, connect))
                        {
                            cmd.Parameters.AddWithValue("@usern", addUsers_username.Text.Trim());

                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                            if(table.Rows.Count > 0)
                            {
                                MessageBox.Show(addUsers_username.Text.Trim() 
                                    + " is already taken", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                string insertData = "INSERT INTO users (username, password, role, status, date)" +
                                    "VALUES(@usern, @pass, @role, @status, @date)";
                                using(SqlCommand insertD = new SqlCommand(insertData, connect)) {
                                    insertD.Parameters.AddWithValue("@usern", addUsers_username.Text.Trim());
                                    insertD.Parameters.AddWithValue("@pass", addUsers_password.Text.Trim());
                                    insertD.Parameters.AddWithValue("@role", addUsers_role.SelectedItem.ToString());
                                    insertD.Parameters.AddWithValue("@status", addUsers_status.SelectedItem.ToString());

                                    DateTime today = DateTime.Today;
                                    insertD.Parameters.AddWithValue("@date", today);

                                    insertD.ExecuteNonQuery();

                                    MessageBox.Show("Added successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }catch (Exception ex) 
                    {
                        MessageBox.Show("Connection failed: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
            }
        }

        public bool checkConnection()
        { 
           if(connect.State == ConnectionState.Closed)
           {
                return true;
           }
           else
           {
                return false; 
           }
        }
    }
}
