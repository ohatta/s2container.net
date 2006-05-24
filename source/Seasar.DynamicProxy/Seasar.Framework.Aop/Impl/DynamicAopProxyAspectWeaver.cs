using System;
using System.Collections;
using Seasar.Framework.Container;
using Seasar.Framework.Aop.Proxy;

namespace Seasar.Framework.Aop.Impl
{
    /// <summary>
    /// DynamicAopProxy��p����Aspect��D�荞�ޏ��������N���X
    /// </summary>
	public class DynamicAopProxyAspectWeaver : AbstractAspectWeaver
	{
        /// <summary>
        /// AopProxy��p����Aspect��D�荞��
        /// </summary>
        /// <param name="target">Aspect��D�荞�ޑΏۂ̃I�u�W�F�N�g</param>
        /// <param name="componentDef">Aspect��D�荞�ޑΏۂ̃R���|�[�l���g��`</param>
        public override void WeaveAspect(ref object target, Seasar.Framework.Container.IComponentDef componentDef)
        {
            if (componentDef.AspectDefSize == 0) return;
            Hashtable parameters = new Hashtable();
            parameters[ContainerConstants.COMPONENT_DEF_NAME] = componentDef;

            Type[] interfaces = componentDef.ComponentType.GetInterfaces();

            foreach (Type interfaceType in interfaces)
            {
                this.AddProxy(ref target, componentDef, interfaceType, parameters);
            }
            if (!componentDef.ComponentType.IsInterface)
            {
                this.AddProxy(ref target, componentDef, componentDef.ComponentType, parameters);
            }
        }

        /// <summary>
        /// �R���|�[�l���g��`��Proxy��ǉ�����
        /// </summary>
        /// <param name="target">Aspect��D�荞�ޑΏۂ̃I�u�W�F�N�g</param>
        /// <param name="componentDef">Aspect��D�荞�ޑΏۂ̃R���|�[�l���g��`</param>
        /// <param name="type">�R���|�[�l���g��`�ɒǉ�����Proxy��Type</param>
        /// <param name="parameters">�p�����[�^</param>
        protected void AddProxy(ref object target, IComponentDef componentDef, Type type, Hashtable parameters)
        {
            DynamicAopProxy aopProxy = new DynamicAopProxy(type,
                    GetAspects(componentDef), parameters, target);
            componentDef.AddProxy(type, aopProxy.Create());
        }
	}
}
