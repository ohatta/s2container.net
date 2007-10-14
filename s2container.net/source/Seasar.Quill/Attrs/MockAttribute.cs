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
    /// Mock���w�肷�鑮���N���X
    /// </summary>
    /// <remarks>
    /// �C���^�[�t�F�[�X�ɐݒ肷�邱�Ƃ��ł���B�i�����ݒ肷�邱�Ƃ͂ł��Ȃ��j
    /// </remarks>
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
    public class MockAttribute : Attribute
    {
        // Mock�N���X��Type
        protected Type mockType;

        /// <summary>
        /// Mock�N���X��Type���w�肵��MockAttribute��
        /// ����������R���X�g���N�^
        /// </summary>
        /// <param name="mockType">Mock�N���X��Type</param>
        public MockAttribute(Type mockType)
        {
            this.mockType = mockType;
        }

        /// <summary>
        /// Mock�N���X��Type��Ԃ�
        /// </summary>
        /// <value>Mock�N���X��Type</value>
        public Type MockType
        {
            get { return mockType; }
        }
    }
}