#region Copyright
/*
 * Copyright 2005-2015 the Seasar Foundation and the Others.
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
using Seasar.Framework.Container.Impl;

namespace Seasar.Framework.Container.AutoRegister
{
    /// <summary>
    /// �R���|�[�l���g�������o�^������N���X�ł��B
    /// </summary>
    public abstract class AbstractComponentAutoRegister : AbstractAutoRegister
    {
        /// <summary>
        /// �R���|�[�l���g�Ɏ����I�ɖ��O��t����ׂ� AutoNaming ��ݒ�E�擾���܂��B
        /// </summary>
        public IAutoNaming AutoNaming { set; get; } = new DefaultAutoNaming();

        /// <summary>
        /// �C���X�^���X�̃��[�h��ݒ�E�擾���܂��B
        /// </summary>
        public string InstanceMode { set; get; } = ContainerConstants.INSTANCE_SINGLETON;

        /// <summary>
        /// �����o�C���f�B���O�̃��[�h��ݒ�E�擾���܂��B
        /// </summary>
        public string AutoBindingMode { set; get; } = ContainerConstants.AUTO_BINDING_AUTO;

        /// <summary>
        /// Type �ɑ΂��Ď����o�^���邩�ǂ����̏������s���܂��B
        /// </summary>
        /// <param name="type">�����o�^���s�������f����Ώۂ� Type</param>
        public void ProcessType(Type type)
        {
            if (IsIgnore(type))
            {
                return;
            }

            for (var i = 0; i < ClassPatternSize; ++i)
            {
                var cp = GetClassPattern(i);

                if (cp.IsAppliedNamespaceName(type.Namespace)
                    && cp.IsAppliedShortClassName(type.Name))
                {
                    Register(type);
                    return;
                }
            }
        }

        /// <summary>
        /// �R���|�[�l���g��o�^���܂��B
        /// </summary>
        /// <param name="type">�o�^���� Type</param>
        public void Register(Type type)
        {
            var componentName = AutoNaming.DefineName(type);
            IComponentDef cd = new ComponentDefImpl(type, componentName);
            cd.InstanceMode = InstanceMode;
            cd.AutoBindingMode = AutoBindingMode;

            Container.Register(cd);
        }
    }
}
