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

using System;
using Seasar.Quill.Attrs;
using Seasar.S2FormExample.Logics.Dao;
using Seasar.S2FormExample.Logics.Dto;
using Seasar.S2FormExample.Logics.Page;

namespace Seasar.S2FormExample.Logics.Service.Impl
{
    /// <summary>
    /// �Ј��o�^�p�T�[�r�X�p�����N���X
    /// </summary>
    public class EmployeeEditServiceImpl : BaseServiceImpl, IEmployeeEditService
    {
        /// <summary>
        /// �Ј�DAO
        /// </summary>
        protected IEmployeeDao dao;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public EmployeeEditServiceImpl()
        {
            ;
        }

        #region IEmployeeEditService Members

        /// <summary>
        /// �Ј��f�[�^���擾����
        /// </summary>
        /// <param name="id">�Ј�ID</param>
        /// <returns>�Ј��f�[�^</returns>
        public EmployeeEditPage GetData(int id)
        {
            EmployeeEditPage page = new EmployeeEditPage();

            EmployeeDto dto = dao.GetData(id);
            if (dto != null)
            {
                page.Code = dto.Code;
                page.Depart = dto.DeptNo;
                page.Entry = dto.EntryDay;
                page.Gender = dto.Gender;
                page.Id = dto.Id;
                page.Name = dto.Name;
            }
            else
            {
                page = null;
            }
            return page;
        }

        /// <summary>
        /// �Ј��f�[�^��o�^����
        /// </summary>
        /// <param name="data">�o�^�Ј��f�[�^</param>
        /// <returns>�o�^����</returns>
        [Transaction]
        public virtual int ExecUpdate(EmployeeEditPage data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            EmployeeDto dto = new EmployeeDto();
            dto.Code = data.Code;
            dto.DeptNo = data.Depart;
            dto.EntryDay = data.Entry;
            dto.Gender = data.Gender;
            dto.Id = data.Id;
            dto.Name = data.Name;

            if (data.Id.HasValue)
            {
                EmployeeDto e1 = dao.GetData(data.Id.Value);
                if (e1 != null)
                    return (dao.UpdateData(dto));
                else
                    return (dao.InsertData(dto));
            }
            else
            {
                return (dao.InsertData(dto));
            }
        }

        /// <summary>
        /// �Ј��f�[�^���폜����
        /// </summary>
        /// <param name="id">�폜�Ј�ID</param>
        /// <returns>�폜����</returns>
        [Transaction]
        public virtual int ExecDelete(int id)
        {
            EmployeeDto data = new EmployeeDto();
            data.Id = id;

            return (dao.DeleteData(data));
        }

        #endregion
    }
}