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

using System;
using System.IO;
using System.EnterpriseServices;
using System.Reflection;
using Seasar.Framework.Container;
using Seasar.Framework.Container.Factory;
using log4net;
using log4net.Config;
using log4net.Util;
using MbUnit.Framework;

namespace Seasar.Tests.Extension.Tx.Impl
{
    [Ignore("DTC�n�͒���������Ǝv���邽�ߕۗ�")]
    [TestFixture]
    public class DTCRequiredInterceptorTest
    {
        private const string PATH = "Seasar/Tests/Extension/Tx/Impl/DTCRequiredInterceptorTest.dicon";
        private IS2Container _container = null;
        private Tx2Test _tester = null;

        static DTCRequiredInterceptorTest()
        {
            FileInfo info = new FileInfo(SystemInfo.AssemblyFileName(
                Assembly.GetExecutingAssembly()) + ".config");
            XmlConfigurator.Configure(LogManager.GetRepository(), info);
        }

        [SetUp]
        public void SetUp()
        {
            _container = S2ContainerFactory.Create(PATH);
            _container.Init();
            _tester = _container.GetComponent(typeof(Tx2Test)) as Tx2Test;
        }

        [Ignore("DTC�n�͒���������Ǝv���邽�ߕۗ�")]
        [Test]
        public void TestProceed()
        {
            Assert.IsTrue(_tester.IsInTransaction());
            Assert.IsFalse(ContextUtil.IsInTransaction);
        }

        [Ignore("DTC�n�͒���������Ǝv���邽�ߕۗ�")]
        [Test]
        public void TestProceedException()
        {
            try
            {
                _tester.throwException();
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is NotSupportedException);
                Assert.IsFalse(ContextUtil.IsInTransaction);
            }

        }
    }

    [Ignore("DTC�n�͒���������Ǝv���邽�ߕۗ�")]
    [TestFixture]
    //[Transaction(TransactionOption.RequiresNew)]
    public class DTCRequiredInterceptorTestES : ServicedComponent
    {
        private const string path = "Seasar/Tests/Extension/Tx/Impl/DTCRequiredInterceptorTest.dicon";
        static DTCRequiredInterceptorTestES()
        {
            FileInfo info = new FileInfo(SystemInfo.AssemblyFileName(
                Assembly.GetExecutingAssembly()) + ".config");
            XmlConfigurator.Configure(LogManager.GetRepository(), info);
        }

        private IS2Container container = null;
        private Tx2Test tester = null;
        [SetUp]
        public void SetUp()
        {
            container = S2ContainerFactory.Create(path);
            container.Init();
            tester = container.GetComponent(typeof(Tx2Test)) as Tx2Test;
        }

        [Ignore("DTC�n�͒���������Ǝv���邽�ߕۗ�")]
        [Test]
        [AutoComplete]
        public void TestProceed()
        {
            Guid guid = ContextUtil.TransactionId;
            Assert.IsTrue(tester.IsInTransaction());
            Assert.AreEqual(guid, tester.GetTransactionId());
        }
    }
}