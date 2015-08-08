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

using System.Data;
using Seasar.Framework.Exceptions;

namespace Seasar.Extension.ADO.Impl
{
    /// <summary>
    /// �����f�[�^�\�[�X�ւ̐ڑ����T�|�[�g���钊�ۃN���X
    /// �����I�Ƀf�[�^�\�[�X���o�^����Ă���ꍇ��IDictionary�̕���
    /// �����I�ɓo�^����Ă��Ȃ����S2Container�̕���T���܂��B
    /// </summary>
    public abstract class AbstractSelectableDataSourceProxy : IDataSource
    {
        /// <summary>
        /// �g�p����f�[�^�\�[�X���̎擾
        /// </summary>
        /// <returns></returns>
        public abstract string GetDataSourceName();

        /// <summary>
        /// ���[�J���f�[�^�������X���b�g�Ƀf�[�^�\�[�X����ݒ�
        /// </summary>
        /// <param name="dataSourceName"></param>
        public abstract void SetDataSourceName(string dataSourceName);

        /// <summary>
        /// �f�[�^�\�[�X�̎擾
        /// </summary>
        /// <returns></returns>
        public virtual IDataSource GetDataSource()
        {
            string dataSourceName = GetDataSourceName();
            if ( string.IsNullOrEmpty(dataSourceName) )
            {
                throw new EmptyRuntimeException(dataSourceName + " at slot");
            }
            return GetDataSource(dataSourceName);
        }

        /// <summary>
        /// �f�[�^�\�[�X�̎擾
        /// </summary>
        /// <param name="dataSourceName">�f�[�^�\�[�X��</param>
        /// <returns></returns>
        public abstract IDataSource GetDataSource(string dataSourceName);

        #region IDataSource �����o

        public virtual IDbConnection GetConnection()
        {
            return GetDataSource().GetConnection();
        }

        public virtual void CloseConnection(IDbConnection connection)
        {
            GetDataSource().CloseConnection(connection);
        }

        public virtual IDbCommand GetCommand()
        {
            return GetDataSource().GetCommand();
        }

        public virtual IDbCommand GetCommand(string text)
        {
            return GetDataSource().GetCommand(text);
        }

        public virtual IDbCommand GetCommand(string text, IDbConnection connection)
        {
            return GetDataSource().GetCommand(text, connection);
        }

        public virtual IDbCommand GetCommand(string text, IDbConnection connection, IDbTransaction transaction)
        {
            return GetDataSource().GetCommand(text, connection, transaction);
        }

        public virtual IDataParameter GetParameter()
        {
            return GetDataSource().GetParameter();
        }

        public virtual IDataParameter GetParameter(string name, DbType dataType)
        {
            return GetDataSource().GetParameter(name, dataType);
        }

        public virtual IDataParameter GetParameter(string name, object value)
        {
            return GetDataSource().GetParameter(name, value);
        }

        public virtual IDataParameter GetParameter(string name, DbType dataType, int size)
        {
            return GetDataSource().GetParameter(name, dataType, size);
        }

        public virtual IDataParameter GetParameter(string name, DbType dataType, int size, string srcColumn)
        {
            return GetDataSource().GetParameter(name, dataType, size, srcColumn);
        }

        public virtual IDataAdapter GetDataAdapter()
        {
            return GetDataSource().GetDataAdapter();
        }

        public virtual IDataAdapter GetDataAdapter(IDbCommand selectCommand)
        {
            return GetDataSource().GetDataAdapter(selectCommand);
        }

        public virtual IDataAdapter GetDataAdapter(string selectCommandText, string selectConnectionString)
        {
            return GetDataSource().GetDataAdapter(selectCommandText, selectConnectionString);
        }

        public virtual IDataAdapter GetDataAdapter(string selectCommandText, IDbConnection selectConnection)
        {
            return GetDataSource().GetDataAdapter(selectCommandText, selectConnection);
        }

        public virtual IDbTransaction GetTransaction()
        {
            return GetDataSource().GetTransaction();
        }

        public virtual void SetTransaction(IDbCommand cmd)
        {
            GetDataSource().SetTransaction(cmd);
        }

        #endregion
    }
}
