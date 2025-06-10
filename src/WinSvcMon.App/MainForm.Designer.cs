namespace WinSvcMon.App;

partial class MainForm
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ColumnOfDisplayName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnOfServiceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnOfServiceState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnToChangeState = new System.Windows.Forms.DataGridViewButtonColumn();
            servicesGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(servicesGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // servicesGridView
            // 
            servicesGridView.AllowUserToAddRows = false;
            servicesGridView.AllowUserToDeleteRows = false;
            servicesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            servicesGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnOfDisplayName,
            this.ColumnOfServiceName,
            this.ColumnOfServiceState,
            this.ColumnToChangeState});
            servicesGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            servicesGridView.Location = new System.Drawing.Point(10, 10);
            servicesGridView.MultiSelect = false;
            servicesGridView.Name = "servicesGridView";
            servicesGridView.ReadOnly = true;
            servicesGridView.RowHeadersVisible = false;
            servicesGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("BIZ UDゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            servicesGridView.RowTemplate.Height = 30;
            servicesGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            servicesGridView.Size = new System.Drawing.Size(604, 421);
            servicesGridView.TabIndex = 2;
            servicesGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnServicesGridViewCellContentClick);
            // 
            // ColumnOfDisplayName
            // 
            this.ColumnOfDisplayName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnOfDisplayName.HeaderText = "サービス表示名";
            this.ColumnOfDisplayName.Name = "ColumnOfDisplayName";
            this.ColumnOfDisplayName.ReadOnly = true;
            // 
            // ColumnOfServiceName
            // 
            this.ColumnOfServiceName.FillWeight = 120F;
            this.ColumnOfServiceName.HeaderText = "サービス名";
            this.ColumnOfServiceName.Name = "ColumnOfServiceName";
            this.ColumnOfServiceName.ReadOnly = true;
            this.ColumnOfServiceName.Width = 160;
            // 
            // ColumnOfServiceState
            // 
            this.ColumnOfServiceState.HeaderText = "状態";
            this.ColumnOfServiceState.Name = "ColumnOfServiceState";
            this.ColumnOfServiceState.ReadOnly = true;
            // 
            // ColumnToChangeState
            // 
            this.ColumnToChangeState.HeaderText = "操作";
            this.ColumnToChangeState.Name = "ColumnToChangeState";
            this.ColumnToChangeState.ReadOnly = true;
            this.ColumnToChangeState.Text = "切替";
            this.ColumnToChangeState.UseColumnTextForButtonValue = true;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(servicesGridView);
            this.Font = new System.Drawing.Font("BIZ UDゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "Windows Service Monitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(servicesGridView)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOfDisplayName;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOfServiceName;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOfServiceState;
    private System.Windows.Forms.DataGridViewButtonColumn ColumnToChangeState;
    private System.Windows.Forms.DataGridView servicesGridView;
}
