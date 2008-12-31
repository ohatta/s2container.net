#region Copyright
/*
 * Copyright 2005-2009 the Seasar Foundation and the Others.
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
using System.Text;
using MbUnit.Framework;
using Seasar.Dao.Unit;
using Seasar.Dao;
using Seasar.Dao.Impl;
using Seasar.Extension.Unit;

namespace Seasar.Tests.Dao.Impl
{
    [TestFixture]
    public class UpdateModifiedOnlyCommandTest : S2DaoTestCase
    {
        public void SetUp()
        {
            Include("Seasar.Tests.Dao.Dao.dicon");
        }

        [Test, S2(Tx.Rollback)]
        public void TestExecuteModifiedOnlyTx()
        {
            IDaoMetaData dmd = CreateDaoMetaData(typeof(IEmployeeModifiedOnlyDao));
            ISqlCommand updateCommand = dmd.GetSqlCommand("UpdateModifiedOnly");
            ISqlCommand selectCommand = dmd.GetSqlCommand("GetEmployee");
            const int TEST_EMP_NO = 7369;
            {
                EmployeeModifiedOnly entity = new EmployeeModifiedOnly();
                Assert.AreEqual(0, entity.ModifiedPropertyNames.Count);
                entity.Empno = TEST_EMP_NO;
                entity.JobName = "Hoge";
                Assert.IsTrue(entity.ModifiedPropertyNames.Count > 0);

                int modifiedCount = (int)updateCommand.Execute(new object[] { entity });

                Assert.IsTrue(modifiedCount > 0);
                EmployeeModifiedOnly afterEntity = selectCommand.Execute(new object[] { TEST_EMP_NO }) as EmployeeModifiedOnly;
                Assert.IsNotNull(afterEntity, "�X�V�����G���e�B�e�B�����݂���");
                Console.WriteLine(afterEntity.ToString());
                Assert.AreEqual(entity.JobName, afterEntity.JobName, "�X�V�����v���p�e�B�͍X�V��̒l�ɂȂ��Ă���");
                Assert.IsFalse(string.IsNullOrEmpty(afterEntity.Ename), "�X�V���Ă��Ȃ��v���p�e�B�͍X�V���s�O�̒l�̂܂�1");
                Assert.IsTrue(afterEntity.Hiredate.HasValue, "�X�V���Ă��Ȃ��v���p�e�B�͍X�V���s�O�̒l�̂܂�2");
                Assert.IsTrue(afterEntity.Mgr.HasValue, "�X�V���Ă��Ȃ��v���p�e�B�͍X�V���s�O�̒l�̂܂�3");
                Assert.IsTrue(afterEntity.Sal.HasValue, "�X�V���Ă��Ȃ��v���p�e�B�͍X�V���s�O�̒l�̂܂�4");
            }
            {
                EmployeeModifiedOnly entity = selectCommand.Execute(new object[] { TEST_EMP_NO }) as EmployeeModifiedOnly;
                Assert.IsNotNull(entity);
                Assert.AreEqual(0, entity.ModifiedPropertyNames.Count, "select����͍X�V�v���p�e�B�͖����̏��");
                entity.Empno = TEST_EMP_NO;
                entity.JobName = "Hoge";
                Assert.IsTrue(entity.ModifiedPropertyNames.Count > 0);

                int modifiedCount = (int)updateCommand.Execute(new object[] { entity });

                Assert.IsTrue(modifiedCount > 0);
                EmployeeModifiedOnly afterEntity = selectCommand.Execute(new object[] { TEST_EMP_NO }) as EmployeeModifiedOnly;
                Assert.IsNotNull(afterEntity, "�X�V�����G���e�B�e�B�����݂���");
                Console.WriteLine(afterEntity.ToString());
                Assert.AreEqual(entity.JobName, afterEntity.JobName, "�X�V�����v���p�e�B�͍X�V��̒l�ɂȂ��Ă���");
                Assert.IsFalse(string.IsNullOrEmpty(afterEntity.Ename), "�X�V���Ă��Ȃ��v���p�e�B�͍X�V���s�O�̒l�̂܂�1");
                Assert.IsTrue(afterEntity.Hiredate.HasValue, "�X�V���Ă��Ȃ��v���p�e�B�͍X�V���s�O�̒l�̂܂�2");
                Assert.IsTrue(afterEntity.Mgr.HasValue, "�X�V���Ă��Ȃ��v���p�e�B�͍X�V���s�O�̒l�̂܂�3");
                Assert.IsTrue(afterEntity.Sal.HasValue, "�X�V���Ă��Ȃ��v���p�e�B�͍X�V���s�O�̒l�̂܂�4");
            }
            {
                EmployeeModifiedOnly entity = selectCommand.Execute(new object[] { TEST_EMP_NO }) as EmployeeModifiedOnly;
                Assert.IsNotNull(entity);
                Assert.AreEqual(0, entity.ModifiedPropertyNames.Count, "select����͍X�V�v���p�e�B�͖����̏��");
                entity.Empno = TEST_EMP_NO;

                int modifiedCount = (int)updateCommand.Execute(new object[] { entity });

                Assert.AreEqual(0, modifiedCount, "�X�V�v���p�e�B���Ȃ��Ƃ��͍X�V�����s����Ȃ�");
            }
        }

        [Test, S2(Tx.Rollback)]
        public void TestExecuteModifiedOnlyWithoutClearMethodTx()
        {
            IDaoMetaData dmd = CreateDaoMetaData(typeof(IEmployeeModifiedOnlyWithoutClearMethodDao));
            ISqlCommand updateCommand = dmd.GetSqlCommand("UpdateModifiedOnly");
            ISqlCommand selectCommand = dmd.GetSqlCommand("GetEmployee");
            const int TEST_EMP_NO = 7369;
            {
                EmployeeModifiedOnlyWithoutClearMethod entity = new EmployeeModifiedOnlyWithoutClearMethod();
                Assert.AreEqual(0, entity.ModifiedPropertyNames.Count);
                entity.Empno = TEST_EMP_NO;
                entity.JobName = "Hoge";
                Assert.IsTrue(entity.ModifiedPropertyNames.Count > 0);

                int modifiedCount = (int)updateCommand.Execute(new object[] { entity });

                Assert.IsTrue(modifiedCount > 0);
                EmployeeModifiedOnlyWithoutClearMethod afterEntity = selectCommand.Execute(new object[] { TEST_EMP_NO }) as EmployeeModifiedOnlyWithoutClearMethod;
                Assert.IsNotNull(afterEntity, "�X�V�����G���e�B�e�B�����݂���");
                Console.WriteLine(afterEntity.ToString());
                Assert.AreEqual(entity.JobName, afterEntity.JobName, "�X�V�����v���p�e�B�͍X�V��̒l�ɂȂ��Ă���");
                Assert.IsFalse(string.IsNullOrEmpty(afterEntity.Ename), "�X�V���Ă��Ȃ��v���p�e�B�͍X�V���s�O�̒l�̂܂�1");
                Assert.IsTrue(afterEntity.Hiredate.HasValue, "�X�V���Ă��Ȃ��v���p�e�B�͍X�V���s�O�̒l�̂܂�2");
                Assert.IsTrue(afterEntity.Mgr.HasValue, "�X�V���Ă��Ȃ��v���p�e�B�͍X�V���s�O�̒l�̂܂�3");
                Assert.IsTrue(afterEntity.Sal.HasValue, "�X�V���Ă��Ȃ��v���p�e�B�͍X�V���s�O�̒l�̂܂�4");
            }
            {
                EmployeeModifiedOnlyWithoutClearMethod entity = selectCommand.Execute(new object[] { TEST_EMP_NO }) as EmployeeModifiedOnlyWithoutClearMethod;
                Assert.IsNotNull(entity);
                Assert.AreEqual(0, entity.ModifiedPropertyNames.Count, "select����͍X�V�v���p�e�B�͖����̏��");
                entity.Empno = TEST_EMP_NO;
                entity.JobName = "Hoge";
                Assert.IsTrue(entity.ModifiedPropertyNames.Count > 0);

                int modifiedCount = (int)updateCommand.Execute(new object[] { entity });

                Assert.IsTrue(modifiedCount > 0);
                EmployeeModifiedOnlyWithoutClearMethod afterEntity = selectCommand.Execute(new object[] { TEST_EMP_NO }) as EmployeeModifiedOnlyWithoutClearMethod;
                Assert.IsNotNull(afterEntity, "�X�V�����G���e�B�e�B�����݂���");
                Console.WriteLine(afterEntity.ToString());
                Assert.AreEqual(entity.JobName, afterEntity.JobName, "�X�V�����v���p�e�B�͍X�V��̒l�ɂȂ��Ă���");
                Assert.IsFalse(string.IsNullOrEmpty(afterEntity.Ename), "�X�V���Ă��Ȃ��v���p�e�B�͍X�V���s�O�̒l�̂܂�1");
                Assert.IsTrue(afterEntity.Hiredate.HasValue, "�X�V���Ă��Ȃ��v���p�e�B�͍X�V���s�O�̒l�̂܂�2");
                Assert.IsTrue(afterEntity.Mgr.HasValue, "�X�V���Ă��Ȃ��v���p�e�B�͍X�V���s�O�̒l�̂܂�3");
                Assert.IsTrue(afterEntity.Sal.HasValue, "�X�V���Ă��Ȃ��v���p�e�B�͍X�V���s�O�̒l�̂܂�4");
            }
            {
                EmployeeModifiedOnlyWithoutClearMethod entity = selectCommand.Execute(new object[] { TEST_EMP_NO }) as EmployeeModifiedOnlyWithoutClearMethod;
                Assert.IsNotNull(entity);
                Assert.AreEqual(0, entity.ModifiedPropertyNames.Count, "select����͍X�V�v���p�e�B�͖����̏��");
                entity.Empno = TEST_EMP_NO;

                int modifiedCount = (int)updateCommand.Execute(new object[] { entity });

                Assert.AreEqual(0, modifiedCount, "�X�V�v���p�e�B���Ȃ��Ƃ��͍X�V�����s����Ȃ�");
            }
        }

        [Test, S2(Tx.Rollback)]
        public void TestExecuteClearModifiedMethodOnlyTx()
        {
            IDaoMetaData dmd = CreateDaoMetaData(typeof(IEmployeeClearModifiedMethodOnlyDao));
            ISqlCommand updateCommand1 = dmd.GetSqlCommand("UpdateModifiedOnly");
            ISqlCommand selectCommand = dmd.GetSqlCommand("GetEmployee");
            const int TEST_EMP_NO = 7369;
            {
                EmployeeClearModifiedMethodOnly entity = selectCommand.Execute(new object[] { TEST_EMP_NO }) as EmployeeClearModifiedMethodOnly;
                Assert.IsTrue(entity.IsClearMethodCalled, "ClearModifiedOnlyPropertyNames���\�b�h������΂���͌Ă΂�Ă���");
                entity.Empno = TEST_EMP_NO;
                entity.JobName = "Hoge";

                try
                {
                    updateCommand1.Execute(new object[] { entity });
                    Assert.Fail("ModifiedOnlyPropertyNames���Ȃ��ꍇ�͗�O����������B");
                }
                catch ( NotFoundModifiedPropertiesRuntimeException ex )
                {
                    Console.WriteLine(ex.Message);
                }
            }
            ISqlCommand updateCommand2 = dmd.GetSqlCommand("Update");
            {
                EmployeeClearModifiedMethodOnly entity = new EmployeeClearModifiedMethodOnly();
                entity.Empno = TEST_EMP_NO;
                entity.JobName = "Hoge";

                int modifiedCount = (int)updateCommand2.Execute(new object[] { entity });

                Assert.IsTrue(modifiedCount > 0, "�ʏ��update�͗�O�ƂȂ炸�Ɏ��s�����");
                EmployeeClearModifiedMethodOnly afterEntity = selectCommand.Execute(new object[] { TEST_EMP_NO }) as EmployeeClearModifiedMethodOnly;
                Assert.IsNotNull(afterEntity, "�X�V�����G���e�B�e�B�����݂���");
                Assert.AreEqual(entity.JobName, afterEntity.JobName);
                Assert.IsNull(afterEntity.Ename, "�X�V���Ă��Ȃ��v���p�e�B��null�X�V1");
                Assert.IsFalse(afterEntity.Hiredate.HasValue, "�X�V���Ă��Ȃ��v���p�e�B��null�X�V2");
                Assert.IsFalse(afterEntity.Mgr.HasValue, "�X�V���Ă��Ȃ��v���p�e�B��null�X�V3");
                Assert.IsFalse(afterEntity.Sal.HasValue, "�X�V���Ă��Ȃ��v���p�e�B��null�X�V4");
            }
        }

        [Test, S2(Tx.Rollback)]
        public void TestExecuteNoModifiedPropertyNamesAndMethodTx()
        {
            IDaoMetaData dmd = CreateDaoMetaData(typeof(IEmployeeNoModifiedPropertyNamesAndMethodDao));
            ISqlCommand updateCommand1 = dmd.GetSqlCommand("UpdateModifiedOnly");
            ISqlCommand selectCommand = dmd.GetSqlCommand("GetEmployee");
            const int TEST_EMP_NO = 7369;
            {
                EmployeeNoModifiedPropertyNamesAndMethod entity = selectCommand.Execute(new object[] { TEST_EMP_NO }) as EmployeeNoModifiedPropertyNamesAndMethod;
                entity.Empno = TEST_EMP_NO;
                entity.JobName = "Hoge";

                try
                {
                    updateCommand1.Execute(new object[] { entity });
                    Assert.Fail("ModifiedOnlyPropertyNames���Ȃ��ꍇ�͗�O����������B");
                }
                catch ( NotFoundModifiedPropertiesRuntimeException ex )
                {
                    Console.WriteLine(ex.Message);
                }
            }
            ISqlCommand updateCommand2 = dmd.GetSqlCommand("Update");
            {
                EmployeeNoModifiedPropertyNamesAndMethod entity = new EmployeeNoModifiedPropertyNamesAndMethod();
                entity.Empno = TEST_EMP_NO;
                entity.JobName = "Hoge";

                int modifiedCount = (int)updateCommand2.Execute(new object[] { entity });

                Assert.IsTrue(modifiedCount > 0, "�ʏ��update�͗�O�ƂȂ炸�Ɏ��s�����");
                EmployeeNoModifiedPropertyNamesAndMethod afterEntity = selectCommand.Execute(new object[] { TEST_EMP_NO }) as EmployeeNoModifiedPropertyNamesAndMethod;
                Assert.IsNotNull(afterEntity, "�X�V�����G���e�B�e�B�����݂���");
                Assert.AreEqual(entity.JobName, afterEntity.JobName);
                Assert.IsNull(afterEntity.Ename, "�X�V���Ă��Ȃ��v���p�e�B��null�X�V1");
                Assert.IsFalse(afterEntity.Hiredate.HasValue, "�X�V���Ă��Ȃ��v���p�e�B��null�X�V2");
                Assert.IsFalse(afterEntity.Mgr.HasValue, "�X�V���Ă��Ȃ��v���p�e�B��null�X�V3");
                Assert.IsFalse(afterEntity.Sal.HasValue, "�X�V���Ă��Ȃ��v���p�e�B��null�X�V4");
            }
        }
    }
}
