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

using Seasar.Dao.Attrs;

namespace Seasar.Dao.Examples.AutoUpdate
{
    [Bean(typeof(Employee))]
    public interface IEmployeeDao
    {
        /// <summary>
        /// 従業員を更新します。
        /// </summary>
        /// <param name="emp">従業員</param>
        /// <returns>更新された数</returns>
        int UpdateEmployee(Employee emp);

        /// <summary>
        /// 従業員番号から従業員を取得します。
        /// </summary>
        /// <param name="empno">従業員番号</param>
        /// <returns>従業員</returns>
        [Query("empno=/*empno*/")]
        Employee GetEmployeeByEmpno(int empno);
    }
}
