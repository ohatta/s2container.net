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
#endif
using Seasar.Dao.Attrs;
using Seasar.WindowsExample.Logics.Dto;

namespace Seasar.WindowsExample.Logics.Dao
{
    /// <summary>
    /// �Ј��pDAO
    /// </summary>
    [Bean(typeof (EmployeeDto))]
    public interface IEmployeeDao
    {
#if NET_1_1
        // NET 1.1
        /// <summary>
        /// �Ј��ꗗ���擾����
        /// </summary>
        /// <returns>�Ј��ꗗ</returns>
        [Query("order by t_emp.n_id")]
        IList GetAll();
#else
        // NET 2.0
        /// <summary>
        /// �Ј��ꗗ���擾����
        /// </summary>
        /// <returns>�Ј��ꗗ</returns>
        [Query("order by t_emp.n_id")]
        IList<EmployeeDto> GetAll();
#endif
        /// <summary>
        /// �Ј��f�[�^���擾����
        /// </summary>
        /// <param name="id">�Ј�ID</param>
        /// <returns>�Ј��f�[�^</returns>
        [Query("t_emp.n_id = /*id*/1")]
        EmployeeDto GetData(int id);

        /// <summary>
        /// �Ј�ID���擾����
        /// </summary>
        /// <param name="code">�Ј��R�[�h</param>
        /// <returns>�Ј�ID</returns>
        [Sql("select n_id from t_emp where s_code = /*code*/'000001'")]
        int GetId(string code);

        /// <summary>
        /// �Ј��f�[�^��}������
        /// </summary>
        /// <param name="data">�}������f�[�^</param>
        /// <returns>�}������</returns>
        [NoPersistentProps("Id")]
        int InsertData(EmployeeDto data);

        /// <summary>
        /// �Ј��f�[�^���X�V����
        /// </summary>
        /// <param name="data">�X�V����f�[�^</param>
        /// <returns>�X�V����</returns>
        int UpdateData(EmployeeDto data);

        /// <summary>
        /// �Ј��f�[�^���폜����
        /// </summary>
        /// <param name="data">�Ј��f�[�^</param>
        /// <returns>�폜����</returns>
        int DeleteData(EmployeeDto data);
    }
}