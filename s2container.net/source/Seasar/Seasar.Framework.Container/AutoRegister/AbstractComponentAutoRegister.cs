#region Copyright
/*
 * Copyright 2005-2009 the Seasar Foundation and the Others.
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
        private IAutoNaming autoNaming = new DefaultAutoNaming();
        private string instanceMode = ContainerConstants.INSTANCE_SINGLETON;
        private string autoBindingMode = ContainerConstants.AUTO_BINDING_AUTO;

        /// <summary>
        /// �R���|�[�l���g�Ɏ����I�ɖ��O��t����ׂ� AutoNaming ��ݒ�E�擾���܂��B
        /// </summary>
        public IAutoNaming AutoNaming
        {
            set { autoNaming = value; }
            get { return autoNaming; }
        }

        /// <summary>
        /// �C���X�^���X�̃��[�h��ݒ�E�擾���܂��B
        /// </summary>
        public string InstanceMode
        {
            set { instanceMode = value; }
            get { return instanceMode; }
        }

        /// <summary>
        /// �����o�C���f�B���O�̃��[�h��ݒ�E�擾���܂��B
        /// </summary>
        public string AutoBindingMode
        {
            set { autoBindingMode = value; }
            get { return autoBindingMode; }
        }

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

            for (int i = 0; i < ClassPatternSize; ++i)
            {
                ClassPattern cp = GetClassPattern(i);

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
            string componentName = autoNaming.DefineName(type);
            IComponentDef cd = new ComponentDefImpl(type, componentName);
            cd.InstanceMode = instanceMode;
            cd.AutoBindingMode = autoBindingMode;

            Container.Register(cd);
        }
    }
}
