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
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using log4net;
using Nullables;
using Seasar.WindowsExample.Logics.Dto;
using Seasar.WindowsExample.Logics.Service;

namespace Seasar.WindowsExample.Forms
{
	/// <summary>
	/// ����o�^�p�t�H�[��
	/// </summary>
	public class FrmDepartmentEdit : Form
	{
	    private Label label1;
	    private Label label2;
	    private Label label3;
	    private Label label4;
	    private Label label5;
	    private TextBox txtCode;
	    private TextBox txtName;
	    private Button btnUpdate;
	    private Button btnDelete;
	    private Button btnClose;
        private TextBox txtOrder;

        /// <summary>
        /// ���O(log4net)
        /// </summary>
        private static readonly ILog logger =
            LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// ����ID
        /// </summary>
        private NullableInt32 _id;

        /// <summary>
        /// ����o�^�p�T�[�r�X
        /// </summary>
	    IDepartmentEditService _service;

        /// <summary>
        /// ��O�G���[���b�Z�[�W����
        /// </summary>
        private const string EXCEPTION_MSG_FORMAT = "�\���ł��Ȃ��G���[���������܂����B�ڍׂ��m�F���Ă��������B�i{0}�j";

	    /// <summary>
		/// �K�v�ȃf�U�C�i�ϐ��ł��B
		/// </summary>
		private System.ComponentModel.Container components = null;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
		public FrmDepartmentEdit()
		{
			//
			// Windows �t�H�[�� �f�U�C�i �T�|�[�g�ɕK�v�ł��B
			//
			InitializeComponent();

			//
			// TODO: InitializeComponent �Ăяo���̌�ɁA�R���X�g���N�^ �R�[�h��ǉ����Ă��������B
			//
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtOrder = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
            this.label1.Location = new System.Drawing.Point(24, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "�R�[�h";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
            this.label2.Location = new System.Drawing.Point(24, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 24);
            this.label2.TabIndex = 4;
            this.label2.Text = "���喼";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(128)));
            this.label3.Location = new System.Drawing.Point(8, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(304, 40);
            this.label3.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(128)));
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
            this.label4.Location = new System.Drawing.Point(8, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(296, 24);
            this.label4.TabIndex = 1;
            this.label4.Text = "����";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
            this.label5.Location = new System.Drawing.Point(24, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 24);
            this.label5.TabIndex = 6;
            this.label5.Text = "�\������";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCode
            // 
            this.txtCode.AutoSize = false;
            this.txtCode.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
            this.txtCode.Location = new System.Drawing.Point(128, 80);
            this.txtCode.MaxLength = 4;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(80, 24);
            this.txtCode.TabIndex = 3;
            this.txtCode.Text = "9999";
            // 
            // txtName
            // 
            this.txtName.AutoSize = false;
            this.txtName.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
            this.txtName.Location = new System.Drawing.Point(128, 112);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(176, 24);
            this.txtName.TabIndex = 5;
            this.txtName.Text = "NNNNNNNNNNNNNNNNNN";
            // 
            // txtOrder
            // 
            this.txtOrder.AutoSize = false;
            this.txtOrder.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
            this.txtOrder.Location = new System.Drawing.Point(128, 144);
            this.txtOrder.MaxLength = 4;
            this.txtOrder.Name = "txtOrder";
            this.txtOrder.Size = new System.Drawing.Size(80, 24);
            this.txtOrder.TabIndex = 7;
            this.txtOrder.Text = "9999";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
            this.btnUpdate.Location = new System.Drawing.Point(16, 192);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(88, 40);
            this.btnUpdate.TabIndex = 8;
            this.btnUpdate.Text = "�o�^(&R)";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
            this.btnDelete.Location = new System.Drawing.Point(120, 192);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(88, 40);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "�폜(&D)";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
            this.btnClose.Location = new System.Drawing.Point(224, 192);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 40);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "����(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FrmDepartmentEdit
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
            this.ClientSize = new System.Drawing.Size(320, 253);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtOrder);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmDepartmentEdit";
            this.Text = "����";
            this.Load += new System.EventHandler(this.FrmDepartmentEdit_Load);
            this.ResumeLayout(false);

        }
		#endregion

        /// <summary>
        /// ����ID
        /// </summary>
        public NullableInt32 Id
	    {
	        get { return _id; }
	        set { _id = value; }
	    }

        /// <summary>
        /// ����o�^�p�T�[�r�X
        /// </summary>
        public IDepartmentEditService Service
        {
            get { return _service; }
            set { _service = value; }
        }

	    /// <summary>
        /// �t�H�[�������[�h�����Ƃ��̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmDepartmentEdit_Load(object sender, System.EventArgs e)
        {
            try
            {
                logger.InfoFormat("{0}�����[�h����܂���", Name);

                _InitializeControls();

                if ( _id.HasValue )
                {
                    DepartmentDto data = _service.GetData(_id.Value );
                    if ( data != null )
                    {
                        _ShowData(data);
                        btnDelete.Enabled = true;
                    }
                    else
                    {
                        throw new ApplicationException("����f�[�^��������܂���ł���");
                    }
                }
            }
            catch ( Exception ex )
            {
                logger.ErrorFormat(EXCEPTION_MSG_FORMAT, ex.Message);
                MessageBox.Show(String.Format(EXCEPTION_MSG_FORMAT, ex.Message), Text, 
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// �o�^�{�^�����������Ƃ��̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, System.EventArgs e)
        {
            try
            {
                if ( MessageBox.Show("�{���ɓo�^���܂����H", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                DepartmentDto data = new DepartmentDto();
                if ( !_SetInputData(data) ) return;

                if ( _service.ExecUpdate(data) > 0 )
                {
                    _InitializeControls();
                    MessageBox.Show("�o�^���܂���", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    throw new ApplicationException("�o�^�Ɏ��s���܂���");
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
        /// �폜�{�^�����������Ƃ��̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            try
            {
                if ( MessageBox.Show("�{���ɍ폜���܂����H", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                if ( _id.HasValue )
                {
                    if ( _service.ExecDelete(_id.Value) > 0 )
                    {
                        MessageBox.Show("�폜���܂���", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                    else
                    {
                        throw new ApplicationException("�폜�Ɏ��s���܂���");
                    }
                }
                else
                {
                    MessageBox.Show("�폜�Ώۂ�I��ł�������", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
        private void btnClose_Click(object sender, System.EventArgs e)
        {
            logger.InfoFormat("{0}���I��", Name);
            Close();
        }

        /// <summary>
        /// ���͂�����������
        /// </summary>
        private void _InitializeControls()
        {
            txtCode.Text = "";
            txtName.Text = "";
            txtOrder.Text = "0";

            btnUpdate.Enabled = true;
            btnDelete.Enabled = false;
        }

        /// <summary>
        /// ����f�[�^��\������
        /// </summary>
        /// <param name="data">����f�[�^</param>
        private void _ShowData(DepartmentDto data)
        {
            txtCode.Text = data.Code;
            txtName.Text = data.Name;
            txtOrder.Text = data.ShowOrder.ToString();
        }

        /// <summary>
        /// ���̓f�[�^���Z�b�g����
        /// </summary>
        /// <param name="data">�Z�b�g�����f�[�^</param>
        /// <returns>�o�^�̉�</returns>
        private bool _SetInputData(DepartmentDto data)
        {
            bool ret = true;

            // ����Id
            if ( _id.HasValue )
                data.Id = _id.Value;
            else
                data.Id = null;

            // ����R�[�h
            if (txtCode.Text != "")
            {
                if ( Regex.IsMatch(txtCode.Text, @"^\d{4}") )
                {
                    data.Code = txtCode.Text;
                }
                else
                {
                    MessageBox.Show("�R�[�h�ɐ����ȊO�̕���������܂�", Text, MessageBoxButtons.OK,  MessageBoxIcon.Exclamation);
                    ret = false;
                }
            }
            else
            {
                MessageBox.Show("�R�[�h����͂��Ă�������", Text, MessageBoxButtons.OK,  MessageBoxIcon.Exclamation);
                ret = false;
            }
            
            // ���喼
            if ( txtName.Text != "" )
                data.Name = txtName.Text;
            else
            {
                MessageBox.Show("���O����͂��Ă�������", Text, MessageBoxButtons.OK,  MessageBoxIcon.Exclamation);
                ret = false;
            }
            
            // �\������
            if ( txtOrder.Text != "" )
            {
                if ( Regex.IsMatch(txtOrder.Text, @"^\d{1,4}") )
                {
                    data.ShowOrder = Convert.ToInt32(txtOrder.Text);
                }
                else
                {
                    MessageBox.Show("�\�����Ԃɐ����ȊO�̕���������܂�", Text, MessageBoxButtons.OK,  MessageBoxIcon.Exclamation);
                    ret = false;
                }
            }
            else
            {
                MessageBox.Show("�\�����Ԃ���͂��Ă�������", Text, MessageBoxButtons.OK,  MessageBoxIcon.Exclamation);
                ret = false;
            }

            return ret;
        }
	}
}
