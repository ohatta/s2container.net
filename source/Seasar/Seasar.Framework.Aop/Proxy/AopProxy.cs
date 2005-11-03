#region Copyright
/*
 * Copyright 2005 the Seasar Foundation and the Others.
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
using System.Reflection;
using System.Collections;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Messaging;
using Seasar.Framework.Aop.Impl;
using Seasar.Framework.Util;

namespace Seasar.Framework.Aop.Proxy
{
	/// <summary>
	/// AopProxy
	/// </summary>
	/// <remarks>
	/// ���ߓI�v���N�V�ɂ����AOP���������Ă��܂��B
	/// </remarks>
	[Serializable]
	public sealed class AopProxy : RealProxy
	{
        /// <summary>
        /// ���ߓI�v���N�V���쐬����C���X�^���X
        /// </summary>
		private object target_;

        /// <summary>
        /// �K�p����Aspect
        /// </summary>
		private IAspect[] aspects_;

        /// <summary>
        /// ���ߓI�v���N�V���쐬����^
        /// </summary>
		private Type type_;

        /// <summary>
        /// ���\�b�h�Ƃ��̃N���X�̃C���X�^���X��������S2�R���e�i�Ɋւ�����
        /// </summary>
		private Hashtable parameters_;

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		/// <param name="type">Aspect�����^�[�Q�b�g�̌^��Type</param>
		/// <param name="aspects">�K������Aspect</param>
		/// <param name="parameters">�p�����[�^</param>
		/// <param name="target">Aspect�����^�[�Q�b�g</param>
		public AopProxy(Type type,IAspect[] aspects,Hashtable parameters, object target) : base(type) 
		{
			type_       = type;
			target_     = target;
			aspects_    = aspects;
			parameters_ = parameters;
		}

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		/// <param name="type">Aspect�����^�[�Q�b�g�̌^��Type</param>
		/// <param name="aspects">�K������Aspect</param>
		/// <param name="parameters">�p�����[�^</param>
		public AopProxy(Type type,IAspect[] aspects,Hashtable parameters)
			: this(type,aspects,parameters,null)
		{
		}

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		/// <param name="type">Aspect�����^�[�Q�b�g�̌^��Type</param>
		/// <param name="aspects">�K������Aspect</param>
		public AopProxy(Type type,IAspect[] aspects)
			: this(type,aspects,null)
		{
		}

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		/// <param name="type">Aspect�����^�[�Q�b�g�̌^��Type</param>
		public AopProxy(Type type)
			: this(type,null)
		{
		}

        /// <summary>
        /// ���ߓI�v���N�V��Ԃ�
        /// </summary>
        /// <returns>���ߓI�v���N�V�̃C���X�^���X</returns>
		public object Create()
		{
			return this.GetTransparentProxy();
		}

        /// <summary>
        /// ���ߓI�v���N�V��Ԃ�
        /// </summary>
        /// <param name="argTypes">���ߓI�v���N�V�̑ΏۂƂȂ�N���X�̃R���X�g���N�^�̈����̌^�̃��X�g</param>
        /// <param name="args">���ߓI�v���N�V�̑ΏۂƂȂ�N���X�̃R���X�g���N�^�̈����̃��X�g</param>
        /// <returns>���ߓI�v���N�V�̃C���X�^���X</returns>
		public object Create(Type[] argTypes,object[] args)
		{
			ConstructorInfo constructor = ClassUtil.GetConstructorInfo(type_,argTypes);
			target_ = ConstructorUtil.NewInstance(constructor,args);
			return this.GetTransparentProxy();
		}

        /// <summary>
        /// ���ߓI�v���N�V��Ԃ�
        /// </summary>
        /// <param name="argTypes">���ߓI�v���N�V�̑ΏۂƂȂ�N���X�̃R���X�g���N�^�̈����̌^�̃��X�g</param>
        /// <param name="args">���ߓI�v���N�V�̑ΏۂƂȂ�N���X�̃R���X�g���N�^�̈����̃��X�g</param>
        /// <param name="targetType">���ߓI�v���N�V�̑ΏۂƂȂ�N���X�̌^</param>
        /// <returns>���ߓI�v���N�V�̃C���X�^���X</returns>
        public object Create(Type[] argTypes,object[] args,Type targetType)
		{
			ConstructorInfo constructor = ClassUtil.GetConstructorInfo(targetType,argTypes);
			target_ = ConstructorUtil.NewInstance(constructor,args);
			return this.GetTransparentProxy();
		}

        #region RealProxy �����o

		/// <summary>
		/// AopProxy��ʂ����I�u�W�F�N�g�̃��\�b�h�����s�����Ƃ��̃��\�b�h���Ă΂�܂�
		/// </summary>
		/// <param name="msg">IMessage</param>
		/// <returns>IMessage</returns>
		/// <seealso="System.Runtime.Remoting.Proxies">System.Runtime.Remoting.Proxies</seealso>
		public override IMessage Invoke(IMessage msg) 
		{
			if(target_ == null)
			{
				if(!type_.IsInterface) target_ = Activator.CreateInstance(type_);
				if(target_==null) target_ = new object();
			}
			IMethodMessage methodMessage = msg as IMethodMessage;
			MethodBase method = methodMessage.MethodBase;

			ArrayList interceptorList = new ArrayList();

			if(aspects_ != null)
			{
				// ��`���ꂽAspect����Interceptor�̃��X�g�̍쐬
				foreach(IAspect aspect in aspects_)
				{
					IPointcut pointcut = aspect.Pointcut;
					// IPointcut���Advice(Interceptor)��}�����邩�m�F
					if(pointcut == null || pointcut.IsApplied(method)) 
					{
						// Aspect��K�p����ꍇ
						interceptorList.Add(aspect.MethodInterceptor);
					}
				}
			}

			Object ret = null;

			if(interceptorList.Count == 0)
			{
				// Interceptor��}�����Ȃ��ꍇ
				ret = method.Invoke(target_,methodMessage.Args);
			}
			else
			{
				// Interceptor��}������ꍇ
				IMethodInterceptor[] interceptors = (IMethodInterceptor[])
					interceptorList.ToArray(typeof(IMethodInterceptor));
				IMethodInvocation invocation = new MethodInvocationImpl(target_,
					method,methodMessage.Args,interceptors,parameters_);
				ret = interceptors[0].Invoke(invocation);
			}

			return new ReturnMessage(ret, null, 0, 
				methodMessage.LogicalCallContext, (IMethodCallMessage)msg);

		}

        #endregion

	}   // AopProxy
}
