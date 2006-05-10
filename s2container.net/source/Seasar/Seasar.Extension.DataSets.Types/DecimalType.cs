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
using System.Data;
using Seasar.Extension.ADO;
using Seasar.Framework.Util;
using Nullables;

namespace Seasar.Extension.DataSets.Types
{
	public class DecimalType : ObjectType, IColumnType
	{
		public DecimalType()
		{
		}

		#region IColumnType �����o

		public override object Convert(object value, string formatPattern)
		{
			if (value == null || value == DBNull.Value || value is INullableType)
			{
				return value;
			}
			return DecimalConversionUtil.ToDecimal(value);
		}

		public override string ToDbTypeString()
		{
			return "NUMBER";
		}

		public override DbType GetDbType()
		{
			return DbType.Decimal;
		}

		public override Type GetColumnType()
		{
			return typeof(Decimal);
		}

		#endregion
	}
}