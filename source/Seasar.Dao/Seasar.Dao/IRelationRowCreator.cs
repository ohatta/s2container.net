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
using System.Data;
using Seasar.Extension.ADO;

namespace Seasar.Dao
{
    public interface IRelationRowCreator
    {
        /// <summary>
        /// 1�s���̃I�u�W�F�N�g���쐬����
        /// </summary>
        /// <returns>1�s����Entity�^�̃I�u�W�F�N�g</returns>
        object CreateRelationRow(IDataReader reader, IRelationPropertyType rpt,
            System.Collections.IList columnNames, System.Collections.Hashtable relKeyValues,
            IDictionary<String, IDictionary<String, IPropertyType>> relationPropertyCache);

        /// <summary>
        /// �֘A�̃v���p�e�B�L���b�V�����쐬����
        /// </summary>
        /// <param name="columnNames">�J�������̃��X�g</param>
        /// <param name="bmd">���^���</param>
        /// <returns>�֘A�̃v���p�e�B�L���b�V��</returns>
        IDictionary<String, IDictionary<String, IPropertyType>> CreateRelationPropertyCache(System.Collections.IList columnNames, IBeanMetaData bmd);
    }
}
