using System.Data;
using Seasar.Extension.ADO;

namespace Seasar.Extension.DataSets.States
{
	public class DetachedState : RowState
	{
		public override string ToString()
		{
			return DataRowState.Detached.ToString();
		}

		#region RowState �����o

		public void Update(IDataSource dataSource, DataRow row)
		{
		}

		#endregion
	}
}
