#region Copyright
/*
 * Copyright 2005-2008 the Seasar Foundation and the Others.
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
using System.Collections.Generic;
using System.Reflection;
using MbUnit.Framework;
using Seasar.Framework.Aop;
using Seasar.Framework.Container.Factory;
using Seasar.Framework.Container.Impl;
using Seasar.Quill;
using Seasar.Quill.Attrs;

namespace Seasar.Tests.Quill
{
    [TestFixture]
	public class AspectBuilderTest : AspectBuilder
    {
        public AspectBuilderTest()
            : base(null)
        { }
        
        #region GetMethodInterceptor�̃e�X�g

        [Test]
        public void TestGetMethodInterceptor_Quill_IMethodInterceptor�ɑ���ł��Ȃ��ꍇ()
        {
            this.container = new QuillContainer();

            try
            {
                this.GetMethodInterceptor(typeof(Hoge1));
                Assert.Fail();
            }
            catch (QuillApplicationException ex)
            {
                Assert.AreEqual("EQLL0012", ex.MessageCode);
            }
        }

        [Test]
        public void TestGetMethodInterceptor_Quill_����ȏꍇ()
        {
            this.container = new QuillContainer();

            IMethodInterceptor interceptor = 
                this.GetMethodInterceptor(typeof(HogeInterceptor));

            Assert.AreEqual(typeof(HogeInterceptor), interceptor.GetType());
        }

        [Test]
        public void TestGetMethodInterceptor_S2_IMethodInterceptor�ɑ���ł��Ȃ��ꍇ()
        {
            S2ContainerImpl container = new S2ContainerImpl();
            ComponentDefImpl def = new ComponentDefImpl(typeof(Hoge1), "hoge");
            container.Register(def);
            SingletonS2ContainerFactory.Container = container;

            this.container = new QuillContainer();

            try
            {
                this.GetMethodInterceptor("hoge");
                Assert.Fail();
            }
            catch (QuillApplicationException ex)
            {
                Assert.AreEqual("EQLL0012", ex.MessageCode);
            }
        }

        [Test]
        public void TestGetMethodInterceptor_S2_����ȏꍇ()
        {
            S2ContainerImpl container = new S2ContainerImpl();
            ComponentDefImpl def = new ComponentDefImpl(typeof(HogeInterceptor), "hoge");
            container.Register(def);
            SingletonS2ContainerFactory.Container = container;

            this.container = new QuillContainer();

            IMethodInterceptor interceptor = this.GetMethodInterceptor("hoge");

            Assert.AreEqual(typeof(HogeInterceptor), interceptor.GetType());
        }

        [Test]
        public void TestGetMethodInterceptor_Aspect�����Ƀp�����[�^���ݒ肳��Ă��Ȃ��ꍇ()
        {
            this.container = new QuillContainer();

            Type type = null;
            AspectAttribute attr = new AspectAttribute(type);

            try
            {
                this.GetMethodInterceptor(attr);

                Assert.Fail();
            }
            catch (QuillApplicationException ex)
            {
                Assert.AreEqual("EQLL0013", ex.MessageCode);
            }
        }

        [Test]
        public void TestGetMethodInterceptor_Aspect������InterceptorType���ݒ肳��Ă���ꍇ()
        {
            this.container = new QuillContainer();

            AspectAttribute attr = new AspectAttribute(typeof(HogeInterceptor));

            IMethodInterceptor interceptor = this.GetMethodInterceptor(attr);

            Assert.IsTrue(
                typeof(HogeInterceptor).IsAssignableFrom(interceptor.GetType()));
        }

        [Test]
        public void TestGetMethodInterceptor_Aspect������componentName���ݒ肳��Ă���ꍇ()
        {
            S2ContainerImpl container = new S2ContainerImpl();
            ComponentDefImpl def = new ComponentDefImpl(typeof(HogeInterceptor), "hoge");
            container.Register(def);
            SingletonS2ContainerFactory.Container = container;

            this.container = new QuillContainer();

            AspectAttribute attr = new AspectAttribute("hoge");

            IMethodInterceptor interceptor = this.GetMethodInterceptor(attr);

            Assert.AreEqual(container.GetComponent("hoge"), interceptor);
        }

        #endregion

        #region GetMethodInterceptor�̃e�X�g�Ŏg�p��������N���X

        public class Hoge1
        {
        }

        public class HogeInterceptor : IMethodInterceptor
        {
            public object Invoke(IMethodInvocation invocation)
            {
                return 2;
            }
        }

        #endregion

        #region AddMethodNamesForPointcut�̃e�X�g

        [Test]
        public void TestAddMethodNamesForPointcut_���߂�Interceptor��pointcut���ݒ肳���ꍇ()
        {
            this.container = new QuillContainer();

            AspectAttribute attr = new AspectAttribute(typeof(HogeInterceptor2));

            IDictionary<IMethodInterceptor, List<string>> methodNames =
                new Dictionary<IMethodInterceptor, List<string>>();

            this.AddMethodNamesForPointcut(methodNames, "Hoge", attr);

            Assert.AreEqual(1, methodNames.Count);
            Assert.AreEqual(1, methodNames[this.GetMethodInterceptor(attr)].Count);
            Assert.AreEqual("Hoge", methodNames[this.GetMethodInterceptor(attr)][0]);
        }

        [Test]
        public void TestAddMehtodNamesForPointcut_����Interceptor��pointcut���ݒ肳��Ă���ꍇ()
        {
            this.container = new QuillContainer();

            AspectAttribute attr = new AspectAttribute(typeof(HogeInterceptor2));

            IDictionary<IMethodInterceptor, List<string>> methodNames =
                new Dictionary<IMethodInterceptor, List<string>>();

            IMethodInterceptor interceptor = this.GetMethodInterceptor(attr);

            List<string> nameList = new List<string>();
            nameList.Add("Hoge");

            methodNames[interceptor] = nameList;

            this.AddMethodNamesForPointcut(methodNames, "Hoge2", attr);

            Assert.AreEqual(1, methodNames.Count);
            Assert.AreEqual(2, methodNames[interceptor].Count);
            Assert.AreEqual("Hoge", methodNames[interceptor][0]);
            Assert.AreEqual("Hoge2", methodNames[interceptor][1]);
        }

        [Test]
        public void TestAddMethodNamesForPointcut()
        {
            this.container = new QuillContainer();

            MethodInfo method = typeof(Hoge2).GetMethod("HogeHoge");

            IDictionary<IMethodInterceptor, List<string>> methodNames =
                new Dictionary<IMethodInterceptor, List<string>>();

            this.AddMethodNamesForPointcut(methodNames, method);

            AspectAttribute attr = (AspectAttribute)
                Attribute.GetCustomAttribute(method, typeof(AspectAttribute));

            Assert.AreEqual(1, methodNames.Count);
            Assert.AreEqual(1, methodNames[this.GetMethodInterceptor(attr)].Count);
            Assert.AreEqual("HogeHoge", methodNames[this.GetMethodInterceptor(attr)][0]);
        }

        #endregion

        #region AddMethodNamesForPointcut�̃e�X�g�Ŏg�p��������N���X

        public class Hoge2
        {
            [Aspect(typeof(HogeInterceptor2))]
            public virtual int HogeHoge()
            {
                return 0;
            }
        }

        public class HogeInterceptor2 : IMethodInterceptor
        {
            public object Invoke(IMethodInvocation invocation)
            {
                return 2;
            }
        }

        #endregion

        #region CreateAspect�̃e�X�g

        [Test]
        public void TestCreateAspect_�S�Ẵ��\�b�h�ŗL����Aspect���쐬����ꍇ()
        {
            this.container = new QuillContainer();
            AspectAttribute attr = new AspectAttribute(typeof(HogeInterceptor3));

            IAspect aspect = this.CreateAspect(attr);

            IMethodInterceptor interceptor = (IMethodInterceptor) container.GetComponent(
                typeof(HogeInterceptor3)).GetComponentObject(typeof(HogeInterceptor3));

            Assert.AreEqual(interceptor, aspect.MethodInterceptor);
            Assert.IsNull(aspect.Pointcut);
        }

        [Test]
        public void TestCreateAspect_���\�b�h�����w�肵�ėL����Aspect���쐬����ꍇ()
        {
            this.container = new QuillContainer();

            IMethodInterceptor interceptor = new HogeInterceptor3();

            IAspect aspect = this.CreateAspect(
                interceptor, new string[] { "Hoge1", "Hoge2" });

            Assert.AreEqual(interceptor, aspect.MethodInterceptor);
            Assert.IsTrue(aspect.Pointcut.IsApplied("Hoge1"));
            Assert.IsTrue(aspect.Pointcut.IsApplied("Hoge2"));
            Assert.IsFalse(aspect.Pointcut.IsApplied("Hoge3"));
        }

        #endregion

        #region CreateAspect�̃e�X�g�Ŏg�p��������N���X

        public class HogeInterceptor3 : IMethodInterceptor
        {
            public object Invoke(IMethodInvocation invocation)
            {
                return null;
            }
        }

        #endregion

        #region CreateAspectList�̃e�X�g

        [Test]
        public void TestCreateAspectList_Aspect���K�p����Ă��Ȃ��ꍇ()
        {
            this.container = new QuillContainer();
            MethodInfo[] methods = typeof(Hoge3).GetMethods();

            IList<IAspect> list = this.CreateAspectList(methods);

            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void TestCreateAspectList_Aspect���K�p�����ꍇ()
        {
            this.container = new QuillContainer();
            MethodInfo[] methods = typeof(Hoge4).GetMethods();

            IList<IAspect> list = this.CreateAspectList(methods);

            Assert.AreEqual(1, list.Count);

            IAspect aspect = list[0];
            IMethodInterceptor interceptor = (IMethodInterceptor)container.GetComponent(
                typeof(HogeInterceptor4)).GetComponentObject(typeof(HogeInterceptor4));

            Assert.AreEqual(interceptor, aspect.MethodInterceptor);

            IPointcut pointcut = aspect.Pointcut;

            Assert.IsTrue(pointcut.IsApplied("Hoge1"));
            Assert.IsTrue(pointcut.IsApplied("Hoge2"));
            Assert.IsFalse(pointcut.IsApplied("Hoge3"));
        }

        #endregion

        #region CreateAspectList�̃e�X�g�Ŏg�p��������N���X

        public class Hoge3
        {
            public void Hoge1()
            {
            }

            public void Hoge2()
            {
            }
        }

        public class Hoge4
        {
            [Aspect(typeof(HogeInterceptor4))]
            public virtual void Hoge1()
            {
            }

            [Aspect(typeof(HogeInterceptor4))]
            public virtual void Hoge2()
            {
            }

            public virtual void Hoge3()
            {
            }
        }

        public class HogeInterceptor4 : IMethodInterceptor
        {
            public object Invoke(IMethodInvocation invocation)
            {
                return null;
            }
        }

        #endregion

        #region CreateAspects�̃e�X�g

        [Test]
        public void TestCreateAspects_Aspect���K�p����Ȃ��ꍇ()
        {
            this.container = new QuillContainer();
            IAspect[] aspects = this.CreateAspects(typeof(Hoge5));
            
            Assert.AreEqual(0, aspects.Length);
        }

        [Test]
        public void TestCreateAspects_Aspect���K�p�����ꍇ()
        {
            this.container = new QuillContainer();
            IAspect[] aspects = this.CreateAspects(typeof(Hoge6));

            Assert.AreEqual(2, aspects.Length);
        }

        #endregion

        #region CreateAspects�̃e�X�g�Ŏg�p��������N���X

        public class Hoge5
        {
            public virtual void Fuga1()
            {
            }

            public virtual void Fuga2()
            {
            }
        }

        [Aspect(typeof(HogeInterceptor5))]
        public class Hoge6
        {
            [Aspect(typeof(HogeInterceptor5))]
            public virtual void Fuga1()
            {
            }

            [Aspect(typeof(HogeInterceptor5))]
            public virtual void Fuga2()
            {
            }

            public virtual void Fuga3()
            {
            }
        }

        public class HogeInterceptor5 : IMethodInterceptor
        {
            public object Invoke(IMethodInvocation invocation)
            {
                return null;
            }
        }

        #endregion    
    }
}
