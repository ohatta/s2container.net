#region Copyright
/*
 * Copyright 2005 the Seasar Foundation and the Others.
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

using Seasar.Framework.Aop;

namespace Seasar.Extension.Tx.Impl
{
	/// <summary>
	/// LocalNotSupportedTxHandler �̊T�v�̐����ł��B
	/// </summary>
	public class LocalNotSupportedTxHandler : AbstractLocalTxHandler
	{
		public override object Handle(IMethodInvocation invocation, bool alreadyInTransaction)
		{
			if(alreadyInTransaction)
			{
				return HandleTransaction(invocation);
			} 
			else 
			{
				return invocation.Proceed();
			}
		}

		private object HandleTransaction(IMethodInvocation invocation)
		{
			using(ITransactionContext current = this.Context.Create())
			{
				ITransactionContext parent = this.Context.Current;
				current.Parent = parent;
				this.Context.Current = null;
				current.OpenConnection();
				this.Context.Current = current;

				try
				{
					return invocation.Proceed();
				}
				finally
				{
					this.Context.Current = parent;
					this.Context.Current.Parent = null;
				}			
			}
		}
	}
}