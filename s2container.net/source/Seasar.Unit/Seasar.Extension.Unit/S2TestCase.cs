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

using System.Data;
using Seasar.Extension.ADO;
using Seasar.Extension.ADO.Impl;
using Seasar.Extension.DataSets;
using Seasar.Extension.DataSets.Impl;
using Seasar.Framework.Exceptions;
using Seasar.Framework.Unit;
using Seasar.Framework.Util;
using IDataReader = Seasar.Extension.DataSets.IDataReader;

namespace Seasar.Extension.Unit
{
    public class S2TestCase : S2FrameworkTestCaseBase
    {
        private IDataSource dataSource;

        private IDbConnection connection;

        public S2TestCase()
        {
        }

        public IDataSource DataSource
        {
            get
            {
                if (dataSource == null)
                {
                    throw new EmptyRuntimeException("dataSource");
                }
                return dataSource;
            }
        }

        public IDbConnection Connection
        {
            get
            {
                if (connection != null)
                {
                    return connection;
                }
                connection = DataSourceUtil.GetConnection(dataSource);
                return connection;
            }
        }

        public bool HasConnection
        {
            get { return connection != null; }
        }

        internal void SetConnection(IDbConnection connection)
        {
            this.connection = connection;
        }

        internal void SetDataSource(IDataSource dataSource)
        {
            this.dataSource = dataSource;
        }

        /// <summary>
        /// Excel�t�@�C����ǂ݁ADataSet���쐬���܂��B
        /// �V�[�g�����e�[�u�����A��s�ڂ��J�������A��s�ڈȍ~���f�[�^ �Ƃ��ēǂݍ��݂܂��B
        /// 
        /// �p�X��Assembly�Ŏw�肳��Ă���f�B���N�g�������[�g�Ƃ���B
        /// �ݒ�t�@�C���̐�΃p�X���A�t�@�C�����݂̂��w�肵�܂��B
        /// �t�@�C�����݂̂̏ꍇ�A�e�X�g�P�[�X�Ɠ����p�b�P�[�W�ɂ�����̂Ƃ��܂��B
        /// <seealso cref="Seasar.Extension.DataSets.Impl.XlsReader.Read"/>
        /// </summary>
        /// <param name="path">Excel�t�@�C���̃p�X</param>
        /// <returns>Excel�t�@�C���̓��e����쐬����DataSet</returns>
        public virtual DataSet ReadXls(string path)
        {
            IDataReader reader = new XlsReader(ConvertPath(path));
            return reader.Read();
        }

        /// <summary>
        /// DataSet�̓��e����AExcel�t�@�C�����쐬���܂��B
        /// �V�[�g���Ƀe�[�u�����A��s�ڂɃJ�������A��s�ڈȍ~�Ƀf�[�^ ���������݂܂��B
        /// 
        /// �p�X��Assembly�Ŏw�肳��Ă���f�B���N�g�������[�g�Ƃ���B
        /// �ݒ�t�@�C���̐�΃p�X���A�t�@�C�����݂̂��w�肵�܂��B
        /// �t�@�C�����݂̂̏ꍇ�A�e�X�g�P�[�X�Ɠ����p�b�P�[�W�ɂ�����̂Ƃ��܂��B
        /// <seealso cref="Seasar.Extension.DataSets.Impl.XlsWriter.Write"/>
        /// </summary>
        /// <param name="path">Excel�t�@�C���̃p�X</param>
        /// <param name="dataSet">Excel�t�@�C���ɏ������ޓ��e��DataSet</param>
        public virtual void WriteXls(string path, DataSet dataSet)
        {
            IDataWriter writer = new XlsWriter(ConvertPath(path));
            writer.Write(dataSet);
        }

        /// <summary>
        /// DataSet��DB�ɏ������݂܂��B
        /// <seealso cref="Seasar.Extension.DataSets.Impl.SqlWriter.Write"/>
        /// </summary>
        /// <param name="dataSet">�f�[�^�x�[�X�ɏ������ޓ��e��DataSet</param>
        public virtual void WriteDb(DataSet dataSet)
        {
            IDataWriter writer = new SqlWriter(DataSource);
            writer.Write(dataSet);
        }

        /// <summary>
        /// DB���烌�R�[�h��ǂݍ��݁ADataTable���쐬���܂��B
        /// <seealso cref="Seasar.Extension.DataSets.Impl.SqlTableReader.Read"/>
        /// </summary>
        /// <param name="table">�ǂݍ��ރe�[�u����</param>
        /// <returns>�ǂݍ��񂾓��e����쐬����DataTable</returns>
        public virtual DataTable ReadDbByTable(string table)
        {
            return ReadDbByTable(table, null);
        }

        /// <summary>
        /// DB���烌�R�[�h��ǂݍ��݁ADataTable���쐬���܂��B
        /// �ǂݍ��ރ��R�[�h��condition�̏����𖞂������R�[�h�ł��B condition�ɂ�" WHERE "�������Z�b�g���Ă��������B
        /// <seealso cref="Seasar.Extension.DataSets.Impl.SqlTableReader.Read"/>
        /// </summary>
        /// <param name="table">�ǂݍ��ރe�[�u����</param>
        /// <param name="condition">������(WHERE�̌��)</param>
        /// <returns>�ǂݍ��񂾓��e����쐬����DataTable</returns>
        public virtual DataTable ReadDbByTable(string table, string condition)
        {
            SqlTableReader reader = new SqlTableReader(DataSource);
            reader.SetTable(table, condition);
            return reader.Read();
        }

        /// <summary>
        /// DB����SQL���̎��s���ʂ��擾���ADataTable���쐬���܂��B
        /// �쐬����DataTable�̃e�[�u������tableName�ɂȂ�܂��B
        /// <seealso cref="Seasar.Extension.DataSets.Impl.SqlTableReader.Read"/>
        /// </summary>
        /// <param name="sql">���s����SQL��</param>
        /// <param name="tableName">�쐬����DataTable�̃e�[�u����</param>
        /// <returns>�ǂݏo�������e��DataTable</returns>
        public virtual DataTable ReadDbBySql(string sql, string tableName)
        {
            SqlTableReader reader = new SqlTableReader(DataSource);
            reader.SetSql(sql, tableName);
            return reader.Read();

        }

        /// <summary>
        /// Excel�t�@�C����ǂݍ��݁ADB�ɏ������݂܂��B
        /// �V�[�g�����e�[�u�����A��s�ڂ��J�������A��s�ڈȍ~���f�[�^ �Ƃ��ēǂݍ��݂܂��B
        /// 
        /// �p�X��Assembly�Ŏw�肳��Ă���f�B���N�g�������[�g�Ƃ���B
        /// �ݒ�t�@�C���̐�΃p�X���A�t�@�C�����݂̂��w�肵�܂��B
        /// �t�@�C�����݂̂̏ꍇ�A�e�X�g�P�[�X�Ɠ����p�b�P�[�W�ɂ�����̂Ƃ��܂��B
        /// <seealso cref="Seasar.Extension.DataSets.Impl.XlsReader.Read"/>
        /// <seealso cref="Seasar.Extension.DataSets.Impl.SqlWriter.Write"/>
        /// </summary>
        /// <param name="path">Excel�t�@�C���̃p�X</param>
        public virtual void ReadXlsWriteDb(string path)
        {
            WriteDb(ReadXls(path));
        }

        /// <summary>
        /// Excel�t�@�C����ǂݍ��݁ADB�ɏ������݂܂��B
        /// �V�[�g�����e�[�u�����A��s�ڂ��J�������A��s�ڈȍ~���f�[�^ �Ƃ��ēǂݍ��݂܂��B
        /// Excel�̓��e��DB�̃��R�[�h�ƂŎ�L�[����v������̂�����΁A ���̃��R�[�h���폜������ɏ������݂܂��B
        /// 
        /// �p�X��Assembly�Ŏw�肳��Ă���f�B���N�g�������[�g�Ƃ���B
        /// �ݒ�t�@�C���̐�΃p�X���A�t�@�C�����݂̂��w�肵�܂��B
        /// �t�@�C�����݂̂̏ꍇ�A�e�X�g�P�[�X�Ɠ����p�b�P�[�W�ɂ�����̂Ƃ��܂��B
        /// <seealso cref="Seasar.Extension.DataSets.Impl.XlsReader.Read"/>
        /// <seealso cref="Seasar.Extension.DataSets.Impl.SqlWriter.Write"/>
        /// </summary>
        /// <param name="path">Excel�t�@�C���̃p�X</param>
        public virtual void ReadXlsReplaceDb(string path)
        {
            DataSet dataSet = ReadXls(path);
            DeleteDb(dataSet);
            WriteDb(dataSet);
        }

        /// <summary>
        /// Excel�t�@�C����ǂݍ��݁ADB�ɏ������݂܂��B
        /// �V�[�g�����e�[�u�����A��s�ڂ��J�������A��s�ڈȍ~���f�[�^ �Ƃ��ēǂݍ��݂܂��B
        /// �ΏۂƂȂ�e�[�u���̃��R�[�h��S�č폜������ɏ������݂܂��B
        /// 
        /// �p�X��Assembly�Ŏw�肳��Ă���f�B���N�g�������[�g�Ƃ���B
        /// �ݒ�t�@�C���̐�΃p�X���A�t�@�C�����݂̂��w�肵�܂��B
        /// �t�@�C�����݂̂̏ꍇ�A�e�X�g�P�[�X�Ɠ����p�b�P�[�W�ɂ�����̂Ƃ��܂��B
        /// <seealso cref="Seasar.Extension.DataSets.Impl.XlsReader.Read"/>
        /// <seealso cref="Seasar.Extension.DataSets.Impl.SqlWriter.Write"/>
        /// </summary>
        /// <param name="path">Excel�t�@�C���̃p�X</param>
        public virtual void ReadXlsAllReplaceDb(string path)
        {
            DataSet dataSet = ReadXls(path);
            for (int i = dataSet.Tables.Count - 1; i >= 0; --i)
            {
                DeleteTable(dataSet.Tables[i].TableName);
            }
            WriteDb(dataSet);
        }

        /// <summary>
        /// DataSet�ɑΉ�����DB�̃��R�[�h��ǂݍ��݁ADataSet���쐬���܂� �B
        /// <seealso cref="Seasar.Extension.DataSets.Impl.SqlReloadReader.Read"/>
        /// </summary>
        /// <param name="dataSet">�Ώ�DB�ɑΉ�����DataSet</param>
        /// <returns>�ŐV��Ԃ�DataSet</returns>
        public virtual DataSet Reload(DataSet dataSet)
        {
            return new SqlReloadReader(DataSource, dataSet).Read();
        }

        /// <summary>
        /// DataTable�ɑΉ�����DB�̃��R�[�h��ǂݍ��݁ADataTable���쐬 ���܂��B
        /// <seealso cref="Seasar.Extension.DataSets.Impl.SqlReloadReader.Read"/>
        /// </summary>
        /// <param name="table">�Ώ�DB�ɑΉ�����DataTable</param>
        /// <returns>�ŐV��Ԃ�DataTable</returns>
        public virtual DataTable Reload(DataTable table)
        {
            return new SqlReloadTableReader(DataSource, table).Read();
        }

        /// <summary>
        /// DataSet�ɑΉ�����DB�̃��R�[�h���폜���܂��B
        /// <seealso cref="Seasar.Extension.DataSets.Impl.SqlDeleteTableWriter.Write"/>
        /// </summary>
        /// <param name="dataSet">�Ώ�DB�ɑΉ�����DataSet</param>
        public virtual void DeleteDb(DataSet dataSet)
        {
            SqlDeleteTableWriter writer = new SqlDeleteTableWriter(DataSource);
            for (int i = dataSet.Tables.Count - 1; i >= 0; --i)
            {
                writer.Write(dataSet.Tables[i]);
            }
        }

        /// <summary>
        /// DB����w�肷��e�[�u���̑S���R�[�h���폜���܂��B
        /// </summary>
        /// <param name="tableName">�폜�Ώۂ̃e�[�u����</param>
        public virtual void DeleteTable(string tableName)
        {
            IUpdateHandler handler = new BasicUpdateHandler(
                DataSource,
                "DELETE FROM " + tableName
                );
            handler.Execute(null);
        }
    }
}
