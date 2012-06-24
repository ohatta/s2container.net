﻿#region Copyright
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

using Seasar.Extension.ADO;

namespace Seasar.Quill.Database.Provider
{
    /// <summary>
    /// ODBC接続用のプロバイダクラス
    /// </summary>
    public class ODBC : DataProvider
    {
        public ODBC()
        {
            ConnectionType = "System.Data.Odbc.OdbcConnection";
            CommandType = "System.Data.Odbc.OdbcCommand";
            ParameterType = "System.Data.Odbc.OdbcParameter";
            DataAdapterType = "System.Data.Odbc.OdbcDataAdapter";
        }
    }
}
