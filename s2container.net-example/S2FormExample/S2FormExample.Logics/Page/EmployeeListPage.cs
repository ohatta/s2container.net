#region Copyright

/*
S * Copyright 2005-2008 the Seasar Foundation and the Others.
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

using System.Collections.Generic;
using Seasar.S2FormExample.Logics.Dto;

namespace Seasar.S2FormExample.Logics.Page
{
    /// <summary>
    /// �Ј��ꗗPage�N���X
    /// </summary>
    public class EmployeeListPage
    {
        private string _genderId;
        private string _genderName;
        private IList<EmployeeDto> _list;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public EmployeeListPage()
        {
            _list = new List<EmployeeDto>();
        }

        /// <summary>
        /// ����ID
        /// </summary>
        public string GenderId
        {
            get { return _genderId; }
            set { _genderId = value; }
        }

        /// <summary>
        /// ���ʖ�
        /// </summary>
        public string GenderName
        {
            get { return _genderName; }
            set { _genderName = value; }
        }

        /// <summary>
        /// �Ј����X�g
        /// </summary>
        public IList<EmployeeDto> List
        {
            get { return _list; }
            set { _list = value; }
        }
    }
}