#region Copyright
/*
 * Copyright 2005-2010 the Seasar Foundation and the Others.
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

namespace Seasar.Windows.Attr
{
    /// <summary>
    /// Window�̕\�����[�h�^�C�v
    /// </summary>
    public enum ModalType
    {
        Modal,
        Modaless
    }

    /// <summary>
    /// TargetForm����
    /// </summary>
    /// <remarks>�\������WindowsForm���w�肷��</remarks>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TargetFormAttribute : Attribute
    {
        /// <summary>
        /// WindowsForm�N���X
        /// </summary>
        private readonly Type _type;

        /// <summary>
        /// Window�\�����[�h�^�C�v
        /// </summary>
        private readonly ModalType _mode = ModalType.Modaless;


        /// <summary>
        /// �Ԃ�l�p�v���p�e�B��
        /// </summary>
        private string _returnPropertyName;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="type">WindowsForm�N���X</param>
        /// <param name="mode">Window�\�����[�h</param>
        /// <param name="returnPropetyName">�Ԃ�l�p�v���p�e�B��</param>
        public TargetFormAttribute(Type type, ModalType mode, string returnPropetyName)
        {
            _type = type;
            _mode = mode;
            _returnPropertyName = returnPropetyName;
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="type">WindowsForm�N���X</param>
        /// <param name="mode">Window�\�����[�h</param>
        public TargetFormAttribute(Type type, ModalType mode)
        {
            _type = type;
            _mode = mode;
            _returnPropertyName = string.Empty;
        }

        /// <summary>
        /// WindowsForm�N���X
        /// </summary>
        public Type FormType
        {
            get { return _type; }
        }

        /// <summary>
        /// Window�\�����[�h
        /// </summary>
        public ModalType Mode
        {
            get { return _mode; }
        }

        /// <summary>
        /// �Ԃ�l�p�v���p�e�B��
        /// </summary>
        public string ReturnPropertyName
        {
            get { return _returnPropertyName; }
            set { _returnPropertyName = value; }
        }
    }
}
