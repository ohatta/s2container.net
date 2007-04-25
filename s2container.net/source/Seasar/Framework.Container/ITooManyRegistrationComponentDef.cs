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

namespace Seasar.Framework.Container
{
    /// <summary>
    /// S2�R���e�i����1�̃L�[�ŕ����o�^���ꂽ�R���|�[�l���g�̒�`��\���C���^�[�t�F�[�X�ł��B
    /// </summary>
    /// <remarks>
    /// <para>
    /// S2�R���e�i�ɃR���|�[�l���g���o�^�����ۂɁA ���̃L�[(�R���|�[�l���g�̃N���X�A
    /// �C���^�[�t�F�[�X�A���邢�͖��O)�ɑΉ�����R���|�[�l���g�����łɓo�^����Ă���ƁA
    /// �R���|�[�l���g��`��<code>TooManyRegistrationComponentDef</code>�ɂȂ�܂��B
    /// </para>
    /// <para>
    /// <code>ITooManyRegistrationComponentDef</code>�Œ�`����Ă���R���|�[�l���g���擾���悤�Ƃ���ƁA
    /// <see cref="TooManyRegistrationRuntimeException"/>���X���[����܂��B
    /// </para>
    /// </remarks>
    /// <seealso cref="TooManyRegistrationRuntimeException"/>
    public interface ITooManyRegistrationComponentDef : IComponentDef
    {
        /// <summary>
        /// �����L�[�œo�^���ꂽ�R���|�[�l���g��`��ǉ����܂��B
        /// </summary>
        /// <param name="cd">�����L�[�œo�^���ꂽ�R���|�[�l���g��`</param>
        void AddComponentDef(IComponentDef cd);

        /// <summary>
        /// �����o�^���ꂽ�R���|�[�l���g�̒�`��̃N���X�̔z���Ԃ��܂��B
        /// </summary>
        /// <value>�����o�^���ꂽ�R���|�[�l���g�̒�`��̃N���X�̔z��</value>
        Type[] ComponentTypes { get; }

        /// <summary>
        /// �����o�^���ꂽ�R���|�[�l���g��`�̔z���Ԃ��܂��B
        /// </summary>
        /// <value>�����o�^���ꂽ�R���|�[�l���g��`�̔z��</value>
        IComponentDef[] ComponentDefs { get; }
    }
}
