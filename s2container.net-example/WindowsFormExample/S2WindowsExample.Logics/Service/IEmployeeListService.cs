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

#if NET_1_1
// NET 1.1
using System.Collections;
#else
// NET 2.0
using System.Collections.Generic;
using Seasar.WindowsExample.Logics.Dto;
#endif

namespace Seasar.WindowsExample.Logics.Service
{
    /// <summary>
    /// 社員リストサービス用インターフェイス
    /// </summary>
    public interface IEmployeeListService
    {
#if NET_1_1
        // NET 1.1
        /// <summary>
        /// 社員一覧を取得する
        /// </summary>
        /// <returns>社員一覧</returns>
        IList GetAll();
#else
        // NET 1.1
        /// <summary>
        /// 社員一覧を取得する
        /// </summary>
        /// <returns>社員一覧</returns>
        IList<EmployeeDto> GetAll();
#endif
        /// <summary>
        /// CSVで出力する
        /// </summary>
        /// <param name="path">出力先パス</param>
        /// <returns>出力件数</returns>
        int OutputCSV(string path);
    }
}