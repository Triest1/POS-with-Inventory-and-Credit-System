using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace POSInventoryCreditSystem
{
    public partial class AdminAddProducts : UserControl
    {
        SqlConnection
            connect = new SqlConnection(@"Data Source=LAPTOP-DS3FBCLH\SQLEXPRESS01;Initial Catalog=posinventorycredit;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");

        public AdminAddProducts()
        {
            InitializeComponent();

          
            displayAllProducts();
            InitializeProdIDAutoComplete();
        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }
           
            displayAllProducts();
        }

        public void displayAllProducts()
        {
            AddProductsData apData = new AddProductsData();
            List<AddProductsData> listData = apData.AllProductsData();

            DataGridView1.DataSource = listData;
        }

        public bool emptyFields()
        {
            if (addProducts_prodID.Text == "" || addProducts_prodName.Text == "" || addProducts_category.Text == ""
                || addProducts_price.Text == "" || addProducts_stock.Text == "" || addProducts_status.SelectedIndex == -1
                || addProducts_imageView.Image == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void InitializeProdIDAutoComplete()
        {
            // Create an instance of AddProductsData to access AllProductsData
            AddProductsData apData = new AddProductsData();

            // Initialize the AutoCompleteStringCollection
            AutoCompleteStringCollection prodIDSuggestions = new AutoCompleteStringCollection();

            // Populate the AutoCompleteStringCollection with product IDs
            foreach (var product in apData.AllProductsData())
            {
                prodIDSuggestions.Add(product.Description);
            }

            // Set the AutoComplete properties for the addProducts_prodID textbox
            addProducts_prodID.AutoCompleteCustomSource = prodIDSuggestions;
            addProducts_prodID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            addProducts_prodID.AutoCompleteSource = AutoCompleteSource.CustomSource;

            // Attach the event handler for the KeyDown event
            addProducts_prodID.KeyDown += addProducts_prodID_KeyDown;
        }

        private void addProducts_prodID_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the pressed key is Enter
            if (e.KeyCode == Keys.Enter)
            {
                // Prevent the form from processing the Enter key further
                e.SuppressKeyPress = true;

                // Get the current text in the textbox
                string currentText = addProducts_prodID.Text.Trim();

                // Find the suggested value that matches the current input (case-insensitive)
                string suggestedValue = addProducts_prodID.AutoCompleteCustomSource
                    .OfType<string>()
                    .FirstOrDefault(s => s.Equals(currentText, StringComparison.OrdinalIgnoreCase));

                // If a suggested value is found, set the textbox to that value
                if (suggestedValue != null)
                {
                    addProducts_prodID.Text = suggestedValue;
                }
                LoadProductDetails(addProducts_prodID.Text.Trim());
            }
        }

        private void LoadProductDetails(string prodID)
        {
            // Implement the logic to load product details based on the product ID
            // You can query the database or use the existing AllProductsData method
            // For example:
            var productData = new AddProductsData().AllProductsData().FirstOrDefault(p => p.Description == prodID);

            if (productData != null)
            {
                // Populate the fields with product details
                addProducts_prodName.Text = productData.Description;
                addProducts_category.Text = productData.Category;
                addProducts_price.Text = productData.Price;
                addProducts_stock.Text = productData.Stock.ToString();
                addProducts_status.SelectedItem = productData.Status;
                // Load the image if necessary
                if (!string.IsNullOrEmpty(productData.ImagePath))
                {
                    addProducts_imageView.Image = Image.FromFile(productData.ImagePath);
                }
            }
            else
            {
                MessageBox.Show("Product ID not found!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void addProducts_addBtn_Click(object sender, EventArgs e)
        {
            if (emptyFields())
            {
                MessageBox.Show("Empty Fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (checkConnection())
                {
                    try
                    {
                        connect.Open();

                        string selectData = "SELECT * FROM products WHERE prod_id = @prodID";
                        string category = addProducts_category.Text.Trim(); // Get category from TextBox

                        using (SqlCommand cmd = new SqlCommand(selectData, connect))
                        {
                            cmd.Parameters.AddWithValue("@prodID", addProducts_prodID.Text.Trim());

                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            DataTable table = new DataTable();

                            adapter.Fill(table);

                            if (table.Rows.Count > 0)
                            {
                                MessageBox.Show("Product ID: " + addProducts_prodID.Text.Trim() + " already exists.", "Error Message",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

                                string relativePath = Path.Combine("Product_Directory", addProducts_prodID.Text.Trim() + ".jpg");
                                string path = Path.Combine(baseDirectory, relativePath);

                                string directoryPath = Path.GetDirectoryName(path);

                                if (!Directory.Exists(directoryPath))
                                {
                                    Directory.CreateDirectory(directoryPath);
                                }

                                File.Copy(addProducts_imageView.ImageLocation, path, true);

                                string insertData = "INSERT INTO products " + "(prod_id, prod_name, category, price, stock, image_path, status, date_insert) "
                                    + "VALUES(@prodID, @prodName, @cat, @price, @stock, @path, @status, @date)";

                                using (SqlCommand insertD = new SqlCommand(insertData, connect))
                                {
                                    insertD.Parameters.AddWithValue("@prodID", addProducts_prodID.Text.Trim());
                                    insertD.Parameters.AddWithValue("@prodName", addProducts_prodName.Text.Trim());
                                    insertD.Parameters.AddWithValue("@cat", addProducts_category.Text.Trim());
                                    insertD.Parameters.AddWithValue("@price", addProducts_price.Text.Trim());
                                    insertD.Parameters.AddWithValue("@stock", addProducts_stock.Text.Trim());
                                    insertD.Parameters.AddWithValue("path", path);
                                    insertD.Parameters.AddWithValue("@status", addProducts_status.SelectedItem);

                                    DateTime today = DateTime.Today;
                                    insertD.Parameters.AddWithValue("@date", today);

                                    insertD.ExecuteNonQuery();
                                    // Insert into stock
                                    string insertStockData = "INSERT INTO stock (prod_id, quantity, stock_date) VALUES (@prodID, @quantity, @stockDate)";
                                    using (SqlCommand insertStockD = new SqlCommand(insertStockData, connect))
                                    {
                                        insertStockD.Parameters.AddWithValue("@prodID", addProducts_prodID.Text.Trim());
                                        insertStockD.Parameters.AddWithValue("@quantity", addProducts_stock.Text.Trim());
                                        insertStockD.Parameters.AddWithValue("@stockDate", DateTime.Today);

                                        insertStockD.ExecuteNonQuery();
                                    }

                                    clearFields();
                                    displayAllProducts();
                                    MessageBox.Show("Product successfully added!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Connection has failed: " + ex.Message, "Error Message",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (connect.State != ConnectionState.Open)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void addProducts_importBtn_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image Files (*.jpg; *.png)|*.jpg;*.png";
                string imagePath = "";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = dialog.FileName;
                    addProducts_imageView.ImageLocation = imagePath;
                    addProducts_imageView.Image = Image.FromFile(imagePath); // Set the image directly

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void clearFields()
        {
            addProducts_prodID.Text = "";
            addProducts_prodName.Text = "";
            addProducts_category.Text = "";
            addProducts_price.Text = "";
            addProducts_stock.Text = "";
            addProducts_status.SelectedIndex = -1;
            addProducts_imageView.Image = null;

        }
        private void addProducts_clearBtn_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private int getID = 0;
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = DataGridView1.Rows[e.RowIndex];

                getID = (int)row.Cells[0].Value;

                addProducts_prodID.Text = row.Cells[1].Value.ToString();
                addProducts_prodName.Text = row.Cells[2].Value.ToString();
                addProducts_category.Text = row.Cells[3].Value.ToString();
                addProducts_price.Text = row.Cells[4].Value.ToString();
                addProducts_stock.Text = row.Cells[5].Value.ToString();

                string imagepath = row.Cells[6].Value.ToString();

                try
                {
                    if (imagepath != null)
                    {
                        addProducts_imageView.Image = Image.FromFile(imagepath);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Image: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                addProducts_status.Text = row.Cells[7].Value.ToString();
            }
        }

        private void addProducts_updateBtn_Click(object sender, EventArgs e)
        {
            if (emptyFields())
            {
                MessageBox.Show("Empty Fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if(MessageBox.Show("Are you sure you want to update Product ID: " 
                    + addProducts_prodID.Text.Trim() + "?", "Confirmation Messaage"
                    ,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (checkConnection())
                    {
                        try
                        {
                            connect.Open();

                            string imagePath = addProducts_imageView.ImageLocation;

                            string updateData = "UPDATE products SET prod_id = @prodID, prod_name = @prodName" +
                                ", category = @cat, price = @price, image_path = @path, status = @status WHERE id = @id";

                            using (SqlCommand updateD = new SqlCommand(updateData, connect))
                            {
                                updateD.Parameters.AddWithValue("@prodID", addProducts_prodID.Text.Trim());
                                updateD.Parameters.AddWithValue("@prodName", addProducts_prodName.Text.Trim());
                                updateD.Parameters.AddWithValue("@cat", addProducts_category.Text.Trim());
                                updateD.Parameters.AddWithValue("@price", addProducts_price.Text.Trim());
                                updateD.Parameters.AddWithValue("@path", imagePath); // Update the image path
                                updateD.Parameters.AddWithValue("@status", addProducts_status.SelectedItem);
                                updateD.Parameters.AddWithValue("@id", getID);

                                updateD.ExecuteNonQuery();
                            }
                                // Update the stock quantity
                    string updateStockData = "UPDATE stock SET quantity = @quantity WHERE prod_id = @prodID";

                    using (SqlCommand updateStockCmd = new SqlCommand(updateStockData, connect))
                    {
                        updateStockCmd.Parameters.AddWithValue("@quantity", addProducts_stock.Text.Trim());
                        updateStockCmd.Parameters.AddWithValue("@prodID", addProducts_prodID.Text.Trim());

                        updateStockCmd.ExecuteNonQuery();
                    }

                    clearFields();
                    displayAllProducts();

                    MessageBox.Show("Product successfully updated!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection has failed: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
                    }
                }
            }
        }

        private void addProducts_removeBtn_Click(object sender, EventArgs e)
        {
            if (emptyFields())
            {
                MessageBox.Show("Empty Fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to remove Product ID: "
                    + addProducts_prodID.Text.Trim() + "?", "Confirmation Messaage"
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (checkConnection())
                    {
                        try
                        {
                            connect.Open();

                            // Get the image path before deletion
                            string selectImagePath = "SELECT image_path FROM products WHERE id = @id";
                            string imagePath = "";

                            // Delete from the stock table
                            string deleteStockData = "DELETE FROM stock WHERE prod_id = @prodID";
                            using (SqlCommand deleteStockCmd = new SqlCommand(deleteStockData, connect))
                            {
                                deleteStockCmd.Parameters.AddWithValue("@prodID", addProducts_prodID.Text.Trim());
                                deleteStockCmd.ExecuteNonQuery();
                            }

                            // Delete from the products table
                            string deleteProductData = "DELETE FROM products WHERE id = @id";
                            using (SqlCommand deleteProductCmd = new SqlCommand(deleteProductData, connect))
                            {
                                deleteProductCmd.Parameters.AddWithValue("@id", getID);
                                deleteProductCmd.ExecuteNonQuery();
                            }


                            // Remove the image file if it exists
                            if (File.Exists(imagePath))
                            {
                                File.Delete(imagePath);
                            }

                            clearFields();
                            displayAllProducts();

                            MessageBox.Show("Product successfully removed!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Connection has failed: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            connect.Close();
                        }
                    }
                }
            }
        }
    }
}
