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
using Seasar.Framework.Exceptions;

namespace Seasar.Framework.Container
{
    /// <summary>
    /// dicon�t�@�C���Ȃǂ̐ݒ���ɑΉ�����S2�R���e�i���A 
    /// �R���e�i�c���[�ɓo�^����Ă��Ȃ������ꍇ�ɃX���[����܂��B
    /// </summary>
    [Serializable]
    public class ContainerNotRegisteredRuntimeException : SRuntimeException
    {
        private string path;

        /// <summary>
        /// �o�^����Ă��Ȃ������ݒ���̃p�X���w�肵�āA
        /// <code>ContainerNotRegisteredRuntimeException</code>���\�z���܂��B
        /// </summary>
        /// <param name="path">�o�^����Ă��Ȃ������ݒ���̃p�X</param>
        public ContainerNotRegisteredRuntimeException(string path)
            : base("ESSR0075", new object[] { path })
        {
            this.path = path;
        }

        /// <summary>
        /// �R���e�i�c���[�ɓo�^����Ă��Ȃ������ݒ���̃p�X��Ԃ��܂��B
        /// </summary>
        /// <value>�o�^����Ă��Ȃ������ݒ���̃p�X</value>
        public string Path
        {
            get { return path; }
        }
    }
}
