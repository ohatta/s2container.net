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

using System.Data;
using Seasar.Extension.ADO;
using Seasar.Extension.ADO.Impl;

namespace Seasar.Extension.DataSets.Impl
{
    public class SqlWriter : IDataWriter
    {
        private readonly IDataSource _dataSource;
        private ICommandFactory _commandFactory;

        public SqlWriter(IDataSource dataSource)
            : this(dataSource, BasicCommandFactory.INSTANCE)
        {
        }

        public SqlWriter(IDataSource dataSource, ICommandFactory commandFactory)
        {
            _dataSource = dataSource;
            _commandFactory = commandFactory;
        }

        public IDataSource DataSource
        {
            get { return _dataSource; }
        }

        public ICommandFactory CommandFactory
        {
            get { return _commandFactory; }
            set { _commandFactory = value; }
        }

        #region IDataWriter �����o

        public virtual void Write(DataSet dataSet)
        {
            ITableWriter writer = new SqlTableWriter(DataSource, CommandFactory);
            foreach (DataTable table in dataSet.Tables)
            {
                writer.Write(table);
            }
        }

        #endregion
    }
}
