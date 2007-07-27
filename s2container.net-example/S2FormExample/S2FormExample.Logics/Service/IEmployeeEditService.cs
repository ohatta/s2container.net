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

using Seasar.Quill.Attrs;
using Seasar.S2FormExample.Logics.Page;
using Seasar.S2FormExample.Logics.Service.Impl;

namespace Seasar.S2FormExample.Logics.Service
{
    /// <summary>
    /// �Ј��o�^�p�T�[�r�X�p�C���^�[�t�F�C�X
    /// </summary>
    [Implementation(typeof (EmployeeEditServiceImpl))]
    public interface IEmployeeEditService : IBaseService
    {
        /// <summary>
        /// �Ј��f�[�^���擾����
        /// </summary>
        /// <param name="id">�Ј�ID</param>
        /// <returns>�Ј��f�[�^</returns>
        EmployeeEditPage GetData(int id);

        /// <summary>
        /// �Ј��f�[�^��o�^����
        /// </summary>
        /// <param name="data">�o�^�Ј��f�[�^</param>
        /// <returns>�o�^����</returns>
        int ExecUpdate(EmployeeEditPage data);

        /// <summary>
        /// �Ј��f�[�^���폜����
        /// </summary>
        /// <param name="id">�폜�Ј�ID</param>
        /// <returns>�폜����</returns>
        int ExecDelete(int id);
    }
}