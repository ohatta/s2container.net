#region Copyright
/*
 * Copyright 2005-2012 the Seasar Foundation and the Others.
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
using System.Data.SqlTypes;

namespace Seasar.Extension.ADO.Types
{
    public class SqlDoubleType : SqlBaseType, IValueType
    {
        #region IValueType �����o

        public override void BindValue(IDbCommand cmd, string columnName, object value)
        {
            BindValue(cmd, columnName, value, DbType.Double);
        }

        #endregion

        protected override object GetValue(object value)
        {
            if (value == DBNull.Value)
            {
                return SqlDouble.Null;
            }
            else if (value is double)
            {
                return new SqlDouble((double) value);
            }
            else
            {
                return SqlDouble.Parse(value.ToString());
            }
        }
    }
}
