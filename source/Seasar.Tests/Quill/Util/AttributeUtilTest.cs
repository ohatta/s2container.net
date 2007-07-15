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
using Seasar.Quill.Attrs;
using Seasar.Quill.Util;
using Seasar.Quill;
using Seasar.Framework.Aop.Interceptors;
using System.Reflection;

namespace Seasar.Tests.Quill.Util
{
    [TestFixture]
	public class AttributeUtilTest
    {
        #region GetImplementationAttr�̃e�X�g

        [Test]
        public void TestGetImplementationAttr_�������w�肳��Ă��Ȃ��ꍇ()
        {
            ImplementationAttribute attr = 
                AttributeUtil.GetImplementationAttr(typeof(Hoge1));

            Assert.IsNull(attr);
        }

        [Test]
        public void TestGetImplementationAttr_�N���X�̑����Ɏ����N���X���w�肳��Ă���ꍇ()
        {
            try
            {
                ImplementationAttribute attr =
                    AttributeUtil.GetImplementationAttr(typeof(Hoge2));
                Assert.Fail();
            }
            catch (QuillApplicationException ex)
            {
                Assert.AreEqual("EQLL0001", ex.MessageCode);
                Console.WriteLine(ex.Message);
            }
        }

        [Test]
        public void TestGetImplementationAttr_�����̎����N���X�ɃC���^�[�t�F�[�X���w�肳��Ă���ꍇ()
        {
            try
            {
                ImplementationAttribute attr =
                    AttributeUtil.GetImplementationAttr(typeof(IFuga1));
                Assert.Fail();
            }
            catch (QuillApplicationException ex)
            {
                Assert.AreEqual("EQLL0002", ex.MessageCode);
                Console.WriteLine(ex.Message);
            }
        }

        [Test]
        public void TestGetImplementationAttr_�����̎����N���X�ɒ��ۃN���X���w�肳��Ă���ꍇ()
        {
            try
            {
                ImplementationAttribute attr =
                    AttributeUtil.GetImplementationAttr(typeof(IFuga2));
                Assert.Fail();
            }
            catch (QuillApplicationException ex)
            {
                Assert.AreEqual("EQLL0003", ex.MessageCode);
                Console.WriteLine(ex.Message);
            }
        }

        [Test]
        public void TestGetImplementationAttr_�����̎����N���X�ɑ�����s�\�ȃN���X���w�肳��Ă���ꍇ()
        {
            try
            {
                ImplementationAttribute attr =
                    AttributeUtil.GetImplementationAttr(typeof(IFuga3));
                Assert.Fail();
            }
            catch (QuillApplicationException ex)
            {
                Assert.AreEqual("EQLL0004", ex.MessageCode);
                Console.WriteLine(ex.Message);
            }
        }

        [Test]
        public void TestGetImplementationAttr_����ȏꍇ()
        {
            ImplementationAttribute attr =
                AttributeUtil.GetImplementationAttr(typeof(IFuga4));

            Assert.AreEqual(typeof(Fuga4), attr.ImplementationType);
        }

        #endregion

        #region GetImplementationAttr�̃e�X�g�Ŏg�p��������N���X�E�C���^�[�t�F�[�X

        private class Hoge1
        {
        }

        [Implementation(typeof(Hoge1))]
        private class Hoge2
        {
        }

        [Implementation(typeof(IFuga2))]
        private interface IFuga1
        {
        }

        [Implementation(typeof(Hoge3))]
        private interface IFuga2
        {
        }

        private abstract class Hoge3
        {
        }

        [Implementation(typeof(Hoge1))]
        private interface IFuga3
        {
        }

        [Implementation(typeof(Fuga4))]
        private interface IFuga4
        {
        }

        private class Fuga4 : IFuga4
        {
        }

        #endregion

        #region GetAspectAttrsByMember�̃e�X�g

        [Test]
        public void TestGetAspectAttrsByMember_Type�ő������ݒ肳��Ă��Ȃ��ꍇ()
        {
            AspectAttribute[] aspectAttrs = 
                AttributeUtil.GetAspectAttrsByMember(typeof(AspectHoge1));

            Assert.AreEqual(0, aspectAttrs.Length);
        }

        [Test]
        public void TestGetAspectAttrsByMember_Type��1�������ݒ肳��Ă���ꍇ()
        {
            AspectAttribute[] aspectAttrs =
                AttributeUtil.GetAspectAttrsByMember(typeof(AspectHoge2));

            Assert.AreEqual(1, aspectAttrs.Length);
            Assert.AreEqual(typeof(TraceInterceptor), aspectAttrs[0].InterceptorType);
            Assert.IsNull(aspectAttrs[0].ComponentName);
        }

        [Test]
        public void TestGetAspectAttrsByMember_Type��2�������ݒ肳��Ă���ꍇ()
        {
            AspectAttribute[] aspectAttrs =
                AttributeUtil.GetAspectAttrsByMember(typeof(AspectHoge3));

            Assert.AreEqual(2, aspectAttrs.Length,"1");
            Assert.AreEqual(typeof(TraceInterceptor), aspectAttrs[0].InterceptorType, "2");
            Assert.IsNull(aspectAttrs[0].ComponentName, "3");
            Assert.IsNull(aspectAttrs[1].InterceptorType, "4");
            Assert.AreEqual("Hogeceptor", aspectAttrs[1].ComponentName, "5");
        }

        [Test]
        public void TestGetAspectAttrsByMember_Method�ő������ݒ肳��Ă��Ȃ��ꍇ()
        {
            AspectAttribute[] aspectAttrs =AttributeUtil.GetAspectAttrsByMember(
                typeof(AspectHoge1).GetMethod("Hoge"));

            Assert.AreEqual(0, aspectAttrs.Length);
        }

        [Test]
        public void TestGetAspectAttrsByMember_Method��1�������ݒ肳��Ă���ꍇ()
        {
            AspectAttribute[] aspectAttrs = AttributeUtil.GetAspectAttrsByMember(
                typeof(AspectHoge2).GetMethod("Hoge"));

            Assert.AreEqual(1, aspectAttrs.Length);
            Assert.AreEqual(typeof(TraceInterceptor), aspectAttrs[0].InterceptorType);
            Assert.IsNull(aspectAttrs[0].ComponentName);
        }

        [Test]
        public void TestGetAspectAttrsByMember_Method��2�������ݒ肳��Ă���ꍇ()
        {
            AspectAttribute[] aspectAttrs = AttributeUtil.GetAspectAttrsByMember(
                typeof(AspectHoge3).GetMethod("Hoge"));

            Assert.AreEqual(2, aspectAttrs.Length);
            Assert.IsNull(aspectAttrs[0].InterceptorType);
            Assert.AreEqual("Hogeceptor", aspectAttrs[0].ComponentName);
            Assert.IsNull(aspectAttrs[1].ComponentName);
            Assert.AreEqual(typeof(TraceInterceptor), aspectAttrs[1].InterceptorType);
        }

        #endregion

        #region GetAspectAttrsByMember�̃e�X�g�Ŏg�p��������N���X

        private class AspectHoge1
        {
            public void Hoge() { }
        }

        [Aspect(typeof(TraceInterceptor))]
        private class AspectHoge2
        {
            [Aspect(typeof(TraceInterceptor))]
            public void Hoge() { }
        }

        [Aspect(typeof(TraceInterceptor), 1)]
        [Aspect("Hogeceptor", 2)]
        private class AspectHoge3
        {
            [Aspect(typeof(TraceInterceptor), 2)]
            [Aspect("Hogeceptor", 1)]
            public void Hoge() { }
        }

        #endregion

        #region GetAspectAttrs�̃e�X�g

        [Test]
        public void TestGetAspectAttrs()
        {
            AspectAttribute[] aspectAttrs =
                AttributeUtil.GetAspectAttrs(typeof(AspectAttrsHoge));

            Assert.AreEqual(1, aspectAttrs.Length);
            Assert.AreEqual(typeof(TraceInterceptor), aspectAttrs[0].InterceptorType);
            Assert.IsNull(aspectAttrs[0].ComponentName);
        }

        [Test]
        public void TestGetAspectAttrs_public�ł͂Ȃ��N���X�ɑ������w�肳�ꂽ�ꍇ()
        {
            try
            {
                AspectAttribute[] aspectAttrs =
                    AttributeUtil.GetAspectAttrs(typeof(AspectAttrsHoge2));
                
                Assert.Fail();
            }
            catch (QuillApplicationException ex)
            {
                Assert.AreEqual("EQLL0016", ex.MessageCode);
            }
        }

        #endregion

        #region GetAspectAttrs�̃e�X�g�Ŏg�p��������N���X

        [Aspect(typeof(TraceInterceptor))]
        public class AspectAttrsHoge
        {
        }

        [Aspect(typeof(TraceInterceptor))]
        private class AspectAttrsHoge2
        {
        }

        #endregion

        #region GetAspectAttrsByMethod�̃e�X�g

        [Test]
        public void TestGetAspectAttrsByMethod_�������ݒ肳��Ă��Ȃ��ꍇ()
        {
            AspectAttribute[] aspectAttrs = AttributeUtil.GetAspectAttrsByMethod(
                typeof(AspectAttrMethodHoge1).GetMethod("Hoge"));

            Assert.AreEqual(0, aspectAttrs.Length);
        }

        [Test]
        public void TestGetAspectAttrsByMethod_�N���X��public�ł͂Ȃ��ꍇ()
        {
            try
            {
                AspectAttribute[] aspectAttrs = AttributeUtil.GetAspectAttrsByMethod(
                    typeof(AspectAttrMethodHoge6).GetMethod("Hoge"));

                Assert.Fail();
            }
            catch (QuillApplicationException ex)
            {
                Assert.AreEqual("EQLL0016", ex.MessageCode);
            }
        }

        [Test]
        public void TestGetAspectAttrsByMethod_���\�b�h��static�̏ꍇ()
        {
            try
            {
                AspectAttribute[] aspectAttrs = AttributeUtil.GetAspectAttrsByMethod(
                    typeof(AspectAttrMethodHoge2).GetMethod("Hoge"));

                Assert.Fail();
            }
            catch (QuillApplicationException ex)
            {
                Assert.AreEqual("EQLL0005", ex.MessageCode);
            }
        }

        [Test]
        public void TestGetAspectAttrsByMethod_���\�b�h��private�̏ꍇ()
        {
            try
            {
                AspectAttribute[] aspectAttrs = AttributeUtil.GetAspectAttrsByMethod(
                    typeof(AspectAttrMethodHoge3).GetMethod(
                    "Hoge", BindingFlags.NonPublic | BindingFlags.Instance));

                Assert.Fail();
            }
            catch (QuillApplicationException ex)
            {
                Assert.AreEqual("EQLL0006", ex.MessageCode);
            }
        }


        [Test]
        public void TestGetAspectAttrsByMethod_���\�b�h��virtual�ł͂Ȃ��ꍇ()
        {
            try
            {
                AspectAttribute[] aspectAttrs = AttributeUtil.GetAspectAttrsByMethod(
                    typeof(AspectAttrMethodHoge4).GetMethod("Hoge"));

                Assert.Fail();
            }
            catch (QuillApplicationException ex)
            {
                Assert.AreEqual("EQLL0007", ex.MessageCode);
            }
        }

        [Test]
        public void TestGetAspectAttrsByMethod_����ȏꍇ()
        {
            AspectAttribute[] aspectAttrs = AttributeUtil.GetAspectAttrsByMethod(
                typeof(AspectAttrMethodHoge5).GetMethod("Hoge"));

            Assert.AreEqual(1, aspectAttrs.Length);
            Assert.IsNull(aspectAttrs[0].InterceptorType);
            Assert.AreEqual("Hogeceptor", aspectAttrs[0].ComponentName);
        }

        #endregion

        #region GetAspectAttrsByMethod�̃e�X�g�Ŏg�p��������N���X

        public class AspectAttrMethodHoge1
        {
            public void Hoge() { }
        }

        public class AspectAttrMethodHoge2
        {
            [Aspect("Hogeceptor")]
            public static void Hoge() { }
        }

        public class AspectAttrMethodHoge3
        {
            [Aspect("Hogeceptor")]
            private void Hoge() { }
        }

        public class AspectAttrMethodHoge4
        {
            [Aspect("Hogeceptor")]
            public void Hoge() { }
        }

        public class AspectAttrMethodHoge5
        {
            [Aspect("Hogeceptor")]
            public virtual void Hoge() { }
        }

        private class AspectAttrMethodHoge6
        {
            [Aspect("Hogeceptor")]
            public virtual void Hoge() { }
        }

        #endregion

        #region GetBindingAttr�̃e�X�g

        [Test]
        public void TestGetBindingAttr_static�t�B�[���h�ɑ������ݒ肳�ꂽ�ꍇ()
        {
            try
            {
                BindingAttribute attr = AttributeUtil.GetBindingAttr(
                    typeof(BindingHoge1).GetField("hoge",
                    BindingFlags.Public | BindingFlags.Static));

                Assert.Fail();
            }
            catch (QuillApplicationException ex)
            {
                Assert.AreEqual("EQLL0015", ex.MessageCode);
            }
        }

        [Test]
        public void TestGetBindingAttr_�������ݒ肳��Ă��Ȃ��ꍇ()
        {
            BindingAttribute attr = AttributeUtil.GetBindingAttr(
                typeof(BindingHoge2).GetField("hoge", 
                BindingFlags.Public | BindingFlags.Instance));

            Assert.IsNull(attr);
        }

        [Test]
        public void TestGetBindingAttr_�R���|�[�l���g�����ݒ肳��Ă��Ȃ��ꍇ()
        {
            BindingAttribute attr = AttributeUtil.GetBindingAttr(
                typeof(BindingHoge3).GetField("hoge",
                BindingFlags.Public | BindingFlags.Instance));

            Assert.IsNull(attr);
        }

        [Test]
        public void TestGetBindingAttr_����ȏꍇ()
        {
            BindingAttribute attr = AttributeUtil.GetBindingAttr(
                typeof(BindingHoge4).GetField("hoge",
                BindingFlags.Public | BindingFlags.Instance));

            Assert.AreEqual("HogeComponent", attr.ComponentName);
        }

        #endregion

        #region GetBindingAttr�̃e�X�g�Ŏg�p��������N���X

        private class BindingHoge1
        {
            public static string hoge = null;
        }

        private class BindingHoge2
        {
            public string hoge = null;
        }

        private class BindingHoge3
        {
            [Binding(null)]
            public string hoge = null;
        }

        private class BindingHoge4
        {
            [Binding("HogeComponent")]
            public string hoge = null;
        }

        #endregion
    }
}
