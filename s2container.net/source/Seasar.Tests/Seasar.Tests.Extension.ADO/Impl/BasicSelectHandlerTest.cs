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
using System.Data;
using MbUnit.Framework;
using Seasar.Extension.ADO;
using Seasar.Extension.ADO.Impl;
using Seasar.Extension.Unit;

namespace Seasar.Tests.Extension.ADO.Impl
{
	[TestFixture]
	public class BasicSelectHandlerTest : S2TestCase
	{
		public const string WAVE_DASH = "\u301C";
		public const string FULL_WIDTH_TILDE = "\uFF5E";

		private const string PATH = "Ado.dicon";

		public void SetUpExecute() 
		{
			Include(PATH);
		}

		[Test, S2(Tx.Rollback)]
		public void Execute()
		{
			string sql = "insert into emp(empno, ename) values(99, @ename)";
			BasicUpdateHandler handler = new BasicUpdateHandler(DataSource, sql);
			object[] args = new object[] { FULL_WIDTH_TILDE };
			string[] argNames = new string[] { "ename" };
			handler.Execute(args, Type.GetTypeArray(args), argNames);
			
			string sql2 = "select ename from emp where empno = 99";
			BasicSelectHandler handler2 = new BasicSelectHandler(
				DataSource,
				sql2,
				new ObjectDataReaderHandler(),
				BasicCommandFactory.INSTANCE,
				BasicDataReaderFactory.INSTANCE
				);

			string ret = (string) handler2.Execute(null);
			Console.Out.WriteLine(ret);
			Assert.AreEqual(FULL_WIDTH_TILDE, ret, "1");
		}
	}
}