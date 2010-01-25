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

using System.Data;
using System.Reflection;
using System.Text;
using Seasar.Extension.ADO;
using Seasar.Framework.Log;

namespace Seasar.Dao.Pager
{
    public class PagerDataReaderFactoryLimitOffsetWrapper : AbstractPagerDataReaderFactoryWrapper, IDataReaderFactory
    {
        private static readonly Logger _logger = Logger.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public PagerDataReaderFactoryLimitOffsetWrapper(
            IDataReaderFactory dataReaderFactory,
            ICommandFactory commandFactory
            )
            : base(dataReaderFactory, commandFactory)
        {
        }

        #region IDataReaderFactory �����o

        public IDataReader CreateDataReader(IDataSource dataSource, IDbCommand cmd)
        {
            IPagerCondition condition = PagerContext.GetContext().PeekArgs();
            if (condition != null)
            {
                string baseSql = GetBaseSql(cmd);
                if (_logger.IsDebugEnabled)
                {
                    _logger.Debug("S2Pager base SQL : " + baseSql);
                }
                condition.Count = GetCount(dataSource, cmd, baseSql);
                if (condition.Limit > 0 && condition.Offset > -1)
                {
                    cmd.CommandText = MakeLimitOffsetSql(baseSql, condition.Limit, condition.Offset);
                    if (_logger.IsDebugEnabled)
                    {
                        _logger.Debug("S2Pager execute SQL : " + cmd.CommandText);
                    }
                }
                return DataReaderFactory.CreateDataReader(dataSource, cmd);
            }
            else
            {
                return DataReaderFactory.CreateDataReader(dataSource, cmd);
            }
        }

        #endregion

        protected string MakeLimitOffsetSql(string baseSql, int limit, int offset)
        {
            StringBuilder buf = new StringBuilder(baseSql, baseSql.Length + 32);
            buf.AppendFormat(" LIMIT {0} OFFSET {1}", limit, offset);
            return buf.ToString();
        }
    }
}
