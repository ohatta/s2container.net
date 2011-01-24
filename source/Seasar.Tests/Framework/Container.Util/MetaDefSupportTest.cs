#region Copyright
/*
 * Copyright 2005-2010 the Seasar Foundation and the Others.
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

using MbUnit.Framework;
using Seasar.Framework.Container;
using Seasar.Framework.Container.Impl;
using Seasar.Framework.Container.Util;

namespace Seasar.Tests.Framework.Container.Util
{
    [TestFixture]
    public class MetaDefSupportTest
    {
        [Test]
        public void TestGetMetaDefs()
        {
            MetaDefSupport support = new MetaDefSupport();
            support.AddMetaDef(new MetaDefImpl("aaa"));
            support.AddMetaDef(new MetaDefImpl("bbb"));
            support.AddMetaDef(new MetaDefImpl("aaa"));
            IMetaDef[] metaDefs = support.GetMetaDefs("aaa");
            Assert.AreEqual(2, metaDefs.Length);
        }
    }
}