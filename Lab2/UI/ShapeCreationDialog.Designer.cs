using System.Windows.Forms;

namespace OOP_Lab2_GraphicEditor.UI
{
    partial class ShapeCreationDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Label lblShapeTypeCaption;
        private Label lblShapeTypeInfo;
        private Label lblX;
        private Label lblY;
        private Label lblSize1;
        private Label lblSize2;
        private NumericUpDown numX;
        private NumericUpDown numY;
        private NumericUpDown numSize1;
        private NumericUpDown numSize2;
        private Button btnOk;
        private Button btnCancel;
        private Button btnColor;
        private Label lblColor;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Initializes dialog visual components and layout.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblShapeTypeCaption = new System.Windows.Forms.Label();
            this.lblShapeTypeInfo = new System.Windows.Forms.Label();
            this.lblX = new System.Windows.Forms.Label();
            this.lblY = new System.Windows.Forms.Label();
            this.lblSize1 = new System.Windows.Forms.Label();
            this.lblSize2 = new System.Windows.Forms.Label();
            this.numX = new System.Windows.Forms.NumericUpDown();
            this.numY = new System.Windows.Forms.NumericUpDown();
            this.numSize1 = new System.Windows.Forms.NumericUpDown();
            this.numSize2 = new System.Windows.Forms.NumericUpDown();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnColor = new System.Windows.Forms.Button();
            this.lblColor = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSize1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSize2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblShapeTypeCaption
            // 
            this.lblShapeTypeCaption.AutoSize = true;
            this.lblShapeTypeCaption.Location = new System.Drawing.Point(12, 9);
            this.lblShapeTypeCaption.Name = "lblShapeTypeCaption";
            this.lblShapeTypeCaption.Size = new System.Drawing.Size(69, 15);
            this.lblShapeTypeCaption.TabIndex = 0;
            this.lblShapeTypeCaption.Text = "Shape type:";
            // 
            // lblShapeTypeInfo
            // 
            this.lblShapeTypeInfo.AutoSize = true;
            this.lblShapeTypeInfo.Location = new System.Drawing.Point(120, 9);
            this.lblShapeTypeInfo.Name = "lblShapeTypeInfo";
            this.lblShapeTypeInfo.Size = new System.Drawing.Size(38, 15);
            this.lblShapeTypeInfo.TabIndex = 1;
            this.lblShapeTypeInfo.Text = "Circle";
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.Location = new System.Drawing.Point(12, 40);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(16, 15);
            this.lblX.TabIndex = 2;
            this.lblX.Text = "X:";
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.Location = new System.Drawing.Point(12, 70);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(16, 15);
            this.lblY.TabIndex = 3;
            this.lblY.Text = "Y:";
            // 
            // lblSize1
            // 
            this.lblSize1.AutoSize = true;
            this.lblSize1.Location = new System.Drawing.Point(12, 100);
            this.lblSize1.Name = "lblSize1";
            this.lblSize1.Size = new System.Drawing.Size(42, 15);
            this.lblSize1.TabIndex = 4;
            this.lblSize1.Text = "Size 1:";
            // 
            // lblSize2
            // 
            this.lblSize2.AutoSize = true;
            this.lblSize2.Location = new System.Drawing.Point(12, 130);
            this.lblSize2.Name = "lblSize2";
            this.lblSize2.Size = new System.Drawing.Size(42, 15);
            this.lblSize2.TabIndex = 5;
            this.lblSize2.Text = "Size 2:";
            // 
            // numX
            // 
            this.numX.Location = new System.Drawing.Point(120, 38);
            this.numX.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numX.Name = "numX";
            this.numX.Size = new System.Drawing.Size(120, 23);
            this.numX.TabIndex = 6;
            // 
            // numY
            // 
            this.numY.Location = new System.Drawing.Point(120, 68);
            this.numY.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numY.Name = "numY";
            this.numY.Size = new System.Drawing.Size(120, 23);
            this.numY.TabIndex = 7;
            // 
            // numSize1
            // 
            this.numSize1.Location = new System.Drawing.Point(120, 98);
            this.numSize1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numSize1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSize1.Name = "numSize1";
            this.numSize1.Size = new System.Drawing.Size(120, 23);
            this.numSize1.TabIndex = 8;
            this.numSize1.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // numSize2
            // 
            this.numSize2.Location = new System.Drawing.Point(120, 128);
            this.numSize2.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numSize2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSize2.Name = "numSize2";
            this.numSize2.Size = new System.Drawing.Size(120, 23);
            this.numSize2.TabIndex = 9;
            this.numSize2.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(84, 203);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 27);
            this.btnOk.TabIndex = 12;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(165, 203);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 27);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnColor
            // 
            this.btnColor.BackColor = System.Drawing.Color.Black;
            this.btnColor.Location = new System.Drawing.Point(120, 162);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(120, 23);
            this.btnColor.TabIndex = 11;
            this.btnColor.UseVisualStyleBackColor = false;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Location = new System.Drawing.Point(12, 166);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(39, 15);
            this.lblColor.TabIndex = 10;
            this.lblColor.Text = "Color:";
            // 
            // ShapeCreationDialog
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(259, 242);
            this.Controls.Add(this.lblColor);
            this.Controls.Add(this.btnColor);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.numSize2);
            this.Controls.Add(this.numSize1);
            this.Controls.Add(this.numY);
            this.Controls.Add(this.numX);
            this.Controls.Add(this.lblSize2);
            this.Controls.Add(this.lblSize1);
            this.Controls.Add(this.lblY);
            this.Controls.Add(this.lblX);
            this.Controls.Add(this.lblShapeTypeInfo);
            this.Controls.Add(this.lblShapeTypeCaption);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShapeCreationDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create Shape";
            ((System.ComponentModel.ISupportInitialize)(this.numX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSize1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSize2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

