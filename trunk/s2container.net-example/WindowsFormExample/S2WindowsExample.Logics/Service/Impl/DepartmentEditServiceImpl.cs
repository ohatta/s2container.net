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

using System;
using Seasar.WindowsExample.Logics.Dao;
using Seasar.WindowsExample.Logics.Dto;

namespace Seasar.WindowsExample.Logics.Service.Impl
{
    /// <summary>
    /// ����o�^�T�[�r�X�p�����N���X
    /// </summary>
    public class DepartmentEditServiceImpl : BaseServiceImpl, IDepartmentEditService
    {
        /// <summary>
        /// ����pDAO
        /// </summary>
        private IDepartmentDao _dao;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="dao">����pDAO</param>
        public DepartmentEditServiceImpl(IDepartmentDao dao)
        {
            _dao = dao;
        }

        /// <summary>
        /// ����f�[�^���擾����
        /// </summary>
        /// <param name="id">����ID</param>
        /// <returns>����f�[�^</returns>
        public DepartmentDto GetData(int id)
        {
            return ( _dao.GetData(id) );
        }

        /// <summary>
        /// ����f�[�^��o�^����
        /// </summary>
        /// <param name="dto">����f�[�^</param>
        /// <returns>�o�^����</returns>
        public int ExecUpdate(DepartmentDto dto)
        {
            if ( dto == null )
                throw new ArgumentNullException("dto");

            if ( dto.Id.HasValue )
            {
                DepartmentDto data = _dao.GetData(dto.Id.Value);
                if ( data != null )
                    return ( _dao.UpdateData(dto) );
                else
                    return ( _dao.InsertData(dto) );
            }
            else
            {
                return ( _dao.InsertData(dto) );
            }            
        }

        /// <summary>
        /// ������폜����
        /// </summary>
        /// <param name="id">����ID</param>
        /// <returns>�폜����</returns>
        public int ExecDelete(int id)
        {
            DepartmentDto data = new DepartmentDto();
            data.Id = id;

            return ( _dao.DeleteData(data) );
        }
    }
}