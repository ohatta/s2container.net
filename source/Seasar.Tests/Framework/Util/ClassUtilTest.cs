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
using System.Diagnostics;
using System.Reflection;
using MbUnit.Framework;
using Seasar.Framework.Util;
using Seasar.Framework.Exceptions;
using System.Configuration;
using Seasar.Quill;

namespace Seasar.Tests.Framework.Util
{
    [TestFixture]
    public class ClassUtilTest
    {
        [Test]
        public void TestGetConstructorInfo()
        {
            try
            {
                ConstructorInfo constructor = ClassUtil.GetConstructorInfo(
                    typeof(A), Type.EmptyTypes);
                Assert.Fail();
            }
            catch (NoSuchConstructorRuntimeException ex)
            {
                Trace.WriteLine(ex.Message);
            }
            Type[] types = new Type[] { typeof(string) };
            ConstructorInfo constructor2 = ClassUtil.GetConstructorInfo(
                typeof(A), types);
            Assert.IsNotNull(constructor2);
        }

        [Test]
        public void TestForName()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            Assert.AreEqual(typeof(A), ClassUtil.ForName(
                "Seasar.Tests.Framework.Util.ClassUtilTest+A",
                new Assembly[] { asm }));
        }

        [Test]
        public void TestNewInstance()
        {
            Assert.IsNotNull(ClassUtil.NewInstance(typeof(B)));
        }

        [Test]
        public void TestNewInstance2()
        {
            string exeConfigPath = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath;
            string[] filePathParts = exeConfigPath.Split('\\');
            string dllConfigPath = exeConfigPath.Replace(filePathParts[filePathParts.Length - 1], string.Empty) + "\\" +
                             "Seasar.Tests.dll";
            Assert.IsNotNull(ClassUtil.NewInstance(
                "Seasar.Tests.Framework.Util.ClassUtilTest+B",
                dllConfigPath));
        }

        public class A
        {
            private string _abc;

            public A(string abc)
            {
                _abc = abc;
            }
        }

        public class B
        {
            public B()
            {
            }
        }
    }
}