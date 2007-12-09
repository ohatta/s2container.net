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
using Seasar.Framework.Util;

namespace Seasar.Extension.ADO.Impl
{
    public class DatabaseMetaDataImpl : IDatabaseMetaData
    {
#if NET_1_1
        private IDictionary _primaryKeys = new Hashtable(
            new CaseInsensitiveHashCodeProvider(), new CaseInsensitiveComparer());
        private IDictionary _columns = new Hashtable(
            new CaseInsensitiveHashCodeProvider(), new CaseInsensitiveComparer());
        private IDictionary _autoIncrementColumns = new Hashtable(
            new CaseInsensitiveHashCodeProvider(), new CaseInsensitiveComparer());
#else
        private readonly IDictionary _primaryKeys = new Hashtable(StringComparer.CurrentCultureIgnoreCase);
        private readonly IDictionary _columns = new Hashtable(StringComparer.CurrentCultureIgnoreCase);
        private readonly IDictionary _autoIncrementColumns = new Hashtable(StringComparer.CurrentCultureIgnoreCase);
#endif

        private readonly IDataSource _dataSource;

        public DatabaseMetaDataImpl(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        #region IDatabaseMetaData �����o

        public IList GetPrimaryKeySet(string tableName)
        {
            if(!_primaryKeys.Contains(tableName)) CreateTableMetaData(tableName);
            return (IList) _primaryKeys[tableName];
        }

        public IList GetColumnSet(string tableName)
        {
            if(!_columns.Contains(tableName)) CreateTableMetaData(tableName);
            return (IList) _columns[tableName];
        }

        public IList GetAutoIncrementColumnSet(string tableName)
        {
            if (!_autoIncrementColumns.Contains(tableName)) CreateTableMetaData(tableName);
            return (IList) _autoIncrementColumns[tableName];
        }

        #endregion

        /// <summary>
        /// �e�[�u����`�����쐬����
        /// </summary>
        /// <param name="tableName">�e�[�u����</param>
        private void CreateTableMetaData(string tableName)
        {
            lock(this)
            {
                // IDbConnection���擾����
                IDbConnection cn = DataSourceUtil.GetConnection(_dataSource);
                try
                {
                    // �e�[�u����`�����擾���邽�߂�SQL���쐬����
                    string sql = "SELECT * FROM " + tableName;

                    // IDbCommand���擾����
                    IDbCommand cmd = _dataSource.GetCommand(sql, cn);

                    // Transaction�̏������s��
                    _dataSource.SetTransaction(cmd);

                    // IDataAdapter���擾����
                    IDataAdapter adapter = _dataSource.GetDataAdapter(cmd);

                    // �e�[�u����`
                    DataTable[] metaDataTables;

                    // �e�[�u����`�����擾����
                    try
                    {
                        metaDataTables = adapter.FillSchema(new DataSet(), SchemaType.Mapped);
                    }
                    catch
                    {
                        return;
                    }

                    // �e�[�u����`��񂩂�v���C�}���L�[���擾����
                    _primaryKeys[tableName] = GetPrimaryKeySet(metaDataTables[0].PrimaryKey);

                    // �e�[�u����`��񂩂�J�������擾����
                    _columns[tableName] = GetColumnSet(metaDataTables[0].Columns);

                    // �e�[�u����`��񂩂�AutoIncrement�J�������擾����
                    _autoIncrementColumns[tableName] = GetAutoIncrementColumnSet(metaDataTables[0].Columns);
                }
                finally
                {
                    // IDbConnection��Close�������s��
                    _dataSource.CloseConnection(cn);
                }
            }
        }

        private IList GetPrimaryKeySet(DataColumn[] primarykeys)
        {
            IList list = new CaseInsentiveSet();
            foreach (DataColumn pkey in primarykeys)
            {
                list.Add(pkey.ColumnName);
            }
            return list;
        }

        private IList GetColumnSet(DataColumnCollection columns)
        {
            IList list = new CaseInsentiveSet();
            foreach (DataColumn column in columns)
            {
                list.Add(column.ColumnName);
            }
            return list;
        }

        private IList GetAutoIncrementColumnSet(DataColumnCollection columns)
        {
            IList list = new CaseInsentiveSet();
            foreach (DataColumn column in columns)
            {
                if (column.AutoIncrement)
                {
                    list.Add(column.ColumnName);
                }
            }
            return list;
        }
    }
}
