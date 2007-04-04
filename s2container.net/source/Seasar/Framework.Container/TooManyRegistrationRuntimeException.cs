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
using Seasar.Framework.Exceptions;
using System.Text;

namespace Seasar.Framework.Container
{
    /// <summary>
    /// 1�̃L�[�ɕ����̃R���|�[�l���g���o�^����Ă����ꍇ�ɃX���[����܂��B
    /// </summary>
    /// <remarks>
    /// <para>
    /// S2�R���e�i����R���|�[�l���g���擾���悤�Ƃ����ۂɁA �w�肵���L�[(�R���|�[�l���g�̃N���X�A �C���^�[�t�F�[�X�A
    /// ���邢�͖��O)�ɊY������R���|�[�l���g��`���������݂����ꍇ�A ���̗�O���������܂��B
    /// </para>
    /// </remarks>
    [Serializable]
    public sealed class TooManyRegistrationRuntimeException : SRuntimeException
    {
        private object key;
        private Type[] componentTypes;

        /// <summary>
        /// <code>TooManyRegistrationRuntimeException</code>���\�z���܂��B
        /// </summary>
        /// <param name="key">�R���|�[�l���g���擾���悤�Ƃ����ۂɎg�p�����L�[</param>
        /// <param name="componentTypes">1�̃L�[�ɓo�^���ꂽ�����R���|�[�l���g��Type�̔z��</param>
        public TooManyRegistrationRuntimeException(object key, Type[] componentTypes)
            : base("ESSR0045", new object[] { key, GetTypeNames(componentTypes) })
        {
            this.key = key;
            this.componentTypes = componentTypes;
        }

        /// <summary>
        /// �R���|�[�l���g���擾���悤�Ƃ����ۂɎg�p�����L�[��Ԃ��܂��B
        /// </summary>
        /// <value>�R���|�[�l���g���擾���邽�߂̃L�[</value>
        public object Key
        {
            get { return key; }
        }

        /// <summary>
        /// 1�̃L�[�ɓo�^���ꂽ�����R���|�[�l���g��Type�̔z���Ԃ��܂��B
        /// </summary>
        /// <value>�R���|�[�l���g��Type�̔z��</value>
        public Type[] ComponentTypes
        {
            get { return componentTypes; }
        }

        private static string GetTypeNames(Type[] componentTypes)
        {
            StringBuilder buf = new StringBuilder(255);

            foreach (Type componentType in componentTypes)
            {
                if (componentType != null)
                {
                    buf.Append(componentType.FullName);
                }
                else
                {
                    buf.Append("<unknown>");
                }

                buf.Append(", ");
            }

            buf.Length -= 2;

            return buf.ToString();
        }
    }
}
