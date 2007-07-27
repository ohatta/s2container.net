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

using System;
using System.Collections.Generic;
using Seasar.S2FormExample.Logics.Dao;
using Seasar.S2FormExample.Logics.Dto;
using Seasar.S2FormExample.Logics.Page;

namespace Seasar.S2FormExample.Logics.Service.Impl
{
    /// <summary>
    /// �Ј����X�g�T�[�r�X�p�����N���X
    /// </summary>
    public class EmployeeListServiceImpl : BaseServiceImpl, IEmployeeListService
    {
        /// <summary>
        /// CSV�p�Ј�DAO
        /// </summary>
        protected IEmployeeCSVDao daoOfCsv;

        /// <summary>
        /// �Ј�DAO
        /// </summary>
        protected IEmployeeDao daoOfEmployee;

        /// <summary>
        /// �o�͗pDAO
        /// </summary>
        protected IOutputCSVDao daoOfOutput;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public EmployeeListServiceImpl()
        {
            ;
        }

        #region IEmployeeListService Members

        /// <summary>
        /// �Ј��ꗗ���擾����
        /// </summary>
        /// <returns>�Ј��ꗗ</returns>
        public EmployeeListPage GetAll()
        {
            EmployeeListPage page = new EmployeeListPage();

            page.GenderId = "99";
            page.GenderName = "�S��";
            page.List = daoOfEmployee.GetAll();

            return page;
        }


        /// <summary>
        /// �Ј��ꗗ����������
        /// </summary>
        /// <param name="condition">��������</param>
        /// <returns>�Ј��ꗗ</returns>
        public EmployeeListPage Find(EmployeeListPage condition)
        {
            EmployeeListPage page = new EmployeeListPage();
            page.GenderId = condition.GenderId;
            IList<GenderDto> genderList = this.GetGenderAll();
            foreach (GenderDto dto in genderList)
            {
                if (dto.Id == Convert.ToInt32(condition.GenderId))
                    page.GenderName = dto.Name;
            }
            if (condition.GenderId == "99")
                page.GenderName = "�S��";

            IList<EmployeeDto> list;
            if (page.GenderId != "99")
                list = daoOfEmployee.FindByGender(Convert.ToInt32(condition.GenderId));
            else
                list = daoOfEmployee.GetAll();

            if (list != null)
                page.List = list;

            return page;
        }

        /// <summary>
        /// CSV�ŏo�͂���
        /// </summary>
        /// <param name="path">�o�͐�p�X</param>
        /// <returns>�o�͌���</returns>
        public int OutputCSV(string path)
        {
            if (path == "")
                throw new ArgumentNullException("path");

            IList<EmployeeCsvDto> list = daoOfCsv.GetAll();

            if (list.Count == 0)
                return 0;

            return (daoOfOutput.OutputEmployeeList(path, list));
        }

        #endregion
    }
}