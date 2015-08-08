#region Copyright
/*
 * Copyright 2005-2015 the Seasar Foundation and the Others.
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
using System.Windows.Forms;

namespace Seasar.Windows.Attr
{
    /// <summary>
    /// �R���g���[������
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=true)]
    public class ControlAttribute : Attribute
    {
#if NET_1_1
#else

#endif
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="controlName">�ΏۃR���g���[����</param>
        /// <param name="controlProperty">�ΏۃR���g���[���̃v���p�e�B</param>
        /// <param name="propertyName">�o�C���f�B���O�v���p�e�B��</param>
        public ControlAttribute(string controlName, string controlProperty, string propertyName)
        {
            ControlName = controlName;
            ControlProperty = controlProperty;
            PropertyName = propertyName;

#if NET_1_1
#else
            FormattingEnabled = true;
            UpdateMode = DataSourceUpdateMode.OnValidation;
            NullValue = null;
            FormatString = "";
#endif

        }

#if NET_1_1
#else

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="controlName">�ΏۃR���g���[����</param>
        /// <param name="controlProperty">�ΏۃR���g���[���̃v���p�e�B</param>
        /// <param name="propertyName">�o�C���f�B���O�v���p�e�B��</param>
        /// <param name="updateMode">�f�[�^�\�[�X�̍X�V�^�C�~���O</param>
        public ControlAttribute(string controlName, string controlProperty, string propertyName, DataSourceUpdateMode updateMode)
        {
            ControlName = controlName;
            ControlProperty = controlProperty;
            PropertyName = propertyName;
            FormattingEnabled = true;
            UpdateMode = updateMode;
            NullValue = null;
            FormatString = "";
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="controlName">�ΏۃR���g���[����</param>
        /// <param name="controlProperty">�ΏۃR���g���[���̃v���p�e�B</param>
        /// <param name="propertyName">�o�C���f�B���O�v���p�e�B��</param>
        /// <param name="formattingEnabled">�\�������f�[�^�̏������w�肷��ꍇ�� true�B����ȊO�̏ꍇ�� false</param>
        /// <param name="updateMode">�f�[�^�\�[�X�̍X�V�^�C�~���O</param>
        public ControlAttribute(string controlName, string controlProperty, string propertyName, bool formattingEnabled, DataSourceUpdateMode updateMode)
        {
            ControlName = controlName;
            ControlProperty = controlProperty;
            PropertyName = propertyName;
            FormattingEnabled = formattingEnabled;
            UpdateMode = updateMode;
            NullValue = null;
            FormatString = "";
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="controlName">�ΏۃR���g���[����</param>
        /// <param name="controlProperty">�ΏۃR���g���[���̃v���p�e�B</param>
        /// <param name="propertyName">�o�C���f�B���O�v���p�e�B��</param>
        /// <param name="formattingEnabled">�\�������f�[�^�̏������w�肷��ꍇ�� true�B����ȊO�̏ꍇ�� false</param>
        /// <param name="updateMode">�f�[�^�\�[�X�̍X�V�^�C�~���O</param>
        /// <param name="nullValue">�f�[�^ �\�[�X�̒l��DBNull�ł���ꍇ�ɁA�o�C���h���ꂽ�R���g���[���v���p�e�B�ɓK�p�����Object</param>
        /// <param name="formatString">�l�̕\�����@������1�ȏ�̏����w��q����</param>
        public ControlAttribute(string controlName, string controlProperty, string propertyName, bool formattingEnabled, DataSourceUpdateMode updateMode, 
            object nullValue, string formatString)
        {
            ControlName = controlName;
            ControlProperty = controlProperty;
            PropertyName = propertyName;
            FormattingEnabled = formattingEnabled;
            UpdateMode = updateMode;
            NullValue = nullValue;
            FormatString = formatString;
        }

#endif

        /// <summary>
        /// �ΏۃR���g���[����
        /// </summary>
        public string ControlName { get; set; }

        /// <summary>
        /// �ΏۃR���g���[���̃v���p�e�B
        /// </summary>
        public string ControlProperty { get; set; }

        /// <summary>
        /// �擾�v���p�e�B��
        /// </summary>
        public string PropertyName { get; set; }

#if NET_1_1
#else
        /// <summary>
        /// �\�������f�[�^�̏������w�肷��t���O
        /// </summary>
        public bool FormattingEnabled { get; set; }

        /// <summary>
        /// �f�[�^�\�[�X�̍X�V�^�C�~���O
        /// </summary>
        public DataSourceUpdateMode UpdateMode { get; set; }

        /// <summary>
        /// �f�[�^ �\�[�X�̒l��DBNull�ł���ꍇ�ɁA�o�C���h���ꂽ�R���g���[���v���p�e�B�ɓK�p�����Object
        /// </summary>
        public object NullValue { get; set; }

        /// <summary>
        /// �l�̕\�����@������1�ȏ�̏����w��q����
        /// </summary>
        public string FormatString { get; set; }

#endif

    }
}
