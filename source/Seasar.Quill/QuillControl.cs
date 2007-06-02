#region Copyright
/*
 * Copyright 2005-2007 the Seasar Foundation and the Others.
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Seasar.Quill.Util;

namespace Seasar.Quill
{
    /// <summary>
    /// �e�R���e�i��DI��L���ɂ���ׂ̃R���g���[���N���X
    /// </summary>
    public partial class QuillControl : UserControl, ISupportInitialize
    {
        /// <summary>
        /// QuillControl������������R���X�g���N�^
        /// </summary>
        public QuillControl()
        {
            // �f�t�H���g�Ŕ�\���̏�ԂƂ���
            this.Visible = false;

            // �R���|�[�l���g�̏������������s��
            InitializeComponent();
        }

        #region ISupportInitialize �����o

        /// <summary>
        /// �R���g���[���̏��������J�n�����ƌĂяo����郁�\�b�h
        /// </summary>
        public void BeginInit()
        {
        }

        /// <summary>
        /// �R���g���[���̏��������I������ƌĂяo����郁�\�b�h
        /// </summary>
        /// <remarks>
        /// QuillInjector���g�p����DI���s���B
        /// </remarks>
        public void EndInit()
        {
            if (DesignMode)
            {
                // �f�U�C�����[�h�̏ꍇ��DI�͍s��Ȃ�
                return;
            }

            Debug.WriteLine(MessageUtil.GetSimpleMessage("IQLL0001",
                new object[] { DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") } ));

            // QuilInjector�̃C���X�^���X���擾����
            QuillInjector injector = QuillInjector.GetInstance();

            // �e�R���e�i�ɑ΂���DI���s��
            injector.Inject(Parent);

            Debug.WriteLine(MessageUtil.GetSimpleMessage("IQLL0002",
                            new object[] { DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") }));
        }

        #endregion
    }
}
