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

using Seasar.WindowsExample.Logics.Dto;

namespace Seasar.WindowsExample.Logics.Service
{
    /// <summary>
    /// 部門登録サービス用インターフェイス
    /// </summary>
    public interface IDepartmentEditService : IBaseService
    {
        /// <summary>
        /// 部門データを取得する
        /// </summary>
        /// <param name="id">部門ID</param>
        /// <returns>部門データ</returns>
        DepartmentDto GetData(int id);

        /// <summary>
        /// 部門データを登録する
        /// </summary>
        /// <param name="dto">登録部門データ</param>
        /// <returns>登録件数</returns>
        int ExecUpdate(DepartmentDto dto);

        /// <summary>
        /// 部門を削除する
        /// </summary>
        /// <param name="id">部門ID</param>
        /// <returns>削除件数</returns>
        int ExecDelete(int id);
    }
}