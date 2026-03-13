namespace lab3
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.ListBox listVehicles;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.ComboBox comboVehicleTypes;
        private System.Windows.Forms.Label labelTypes;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listVehicles = new System.Windows.Forms.ListBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.comboVehicleTypes = new System.Windows.Forms.ComboBox();
            this.labelTypes = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listVehicles
            // 
            this.listVehicles.FormattingEnabled = true;
            this.listVehicles.ItemHeight = 15;
            this.listVehicles.Location = new System.Drawing.Point(12, 49);
            this.listVehicles.Name = "listVehicles";
            this.listVehicles.Size = new System.Drawing.Size(480, 379);
            this.listVehicles.TabIndex = 0;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(510, 49);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(120, 30);
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(510, 95);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(120, 30);
            this.buttonEdit.TabIndex = 2;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(510, 141);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(120, 30);
            this.buttonRemove.TabIndex = 3;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(510, 283);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(120, 30);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.Text = "Serialize";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(510, 329);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(120, 30);
            this.buttonLoad.TabIndex = 5;
            this.buttonLoad.Text = "Deserialize";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // comboVehicleTypes
            // 
            this.comboVehicleTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboVehicleTypes.FormattingEnabled = true;
            this.comboVehicleTypes.Location = new System.Drawing.Point(126, 12);
            this.comboVehicleTypes.Name = "comboVehicleTypes";
            this.comboVehicleTypes.Size = new System.Drawing.Size(366, 23);
            this.comboVehicleTypes.TabIndex = 6;
            // 
            // labelTypes
            // 
            this.labelTypes.AutoSize = true;
            this.labelTypes.Location = new System.Drawing.Point(12, 15);
            this.labelTypes.Name = "labelTypes";
            this.labelTypes.Size = new System.Drawing.Size(100, 15);
            this.labelTypes.TabIndex = 7;
            this.labelTypes.Text = "Vehicle type to add:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 450);
            this.Controls.Add(this.labelTypes);
            this.Controls.Add(this.comboVehicleTypes);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.listVehicles);
            this.Name = "Form1";
            this.Text = "Vehicle manager (BSON)";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}

