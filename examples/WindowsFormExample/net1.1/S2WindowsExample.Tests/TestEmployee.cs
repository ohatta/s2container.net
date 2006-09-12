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
using System.Collections;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;
using log4net.Util;
using MbUnit.Framework;
using Nullables;
using Seasar.Extension.Unit;
using Seasar.WindowsExample.Logics.Dao;
using Seasar.WindowsExample.Logics.Dto;
using Seasar.WindowsExample.Logics.Service;

namespace Seasar.WindowsExample.Tests
{
    /// <summary>
    /// �Ј��p�e�X�g�P�[�X�N���X
    /// </summary>
    [TestFixture]
    public class TestEmployee : S2TestCase
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

            IEmployeeDao dao = (IEmployeeDao) GetComponent(typeof (IEmployeeDao));
            Assert.IsNotNull(dao, "NotNull");

            // �ꗗ�Ŏ擾����
            IList list = dao.GetAll();
            Assert.AreEqual(5, list.Count, "Count");
            int i = 0;
            foreach (EmployeeDto dto in list)
            {
                if ( i == 2 )
                {
                    Assert.AreEqual(3, dto.Id.Value, "Id");
                    Assert.AreEqual("�������q", dto.Name, "Name");
                    Assert.AreEqual("010003", dto.Code, "Code");
                    Assert.AreEqual(1, dto.Gender, "Gender");
                    Assert.AreEqual(new DateTime(2001, 4, 1, 0, 0, 0),
                                    dto.EntryDay.Value, "Entry");
                    Assert.AreEqual(1, dto.DeptNo.Value, "DeptNo");
                    Assert.AreEqual(1, dto.Department.Id.Value, "Dept.No");
                    Assert.AreEqual("�c�ƕ�", dto.Department.Name, "Dept.Name");
                    Assert.AreEqual("0001", dto.Department.Code, "Dept.Code");
                }
                i++;
            }

            // �ʂɎ擾����

            EmployeeDto data = dao.GetData(3);

            Assert.AreEqual(3, data.Id.Value, "Id2");
            Assert.AreEqual("�������q", data.Name, "Name2");
            Assert.AreEqual("010003", data.Code, "Code2");
            Assert.AreEqual(1, data.Gender, "Gender2");
            Assert.AreEqual(new DateTime(2001, 4, 1, 0, 0, 0),
                            data.EntryDay.Value, "Entry2");
            Assert.AreEqual(1, data.DeptNo.Value, "DeptN2o");
            Assert.AreEqual(1, data.Department.Id.Value, "Dept.No2");
            Assert.AreEqual("�c�ƕ�", data.Department.Name, "Dept.Name2");
            Assert.AreEqual("0001", data.Department.Code, "Dept.Code2");

            Assert.AreEqual(3, dao.GetId("010003"), "GetId");
        }

        /// <summary>
        /// �X�V�n�̃e�X�g
        /// </summary>
        [Test, S2(Tx.Rollback)]
        public void TestInsertOfDao()
        {
            Include(PATH);

            IEmployeeDao dao = (IEmployeeDao) GetComponent(typeof (IEmployeeDao));
            Assert.IsNotNull(dao, "NotNull");

            // �}���̃e�X�g
            EmployeeDto data = new EmployeeDto();
            data.Code = "060006";
            data.Name = "�㓡�Z�Y";
            data.EntryDay = new NullableDateTime(new DateTime(2006, 4, 1, 0, 0, 0));
            data.Gender = 0;
            data.DeptNo = 1;

            Assert.AreEqual(1, dao.InsertData(data), "Insert");

            // �X�V�̃e�X�g
            int id = dao.GetId("060006");
            data.Id = id;
            data.Code = "060006";
            data.Name = "�ܓ��Z�Y";
            data.EntryDay = new NullableDateTime(new DateTime(2006, 4, 1, 0, 0, 0));
            data.Gender = 0;
            data.DeptNo = 2;

            Assert.AreEqual(1, dao.UpdateData(data), "Update");

            data = dao.GetData(id);
            Assert.AreEqual(id, data.Id.Value, "Id");
            Assert.AreEqual("�ܓ��Z�Y", data.Name, "Name");
            Assert.AreEqual("060006", data.Code, "Code");
            Assert.AreEqual(0, data.Gender, "Gender2");
            Assert.AreEqual(new DateTime(2006, 4, 1, 0, 0, 0),
                            data.EntryDay.Value, "Entry");
            Assert.AreEqual(2, data.DeptNo.Value, "DeptNo");
            Assert.AreEqual(2, data.Department.Id.Value, "Dept.No2");
            Assert.AreEqual("�Z�p��", data.Department.Name, "Dept.Name2");
            Assert.AreEqual("0002", data.Department.Code, "Dept.Code2");

            // �폜�̃e�X�g
            data = new EmployeeDto();
            data.Id = id;

            Assert.AreEqual(1, dao.DeleteData(data), "Delete");

            IList list = dao.GetAll();
            Assert.AreEqual(5, list.Count, "Count");
        }

        /// <summary>
        /// CSV�p�e�X�g
        /// </summary>
        [Test, S2]
        public void TestDaoOfCSV()
        {
            Include(PATH);

            // �擾�̃e�X�g
            IEmployeeCSVDao dao = (IEmployeeCSVDao) GetComponent(typeof (IEmployeeCSVDao));
            Assert.IsNotNull(dao, "NotNull");

            IList list = dao.GetAll();
            Assert.AreEqual(5, list.Count, "Count");
            int i = 0;
            foreach (EmployeeCsvDto dto in list)
            {
                if ( i == 2 )
                {
                    Assert.AreEqual("�������q", dto.Name, "Name");
                    Assert.AreEqual("010003", dto.Code, "Code");
                    Assert.AreEqual(1, dto.Gender, "Gender");
                    Assert.AreEqual(new DateTime(2001, 4, 1, 0, 0, 0),
                                    dto.EntryDay.Value, "Entry");
                    Assert.AreEqual("�c�ƕ�", dto.DeptName, "Dept.Name");
                    Assert.AreEqual("0001", dto.DeptCode, "Dept.Code");
                }
                i++;
            }

            // �o�͂̃e�X�g
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\csvtest.csv";
            IOutputCSVDao daoOfCsv = (IOutputCSVDao) GetComponent(typeof (IOutputCSVDao));
            Assert.AreEqual(5, daoOfCsv.OutputEmployeeList(path, list));
        }

        /// <summary>
        /// �Ј����X�g�T�[�r�X�e�X�g
        /// </summary>
        [Test, S2]
        public void TestListService()
        {
            Include(PATH);

            IEmployeeListService service = (IEmployeeListService) GetComponent(typeof (IEmployeeListService));
            Assert.IsNotNull(service, "NotNull");

            IList list = service.GetAll();
            Assert.AreEqual(5, list.Count, "Count");
            int i = 0;
            foreach (EmployeeDto dto in list)
            {
                if ( i == 2 )
                {
                    Assert.AreEqual(3, dto.Id.Value, "Id");
                    Assert.AreEqual("�������q", dto.Name, "Name");
                    Assert.AreEqual("010003", dto.Code, "Code");
                    Assert.AreEqual(1, dto.Gender, "Gender");
                    Assert.AreEqual(new DateTime(2001, 4, 1, 0, 0, 0),
                                    dto.EntryDay.Value, "Entry");
                    Assert.AreEqual(1, dto.DeptNo.Value, "DeptNo");
                    Assert.AreEqual(1, dto.Department.Id.Value, "Dept.No");
                    Assert.AreEqual("�c�ƕ�", dto.Department.Name, "Dept.Name");
                    Assert.AreEqual("0001", dto.Department.Code, "Dept.Code");
                    Assert.AreEqual("�c�ƕ�", dto.DeptName, "DeptName");
                }
                i++;
            }
        }

        /// <summary>
        /// �Ј��o�^�T�[�r�X�̃e�X�g
        /// </summary>
        [Test, S2(Tx.Rollback)]
        public void TestEditService()
        {
            Include(PATH);

            IEmployeeEditService service = (IEmployeeEditService) GetComponent(typeof (IEmployeeEditService));
            Assert.IsNotNull(service, "NotNull");

            EmployeeDto data = service.GetData(3);
            Assert.AreEqual(3, data.Id.Value, "Id");
            Assert.AreEqual("�������q", data.Name, "Name");
            Assert.AreEqual("010003", data.Code, "Code");
            Assert.AreEqual(1, data.Gender, "Gender");
            Assert.AreEqual(new DateTime(2001, 4, 1, 0, 0, 0),
                            data.EntryDay.Value, "Entry");
            Assert.AreEqual(1, data.DeptNo.Value, "DeptNo");
            Assert.AreEqual(1, data.Department.Id.Value, "Dept.No");
            Assert.AreEqual("�c�ƕ�", data.Department.Name, "Dept.Name");
            Assert.AreEqual("0001", data.Department.Code, "Dept.Code");
            Assert.AreEqual("�c�ƕ�", data.DeptName, "DeptName");

            // �}���̃e�X�g
            data = new EmployeeDto();
            data.Code = "060006";
            data.Name = "�㓡�Z�Y";
            data.EntryDay = new NullableDateTime(new DateTime(2006, 4, 1, 0, 0, 0));
            data.Gender = 0;
            data.DeptNo = 1;

            Assert.AreEqual(1, service.ExecUpdate(data), "Insert");

            // �X�V�̃e�X�g
            data = new EmployeeDto();
            data.Id = 2;
            data.Code = "999999";
            data.Name = "��ؓ�Y";
            data.EntryDay = new NullableDateTime(new DateTime(1999, 5, 1, 0, 0, 0));
            data.Gender = 0;
            data.DeptNo = 2;

            Assert.AreEqual(1, service.ExecUpdate(data), "Update");

            data = service.GetData(2);
            Assert.AreEqual(2, data.Id.Value, "Id");
            Assert.AreEqual("��ؓ�Y", data.Name, "Name");
            Assert.AreEqual("999999", data.Code, "Code");
            Assert.AreEqual(0, data.Gender, "Gender");
            Assert.AreEqual(new DateTime(1999, 5, 1, 0, 0, 0),
                            data.EntryDay.Value, "Entry");
            Assert.AreEqual(2, data.DeptNo.Value, "DeptNo");
            Assert.AreEqual(2, data.Department.Id.Value, "Dept.No");
            Assert.AreEqual("�Z�p��", data.Department.Name, "Dept.Name");
            Assert.AreEqual("0002", data.Department.Code, "Dept.Code");
            Assert.AreEqual("�Z�p��", data.DeptName, "DeptName");
        }
    }
}