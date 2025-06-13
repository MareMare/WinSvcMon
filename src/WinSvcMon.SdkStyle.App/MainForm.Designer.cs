namespace WinSvcMon.SdkStyle.App;

partial class MainForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        var resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
        this.servicesGridView = new DataGridView();
        this.ColumnOfDisplayName = new DataGridViewTextBoxColumn();
        this.ColumnOfServiceName = new DataGridViewTextBoxColumn();
        this.ColumnOfServiceState = new DataGridViewTextBoxColumn();
        this.ColumnToChangeState = new DataGridViewButtonColumn();
        ((System.ComponentModel.ISupportInitialize)this.servicesGridView).BeginInit();
        this.SuspendLayout();
        // 
        // servicesGridView
        // 
        this.servicesGridView.AllowUserToAddRows = false;
        this.servicesGridView.AllowUserToDeleteRows = false;
        this.servicesGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.servicesGridView.Columns.AddRange(new DataGridViewColumn[] { this.ColumnOfDisplayName, this.ColumnOfServiceName, this.ColumnOfServiceState, this.ColumnToChangeState });
        this.servicesGridView.Dock = DockStyle.Fill;
        this.servicesGridView.Location = new Point(10, 10);
        this.servicesGridView.MultiSelect = false;
        this.servicesGridView.Name = "servicesGridView";
        this.servicesGridView.ReadOnly = true;
        this.servicesGridView.RowHeadersVisible = false;
        this.servicesGridView.RowTemplate.DefaultCellStyle.Font = new Font("BIZ UDゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
        this.servicesGridView.RowTemplate.Height = 30;
        this.servicesGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        this.servicesGridView.Size = new Size(604, 421);
        this.servicesGridView.TabIndex = 2;
        this.servicesGridView.CellContentClick += this.OnServicesGridViewCellContentClick;
        // 
        // ColumnOfDisplayName
        // 
        this.ColumnOfDisplayName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
        this.AutoScaleMode = AutoScaleMode.None;
        this.ClientSize = new Size(624, 441);
        this.Controls.Add(this.servicesGridView);
        this.Font = new Font("BIZ UDゴシック", 12F);
        this.Icon = (Icon)resources.GetObject("$this.Icon");
        this.Name = "MainForm";
        this.Padding = new Padding(10);
        this.Text = "Windows Service Monitor";
        this.FormClosing += this.MainForm_FormClosing;
        this.Load += this.MainForm_Load;
        ((System.ComponentModel.ISupportInitialize)this.servicesGridView).EndInit();
        this.ResumeLayout(false);
    }

    #endregion
    private System.Windows.Forms.DataGridView servicesGridView;
    private DataGridViewTextBoxColumn ColumnOfDisplayName;
    private DataGridViewTextBoxColumn ColumnOfServiceName;
    private DataGridViewTextBoxColumn ColumnOfServiceState;
    private DataGridViewButtonColumn ColumnToChangeState;
}
