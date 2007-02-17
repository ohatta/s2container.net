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
using System.Web.SessionState;
using Seasar.Framework.Container.Util;
using Seasar.Framework.Exceptions;

namespace Seasar.Framework.Container.Deployer
{
	/// <summary>
	/// SessionComponentDeployer �̊T�v�̐����ł��B
	/// </summary>
	public class SessionComponentDeployer : AbstractComponentDeployer
	{
		public SessionComponentDeployer(IComponentDef componentDef)
			: base(componentDef)
		{
		}

		public override object Deploy(Type receiveType)
		{
			IComponentDef cd = this.ComponentDef;
			HttpSessionState session = cd.Container.Root.Session;
			if(session == null)
			{
				throw new EmptyRuntimeException("session");
			}
			string componentName = cd.ComponentName;
			if(componentName == null)
			{
				throw new EmptyRuntimeException("componentName");
			}

            object component = session[componentName];

            if (component != null)
            {
                return component;
            }

            component = this.ConstructorAssembler.Assemble();

            object proxy = GetProxy(receiveType);

            if (proxy == null)
            {
                session[componentName] = component;
            }
            else
            {
                session[componentName] = proxy;
            }

            this.PropertyAssembler.Assemble(component);
            this.InitMethodAssembler.Assemble(component);

            if (proxy == null)
            {
                return component;
            }
            else
            {
                return proxy;
            }
		}

		public override void InjectDependency(object outerComponent)
		{
			throw new NotSupportedException("InjectDependency");
		}

		public override void Init()
		{
		}

		public override void Destroy()
		{
		}




	}
}
