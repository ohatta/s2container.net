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
    /// �R���|�[�l���g�̃C���X�^���X���A <see cref="IComponentDef">�R���|�[�l���g��`</see>
    /// �Ɏw�肳�ꂽ�N���X�ɃL���X�g�o���Ȃ��ꍇ�ɃX���[����܂��B
    /// </summary>
    /// <remarks>
    /// <para>
    /// <see cref="IComponentDef.Expression"/>�ŃC���X�^���X�̐������`���Ă���ꍇ�́A
    /// ���̃C���X�^���X���R���|�[�l���g��`�Ɏw�肳�ꂽ�N���X�ɃL���X�g�o���Ȃ����Ƃ�\���܂��B
    /// </para>
    /// <para>
    /// �O���R���|�[�l���g��<see cref="IS2Container.InjectDependency"/>�ȂǂŃC���W�F�N�V��������ꍇ�́A
    /// ���̃R���|�[�l���g���A �R���|�[�l���g��`�Ɏw�肳�ꂽ�N���X�ɃL���X�g�ł��Ȃ����Ƃ�\���܂��B
    /// </para>
    /// </remarks>
    /// <see cref="Seasar.Framework.Container.IConstructorAssembler.Assemble"/>
    /// <see cref="Seasar.Framework.Container.IS2Container.InjectDependency"/>
    [Serializable]
    public class ClassUnmatchRuntimeException : SRuntimeException
    {
        private Type componentType;
        private Type realComponentType;

        /// <summary>
        /// <code>ClassUnmatchRuntimeException</code>���\�z���܂��B
        /// </summary>
        /// <param name="componentType">�R���|�[�l���g��`�Ɏw�肳�ꂽ�N���X��Type</param>
        /// <param name="realComponentType">�R���|�[�l���g�̎��ۂ̌^</param>
        public ClassUnmatchRuntimeException(Type componentType, Type realComponentType)
            : base("ESSR0069", new object[] { componentType.FullName,
                realComponentType != null ? realComponentType.FullName : "null" })
        {
            this.componentType = componentType;
            this.realComponentType = realComponentType;
        }

        /// <summary>
        /// �R���|�[�l���g��`�Ɏw�肳�ꂽ�N���X��Type��Ԃ��܂��B
        /// </summary>
        /// <value>�R���|�[�l���g��`�Ɏw�肳�ꂽ�N���X��Type</value>
        public Type ComponentType
        {
            get { return componentType; }
        }

        /// <summary>
        /// �R���|�[�l���g�̎��ۂ̌^��Ԃ��܂��B
        /// </summary>
        /// <value>�R���|�[�l���g�̎��ۂ̌^</value>
        public Type RealComponentType
        {
            get { return realComponentType; }
        }
    }
}
