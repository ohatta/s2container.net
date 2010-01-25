#region Copyright
/*
 * Copyright 2005-2010 the Seasar Foundation and the Others.
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

using Seasar.Framework.Container;
using System.Reflection;

namespace Seasar.Framework.Aop.Impl
{
    /// <summary>
    /// Aspect��D�荞�ޏ����������ۃN���X
    /// </summary>
    public abstract class AbstractAspectWeaver : IAspectWeaver
    {
        /// <summary>
        /// Aspect��D�荞��
        /// </summary>
        /// <param name="componentDef">Aspect��D�荞�ޑΏۂ̃R���|�[�l���g��`</param>
        /// <param name="constructor">�R���X�g���N�^</param>
        /// <param name="args">�R���X�g���N�^�̈���</param>
        /// <returns>Aspect��D�荞�񂾃I�u�W�F�N�g</returns>
        public abstract object WeaveAspect(IComponentDef componentDef,
            ConstructorInfo constructor, object[] args);

        /// <summary>
        /// �R���|�[�l���g��`�ɐݒ肳��Ă���Aspect���擾����
        /// </summary>
        /// <param name="componentDef">�R���|�[�l���g��`</param>
        /// <returns>Aspect�̔z��</returns>
        protected IAspect[] GetAspects(IComponentDef componentDef)
        {
            int size = componentDef.AspectDefSize;
            IAspect[] aspects = new IAspect[size];
            for (int i = 0; i < size; ++i)
            {
                aspects[i] = componentDef.GetAspectDef(i).Aspect;
            }
            return aspects;
        }

    }
}
