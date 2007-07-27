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
using Seasar.Dao.Attrs;

namespace Seasar.S2FormExample.Logics.Dto
{
    /// <summary>
    /// �Ј��pDTO
    /// </summary>
    [Table("T_EMP")]
    public class EmployeeDto
    {
        /// <summary>
        /// �Ј��R�[�h
        /// </summary>
        private string _code;

        /// <summary>
        /// ����
        /// </summary>
        private DepartmentDto _department;

        /// <summary>
        /// ����ID
        /// </summary>
        private int? _deptNo;

        /// <summary>
        /// ���Г�
        /// </summary>
        private DateTime? _entryDay;

        /// <summary>
        /// ����ID
        /// </summary>
        private int _genderId;

        /// <summary>
        /// �Ј�ID
        /// </summary>
        private int? _id;

        /// <summary>
        /// �Ј���
        /// </summary>
        private string _name;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public EmployeeDto()
        {
            _id = null;
            _code = "";
            _name = "";
        }

        /// <summary>
        /// �Ј�ID
        /// </summary>
        [Column("n_id")]
        public int? Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// �Ј��R�[�h
        /// </summary>
        [Column("s_code")]
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        /// <summary>
        /// �Ј���
        /// </summary>
        [Column("s_name")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// ����ID
        /// </summary>
        [Column("n_gender")]
        public int Gender
        {
            get { return _genderId; }
            set { _genderId = value; }
        }

        /// <summary>
        /// ���Г�
        /// </summary>
        [Column("d_entry")]
        public DateTime? EntryDay
        {
            get { return _entryDay; }
            set { _entryDay = value; }
        }

        /// <summary>
        /// ����ID
        /// </summary>
        [Column("n_dept_id")]
        public int? DeptNo
        {
            get { return _deptNo; }
            set { _deptNo = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        [Relno(0), Relkeys("n_dept_id:n_id")]
        public DepartmentDto Department
        {
            get { return _department; }
            set { _department = value; }
        }

        /// <summary>
        /// ���喼
        /// </summary>
        public string DeptName
        {
            get { return _department.Name; }
        }
    }
}