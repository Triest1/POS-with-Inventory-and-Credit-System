using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace POSInventoryCreditSystem
{
    public partial class AdminAddStock : UserControl
    {
        SqlConnection
            connect = new SqlConnection(@"Data Source=LAPTOP-DS3FBCLH\SQLEXPRESS01;Initial Catalog=posinventorycredit;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");

        public AdminAddStock()
        {
            InitializeComponent();

            displayStockData();

            // Initialize the AutoCompleteStringCollection
            AutoCompleteStringCollection prodIDSuggestions = new AutoCompleteStringCollection();

            // Create an instance of StockData to access GetAllProductIDs
            StockData stockData = new StockData();

            // Populate the AutoCompleteStringCollection with product IDs
            foreach (var prodID in stockData.GetAllProductIDs())
            {
                prodIDSuggestions.Add(prodID);
            }

            // Set the AutoCompleteMode for the stock_prodID textbox
            stock_prodID.AutoCompleteCustomSource = prodIDSuggestions;
            stock_prodID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            stock_prodID.AutoCompleteSource = AutoCompleteSource.CustomSource;

            // Attach the event handler for the Leave event
            stock_prodID.KeyDown += Stock_prodID_KeyDown;
        }
        private void Stock_prodID_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the pressed key is Enter
            if (e.KeyCode == Keys.Enter)
            {
                // Prevent the form from processing the Enter key further
                e.SuppressKeyPress = true;

                // Get the current text in the textbox
                string currentText = stock_prodID.Text.Trim();

                // Find the suggested value that matches the current input (case-insensitive)
                string suggestedValue = stock_prodID.AutoCompleteCustomSource
                    .OfType<string>()
                    .FirstOrDefault(s => s.Equals(currentText, StringComparison.OrdinalIgnoreCase));

                // If a suggested value is found, set the textbox to that value
                if (suggestedValue != null)
                {
                    stock_prodID.Text = suggestedValue;
                }

                // Call the method to check the quantity
                CheckProductQuantity(stock_prodID.Text.Trim());
            }
        }

        private void CheckProductQuantity(string prodID)
        {
            if (!string.IsNullOrWhiteSpace(prodID))
            {
                using (SqlConnection connect = new SqlConnection(@"Data Source=LAPTOP-DS3FBCLH\SQLEXPRESS01;Initial Catalog=posinventorycredit;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"))
                {
                    try
                    {
                        connect.Open();
                        string query = "SELECT quantity FROM stock WHERE prod_id = @prod_id";
                        using (SqlCommand cmd = new SqlCommand(query, connect))
                        {
                            cmd.Parameters.AddWithValue("@prod_id", prodID);
                            object result = cmd.ExecuteScalar();

                            if (result != null)
                            {
                                current_qty.Text = result.ToString();
                            }
                            else
                            {
                                current_qty.Text = "0"; // Set to 0 if product ID not found
                                MessageBox.Show("Product ID does not exist!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Connection failed: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }
            displayStockData();
        }

        public void displayStockData()
        {
            StockData cData = new StockData();
            List<StockData> listData = cData.AllStockData();

            dataGridView1.DataSource = listData;
        }

        private void addCategories_addBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(stock_prodID.Text) || string.IsNullOrWhiteSpace(addstocks_qty.Text))
            {
                MessageBox.Show("Please fill in all fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(addstocks_qty.Text.Trim(), out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Confirmation dialog
            var result = MessageBox.Show("Are you sure you want to add this stock?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                return; // Exit if the user clicks No
            }

            if (CheckConnection())
            {
                try
                {
                    connect.Open();

                    // Check if the product ID exists in the stock table
                    string checkStock = "SELECT * FROM stock WHERE prod_id = @prod_id";
                    using (SqlCommand cmd = new SqlCommand(checkStock, connect))
                    {
                        cmd.Parameters.AddWithValue("@prod_id", stock_prodID.Text.Trim());
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            // If the product exists, update the quantity
                            reader.Close(); // Close the reader before executing another command

                            string updateStock = "UPDATE stock SET quantity = quantity + @qty, stock_date = @date WHERE prod_id = @prod_id";
                            using (SqlCommand updateCmd = new SqlCommand(updateStock, connect))
                            {
                                updateCmd.Parameters.AddWithValue("@qty", quantity);
                                updateCmd.Parameters.AddWithValue("@date", DateTime.Now);
                                updateCmd.Parameters.AddWithValue("@prod_id", stock_prodID.Text.Trim());
                                updateCmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            // Insert a new stock record if the product does not exist
                            reader.Close(); // Close the reader before executing another command

                            string insertStock = "INSERT INTO stock (prod_id, quantity, stock_date) VALUES (@prod_id, @qty, @date)";
                            using (SqlCommand insertCmd = new SqlCommand(insertStock, connect))
                            {
                                insertCmd.Parameters.AddWithValue("@prod_id", stock_prodID.Text.Trim());
                                insertCmd.Parameters.AddWithValue("@qty", quantity);
                                insertCmd.Parameters.AddWithValue("@date", DateTime.Now);
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                    }

                    clearFields();
                    displayStockData();
                    MessageBox.Show("Stock updated successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection failed: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        public bool CheckConnection()
        {
            if (connect.State == ConnectionState.Closed)
                return true;
            else
                return false;
        }

        public void clearFields()
        {
            stock_prodID.Text = "";
            addstocks_qty.Text = "0";
            current_qty.Text = "0";
        }
        private void addCategories_clearBtn_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private int getID = 0;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                getID = (int)row.Cells[0].Value;

                stock_prodID.Text = row.Cells[1].Value.ToString();
                current_qty.Text = row.Cells[2].Value.ToString();

            }
        }

        private void addCategories_removeBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(stock_prodID.Text) || string.IsNullOrWhiteSpace(addstocks_qty.Text))
            {
                MessageBox.Show("Please fill in all fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(addstocks_qty.Text.Trim(), out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Confirmation dialog
            var result = MessageBox.Show("Are you sure you want to remove this stock?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                return; // Exit if the user clicks No
            }

            if (CheckConnection())
            {
                try
                {
                    connect.Open();

                    // Check if the product ID exists in the stock table
                    string checkStock = "SELECT quantity FROM stock WHERE prod_id = @prod_id";
                    using (SqlCommand cmd = new SqlCommand(checkStock, connect))
                    {
                        cmd.Parameters.AddWithValue("@prod_id", stock_prodID.Text.Trim());
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            reader.Read(); // Move to the first record
                            int currentStock = (int)reader["quantity"];

                            if (currentStock >= quantity)
                            {
                                // Deduct the quantity from stock
                                reader.Close(); // Close the reader before executing another command

                                string updateStock = "UPDATE stock SET quantity = quantity - @qty, stock_date = @date WHERE prod_id = @prod_id";
                                using (SqlCommand updateCmd = new SqlCommand(updateStock, connect))
                                {
                                    updateCmd.Parameters.AddWithValue("@qty", quantity);
                                    updateCmd.Parameters.AddWithValue("@date", DateTime.Now);
                                    updateCmd.Parameters.AddWithValue("@prod_id", stock_prodID.Text.Trim());
                                    updateCmd.ExecuteNonQuery();
                                }

                                MessageBox.Show("Stock deducted successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Not enough stock to deduct!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Product ID does not exist in stock!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    clearFields();
                    displayStockData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection failed: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }
    }
}
