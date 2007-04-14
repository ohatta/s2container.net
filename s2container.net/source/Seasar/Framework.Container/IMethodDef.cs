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
using System.Reflection;

namespace Seasar.Framework.Container
{
    /// <summary>
    /// ���\�b�h�E�C���W�F�N�V�������`���邽�߂̃C���^�[�t�F�[�X�ł��B
    /// </summary>
    /// <remarks>
    /// <para>
    /// ���\�b�h�E�C���W�F�N�V�����Ƃ́A�C�ӂ̃��\�b�h�⎮�̌Ăяo���ɂ��
    /// �R���|�[�l���g���C���W�F�N�V�������邱�Ƃł��B
    /// </para>
    /// <para>
    /// ��Ƃ��āA<code>addFoo(Foo)</code> ���\�b�h��ʂ��� <code>Foo</code>��
    /// �C���W�F�N�V��������ꍇ�ɗ��p���邱�Ƃ��ł��܂��B
    /// �����̂Ȃ����\�b�h��C�ӂ̎����Ăяo�����Ƃ��ł��܂��B
    /// </para>
    /// <para>
    /// �R���|�[�l���g�������������Ƃ��Ɏ��s�����initMethod�C���W�F�N�V�����ƁA
    /// �R���e�i�̏I�����Ɏ��s�����desoryMethod�C���W�F�N�V����������܂��B 
    /// DestroyMethod�C���W�F�N�V�������K�p�����̂́A
    /// �R���|�[�l���g��instance�v�f��<code>singleton</code>�̏ꍇ�����ł��B
    /// </para>
    /// </remarks>
    public interface IMethodDef : IArgDefAware
    {
        /// <summary>
        /// ���s���郁�\�b�h��Ԃ��܂��B
        /// </summary>
        /// <value>���s���郁�\�b�h</value>
        MethodInfo Method { get; }

        /// <summary>
        /// ���\�b�h����Ԃ��܂��B
        /// </summary>
        /// <value>���\�b�h��</value>
        string MethodName { get; }

        /// <summary>
        /// ���\�b�h������Ԃ��܂��B
        /// </summary>
        /// <value>���\�b�h����</value>
        object[] Args { get; }

        /// <summary>
        /// ��������ю���]������R���e�L�X�g�ƂȂ�S2�R���e�i���擾�E�ݒ肵�܂��B
        /// </summary>
        /// <value>��������ю���]������R���e�L�X�g�ƂȂ�S2�R���e�i</value>
        IS2Container Container { get; set; }

        /// <summary>
        /// ���s����鎮���擾�E�ݒ肵�܂��B
        /// </summary>
        /// <value>���s����鎮</value>
        IExpression Expression { get; set; }
    }
}
