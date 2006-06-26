#region Copyright

/*
 * Copyright 2006 the Seasar Foundation and the Others.
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
        private Type type_;

        /// <summary>
        /// Window�\�����[�h�^�C�v
        /// </summary>
        private ModalType mode_ = ModalType.Modaless;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="type">WindowsForm�N���X</param>
        /// <param name="mode">Window�\�����[�h</param>
        public TargetFormAttribute(Type type, ModalType mode)
        {
            type_ = type;
            mode_ = mode;
        }

        /// <summary>
        /// WindowsForm�N���X
        /// </summary>
        public Type FormType
        {
            get { return type_; }
        }

        /// <summary>
        /// Window�\�����[�h
        /// </summary>
        public ModalType Mode
        {
            get { return mode_; }
        }
    }
}