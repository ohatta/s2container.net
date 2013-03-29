#region Copyright
/*
* Copyright 2005-2013 the Seasar Foundation and the Others.
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
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Seasar.Framework.Aop.Proxy;
using Seasar.Framework.Container;
using Seasar.Framework.Util;

namespace Seasar.Framework.Aop.Impl
{
    /// <summary>
    /// DynamicAopProxy��p����Aspect��D�荞�ޏ��������N���X
    /// </summary>
    public class DynamicAopProxyAspectWeaver : AbstractAspectWeaver
    {
        private readonly IDictionary<IComponentDef, DynamicAopProxy> _aopProxies =
            new Dictionary<IComponentDef, DynamicAopProxy>();

        /// <summary>
        /// AopProxy��p����Aspect��D�荞��
        /// </summary>
        /// <param name="componentDef">Aspect��D�荞�ޑΏۂ̃R���|�[�l���g��`</param>
        /// <param name="constructor">�R���X�g���N�^</param>
        /// <param name="args">�R���X�g���N�^�̈���</param>
        /// <returns>Aspect��D�荞�񂾃I�u�W�F�N�g</returns>
        public override object WeaveAspect(IComponentDef componentDef, ConstructorInfo constructor, object[] args)
        {
            object target = null;
            if (componentDef.AspectDefSize == 0)
            {
                target = ConstructorUtil.NewInstance(constructor, args);
                return target;
            }

            if (!componentDef.ComponentType.IsInterface)
            {
                DynamicAopProxy aopProxy = GetAopProxy(target, componentDef);
                target = aopProxy.Create(Type.GetTypeArray(args), args);
            }
            else
            {
                target = new object();
                AddProxy(target, componentDef, componentDef.ComponentType);
            }

            Type[] interfaces = componentDef.ComponentType.GetInterfaces();

            foreach (Type interfaceType in interfaces)
            {
                AddProxy(target, componentDef, interfaceType);
            }

            return target;
        }

        /// <summary>
        /// �R���|�[�l���g��`��Proxy��ǉ�����
        /// </summary>
        /// <param name="target">Aspect��D�荞�ޑΏۂ̃I�u�W�F�N�g</param>
        /// <param name="componentDef">Aspect��D�荞�ޑΏۂ̃R���|�[�l���g��`</param>
        /// <param name="type">�R���|�[�l���g��`�ɒǉ�����Proxy��Type</param>
        protected void AddProxy(object target, IComponentDef componentDef, Type type)
        {
            DynamicAopProxy aopProxy = GetAopProxy(target, componentDef);

            componentDef.AddProxy(type, aopProxy.Create(type, target));
        }

        /// <summary>
        /// Proxy���쐬����
        /// </summary>
        /// <param name="target">Aspect��D�荞�ޑΏۂ̃I�u�W�F�N�g</param>
        /// <param name="componentDef">Aspect��D�荞�ޑΏۂ̃R���|�[�l���g��`</param>
        /// <returns>Proxy</returns>
        protected DynamicAopProxy GetAopProxy(object target, IComponentDef componentDef)
        {
            DynamicAopProxy aopProxy;

            if (_aopProxies.ContainsKey(componentDef))
            {
                aopProxy = _aopProxies[componentDef];
            }
            else
            {
                Hashtable parameters = new Hashtable();
                parameters[ContainerConstants.COMPONENT_DEF_NAME] = componentDef;
                aopProxy = new DynamicAopProxy(componentDef.ComponentType,
                    GetAspects(componentDef), parameters, target);
                _aopProxies[componentDef] = aopProxy;
            }

            return aopProxy;
        }
    }
}
