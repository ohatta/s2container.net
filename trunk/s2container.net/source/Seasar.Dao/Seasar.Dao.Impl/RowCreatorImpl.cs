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
using System.Collections;
using System.Data;
using Seasar.Extension.ADO;
using Seasar.Framework.Util;

namespace Seasar.Dao.Impl
{
    public class RowCreatorImpl : IRowCreator
    {
        /// <summary>
        /// 1�s���̃I�u�W�F�N�g���쐬����
        /// </summary>
        /// <param name="reader">IDataReader</param>
        /// <param name="columns">Column�̃��^�f�[�^</param>
        /// <param name="beanType">�I�u�W�F�N�g�̌^</param>
        /// <returns>1�s����Entity�^�̃I�u�W�F�N�g</returns>
        public virtual object CreateRow(IDataReader reader, IColumnMetaData[] columns, Type beanType) {
            object row = NewBean(beanType);
            foreach (IColumnMetaData column in columns) {
                object value = column.ValueType.GetValue(reader, column.ColumnName);
                column.PropertyInfo.SetValue(row, value, null);
            }
            return row;
        }

        protected virtual object NewBean(Type beanType) {
            return ClassUtil.NewInstance(beanType);
        }

        /// <summary>
        /// Column�̃��^�f�[�^���쐬����
        /// </summary>
        /// <param name="columnNames">�J�������̃��X�g</param>
        /// <param name="beanMetaData">���^���</param>
        /// <returns>Column�̃��^�f�[�^�̔z��</returns>
        public virtual IColumnMetaData[] CreateColumnMetaData(IList columnNames, IBeanMetaData beanMetaData)
        {
            System.Collections.Generic.IDictionary<string, string> names = null;
            System.Collections.Generic.List<IColumnMetaData> columnMetaDataList =
                new System.Collections.Generic.List<IColumnMetaData>();

            for (int i = 0; i < beanMetaData.PropertyTypeSize; ++i)
            {
                IPropertyType pt = beanMetaData.GetPropertyType(i);

                // [DAONET-56] (2007/08/29)
                // Performance����̂��߂�Setter�̖���Property�͑Ώۂɂ��Ȃ��悤����B
                if (!IsTargetProperty(pt)) {
                    continue;
                }

                string columnName;

                columnName = FindColumnName(columnNames, pt.ColumnName);

                if (columnName != null)
                {
                    columnMetaDataList.Add(NewColumnMetaDataImpl(pt, columnName));
                    continue;
                }

                columnName = FindColumnName(columnNames, pt.PropertyName);

                if (columnName != null)
                {
                    columnMetaDataList.Add(NewColumnMetaDataImpl(pt, columnName));
                    continue;
                }

                if (!pt.IsPersistent)
                {
                    if (names == null)
                    {
                        names = new System.Collections.Generic.Dictionary<string, string>();
                        foreach (string name in columnNames)
                        {
                            names[name.Replace("_", string.Empty).ToUpper()] = name;
                        }
                    }
                    if (names.ContainsKey(pt.ColumnName.ToUpper()))
                    {
                        columnMetaDataList.Add(NewColumnMetaDataImpl(
                            pt, names[pt.ColumnName.ToUpper()]));
                    }

                }
            }
            return columnMetaDataList.ToArray();
        }

        /// <summary>
        /// �J�������̃��X�g����啶������������ʂ����Ɉ�v����J��������T��
        /// </summary>
        /// <param name="columnNames">�����Ώۂ̃J�������̃��X�g</param>
        /// <param name="columnName">�T���o���J������</param>
        /// <returns>���������J�������i�J�������̃��X�g����擾�����J�������j</returns>
        protected virtual string FindColumnName(IList columnNames, string columnName)
        {
            foreach (string realColumnName in columnNames)
            {
                if (string.Compare(realColumnName, columnName, true) == 0)
                {
                    return realColumnName;
                }
            }
            return null;
        }

        protected virtual bool IsTargetProperty(IPropertyType pt) {
            return pt.PropertyInfo.CanWrite;
        }

        protected virtual ColumnMetaDataImpl NewColumnMetaDataImpl(IPropertyType propertyType, string columnName) {
            return new ColumnMetaDataImpl(propertyType, columnName);
        }
    }
}
