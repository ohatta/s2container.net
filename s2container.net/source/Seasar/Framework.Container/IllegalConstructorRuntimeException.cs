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

namespace Seasar.Framework.Container
{
    /// <summary>
    /// �R���|�[�l���g�̍\�z�Ɏ��s�����ꍇ�ɃX���[����܂��B
    /// </summary>
    /// <remarks>
    /// <para>
    /// ���̗�O�́A �R���|�[�l���g��`�ŃR���X�g���N�^�̈����Ƃ���
    /// �w�肳�ꂽ�R���|�[�l���g�̎擾�Ɏ��s�����ꍇ�Ȃǂɔ������܂��B
    /// </para>
    /// </remarks>
    [Serializable]
    public class IllegalConstructorRuntimeException : SRuntimeException
    {
        private Type componentType;

        /// <summary>
        /// <code>IllegalConstructorRuntimeException</code>���\�z���܂��B
        /// </summary>
        /// <param name="componentType">�\�z�Ɏ��s�����R���|�[�l���g�̃N���X��Type</param>
        /// <param name="cause">�R���|�[�l���g�̍\�z�Ɏ��s����������\���G���[�܂��͗�O</param>
        public IllegalConstructorRuntimeException(Type componentType, Exception cause)
            : base("ESSR0058", new object[] { componentType.FullName, cause }, cause)
        {
            this.componentType = componentType;
        }

        /// <summary>
        /// �\�z�Ɏ��s�����R���|�[�l���g�̃N���X��Ԃ��܂��B
        /// </summary>
        /// <value>�\�z�Ɏ��s�����R���|�[�l���g�̃N���X</value>
        public Type ComponentType
        {
            get { return componentType; }
        }
    }
}
