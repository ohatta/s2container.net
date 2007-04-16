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
    /// �w�肳�ꂽ�p�X�̃t�@�C�����ɁA �g���q���t���Ă��Ȃ������ꍇ�ɃX���[����܂��B
    /// </summary>
    /// <remarks>
    /// <para>
    /// <see cref="Seasar.Framework.Container.Factory.S2ContainerFactory">S2�R���e�i�t�@�N�g��</see>�́A
    /// S2�R���e�i���\�z���悤�Ƃ����ۂɁA �g���q�ɉ�����
    /// <see cref="Seasar.Framework.Container.Factory.IS2ContainerBuilder">S2�R���e�i�r���_�[</see>��
    /// �؂�ւ��܂��B���̂��߁A �w�肳�ꂽ�ݒ�t�@�C��(dicon�t�@�C���Ȃ�)�̃t�@�C������
    /// �g���q���t���Ă��Ȃ��ꍇ�ɂ́A ���̗�O���������܂��B
    /// </para>
    /// </remarks>
    [Serializable]
    public class ExtensionNotFoundRuntimeException : SRuntimeException
    {
        private string path;

        /// <summary>
        /// �p�X���w�肵��<code>ExtensionNotFoundRuntimeException</code>���\�z���܂��B
        /// </summary>
        /// <param name="path">�w�肳�ꂽ�p�X</param>
        public ExtensionNotFoundRuntimeException(string path)
            : base("ESSR0074", new object[] { path })
        {
            this.path = path;
        }

        /// <summary>
        /// �w�肳�ꂽ�p�X��Ԃ��܂��B
        /// </summary>
        /// <value>�w�肳�ꂽ�p�X</value>
        public string Path
        {
            get { return path; }
        }
    }
}
