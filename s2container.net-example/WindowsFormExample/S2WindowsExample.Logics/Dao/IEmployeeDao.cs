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
using Seasar.Dao.Attrs;
using Seasar.WindowsExample.Logics.Dto;

namespace Seasar.WindowsExample.Logics.Dao
{
    /// <summary>
    /// 社員用DAO
    /// </summary>
    [Bean(typeof (EmployeeDto))]
    public interface IEmployeeDao
    {
        /// <summary>
        /// 社員一覧を取得する
        /// </summary>
        /// <returns>社員一覧</returns>
        [Query("order by t_emp.n_id")]
        IList<EmployeeDto> GetAll();

        /// <summary>
        /// 社員データを取得する
        /// </summary>
        /// <param name="id">社員ID</param>
        /// <returns>社員データ</returns>
        [Query("t_emp.n_id = /*id*/1")]
        EmployeeDto GetData(int id);

        /// <summary>
        /// 社員IDを取得する
        /// </summary>
        /// <param name="code">社員コード</param>
        /// <returns>社員ID</returns>
        [Sql("select n_id from t_emp where s_code = /*code*/'000001'")]
        int GetId(string code);

        /// <summary>
        /// 社員データを挿入する
        /// </summary>
        /// <param name="data">挿入するデータ</param>
        /// <returns>挿入件数</returns>
        [NoPersistentProps("Id")]
        int InsertData(EmployeeDto data);

        /// <summary>
        /// 社員データを更新する
        /// </summary>
        /// <param name="data">更新するデータ</param>
        /// <returns>更新件数</returns>
        int UpdateData(EmployeeDto data);

        /// <summary>
        /// 社員データを削除する
        /// </summary>
        /// <param name="data">社員データ</param>
        /// <returns>削除件数</returns>
        int DeleteData(EmployeeDto data);
    }
}