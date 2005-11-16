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
using Seasar.Extension.Tx.Impl;
using Seasar.Framework.Exceptions;
using Seasar.Framework.Log;

namespace Seasar.Framework.Util
{
    public class DataSourceUtil
    {
        private static readonly Logger logger = Logger.GetLogger(typeof(DataSourceUtil));

        private DataSourceUtil()
        {
        }

        public static IDbConnection GetConnection(IDataSource dataSource)
        {
            try
            {
                IDbConnection cn = dataSource.GetConnection();
                if(cn.State != ConnectionState.Open)
                {
                    cn.Open();
                    logger.Log("DSSR0007", null);
                }
                return cn;
            }
            catch(Exception ex)
            {
                throw new SQLRuntimeException(ex);
            }
        }

        public static void CloseConnection(IDataSource dataSource, IDbConnection cn)
        {
            try
            {
                if(dataSource is TxDataSource)
                {
                    TxDataSource txDataSoure = dataSource as TxDataSource;
                    if(txDataSoure.Context.Current.IsInTransaction) return;
                }
                ConnectionUtil.Close(cn);
            }
            catch(Exception ex)
            {
                throw new SQLRuntimeException(ex);
            }
        }

        public static void SetTransaction(IDataSource dataSource, IDbCommand cmd)
        {
            if(dataSource is TxDataSource)
            {
                TxDataSource txDataSource = dataSource as TxDataSource;
                if(txDataSource.Context.Current.IsInTransaction)
                    cmd.Transaction = txDataSource.Context.Current.Transaction;
            }
        }
    }
}