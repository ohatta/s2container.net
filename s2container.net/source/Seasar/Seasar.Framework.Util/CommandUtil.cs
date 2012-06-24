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
using Seasar.Extension.ADO;
using Seasar.Framework.Exceptions;

namespace Seasar.Framework.Util
{
    public sealed class CommandUtil
    {
        private CommandUtil()
        {
        }

        public static void Close(IDbCommand cmd)
        {
            if (cmd == null)
            {
                return;
            }
            try
            {
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw new SQLRuntimeException(ex);
            }
        }

        public static IDataReader ExecuteReader(IDataSource dataSource, IDbCommand cmd)
        {
            try
            {
                dataSource.SetTransaction(cmd);
                return cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw new SQLRuntimeException(ex, cmd.CommandText);
            }
        }

        public static int ExecuteNonQuery(IDataSource dataSource, IDbCommand cmd)
        {
            try
            {
                dataSource.SetTransaction(cmd);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new SQLRuntimeException(ex, cmd.CommandText);
            }
        }

        public static object ExecuteScalar(IDataSource dataSource, IDbCommand cmd)
        {
            try
            {
                dataSource.SetTransaction(cmd);
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new SQLRuntimeException(ex, cmd.CommandText);
            }
        }
    }
}
