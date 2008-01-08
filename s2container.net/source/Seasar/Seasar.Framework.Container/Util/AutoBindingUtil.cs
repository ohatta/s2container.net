#region Copyright
/*
 * Copyright 2005-2008 the Seasar Foundation and the Others.
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
using System.Reflection;
using Seasar.Framework.Container;

namespace Seasar.Framework.Container.Util
{
    public sealed class AutoBindingUtil
    {
        private AutoBindingUtil()
        {
        }

        public static bool IsSuitable(ParameterInfo[] parameters)
        {
            foreach (ParameterInfo parameter in parameters)
            {
                if (!IsSuitable(parameter.ParameterType))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsSuitable(Type type)
        {
            return type.IsInterface;
        }

        /// <summary>
        /// AutoBinding���L���ł��邩��Ԃ�
        /// </summary>
        /// <param name="propertyType">�v���p�e�B��Type</param>
        /// <param name="component">�v���p�e�B�����R���|�[�l���g</param>
        /// <param name="propertyName">�v���p�e�B��</param>
        /// <returns><see cref="System.Windows.Forms.Form"/>�N���X�̃v���p�e�B��
        /// (AcceptButton, CancelButton, Site)�̏ꍇ�͎����o�C���f�B���O�͖����Ƃ���B
        /// ����ȊO�̏ꍇ��propertyType��Interface�^�ł���ΗL���Ƃ���B</returns>
        public static bool IsSuitable(Type propertyType, object component, string propertyName)
        {
            if (component is System.Windows.Forms.Form
                    && ("AcceptButton".Equals(propertyName)
                        || "CancelButton".Equals(propertyName)
                        || "Site".Equals(propertyName)))
            {
                return false;
            }
            else
            {
                return IsSuitable(propertyType);
            }
        }

        public static bool IsAuto(string mode)
        {
            return ContainerConstants.AUTO_BINDING_AUTO.ToLower().Equals(mode.ToLower());
        }

        public static bool IsConstructor(string mode)
        {
            return ContainerConstants.AUTO_BINDING_CONSTRUCTOR.ToLower().Equals(mode.ToLower());
        }

        public static bool IsProperty(string mode)
        {
            return ContainerConstants.AUTO_BINDING_PROPERTY.ToLower().Equals(mode.ToLower());
        }

        public static bool IsNone(string mode)
        {
            return ContainerConstants.AUTO_BINDING_NONE.ToLower().Equals(mode.ToLower());
        }
    }
}
