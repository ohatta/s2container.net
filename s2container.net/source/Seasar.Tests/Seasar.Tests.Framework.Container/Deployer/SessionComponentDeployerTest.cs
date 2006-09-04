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
using System.IO;
using System.Text;
using System.Web;
using System.Web.Hosting;
using NUnit.Framework;

namespace Seasar.Tests.Framework.Container.Deployer
{
	/// <summary>
	/// SessionComponentDeployerTest �̊T�v�̐����ł��B
	/// </summary>
	[TestFixture]
	public class SessionComponentDeployerTest
	{
		[Test]
		public void TestDeployAutoAutoConstructor()
		{
			MockWebHost host = (MockWebHost) ApplicationHost.CreateApplicationHost(
				typeof(MockWebHost), "/test", Environment.CurrentDirectory);
			Assert.AreEqual("<span id=\"ResultLabel\"></span>", host.Process());
		}

		public class MockWebHost : MarshalByRefObject
		{
			public string Process()
			{
				StringBuilder sb = new StringBuilder();
				TextWriter writer = new StringWriter(sb);
				SimpleWorkerRequest workerRequest = new SimpleWorkerRequest(
					"SessionComponentDeployerWebPage.aspx", "", writer);
				HttpRuntime.ProcessRequest(workerRequest);
				return sb.ToString();
			}
		}
	}
}
