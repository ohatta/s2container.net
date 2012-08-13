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

using Seasar.Dao.Attrs;

namespace Seasar.S2FormExample.Logics.Dto
{
    /// <summary>
    /// ���ʗpDTO
    /// </summary>
    [Table("T_GENDER")]
    public class GenderDto
    {
        /// <summary>
        /// ����ID
        /// </summary>
        private int _id;

        /// <summary>
        /// ���ʖ�
        /// </summary>
        private string _name;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public GenderDto()
        {
            _name = "";
        }

        /// <summary>
        /// ����ID
        /// </summary>
        [Column("N_ID")]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// ���ʖ�
        /// </summary>
        [Column("S_NAME")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}