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
using System.Reflection;
using Seasar.Framework.Aop;
using Seasar.Framework.Aop.Impl;
using Seasar.Framework.Util;

namespace Seasar.Framework.Container.Util
{
    public sealed class AopProxyUtil
    {
        /// <summary>
        /// �f�t�H���g��AspectWeaver�C���X�^���X
        /// </summary>
        private static readonly IAspectWeaver DEFAULT_ASPECTWEAVER_INSTANCE = new AopProxyAspectWeaver();

        private AopProxyUtil()
        {
        }

        /// <summary>
        /// Aspect��D�荞��
        /// </summary>
        /// <param name="componentDef">Aspect��D�荞�ޑΏۂ̃R���|�[�l���g��`</param>
        /// <param name="constructor">�R���X�g���N�^</param>
        /// <param name="args">�R���X�g���N�^�̈���</param>
        /// <returns>Aspect��D�荞�񂾃I�u�W�F�N�g</returns>
        public static object WeaveAspect(IComponentDef componentDef, ConstructorInfo constructor, object[] args)
        {
            // S2�R���e�i���擾����
            IS2Container container = componentDef.Container;

            if (componentDef.ComponentType.FindInterfaces(new TypeFilter(TypeFilter), null).Length > 0)
            {
                return ConstructorUtil.NewInstance(constructor, args);
            }

            // S2�R���e�i��IAspectWeaver�����݂���ꍇ�́AS2�R���e�i����擾����
            // ���݂��Ȃ��ꍇ�́A�f�t�H���g��AopProxyAspectWeaver���g�p����
            IAspectWeaver aspectWeaver = container != null && container.HasComponentDef(typeof(IAspectWeaver)) ?
                (IAspectWeaver) container.GetComponent(typeof(IAspectWeaver)) : DEFAULT_ASPECTWEAVER_INSTANCE;

            // Aspect��D�荞��
            return aspectWeaver.WeaveAspect(componentDef, constructor, args);

        }

        public static bool TypeFilter(Type type, object filterCriteria)
        {
            return type == typeof(IAspectWeaver);
        }
    }
}
