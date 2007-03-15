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
using System.Text;
using MbUnit.Framework;
using Seasar.Framework.Util;
using Seasar.Framework.Container;
using Seasar.Framework.Container.Impl;
using System.Collections;

namespace Seasar.Tests.Framework.Util
{
    [TestFixture]
	public class JScriptUtilTest
	{
        [Test]
        public void TestString()
        {
            Assert.AreEqual("������", JScriptUtil.Evaluate("\"������\"", null));
        }

        [Test]
        public void TestInt32()
        {
            Assert.AreEqual(7, JScriptUtil.Evaluate("7", null));
        }

        [Test]
        public void TestFloat()
        {
            Assert.AreEqual(3.14, JScriptUtil.Evaluate("3.14", null));
        }

        [Test]
        public void TestContainer()
        {
            IS2Container container = new S2ContainerImpl();
            IComponentDef componentDef = new ComponentDefImpl(typeof(Hashtable), "Message");
            IInitMethodDef initMethodDef = new InitMethodDefImpl("Add");
            initMethodDef.AddArgDef(new ArgDefImpl("Hello"));
            initMethodDef.AddArgDef(new ArgDefImpl("����ɂ���"));
            componentDef.AddInitMethodDef(initMethodDef);
            container.Register(componentDef);

            Assert.AreEqual("����ɂ���", JScriptUtil.Evaluate(
                "container.GetComponent(\"Message\")[\"Hello\"]", container));
        }

        [Test]
        public void TestOut()
        {
            string outStr = "Out";
            Hashtable hashTable = new Hashtable();
            hashTable["out"] = outStr;

            Assert.AreEqual("Out", JScriptUtil.Evaluate("out", hashTable, null));
        }

        [Test]
        public void TestErr()
        {
            string errStr = "Error";
            Hashtable hashTable = new Hashtable();
            hashTable["err"] = errStr;

            Assert.AreEqual("Error", JScriptUtil.Evaluate("err", hashTable, null));
        }

        [Test]
        public void TestSelf()
        {
            string selfStr = "Self";
            Hashtable hashTable = new Hashtable();
            hashTable["self"] = selfStr;

            Assert.AreEqual("Self", JScriptUtil.Evaluate("self", hashTable, null));
        }

        [Test]
        public void TestAppSettings()
        {
            Assert.AreEqual("Hello", JScriptUtil.Evaluate(
                "appSettings['message']", null));
        }

        [Test]
        public void TestReturn()
        {
            string str = @"'a
b'";
            Assert.AreEqual("a\r\nb", JScriptUtil.Evaluate(str, null));
        }
	}
}