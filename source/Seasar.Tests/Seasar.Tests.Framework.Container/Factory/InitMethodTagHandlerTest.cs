#region Copyright
/*
 * Copyright 2005 the Seasar Foundation and the Others.
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
using NUnit.Framework;
using Seasar.Framework.Container;
using Seasar.Framework.Container.Factory;

namespace Seasar.Tests.Framework.Container.Factory
{
	/// <summary>
	/// InitMethodTagHandlerTest �̊T�v�̐����ł��B
	/// </summary>
	[TestFixture]
	public class InitMethodTagHandlerTest
	{
		private const string PATH 
            = "Seasar/Tests/Framework/Container/Factory/InitMethodTagHandlerTest.dicon";

		[Test]
		public void TestArg()
		{
			IS2Container container = S2ContainerFactory.Create(PATH);
			Hashtable aaa = (Hashtable) container.GetComponent("aaa");
			Assert.AreEqual(111, aaa["aaa"]);
			Assert.AreEqual(222, aaa["bbb"]);
			Bbb bbb = (Bbb) container.GetComponent("bbb");
			Assert.AreEqual(false, bbb.IsEmpty);
		}

		public class Bbb
		{
			private IList value_;

			public void Value(IList value)
			{
				value_ = value;
			}

			public bool IsEmpty
			{
				get { return value_ == null; }
			}
		}
	}
}
