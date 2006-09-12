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
using System.Collections;
using Seasar.WindowsExample.Logics.Dao;

namespace Seasar.WindowsExample.Logics.Service.Impl
{
    /// <summary>
    /// �Ј����X�g�T�[�r�X�p�����N���X
    /// </summary>
    public class EmployeeListServiceImpl : IEmployeeListService
    {
        /// <summary>
        /// �Ј�DAO
        /// </summary>
        private IEmployeeDao _daoOfEmployee;

        /// <summary>
        /// CSV�p�Ј�DAO
        /// </summary>
        private IEmployeeCSVDao _daoOfCsv;

        /// <summary>
        /// �o�͗pDAO
        /// </summary>
        private IOutputCSVDao _daoOfOutput;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public EmployeeListServiceImpl()
        {
            ;
        }

        /// <summary>
        /// �Ј�DAO
        /// </summary>
        public IEmployeeDao DaoOfEmployee
        {
            get { return _daoOfEmployee; }
            set { _daoOfEmployee = value; }
        }

        /// <summary>
        /// CSV�p�Ј�DAO
        /// </summary>
        public IEmployeeCSVDao DaoOfCsv
        {
            get { return _daoOfCsv; }
            set { _daoOfCsv = value; }
        }

        /// <summary>
        /// �o�͗pDAO
        /// </summary>
        public IOutputCSVDao DaoOfOutput
        {
            get { return _daoOfOutput; }
            set { _daoOfOutput = value; }
        }

        /// <summary>
        /// �Ј��ꗗ���擾����
        /// </summary>
        /// <returns>�Ј��ꗗ</returns>
        public IList GetAll()
        {
            return ( _daoOfEmployee.GetAll() );
        }

        /// <summary>
        /// CSV�ŏo�͂���
        /// </summary>
        /// <param name="path">�o�͐�p�X</param>
        /// <returns>�o�͌���</returns>
        public int OutputCSV(string path)
        {
            if ( path == "" )
                throw new ArgumentNullException("path");

            IList list = _daoOfCsv.GetAll();
            if ( list.Count == 0 )
                return 0;

            return ( _daoOfOutput.OutputEmployeeList(path, list) );
        }
    }
}