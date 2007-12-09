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

namespace Seasar.S2FormExample.Logics.Page
{
    /// <summary>
    /// �Ј��ҏWPage�N���X
    /// </summary>
    public class EmployeeEditPage
    {
        private int? _id;
        private string _code;
        private string _name;
        private int _gender;
        private DateTime? _entry;
        private int? _depart;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public EmployeeEditPage()
        {
            _entry = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
        }

        /// <summary>
        /// �Ј�ID
        /// </summary>
        public int? Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// �Ј��R�[�h
        /// </summary>
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        /// <summary>
        /// �Ј���
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// ����ID
        /// </summary>
        public int Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        /// <summary>
        /// ���Г�
        /// </summary>
        public DateTime? Entry
        {
            get { return _entry; }
            set { _entry = value; }
        }

        /// <summary>
        /// ����ID
        /// </summary>
        public int? Depart
        {
            get { return _depart; }
            set { _depart = value; }
        }
    }
}