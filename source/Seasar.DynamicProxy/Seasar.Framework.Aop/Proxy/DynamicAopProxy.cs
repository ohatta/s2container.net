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
    /// Castle.DynamicProxy���g�p�����AAspect���s�̂��߂̃v���L�V�N���X
    /// </summary>
    /// <author>Kazz
    /// </author>
    /// <version>1.3 2006/05/23</version>
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
        /// <param name="type">Aspect���E�v�����^</param>
        public DynamicAopProxy(Type type)
            : this(type, null)
        {
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="type">Aspect���E�v�����^</param>
        /// <param name="aspects">�E�v����Aspect�̔z��</param>
        public DynamicAopProxy(Type type, IAspect[] aspects)
            : this(type, aspects, null)
        {
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="type">Aspect���E�v�����^</param>
        /// <param name="aspects">�E�v����Aspect�̔z��</param>
        /// <param name="parameters">�p�����[�^</param>
        public DynamicAopProxy(Type type, IAspect[] aspects, Hashtable parameters)
            : this(type, aspects, parameters, null)
        {
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="type">Aspect���E�v�����^</param>
        /// <param name="aspects">�E�v����Aspect�̔z��</param>
        /// <param name="parameters">�p�����[�^</param>
        /// <param name="target">Aspect���E�v�����^�[�Q�b�g</param>
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

        /// <summary>
        /// �v���L�V�I�u�W�F�N�g�𐶐����܂�
        /// </summary>
        public object Create()
        {
            object result = null;
            if (this.type.IsInterface)
            {
                result = this.generator.CreateProxy(this.type, this, this.target);
            }
            else
            {
                result = this.generator.CreateClassProxy(this.type, this, new object[] { });
                
            }
            return result;
        }

        /// <summary>
        /// �v���L�V�I�u�W�F�N�g�𐶐����܂�    
        /// </summary>
        /// <param name="argTypes">�p�����^�^�̔z��</param>
        /// <param name="args">�������̃p�����^�̔z��</param>
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
        /// <summary>
        /// �v���L�V�I�u�W�F�N�g�𐶐����܂�
        /// </summary>
        /// <param name="argTypes">�p�����^�^�̔z��</param>
        /// <param name="args">�������̃p�����^�̔z��</param>
        /// <param name="targetType">�^�[�Q�b�g�̌^</param>
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
            ArrayList interceptorList = new ArrayList();
            object ret = null;

            if (aspects != null)
            {
                foreach (IAspect aspect in aspects)
                {
                    IPointcut pointcut = aspect.Pointcut;
                    if (pointcut == null || pointcut.IsApplied(invocation.Method))
                    {
                        interceptorList.Add(aspect.MethodInterceptor);
                    }
                }
            }
            if (interceptorList.Count == 0)
            {
                ret = invocation.Proceed(args);
            }
            else
            {
                IMethodInterceptor[] interceptors = (IMethodInterceptor[])
                    interceptorList.ToArray(typeof(IMethodInterceptor));
                IMethodInvocation mehotdInvocation =
                    new DynamicProxyMethodInvocation(
                        invocation.InvocationTarget, this.type, invocation, args, interceptors, parameters);
                ret = interceptors[0].Invoke(mehotdInvocation);
            }
            return ret;
        }

        #endregion
    }
}
