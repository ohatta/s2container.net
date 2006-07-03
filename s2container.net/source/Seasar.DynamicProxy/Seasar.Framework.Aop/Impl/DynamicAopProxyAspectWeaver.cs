using System;
using System.Collections;
using System.Collections.Generic;
using Seasar.Framework.Container;
using Seasar.Framework.Aop.Proxy;

namespace Seasar.Framework.Aop.Impl
{
    /// <summary>
    /// DynamicAopProxy��p����Aspect��D�荞�ޏ��������N���X
    /// </summary>
    public class DynamicAopProxyAspectWeaver : AbstractAspectWeaver
    {
        private IDictionary<IComponentDef, DynamicAopProxy> aopProxies =
            new Dictionary<IComponentDef, DynamicAopProxy>();

        /// <summary>
        /// AopProxy��p����Aspect��D�荞��
        /// </summary>
        /// <param name="target">Aspect��D�荞�ޑΏۂ̃I�u�W�F�N�g</param>
        /// <param name="componentDef">Aspect��D�荞�ޑΏۂ̃R���|�[�l���g��`</param>
        public override void WeaveAspect(ref object target, IComponentDef componentDef)
        {
            if (componentDef.AspectDefSize == 0) return;

            Type[] interfaces = componentDef.ComponentType.GetInterfaces();

            if (!componentDef.ComponentType.IsInterface)
            {
                DynamicAopProxy aopProxy = GetAopProxy(ref target, componentDef);
                target = aopProxy.Create();

            }
            else
            {
                this.AddProxy(ref target, componentDef, componentDef.ComponentType);
            }

            foreach (Type interfaceType in interfaces)
            {
                this.AddProxy(ref target, componentDef, interfaceType);
            }

        }

        /// <summary>
        /// Proxy���쐬����
        /// </summary>
        /// <param name="target">Aspect��D�荞�ޑΏۂ̃I�u�W�F�N�g</param>
        /// <param name="componentDef">Aspect��D�荞�ޑΏۂ̃R���|�[�l���g��`</param>
        /// <returns>Proxy</returns>
        protected DynamicAopProxy GetAopProxy(ref object target, IComponentDef componentDef)
        {
            DynamicAopProxy aopProxy = null;

            if (aopProxies.ContainsKey(componentDef))
            {
                aopProxy = aopProxies[componentDef];
            }
            else
            {
                Hashtable parameters = new Hashtable();
                parameters[ContainerConstants.COMPONENT_DEF_NAME] = componentDef;
                aopProxy = new DynamicAopProxy(componentDef.ComponentType,
                    GetAspects(componentDef), parameters, target);
                aopProxies[componentDef] = aopProxy;
            }

            return aopProxy;
        }

        /// <summary>
        /// �R���|�[�l���g��`��Proxy��ǉ�����
        /// </summary>
        /// <param name="target">Aspect��D�荞�ޑΏۂ̃I�u�W�F�N�g</param>
        /// <param name="componentDef">Aspect��D�荞�ޑΏۂ̃R���|�[�l���g��`</param>
        /// <param name="type">�R���|�[�l���g��`�ɒǉ�����Proxy��Type</param>
        protected void AddProxy(ref object target, IComponentDef componentDef, Type type)
        {
            DynamicAopProxy aopProxy = GetAopProxy(ref target, componentDef);

            componentDef.AddProxy(type, aopProxy.Create(type, target));
        }
    }
}
