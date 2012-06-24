#region Copyright
/*
 * Copyright 2005-2012 the Seasar Foundation and the Others.
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

using Seasar.Framework.Aop;
using Seasar.Framework.Aop.Impl;

namespace Seasar.Framework.Container.Impl
{
    /// <summary>
    /// Aspectを定義します
    /// </summary>
    public class AspectDefImpl : ArgDefImpl, IAspectDef
    {
        private readonly IPointcut pointcut;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AspectDefImpl()
        {
        }

        public AspectDefImpl(IPointcut pointcut)
        {
            this.pointcut = pointcut;
        }

        public AspectDefImpl(IMethodInterceptor interceptor)
        {
            Value = interceptor;
        }

        public AspectDefImpl(IMethodInterceptor interceptor, IPointcut pointcut)
        {
            Value = interceptor;
            this.pointcut = pointcut;
        }

        #region AspectDef メンバ

        public IAspect Aspect
        {
            get
            {
                IMethodInterceptor interceptor = (IMethodInterceptor) Value;
                return new AspectImpl(interceptor, pointcut);
            }
        }

        #endregion
    }
}
