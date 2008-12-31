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
using System.Collections;
using Seasar.Framework.Container;
using Seasar.Framework.Aop.Proxy;
using System.Reflection;
using Seasar.Framework.Util;

namespace Seasar.Framework.Aop.Impl
{
    /// <summary>
    /// AopProxy��p����Aspect��D�荞�ޏ��������N���X
    /// </summary>
    public class AopProxyAspectWeaver : AbstractAspectWeaver
    {
        /// <summary>
        /// AopProxy��p����Aspect��D�荞��
        /// </summary>
        /// <param name="componentDef">Aspect��D�荞�ޑΏۂ̃R���|�[�l���g��`</param>
        /// <param name="constructor">�R���X�g���N�^</param>
        /// <param name="args">�R���X�g���N�^�̈���</param>
        /// <returns>Aspect��D�荞�񂾃I�u�W�F�N�g</returns>
        public override object WeaveAspect(IComponentDef componentDef, ConstructorInfo constructor, object[] args)
        {
            object target;

            if (componentDef.ComponentType.IsInterface)
            {
                target = new object();
            }
            else
            {
                target = ConstructorUtil.NewInstance(constructor, args);
            }

            if (componentDef.AspectDefSize == 0)
            {
                return target;
            }

            Hashtable parameters = new Hashtable();
            parameters[ContainerConstants.COMPONENT_DEF_NAME] = componentDef;

            Type[] interfaces = componentDef.ComponentType.GetInterfaces();
            if (componentDef.ComponentType.IsMarshalByRef)
            {
                AopProxy aopProxy = new AopProxy(componentDef.ComponentType,
                    GetAspects(componentDef), parameters, target);
                componentDef.AddProxy(componentDef.ComponentType, aopProxy.Create());
            }
            else if (componentDef.ComponentType.IsInterface)
            {
                AopProxy aopProxy = new AopProxy(componentDef.ComponentType,
                    GetAspects(componentDef), parameters, target);
                target = aopProxy.Create();
            }
            foreach (Type interfaceType in interfaces)
            {
                AopProxy aopProxy = new AopProxy(interfaceType,
                    GetAspects(componentDef), parameters, target);
                componentDef.AddProxy(interfaceType, aopProxy.Create());
            }

            return target;
        }
    }
}
