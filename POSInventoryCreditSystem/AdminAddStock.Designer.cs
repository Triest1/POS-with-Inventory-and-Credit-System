namespace POSInventoryCreditSystem
{
    partial class AdminAddStock
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.stock_prodID = new System.Windows.Forms.TextBox();
            this.current_qty = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.addstocks_qty = new System.Windows.Forms.NumericUpDown();
            this.addCategories_clearBtn = new System.Windows.Forms.Button();
            this.addCategories_removeBtn = new System.Windows.Forms.Button();
            this.addCategories_addBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.addstocks_qty)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(387, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(703, 707);
            this.panel2.TabIndex = 5;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(17, 69);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(668, 617);
            this.dataGridView1.TabIndex = 13;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label5.Location = new System.Drawing.Point(22, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 24);
            this.label5.TabIndex = 12;
            this.label5.Text = "All Stock";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.stock_prodID);
            this.panel1.Controls.Add(this.current_qty);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.addstocks_qty);
            this.panel1.Controls.Add(this.addCategories_clearBtn);
            this.panel1.Controls.Add(this.addCategories_removeBtn);
            this.panel1.Controls.Add(this.addCategories_addBtn);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(23, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(348, 707);
            this.panel1.TabIndex = 4;
            // 
            // stock_prodID
            // 
            this.stock_prodID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stock_prodID.Location = new System.Drawing.Point(21, 102);
            this.stock_prodID.Name = "stock_prodID";
            this.stock_prodID.Size = new System.Drawing.Size(219, 26);
            this.stock_prodID.TabIndex = 17;
            // 
            // current_qty
            // 
            this.current_qty.AutoSize = true;
            this.current_qty.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.current_qty.ForeColor = System.Drawing.Color.RoyalBlue;
            this.current_qty.Location = new System.Drawing.Point(169, 175);
            this.current_qty.Name = "current_qty";
            this.current_qty.Size = new System.Drawing.Size(19, 20);
            this.current_qty.TabIndex = 16;
            this.current_qty.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label3.Location = new System.Drawing.Point(17, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 20);
            this.label3.TabIndex = 15;
            this.label3.Text = "Current Quantity:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label2.Location = new System.Drawing.Point(17, 245);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "Quantity";
            // 
            // addstocks_qty
            // 
            this.addstocks_qty.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addstocks_qty.Location = new System.Drawing.Point(21, 268);
            this.addstocks_qty.Name = "addstocks_qty";
            this.addstocks_qty.Size = new System.Drawing.Size(219, 26);
            this.addstocks_qty.TabIndex = 12;
            // 
            // addCategories_clearBtn
            // 
            this.addCategories_clearBtn.BackColor = System.Drawing.Color.RoyalBlue;
            this.addCategories_clearBtn.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addCategories_clearBtn.ForeColor = System.Drawing.Color.White;
            this.addCategories_clearBtn.Location = new System.Drawing.Point(21, 389);
            this.addCategories_clearBtn.Name = "addCategories_clearBtn";
            this.addCategories_clearBtn.Size = new System.Drawing.Size(302, 46);
            this.addCategories_clearBtn.TabIndex = 11;
            this.addCategories_clearBtn.Text = "Clear";
            this.addCategories_clearBtn.UseVisualStyleBackColor = false;
            this.addCategories_clearBtn.Click += new System.EventHandler(this.addCategories_clearBtn_Click);
            // 
            // addCategories_removeBtn
            // 
            this.addCategories_removeBtn.BackColor = System.Drawing.Color.RoyalBlue;
            this.addCategories_removeBtn.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addCategories_removeBtn.ForeColor = System.Drawing.Color.White;
            this.addCategories_removeBtn.Location = new System.Drawing.Point(200, 317);
            this.addCategories_removeBtn.Name = "addCategories_removeBtn";
            this.addCategories_removeBtn.Size = new System.Drawing.Size(123, 46);
            this.addCategories_removeBtn.TabIndex = 10;
            this.addCategories_removeBtn.Text = "Remove";
            this.addCategories_removeBtn.UseVisualStyleBackColor = false;
            this.addCategories_removeBtn.Click += new System.EventHandler(this.addCategories_removeBtn_Click);
            // 
            // addCategories_addBtn
            // 
            this.addCategories_addBtn.BackColor = System.Drawing.Color.RoyalBlue;
            this.addCategories_addBtn.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addCategories_addBtn.ForeColor = System.Drawing.Color.White;
            this.addCategories_addBtn.Location = new System.Drawing.Point(21, 317);
            this.addCategories_addBtn.Name = "addCategories_addBtn";
            this.addCategories_addBtn.Size = new System.Drawing.Size(123, 46);
            this.addCategories_addBtn.TabIndex = 8;
            this.addCategories_addBtn.Text = "Add";
            this.addCategories_addBtn.UseVisualStyleBackColor = false;
            this.addCategories_addBtn.Click += new System.EventHandler(this.addCategories_addBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.Location = new System.Drawing.Point(17, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Product Description";
            // 
            // AdminAddStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "AdminAddStock";
            this.Size = new System.Drawing.Size(1113, 756);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.addstocks_qty)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button addCategories_clearBtn;
        private System.Windows.Forms.Button addCategories_removeBtn;
        private System.Windows.Forms.Button addCategories_addBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown addstocks_qty;
        private System.Windows.Forms.Label current_qty;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox stock_prodID;
    }
}
