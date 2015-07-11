﻿#region Copyright
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

#if NET_4_0
using Seasar.Unit.Core;
#else
#region NET2.0
using System;
using MbUnit.Core.Framework;
using MbUnit.Core.Invokers;
#endregion
#endif

namespace Seasar.Extension.Unit
{
    /// <summary>
    /// S2Container用テスト属性
    /// </summary>
#if NET_4_0
    public class S2Attribute : S2MbUnitAttributeBase
    {
        public S2Attribute()
            : base()
        {
        }

        public S2Attribute(Seasar.Extension.Unit.Tx txTreatment)
            : base(txTreatment)
        {
        }

        protected override S2TestCaseRunnerBase CreateRunner(Seasar.Extension.Unit.Tx txTreatment)
        {
            return new S2TestCaseRunner(txTreatment);
        }
    }
#else
#region NET2.0
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class S2Attribute : DecoratorPatternAttribute
    {
        private readonly Tx _tx;

        public S2Attribute()
        {
            _tx = Tx.NotSupported;
        }

        public S2Attribute(Tx tx)
        {
            _tx = tx;
        }

        public override IRunInvoker GetInvoker(IRunInvoker invoker)
        {
            return new S2TestCaseRunInvoker(invoker, _tx);
        }
    }

    public enum Tx
    {
        Rollback,
        Commit,
        NotSupported
    }
#endregion
#endif
}
