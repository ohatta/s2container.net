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

            DynamicAopProxy aopProxy = new DynamicAopProxy(componentDef.ComponentType,
                GetAspects(componentDef), parameters, target);
            target = aopProxy.Create();
            componentDef.AddProxy(componentDef.ComponentType, target);
        }
	}
}
