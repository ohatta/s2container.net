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
using System.Reflection;
using System.Resources;

using NUnit.Framework;

using Seasar.Framework.Message;

namespace TestSeasar.Framework.Message
{
	[TestFixture]
	public class MessageFormatterTest
	{
		public MessageFormatterTest()
		{
		}
		
		[Test]
		public void TestGetMessage() {
			string message = MessageFormatter.GetMessage("ESSR0001", new object[]{"test"});
			Assert.AreEqual("[ESSR0001] test��������܂���", message, "���b�Z�[�W���\�[�X���擾�o���鎖");
			
		}
		
		[Test]
		public void TestGetMessage2() {
			Assembly asm = Assembly.GetExecutingAssembly();
			string message = MessageFormatter.GetMessage("ETST0001", new object[]{"test"}, asm);
			ResourceManager rm = new ResourceManager("TSTMessages",asm);
			try {
				Console.WriteLine(rm.GetString("ETST0001"));
			} catch(Exception e) {
				Console.WriteLine(e.StackTrace);
			}
			Assert.AreEqual("[ETST0001] test message", message , "���b�Z�[�W���\�[�X���擾�o���鎖");
		}

		[Test]
		public void TestGetMessage3()
		{
			string message = MessageFormatter.GetMessage("ESSR0001", new object[]{"test"});
			Assert.AreEqual("[ESSR0001] test��������܂���", message, "���b�Z�[�W���\�[�X���擾�o���鎖");
			message = MessageFormatter.GetMessage("ESSR0001", new object[]{"test"});
			Assert.AreEqual("[ESSR0001] test��������܂���", message, "���b�Z�[�W���\�[�X���擾�o���鎖");
		}

	}
}