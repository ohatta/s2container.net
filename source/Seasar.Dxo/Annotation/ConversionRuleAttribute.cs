#region Copyright
/*
 * Copyright 2005-2013 the Seasar Foundation and the Others.
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

namespace Seasar.Dxo.Annotation
{
    /// <summary>
    /// DataExchange(DxO)�ɑ΂��āA����̃v���p�e�B�ɑ΂���ϊ����@���L�q����J�X�^������
    /// </summary>
    /// <seealso cref="Attribute"/>
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public sealed class ConversionRuleAttribute : Attribute
    {
        private string _propertyName;
        private string _targetPropertyName;
        private Type _propertyConverter;
        private bool _ignore;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="propertyName">�ϊ����J�X�^�}�C�Y����v���p�e�B��</param>
        /// <param name="targetPropertyName">�ϊ���ƂȂ�v���p�e�B��(�ȗ����A�ϊ����PropertyName�Ɠ���)</param>
        /// <param name="propertyConverter">�Ώۂ̃v���p�e�B��ϊ����邽�߂̃R���o�[�^�̌^</param>
        /// <param name="ignore">�^�ϊ����s��Ȃ����Ƃ��w������t���O</param>
        public ConversionRuleAttribute(string propertyName, string targetPropertyName, Type propertyConverter, bool ignore)
        {
            this._propertyName = propertyName;
            if (!string.IsNullOrEmpty(targetPropertyName))
                this._targetPropertyName = targetPropertyName;
            else
                this._targetPropertyName = propertyName;
            this._propertyConverter = propertyConverter;
            this._ignore = ignore;
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="propertyName">�ϊ����J�X�^�}�C�Y����v���p�e�B��</param>
        /// <param name="targetPropertyName">�ϊ���ƂȂ�v���p�e�B��(�ȗ����A�ϊ����PropertyName�Ɠ���)</param>
        /// <param name="propertyConverter">�Ώۂ̃v���p�e�B��ϊ����邽�߂̃R���o�[�^�̌^</param>
        public ConversionRuleAttribute(string propertyName, string targetPropertyName, Type propertyConverter)
        {
            this._propertyName = propertyName;
            if (!String.IsNullOrEmpty(targetPropertyName))
                this._targetPropertyName = targetPropertyName;
            else
                this._targetPropertyName = propertyName;
            this._propertyConverter = propertyConverter;
            this._ignore = false;
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="propertyName">�ϊ����J�X�^�}�C�Y����v���p�e�B��</param>
        /// <param name="targetPropertyName">�ϊ���ƂȂ�v���p�e�B��(�ȗ����A�ϊ����PropertyName�Ɠ���)</param>
        public ConversionRuleAttribute(string propertyName, string targetPropertyName)
        {
            this._propertyName = propertyName;
            if (!String.IsNullOrEmpty(targetPropertyName))
                this._targetPropertyName = targetPropertyName;
            else
                this._targetPropertyName = propertyName;
            _propertyConverter = null;
            _ignore = false;
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public ConversionRuleAttribute()
        {
            this._propertyName = String.Empty;
            this._targetPropertyName = String.Empty;
            this._propertyConverter = null;
            this._ignore = false;
        }

        /// <summary>
        /// �ϊ����J�X�^�}�C�Y����v���p�e�B��
        /// </summary>
        public string PropertyName
        {
            get { return _propertyName; }
            set { _propertyName = value; }
        }

        /// <summary>
        /// �ϊ���ƂȂ�v���p�e�B��
        /// (�ȗ������ꍇ�A�ϊ����PropertyName�Ɠ���Ƃ݂Ȃ���܂�)
        /// </summary>
        public string TargetPropertyName
        {
            get { return _targetPropertyName; }
            set { _targetPropertyName = value; }
        }

        /// <summary>
        /// �Ώۂ̃v���p�e�B��ϊ����邽�߂̃R���o�[�^�̌^
        /// (���̌^�̃f�t�H���g�R���X�g���N�^�ŁA�R���o�[�^�̃C���X�^���X�𐶐����邱�Ƃ�
        /// �ł��Ȃ��Ă͂Ȃ�܂���)
        /// </summary>
        public Type PropertyConverter
        {
            get { return _propertyConverter; }
            set { _propertyConverter = value; }
        }

        /// <summary>
        /// �v���p�e�B���Ƌ��Ɏw�肷�邱�ƂŁA�^�ϊ����s��Ȃ����Ƃ��w������t���O
        /// </summary>
        public bool Ignore
        {
            get { return _ignore; }
            set { _ignore = value; }
        }
    }
}
