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

using MbUnit.Framework;
using System.Reflection;
using Seasar.Framework.Container.AutoRegister;
using Seasar.Framework.Container.Impl;

namespace Seasar.Tests.Framework.Container.AutoRegister
{
    [TestFixture]
    public class AssemblyComponentAutoRegisterTest
    {
        [Test]
        public void TestProcessAssembly()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            AssemblyComponentAutoRegister register = new AssemblyComponentAutoRegister();
            register.AddClassPattern("Seasar.Tests.Framework.Container.AutoRegister", 
                "AssemblyComponentAutoRegisterTestHoge");
            register.Container = new S2ContainerImpl();
            register.ProcessAssembly(asm);

            Assert.AreEqual(2, register.Container.ComponentDefSize, "1");
        }

        [Test]
        public void TestRegisterAll()
        {
            AssemblyComponentAutoRegister register = new AssemblyComponentAutoRegister();
            register.AddClassPattern("Seasar.Tests.Framework.Container.AutoRegister",
                "AssemblyComponentAutoRegisterTestHoge");
            register.Container = new S2ContainerImpl();
            register.RegisterAll();

            Assert.AreEqual(2, register.Container.ComponentDefSize, "1");

            register = new AssemblyComponentAutoRegister();
            register.AddClassPattern("Seasar.Tests.Framework.Container.AutoRegister",
                "AssemblyComponentAutoRegisterTestHoge");
            register.Container = new S2ContainerImpl();
            register.AssemblyName = "Seasar.Tests";
            register.RegisterAll();

            Assert.AreEqual(2, register.Container.ComponentDefSize, "2");
        }

        private class AssemblyComponentAutoRegisterTestHoge
        {
        }

        private class AssemblyComponentAutoRegisterTestHoge2
        {
        }
    }
}
