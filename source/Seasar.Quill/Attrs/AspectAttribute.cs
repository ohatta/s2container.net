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
    /// Aspect���w�肷�鑮���N���X
    /// </summary>
    /// <remarks>
    /// �N���X�E�C���^�[�t�F�[�X�E���\�b�h�ɐݒ肷�邱�Ƃ��ł���B
    /// (�����ݒ肷�邱�Ƃ��ł���)
    /// </remarks>
    [AttributeUsage(AttributeTargets.Interface |
       AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class AspectAttribute : Attribute
    {
        /// <summary>
        /// Interceptor��Type
        /// </summary>
        protected Type interceptorType;

        /// <summary>
        /// S2Container�ɂ�����R���|�[�l���g��
        /// </summary>
        protected string componentName;

        /// <summary>
        /// Aspect��K�p���鏇��(�����������ق�����ɓK�p�����)
        /// </summary>
        protected int ordinal = 0;

        /// <summary>
        /// Interceptor��Type���w�肵��AspectAttribute������������R���X�g���N�^
        /// </summary>
        /// <param name="interceptorType">Interceptor��Type</param>
        public AspectAttribute(Type interceptorType)
        {
            // Interceptor��Type��ݒ肷��
            this.interceptorType = interceptorType;
        }

        /// <summary>
        /// Interceptor��Type���w�肵��AspectAttribute������������R���X�g���N�^
        /// </summary>
        /// <param name="interceptorType">Interceptor��Type</param>
        /// <param name="ordinal">Aspect��K�p���鏇��</param>
        public AspectAttribute(Type interceptorType, int ordinal)
        {
            // Interceptor��Type��ݒ肷��
            this.interceptorType = interceptorType;

            // Aspect��K�p���鏇�Ԃ�ݒ肷��
            this.ordinal = ordinal;
        }

        /// <summary>
        /// Interceptor��S2Container�ɂ�����R���|�[�l���g�����w�肵��
        /// AspectAttribute������������R���X�g���N�^
        /// </summary>
        /// <param name="componentName">S2Container�ɂ�����R���|�[�l���g��</param>
        public AspectAttribute(string componentName)
        {
            // S2Container�ɂ�����R���|�[�l���g����ݒ肷��
            this.componentName = componentName;
        }

        /// <summary>
        /// Interceptor��S2Container�ɂ�����R���|�[�l���g�����w�肵��
        /// AspectAttribute������������R���X�g���N�^
        /// </summary>
        /// <param name="componentName">S2Container�ɂ�����R���|�[�l���g��</param>
        /// <param name="ordinal">Aspect��K�p���鏇��</param>
        public AspectAttribute(string componentName, int ordinal)
        {
            // S2Container�ɂ�����R���|�[�l���g����ݒ肷��
            this.componentName = componentName;

            // Aspect��K�p���鏇�Ԃ�ݒ肷��
            this.ordinal = ordinal;
        }

        /// <summary>
        /// Interceptor��Type���擾����
        /// </summary>
        /// <value>Interceptor��Type</value>
        public Type InterceptorType
        {
            get { return interceptorType; }
        }

        /// <summary>
        /// S2Container�ɂ�����R���|�[�l���g�����擾����
        /// </summary>
        /// <value>S2Container�ɂ�����R���|�[�l���g��</value>
        public string ComponentName
        {
            get { return componentName; }
        }

        /// <summary>
        /// Aspect��K�p���鏇�Ԃ��擾����
        /// (�����������ق�����ɓK�p�����)
        /// </summary>
        /// <value>Aspect��K�p���鏇��(�����������ق�����ɓK�p�����)</value>
        public int Ordinal
        {
            get { return ordinal; }
        }
    }
}
