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
    /// �Ј��o�^�p�T�[�r�X�p�C���^�[�t�F�C�X
    /// </summary>
    public class EmployeeEditServiceImpl : BaseServiceImpl, IEmployeeEditService
    {
        /// <summary>
        /// �Ј�DAO
        /// </summary>
        private IEmployeeDao _dao;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public EmployeeEditServiceImpl()
        {
            ;
        }

        /// <summary>
        /// �Ј�DAO
        /// </summary>
        public IEmployeeDao DaoOfEmp
        {
            get { return _dao; }
            set { _dao = value; }
        }

        /// <summary>
        /// �Ј��f�[�^���擾����
        /// </summary>
        /// <param name="id">�Ј�ID</param>
        /// <returns>�Ј��f�[�^</returns>
        public EmployeeDto GetData(int id)
        {
            return ( _dao.GetData(id) );
        }

        /// <summary>
        /// �Ј��f�[�^��o�^����
        /// </summary>
        /// <param name="data">�o�^�Ј��f�[�^</param>
        /// <returns>�o�^����</returns>
        public int ExecUpdate(EmployeeDto data)
        {
            if ( data == null )
                throw new ArgumentNullException("data");

            if ( data.Id.HasValue )
            {
                EmployeeDto e1 = _dao.GetData(data.Id.Value);
                if ( e1 != null )
                    return ( _dao.UpdateData(data) );
                else
                    return ( _dao.InsertData(data) );
            }
            else
            {
                return ( _dao.InsertData(data) );
            }
        }

        /// <summary>
        /// �Ј��f�[�^���폜����
        /// </summary>
        /// <param name="id">�폜�Ј�ID</param>
        /// <returns>�폜����</returns>
        public int ExecDelete(int id)
        {
            EmployeeDto data = new EmployeeDto();
            data.Id = id;

            return ( _dao.DeleteData(data) );
        }
    }
}