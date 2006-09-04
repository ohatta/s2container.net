#region Copyright
/*
 * Copyright 2005-2006 the Seasar Foundation and the Others.
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
using System.Web;
using Seasar.Framework.Container;
using Seasar.Framework.Container.Util;
using Seasar.Framework.Exceptions;
using Seasar.Framework.Log;
using Seasar.Framework.Util;

namespace Seasar.Framework.Container.Deployer
{
	/// <summary>
	/// RequestComponentDeployer の概要の説明です。
	/// </summary>
	public class RequestComponentDeployer : AbstractComponentDeployer
	{
		private static Logger logger_ = Logger.GetLogger(typeof(RequestComponentDeployer));

		public RequestComponentDeployer(IComponentDef componentDef)
			: base(componentDef)
		{
		}

		public override object Deploy(Type receiveType)
		{
			IComponentDef cd = this.ComponentDef;
			HttpContext context = cd.Container.Root.HttpContext;
			if(context == null)
			{
				ApplicationException ae = new EmptyRuntimeException("HttpContext");
				logger_.Log(ae);
				throw ae;
			}
			string componentName = cd.ComponentName;
			if(componentName == null)
			{
				componentName = cd.ComponentType.Name;
				componentName = StringUtil.Decapitalize(componentName);
			}
			object component = context.Items[componentName];
			if(component != null) return component;

			component = this.ConstructorAssembler.Assemble();
			context.Items[componentName] = component;
			this.PropertyAssembler.Assemble(component);
			this.InitMethodAssembler.Assemble(component);

			object proxy = GetProxy(receiveType);
			return proxy == null ? component : proxy;
		}

		public override void InjectDependency(object component)
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
