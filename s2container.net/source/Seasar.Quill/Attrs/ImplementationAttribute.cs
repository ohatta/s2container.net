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

namespace Seasar.Quill.Attrs
{
    /// <summary>
    /// �����N���X���w�肷�鑮���N���X
    /// </summary>
    /// <remarks>
    /// �N���X�E�C���^�[�t�F�[�X�ɐݒ肷�邱�Ƃ��ł���B
    /// (�����ݒ肷�邱�Ƃ͂ł��Ȃ�)
    /// </remarks>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class,
       AllowMultiple = false)]
    public class ImplementationAttribute : Attribute
    {
        // �����N���X��Type
        protected Type implementationType;

        /// <summary>
        /// �������w�肳��Ă���N���X���g�������N���X�Ƃ���
        /// ImplementationAttribute������������R���X�g���N�^
        /// </summary>
        public ImplementationAttribute()
        {
        }

        /// <summary>
        /// �����N���X��Type���w�肵��ImplementationAttribute��
        /// ����������R���X�g���N�^
        /// </summary>
        /// <param name="implementationType">�����N���X��Type</param>
        public ImplementationAttribute(Type implementationType)
        {
            this.implementationType = implementationType;
        }

        /// <summary>
        /// �����N���X��Type��Ԃ�
        /// </summary>
        /// <value>�����N���X��Type</value>
        public Type ImplementationType
        {
            get { return implementationType; }
        }
    }
}
