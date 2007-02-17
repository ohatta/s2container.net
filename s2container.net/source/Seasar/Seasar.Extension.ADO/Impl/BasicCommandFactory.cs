#region Copyright
/*
 * Copyright 2005-2006 the Seasar Foundation and the Others.
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
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Seasar.Framework.Util;
using Nullables;

namespace Seasar.Extension.ADO.Impl
{
    public class BasicCommandFactory : ICommandFactory
    {
        public static readonly ICommandFactory INSTANCE = new BasicCommandFactory();

        private readonly IDbParameterParser parser;

        private string sqlLogDateFormat = "yyyy-MM-dd";

        private string sqlLogDateTimeFormat = "yyyy-MM-dd HH.mm.ss";

        public BasicCommandFactory()
            : this(DbParameterParser.INSTANCE)
        {
        }

        public BasicCommandFactory(IDbParameterParser dbParameterParser)
        {
            this.parser = dbParameterParser;
        }

        public string SqlLogDateFormat
        {
            get { return sqlLogDateFormat; }
            set { sqlLogDateFormat = value; }
        }

        public string SqlLogDateTimeFormat
        {
            get { return sqlLogDateTimeFormat; }
            set { sqlLogDateTimeFormat = value; }
        }

        #region ICommandFactory �����o

        public IDbCommand CreateCommand(IDbConnection con, string sql)
        {
            string changeSignSql = sql;
            IDbCommand cmd = con.CreateCommand();
            switch (DataProviderUtil.GetBindVariableType(cmd))
            {
                case BindVariableType.AtmarkWithParam:
                    changeSignSql = GetChangeSignCommandText(sql, "@");
                    break;
                case BindVariableType.Question:
                    changeSignSql = GetCommandText(sql, "?");
                    break;
                case BindVariableType.QuestionWithParam:
                    changeSignSql = GetChangeSignCommandText(sql, "?");
                    break;
                case BindVariableType.ColonWithParam:
                    changeSignSql = GetChangeSignCommandText(sql, ":");
                    break;
                case BindVariableType.ColonWithParamToLower:
                    changeSignSql = GetChangeSignCommandText(sql, ":");
                    changeSignSql = changeSignSql.ToLower();
                    break;
            }
            cmd.CommandText = changeSignSql;
            return cmd;
        }

        public string GetCompleteSql(string sql, object[] args)
        {
            MatchCollection sqlParameters = parser.Parse(sql);
            return ReplaceSql(sql, args);
        }

        #endregion

        private string GetChangeSignCommandText(string sql, string sign)
        {
            StringBuilder text = new StringBuilder(sql);
            for (int startIndex = 0, parameterIndex = 0; ; ++parameterIndex)
            {
                Match match = parser.Match(text.ToString(), startIndex);
                if (!match.Success)
                {
                    break;
                }
                string parameterName = sign + parameterIndex.ToString();
                text.Replace(match.Value, parameterName, match.Index, match.Length);
                startIndex = match.Index + parameterName.Length;
            }
            return text.ToString();
        }

        private string GetCommandText(string sql, string sign)
        {
            return ReplaceSql(sql, sign);
        }

        private string ReplaceSql(string sql, string newValue)
        {
            StringBuilder text = new StringBuilder(sql);
            for (int startIndex = 0; ; )
            {
                Match match = parser.Match(text.ToString(), startIndex);
                if (!match.Success)
                {
                    break;
                }
                text.Replace(match.Value, newValue, match.Index, match.Length);
                startIndex = match.Index + newValue.Length;
            }
            return text.ToString();
        }

        private string ReplaceSql(string sql, object[] args)
        {
            StringBuilder text = new StringBuilder(sql);
            for (int startIndex = 0, argsIndex = 0; argsIndex < args.Length; ++argsIndex)
            {
                Match match = parser.Match(text.ToString(), startIndex);
                if (!match.Success)
                {
                    break;
                }
                string newValue = GetBindVariableText(args[argsIndex]);
                text.Replace(match.Value, newValue, match.Index, match.Length);
                startIndex = match.Index + newValue.Length;
            }
            return text.ToString();
        }

        protected virtual string GetBindVariableText(object bindVariable)
        {
            if (bindVariable is INullable)
            {
                INullable nullable = bindVariable as INullable;
                if (nullable.IsNull)
                {
                    return GetBindVariableText(null);
                }
                else
                {
                    PropertyInfo pi = bindVariable.GetType().GetProperty("Value");
                    return GetBindVariableText(pi.GetValue(bindVariable, null));
                }
            }
            else if (bindVariable is INullableType)
            {
                INullableType nullable = bindVariable as INullableType;
                if (!nullable.HasValue)
                {
                    return GetBindVariableText(null);
                }
                else
                {
                    PropertyInfo pi = bindVariable.GetType().GetProperty("Value");
                    return GetBindVariableText(pi.GetValue(bindVariable, null));
                }
            }
            else if (bindVariable is string)
            {
                return "'" + bindVariable + "'";
            }
            else if (bindVariable == null)
            {
                return "null";
            }
            else if (bindVariable.GetType().IsPrimitive)
            {
                return bindVariable.ToString();
            }
            else if (bindVariable is decimal)
            {
                return bindVariable.ToString();
            }
            else if (bindVariable is DateTime)
            {
                if ((DateTime) bindVariable == ((DateTime) bindVariable).Date)
                {
                    return "'" + ((DateTime) bindVariable).ToString(SqlLogDateFormat) + "'";
                }
                else
                {
                    return "'" + ((DateTime) bindVariable).ToString(SqlLogDateTimeFormat) + "'";
                }
            }
            else if (bindVariable is bool)
            {
                return bindVariable.ToString();
            }
            else
            {
                return "'" + bindVariable.ToString() + "'";
            }
        }
    }
}
