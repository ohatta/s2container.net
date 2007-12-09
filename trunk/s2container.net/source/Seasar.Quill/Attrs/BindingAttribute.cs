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
    /// S2Container�̃R���|�[�l���g���o�C���f�B���O���邽�߂̑����N���X
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class BindingAttribute : Attribute
    {
        /// <summary>
        /// S2Container�ɂ�����R���|�[�l���g��
        /// </summary>
        protected string componentName;

        /// <summary>
        /// S2Container�ɂ�����R���|�[�l���g�����w�肵��
        /// BindingAttribute������������R���X�g���N�^
        /// </summary>
        /// <param name="componentName">S2Container�ɂ�����R���|�[�l���g��</param>
        public BindingAttribute(string componentName)
        {
            // �R���|�[�l���g�����Z�b�g����
            this.componentName = componentName;
        }

        /// <summary>
        /// S2Container�ɂ�����R���|�[�l���g�����擾����
        /// </summary>
        /// <value>S2Container�ɂ�����R���|�[�l���g��</value>
        public string ComponentName
        {
            get { return componentName; }
        }
    }
}
