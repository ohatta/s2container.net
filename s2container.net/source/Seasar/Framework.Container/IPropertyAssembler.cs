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
    /// �v���p�e�B�E�C���W�F�N�V������t�B�[���h�E�C���W�F�N�V���������s���ăR���|�[�l���g��g�ݗ��Ă܂��B
    /// </summary>
    /// <remarks>
    /// <para>
    /// �C���W�F�N�V�����̎��s�́A<see cref="IPropertyDef">�v���p�e�B��`</see>�Ɋ�Â��čs���܂��B 
    /// �v���p�e�B��`���w�肳��Ă��Ȃ��ꍇ�̓���́A
    /// <see cref="IAutoBindingDef">�����o�C���f�B���O</see>�Ɋ�Â��܂��B
    /// </para>
    /// <para>
    /// �܂��A<see cref="IComponentDef">�R���|�[�l���g��`</see>��
    /// <code>externalBinding</code>������<code>true</code>�̏ꍇ�A
    /// <see cref="IExternalContext"/>�̕ێ����Ă���l���o�C���f�B���O�̑ΏۂƂ��܂��B
    /// </para>
    /// </remarks>
    /// <seealso cref="IPropertyDef"/>
    /// <seealso cref="IAutoBindingDef"/>
    /// <seealso cref="IExternalContext"/>
    public interface IPropertyAssembler
    {
        /// <summary>
        /// �w�肳�ꂽ<code>component</code>�ɑ΂��āA
        /// �v���p�e�B�E�C���W�F�N�V������t�B�[���h�E�C���W�F�N�V���������s���܂��B
        /// �R���|�[�l���g��`��<code>externalBinding</code>������<code>true</code>�ɂ��ւ�炸�A
        /// <see cref="IExternalContext"/>��S2�R���e�i�ɐݒ肳��Ă��Ȃ��ꍇ�ɂ́A
        /// EmptyRuntimeException���X���[���܂��B
        /// </summary>
        /// <param name="component">S2�R���e�i��̃R���|�[�l���g���Z�b�g�����Ώ�</param>
        /// <exception cref="Seasar.Framework.Beans.IllegalPropertyRuntimeException">
        /// �v���p�e�B��������Ȃ��Ȃǂ̗��R�ŃC���W�F�N�V�����Ɏ��s�����ꍇ
        /// </exception>
        /// <exception cref="Seasar.Framework.Exceptions.EmptyRuntimeException">
        /// ExternalContext��S2�R���e�i�ɐݒ肳��Ă��Ȃ��ꍇ
        /// </exception>
        void Assemble(object component);
    }
}
