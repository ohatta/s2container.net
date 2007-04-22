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
    /// �����o�C���f�B���O�̑ΏۂƂȂ�R���|�[�l���g��������Ȃ������ꍇ�ɃX���[����܂��B
    /// </summary>
    /// <remarks>
    /// <para>
    /// ���̗�O���X���[�����̂́A<see cref="IBindingTypeDef">�o�C���f�B���O�^�C�v��`</see>��
    /// <code>must</code>�Ŏ����o�C���f�B���O�̑Ώۂ�������Ȃ��������ł��B
    /// </para>
    /// </remarks>
    /// <seealso cref="IAutoBindingDef"/>
    /// <seealso cref="IBindingTypeDef"/>
    [Serializable]
    public class IllegalAutoBindingPropertyRuntimeException : SRuntimeException
    {
        private Type componentType;
        private string propertyName;

        public IllegalAutoBindingPropertyRuntimeException(Type componentType,
            string propertyName)
            : base("ESSR0080", new object[] { componentType.FullName, propertyName })
        {
            this.componentType = componentType;
            this.propertyName = propertyName;
        }

        /// <summary>
        /// �����o�C���f�B���O�Ɏ��s�����R���|�[�l���g�̃N���X��Type��Ԃ��܂��B
        /// </summary>
        /// <value>�����o�C���f�B���O�Ɏ��s�����R���|�[�l���g�̃N���X��Type</value>
        public Type ComponentType
        {
            get { return componentType; }
        }

        /// <summary>
        /// �����o�C���f�B���O�Ώۂ�������Ȃ������v���p�e�B�܂��̓t�B�[���h�̖��̂�Ԃ��܂��B
        /// </summary>
        /// <value>�����o�C���f�B���O�Ɏ��s�����v���p�e�B�܂��̓t�B�[���h�̖���</value>
        public string PropertyName
        {
            get { return propertyName; }
        }
    }
}
