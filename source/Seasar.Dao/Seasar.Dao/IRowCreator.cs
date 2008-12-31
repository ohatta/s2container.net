#region Copyright
/*
 * Copyright 2005-2009 the Seasar Foundation and the Others.
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
using System.Data;

namespace Seasar.Dao
{
    public interface IRowCreator
    {
        /// <summary>
        /// 1�s���̃I�u�W�F�N�g���쐬����
        /// </summary>
        /// <param name="reader">IDataReader</param>
        /// <param name="columns">Column�̃��^�f�[�^</param>
        /// <param name="beanType">�I�u�W�F�N�g�̌^</param>
        /// <returns>1�s����Entity�^�̃I�u�W�F�N�g</returns>
        object CreateRow(IDataReader reader, IColumnMetaData[] columns, Type beanType);

        /// <summary>
        /// Column�̃��^�f�[�^���쐬����
        /// </summary>
        /// <param name="columnNames">�J�������̃��X�g</param>
        /// <param name="beanMetaData">���^���</param>
        /// <returns>Column�̃��^�f�[�^�̔z��</returns>
        IColumnMetaData[] CreateColumnMetaData(IList columnNames, IBeanMetaData beanMetaData);
    }
}
