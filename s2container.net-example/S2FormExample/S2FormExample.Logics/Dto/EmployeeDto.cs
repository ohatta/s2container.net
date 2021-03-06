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
using Seasar.Dao.Attrs;

namespace Seasar.S2FormExample.Logics.Dto
{
    /// <summary>
    /// 社員用DTO
    /// </summary>
    [Table("T_EMP")]
    public class EmployeeDto
    {
        /// <summary>
        /// 社員コード
        /// </summary>
        private string _code;

        /// <summary>
        /// 部門
        /// </summary>
        private DepartmentDto _department;

        /// <summary>
        /// 部門ID
        /// </summary>
        private int? _deptNo;

        /// <summary>
        /// 入社日
        /// </summary>
        private DateTime? _entryDay;

        /// <summary>
        /// 性別ID
        /// </summary>
        private int _genderId;

        /// <summary>
        /// 社員ID
        /// </summary>
        private int? _id;

        /// <summary>
        /// 社員名
        /// </summary>
        private string _name;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public EmployeeDto()
        {
            _id = null;
            _code = "";
            _name = "";
        }

        /// <summary>
        /// 社員ID
        /// </summary>
        [Column("n_id")]
        public int? Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// 社員コード
        /// </summary>
        [Column("s_code")]
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        /// <summary>
        /// 社員名
        /// </summary>
        [Column("s_name")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// 性別ID
        /// </summary>
        [Column("n_gender")]
        public int Gender
        {
            get { return _genderId; }
            set { _genderId = value; }
        }

        /// <summary>
        /// 入社日
        /// </summary>
        [Column("d_entry")]
        public DateTime? EntryDay
        {
            get { return _entryDay; }
            set { _entryDay = value; }
        }

        /// <summary>
        /// 部門ID
        /// </summary>
        [Column("n_dept_id")]
        public int? DeptNo
        {
            get { return _deptNo; }
            set { _deptNo = value; }
        }

        /// <summary>
        /// 部門
        /// </summary>
        [Relno(0), Relkeys("n_dept_id:n_id")]
        public DepartmentDto Department
        {
            get { return _department; }
            set { _department = value; }
        }

        /// <summary>
        /// 部門名
        /// </summary>
        public string DeptName
        {
            get { return _department.Name; }
        }
    }
}