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
    /// ���̃C���^�[�t�F�[�X�́A�A�X�y�N�g��`��o�^����ю擾���邱�Ƃ��ł���I�u�W�F�N�g��\���܂��B
    /// </summary>
    /// <remarks>
    /// <para>
    /// �A�X�y�N�g��`�͕����o�^���邱�Ƃ��o���܂��B
    /// �A�X�y�N�g��`�̎擾�̓C���f�b�N�X�ԍ����w�肵�čs���܂��B
    /// �A�X�y�N�g��`�͓o�^����Ă��鏇�ɓK�p����܂��B
    /// </para>
    /// </remarks>
    /// <seealso cref="IAspectDef"/>
    public interface IAspectDefAware
    {
        /// <summary>
        /// �A�X�y�N�g��`��ǉ����܂��B
        /// </summary>
        /// <param name="aspectDef">�A�X�y�N�g��`</param>
        void AddAspectDef(IAspectDef aspectDef);

        /// <summary>
        /// �A�X�y�N�g��`���w��̈ʒu�ɒǉ����܂��B
        /// </summary>
        /// <param name="index">�A�X�y�N�g��`��ǉ�����ʒu</param>
        /// <param name="aspectDef">�A�X�y�N�g��`</param>
        void AddAspectDef(int index, IAspectDef aspectDef);

        /// <summary>
        /// �o�^����Ă���<see cref="IAspectDef">�A�X�y�N�g��`</see>�̐���Ԃ��܂��B
        /// </summary>
        /// <remarks>
        /// <para>
        /// �o�^����Ă���<see cref="Seasar.Framework.Aop.IMethodInterceptor">�C���^�[�Z�v�^</see>
        /// �̐��ł͂Ȃ��A�A�X�y�N�g��`�̐���Ԃ��܂��B
        /// �A�X�y�N�g��`�̃R���|�[�l���g(�C���^�[�Z�v�^)�̃N���X��
        /// <see cref="Seasar.Framework.Aop.Interceptors.InterceptorChain"/>�ŁA
        /// ���̒��ɕ����̃C���^�[�Z�v�^���܂܂��ꍇ���A 1�̃A�X�y�N�g��`�Ƃ��ăJ�E���g���܂��B
        /// </para>
        /// </remarks>
        /// <value>�o�^����Ă���A�X�y�N�g��`�̐�</value>
        int AspectDefSize { get; }

        /// <summary>
        /// �w�肳�ꂽ�C���f�b�N�X�ԍ�<code>index</code>�̃A�X�y�N�g��`��Ԃ��܂��B
        /// </summary>
        /// <remarks>
        /// <para>
        /// �C���f�b�N�X�ԍ��́A �o�^�������Ԃ� 0,1,2,�c �ƂȂ�܂��B
        /// </para>
        /// </remarks>
        /// <param name="index">�A�X�y�N�g��`���w�肷��C���f�b�N�X�ԍ�</param>
        /// <returns>�A�X�y�N�g��`</returns>
        IAspectDef GetAspectDef(int index);
    }
}
