#region Copyright
/*
 * Copyright 2005-2013 the Seasar Foundation and the Others.
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
        /// 全ての従業員を取得する
        /// </summary>
        /// <returns>Employeeのリスト</returns>
        IList GetAllEmployees();

        /// <summary>
        /// 従業員番号から従業員を取得する
        /// </summary>
        /// <param name="empno">従業員番号</param>
        /// <returns>従業員</returns>
        [Query("empno=/*empno*/")]
        Employee GetEmployee(int empno);

        /// <summary>
        /// 従業員の件数を取得する
        /// </summary>
        /// <returns>従業員数</returns>
        [Sql("select count(*) from EMP")]
        int GetCount();

        /// <summary>
        /// 従業員を追加する
        /// </summary>
        /// <param name="empno">従業員番号</param>
        /// <param name="ename">従業員名</param>
        /// <returns>追加件数</returns>
        int Insert(int empno, string ename);

        /// <summary>
        /// 従業員を更新する
        /// </summary>
        /// <param name="employee">従業員</param>
        /// <returns>更新件数</returns>
        int Update(Employee employee);

        [Sql("select empno from EMP /*IF emp.Ename != null*/ where ename=/*emp.Ename*/'1' /*END*/")]
        int? GetEmpnoByEmp(Employee emp);

        [Sql("select empno from EMP /*IF hoge.Parent.Val != null*/ where ename=/*hoge.Parent.Val*/'1' /*END*/")]
        SqlInt32 GetEmpnoByHoge(Hoge hoge);

        [Sql("select empno from EMP where ename=/*hoge.Parent.Val*/'1'")]
        SqlInt32 GetEmpnoByHoge2(Hoge hoge);
    }
}
