#region Copyright
/*
 * Copyright 2005-2006 the Seasar Foundation and the Others.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
 * either express or implied. See the License for the specific language
 * governing permissions and limitations under the License.
 */
#endregion

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using log4net;
using Nullables;
using Seasar.Windows.Utils;
using Seasar.WindowsExample.Logics.Dto;
using Seasar.WindowsExample.Logics.Service;

namespace Seasar.WindowsExample.Forms
{
	/// <summary>
	/// �Ј����X�g�p�t�H�[��
	/// </summary>
	public class FrmEmployeeList : Form
	{
	    private Button btnNew;
	    private Label label2;
	    private Label label1;
	    private Button btnClose;
	    private Button btnOutput;
	    private SaveFileDialog dlgSave;
        private DataGrid gridList;

	    /// <summary>
		/// �K�v�ȃf�U�C�i�ϐ��ł��B
		/// </summary>
	    private Container components = null;

        /// <summary>
        /// �Ј����X�g�T�[�r�X
        /// </summary>
        IEmployeeListService _service;

	    /// <summary>
        /// ��ʃf�B�X�p�b�`���[
        /// </summary>
        IFormDispatcher _dispatcher;

        /// <summary>
        /// ���O(log4net)
        /// </summary>
        private static readonly ILog logger =
            LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// ��O�G���[���b�Z�[�W����
        /// </summary>
        private const string EXCEPTION_MSG_FORMAT = "�\���ł��Ȃ��G���[���������܂����B�ڍׂ��m�F���Ă��������B�i{0}�j";

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="dispatcher">��ʃf�B�X�p�b�`���[</param>
		public FrmEmployeeList(IFormDispatcher dispatcher)
		{
			//
			// Windows �t�H�[�� �f�U�C�i �T�|�[�g�ɕK�v�ł��B
			//
			InitializeComponent();

			//
			// TODO: InitializeComponent �Ăяo���̌�ɁA�R���X�g���N�^ �R�[�h��ǉ����Ă��������B
			//
            _dispatcher = dispatcher;

            // �O���b�h��ݒ�
            _InitGrid();
		}

		/// <summary>
		/// �g�p����Ă��郊�\�[�X�Ɍ㏈�������s���܂��B
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows �t�H�[�� �f�U�C�i�Ő������ꂽ�R�[�h 
		/// <summary>
		/// �f�U�C�i �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��B���̃��\�b�h�̓��e��
		/// �R�[�h �G�f�B�^�ŕύX���Ȃ��ł��������B
		/// </summary>
		private void InitializeComponent()
		{
            this.btnNew = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOutput = new System.Windows.Forms.Button();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.gridList = new System.Windows.Forms.DataGrid();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNew
            // 
            this.btnNew.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
            this.btnNew.Location = new System.Drawing.Point(160, 184);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(96, 40);
            this.btnNew.TabIndex = 7;
            this.btnNew.Text = "�V�K(&N)";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(255)), ((System.Byte)(192)));
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
            this.label2.Location = new System.Drawing.Point(24, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 24);
            this.label2.TabIndex = 6;
            this.label2.Text = "�Ј��ꗗ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(255)), ((System.Byte)(192)));
            this.label1.Location = new System.Drawing.Point(24, 184);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 40);
            this.label1.TabIndex = 5;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
            this.btnClose.Location = new System.Drawing.Point(368, 184);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(96, 40);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "����(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOutput
            // 
            this.btnOutput.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
            this.btnOutput.Location = new System.Drawing.Point(264, 184);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(96, 40);
            this.btnOutput.TabIndex = 9;
            this.btnOutput.Text = "�o��(&O)";
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // gridList
            // 
            this.gridList.DataMember = "";
            this.gridList.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.gridList.Location = new System.Drawing.Point(16, 16);
            this.gridList.Name = "gridList";
            this.gridList.ReadOnly = true;
            this.gridList.Size = new System.Drawing.Size(448, 152);
            this.gridList.TabIndex = 10;
            this.gridList.DoubleClick += new System.EventHandler(this.gridList_DoubleClick);
            // 
            // FrmEmployeeList
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
            this.ClientSize = new System.Drawing.Size(486, 237);
            this.Controls.Add(this.gridList);
            this.Controls.Add(this.btnOutput);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmEmployeeList";
            this.Text = "�Ј����X�g";
            this.Load += new System.EventHandler(this.FrmEmployeeList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).EndInit();
            this.ResumeLayout(false);

        }
		#endregion

        /// <summary>
        /// �Ј����X�g�T�[�r�X
        /// </summary>
	    public IEmployeeListService Service
	    {
	        get { return _service; }
	        set { _service = value; }
	    }

	    /// <summary>
        /// �t�H�[�������[�h�����Ƃ��̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmEmployeeList_Load(object sender, EventArgs e)
        {
            try
            {
                logger.InfoFormat("{0}�����[�h����܂���", Name);

                _ShowList();
            }
            catch (Exception ex)
            {
                logger.ErrorFormat(EXCEPTION_MSG_FORMAT, ex.Message);
                MessageBox.Show(String.Format(EXCEPTION_MSG_FORMAT, ex.Message), Text, 
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// �V�K�{�^�����������Ƃ��̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                _dispatcher.ShowDataEdit(null);

                _ShowList();
            }
            catch (Exception ex)
            {
                logger.ErrorFormat(EXCEPTION_MSG_FORMAT, ex.Message);
                MessageBox.Show(String.Format(EXCEPTION_MSG_FORMAT, ex.Message), Text, 
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// �o�̓{�^�����������Ƃ��̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOutput_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("�ۑ�����w�肵�Ă�������", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                _InitializeSaveDialog();

                if ( dlgSave.ShowDialog(this) == DialogResult.OK )
                {
                    if ( _service.OutputCSV(dlgSave.FileName) > 0 )
                        MessageBox.Show("�o�͂��܂���", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("�o�͂���f�[�^������܂���ł���", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {
                logger.ErrorFormat(EXCEPTION_MSG_FORMAT, ex.Message);
                MessageBox.Show(String.Format(EXCEPTION_MSG_FORMAT, ex.Message), Text, 
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// ����{�^�����������Ƃ��̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            logger.InfoFormat("{0}���I��", Name);
            Close();
        }

        /// <summary>
        /// �O���b�h���_�u���N���b�N�����Ƃ��̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridList_DoubleClick(object sender, System.EventArgs e)
        {
            try
            {
                DataSet ds = (DataSet)gridList.DataSource;
                DataRow row = ds.Tables[0].Rows[gridList.CurrentRowIndex];
                logger.Debug("ID:" + row["Id"]);
                
                NullableInt32 id = (NullableInt32) row["Id"];

                _dispatcher.ShowDataEdit(id);

                _ShowList();

            }
            catch ( Exception ex )
            {
                MessageBox.Show(this, ex.Message, Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// �O���b�h������������
        /// </summary>
        private void _InitGrid()
        {
            try
            {
                // Grid�ɕ\���p�e�[�u�����쐬����B
                // MappingName��DTO�̃v���p�e�B�����g�p���邱�ƁB
                // PONO���琶�������DataSet�ł�DTO�̃v���p�e�B�����g�p���Ă��邩��B

                DataGridTableStyle ts1 = new DataGridTableStyle();
                ts1.MappingName = typeof (EmployeeDto).Name;

                DataGridColumnStyle style1 = new DataGridTextBoxColumn();
                style1.MappingName = "Code";
                style1.HeaderText = "�R�[�h";
                style1.Width = 100;
                ts1.GridColumnStyles.Add(style1);

                DataGridColumnStyle style2 = new DataGridTextBoxColumn();
                style2.MappingName = "Name";
                style2.HeaderText = "���O ";
                style2.Width = 150;
                ts1.GridColumnStyles.Add(style2);

                DataGridColumnStyle style3 = new DataGridTextBoxColumn();
                style3.MappingName = "DeptName";
                style3.HeaderText = "���� ";
                style3.Width = 150;
                ts1.GridColumnStyles.Add(style3);

                gridList.TableStyles.Add(ts1);

                DataTable dt = new DataTable(typeof (EmployeeDto).Name);
                dt.Columns.Add(new DataColumn("Code"));
                dt.Columns.Add(new DataColumn("Name"));
                dt.Columns.Add(new DataColumn("DeptName"));

                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                gridList.SetDataBinding(ds, typeof (EmployeeDto).Name);
            }
            catch (Exception ex)
            {
                logger.ErrorFormat(EXCEPTION_MSG_FORMAT, ex.Message);
                MessageBox.Show(String.Format(EXCEPTION_MSG_FORMAT ,ex.Message), Text, 
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// Grid�ɕ\������
        /// </summary>
        private void _ShowList()
        {
            IList list = _service.GetAll();
            
            DataSet ds
                = Converter.ConvertPONOToDataSet(typeof ( EmployeeDto ), list);
            gridList.SetDataBinding(ds, typeof(EmployeeDto).Name);

        }

        /// <summary>
        /// �ۑ��_�C�A���O������������
        /// </summary>
        private void _InitializeSaveDialog()
        {
            dlgSave.DefaultExt = "*.csv";
            dlgSave.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            dlgSave.Title = "CSV�o��";
            dlgSave.Filter = "CSV�t�@�C�� (*.csv)|*.csv|���ׂẴt�@�C�� (*.*)|*.*";
            dlgSave.AddExtension = true;
            dlgSave.OverwritePrompt = true;
            dlgSave.FileName = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\�Ј��ꗗ.csv";
            dlgSave.RestoreDirectory = true;
        }
	}
}
