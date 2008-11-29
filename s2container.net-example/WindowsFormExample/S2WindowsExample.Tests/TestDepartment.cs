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

using System.Collections.Generic;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;
using log4net.Util;
using MbUnit.Framework;
using Seasar.Extension.Unit;
using Seasar.WindowsExample.Logics.Dao;
using Seasar.WindowsExample.Logics.Dto;
using Seasar.WindowsExample.Logics.Service;

namespace Seasar.WindowsExample.Tests
{
    /// <summary>
    /// ����p�e�X�g�P�[�X�N���X
    /// </summary>
    [TestFixture]
    public class TestDepartment : S2TestCase
    {
        /// <summary>
        /// Logic�ݒ�t�@�C��
        /// </summary>
        private const string PATH = "ExampleLogics.dicon";

        /// <summary>
        /// �e�X�g�̃Z�b�g�A�b�v
        /// </summary>
        [SetUp]
        public void Setup()
        {
            FileInfo info = new FileInfo(
                string.Format("{0}.dll.config", SystemInfo.AssemblyShortName(
                                                    Assembly.GetExecutingAssembly())));
            // �A�Z���u����dll�̏ꍇ��".dll.config"

            XmlConfigurator.Configure(LogManager.GetRepository(), info);
        }

        /// <summary>
        /// �����n�̃e�X�g
        /// </summary>
        [Test, S2]
        public void TestSelectOfDao()
        {
            Include(PATH);

            IDepartmentDao dao = (IDepartmentDao) GetComponent(typeof (IDepartmentDao));
            Assert.IsNotNull(dao, "NotNull");

            // �ꗗ�̃e�X�g

            IList<DepartmentDto> list = dao.GetAll();
            Assert.AreEqual(3, list.Count, "Count");

            int i = 0;
            foreach (DepartmentDto dto in list)
            {
                if (i == 1)
                {
                    Assert.AreEqual(2, dto.Id.Value, "ID");
                    Assert.AreEqual("0002", dto.Code, "Code");
                    Assert.AreEqual("�Z�p��", dto.Name, "Name");
                    Assert.AreEqual(2, dto.ShowOrder, "Order");
                }
                i++;
            }

            // �ʎ擾�̃e�X�g
            DepartmentDto data = dao.GetData(2);
            Assert.AreEqual(2, data.Id.Value, "ID");
            Assert.AreEqual("0002", data.Code, "Code");
            Assert.AreEqual("�Z�p��", data.Name, "Name");
            Assert.AreEqual(2, data.ShowOrder, "Order");

            Assert.AreEqual(2, dao.GetId("0002"), "GetId");
        }

        /// <summary>
        /// �X�V�n�̃e�X�g
        /// </summary>
        [Test, S2(Tx.Rollback)]
        public void TestInsertOfDao()
        {
            Include(PATH);

            IDepartmentDao dao = (IDepartmentDao) GetComponent(typeof (IDepartmentDao));
            Assert.IsNotNull(dao, "NotNull");

            // �}���̃e�X�g
            DepartmentDto data = new DepartmentDto();
            data.Code = "0102";
            data.Name = "�Ǘ���";
            data.ShowOrder = 4;

            Assert.AreEqual(1, dao.InsertData(data), "Insert");

            // �X�V�̃e�X�g
            int id = dao.GetId("0102");
            data = new DepartmentDto();
            data.Code = "0102";
            data.Id = id;
            data.Name = "���ƊǗ���";
            data.ShowOrder = 4;

            Assert.AreEqual(1, dao.UpdateData(data), "Update");

            data = dao.GetData(id);
            Assert.AreEqual(id, data.Id.Value, "ID");
            Assert.AreEqual("0102", data.Code, "Code");
            Assert.AreEqual("���ƊǗ���", data.Name, "Name");
            Assert.AreEqual(4, data.ShowOrder, "Order");

            // �폜�̃e�X�g
            data = new DepartmentDto();
            data.Id = id;
            Assert.AreEqual(1, dao.DeleteData(data), "Delete");

            IList<DepartmentDto> list = dao.GetAll();
            Assert.AreEqual(3, list.Count, "Count");
        }

        /// <summary>
        /// ���僊�X�g�T�[�r�X�e�X�g
        /// </summary>
        [Test, S2]
        public void TestListService()
        {
            Include(PATH);

            IDepartmentListService service = (IDepartmentListService) GetComponent(typeof (IDepartmentListService));
            Assert.IsNotNull(service, "NotNull");

            IList<DepartmentDto> list = service.GetAll();
            Assert.AreEqual(3, list.Count, "Count");
        }

        /// <summary>
        /// ����o�^�p�T�[�r�X�e�X�g
        /// </summary>
        [Test, S2(Tx.Rollback)]
        public void TestEditService()
        {
            Include(PATH);

            IDepartmentEditService service = (IDepartmentEditService) GetComponent(typeof (IDepartmentEditService));
            Assert.IsNotNull(service, "NotNull");

            DepartmentDto data = service.GetData(2);
            Assert.AreEqual(2, data.Id.Value, "ID");
            Assert.AreEqual("0002", data.Code, "Code");
            Assert.AreEqual("�Z�p��", data.Name, "Name");
            Assert.AreEqual(2, data.ShowOrder, "Order");

            // �}���̃e�X�g
            data = new DepartmentDto();
            data.Code = "0102";
            data.Name = "�Ǘ���";
            data.ShowOrder = 4;

            Assert.AreEqual(1, service.ExecUpdate(data), "Insert");

            // �X�V�̃e�X�g
            data = new DepartmentDto();
            data.Id = 2;
            data.Code = "0020";
            data.Name = "�Z�p���ƕ�";
            data.ShowOrder = 5;

            Assert.AreEqual(1, service.ExecUpdate(data), "Update");

            data = service.GetData(2);
            Assert.AreEqual(2, data.Id.Value, "ID");
            Assert.AreEqual("0020", data.Code, "Code");
            Assert.AreEqual("�Z�p���ƕ�", data.Name, "Name");
            Assert.AreEqual(5, data.ShowOrder, "Order");
        }
    }
}