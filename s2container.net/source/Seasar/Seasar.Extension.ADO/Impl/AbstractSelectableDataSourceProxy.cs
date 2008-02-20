using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading;
using Seasar.Framework.Container;
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
        private readonly LocalDataStoreSlot _slot = Thread.AllocateDataSlot();

        protected LocalDataStoreSlot Slot
        {
            get
            {
                return _slot;
            }
        }

        /// <summary>
        /// ���[�J���f�[�^�������X���b�g����f�[�^�\�[�X���̎擾
        /// </summary>
        /// <returns></returns>
        public virtual string GetDataSourceName()
        {
            return (string)Thread.GetData(_slot);
        }

        /// <summary>
        /// ���[�J���f�[�^�������X���b�g�Ƀf�[�^�\�[�X����ݒ�
        /// </summary>
        /// <param name="dataSourceName"></param>
        public virtual void SetDataSourceName(string dataSourceName)
        {
            Thread.SetData(_slot, dataSourceName);
        }

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

        public virtual IDbCommand GetCommand(string cmdText)
        {
            return GetDataSource().GetCommand(cmdText);
        }

        public virtual IDbCommand GetCommand(string cmdText, IDbConnection connection)
        {
            return GetDataSource().GetCommand(cmdText, connection);
        }

        public virtual IDbCommand GetCommand(string cmdText, IDbConnection connection, IDbTransaction transaction)
        {
            return GetDataSource().GetCommand(cmdText, connection, transaction);
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
