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
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Reflection;
using Seasar.Extension.ADO;
using Seasar.Extension.ADO.Impl;
using Seasar.Extension.ADO.Types;
using Seasar.Framework.Exceptions;
using Seasar.Framework.Log;
using Seasar.Framework.Util;

namespace Seasar.Dao.Impl
{
    /// <summary>
    /// Procedure��{Handler
    /// </summary>
    public class AbstractProcedureHandler : BasicHandler
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="dataSource">�f�[�^�\�[�X��</param>
        /// <param name="commandFactory">IDbCommand Factory</param>
        /// <param name="procedureName">�X�g�A�h�v���V�[�W����</param>
        public AbstractProcedureHandler(IDataSource dataSource, ICommandFactory commandFactory, string procedureName)
        {
            DataSource = dataSource;
            CommandFactory = commandFactory;
            ProcedureName = procedureName;
        }

        public static Logger Logger { get; } = Logger.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// �����^�C�v
        /// </summary>
        public Type[] ArgumentTypes { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        public string[] ArgumentNames { get; set; }

        /// <summary>
        /// �����̓��o�͎��
        /// </summary>
        public ParameterDirection[] ArgumentDirection { get; set; }

        /// <summary>
        /// �X�g�A�h�v���V�[�W����
        /// </summary>
        public string ProcedureName { get; set; }

        /// <summary>
        /// IDbCommand�I�u�W�F�N�g���擾����
        /// </summary>
        /// <param name="connection">�R�l�N�V�����I�u�W�F�N�g</param>
        /// <param name="procedureName">�X�g�A�h�v���V�[�W����</param>
        /// <returns></returns>
        protected IDbCommand GetCommand(IDbConnection connection, string procedureName)
        {
            if (procedureName == null)
                throw new EmptyRuntimeException("procedureName");

            var cmd = CommandFactory.CreateCommand(connection, procedureName);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.UpdatedRowSource = UpdateRowSource.OutputParameters;
            
//            DataSource.SetTransaction(cmd);
            
            return cmd;
        }

        /// <summary>
        /// �X�g�A�h�v���V�[�W���pIN�p�����[�^�����蓖�Ă�
        /// </summary>
        /// <param name="command">IDbCommand�I�u�W�F�N�g</param>
        /// <param name="args">�����l</param>
        /// <param name="argTypes">�����^�C�v</param>
        /// <param name="argNames">������</param>
        /// <param name="argDirection">�����̓��o�͎��</param>
        protected void BindParamters(IDbCommand command, object[] args, Type[] argTypes,
                                     string[] argNames, ParameterDirection[] argDirection)
        {
            if (args == null) return;
            for (var i = 0; i < args.Length; ++i)
            {
                var columnName = argNames[i];
                var vt = DataProviderUtil.GetBindVariableType(command);
                switch (vt)
                {
                    case BindVariableType.QuestionWithParam:
                        columnName = "?" + columnName;
                        break;
                    case BindVariableType.ColonWithParam:
                        if ("OracleCommand".Equals(command.GetExType().Name))
                        {
                            columnName = string.Empty + columnName;
                        }
                        else
                        {
                            columnName = ":" + columnName;
                        }
                        break;
                    default:
                        columnName = "@" + columnName;
                        break;
                }

                var dbType = GetDbValueType(argTypes[i]);
                var parameter = command.CreateParameter();
                parameter.ParameterName = columnName;
                parameter.Direction = argDirection[i];
                parameter.DbType = dbType;
                if ("OracleCommand".Equals(command.GetExType().Name) && args[i] is Array)
                {
                    // ODP.NET�̂ݔz��o�C���h�ɑΉ�
                    var info = parameter.GetExType().GetProperty("CollectionType",
                                                                        BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance);
                    var asms = AppDomain.CurrentDomain.GetAssemblies();
                    Assembly asm = null;
                    foreach (var assembly in asms)
                    {
                        if (assembly.GetName().Name == "Oracle.DataAccess" || assembly.GetName().Name == "Oracle.ManagedDataAccess")
                        {
                            asm = assembly;
                            break;
                        }
                    }
                    if (asm != null)
                    {
                        Type t;
                        if (asm.GetName().Name == "Oracle.DataAccess")
                            t = asm.GetType("Oracle.DataAccess.Client.OracleCollectionType");
                        else
                            t = asm.GetType("Oracle.ManagedDataAccess.Client.OracleCollectionType");
                        var f = t.GetField("PLSQLAssociativeArray");
                        info.SetValue(parameter, f.GetValue(null), null);
                    }

                    parameter.Size = ((Array)args[i]).Length;

                    if (parameter.Direction != ParameterDirection.Input)
                    {
                        // Output, Output/Input�ł͕K�v�B
                        info = parameter.GetExType().GetProperty("ArrayBindSize",
                                                               BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance);
                        int[] sizes = {4096};
                        Array.Resize(ref sizes, ((Array)args[i]).Length);
                        for (var j = 0; j < sizes.Length; j++)
                        {
                            sizes[j] = 4096;
                        }
                        info.SetValue(parameter, sizes, null);
                    }
                }
                else
                {
                    parameter.Size = 4096;
                }
                parameter.Value = args[i];

                if ("OleIDbCommand".Equals(command.GetExType().Name) && dbType == DbType.String)
                {
                    var oleDbParam = parameter as OleDbParameter;
                    if (oleDbParam != null) oleDbParam.OleDbType = OleDbType.VarChar;
                }
                else if ("SqlCommand".Equals(command.GetExType().Name) && dbType == DbType.String)
                {
                    var sqlDbParam = parameter as SqlParameter;
                    if (sqlDbParam != null) sqlDbParam.SqlDbType = SqlDbType.VarChar;
                }
                command.Parameters.Add(parameter);
            }
        }

        /// <summary>
        /// �߂�l�p�o�C���h�ϐ������蓖�Ă�
        /// </summary>
        /// <param name="command">IDbCommand�I�u�W�F�N�g</param>
        /// <param name="parameterName">�߂�l�p�����[�^��</param>
        /// <param name="dbType">DB�^�C�v</param>
        protected string BindReturnValues(IDbCommand command, string parameterName, DbType dbType)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Direction = ParameterDirection.ReturnValue;
            parameter.DbType = dbType;
            parameter.Size = 4096;
            if ("OleIDbCommand".Equals(command.GetExType().Name) && dbType == DbType.String)
            {
                var oleDbParam = parameter as OleDbParameter;
                if (oleDbParam != null) oleDbParam.OleDbType = OleDbType.VarChar;
            }
            else if ("SqlIDbCommand".Equals(command.GetExType().Name) && dbType == DbType.String)
            {
                var sqlDbParam = parameter as SqlParameter;
                if (sqlDbParam != null) sqlDbParam.SqlDbType = SqlDbType.VarChar;
            }
            command.Parameters.Add(parameter);

            return parameter.ParameterName;
        }

        /// <summary>
        /// DBType�֕ϊ�����
        /// </summary>
        /// <param name="type">�^�C�v</param>
        /// <returns></returns>
        protected static DbType GetDbValueType(Type type)
        {
            if (type == typeof(byte) || type.FullName == "System.Byte&" || type == typeof(byte[]) || type.FullName == "System.Byte[]&")
                return DbType.Byte;
            if (type == typeof(byte?) || type.FullName == "System.Nullable<Byte>&" || type == typeof(byte?[]) || type.FullName == "System.Nullable<Byte>[]&")
                return DbType.Byte;
            if (type == typeof(sbyte) || type.FullName == "System.SByte&" || type == typeof(sbyte[]) || type.FullName == "System.SByte[]&")
                return DbType.SByte;
            if (type == typeof(sbyte?) || type.FullName == "System.Nullable<SByte>&" || type == typeof(sbyte?[]) || type.FullName == "System.Nullable<SByte>[]&")
                return DbType.SByte;
            if (type == typeof(short) || type.FullName == "System.Int16&" || type == typeof(short[]) || type.FullName == "System.Int16[]&")
                return DbType.Int16;
            if (type == typeof(short?) || type.FullName == "System.Nullable<Int16>&" || type == typeof(short?[]) || type.FullName == "System.Nullable<Int16>[]&")
                return DbType.Int16;
            if (type == typeof(int) || type.FullName == "System.Int32&" || type == typeof(int[]) || type.FullName == "System.Int32[]&")
                return DbType.Int32;
            if (type == typeof(int?) || type.FullName == "System.Nullable<Int32>&" || type == typeof(int?[]) || type.FullName == "System.Nullable<Int32>[]&")
                return DbType.Int32;
            if (type == typeof(long) || type.FullName == "System.Int64&" || type == typeof(long[]) || type.FullName == "System.Int64[]&")
                return DbType.Int64;
            if (type == typeof(long?) || type.FullName == "System.Nullable<Int64>&" || type == typeof(long?[]) || type.FullName == "System.Nullable<Int64>[]&")
                return DbType.Int64;
            if (type == typeof(float) || type.FullName == "System.Single&" || type == typeof(float[]) || type.FullName == "System.Single[]&")
                return DbType.Single;
            if (type == typeof(float?) || type.FullName == "System.Nullable<Single>&" || type == typeof(float?[]) || type.FullName == "System.Nullable<Single>[]&")
                return DbType.Single;
            if (type == typeof(double) || type.FullName == "System.Double&" || type == typeof(double[]) || type.FullName == "System.Double[]&")
                return DbType.Double;
            if (type == typeof(double?) || type.FullName == "System.Nullable<Double>&" || type == typeof(double?[]) || type.FullName == "System.Nullable<Double>[]&")
                return DbType.Double;
            if (type == typeof(decimal) || type.FullName == "System.Decimal&" || type == typeof(decimal[]) || type.FullName == "System.Decimal[]&")
                return DbType.Decimal;
            if (type == typeof(decimal?) || type.FullName == "System.Nullable<Decimal>" || type == typeof(decimal[]) || type.FullName == "System.Nullable<Decimal>[]&")
                return DbType.Decimal;
            if (type == typeof(DateTime) || type.FullName == "System.DateTime&" || type == typeof(DateTime[]) || type.FullName == "System.DateTime[]&")
                return DbType.DateTime;
            if (type == typeof(DateTime?) || type.FullName == "System.Nullable<DateTime>&" || type == typeof(DateTime?[]) || type.FullName == "System.Nullable<DateTime>[]")
                return DbType.DateTime;
            if (type == ValueTypes.BYTE_ARRAY_TYPE)
                return DbType.Binary;
            if (type == typeof(string) || type.FullName == "System.String&" || type == typeof(string[]) || type.FullName == "System.String[]&")
                return DbType.String;
            if (type == typeof(bool) || type.FullName == "System.Boolean&" || type == typeof(bool[]) || type.FullName == "System.Boolean[]&")
                return DbType.Boolean;
            if (type == typeof(bool?) || type.FullName == "System.Nullable<Boolean>&" || type == typeof(bool?[]) || type.FullName == "System.Nullable<Boolean>[]&")
                return DbType.Boolean;
            if (type == typeof(Guid) || type.FullName == "System.Guid&" || type == typeof(Guid[]) || type.FullName == "System.Guid[]&")
                return DbType.Guid;
            if (type == typeof(Guid?) || type.FullName == "System.Nullable<Guid>&" || type == typeof(Guid?[]) || type.FullName == "System.Nullable<Guid>[]&")
                return DbType.Guid;

            return DbType.Object;
        }

        /// <summary>
        /// �p�����[�^�̕������擾����
        /// </summary>
        /// <param name="mi">���\�b�h���</param>
        /// <returns>�p�����[�^�����z��</returns>
        public static ParameterDirection[] GetParameterDirections(MethodInfo mi)
        {
            var parameters = mi.GetParameters();
            var ret = new ParameterDirection[parameters.Length];
            for (var i = 0; i < parameters.Length; ++i)
            {
                if (parameters[i].IsOut)
                {
                    ret[i] = ParameterDirection.Output;
                }
                else if (parameters[i].ParameterType.IsByRef)
                {
                    ret[i] = ParameterDirection.InputOutput;
                }
                else
                {
                    ret[i] = ParameterDirection.Input;
                }
            }
            return ret;
        }
    }
}
