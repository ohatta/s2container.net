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

using System.Collections;
using System.Data.SqlTypes;
using Seasar.Dao.Attrs;

namespace Seasar.Tests.Dao.Interceptors
{
    [Bean(typeof(Employee))]
    public interface IEmployeeDao
    {
        /// <summary>
        /// �S�Ă̏]�ƈ����擾����
        /// </summary>
        /// <returns>Employee�̃��X�g</returns>
        IList GetAllEmployees();

        /// <summary>
        /// �]�ƈ��ԍ�����]�ƈ����擾����
        /// </summary>
        /// <param name="empno">�]�ƈ��ԍ�</param>
        /// <returns>�]�ƈ�</returns>
        [Query("empno=/*empno*/")]
        Employee GetEmployee(int empno);

        /// <summary>
        /// �]�ƈ��̌������擾����
        /// </summary>
        /// <returns>�]�ƈ���</returns>
        [Sql("select count(*) from EMP")]
        int GetCount();

        /// <summary>
        /// �]�ƈ���ǉ�����
        /// </summary>
        /// <param name="empno">�]�ƈ��ԍ�</param>
        /// <param name="ename">�]�ƈ���</param>
        /// <returns>�ǉ�����</returns>
        int Insert(int empno, string ename);

        /// <summary>
        /// �]�ƈ����X�V����
        /// </summary>
        /// <param name="employee">�]�ƈ�</param>
        /// <returns>�X�V����</returns>
        int Update(Employee employee);

        [Sql("select empno from EMP /*IF emp.Ename != null*/ where ename=/*emp.Ename*/'1' /*END*/")]
        int? GetEmpnoByEmp(Employee emp);

        [Sql("select empno from EMP /*IF hoge.Parent.Val != null*/ where ename=/*hoge.Parent.Val*/'1' /*END*/")]
        SqlInt32 GetEmpnoByHoge(Hoge hoge);

        [Sql("select empno from EMP where ename=/*hoge.Parent.Val*/'1'")]
        SqlInt32 GetEmpnoByHoge2(Hoge hoge);
    }
}