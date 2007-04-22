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
    /// �s���ȃ��\�b�h�E�C���W�F�N�V������`���w�肳��Ă����ꍇ�ɃX���[����܂��B
    /// </summary>
    /// <remarks>
    /// <para>
    /// ���\�b�h�E�C���W�F�N�V���������s�����ۂɁA���\�b�h�̈����Ƃ��Ďw�肳�ꂽ�R���|�[�l���g��
    /// ������Ȃ��ꍇ��A������K�؂Ȍ^�ɕϊ��o���Ȃ��ꍇ�Ȃǂɔ������܂��B
    /// </para>
    /// </remarks>
    /// <seealso cref="IMethodDef"/>
    /// <seealso cref="IInitMethodDef"/>
    /// <seealso cref="IDestroyMethodDef"/>
    /// <seealso cref="IMethodAssembler"/>
    /// <seealso cref="Seasar.Framework.Container.Assembler.AbstraceMethodAssembler"/>
    [Serializable]
    public class IllegalMethodRuntimeException : SRuntimeException
    {
        private Type componentType;
        private string methodName;

        public IllegalMethodRuntimeException(Type componentType,
            string methodName, Exception cause)
            : base("ESSR0060", new object[] { componentType.FullName, 
                methodName, cause }, cause)
        {
            this.componentType = componentType;
            this.methodName = methodName;
        }

        /// <summary>
        /// �s���ȃ��\�b�h�E�C���W�F�N�V������`���܂ރR���|�[�l���g�̃N���X��Type��Ԃ��܂��B
        /// </summary>
        /// <value>�R���|�[�l���g�̃N���X��Type</value>
        public Type ComponentType
        {
            get { return componentType; }
        }

        /// <summary>
        /// �s���ȃ��\�b�h�E�C���W�F�N�V������`�̃��\�b�h����Ԃ��܂��B
        /// </summary>
        /// <value>���\�b�h��</value>
        public string MethodName
        {
            get { return methodName; }
        }
    }
}
