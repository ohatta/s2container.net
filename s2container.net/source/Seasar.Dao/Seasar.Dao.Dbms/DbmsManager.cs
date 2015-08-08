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
using System.Data.Odbc;
using System.Data.OleDb;
using System.Reflection;
using System.Resources;
using Seasar.Extension.ADO;
using Seasar.Framework.Util;

namespace Seasar.Dao.Dbms
{
    public sealed class DbmsManager
    {
        private static readonly ResourceManager _resourceManager;

        static DbmsManager()
        {
            _resourceManager = new ResourceManager(
                "Dbms", Assembly.GetExecutingAssembly());
        }

        private DbmsManager()
        {
        }

        public static IDbms GetDbms(IDataSource dataSource)
        {
            // IDbConnection��DataSource����擾����
            var cn = dataSource.GetConnection();

            //IDbms�̎����N���X���擾���邽�߂�Key
            string dbmsKey;

            if (cn is OleDbConnection)
            {
                // OleDbConnection�̏ꍇ��Key��Type����Provider������쐬����
                var oleDbCn = cn as OleDbConnection;
                dbmsKey = cn.GetExType().Name + "_" + oleDbCn.Provider;
            }
            else if (cn is OdbcConnection)
            {
                // OdbcConnection�̏ꍇ��Key��Type����Driver������쐬����
                var odbcCn = cn as OdbcConnection;
                dbmsKey = cn.GetExType().Name + "_" + odbcCn.Driver;
            }
            else
            {
                dbmsKey = cn.GetExType().Name;
            }

            // Key����IDbms�����N���X�̃C���X�^���X���擾����
            return GetDbms(dbmsKey);
        }

        /// <summary>
        /// Dbms.resx��dbmsKey�ŒT���AIDbms�����N���X�̃C���X�^���X���擾����
        /// </summary>
        /// <param name="dbmsKey">Dbms.resx����������ׂ�Key</param>
        /// <returns>IDbms�����N���X�̃C���X�^���X</returns>
        /// <remarks>dbmsKey�ɑΉ�������̂�������Ȃ��ꍇ�́A
        /// �W����Standard���g�p����</remarks>
        public static IDbms GetDbms(string dbmsKey)
        {
            // Dbms.resx����IDbms�̎����N���X�����擾����
            var typeName = _resourceManager.GetString(dbmsKey);

            // IDbms�����N���X��Type���擾����
            // Dbms.resx�ɑΉ�����IDbms�����N���X�������ꍇ�́A�W����Standard���g�p����
            var type = typeName == null ? typeof(Standard) : Type.GetType(typeName);

            // IDbms�����N���X�̃C���X�^���X���쐬���ĕԂ�
//            return (IDbms)Activator.CreateInstance(type, false);
            return (IDbms) ClassUtil.NewInstance(type);
        }
    }
}
