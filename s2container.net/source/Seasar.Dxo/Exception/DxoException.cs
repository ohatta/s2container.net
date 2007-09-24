#region Copyright

/*
 * Copyright 2005-2007 the Seasar Foundation and the Others.
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

namespace Seasar.Dxo.Exception
{
    /// <summary>
    /// DXO�l�[���X�y�[�X�ŃX���[�����O�̃��[�g�N���X
    /// </summary>
    [Serializable]
    public class DxoException : System.Exception
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="info">�V���A�����܂��͋t�V���A�����ɕK�v�Ȃ��ׂẴf�[�^</param>
        /// <param name="context">�w�肵���V���A�����X�g���[���̓]�����Ɠ]����</param>
        protected DxoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            ;
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="message">��O��������郁�b�Z�[�W</param>
        /// <param name="innerException">���̗�O�̔������ƂȂ�����O</param>
        public DxoException(string message, System.Exception innerException)
            : base(message, innerException)
        {
            ;
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="message">��O��������郁�b�Z�[�W</param>
        public DxoException(string message) : base(message)
        {
            ;
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public DxoException()
        {
            ;
        }
    }
}