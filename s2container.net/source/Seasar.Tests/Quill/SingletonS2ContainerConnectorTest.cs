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
using Seasar.Framework.Container.Impl;
using Seasar.Framework.Container.Factory;

namespace Seasar.Tests.Quill
{
    [TestFixture]
	public class SingletonS2ContainerConnectorTest
    {
        #region GetComponent�̃e�X�g

        [Test]
        public void TestGetComponent_S2Container���쐬����Ă��Ȃ��ꍇ()
        {
            SingletonS2ContainerFactory.Container = null;

            try
            {
                SingletonS2ContainerConnector.GetComponent("hoge", typeof(string));
                Assert.Fail();
            }
            catch (QuillApplicationException ex)
            {
                Assert.AreEqual("EQLL0009", ex.MessageCode);
            }
        }

        [Test]
        public void TestGetComponent_�R���|�[�l���g���o�^����Ă��Ȃ��ꍇ()
        {
            S2ContainerImpl container = new S2ContainerImpl();
            SingletonS2ContainerFactory.Container = container;

            try
            {
                SingletonS2ContainerConnector.GetComponent("hoge", typeof(string));
                Assert.Fail();
            }
            catch (QuillApplicationException ex)
            {
                Assert.AreEqual("EQLL0010", ex.MessageCode);
            }
            finally
            {
                SingletonS2ContainerFactory.Container = null;
            }
        }

        [Test]
        public void TestGetComponent_�R���|�[�l���g�̎擾��S2Container�����O���X���[���ꂽ�ꍇ()
        {
            S2ContainerImpl container = new S2ContainerImpl();
            SingletonS2ContainerFactory.Container = container;
            ComponentDefImpl def = new ComponentDefImpl(typeof(Hoge), "hoge");
            PropertyDefImpl propertyDef = new PropertyDefImpl("HogeHoge", "test");
            def.AddPropertyDef(propertyDef);
            container.Register(def);

            try
            {
                SingletonS2ContainerConnector.GetComponent("hoge", typeof(string));
                Assert.Fail();
            }
            catch (QuillApplicationException ex)
            {
                Assert.AreEqual("EQLL0011", ex.MessageCode);
            }
            finally
            {
                SingletonS2ContainerFactory.Container = null;
            }
        }

        [Test]
        public void TestGetComponent_�󂯑��̌^���w�肵�Ȃ��ŃR���|�[�l���g���󂯎��ꍇ1()
        {
            S2ContainerImpl container = new S2ContainerImpl();
            SingletonS2ContainerFactory.Container = container;
            ComponentDefImpl def = new ComponentDefImpl(typeof(Hoge), "hoge");
            container.Register(def);

            Hoge hoge = (Hoge) SingletonS2ContainerConnector.GetComponent("hoge", null);
            Assert.IsNotNull(hoge);
            SingletonS2ContainerFactory.Container = null;
        }

        [Test]
        public void TestGetComponent_�󂯑��̌^���w�肵�Ȃ��ŃR���|�[�l���g���󂯎��ꍇ2()
        {
            S2ContainerImpl container = new S2ContainerImpl();
            SingletonS2ContainerFactory.Container = container;
            ComponentDefImpl def = new ComponentDefImpl(typeof(Hoge), "hoge");
            container.Register(def);

            Hoge hoge = (Hoge)SingletonS2ContainerConnector.GetComponent("hoge");
            Assert.IsNotNull(hoge);
            SingletonS2ContainerFactory.Container = null;
        }

        [Test]
        public void TestGetComponent_�󂯑��̌^���w�肵�ăR���|�[�l���g���󂯎��ꍇ()
        {
            S2ContainerImpl container = new S2ContainerImpl();
            SingletonS2ContainerFactory.Container = container;
            ComponentDefImpl def = new ComponentDefImpl(typeof(Hoge), "hoge");
            container.Register(def);

            IHoge hoge = (IHoge)SingletonS2ContainerConnector.GetComponent(
                "hoge", typeof(IHoge));
            Assert.IsNotNull(hoge);
            SingletonS2ContainerFactory.Container = null;
        }

        #endregion

        #region GetComponent�̃e�X�g�Ŏg�p��������N���X

        private class Hoge : IHoge
        {
            public int HogeHoge
            {
                get { return 0; }
            }
        }

        private interface IHoge
        {
        }

        #endregion
    }
}
