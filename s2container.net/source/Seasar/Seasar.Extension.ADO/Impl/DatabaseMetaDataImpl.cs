#region Copyright
/*
 * Copyright 2005-2015 the Seasar Foundation and the Others.
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
using System.Collections.Generic;
using System.Data;
using Seasar.Framework.Util;

namespace Seasar.Extension.ADO.Impl
{
    public class DatabaseMetaDataImpl : IDatabaseMetaData
    {
        private readonly IDictionary _primaryKeys = new Hashtable(StringComparer.CurrentCultureIgnoreCase);
        private readonly IDictionary _columns = new Hashtable(StringComparer.CurrentCultureIgnoreCase);
        private readonly IDictionary _autoIncrementColumns = new Hashtable(StringComparer.CurrentCultureIgnoreCase);

        private DataSet _metaDataSet;

        private readonly IDataSource _dataSource;

        public DatabaseMetaDataImpl(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        #region IDatabaseMetaData �����o

        public IList GetPrimaryKeySet(string tableName)
        {
            if (!_primaryKeys.Contains(tableName)) CreateTableMetaData(tableName);
            return (IList) _primaryKeys[tableName];
        }

        public IList GetColumnSet(string tableName)
        {
            if (!_columns.Contains(tableName)) CreateTableMetaData(tableName);
            return (IList) _columns[tableName];
        }

        public IList GetAutoIncrementColumnSet(string tableName)
        {
            if (!_autoIncrementColumns.Contains(tableName)) CreateTableMetaData(tableName);
            return (IList) _autoIncrementColumns[tableName];
        }

        #endregion

        private string _metaDataSetClassName;

        /// <summary>
        /// DB�̃��^�����i�[����<seealso cref="DataSet"/>�̊��S�C������ݒ肷��
        /// <seealso cref="DataSet"/>���܂ރA�Z���u���́A
        /// ���݂̃A�v���P�[�V�����E�h���C���Ɋ܂܂��K�v������
        /// </summary>
        public string MetaDataSetClassName
        {
            set { _metaDataSetClassName = value; }
        }

        /// <summary>
        /// �e�[�u����`�����쐬����
        /// </summary>
        /// <param name="tableName">�e�[�u����</param>
        protected virtual void CreateTableMetaData(string tableName)
        {
            lock (this)
            {
                // �e�[�u����`�����擾����
                DataTable metaDataTable;
                if (_metaDataSetClassName == null)
                {
                    metaDataTable = _GetMetaDataForDatabase(tableName);
                }
                else
                {
                    metaDataTable = _GetMetaDataForDataSet(tableName);
                }

                if (metaDataTable != null)
                {
                    // �e�[�u����`��񂩂�v���C�}���L�[���擾����
                    _primaryKeys[tableName] = GetPrimaryKeySet(metaDataTable.PrimaryKey);

                    // �e�[�u����`��񂩂�J�������擾����
                    _columns[tableName] = GetColumnSet(metaDataTable.Columns);

                    // �e�[�u����`��񂩂�AutoIncrement�J�������擾����
                    _autoIncrementColumns[tableName] = GetAutoIncrementColumnSet(metaDataTable.Columns);
                }
            }
        }

        /// <summary>
        /// DB����e�[�u����`�����擾����
        /// </summary>
        /// <param name="tableName">�e�[�u����</param>
        /// <returns>�e�[�u����`���B�擾�ł��Ȃ������ꍇ�Anull��Ԃ�</returns>
        private DataTable _GetMetaDataForDatabase(string tableName)
        {
            // �e�[�u����`
            DataTable[] metaDataTables;

            // IDbConnection���擾����
            var cn = DataSourceUtil.GetConnection(_dataSource);
            try
            {
                // �e�[�u����`�����擾���邽�߂�SQL���쐬����
                var sql = $"SELECT * FROM {tableName} WHERE 1 = 0";

                // IDbCommand���擾����
                var cmd = _dataSource.GetCommand(sql, cn);

                // Transaction�̏������s��
                _dataSource.SetTransaction(cmd);

                // IDataAdapter���擾����
                var adapter = _dataSource.GetDataAdapter(cmd);

                // �e�[�u����`�����擾����
                try
                {
                    metaDataTables = adapter.FillSchema(new DataSet(), SchemaType.Mapped);
                }
                catch
                {
                    return null;
                }
            }
            finally
            {
                // IDbConnection��Close�������s��
                _dataSource.CloseConnection(cn);
            }

            return metaDataTables[0];
        }

        /// <summary>
        /// DB�̃��^�����i�[����<seealso cref="DataSet"/>����e�[�u����`�����擾����
        /// </summary>
        /// <param name="tableName">�e�[�u����</param>
        /// <returns>�e�[�u����`���B�擾�ł��Ȃ������ꍇ�Anull��Ԃ�</returns>
        private DataTable _GetMetaDataForDataSet(string tableName)
        {
            if (_metaDataSet == null)
            {
                var loadedAssembly = AppDomain.CurrentDomain.GetAssemblies();
                var dataSetType = ClassUtil.ForName(_metaDataSetClassName, loadedAssembly);
                _metaDataSet = (DataSet) ClassUtil.NewInstance(dataSetType);
            }

            if (!_metaDataSet.Tables.Contains(tableName))
            {
                return null;
            }
            else
            {
                return _metaDataSet.Tables[tableName];
            }
        }

        private IList GetPrimaryKeySet(IEnumerable<DataColumn> primarykeys)
        {
            IList list = new CaseInsentiveSet();
            foreach (var pkey in primarykeys)
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
