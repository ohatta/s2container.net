#region Copyright

/*
 * Copyright 2005-2008 the Seasar Foundation and the Others.
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

using System.Collections.Generic;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;
using log4net.Util;
using MbUnit.Framework;
using Seasar.Extension.Unit;
using Seasar.Quill.Unit;
using Seasar.S2FormExample.Logics.Dao;
using Seasar.S2FormExample.Logics.Dto;
using Seasar.S2FormExample.Logics.Page;
using Seasar.S2FormExample.Logics.Service;

namespace Seasar.S2FormExample.Tests
{
    /// <summary>
    /// 部門用テストケースクラス
    /// </summary>
    [TestFixture]
    public class TestDepartment : QuillTestCase
    {
        protected IDepartmentDao daoOfDepartment;
        protected IDepartmentEditService editService;
        protected IDepartmentListService listService;

        /// <summary>
        /// テストのセットアップ
        /// </summary>
        [SetUp]
        public void Setup()
        {
            FileInfo info = new FileInfo(
                string.Format("{0}.dll.config", SystemInfo.AssemblyShortName(
                                                    Assembly.GetExecutingAssembly())));
            // アセンブリがdllの場合は".dll.config"

            XmlConfigurator.Configure(LogManager.GetRepository(), info);
        }

        /// <summary>
        /// 検索系のテスト
        /// </summary>
        [Test, Quill(Tx.Rollback)]
        public void TestSelectOfDao()
        {
            // 一覧のテスト            
            IList<DepartmentDto> list = daoOfDepartment.GetAll();
            Assert.AreEqual(3, list.Count, "Count");
            int i = 0;
            foreach (DepartmentDto dto in list)
            {
                if (i == 1)
                {
                    Assert.AreEqual(2, dto.Id.Value, "ID");
                    Assert.AreEqual("0002", dto.Code, "Code");
                    Assert.AreEqual("技術部", dto.Name, "Name");
                    Assert.AreEqual(2, dto.ShowOrder, "Order");
                }
                i++;
            }

            // 個別取得のテスト
            DepartmentDto data = daoOfDepartment.GetData(2);
            Assert.AreEqual(2, data.Id.Value, "ID");
            Assert.AreEqual("0002", data.Code, "Code");
            Assert.AreEqual("技術部", data.Name, "Name");
            Assert.AreEqual(2, data.ShowOrder, "Order");

            Assert.AreEqual(2, daoOfDepartment.GetId("0002"), "GetId");
        }

        /// <summary>
        /// 更新系のテスト
        /// </summary>
        [Test, Quill(Tx.Rollback)]
        public void TestInsertOfDao()
        {
            // 挿入のテスト
            DepartmentDto data = new DepartmentDto();
            data.Code = "0102";
            data.Name = "管理部";
            data.ShowOrder = 4;

            Assert.AreEqual(1, daoOfDepartment.InsertData(data), "Insert");

            // 更新のテスト
            int id = daoOfDepartment.GetId("0102");
            data = new DepartmentDto();
            data.Code = "0102";
            data.Id = id;
            data.Name = "事業管理部";
            data.ShowOrder = 4;

            Assert.AreEqual(1, daoOfDepartment.UpdateData(data), "Update");

            data = daoOfDepartment.GetData(id);
            Assert.AreEqual(id, data.Id.Value, "ID");
            Assert.AreEqual("0102", data.Code, "Code");
            Assert.AreEqual("事業管理部", data.Name, "Name");
            Assert.AreEqual(4, data.ShowOrder, "Order");

            // 削除のテスト
            data = new DepartmentDto();
            data.Id = id;
            Assert.AreEqual(1, daoOfDepartment.DeleteData(data), "Delete");

            IList<DepartmentDto> list = daoOfDepartment.GetAll();
            Assert.AreEqual(3, list.Count, "Count");
        }

        /// <summary>
        /// 部門リストサービステスト
        /// </summary>
        [Test, Quill(Tx.Rollback)]
        public void TestListService()
        {
            DepartmentListPage page = listService.GetAll();
            Assert.AreEqual(3, page.List.Count, "Count");
        }

        /// <summary>
        /// 部門登録用サービステスト
        /// </summary>
        [Test, Quill(Tx.Rollback)]
        public void TestEditService()
        {
            DepartmentEditPage data = editService.GetData(2);
            Assert.AreEqual(2, data.Id.Value, "ID");
            Assert.AreEqual("0002", data.Code, "Code");
            Assert.AreEqual("技術部", data.Name, "Name");
            Assert.AreEqual("2", data.Order, "Order");

            // 挿入のテスト
            data = new DepartmentEditPage();
            data.Code = "0102";
            data.Name = "管理部";
            data.Order = "4";

            Assert.AreEqual(1, editService.ExecUpdate(data), "Insert");

            // 更新のテスト
            data = new DepartmentEditPage();
            data.Id = 2;
            data.Code = "0020";
            data.Name = "技術事業部";
            data.Order = "5";

            Assert.AreEqual(1, editService.ExecUpdate(data), "Update");

            data = editService.GetData(2);
            Assert.AreEqual(2, data.Id.Value, "ID");
            Assert.AreEqual("0020", data.Code, "Code");
            Assert.AreEqual("技術事業部", data.Name, "Name");
            Assert.AreEqual("5", data.Order, "Order");
        }
    }
}