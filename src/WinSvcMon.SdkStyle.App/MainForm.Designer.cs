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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.servicesGridView = new System.Windows.Forms.DataGridView();
            this.ColumnOfDisplayName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnOfServiceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnOfServiceState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnToChangeState = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.servicesGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // servicesGridView
            // 
            this.servicesGridView.AllowUserToAddRows = false;
            this.servicesGridView.AllowUserToDeleteRows = false;
            this.servicesGridView.AllowUserToResizeRows = false;
            this.servicesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.servicesGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnOfDisplayName,
            this.ColumnOfServiceName,
            this.ColumnOfServiceState,
            this.ColumnToChangeState});
            this.servicesGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.servicesGridView.Location = new System.Drawing.Point(10, 10);
            this.servicesGridView.MultiSelect = false;
            this.servicesGridView.Name = "servicesGridView";
            this.servicesGridView.ReadOnly = true;
            this.servicesGridView.RowHeadersVisible = false;
            this.servicesGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("BIZ UDゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.servicesGridView.RowTemplate.Height = 30;
            this.servicesGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.servicesGridView.Size = new System.Drawing.Size(604, 421);
            this.servicesGridView.TabIndex = 2;
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
            this.Controls.Add(this.servicesGridView);
            this.Font = new System.Drawing.Font("BIZ UDゴシック", 12F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "Windows Service Monitor";
            ((System.ComponentModel.ISupportInitialize)(this.servicesGridView)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.DataGridView servicesGridView;
    private DataGridViewTextBoxColumn ColumnOfDisplayName;
    private DataGridViewTextBoxColumn ColumnOfServiceName;
    private DataGridViewTextBoxColumn ColumnOfServiceState;
    private DataGridViewButtonColumn ColumnToChangeState;
}
