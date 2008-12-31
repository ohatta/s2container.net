#region Copyright

/*
 * Copyright 2005-2009 the Seasar Foundation and the Others.
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
        /// <summary>
        /// �ΏۃR���g���[����
        /// </summary>
        private string _controlName;

        /// <summary>
        /// �ΏۃR���g���[���̃v���p�e�B
        /// </summary>
        private string _controlProperty;

        /// <summary>
        /// �擾�v���p�e�B��
        /// </summary>
        private string _propertyName;

#if NET_1_1
#else
        /// <summary>
        /// �\�������f�[�^�̏������w�肷��t���O
        /// </summary>
        private bool _formattingEnabled;

        /// <summary>
        /// �f�[�^�\�[�X�̍X�V�^�C�~���O
        /// </summary>
        private DataSourceUpdateMode _updateMode;

        /// <summary>
        /// �f�[�^ �\�[�X�̒l��DBNull�ł���ꍇ�ɁA�o�C���h���ꂽ�R���g���[���v���p�e�B�ɓK�p�����Object
        /// </summary>
        private object _nullValue;

        /// <summary>
        /// �l�̕\�����@������1�ȏ�̏����w��q����
        /// </summary>
        private string _formatString;

#endif
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="controlName">�ΏۃR���g���[����</param>
        /// <param name="controlProperty">�ΏۃR���g���[���̃v���p�e�B</param>
        /// <param name="propertyName">�o�C���f�B���O�v���p�e�B��</param>
        public ControlAttribute(string controlName, string controlProperty, string propertyName)
        {
            this._controlName = controlName;
            this._controlProperty = controlProperty;
            this._propertyName = propertyName;

#if NET_1_1
#else
            this._formattingEnabled = true;
            this._updateMode = DataSourceUpdateMode.OnValidation;
            this._nullValue = null;
            this._formatString = "";
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
            this._controlName = controlName;
            this._controlProperty = controlProperty;
            this._propertyName = propertyName;
            this._formattingEnabled = true;
            this._updateMode = updateMode;
            this._nullValue = null;
            this._formatString = "";
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
            this._controlName = controlName;
            this._controlProperty = controlProperty;
            this._propertyName = propertyName;
            this._formattingEnabled = formattingEnabled;
            this._updateMode = updateMode;
            this._nullValue = null;
            this._formatString = "";
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
            this._controlName = controlName;
            this._controlProperty = controlProperty;
            this._propertyName = propertyName;
            this._formattingEnabled = formattingEnabled;
            this._updateMode = updateMode;
            this._nullValue = nullValue;
            this._formatString = formatString;
        }

#endif

        /// <summary>
        /// �ΏۃR���g���[����
        /// </summary>
        public string ControlName
        {
            get { return _controlName; }
            set { _controlName = value; }
        }

        /// <summary>
        /// �ΏۃR���g���[���̃v���p�e�B
        /// </summary>
        public string ControlProperty
        {
            get { return _controlProperty; }
            set { _controlProperty = value; }
        }

        /// <summary>
        /// �擾�v���p�e�B��
        /// </summary>
        public string PropertyName
        {
            get { return _propertyName; }
            set { _propertyName = value; }
        }

#if NET_1_1
#else
        /// <summary>
        /// �\�������f�[�^�̏������w�肷��t���O
        /// </summary>
        public bool FormattingEnabled
        {
            get { return _formattingEnabled; }
            set { _formattingEnabled = value; }
        }

        /// <summary>
        /// �f�[�^�\�[�X�̍X�V�^�C�~���O
        /// </summary>
        public DataSourceUpdateMode UpdateMode
        {
            get { return _updateMode; }
            set { _updateMode = value; }
        }

        /// <summary>
        /// �f�[�^ �\�[�X�̒l��DBNull�ł���ꍇ�ɁA�o�C���h���ꂽ�R���g���[���v���p�e�B�ɓK�p�����Object
        /// </summary>
        public object NullValue
        {
            get { return _nullValue; }
            set { _nullValue = value; }
        }

        /// <summary>
        /// �l�̕\�����@������1�ȏ�̏����w��q����
        /// </summary>
        public string FormatString
        {
            get { return _formatString; }
            set { _formatString = value; }
        }

#endif

    }
}