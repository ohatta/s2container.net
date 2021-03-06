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

using System.Collections.Generic;
using Seasar.Dao.Attrs;
using Seasar.Quill.Attrs;
using Seasar.S2FormExample.Logics.Dto;

namespace Seasar.S2FormExample.Logics.Dao
{
    /// <summary>
    /// CSV用社員DAO
    /// </summary>
    [S2Dao]
    [Implementation]
    [Bean(typeof(EmployeeCsvDto))]
    public interface IEmployeeCSVDao
    {
        /// <summary>
        /// 社員を一覧で取得する
        /// </summary>
        /// <returns>社員一覧</returns>
        IList<EmployeeCsvDto> GetAll();
    }
}