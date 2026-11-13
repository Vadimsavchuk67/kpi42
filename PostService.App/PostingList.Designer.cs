namespace PostService.App;

partial class PostingList
{
    private System.ComponentModel.IContainer components = new System.ComponentModel.Container();
    private DataGridView gridPostings = null!;
    private Button btnDelete = null!;
    private Button btnRefresh = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            components?.Dispose();
            client.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        gridPostings = new DataGridView();
        btnDelete = new Button();
        btnRefresh = new Button();
        ((System.ComponentModel.ISupportInitialize)gridPostings).BeginInit();
        SuspendLayout();

        gridPostings.Name = "gridPostings";
        gridPostings.Location = new Point(12, 12);
        gridPostings.Size = new Size(1140, 420);
        gridPostings.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        gridPostings.ReadOnly = true;
        gridPostings.AllowUserToAddRows = false;
        gridPostings.AllowUserToDeleteRows = false;
        gridPostings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridPostings.MultiSelect = false;
        gridPostings.AutoGenerateColumns = false;
        gridPostings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        gridPostings.RowHeadersVisible = false;
        gridPostings.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colId",
            DataPropertyName = "Id",
            HeaderText = "Номер",
            Visible = false,
            MinimumWidth = 70
        });
        gridPostings.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colFrom",
            DataPropertyName = "From",
            HeaderText = "Відправник",
            Visible = true,
            MinimumWidth = 70
        });
        gridPostings.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colTo",
            DataPropertyName = "To",
            HeaderText = "Отримувач",
            Visible = true,
            MinimumWidth = 70
        });
        gridPostings.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colContent",
            DataPropertyName = "Content",
            HeaderText = "Вміст",
            Visible = true,
            MinimumWidth = 70
        });
        gridPostings.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colDeliveryType",
            DataPropertyName = "DeliveryType",
            HeaderText = "Доставка",
            Visible = true,
            MinimumWidth = 70
        });
        gridPostings.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colWeight",
            DataPropertyName = "Weight",
            HeaderText = "Вага",
            Visible = true,
            MinimumWidth = 70
        });
        gridPostings.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colWidth",
            DataPropertyName = "Width",
            HeaderText = "Ширина",
            Visible = true,
            MinimumWidth = 70
        });
        gridPostings.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colHeight",
            DataPropertyName = "Height",
            HeaderText = "Висота",
            Visible = true,
            MinimumWidth = 70
        });
        gridPostings.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colDepth",
            DataPropertyName = "Depth",
            HeaderText = "Глибина",
            Visible = true,
            MinimumWidth = 70
        });
        gridPostings.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colValue",
            DataPropertyName = "Value",
            HeaderText = "Цінність",
            Visible = true,
            MinimumWidth = 70
        });
        gridPostings.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colPrice",
            DataPropertyName = "Price",
            HeaderText = "Вартість",
            Visible = true,
            MinimumWidth = 70
        });
        gridPostings.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colCreatedAt",
            DataPropertyName = "CreatedAt",
            HeaderText = "Дата створення",
            Visible = false,
            MinimumWidth = 70
        });

        btnRefresh.Name = "btnRefresh";
        btnRefresh.Text = "Оновити";
        btnRefresh.Location = new Point(12, 448);
        btnRefresh.Size = new Size(120, 36);
        btnRefresh.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnRefresh.Click += btnRefresh_Click;

        btnDelete.Name = "btnDelete";
        btnDelete.Text = "Видалити";
        btnDelete.Location = new Point(144, 448);
        btnDelete.Size = new Size(120, 36);
        btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnDelete.Click += btnDelete_Click;

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1164, 500);
        MinimumSize = new Size(900, 400);
        StartPosition = FormStartPosition.CenterScreen;
        Name = "PostingList";
        Text = "Поштові відправлення";
        Controls.Add(gridPostings);
        Controls.Add(btnDelete);
        Controls.Add(btnRefresh);
        Load += PostingList_Load;
        ((System.ComponentModel.ISupportInitialize)gridPostings).EndInit();
        ResumeLayout(false);
    }
}
