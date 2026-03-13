using System.Windows.Forms;

namespace OOP_Lab2_GraphicEditor
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Panel canvasPanel;
        private ComboBox comboShapeType;
        private Button btnAddShape;
        private Button btnClear;
        private Label lblShapeType;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">True if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Initializes the layout and visual components of the main form.
        /// </summary>
        private void InitializeComponent()
        {
            canvasPanel = new Panel();
            comboShapeType = new ComboBox();
            btnAddShape = new Button();
            btnClear = new Button();
            lblShapeType = new Label();
            SuspendLayout();
            // 
            // canvasPanel
            // 
            canvasPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            canvasPanel.BackColor = Color.White;
            canvasPanel.BorderStyle = BorderStyle.FixedSingle;
            canvasPanel.Location = new Point(14, 80);
            canvasPanel.Margin = new Padding(3, 4, 3, 4);
            canvasPanel.Name = "canvasPanel";
            canvasPanel.Size = new Size(885, 530);
            canvasPanel.TabIndex = 0;
            canvasPanel.Paint += canvasPanel_Paint;
            // 
            // comboShapeType
            // 
            comboShapeType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboShapeType.FormattingEnabled = true;
            comboShapeType.Location = new Point(97, 24);
            comboShapeType.Margin = new Padding(3, 4, 3, 4);
            comboShapeType.Name = "comboShapeType";
            comboShapeType.Size = new Size(182, 28);
            comboShapeType.TabIndex = 1;
            // 
            // btnAddShape
            // 
            btnAddShape.Location = new Point(297, 23);
            btnAddShape.Margin = new Padding(3, 4, 3, 4);
            btnAddShape.Name = "btnAddShape";
            btnAddShape.Size = new Size(114, 33);
            btnAddShape.TabIndex = 2;
            btnAddShape.Text = "Add Shape";
            btnAddShape.UseVisualStyleBackColor = true;
            btnAddShape.Click += btnAddShape_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(429, 23);
            btnClear.Margin = new Padding(3, 4, 3, 4);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(114, 33);
            btnClear.TabIndex = 3;
            btnClear.Text = "Clear Canvas";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // lblShapeType
            // 
            lblShapeType.AutoSize = true;
            lblShapeType.Location = new Point(14, 28);
            lblShapeType.Name = "lblShapeType";
            lblShapeType.Size = new Size(86, 20);
            lblShapeType.TabIndex = 4;
            lblShapeType.Text = "Shape type:";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(913, 627);
            Controls.Add(lblShapeType);
            Controls.Add(btnClear);
            Controls.Add(btnAddShape);
            Controls.Add(comboShapeType);
            Controls.Add(canvasPanel);
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(569, 384);
            Name = "MainForm";
            Text = "OOP Lab 2 - Graphic Editor";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

