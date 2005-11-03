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
using System.Reflection;

namespace Seasar.Framework.Exceptions
{
	/// <summary>
	/// TargetInvocationException�����b�v������s����O�ł��B
	/// </summary>
	public class InvocationTargetRuntimeException : SRuntimeException
	{
		private Type targetType_;

		public InvocationTargetRuntimeException(
			Type targetType,TargetInvocationException cause)
			: base("ESSR0043",new object[] { targetType.FullName,cause.GetBaseException() })

		{
			targetType_ = targetType;
		}

		public Type TargetType
		{
			get { return targetType_; }
		}
	}
}
