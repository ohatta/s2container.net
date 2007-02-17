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
using System.Web;

namespace Seasar.Framework.Container.Impl
{
	/// <summary>
	/// RequestComponentDef �̊T�v�̐����ł��B
	/// </summary>
	public class RequestComponentDef : SimpleComponentDef
	{
		private IS2Container container_;

		public RequestComponentDef(IS2Container container)
			: base(typeof(HttpRequest))
		{
			container_ = container;
		}

		public IS2Container Root
		{
			get { return container_.Root; }
		}

		public override object GetComponent()
		{
			return this.Root.Request;
		}
	}
}
