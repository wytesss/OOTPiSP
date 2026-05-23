// =============================================================================
// Файл: SettingsForm.Designer.cs — разметка окна настроек (flowProcessing для чекбоксов).
// =============================================================================

namespace lab5
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;
        private FlowLayoutPanel flowProcessing;
        private Label labelTitle;
        private Button buttonOk;
        private Button buttonCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            labelTitle = new Label();
            flowProcessing = new FlowLayoutPanel();
            buttonOk = new Button();
            buttonCancel = new Button();
            SuspendLayout();
            //
            // labelTitle
            //
            labelTitle.AutoSize = true;
            labelTitle.Location = new Point(14, 14);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(280, 20);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Типы обработки при сохранении/загрузке:";
            //
            // flowProcessing
            //
            flowProcessing.AutoScroll = true;
            flowProcessing.FlowDirection = FlowDirection.TopDown;
            flowProcessing.Location = new Point(14, 44);
            flowProcessing.Name = "flowProcessing";
            flowProcessing.Padding = new Padding(4);
            flowProcessing.Size = new Size(456, 220);
            flowProcessing.TabIndex = 1;
            flowProcessing.WrapContents = false;
            //
            // buttonOk
            //
            buttonOk.Location = new Point(274, 278);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(94, 32);
            buttonOk.TabIndex = 2;
            buttonOk.Text = "OK";
            buttonOk.UseVisualStyleBackColor = true;
            buttonOk.Click += buttonOk_Click;
            //
            // buttonCancel
            //
            buttonCancel.Location = new Point(376, 278);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(94, 32);
            buttonCancel.TabIndex = 3;
            buttonCancel.Text = "Отмена";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            //
            // SettingsForm
            //
            AcceptButton = buttonOk;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new Size(484, 324);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOk);
            Controls.Add(flowProcessing);
            Controls.Add(labelTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SettingsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Настройки обработки";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
