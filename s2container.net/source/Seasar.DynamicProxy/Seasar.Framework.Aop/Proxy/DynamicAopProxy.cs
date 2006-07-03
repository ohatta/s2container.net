#region using directives

using System;
using System.Diagnostics;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

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
    /// <remarks>edited Kazuya Sugimoto</remarks>
    /// <version>1.7.1 2006/07/03</version>
    ///
    [Serializable]
    public class DynamicAopProxy : IInterceptor
    {
        #region fields

        private ProxyGenerator generator;
        private object target;
        private IAspect[] aspects;
        private Hashtable interceptors = new Hashtable();
        private Type type;
        private Type enhancedType;
        private Hashtable parameters;

        #endregion
        #region constructors

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="type">Aspect���K�p�����^</param>
        public DynamicAopProxy(Type type)
            : this(type, null)
        {
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="type">Aspect���K�p�����^</param>
        /// <param name="aspects">�K�p����Aspect�̔z��</param>
        public DynamicAopProxy(Type type, IAspect[] aspects)
            : this(type, aspects, null)
        {
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="type">Aspect���K�p�����^</param>
        /// <param name="aspects">�K�p����Aspect�̔z��</param>
        /// <param name="parameters">�p�����[�^</param>
        public DynamicAopProxy(Type type, IAspect[] aspects, Hashtable parameters)
            : this(type, aspects, parameters, null)
        {
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="type">Aspect���K�p�����^</param>
        /// <param name="aspects">�K�p����Aspect�̔z��</param>
        /// <param name="parameters">�p�����[�^</param>
        /// <param name="target">Aspect���K�p�����^�[�Q�b�g</param>
        public DynamicAopProxy(Type type, IAspect[] aspects, Hashtable parameters, object target)
        {
            this.type = type;
            this.target = target;
            if (this.target == null) this.target = new object();
            this.aspects = aspects;
            this.parameters = parameters;
            this.generator = new ProxyGenerator();

            if (this.type.IsInterface)
            {
                this.enhancedType = this.generator.ProxyBuilder.CreateInterfaceProxy(new Type[] { this.type }, this.target.GetType());
            }
            else
            {
                this.enhancedType = this.generator.ProxyBuilder.CreateClassProxy(this.type);
            }
            this.SetUpAspects();
        }

        #endregion

        #region properties

        /// <summary>
        /// Proxy�ɂ��g�����ꂽ�^���擾����v���p�e�B
        /// </summary>
        /// <value>Type �g�����ꂽ�^</value>
        public Type EnhancedType
        {
            get { return this.enhancedType; }
        }

        #endregion
        #region public method

        /// <summary>
        /// �v���L�V�I�u�W�F�N�g�𐶐����܂�
        /// </summary>
        public object Create()
        {
            return Create(type, target);
        }

        public object Create(Type type, object target)
        {
            ArrayList args = new ArrayList();
            args.Add(this);
            if (this.type.IsInterface)
            {
                args.AddRange(new object[] { null });
            }
            if (type.IsInterface && this.target.GetType() != typeof(object))
            {
                return this.generator.CreateProxy(type, this, target);
            }
            else
            {
                return Activator.CreateInstance(this.enhancedType, args.ToArray());
            }
        }

        /// <summary>
        /// �v���L�V�I�u�W�F�N�g�𐶐����܂�    
        /// </summary>
        /// <param name="argTypes">�p�����^�^�̔z��</param>
        /// <param name="args">�������̃p�����^�̔z��</param>
        public object Create(Type[] argTypes, object[] args)
        {
            ArrayList newArgs = new ArrayList();
            newArgs.Add(this);
            newArgs.AddRange(args);
            return Activator.CreateInstance(this.enhancedType, newArgs.ToArray());
        }
        /// <summary>
        /// �v���L�V�I�u�W�F�N�g�𐶐����܂�
        /// </summary>
        /// <param name="argTypes">�p�����^�^�̔z��</param>
        /// <param name="args">�������̃p�����^�̔z��</param>
        /// <param name="targetType">�^�[�Q�b�g�̌^</param>
        public object Create(Type[] argTypes, object[] args, Type targetType)
        {
            if (this.type.IsInterface)
            {
                return this.generator.CreateProxy(targetType, this, args);
            }
            else
            {
                return this.generator.CreateClassProxy(targetType, this, args);
            }
        }

        #endregion

        #region IInterceptor member

        public object Intercept(IInvocation invocation, params object[] args)
        {
            object ret = null;
            if ((invocation.Proxy == invocation.InvocationTarget ||
                !(invocation.Method.IsVirtual && !invocation.Method.IsFinal)) &&
                this.interceptors.ContainsKey(invocation.Method.Name))
            {
                IMethodInterceptor[] interceptors = this.interceptors[invocation.Method.Name] as IMethodInterceptor[];
                IMethodInvocation mehotdInvocation =
                   new DynamicProxyMethodInvocation(invocation.InvocationTarget, this.type, invocation, args, interceptors, parameters);
                ret = interceptors[0].Invoke(mehotdInvocation);

            }
            else
            {
                ret = invocation.Proceed(args);
            }
            return ret;
        }

        #endregion

        #region private methods

        /// <summary>
        /// �A�X�y�N�g���Z�b�g�A�b�v���܂�
        /// </summary>
        private void SetUpAspects()
        {
            if (this.aspects != null)
            {
                MethodInfo[] methodInfos = this.type.GetMethods();
                foreach (MethodInfo method in methodInfos)
                {
                    if (method.IsVirtual || this.type.IsInterface)
                    {
                        ArrayList interceptorList = new ArrayList();
                        foreach (IAspect aspect in this.aspects)
                        {
                            IPointcut pointcut = aspect.Pointcut;
                            if (pointcut == null || pointcut.IsApplied(method))
                            {
                                interceptorList.Add(aspect.MethodInterceptor);
                            }
                        }
                        if (interceptorList.Count > 0)
                        {
                            IMethodInterceptor[] interceptors = (IMethodInterceptor[])
                            interceptorList.ToArray(typeof(IMethodInterceptor));
                            this.interceptors.Add(method.Name, interceptors);
                        }
                    }
                }
            }
        }

        #endregion
    }
}
