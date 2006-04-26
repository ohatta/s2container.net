#region using directives

using System;
using System.Diagnostics;
using System.Text;
using System.Reflection;
using System.Collections;

using Seasar.Framework.Aop.Impl;
using Seasar.Framework.Util;
using Seasar.Framework.Aop.Interceptors;
using Seasar.Framework.Container.Util;

using Castle.DynamicProxy;

#endregion

namespace Seasar.Framework.Aop.Proxy
{
    /// <summary>
    /// Castle.DynamicProxy���g�p�����AAop�ɂ����郁�\�b�h�̈Ϗ���Aspect�Ƃ��Ď��s���邽�߂̃v���L�V�N���X
    /// </summary>
    /// <author>Kazz
    /// </author>
    /// <version>1.0 2006/04/18</version>
    ///
    [Serializable]
    public class DynamicAopProxy : IInterceptor
    {
        #region fields

        private ProxyGenerator generator;

        private object target;
        private IAspect[] aspects;
        private Type type;
        private Hashtable parameters;

        #endregion
        #region constructors

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="type">Aspect�����^�[�Q�b�g�̌^��Type</param>
        public DynamicAopProxy(Type type)
            :this(type, null)
        {
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="type">Aspect�����^�[�Q�b�g�̌^��Type</param>
        /// <param name="aspects">�K������Aspect</param>
        public DynamicAopProxy(Type type, IAspect[] aspects)
            : this(type, aspects, null)
        {
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="type">Aspect�����^�[�Q�b�g�̌^��Type</param>
        /// <param name="aspects">�K������Aspect</param>
        /// <param name="parameters">�p�����[�^</param>
        public DynamicAopProxy(Type type, IAspect[] aspects, Hashtable parameters)
            : this(type, aspects, parameters, null)
        {
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="type">Aspect�����^�[�Q�b�g�̌^��Type</param>
        /// <param name="aspects">�K������Aspect</param>
        /// <param name="parameters">�p�����[�^</param>
        /// <param name="target">Aspect�����^�[�Q�b�g</param>
        public DynamicAopProxy(Type type, IAspect[] aspects, Hashtable parameters, object target)
        {
            this.type = type;
            this.target = target;
            if (this.target == null) this.target = new object();
            this.aspects = aspects;
            this.parameters = parameters;
            this.generator = new ProxyGenerator();
        }

        #endregion
        #region public method

        public object Create()
        {
            object result = null;
            if (this.type.IsInterface)
            {
                result = this.generator.CreateProxy(this.type, this, this.target);
            }
            else
            {
                result = this.generator.CreateClassProxy(this.type, this, new object[] { null });
            }
            return result;
        }

        public object Create(Type[] argTypes, object[] args)
        {
            object result = null;
            if (this.type.IsInterface)
            {
                result = this.generator.CreateProxy(this.type, this, args);
            }
            else
            {
                result = this.generator.CreateClassProxy(this.type, this, args);
            }
            return result;
        }

        public object Create(Type[] argTypes, object[] args, Type targetType)
        {
            object result = null;
            if (this.type.IsInterface)
            {
                result = this.generator.CreateProxy(targetType, this, args);
            }
            else
            {
                result = this.generator.CreateClassProxy(targetType, this, args);
            }
            return result;
        }

        #endregion
        #region IInterceptor member

        public object Intercept(IInvocation invocation, params object[] args)
        {
            Debug.WriteLine(target.ToString() + " invocation method = " + invocation.Method.Name);
            ArrayList interceptorList = new ArrayList();
            object ret = null;

            if (aspects != null)
            {
                // ��`���ꂽAspect����Interceptor�̃��X�g�̍쐬
                foreach (IAspect aspect in aspects)
                {
                    IPointcut pointcut = aspect.Pointcut;
                    // IPointcut���Advice(Interceptor)��}�����邩�m�F
                    if (pointcut == null || pointcut.IsApplied(invocation.Method))
                    {
                        // Aspect��K�p����ꍇ
                        interceptorList.Add(aspect.MethodInterceptor);
                    }
                }
            }

            if (interceptorList.Count == 0)
            {
                // Interceptor��}�����Ȃ��ꍇ
                //ret = invocation.Method.Invoke(target, args);
                ret = invocation.Proceed(args);
            }
            else
            {
                // Interceptor��}������ꍇ
                IMethodInterceptor[] interceptors = (IMethodInterceptor[])
                    interceptorList.ToArray(typeof(IMethodInterceptor));
                IMethodInvocation mehotdInvocation =
                    new DynamicProxyMethodInvocation(target, invocation, args, interceptors, parameters);
                ret = interceptors[0].Invoke(mehotdInvocation);
            }
            return ret;
        }

        #endregion
    }
}
