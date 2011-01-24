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
    /// ����pDAO
    /// </summary>
    [S2Dao]
    [Implementation]
    [Bean(typeof (DepartmentDto))]
    public interface IDepartmentDao
    {
        /// <summary>
        /// ����ꗗ���擾����
        /// </summary>
        /// <returns>���僊�X�g</returns>
        [Query("order by n_show_order")]
        IList<DepartmentDto> GetAll();

        /// <summary>
        /// ����f�[�^���擾����
        /// </summary>
        /// <param name="id">����ID</param>
        /// <returns>����f�[�^</returns>
        [Query("n_id = /*id*/1")]
        DepartmentDto GetData(int id);

        /// <summary>
        /// ����ID���擾����
        /// </summary>
        /// <param name="code">����R�[�h</param>
        /// <returns>����ID</returns>
        [Sql("select n_id from t_dept where s_code = /*code*/'0002'")]
        int GetId(string code);

        /// <summary>
        /// �����}������
        /// </summary>
        /// <param name="dto">�}������f�[�^</param>
        /// <returns>�}������</returns>
        [NoPersistentProps("Id")]
        int InsertData(DepartmentDto dto);

        /// <summary>
        /// ������X�V����
        /// </summary>
        /// <param name="dto">�X�V�f�[�^</param>
        /// <returns>�X�V����</returns>
        int UpdateData(DepartmentDto dto);

        /// <summary>
        /// ������폜����
        /// </summary>
        /// <param name="dto">�폜�f�[�^</param>
        /// <returns>�폜����</returns>
        int DeleteData(DepartmentDto dto);
    }
}