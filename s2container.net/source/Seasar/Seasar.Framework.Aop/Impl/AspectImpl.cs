#region Copyright
/*
 * Copyright 2005-2015 the Seasar Foundation and the Others.
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

namespace Seasar.Framework.Aop.Impl
{
    /// <summary>
    /// IAspect�C���^�[�t�F�C�X�̎���
    /// </summary>
    [Serializable]
    public class AspectImpl : IAspect
    {
        /// <summary>
        /// Interceptor
        /// </summary>
        private readonly IMethodInterceptor _methodInterceptor;

        /// <summary>
        /// Pointcut
        /// </summary>
        /// <remarks>
        /// ���̃t�B�[���h��null�̏ꍇ�͂��ׂẴ��\�b�h��Intercept����܂��B
        /// </remarks>
        private IPointcut _pointcut;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="methodInterceptor">IMethodInterceptor</param>
        /// <param name="pointcut">
        /// IPointcut(null�ł��悢�B���̏ꍇ���ׂẴ��\�b�h��Intercept�̑ΏۂƂȂ�)
        /// </param>
        public AspectImpl(IMethodInterceptor methodInterceptor, IPointcut pointcut)
        {
            _methodInterceptor = methodInterceptor;
            _pointcut = pointcut;
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <remarks>
        /// ���ׂẴ��\�b�h��Intercept����ꍇ�͂��̃R���X�g���N�^�������܂��B
        /// </remarks>
        /// <param name="methodInterceptor">IMethodInterceptor</param>
        public AspectImpl(IMethodInterceptor methodInterceptor)
            : this(methodInterceptor, null)
        {
        }

        #region IAspect �����o

        /// <summary>
        /// Advice(Interceptor)
        /// </summary>
        public IMethodInterceptor MethodInterceptor
        {
            get { return _methodInterceptor; }
        }

        /// <summary>
        /// Pointcut
        /// </summary>
        public IPointcut Pointcut
        {
            get { return _pointcut; }
            set { _pointcut = value; }
        }

        #endregion
    }
}
