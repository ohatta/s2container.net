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
    /// ���\�b�h�E�C���W�F�N�V���������s���ăR���|�[�l���g��g�ݗ��Ă܂��B
    /// </summary>
    /// <remarks>
    /// <para>
    /// �C���W�F�N�V�����̎��s��<see cref="IMethodDef">���\�b�h��`</see>�Ɋ�Â��čs���܂��B
    /// </para>
    /// </remarks>
    public interface IMethodAssembler
    {
        /// <summary>
        /// �w�肳�ꂽ<code>component</code>�ɑ΂��āA ���\�b�h�E�C���W�F�N�V���������s���܂��B
        /// </summary>
        /// <remarks>
        /// <para>
        /// ���\�b�h�̈����Ƃ��Ďw�肳�ꂽ�R���|�[�l���g��������Ȃ��ꍇ�ɂ́A
        /// IllegalMethodRuntimeException���X���[����܂��B
        /// </para>
        /// </remarks>
        /// <param name="component">S2�R���e�i��̃R���|�[�l���g���Z�b�g�����Ώ�</param>
        /// <exception cref="IllegalMethodRuntimeException">
        /// ���\�b�h�̈����Ƃ��Ďw�肳�ꂽ�R���|�[�l���g��������Ȃ��ꍇ
        /// </exception>
        /// <exception cref="InvocationTargetRuntimeException">
        /// ���\�b�h���s���Ɍ�����O�����������ꍇ
        /// (���s����O�ƃG���[�����������ꍇ�ɂ͂��̂܂܃X���[����܂�)
        /// </exception>
        /// <exception cref="IllegalAccessRuntimeException"></exception>
        void Assemble(object component);
    }
}
