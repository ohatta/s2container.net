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
using Nullables;
using Seasar.Dao.Attrs;
#else
// NET 2.0
using System;
using Seasar.Dao.Attrs;
#endif

namespace Seasar.WindowsExample.Logics.Dto
{
    /// <summary>
    /// CSV�p�Ј�DTO
    /// </summary>
    [Table("T_EMP")]
    public class EmployeeCsvDto
    {
        /// <summary>
        /// �Ј��R�[�h
        /// </summary>
        private string _code;

        /// <summary>
        /// �Ј���
        /// </summary>
        private string _name;

        /// <summary>
        /// ����ID
        /// </summary>
        private int _genderId;

        /// <summary>
        /// ���ʖ�
        /// </summary>
        private string _genderName;

#if NET_1_1
        // NET 1.1

        /// <summary>
        /// ���Г�
        /// </summary>
        private NullableDateTime _entryDay;
#else
        // NET 2.0
        /// <summary>
        /// ���Г�
        /// </summary>
        private Nullable<DateTime> _entryDay;
#endif

        /// <summary>
        /// ����R�[�h
        /// </summary>
        private string _deptCode;

        /// <summary>
        /// ���喼
        /// </summary>
        private string _deptName;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public EmployeeCsvDto()
        {
            _code = "";
            _name = "";
            _entryDay = null;
            _deptCode = "";
            _deptName = "";
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
        /// ���ʖ�
        /// </summary>
        [Column("s_gender_name")]
        public string GenderName
        {
            get { return _genderName; }
            set { _genderName = value; }
        }

#if NET_1_1
        // NET 1.1
        /// <summary>
        /// ���Г�
        /// </summary>
        [Column("d_entry")]
        public NullableDateTime EntryDay
        {
            get { return _entryDay; }
            set { _entryDay = value; }
        }
#else
        // NET 2.0
        /// <summary>
        /// ���Г�
        /// </summary>
        [Column("d_entry")]
        public Nullable<DateTime> EntryDay
        {
            get { return _entryDay; }
            set { _entryDay = value; }
        }
#endif

        /// <summary>
        /// ����R�[�h
        /// </summary>
        [Column("s_dept_code")]
        public string DeptCode
        {
            get { return _deptCode; }
            set { _deptCode = value; }
        }

        /// <summary>
        /// ���喼
        /// </summary>
        [Column("s_dept_name")]
        public string DeptName
        {
            get { return _deptName; }
            set { _deptName = value; }
        }
    }
}