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

using System.Collections.Generic;
using Seasar.Extension.ADO;
using Seasar.Extension.ADO.Impl;
using Seasar.Extension.Tx;
using Seasar.Extension.Tx.Impl;
using Seasar.Framework.Container;
using Seasar.Quill.Attrs;
using Seasar.Quill.Database.DataSource.Selector;
using System;

namespace Seasar.Quill.Database.DataSource.Impl
{
    [Implementation]
    public class SelectableDataSourceProxyWithDictionary : AbstractSelectableDataSourceProxy
    {
        /// <summary>
        /// �f�[�^�\�[�X��
        /// </summary>
        [ThreadStatic]
        private static string _dataSourceName;

        private IDataSourceSelector _dataSourceSelector = null;

        /// <summary>
        /// �f�[�^�\�[�X�I�����W�b�N�C���^�[�t�F�[�X
        /// </summary>
        public IDataSourceSelector DataSourceSelector
        {
            set { _dataSourceSelector = value; }
            get { return _dataSourceSelector; }
        }

        private readonly IDictionary<string, IDataSource> _dataSourceCollection 
            = new Dictionary<string, IDataSource>();

        public IDictionary<string, IDataSource> DataSourceCollection
        {
            get { return _dataSourceCollection; }
        }

        #region AbstractSelectableDataSourceProxy�����o

        public override string GetDataSourceName()
        {
            if ( DataSourceSelector != null )
            {
                //  �f�[�^�\�[�X������̃J�X�^���p
                return DataSourceSelector.SelectDataSourceName(DataSourceCollection.Keys);
            }

            //  �f�[�^�\�[�X�������ݒ�̏ꍇ�͈�ԍŏ��̃L�[�ɑΉ�����f�[�^�\�[�X�����g��
            if (string.IsNullOrEmpty(_dataSourceName))
            {
                foreach (string dsname in DataSourceCollection.Keys)
                {
                    return dsname;
                }
            }
            return _dataSourceName;
        }

        public override void SetDataSourceName(string dataSourceName)
        {
            _dataSourceName = dataSourceName;
        }

        public override IDataSource GetDataSource(string dataSourceName)
        {
            if ( DataSourceCollection.ContainsKey(dataSourceName) )
            {
                return DataSourceCollection[dataSourceName];
            }
            throw new ComponentNotFoundRuntimeException(dataSourceName);
        }

        #endregion

        /// <summary>
        /// �f�[�^�\�[�X��o�^
        /// </summary>
        /// <remarks>�㏟���œo�^���܂�</remarks>
        /// <param name="dataSourceName"></param>
        /// <param name="dataSource"></param>
        public virtual void RegistDataSource(string dataSourceName, IDataSource dataSource)
        {
            DataSourceCollection[dataSourceName] = dataSource;
        }

        /// <summary>
        /// �ێ����Ă���f�[�^�\�[�X��TransactionContext��K�p���܂��B
        /// </summary>
        /// <param name="txContext"></param>
        public virtual void SetTransactionContext(ITransactionContext txContext)
        {
            foreach (IDataSource dataSource in DataSourceCollection.Values)
            {
                if (dataSource is TxDataSource)
                {
                    ((TxDataSource)dataSource).Context = txContext;
                }
            }
        }
    }
}
