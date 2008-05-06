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

using System;
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
    /// �Ј��p�e�X�g�P�[�X�N���X
    /// </summary>
    [TestFixture]
    public class TestEmployee : QuillTestCase
    {
        protected IEmployeeDao daoOfEmp;
        protected IOutputCSVDao daoOfOutput;
        protected IEmployeeCSVDao daoOfCsv;
        protected IEmployeeEditService editService;
        protected IEmployeeListService listService;

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
        [Test, Quill(Tx.Rollback)]
        public void TestSelectOfDao()
        {
            // �ꗗ�Ŏ擾����            
            IList<EmployeeDto> list = daoOfEmp.GetAll();
            Assert.AreEqual(5, list.Count, "Count");
            int i = 0;
            foreach (EmployeeDto dto in list)
            {
                if (i == 2)
                {
                    Assert.AreEqual(3, dto.Id.Value, "Id");
                    Assert.AreEqual("�������q", dto.Name, "Name");
                    Assert.AreEqual("010003", dto.Code, "Code");
                    Assert.AreEqual(2, dto.Gender, "Gender");
                    Assert.AreEqual(new DateTime(2001, 4, 1, 0, 0, 0),
                                    dto.EntryDay.Value, "Entry");
                    Assert.AreEqual(1, dto.DeptNo.Value, "DeptNo");
                    Assert.AreEqual(1, dto.Department.Id.Value, "Dept.No");
                    Assert.AreEqual("�c�ƕ�", dto.Department.Name, "Dept.Name");
                    Assert.AreEqual("0001", dto.Department.Code, "Dept.Code");
                }
                i++;
            }

            list = daoOfEmp.FindByGender(1);
            Assert.AreEqual(4, list.Count, "Count2");

            // �ʂɎ擾����

            EmployeeDto data = daoOfEmp.GetData(3);

            Assert.AreEqual(3, data.Id.Value, "Id2");
            Assert.AreEqual("�������q", data.Name, "Name2");
            Assert.AreEqual("010003", data.Code, "Code2");
            Assert.AreEqual(2, data.Gender, "Gender2");
            Assert.AreEqual(new DateTime(2001, 4, 1, 0, 0, 0),
                            data.EntryDay.Value, "Entry2");
            Assert.AreEqual(1, data.DeptNo.Value, "DeptN2o");
            Assert.AreEqual(1, data.Department.Id.Value, "Dept.No2");
            Assert.AreEqual("�c�ƕ�", data.Department.Name, "Dept.Name2");
            Assert.AreEqual("0001", data.Department.Code, "Dept.Code2");

            Assert.AreEqual(3, daoOfEmp.GetId("010003"), "GetId");
        }

        /// <summary>
        /// �X�V�n�̃e�X�g
        /// </summary>
        [Test, Quill(Tx.Rollback)]
        public void TestInsertOfDao()
        {
            // �}���̃e�X�g
            EmployeeDto data = new EmployeeDto();
            data.Code = "060006";
            data.Name = "�㓡�Z�Y";
            data.EntryDay = new DateTime(2006, 4, 1, 0, 0, 0);
            data.Gender = 1;
            data.DeptNo = 1;

            Assert.AreEqual(1, daoOfEmp.InsertData(data), "Insert");

            // �X�V�̃e�X�g
            int id = daoOfEmp.GetId("060006");
            data.Id = id;
            data.Code = "060006";
            data.Name = "�ܓ��Z�Y";
            data.EntryDay = new DateTime(2006, 4, 1, 0, 0, 0);
            data.Gender = 1;
            data.DeptNo = 2;

            Assert.AreEqual(1, daoOfEmp.UpdateData(data), "Update");

            data = daoOfEmp.GetData(id);
            Assert.AreEqual(id, data.Id.Value, "Id");
            Assert.AreEqual("�ܓ��Z�Y", data.Name, "Name");
            Assert.AreEqual("060006", data.Code, "Code");
            Assert.AreEqual(1, data.Gender, "Gender2");
            Assert.AreEqual(new DateTime(2006, 4, 1, 0, 0, 0),
                            data.EntryDay.Value, "Entry");
            Assert.AreEqual(2, data.DeptNo.Value, "DeptNo");
            Assert.AreEqual(2, data.Department.Id.Value, "Dept.No2");
            Assert.AreEqual("�Z�p��", data.Department.Name, "Dept.Name2");
            Assert.AreEqual("0002", data.Department.Code, "Dept.Code2");

            // �폜�̃e�X�g
            data = new EmployeeDto();
            data.Id = id;

            Assert.AreEqual(1, daoOfEmp.DeleteData(data), "Delete");

            IList<EmployeeDto> list = daoOfEmp.GetAll();
            Assert.AreEqual(5, list.Count, "Count");
        }

        /// <summary>
        /// CSV�p�e�X�g
        /// </summary>
        [Test, Quill(Tx.Rollback)]
        public void TestDaoOfCSV()
        {
            // �擾�̃e�X�g
            IList<EmployeeCsvDto> list = daoOfCsv.GetAll();
            Assert.AreEqual(5, list.Count, "Count");
            int i = 0;
            foreach (EmployeeCsvDto dto in list)
            {
                if (i == 2)
                {
                    Assert.AreEqual("�������q", dto.Name, "Name");
                    Assert.AreEqual("010003", dto.Code, "Code");
                    Assert.AreEqual(2, dto.Gender, "Gender");
                    Assert.AreEqual("����", dto.GenderName, "GenderName");
                    Assert.AreEqual(new DateTime(2001, 4, 1, 0, 0, 0),
                                    dto.EntryDay.Value, "Entry");
                    Assert.AreEqual("�c�ƕ�", dto.DeptName, "Dept.Name");
                    Assert.AreEqual("0001", dto.DeptCode, "Dept.Code");
                }
                i++;
            }

            // �o�͂̃e�X�g
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\csvtest.csv";
            Assert.AreEqual(5, daoOfOutput.OutputEmployeeList(path, list));
        }

        /// <summary>
        /// �Ј����X�g�T�[�r�X�e�X�g
        /// </summary>
        [Test, Quill(Tx.Rollback)]
        public void TestListService()
        {
            EmployeeListPage page = listService.GetAll();
            Assert.AreEqual(5, page.List.Count, "Count");
            int i = 0;
            foreach (EmployeeDto dto in page.List)
            {
                if (i == 2)
                {
                    Assert.AreEqual(3, dto.Id.Value, "Id");
                    Assert.AreEqual("�������q", dto.Name, "Name");
                    Assert.AreEqual("010003", dto.Code, "Code");
                    Assert.AreEqual(2, dto.Gender, "Gender");
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
        [Test, Quill(Tx.Rollback)]
        public void TestEditService()
        {
            EmployeeEditPage data = editService.GetData(3);
            Assert.AreEqual(3, data.Id.Value, "Id");
            Assert.AreEqual("�������q", data.Name, "Name");
            Assert.AreEqual("010003", data.Code, "Code");
            Assert.AreEqual(2, data.Gender, "Gender");
            Assert.AreEqual(new DateTime(2001, 4, 1, 0, 0, 0),
                            data.Entry, "Entry");
            Assert.AreEqual(1, data.Depart, "DeptNo");

            // �}���̃e�X�g
            data = new EmployeeEditPage();
            data.Code = "060006";
            data.Name = "�㓡�Z�Y";
            data.Entry = new DateTime(2006, 4, 1, 0, 0, 0);
            data.Gender = 1;
            data.Depart = 1;

            Assert.AreEqual(1, editService.ExecUpdate(data), "Insert");

            // �X�V�̃e�X�g
            data = new EmployeeEditPage();
            data.Id = 2;
            data.Code = "999999";
            data.Name = "��ؓ�Y";
            data.Entry = new DateTime(1999, 5, 1, 0, 0, 0);
            data.Gender = 1;
            data.Depart = 2;

            Assert.AreEqual(1, editService.ExecUpdate(data), "Update");

            data = editService.GetData(2);
            Assert.AreEqual(2, data.Id.Value, "Id");
            Assert.AreEqual("��ؓ�Y", data.Name, "Name");
            Assert.AreEqual("999999", data.Code, "Code");
            Assert.AreEqual(1, data.Gender, "Gender");
            Assert.AreEqual(new DateTime(1999, 5, 1, 0, 0, 0),
                            data.Entry, "Entry");
            Assert.AreEqual(2, data.Depart, "DeptNo");
        }
    }
}