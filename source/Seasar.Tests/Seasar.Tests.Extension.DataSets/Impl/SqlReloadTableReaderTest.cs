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

using System.Data;
using System.Diagnostics;
using MbUnit.Framework;
using Seasar.Extension.Unit;
using Seasar.Extension.DataSets.Impl;
using Seasar.Framework.Util;

namespace Seasar.Tests.Extension.DataSets.Impl
{
	[TestFixture]
	public class SqlReloadTableReaderTest : S2TestCase
	{
		private const string PATH = "Ado.dicon";

		public void SetUpRead() 
		{
			Include(PATH);
		}

        [Test, S2(Seasar.Extension.Unit.Tx.Rollback)]
		public void Read() 
		{
			DataTable table = new DataTable("emp");
			table.Columns.Add("empno", typeof(int));
			table.Columns.Add("ename", typeof(string));
			DataRow row = table.NewRow();
			row["empno"] = 7788;
			row["ename"] = "SCOTT";
			table.Rows.Add(row);
			SqlReloadTableReader reader = new SqlReloadTableReader(DataSource, table);
			DataTable ret = reader.Read();
            Trace.WriteLine(ToStringUtil.ToString(ret));
			S2Assert.AreEqual(table, ret, "1");
			Assert.AreEqual(DataRowState.Unchanged, ret.Rows[0].RowState, "2");
		}
	}
}
