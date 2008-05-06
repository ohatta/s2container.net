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
using Seasar.Quill.Attrs;
using Seasar.S2FormExample.Logics.Dao.Impl;
using Seasar.S2FormExample.Logics.Dto;

namespace Seasar.S2FormExample.Logics.Dao
{
    /// <summary>
    /// CSV�o�͗pDAO�C���^�[�t�F�C�X
    /// </summary>
    [Implementation(typeof(OutputCSVDaoImpl))]
    public interface IOutputCSVDao
    {
        /// <summary>
        /// �Ј��f�[�^���o�͂���
        /// </summary>
        /// <param name="path">�o�͐�p�X</param>
        /// <param name="list">�Ј��f�[�^</param>
        /// <returns>�o�͌���</returns>
        int OutputEmployeeList(string path, IList<EmployeeCsvDto> list);
    }
}