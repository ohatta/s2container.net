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

namespace Seasar.Framework.Aop
{
    /// <summary>
    /// Interceptor����C���^�[�Z�v�g����Ă��郁�\�b�h�̏��ɃA�N�Z�X���邽�߂̃C���^�[�t�F�C�X
    /// </summary>
    /// <remarks>
    /// ���̃C���^�[�t�F�C�X��AOP�A���C�A���X�����B
    /// </remarks>
    /// <seealso href="http://aopalliance.sourceforge.net/doc/index.html">AOP Alliance</seealso>
    public interface IMethodInvocation
    {
        /// <summary>
        /// Intercept����郁�\�b�h��MethodBase
        /// </summary>
        MethodBase Method { get; }

        /// <summary>
        /// Intercept�����I�u�W�F�N�g
        /// </summary>
        object Target { get; }

        /// <summary>
        /// Intercept����郁�\�b�h�̈���
        /// </summary>
        object[] Arguments { get; }

        /// <summary>
        /// ���Ƀ`�F�[������Ă���Interceptor������΁AInterceptor���Ăяo���܂��i�ċA�I�ɌĂяo�����j
        /// ���Ƀ`�F�[������Ă���Interceptor��������΁AIntercept����Ă��郁�\�b�h�����s���܂�
        /// </summary>
        /// <returns>Intercept���ꂽ���\�b�h�̖߂�l</returns>
        object Proceed();
    }
}
