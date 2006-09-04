#region Copyright
/*
 * Copyright 2005-2006 the Seasar Foundation and the Others.
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
using System.Collections;
using System.Reflection;
using Seasar.Framework.Util;

namespace Seasar.Framework.Aop.Impl
{
	/// <summary>
	/// IS2MethodInvocation�C���^�[�t�F�C�X�̎���
	/// </summary>
	public class MethodInvocationImpl : IS2MethodInvocation
	{
		/// <summary>
		/// �Ăяo����郁�\�b�h��������C���X�^���X
		/// </summary>
		private object target_;

		/// <summary>
		/// �Ăяo����郁�\�b�h
		/// </summary>
		private MethodBase method_;

		/// <summary>
		/// ���\�b�h��Intercept����Interceptor�̔z��
		/// </summary>
		private IMethodInterceptor[] interceptors_;

		/// <summary>
		/// ��������Ă���Interceptor�̍ċA���x��
		/// </summary>
		private int interceptorsIndex_ = 1;

		/// <summary>
		/// ���\�b�h�̈���
		/// </summary>
		private object[] arguments_;

		/// <summary>
		/// ���\�b�h�Ƃ��̃N���X�̃C���X�^���X��������S2�R���e�i�Ɋւ�����
		/// </summary>
		private Hashtable parameters_;

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		/// <param name="target">Intercept�����I�u�W�F�N�g</param>
		/// <param name="method">Intercept����郁�\�b�h��MethodBase</param>
		/// <param name="interceptors">���\�b�h��Intercept����Interceptor</param>
		/// <param name="parameters">Intercept����郁�\�b�h�Ƃ��̃N���X�̃C���X�^���X��������S2�R���e�i�Ɋւ�����</param>
		public MethodInvocationImpl(object target,MethodBase method,
			object[] arguments,IMethodInterceptor[] interceptors,Hashtable parameters)
		{
			if(target==null) throw new NullReferenceException("target");
			if(method==null) throw new NullReferenceException("method");
			if(interceptors==null) throw new NullReferenceException("interceptors");
			target_       = target;
			method_       = method;
			arguments_    = arguments;
			interceptors_ = interceptors;
			parameters_   = parameters;
		}

		#region IMethodInvocation �����o

		/// <summary>
		/// Intercept����郁�\�b�h��Method
		/// </summary>
		public MethodBase Method
		{
			get
			{
				return method_;
			}
		}

		/// <summary>
		/// Intercept�����I�u�W�F�N�g
		/// </summary>
		public Object Target
		{
			get
			{
				return target_;
			}
		}

		/// <summary>
		/// Intercept����郁�\�b�h�̈���
		/// </summary>
		public Object[] Arguments
		{
			get
			{
				return arguments_;
			}
		}

		/// <summary>
		/// ���\�b�h�̌Ăяo��
		/// </summary>
		/// <remarks>
		/// ���Ƀ`�F�[������Ă���Interceptor������΁AInterceptor���Ăяo���܂��i�ċA�I�ɌĂяo�����j�B
		/// ���Ƀ`�F�[������Ă���Interceptor��������΁AIntercept����Ă��郁�\�b�h�����s���܂��B
		/// <remarks>
		/// <returns>Intercept���ꂽ���\�b�h�̖߂�l</returns>
		public Object Proceed()
		{
			while(interceptorsIndex_ < interceptors_.Length)
			{
				// ����Interceptor������΁AInterceptor���Ăяo��
				return interceptors_[interceptorsIndex_++].Invoke(this);
			}

            try
            {
                // Intercept���ꂽ���\�b�h�����s����
                return method_.Invoke(target_, arguments_);
            }
            catch (TargetInvocationException ex)
            {
                // InnerException��StackTrace��ۑ�����
                ExceptionUtil.SaveStackTraceToRemoteStackTraceString(ex.InnerException);

                // InnerException��throw����
                throw ex.InnerException;
            }
		}

		#endregion

		#region IS2MethodInvocation �����o

		/// <summary>
		/// ���\�b�h��������N���X�̌^���
		/// </summary>
		public Type TargetType
		{
			get { return target_.GetType(); }
		}

		/// <summary>
		/// ���\�b�h�Ƃ��̃N���X�̃C���X�^���X��������S2�R���e�i�Ɋւ�����
		/// </summary>
		public object GetParameter(string name)
		{
			return parameters_[name];
		}

		#endregion
	}
}
