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
using System.Reflection;
using Seasar.Extension.ADO;
using Seasar.Framework.Exceptions;
using Seasar.Framework.Log;
using Seasar.Framework.Util;

namespace Seasar.Dao.Impl
{
    /// <summary>
    /// �o�̓p�����[�^���P��ł���ProcedureHandler
    /// </summary>
    public class ObjectBasicProcedureHandler : AbstractProcedureHandler
    {
        private static readonly Logger _logger = Logger.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="dataSource">�f�[�^�\�[�X��</param>
        /// <param name="commandFactory">IDbCommand Factory</param>
        /// <param name="procedureName">�v���V�[�W����</param>
        public ObjectBasicProcedureHandler(IDataSource dataSource, ICommandFactory commandFactory, string procedureName)
            : base(dataSource, commandFactory, procedureName)
        {
            ;
        }

        /// <summary>
        /// �X�g�A�h�v���V�[�W�������s����
        /// </summary>
        /// <param name="args">����</param>
        /// <param name="returnType">�߂�l�^�C�v</param>
        /// <returns>�o�̓p�����[�^�l</returns>
        public object Execute(object[] args, Type returnType)
        {
            if (DataSource == null) throw new EmptyRuntimeException("dataSource");
            var conn = DataSourceUtil.GetConnection(DataSource);

            try
            {
                if (_logger.IsDebugEnabled)
                {
                    _logger.Debug(ProcedureName);
                }

                IDbCommand cmd = null;
                try
                {
                    object ret = null;
                    cmd = GetCommand(conn, ProcedureName);
                    var cnt = 0;

                    // �p�����[�^���Z�b�g���A�Ԓl���擾����
                    if (returnType != typeof(void))
                    {
                        // ODP.NET�ł́A�ŏ���RETURN�p�����[�^���Z�b�g���Ȃ���RETURN�l���擾�ł��Ȃ��H
                        var returnParamName = BindReturnValues(cmd, "RetValue", GetDbValueType(returnType));

                        BindParamters(cmd, args, ArgumentTypes, ArgumentNames, ArgumentDirection);

                        CommandFactory.ExecuteNonQuery(DataSource, cmd);

                        var param = (IDbDataParameter) cmd.Parameters[returnParamName];
                        ret = param.Value;
                        cnt = 1;
                    }
                    else
                    {
                        BindParamters(cmd, args, ArgumentTypes, ArgumentNames, ArgumentDirection);
                        CommandFactory.ExecuteNonQuery(DataSource, cmd);
                    }

                    // Out�܂���InOut�p�����[�^�l���擾����
                    for (var i = 0; i < args.Length; i++)
                    {
                        if (ArgumentDirection[i] == ParameterDirection.InputOutput ||
                             ArgumentDirection[i] == ParameterDirection.Output)
                        {
                            var param = (IDbDataParameter)cmd.Parameters[i + cnt];
                            args[i] = ConversionUtil.ConvertTargetType(param.Value, ArgumentTypes[i]);
                        }
                    }

                    return ret;
                }
                finally
                {
                    CommandUtil.Close(cmd);
                }
            }
            catch (Exception e)
            {
                throw new SQLRuntimeException(e);
            }
            finally
            {
                DataSource.CloseConnection(conn);
            }
        }
    }
}
