#region Copyright
/*
 * Copyright 2005-2010 the Seasar Foundation and the Others.
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
using System.Diagnostics;
using System.Data;
using System.IO;
using System.Reflection;
using MbUnit.Framework;
using Seasar.Extension.DataSets.Impl;
using Seasar.Extension.Unit;
using Seasar.Framework.Util;
using System.Configuration;

namespace Seasar.Tests.Extension.DataSets.Impl
{
    [TestFixture]
    [Ignore("Microsoft.Jet.OLEDB4.0��32bitOS�ł������삵�Ȃ����ߓ��������B�ۗ��B")]
    public class XlsReaderTest : S2TestCase
    {
        private const string PATH = "Seasar.Tests.Extension.DataSets.Impl.XlsReaderImplTest.xls";

        private DataSet dataSet;

        [SetUp]
        public void SetUp()
        {
            using (Stream stream = ResourceUtil.GetResourceAsStream(PATH, Assembly.GetExecutingAssembly()))
            {
                dataSet = new XlsReader(stream).Read();
            }
        }

        [Ignore("Microsoft.Jet.OLEDB4.0��32bitOS�ł������삵�Ȃ����ߓ��������B�ۗ��B")]
        [Test]
        public void TestCreateTable()
        {
            Assert.AreEqual(4, dataSet.Tables.Count, "1");
            Trace.WriteLine(ToStringUtil.ToString(dataSet));
        }

        [Ignore("Microsoft.Jet.OLEDB4.0��32bitOS�ł������삵�Ȃ����ߓ��������B�ۗ��B")]
        [Test]
        public void TestSetupColumns()
        {
            // Java�łƈႢ�A�e�[�u�����Ń\�[�g����Ă���H�̂ŁAindex�ł͂Ȃ��Aname�Ŏ擾�B
            DataTable table = dataSet.Tables["TEST_TABLE"];
            Assert.AreEqual(4, table.Columns.Count, "1");
            for (int i = 0; i < table.Columns.Count; ++i)
            {
                Assert.AreEqual("COLUMN" + i, table.Columns[i].ColumnName, "2");
            }
        }

        [Ignore("Microsoft.Jet.OLEDB4.0��32bitOS�ł������삵�Ȃ����ߓ��������B�ۗ��B")]
        [Test]
        public void TestSetupRows()
        {
            DataTable table = dataSet.Tables["TEST_TABLE"];
            Assert.AreEqual(12, table.Rows.Count, "1");
            for (int i = 0; i < table.Rows.Count; ++i)
            {
                DataRow row = table.Rows[i];
                for (int j = 0; j < table.Columns.Count; ++j)
                {
                    Assert.AreEqual("row " + i + " col " + j, row[j], "2");
                }
            }
            DataTable table2 = dataSet.Tables["EMPTY_TABLE"];
            Assert.AreEqual(0, table2.Rows.Count, "3");
        }

        [Ignore("Microsoft.Jet.OLEDB4.0��32bitOS�ł������삵�Ȃ����ߓ��������B�ۗ��B")]
        [Test]
        public void TestGetValue()
        {
            DataTable table = dataSet.Tables["��"];
            DataRow row = table.Rows[0];
            Assert.AreEqual(
                new DateTime(2004, 3, 22),
                row[0],
                "1"
                );
            Assert.AreEqual(
                123m,
                row[1],
                "2"
                );
            Assert.AreEqual(
                "\u3042",
                row[2],
                "3"
                );
        }

        [Ignore("BASE64_FORMAT���Ή��̂���")]
        public void TestGetValueIgnore()
        {
            DataTable table = dataSet.Tables["��"];
            DataRow row = table.Rows[0];
            Assert.AreEqual(
                "YWJj",
                Convert.ToBase64String((byte[]) row[3]),
                "4"
                );
        }

        [Ignore("Microsoft.Jet.OLEDB4.0��32bitOS�ł������삵�Ȃ����ߓ��������B�ۗ��B")]
        [Test]
        public void TestDataRowState()
        {
            DataTable ret = dataSet.Tables["TEST_TABLE"];
            Assert.AreEqual(DataRowState.Added, ret.Rows[0].RowState);
        }

        [Ignore("Microsoft.Jet.OLEDB4.0��32bitOS�ł������삵�Ȃ����ߓ��������B�ۗ��B")]
        [Test]
        public void TestInvalidColumn()
        {
            string exeConfigPath = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath;
            string[] filePathParts = exeConfigPath.Split('\\');
            string xlsFilePath = exeConfigPath.Replace(filePathParts[filePathParts.Length - 1], string.Empty) + "\\" +
                             "../../../Extension/DataSets.Impl/XlsReaderTest_InvalidColumn.xls";
            //XlsReader reader = new XlsReader("../Extension/DataSets.Impl/XlsReaderTest_InvalidColumn.xls");
            XlsReader reader = new XlsReader(xlsFilePath);
            DataTable dt = reader.Read().Tables["table"];
            Assert.AreEqual(1, dt.Columns.Count);
            Assert.AreEqual("INF01", dt.Columns[0].ColumnName);
        }
    }
}
