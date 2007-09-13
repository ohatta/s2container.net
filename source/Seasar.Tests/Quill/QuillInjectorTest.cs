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
using MbUnit.Framework;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using System.Reflection;
using Seasar.Framework.Aop.Interceptors;
using Seasar.Framework.Container;
using Seasar.Framework.Container.Impl;
using Seasar.Framework.Container.Factory;

namespace Seasar.Tests.Quill
{
    [TestFixture]
	public class QuillInjectorTest : QuillInjector
    {
        public QuillInjectorTest()
            : base()
        {
        }

        public override void Dispose()
        {
            // MbUnit��Dispose���Ăяo���̂�Dispose��override���Ă���
        }

        #region GetInstance�̃e�X�g

        [Test]
        public void TestGetInstance()
        {
            container = new QuillContainer();
            QuillInjector injector1 = QuillInjector.GetInstance();
            QuillInjector injector2 = QuillInjector.GetInstance();

            Assert.AreSame(injector1, injector2);
        }

        [Test]
        public void TestGetInstance_Destroy�ς݂̏ꍇ()
        {
            QuillInjector injector1 = QuillInjector.GetInstance();
            QuillInjector.GetInstance().Destroy();
            QuillInjector injector2 = QuillInjector.GetInstance();

            Assert.IsNotNull(injector2);
            Assert.AreNotSame(injector1, injector2);
        }

        #endregion

        #region InjectField�̃e�X�g

        [Test]
        public void TestInjectField_Quill_�^���N���X�̏ꍇ()
        {
            container = new QuillContainer();
            ImplementationAttribute attr = new ImplementationAttribute();
            FieldInfo field = typeof(Target1).GetField("Hoge1");
            Target1 target = new Target1();

            this.InjectField(target, field, attr);

            Assert.IsNotNull(target.Hoge1);
        }

        [Test]
        public void TestInjectField_Quill_�^���C���^�[�t�F�[�X�Ŏ����N���X���w�肳��Ă��Ȃ��ꍇ()
        {
            container = new QuillContainer();
            ImplementationAttribute attr = new ImplementationAttribute();
            FieldInfo field = typeof(Target2).GetField("Hoge2");
            Target2 target = new Target2();

            this.InjectField(target, field, attr);

            Assert.IsNotNull(target.Hoge2);
        }

        [Test]
        public void TestInjectField_Quill_�^���C���^�[�t�F�[�X�Ŏ����N���X���w�肳��Ă���ꍇ()
        {
            container = new QuillContainer();
            ImplementationAttribute attr = new ImplementationAttribute(typeof(Hoge3));
            FieldInfo field = typeof(Target3).GetField("Hoge3");
            Target3 target = new Target3();

            this.InjectField(target, field, attr);

            Assert.IsNotNull(target.Hoge3);
        }

        [Test]
        public void TestInjectField_S2_����s�\�ȏꍇ()
        {
            container = new QuillContainer();
            IS2Container s2Container = new S2ContainerImpl();
            IComponentDef def = new ComponentDefImpl(typeof(Hoge1), "hoge3");
            s2Container.Register(def);
            SingletonS2ContainerFactory.Container = s2Container;

            BindingAttribute attr = new BindingAttribute("hoge3");
            Target4 target = new Target4();
            FieldInfo field = typeof(Target4).GetField("Hoge3");

            try
            {
                this.InjectField(target, field, attr);
                Assert.Fail();
            }
            catch (QuillApplicationException ex)
            {
                Assert.AreEqual("EQLL0014", ex.MessageCode);
            }
        }

        [Test]
        public void TestInjectField_S2_����ȏꍇ()
        {
            container = new QuillContainer();
            IS2Container s2Container = new S2ContainerImpl();
            IComponentDef def = new ComponentDefImpl(typeof(Hoge3), "hoge3");
            s2Container.Register(def);
            SingletonS2ContainerFactory.Container = s2Container;

            BindingAttribute attr = new BindingAttribute("hoge3");
            Target5 target = new Target5();
            FieldInfo field = typeof(Target5).GetField("Hoge3");

            this.InjectField(target, field, attr);

            Assert.AreSame(s2Container.GetComponent("hoge3"), target.Hoge3);
        }

        [Test]
        public void TestInjectField_Binding�������ݒ肳��Ă���ꍇ()
        {
            container = new QuillContainer();
            IS2Container s2Container = new S2ContainerImpl();
            IComponentDef def = new ComponentDefImpl(typeof(Hoge3), "hoge3");
            s2Container.Register(def);
            SingletonS2ContainerFactory.Container = s2Container;

            Target5 target = new Target5();
            FieldInfo field = typeof(Target5).GetField("Hoge3");

            this.InjectField(target, field);

            Assert.AreSame(s2Container.GetComponent("hoge3"), target.Hoge3);
        }

        [Test]
        public void TestInjectField_Implementation�������ݒ肳��Ă���ꍇ()
        {
            container = new QuillContainer();
            FieldInfo field = typeof(Target3).GetField("Hoge3");
            Target3 target = new Target3();

            this.InjectField(target, field);

            Assert.IsNotNull(target.Hoge3);
        }

        #endregion

        #region InjectFiled�̃e�X�g�Ŏg�p��������N���X

        public class Target1
        {
            public Hoge1 Hoge1;
        }

        [Implementation()]
        public class Hoge1
        {
        }

        public class Target2
        {
            public IHoge2 Hoge2;
        }

        [Implementation()]
        [Aspect(typeof(TraceInterceptor))]
        public interface IHoge2
        {
        }

        public class Target3
        {
            public IHoge3 Hoge3;
        }

        [Implementation(typeof(Hoge3))]
        public interface IHoge3
        {
        }

        public class Hoge3 : IHoge3
        {
        }

        public class Target4
        {
            [Binding("hoge3")]
            public IHoge3 Hoge3;
        }

        public class Target5
        {
            [Binding("hoge3")]
            public IHoge3 Hoge3;
        }


        #endregion

        #region Inject�̃e�X�g

        [Test]
        public void TestInject()
        {
            container = new QuillContainer();
            Target10 target = new Target10();
            Inject(target);

            Assert.IsNotNull(target.Hoge10, "1");
            Assert.IsNotNull(target.Hoge11, "2");
            Assert.IsNotNull(target.Hoge12, "3");
        }

        [Test]
        public void TestInject_Destroy�ς݂̏ꍇ()
        {
            container = new QuillContainer();
            Target10 target = new Target10();
            Destroy();
            try
            {
                Inject(target);
                Assert.Fail();
            }
            catch (QuillApplicationException ex)
            {
                Assert.AreEqual("EQLL0018", ex.MessageCode);
            }
        }

        #endregion

        #region Inject�̃e�X�g�Ŏg�p��������N���X

        public class Target10
        {
            public IHoge10 Hoge10;
            protected Hoge11 hoge11;
            private Hoge12 hoge12 = null;

            public Hoge11 Hoge11
            {
                get { return hoge11; }
            }

            public Hoge12 Hoge12
            {
                get { return hoge12; }
            }
        }

        [Implementation(typeof(Hoge10))]
        public interface IHoge10
        {
        }

        public class Hoge10 : IHoge10
        {
        }

        [Implementation()]
        public class Hoge11
        {
        }

        [Implementation()]
        public class Hoge12
        {
        }

        #endregion

        #region Dispose�̃e�X�g

        [Test]
        public void TestDispose()
        {
            container = new QuillContainer();
            DisposableTarget target = new DisposableTarget();
            Inject(target);

            Assert.IsFalse(target.DisposableClass.Disposed);

            base.Dispose();

            Assert.IsTrue(target.DisposableClass.Disposed);
        }

        #endregion

        #region Dispose�̃e�X�g�Ŏg�p��������N���X

        public class DisposableTarget
        {
            public DisposableClass DisposableClass;
        }

        [Implementation]
        public class DisposableClass : IDisposable
        {
            public bool Disposed = false;

            #region IDisposable �����o

            public void Dispose()
            {
                Disposed = true;
            }

            #endregion
        }

        #endregion 

        #region Destroy�̃e�X�g

        [Test]
        public void TestDestroy()
        {
            container = new QuillContainer();
            Destroy();

            try
            {
                Inject(new DestroyHoge());
                Assert.Fail();
            }
            catch(QuillApplicationException ex)
            {
                Assert.AreEqual("EQLL0018", ex.MessageCode);
            }

            Destroy();
        }

        #endregion

        #region Destroy�̃e�X�g�Ŏg�p��������N���X

        private class DestroyHoge
        {
        }

        #endregion
    }
}
