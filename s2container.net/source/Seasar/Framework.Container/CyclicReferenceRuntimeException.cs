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
    /// �R���|�[�l���g�̏z�Q�Ƃ����������ꍇ�ɃX���[����܂��B
    /// </summary>
    /// <remarks>
    /// <para>�R���|�[�l���g�̃R���X�g���N�^�����ɁA �����R���|�[�l���g���w�肵���ꍇ�Ȃǂɔ������܂��B</para>
    /// </remarks>
    [Serializable]
    public class CyclicReferenceRuntimeException : SRuntimeException
    {
        private Type componentType;

        /// <summary>
        /// �z�Q�Ƃ������N�������R���|�[�l���g�̃N���X���w�肵�āA
        /// <code>CyclicReferenceRuntimeException</code>���\�z���܂��B
        /// </summary>
        /// <param name="componentType">�z�Q�Ƃ������N�������R���|�[�l���g�̃N���X��Type</param>
        public CyclicReferenceRuntimeException(Type componentType)
            : base("ESSR0047", new object[] { componentType.FullName })
        {
            this.componentType = componentType;
        }

        /// <summary>
        /// �z�Q�Ƃ������N�������R���|�[�l���g�̃N���X��Type��Ԃ��܂��B
        /// </summary>
        /// <value>�z�Q�Ƃ������N�������R���|�[�l���g�̃N���X��Type</value>
        public Type ComponentType
        {
            get { return componentType; }
        }
    }
}
