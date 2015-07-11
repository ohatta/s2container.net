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

using System;
using System.Data;
using Seasar.Extension.ADO.Types;

namespace Seasar.Extension.ADO.Impl
{
    public sealed class PropertyTypeUtil
    {
        private PropertyTypeUtil()
        {
        }

        public static IPropertyType[] CreatePropertyTypes(DataTable metaData)
        {
            int count = metaData.Rows.Count;
            IPropertyType[] propertyTypes = new PropertyTypeImpl[count];
            for (int i = 0; i < count; ++i)
            {
                DataRow row = metaData.Rows[i];
                string propertyName = (string) row["ColumnName"];
                Type type = (Type) row["DataType"];
                IValueType valueType = ValueTypes.GetValueType(type);
                propertyTypes[i] = new PropertyTypeImpl(propertyName, valueType, type);
            }
            return propertyTypes;
        }
    }
}
