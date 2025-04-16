namespace uchet
{
    partial class Административное_меню
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            tabPageApplications = new TabPage();
            dataGridViewApplications = new DataGridView();
            dateTimePickerFilter = new DateTimePicker();
            buttonFilter = new Button();
            buttonApprove = new Button();
            buttonReject = new Button();
            buttonViewDetails = new Button();
            tabPageDirections = new TabPage();
            dataGridViewDirections = new DataGridView();
            tabPageEducationLevels = new TabPage();
            dataGridViewEducationLevels = new DataGridView();
            tabPageExecutors = new TabPage();
            dataGridViewExecutors = new DataGridView();
            buttonAssignExecutor = new Button();
            buttonAddComment = new Button();
            tabPageStatistics = new TabPage();
            dataGridViewStatistics = new DataGridView();
            buttonShowApplications = new Button();
            buttonShowDirections = new Button();
            buttonShowEducationLevels = new Button();
            buttonShowExecutors = new Button();
            buttonShowStatistics = new Button();
            buttonLogout = new Button();
            tabControl1.SuspendLayout();
            tabPageApplications.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewApplications).BeginInit();
            tabPageDirections.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewDirections).BeginInit();
            tabPageEducationLevels.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewEducationLevels).BeginInit();
            tabPageExecutors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewExecutors).BeginInit();
            tabPageStatistics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewStatistics).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPageApplications);
            tabControl1.Controls.Add(tabPageDirections);
            tabControl1.Controls.Add(tabPageEducationLevels);
            tabControl1.Controls.Add(tabPageExecutors);
            tabControl1.Controls.Add(tabPageStatistics);
            tabControl1.Location = new Point(16, 72);
            tabControl1.Margin = new Padding(4, 5, 4, 5);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1320, 632);
            tabControl1.TabIndex = 0;
            // 
            // tabPageApplications
            // 
            tabPageApplications.Controls.Add(dataGridViewApplications);
            tabPageApplications.Controls.Add(dateTimePickerFilter);
            tabPageApplications.Controls.Add(buttonFilter);
            tabPageApplications.Controls.Add(buttonApprove);
            tabPageApplications.Controls.Add(buttonReject);
            tabPageApplications.Controls.Add(buttonViewDetails);
            tabPageApplications.Location = new Point(4, 29);
            tabPageApplications.Margin = new Padding(4, 5, 4, 5);
            tabPageApplications.Name = "tabPageApplications";
            tabPageApplications.Padding = new Padding(4, 5, 4, 5);
            tabPageApplications.Size = new Size(1312, 599);
            tabPageApplications.TabIndex = 0;
            tabPageApplications.Text = "Заявки";
            tabPageApplications.UseVisualStyleBackColor = true;
            // 
            // dataGridViewApplications
            // 
            dataGridViewApplications.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewApplications.Location = new Point(8, 77);
            dataGridViewApplications.Margin = new Padding(4, 5, 4, 5);
            dataGridViewApplications.Name = "dataGridViewApplications";
            dataGridViewApplications.RowHeadersWidth = 51;
            dataGridViewApplications.Size = new Size(1296, 515);
            dataGridViewApplications.TabIndex = 0;
            dataGridViewApplications.CellDoubleClick += dataGridViewApplications_CellDoubleClick;
            // 
            // dateTimePickerFilter
            // 
            dateTimePickerFilter.Location = new Point(8, 31);
            dateTimePickerFilter.Margin = new Padding(4, 5, 4, 5);
            dateTimePickerFilter.Name = "dateTimePickerFilter";
            dateTimePickerFilter.Size = new Size(265, 27);
            dateTimePickerFilter.TabIndex = 1;
            // 
            // buttonFilter
            // 
            buttonFilter.Location = new Point(336, 24);
            buttonFilter.Margin = new Padding(4, 5, 4, 5);
            buttonFilter.Name = "buttonFilter";
            buttonFilter.Size = new Size(133, 35);
            buttonFilter.TabIndex = 2;
            buttonFilter.Text = "Фильтр";
            buttonFilter.UseVisualStyleBackColor = true;
            buttonFilter.Click += buttonFilter_Click;
            // 
            // buttonApprove
            // 
            buttonApprove.Location = new Point(536, 24);
            buttonApprove.Margin = new Padding(4, 5, 4, 5);
            buttonApprove.Name = "buttonApprove";
            buttonApprove.Size = new Size(200, 35);
            buttonApprove.TabIndex = 3;
            buttonApprove.Text = "Одобрить";
            buttonApprove.UseVisualStyleBackColor = true;
            buttonApprove.Click += buttonApprove_Click;
            // 
            // buttonReject
            // 
            buttonReject.Location = new Point(832, 24);
            buttonReject.Margin = new Padding(4, 5, 4, 5);
            buttonReject.Name = "buttonReject";
            buttonReject.Size = new Size(200, 35);
            buttonReject.TabIndex = 4;
            buttonReject.Text = "Вернуть на доработку";
            buttonReject.UseVisualStyleBackColor = true;
            buttonReject.Click += buttonReject_Click;
            // 
            // buttonViewDetails
            // 
            buttonViewDetails.Location = new Point(1104, 24);
            buttonViewDetails.Margin = new Padding(4, 5, 4, 5);
            buttonViewDetails.Name = "buttonViewDetails";
            buttonViewDetails.Size = new Size(200, 35);
            buttonViewDetails.TabIndex = 5;
            buttonViewDetails.Text = "Просмотр деталей";
            buttonViewDetails.UseVisualStyleBackColor = true;
            buttonViewDetails.Click += buttonViewDetails_Click;
            // 
            // tabPageDirections
            // 
            tabPageDirections.Controls.Add(dataGridViewDirections);
            tabPageDirections.Location = new Point(4, 29);
            tabPageDirections.Margin = new Padding(4, 5, 4, 5);
            tabPageDirections.Name = "tabPageDirections";
            tabPageDirections.Padding = new Padding(4, 5, 4, 5);
            tabPageDirections.Size = new Size(1312, 599);
            tabPageDirections.TabIndex = 1;
            tabPageDirections.Text = "Направления";
            tabPageDirections.UseVisualStyleBackColor = true;
            // 
            // dataGridViewDirections
            // 
            dataGridViewDirections.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewDirections.Location = new Point(8, 9);
            dataGridViewDirections.Margin = new Padding(4, 5, 4, 5);
            dataGridViewDirections.Name = "dataGridViewDirections";
            dataGridViewDirections.RowHeadersWidth = 51;
            dataGridViewDirections.Size = new Size(1296, 515);
            dataGridViewDirections.TabIndex = 0;
            // 
            // tabPageEducationLevels
            // 
            tabPageEducationLevels.Controls.Add(dataGridViewEducationLevels);
            tabPageEducationLevels.Location = new Point(4, 29);
            tabPageEducationLevels.Margin = new Padding(4, 5, 4, 5);
            tabPageEducationLevels.Name = "tabPageEducationLevels";
            tabPageEducationLevels.Padding = new Padding(4, 5, 4, 5);
            tabPageEducationLevels.Size = new Size(1312, 599);
            tabPageEducationLevels.TabIndex = 2;
            tabPageEducationLevels.Text = "Уровни образования";
            tabPageEducationLevels.UseVisualStyleBackColor = true;
            // 
            // dataGridViewEducationLevels
            // 
            dataGridViewEducationLevels.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewEducationLevels.Location = new Point(8, 9);
            dataGridViewEducationLevels.Margin = new Padding(4, 5, 4, 5);
            dataGridViewEducationLevels.Name = "dataGridViewEducationLevels";
            dataGridViewEducationLevels.RowHeadersWidth = 51;
            dataGridViewEducationLevels.Size = new Size(1296, 515);
            dataGridViewEducationLevels.TabIndex = 0;
            // 
            // tabPageExecutors
            // 
            tabPageExecutors.Controls.Add(dataGridViewExecutors);
            tabPageExecutors.Controls.Add(buttonAssignExecutor);
            tabPageExecutors.Controls.Add(buttonAddComment);
            tabPageExecutors.Location = new Point(4, 29);
            tabPageExecutors.Margin = new Padding(4, 5, 4, 5);
            tabPageExecutors.Name = "tabPageExecutors";
            tabPageExecutors.Padding = new Padding(4, 5, 4, 5);
            tabPageExecutors.Size = new Size(1312, 599);
            tabPageExecutors.TabIndex = 3;
            tabPageExecutors.Text = "Исполнители";
            tabPageExecutors.UseVisualStyleBackColor = true;
            // 
            // dataGridViewExecutors
            // 
            dataGridViewExecutors.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewExecutors.Location = new Point(8, 77);
            dataGridViewExecutors.Margin = new Padding(4, 5, 4, 5);
            dataGridViewExecutors.Name = "dataGridViewExecutors";
            dataGridViewExecutors.RowHeadersWidth = 51;
            dataGridViewExecutors.Size = new Size(1296, 515);
            dataGridViewExecutors.TabIndex = 0;
            // 
            // buttonAssignExecutor
            // 
            buttonAssignExecutor.Location = new Point(8, 31);
            buttonAssignExecutor.Margin = new Padding(4, 5, 4, 5);
            buttonAssignExecutor.Name = "buttonAssignExecutor";
            buttonAssignExecutor.Size = new Size(200, 35);
            buttonAssignExecutor.TabIndex = 1;
            buttonAssignExecutor.Text = "Назначить исполнителя";
            buttonAssignExecutor.UseVisualStyleBackColor = true;
            // 
            // buttonAddComment
            // 
            buttonAddComment.Location = new Point(216, 31);
            buttonAddComment.Margin = new Padding(4, 5, 4, 5);
            buttonAddComment.Name = "buttonAddComment";
            buttonAddComment.Size = new Size(200, 35);
            buttonAddComment.TabIndex = 2;
            buttonAddComment.Text = "Добавить комментарий";
            buttonAddComment.UseVisualStyleBackColor = true;
            // 
            // tabPageStatistics
            // 
            tabPageStatistics.Controls.Add(dataGridViewStatistics);
            tabPageStatistics.Location = new Point(4, 29);
            tabPageStatistics.Margin = new Padding(4, 5, 4, 5);
            tabPageStatistics.Name = "tabPageStatistics";
            tabPageStatistics.Padding = new Padding(4, 5, 4, 5);
            tabPageStatistics.Size = new Size(1312, 599);
            tabPageStatistics.TabIndex = 4;
            tabPageStatistics.Text = "Статистика";
            tabPageStatistics.UseVisualStyleBackColor = true;
            // 
            // dataGridViewStatistics
            // 
            dataGridViewStatistics.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewStatistics.Location = new Point(8, 9);
            dataGridViewStatistics.Margin = new Padding(4, 5, 4, 5);
            dataGridViewStatistics.Name = "dataGridViewStatistics";
            dataGridViewStatistics.RowHeadersWidth = 51;
            dataGridViewStatistics.Size = new Size(1187, 542);
            dataGridViewStatistics.TabIndex = 0;
            // 
            // buttonShowApplications
            // 
            buttonShowApplications.Location = new Point(16, 16);
            buttonShowApplications.Margin = new Padding(4, 5, 4, 5);
            buttonShowApplications.Name = "buttonShowApplications";
            buttonShowApplications.Size = new Size(250, 50);
            buttonShowApplications.TabIndex = 1;
            buttonShowApplications.Text = "Заявки";
            buttonShowApplications.UseVisualStyleBackColor = true;
            buttonShowApplications.Click += buttonShowApplications_Click;
            // 
            // buttonShowDirections
            // 
            buttonShowDirections.Location = new Point(280, 16);
            buttonShowDirections.Margin = new Padding(4, 5, 4, 5);
            buttonShowDirections.Name = "buttonShowDirections";
            buttonShowDirections.Size = new Size(250, 50);
            buttonShowDirections.TabIndex = 2;
            buttonShowDirections.Text = "Направления";
            buttonShowDirections.UseVisualStyleBackColor = true;
            buttonShowDirections.Click += buttonShowDirections_Click;
            // 
            // buttonShowEducationLevels
            // 
            buttonShowEducationLevels.Location = new Point(544, 16);
            buttonShowEducationLevels.Margin = new Padding(4, 5, 4, 5);
            buttonShowEducationLevels.Name = "buttonShowEducationLevels";
            buttonShowEducationLevels.Size = new Size(250, 50);
            buttonShowEducationLevels.TabIndex = 3;
            buttonShowEducationLevels.Text = "Уровни образования";
            buttonShowEducationLevels.UseVisualStyleBackColor = true;
            buttonShowEducationLevels.Click += buttonShowEducationLevels_Click;
            // 
            // buttonShowExecutors
            // 
            buttonShowExecutors.Location = new Point(808, 16);
            buttonShowExecutors.Margin = new Padding(4, 5, 4, 5);
            buttonShowExecutors.Name = "buttonShowExecutors";
            buttonShowExecutors.Size = new Size(250, 50);
            buttonShowExecutors.TabIndex = 4;
            buttonShowExecutors.Text = "Исполнители";
            buttonShowExecutors.UseVisualStyleBackColor = true;
            buttonShowExecutors.Click += buttonShowExecutors_Click;
            // 
            // buttonShowStatistics
            // 
            buttonShowStatistics.Location = new Point(1072, 16);
            buttonShowStatistics.Margin = new Padding(4, 5, 4, 5);
            buttonShowStatistics.Name = "buttonShowStatistics";
            buttonShowStatistics.Size = new Size(250, 50);
            buttonShowStatistics.TabIndex = 5;
            buttonShowStatistics.Text = "Статистика";
            buttonShowStatistics.UseVisualStyleBackColor = true;
            buttonShowStatistics.Click += buttonShowStatistics_Click;
            // 
            // buttonLogout
            // 
            buttonLogout.Location = new Point(1669, 18);
            buttonLogout.Margin = new Padding(4, 5, 4, 5);
            buttonLogout.Name = "buttonLogout";
            buttonLogout.Size = new Size(160, 46);
            buttonLogout.TabIndex = 6;
            buttonLogout.Text = "Выход";
            buttonLogout.UseVisualStyleBackColor = true;
            buttonLogout.Click += buttonLogout_Click;
            // 
            // Административное_меню
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1348, 721);
            Controls.Add(buttonLogout);
            Controls.Add(buttonShowStatistics);
            Controls.Add(buttonShowExecutors);
            Controls.Add(buttonShowEducationLevels);
            Controls.Add(buttonShowDirections);
            Controls.Add(buttonShowApplications);
            Controls.Add(tabControl1);
            Margin = new Padding(4, 5, 4, 5);
            Name = "Административное_меню";
            Text = "Административное меню";
            tabControl1.ResumeLayout(false);
            tabPageApplications.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewApplications).EndInit();
            tabPageDirections.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewDirections).EndInit();
            tabPageEducationLevels.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewEducationLevels).EndInit();
            tabPageExecutors.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewExecutors).EndInit();
            tabPageStatistics.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewStatistics).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageDirections;
        private System.Windows.Forms.DataGridView dataGridViewDirections;
        private System.Windows.Forms.TabPage tabPageEducationLevels;
        private System.Windows.Forms.DataGridView dataGridViewEducationLevels;
        private System.Windows.Forms.TabPage tabPageExecutors;
        private System.Windows.Forms.DataGridView dataGridViewExecutors;
        private System.Windows.Forms.TabPage tabPageStatistics;
        private System.Windows.Forms.DataGridView dataGridViewStatistics;
        private System.Windows.Forms.Button buttonAssignExecutor;
        private System.Windows.Forms.Button buttonAddComment;
        private System.Windows.Forms.Button buttonShowApplications;
        private System.Windows.Forms.Button buttonShowDirections;
        private System.Windows.Forms.Button buttonShowEducationLevels;
        private System.Windows.Forms.Button buttonShowExecutors;
        private System.Windows.Forms.Button buttonShowStatistics;
        private System.Windows.Forms.Button buttonLogout;
        private TabPage tabPageApplications;
        private DataGridView dataGridViewApplications;
        private DateTimePicker dateTimePickerFilter;
        private Button buttonFilter;
        private Button buttonApprove;
        private Button buttonReject;
        private Button buttonViewDetails;
    }
}