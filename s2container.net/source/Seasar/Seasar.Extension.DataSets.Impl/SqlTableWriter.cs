using System.Data;
using Seasar.Extension.ADO;
using Seasar.Extension.ADO.Impl;
using Seasar.Extension.ADO.Types;
using Seasar.Extension.DataSets.States;
using Seasar.Framework.Util;

namespace Seasar.Extension.DataSets.Impl
{
	public class SqlTableWriter : ITableWriter
	{
		private IDataSource dataSource_;

		public SqlTableWriter(IDataSource dataSource)
		{
			dataSource_ = dataSource;
			ValueTypes.Init(dataSource);
		}

		public IDataSource DataSource 
		{
			get { return dataSource_; }
		}

		#region ITableWriter �����o

		public void Write(DataTable table)
		{
			/// TODO ���^�f�[�^���擾�ς݂ł��邩���f���郍�W�b�N��ǉ����邱�ƁB
			//      DbDataAdapter#TableMappings ���画�f���邵���Ȃ����Ȃ��c�B
			SetupMetaData(table);

			DoWrite(table);
		}

		#endregion

		protected virtual void DoWrite(DataTable table) 
		{
			foreach (DataRow row in table.Rows) 
			{
				RowState state = RowStateFactory.GetRowState(row.RowState);
				state.Update(DataSource, row);
			}
			table.AcceptChanges();
		}

		private void SetupMetaData(DataTable table) 
		{
			IDbConnection con = DataSourceUtil.GetConnection(DataSource);
			try 
			{
				IDatabaseMetaData dbMetaData = new DatabaseMetaDataImpl(DataSource);
				DataTableUtil.SetupMetaData(dbMetaData, table);
			} 
			finally 
			{
				DataSourceUtil.CloseConnection(DataSource, con);
			}
		}
	}
}
