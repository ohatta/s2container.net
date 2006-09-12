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
	/// ���僊�X�g�p�t�H�[��
	/// </summary>
	public class FrmDepartmentList : Form
	{
	    private Button btnClose;
	    private Label label1;
	    private Label label2;
	    private DataGrid gridList;
	    private Button btnNew;

        /// <summary>
        /// ���O(log4net)
        /// </summary>
        private static readonly ILog logger =
            LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// ���僊�X�g�T�[�r�X
        /// </summary>
        private IDepartmentListService _service;

        /// <summary>
        /// ��ʃf�B�X�p�b�`���[
        /// </summary>
        private IFormDispatcher _dispatcher;

        /// <summary>
        /// ��O�G���[���b�Z�[�W����
        /// </summary>
        private const string EXCEPTION_MSG_FORMAT = "�\���ł��Ȃ��G���[���������܂����B�ڍׂ��m�F���Ă��������B�i{0}�j";

        /// <summary>
		/// �K�v�ȃf�U�C�i�ϐ��ł��B
		/// </summary>
        private Container components = null;

	    /// <summary>
        /// �R���X�g���N�^
        /// </summary>
		public FrmDepartmentList(IFormDispatcher dispatcher)
		{
			//
			// Windows �t�H�[�� �f�U�C�i �T�|�[�g�ɕK�v�ł��B
			//
			InitializeComponent();

			//
			// TODO: InitializeComponent �Ăяo���̌�ɁA�R���X�g���N�^ �R�[�h��ǉ����Ă��������B
			//

            _dispatcher = dispatcher;

            // �O���b�h������������
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
            this.btnClose = new System.Windows.Forms.Button();
            this.gridList = new System.Windows.Forms.DataGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnNew = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
            this.btnClose.Location = new System.Drawing.Point(368, 216);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(96, 40);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "����(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // gridList
            // 
            this.gridList.DataMember = "";
            this.gridList.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.gridList.Location = new System.Drawing.Point(24, 16);
            this.gridList.Name = "gridList";
            this.gridList.ReadOnly = true;
            this.gridList.Size = new System.Drawing.Size(440, 184);
            this.gridList.TabIndex = 0;
            this.gridList.DoubleClick += new System.EventHandler(this.gridList_DoubleClick);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(192)));
            this.label1.Location = new System.Drawing.Point(24, 216);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 40);
            this.label1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(192)));
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
            this.label2.Location = new System.Drawing.Point(24, 224);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(216, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "����ꗗ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnNew
            // 
            this.btnNew.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
            this.btnNew.Location = new System.Drawing.Point(264, 216);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(96, 40);
            this.btnNew.TabIndex = 3;
            this.btnNew.Text = "�V�K(&N)";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // FrmDepartmentList
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
            this.ClientSize = new System.Drawing.Size(488, 269);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gridList);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmDepartmentList";
            this.Text = "���僊�X�g";
            this.Load += new System.EventHandler(this.FrmDepartmentList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).EndInit();
            this.ResumeLayout(false);

        }
		#endregion

        /// <summary>
        /// ���僊�X�g�T�[�r�X
        /// </summary>
	    public IDepartmentListService Service
	    {
	        get { return _service; }
	        set { _service = value; }
	    }

	    /// <summary>
        /// �t�H�[�������[�h�����Ƃ��̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmDepartmentList_Load(object sender, System.EventArgs e)
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
        private void btnNew_Click(object sender, System.EventArgs e)
        {
            try
            {
                _dispatcher.ShowMasterEdit(null);
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
        /// ����{�^�����������Ƃ��̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, System.EventArgs e)
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

                _dispatcher.ShowMasterEdit(id);

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
	            ts1.MappingName = typeof (DepartmentDto).Name;

	            DataGridColumnStyle style1 = new DataGridTextBoxColumn();
	            style1.MappingName = "Code";
	            style1.HeaderText = "�R�[�h";
	            style1.Width = 100;
	            ts1.GridColumnStyles.Add(style1);

	            DataGridColumnStyle style2 = new DataGridTextBoxColumn();
	            style2.MappingName = "Name";
	            style2.HeaderText = "���O ";
	            style2.Width = 230;
	            ts1.GridColumnStyles.Add(style2);

	            gridList.TableStyles.Add(ts1);

	            DataTable dt = new DataTable(typeof (DepartmentDto).Name);
	            dt.Columns.Add(new DataColumn("Code"));
	            dt.Columns.Add(new DataColumn("Name"));

	            DataSet ds = new DataSet();
	            ds.Tables.Add(dt);
	            gridList.SetDataBinding(ds, typeof (DepartmentDto).Name);
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
                = Converter.ConvertPONOToDataSet(typeof ( DepartmentDto ), list);
            gridList.SetDataBinding(ds, typeof(DepartmentDto).Name);

        }
	}
}
