#region Copyright
/*
 * Copyright 2005-2010 the Seasar Foundation and the Others.
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
using Seasar.Framework.Log;
using Seasar.Framework.Util;

namespace Seasar.Extension.ADO.Impl
{
    public class BasicUpdateHandler : BasicHandler, IUpdateHandler
    {
        private static readonly Logger _logger = Logger.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public BasicUpdateHandler()
        {
        }

        public BasicUpdateHandler(IDataSource dataSource, string sql)
            : base(dataSource, sql)
        {
        }

        public BasicUpdateHandler(IDataSource dataSource, string sql,
            ICommandFactory commandFactory)
            : base(dataSource, sql, commandFactory)
        {
        }

        #region IUpdateHandler �����o

        public virtual int Execute(object[] args)
        {
            return Execute(args, GetArgTypes(args));
        }

        public virtual int Execute(object[] args, Type[] argTypes)
        {
            if (_logger.IsDebugEnabled)
            {
                _logger.Debug(GetCompleteSql(args));
            }
            IDbConnection con = Connection;
            try
            {
                return Execute(con, args, argTypes);
            }
            finally
            {
                DataSource.CloseConnection(con);
            }
        }

        public virtual int Execute(object[] args, Type[] argTypes, string[] argNames)
        {
            return Execute(args, argTypes);
        }

        #endregion

        protected virtual int Execute(IDbConnection connection, object[] args, Type[] argTypes)
        {
            IDbCommand cmd = Command(connection);
            try
            {
                BindArgs(cmd, args, argTypes);
                return CommandFactory.ExecuteNonQuery(DataSource, cmd);
            }
            finally
            {
                CommandUtil.Close(cmd);
            }
        }
    }
}
