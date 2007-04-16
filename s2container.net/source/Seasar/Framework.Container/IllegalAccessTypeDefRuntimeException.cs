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
    /// �s���ȃA�N�Z�X�^�C�v��`���w�肳�ꂽ�ꍇ�ɃX���[����܂��B
    /// </summary>
    /// <remarks>
    /// <para>
    /// �L����<see cref="IAccessTypeDef">�A�N�Z�X�^�C�v��`</see>�Ƃ��ẮA
    /// <see cref="Seasar.Framework.Container.Assembler.AccessTypePropertyDef">PROPERTY</see>��
    /// <see cref="Seasar.Framework.Container.Assembler.AccessTypeFieldDef">FIELD</see>������܂��B
    /// </para>
    /// </remarks>
    /// <seealso cref="Seasar.Framework.Container.Assembler.AccessTypeDefFactory.GetAccessTypeDef"/>
    [Serializable]
    public class IllegalAccessTypeDefRuntimeException : SRuntimeException
    {
        private string accessTypeName;

        /// <summary>
        /// �s���ȃA�N�Z�X�^�C�v��`�����w�肵�āA
        /// <code>IllegalAccessTypeDefRuntimeException</code>���\�z���܂��B
        /// </summary>
        /// <param name="accessTypeName">�s���ȃA�N�Z�X�^�C�v��`��</param>
        public IllegalAccessTypeDefRuntimeException(string accessTypeName)
            : base("ESSR0083", new object[] { accessTypeName })
        {
            this.accessTypeName = accessTypeName;
        }

        /// <summary>
        /// �s���ȃA�N�Z�X�^�C�v��`����Ԃ��܂��B
        /// </summary>
        /// <value>�s���ȃA�N�Z�X�^�C�v��`��</value>
        /// <seealso cref="IAccessTypeDefConstants.PROPERTY_NAME"/>
        /// <seealso cref="IAccessTypeDefConstants.FIELD_NAME"/>
        public string AccessTypeName
        {
            get { return accessTypeName; }
        }
    }
}
