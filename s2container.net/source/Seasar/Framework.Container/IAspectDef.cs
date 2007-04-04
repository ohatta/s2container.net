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
using Seasar.Framework.Aop;

namespace Seasar.Framework.Container
{
    /// <summary>
    /// �R���|�[�l���g�ɓK�p����A�X�y�N�g���`����C���^�[�t�F�[�X�ł��B
    /// </summary>
    /// <remarks>
    /// <para>
    /// 1�̃R���|�[�l���g�ɕ����̃A�X�y�N�g���`���邱�Ƃ��\�ł��B 
    /// ��`�������ɃA�X�y�N�g�̃C���^�[�Z�v�^�����s����܂��B
    /// </para>
    /// <para>
    /// S2AOP.NET�ɂ�����C���^�[�Z�v�^�́A<see cref="Seasar.Framework.Aop.IMethodInterceptor"/>
    /// �C���^�[�t�F�[�X�����������N���X�̃R���|�[�l���g�Ƃ��Ē�`���܂��B
    /// �C���^�[�Z�v�^�[�̃Z�b�g���A�����̃R���|�[�l���g�ɓK�p����ꍇ�ɂ́A 
    /// �����̃C���^�[�Z�v�^��1�̃C���^�[�Z�v�^�E�R���|�[�l���g�Ƃ��Ē�`�ł���A
    /// <see cref="Seasar.Framework.Aop.Interceptors.InterceptorChain"/>
    /// ���g�p����Ɛݒ���ȗ����ł��܂��B
    /// </para>
    /// </remarks>
    /// <seealso cref="http://s2container.net.seasar.org/ja/aop.html">S2AOP.NET�̏ڍ�</seealso>
    public interface IAspectDef : IArgDef
    {
        /// <summary>
        /// �|�C���g�J�b�g
        /// </summary>
        IPointcut Pointcut { get; set; }

        /// <summary>
        /// �A�X�y�N�g
        /// </summary>
        IAspect Aspect { get; }
    }
}
