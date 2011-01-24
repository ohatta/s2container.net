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
using System.Runtime.Serialization;

namespace Seasar.Framework.Exceptions
{
    /// <summary>
    /// RDBMS���x���܂��̓G���[��Ԃ����Ƃ��ɃX���[������O
    /// </summary>
    [Serializable]
    public class SQLRuntimeException : SRuntimeException
    {
        private readonly string _sql;

        /// <summary>
        /// SQLRuntimeException�N���X�̐V�����C���X�^���X�����������A�����ƂȂ�����O��ݒ肷��
        /// </summary>
        /// <param name="cause">�����ƂȂ�����O</param>
        public SQLRuntimeException(Exception cause)
            : base("ESSR0071", new object[] { cause }, cause)
        {
        }

        /// <summary>
        /// SQLRuntimeException�N���X�̐V�����C���X�^���X�����������A�����ƂȂ�����O��SQL��ݒ肷��
        /// </summary>
        /// <param name="cause">�����ƂȂ�����O</param>
        /// <param name="sql">�����ƂȂ���SQL</param>
        public SQLRuntimeException(Exception cause, string sql)
            : this(cause)
        {
            _sql = sql;
        }

        /// <summary>
        /// �V���A���������f�[�^���g�p���āASQLRuntimeException�N���X�̐V�����C���X�^���X������������
        /// </summary>
        /// <param name="info">�V���A�������ꂽ�I�u�W�F�N�g �f�[�^��ێ�����I�u�W�F�N�g</param>
        /// <param name="context">�]�����܂��͓]����Ɋւ���R���e�L�X�g���</param>
        public SQLRuntimeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _sql = info.GetString("_sql");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("_sql", _sql, typeof(string));
            base.GetObjectData(info, context);
        }

        /// <summary>
        /// ��O�̌����ƂȂ���SQL��ݒ�������͎擾����
        /// </summary>
        public string Sql
        {
            get { return _sql; }
        }
    }
}
