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
    /// �R���X�g���N�^�E�C���W�F�N�V���������s���ăR���|�[�l���g��g�ݗ��Ă܂��B
    /// </summary>
    /// <remarks>
    /// <para>
    /// <see cref="IComponentDef">�R���|�[�l���g��`</see>
    /// �ɑ΂��Ė����I�ɃR���X�g���N�^�̈������w�肳��Ȃ��������̓���́A
    /// <see cref="IAutoBindingDef">�����o�C���f�B���O�^�C�v��`</see>�Ɋ�Â��܂��B
    /// </para>
    /// </remarks>
    /// <seealso cref="Seasar.Framework.Container.Assembler.AbstractConstructorAssembler"/>
    /// <see cref="Seasar.Framework.Container.Assembler.AutoConstructorAssembler"/>
    /// <see cref="Seasar.Framework.Container.Assembler.DefaultConstructorConstructorAssembler"/>
    public interface IConstructorAssembler
    {
        /// <summary>
        /// �R���X�g���N�^�E�C���W�F�N�V���������s���āA �g�ݗ��Ă��R���|�[�l���g��Ԃ��܂��B
        /// </summary>
        /// <remarks>
        /// <para>
        /// �܂��A<see cref="IComponentDef">�R���|�[�l���g��`</see>��
        /// <see cref="IExpression">��</see>���w�肳��Ă����ꍇ�A
        /// ���̕]�����ʂ��R���|�[�l���g�Ƃ��ĕԂ��܂��B
        /// </para>
        /// </remarks>
        /// <returns>�R���X�g���N�^�E�C���W�F�N�V�����ς݂̃R���|�[�l���g�̃C���X�^���X</returns>
        /// <exception cref="Seasar.Framework.Beans.ConstructorNotFoundRuntimeException">
        /// �K�؂ȃR���X�g���N�^��������Ȃ������ꍇ
        /// </exception>
        /// <exception cref="IllegalConstructorRuntimeException">
        /// �R���X�g���N�^�̈����ƂȂ�R���|�[�l���g��������Ȃ������ꍇ
        /// </exception>
        /// <exception cref="ClassUnmatchRuntimeException">
        /// �g�ݗ��Ă��R���|�[�l���g�̌^���R���|�[�l���g��`�̃N���X�w��ɓK�����Ȃ������ꍇ
        /// </exception>
        object assembler();
    }
}
