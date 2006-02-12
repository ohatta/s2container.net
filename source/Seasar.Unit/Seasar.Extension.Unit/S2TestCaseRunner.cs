using System;
using System.Collections;
using MbUnit.Core.Invokers;
using Seasar.Extension.ADO;
using Seasar.Extension.ADO.Impl;
using Seasar.Extension.Tx;
using Seasar.Framework.Container;
using Seasar.Framework.Unit;
using Seasar.Framework.Util;

namespace Seasar.Extension.Unit
{
	/// <summary>
	/// S2TestCaseRunner �̊T�v�̐����ł��B
	/// </summary>
	public class S2TestCaseRunner : S2FrameworkTestCaseRunner
	{
		private static readonly String DATASOURCE_NAME = "ado"
			+ ContainerConstants.NS_SEP + "dataSource";
    
		private S2TestCase fixture;
		private Tx tx;
		private ITransactionContext tc;
		private IDataSource dataSource;

		public S2TestCaseRunner()
		{
		}

		public object Run(IRunInvoker invoker, object o, IList args, Tx tx)
		{
			this.tx = tx;
			fixture = o as S2TestCase;
			return this.Run(invoker, o, args);
		}

		protected override void BeginTransactionContext()
		{
			if (Tx.NotSupported != tx)
			{
				try
				{
					tc = (ITransactionContext) this.Container.GetComponent(typeof(ITransactionContext));
					tc.Begin();
				}
				catch (Exception e) 
				{
					Console.Error.WriteLine(e);
				}
			}
		}

		protected override void EndTransactionContext()
		{
			if (tc != null)
			{
				if (Tx.Commit == tx)
					tc.Commit();
				if (Tx.Rollback == tx)
					tc.Rollback();
			}
		}

		protected override void SetUpAfterContainerInit()
		{
			base.SetUpAfterContainerInit();
			SetupDataSource();
		}

		protected override void TearDownBeforeContainerDestroy()
		{
			TearDownDataSource();
			base.TearDownBeforeContainerDestroy();
		}

		protected void SetupDataSource() 
		{
			if (Tx.NotSupported == tx)
				return;

			try 
			{
				if (this.Container.HasComponentDef(DATASOURCE_NAME)) 
				{
					dataSource = this.Container.GetComponent(DATASOURCE_NAME) as IDataSource;
				} 
				else if (this.Container.HasComponentDef(typeof(DataSourceImpl))) 
				{
					dataSource = this.Container.GetComponent(typeof(DataSourceImpl)) as IDataSource;
				}
				if (fixture != null && dataSource != null)
				{
					fixture.SetDataSource(dataSource);
				}
			} 
			catch (Exception e) 
			{
				Console.Error.WriteLine(e);
			}
		}

		protected void TearDownDataSource() 
		{
			if (Tx.NotSupported == tx)
				return;

			//dbMetaData = null;
			if (fixture.Connection != null) 
			{
				ConnectionUtil.Close(fixture.Connection);
				fixture.SetConnection(null);
			}
			if (fixture != null)
			{
				fixture.SetDataSource(null);
			}
			dataSource = null;
		}
	}
}
