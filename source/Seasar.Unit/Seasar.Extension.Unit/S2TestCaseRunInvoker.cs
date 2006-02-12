using System;
using System.Collections;
using MbUnit.Core.Invokers;

namespace Seasar.Extension.Unit
{
	/// <summary>
	/// S2TestCaseRunInvoker �̊T�v�̐����ł��B
	/// </summary>
	public class S2TestCaseRunInvoker : DecoratorRunInvoker
	{
		private S2TestCaseRunner runner;
		private Tx tx;

		public S2TestCaseRunInvoker(IRunInvoker invoker, Tx tx)
			: base(invoker)
		{
			this.tx = tx;
			runner = new S2TestCaseRunner();
		}

		public override object Execute(object o, IList args)
		{
			return runner.Run(this.Invoker, o, args, tx);
		}
	}
}