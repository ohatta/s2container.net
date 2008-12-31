#region Copyright
/*
 * Copyright 2005-2009 the Seasar Foundation and the Others.
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
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using log4net;
using log4net.Config;
using log4net.Util;
using MbUnit.Framework;
using Seasar.Extension.Unit;
using Seasar.Framework.Aop;
using Seasar.Framework.Aop.Impl;
using Seasar.Framework.Aop.Proxy;
using Seasar.Framework.Container;
using Seasar.Framework.Log;

namespace Seasar.Tests.Framework.Unit
{
    [TestFixture]
    public class S2TestCaseBaseTest : S2TestCase
    {
        private static readonly Logger _logger = Logger.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private const String PATH = "S2FrameworkTestCaseTest_ado.dicon";
        private bool _testAaaSetUpInvoked = false;
        private string _ccc = null;
        private Hashtable _bbb = null;
        private DateTime _ddd = new DateTime();
        private IList _list1 = null;
        private Hoge _hoge = null;

        static S2TestCaseBaseTest()
        {
            FileInfo info = new FileInfo(SystemInfo.AssemblyFileName(
                Assembly.GetExecutingAssembly()) + ".config");
            XmlConfigurator.Configure(LogManager.GetRepository(), info);
        }

        [Test, S2]
        public void TestContainer()
        {
            Assert.IsNotNull(this.Container, "コンテナが取得できるはず");
        }

        [SetUp]
        public void SetUp()
        {
            _logger.Debug("SetUp");
        }

        [Test]
        public void TestStandard()
        {
            _logger.Debug("TestStandard");
        }

        [TearDown]
        public void TearDown()
        {
            _logger.Debug("TearDown");
        }

        public void SetUpAaa()
        {
            _logger.Debug("SetUpAaa");
            _testAaaSetUpInvoked = true;
        }
        [Test, S2]
        public void TestAaa()
        {
            Assert.IsTrue(_testAaaSetUpInvoked, "1");
        }

        public void TearDownAaa()
        {
            _logger.Debug("tearDownAaa");
        }

        public void SetUpBbbTx()
        {
            _logger.Debug("setUpBbbTx");
            Include(PATH);
        }

        [Test, S2]
        public void TestBbbTx()
        {
            _logger.Debug("testBbbTx");
        }

        public void SetUpBindField()
        {
            Include(PATH);
            Register(typeof(Hashtable));
            Hashtable s = this.Container.GetComponent(typeof(Hashtable)) as Hashtable;
            s.Add("1", "hoge");
        }

        [Test, S2]
        public void TestBindField()
        {
            Assert.IsNotNull(_bbb, "2");
            Assert.IsTrue(_bbb.Count == 1, "3");
        }

        public void SetUpBindField2()
        {
            Include("Seasar/Tests/Framework/Unit/bbb.dicon");
        }

        [Test, S2]
        public void TestBindField2()
        {
            Assert.IsNotNull(_bbb, "1");
            Assert.AreEqual(new DateTime(2006, 4, 1), _ddd, "2");
            Assert.AreEqual("hoge", _ccc, "3");
        }

        public void SetUpBindField3()
        {
            Include("ccc.dicon");
        }

        [Test, S2]
        public void TestBindField3()
        {
            Assert.IsNotNull(_list1, "1");
        }

        [Test, S2]
        [ExpectedException(typeof(TooManyRegistrationRuntimeException))]
        public void TestInclude()
        {
            Include("aaa.dicon");
            GetComponent(typeof(DateTime));
        }

        public void SetUpIsAssignableFrom()
        {
            Include("bbb.dicon");
        }

        [Test, S2]
        public void TestIsAssignableFrom()
        {
            Assert.AreEqual(_ccc, "hoge", "1");
        }

        public void SetUpPointcut()
        {
            Include("ddd.dicon");
        }

        [Test, S2]
        public void TestPointcut()
        {
            AopProxy aopProxy = RemotingServices.GetRealProxy(_hoge) as AopProxy;

            FieldInfo fieldInfo = aopProxy.GetType()
                .GetField("_aspects", BindingFlags.NonPublic | BindingFlags.Instance);

            IAspect[] aspects = fieldInfo.GetValue(aopProxy) as IAspect[];

            PointcutImpl pointcut = aspects[0].Pointcut as PointcutImpl;

            Assert.AreEqual(pointcut.IsApplied("GetAaa"), false, "1");
            Assert.AreEqual(pointcut.IsApplied("GetGreeting"), false, "2");
            Assert.AreEqual(pointcut.IsApplied("Greeting"), true, "3");
            Assert.AreEqual(pointcut.IsApplied("Greeting2"), true, "4");
            Assert.AreEqual(pointcut.IsApplied("GetGreetingEx"), false, "5");

            _hoge.GetAaa();
            _hoge.GetGreeting();
            _hoge.Greeting();
            _hoge.Greeting2();
            _hoge.GetGreetingEx();
        }

        [Test, S2]
        public void TestEmptyComponent()
        {
            Include("empty.dicon");
        }
    }
}
