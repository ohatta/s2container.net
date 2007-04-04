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
    /// �擾���悤�Ƃ����R���|�[�l���g��S2�R���e�i��Ɍ�����Ȃ������ꍇ�ɃX���[����܂��B
    /// </summary>
    /// <remarks>
    /// <para>
    /// �R���|�[�l���g�̌����ɂ́A �R���|�[�l���g�L�[(�L�[�I�u�W�F�N�g)�Ƃ��āA 
    /// �N���X(�C���^�[�t�F�[�X)�܂��̓R���|�[�l���g�����g�p�ł��܂����A
    /// �ǂ���̏ꍇ�ł��R���|�[�l���g��������Ȃ������ꍇ�ɂ́A ���̗�O���X���[����܂��B
    /// </para>
    /// </remarks>
    [Serializable]
    public class ComponentNotFoundRuntimeException : SRuntimeException
    {
        private object componentKey;

        /// <summary>
        /// �R���|�[�l���g�̌����ɗp�����R���|�[�l���g�L�[���w�肵�āA
        /// <code>ComponentNotFoundRuntimeException</code>���\�z���܂��B
        /// </summary>
        /// <param name="componentKey">�R���|�[�l���g�L�[</param>
        public ComponentNotFoundRuntimeException(object componentKey)
            : base("ESSR0046", new object[] { componentKey })
        {
            this.componentKey = componentKey;
        }

        /// <summary>
        /// �R���|�[�l���g�̌����ɗp�����R���|�[�l���g�L�[
        /// </summary>
        public object ComponentKey
        {
            get { return componentKey; }
        }
    }
}
