namespace MyWinformsApp;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

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
        pictureDisplay = new PictureBox();
        ((System.ComponentModel.ISupportInitialize)pictureDisplay).BeginInit();
        SuspendLayout();
        // 
        // pictureDisplay
        // 
        pictureDisplay.Dock = DockStyle.Fill;
        pictureDisplay.Location = new Point(0, 0);
        pictureDisplay.Name = "pictureDisplay";
        pictureDisplay.Size = new Size(649, 168);
        pictureDisplay.TabIndex = 1;
        pictureDisplay.TabStop = false;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(649, 168);
        Controls.Add(pictureDisplay);
        Name = "Form1";
        Text = "Form1";
        Load += Form1_Load;
        ((System.ComponentModel.ISupportInitialize)pictureDisplay).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private PictureBox pictureDisplay;
}
