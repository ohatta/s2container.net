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

using System.Data;
using Seasar.Extension.ADO;

namespace Seasar.Extension.DataSets.Impl
{
    public class SqlReloadReader : IDataReader
    {
        private readonly IDataSource _dataSource;
        private readonly DataSet _dataSet;

        public SqlReloadReader(IDataSource dataSource, DataSet dataSet)
        {
            _dataSource = dataSource;
            _dataSet = dataSet;
        }

        public IDataSource DataSource
        {
            get { return _dataSource; }
        }

        public DataSet DataSet
        {
            get { return _dataSet; }
        }

        #region IDataReader �����o

        public virtual DataSet Read()
        {
            DataSet newDataSet = new DataSet();
            foreach (DataTable table in _dataSet.Tables)
            {
                ITableReader reader = new SqlReloadTableReader(_dataSource, table);
                newDataSet.Tables.Add(reader.Read());
            }
            return newDataSet;
        }

        #endregion
    }
}
