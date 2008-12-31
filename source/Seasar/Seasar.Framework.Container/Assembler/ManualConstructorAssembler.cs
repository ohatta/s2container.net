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
using System.Reflection;
using Seasar.Framework.Beans;
using Seasar.Framework.Container.Util;

namespace Seasar.Framework.Container.Assembler
{
    public class ManualConstructorAssembler : AbstractConstructorAssembler
    {
        public ManualConstructorAssembler(IComponentDef componentDef)
            : base(componentDef)
        {
        }

        public override object Assemble()
        {
            object[] args = new object[ComponentDef.ArgDefSize];

            for (int i = 0; i < args.Length; ++i)
            {
                try
                {
                    args[i] = ComponentDef.GetArgDef(i).Value;
                }
                catch (ComponentNotFoundRuntimeException cause)
                {
                    throw new IllegalConstructorRuntimeException(ComponentDef.ComponentType, cause);
                }
            }

            ConstructorInfo constructor =
                ComponentDef.ComponentType.GetConstructor(
                Type.GetTypeArray(args));

            if (constructor == null)
            {
                throw new ConstructorNotFoundRuntimeException(
                    ComponentDef.ComponentType, args);
            }

            ParameterInfo[] parameters = constructor.GetParameters();

            for (int i = 0; i < args.Length; ++i)
            {
                IArgDef argDef = ComponentDef.GetArgDef(i);
                object value = GetComponentByReceiveType(parameters[i].ParameterType, argDef.Expression);
                if (value != null)
                {
                    args[i] = value;
                }
            }

            return AopProxyUtil.WeaveAspect(ComponentDef, constructor, args);
        }
    }
}
