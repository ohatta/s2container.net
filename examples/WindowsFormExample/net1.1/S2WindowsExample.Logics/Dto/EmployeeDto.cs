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

using Nullables;
using Seasar.Dao.Attrs;

namespace Seasar.WindowsExample.Logics.Dto
{
    /// <summary>
    /// �Ј��pDTO
    /// </summary>
    [Table("T_EMP")]
    public class EmployeeDto
    {
        /// <summary>
        /// �Ј�ID
        /// </summary>
        private NullableInt32 _id;

        /// <summary>
        /// �Ј��R�[�h
        /// </summary>
        private string _code;

        /// <summary>
        /// �Ј���
        /// </summary>
        private string _name;

        /// <summary>
        /// ����
        /// </summary>
        private int _gender;

        /// <summary>
        /// ���Г�
        /// </summary>
        private NullableDateTime _entryDay;

        /// <summary>
        /// ����ID
        /// </summary>
        private NullableInt32 _deptNo;

        /// <summary>
        /// ����
        /// </summary>
        private DepartmentDto _department;

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
        public NullableInt32 Id
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
        /// ����
        /// </summary>
        [Column("n_gender")]
        public int Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        /// <summary>
        /// ���Г�
        /// </summary>
        [Column("d_entry")]
        public NullableDateTime EntryDay
        {
            get { return _entryDay; }
            set { _entryDay = value; }
        }

        /// <summary>
        /// ����ID
        /// </summary>
        [Column("n_dept_id")]
        public NullableInt32 DeptNo
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