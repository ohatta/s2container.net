#region Copyright
/*
 * Copyright 2005-2013 the Seasar Foundation and the Others.
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
using Seasar.Framework.Util;

namespace Seasar.Dao.Impl
{
    public abstract class AbstractBeanMetaDataDataReaderHandler
        : IDataReaderHandler
    {
        private readonly IBeanMetaData _beanMetaData;

        private string[] _clearModifiedOnlyPropertyNamePrefixes = new string[] { "Clear" }; // [DAONET-57] 2007/10/02

        protected IRowCreator _rowCreator;// [DAONET-56] (2007/08/29)

        protected IRelationRowCreator _relationRowCreator;// [DAONET-56] (2007/08/29)

        /// <summary>
        /// �R���X�g���N�^�B
        /// </summary>
        /// <param name="beanMetaData">Bean meta data. (NotNull)</param>
        /// <param name="rowCreator">Row creator. (NotNull)</param>
        /// <param name="relationRowCreator">Relation row creator. (NotNull)</param>
        public AbstractBeanMetaDataDataReaderHandler(IBeanMetaData beanMetaData, IRowCreator rowCreator, IRelationRowCreator relationRowCreator)
        {
            _beanMetaData = beanMetaData;
            _rowCreator = rowCreator;
            _relationRowCreator = relationRowCreator;
        }

        public IBeanMetaData BeanMetaData
        {
            get { return _beanMetaData; }
        }

        /// <summary>
        /// Column�̃��^�f�[�^���쐬����
        /// </summary>
        /// <param name="columnNames">�J�������̃��X�g (NotNull)</param>
        /// <returns>Column�̃��^�f�[�^�̔z�� (NotNull)</returns>
        protected virtual IColumnMetaData[] CreateColumnMetaData(System.Collections.IList columnNames)
        {
            return _rowCreator.CreateColumnMetaData(columnNames, _beanMetaData);
        }

        /// <summary>
        /// 1�s���̃I�u�W�F�N�g���쐬����
        /// </summary>
        /// <param name="reader">IDataReader (NotNull)</param>
        /// <param name="columns">Column�̃��^�f�[�^ (NotNull)</param>
        /// <returns>1�s����Entity�^�̃I�u�W�F�N�g (NotNull)</returns>
        protected virtual object CreateRow(IDataReader reader, IColumnMetaData[] columns)
        {
            object row = _rowCreator.CreateRow(reader, columns, _beanMetaData.BeanType);
            if ( row != null )
            {
                BeanMetaData.ClearModifiedPropertyNames(row);
            }
            return row;
        }

        /// <summary>
        /// �֘A�I�u�W�F�N�g�̃v���p�e�B�L���b�V�����쐬����
        /// </summary>
        /// <param name="columnNames">�J�������̃��X�g (NotNull)</param>
        /// <returns>�֘A�I�u�W�F�N�g�̃v���p�e�B�L���b�V�� (NotNull)</returns>
        protected virtual IDictionary<string, IDictionary<string, IPropertyType>> CreateRelationPropertyCache(System.Collections.IList columnNames)
        {
            return _relationRowCreator.CreateRelationPropertyCache(columnNames, _beanMetaData);
        }

        /// <summary>
        /// 1�s���̃I�u�W�F�N�g�Ɋ֘A����I�u�W�F�N�g���쐬����
        /// </summary>
        /// <param name="reader">IDataReader</param>
        /// <param name="rpt">Relation property type. (NotNull)</param>
        /// <param name="columnNames">The list of column name. (NotNull)</param>
        /// <param name="relKeyValues">The hashtable of rel key values. (NotNull)</param>
        /// <param name="relationColumnMetaDataCache">The dictionary of relation property cache. (NotNull)</param>
        /// <returns>1�s����Entity�^�̃I�u�W�F�N�g (Nullable)</returns>
        protected virtual object CreateRelationRow(IDataReader reader, IRelationPropertyType rpt,
            System.Collections.IList columnNames, System.Collections.Hashtable relKeyValues,
            IDictionary<String, IDictionary<String, IPropertyType>> relationColumnMetaDataCache)
        {
            object relationRow = _relationRowCreator.CreateRelationRow(reader, rpt, columnNames, relKeyValues, relationColumnMetaDataCache);
            if ( relationRow != null )
            {
                rpt.BeanMetaData.ClearModifiedPropertyNames(relationRow);
            }
            return relationRow;
        }

        protected virtual bool IsTargetProperty(IPropertyType pt)
        {// [DAONET-56] (2007/08/29)
            return pt.PropertyInfo.CanWrite;
        }

        protected virtual object CreateRelationRow(IRelationPropertyType rpt)
        {
            return ClassUtil.NewInstance(rpt.PropertyInfo.PropertyType);
        }

        protected virtual System.Collections.IList CreateColumnNames(DataTable dt)
        {
            System.Collections.IList columnNames = new CaseInsentiveSet();
            foreach (DataRow row in dt.Rows)
            {
                string columnName = (string) row["ColumnName"];
                columnNames.Add(columnName);
            }
            return columnNames;
        }

        #region IDataReaderHandler �����o

        public virtual object Handle(IDataReader dataReader)
        {
            return null;
        }

        #endregion
    }
}
