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
    /// �����Ŏw�肳�ꂽ<see cref="IDestroyMethodDef">destroy���\�b�h�E�C���W�F�N�V������`</see>
    /// ���s���������ꍇ�ɃX���[����܂��B
    /// </summary>
    /// <remarks>
    /// <para>
    /// �����Ŏw�肳�ꂽ���\�b�h�����݂��Ȃ��ꍇ�A������`����Ă���ꍇ�A
    /// ����ш������K�v�ȏꍇ�ɕs���Ƃ݂Ȃ���܂��B
    /// </para>
    /// </remarks>
    [Serializable]
    public class IllegalDestroyMethodAttributeRuntimeException : SRuntimeException
    {
        private Type componentType;
        private string methodName;

        /// <summary>
        /// <code>IllegalDestroyMethodAnnotationRuntimeException</code>���\�z���܂��B
        /// </summary>
        /// <param name="componentType">�������w�肳�ꂽ�N���X</param>
        /// <param name="methodName">�����Ŏw�肳�ꂽ���\�b�h��</param>
        public IllegalDestroyMethodAttributeRuntimeException(
            Type componentType, string methodName)
            : base("ESSR0082", new object[] { componentType.FullName, methodName })
        {
            this.componentType = componentType;
            this.methodName = methodName;
        }

        /// <summary>
        /// ��O�̌����ƂȂ����������w�肳�ꂽ�N���X��Ԃ��܂��B
        /// </summary>
        /// <value>�������w�肳�ꂽ�N���X</value>
        public Type ComponentType
        {
            get { return componentType; }
        }

        /// <summary>
        /// ��O�̌����ƂȂ��������Ŏw�肳�ꂽ���\�b�h����Ԃ��܂��B
        /// </summary>
        /// <value>�����Ŏw�肳�ꂽ���\�b�h��</value>
        public string MethodName
        {
            get { return methodName; }
        }
    }
}
