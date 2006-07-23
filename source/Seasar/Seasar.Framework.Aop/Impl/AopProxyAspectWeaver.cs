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
