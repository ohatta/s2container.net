#region Copyright
/*
 * Copyright 2005-2012 the Seasar Foundation and the Others.
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
        private readonly object _target;

        /// <summary>
        /// �Ăяo����郁�\�b�h
        /// </summary>
        private readonly MethodBase _method;

        /// <summary>
        /// ���\�b�h��Intercept����Interceptor�̔z��
        /// </summary>
        private readonly IMethodInterceptor[] _interceptors;

        /// <summary>
        /// ��������Ă���Interceptor�̍ċA���x��
        /// </summary>
        private int _interceptorsIndex = 1;

        /// <summary>
        /// ���\�b�h�̈���
        /// </summary>
        private readonly object[] _arguments;

        /// <summary>
        /// ���\�b�h�Ƃ��̃N���X�̃C���X�^���X��������S2�R���e�i�Ɋւ�����
        /// </summary>
        private readonly Hashtable _parameters;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="target">Intercept�����I�u�W�F�N�g</param>
        /// <param name="method">Intercept����郁�\�b�h��MethodBase</param>
        /// <param name="arguments">����</param>
        /// <param name="interceptors">���\�b�h��Intercept����Interceptor</param>
        /// <param name="parameters">Intercept����郁�\�b�h�Ƃ��̃N���X�̃C���X�^���X��������S2�R���e�i�Ɋւ�����</param>
        public MethodInvocationImpl(object target, MethodBase method,
            object[] arguments, IMethodInterceptor[] interceptors, Hashtable parameters)
        {
            if (target == null)
            {
                throw new NullReferenceException("target");
            }
            if (method == null)
            {
                throw new NullReferenceException("method");
            }
            if (interceptors == null)
            {
                throw new NullReferenceException("interceptors");
            }
            _target = target;
            _method = method;
            _arguments = arguments;
            _interceptors = interceptors;
            _parameters = parameters;
        }

        #region IMethodInvocation �����o

        /// <summary>
        /// Intercept����郁�\�b�h��Method
        /// </summary>
        public MethodBase Method
        {
            get { return _method; }
        }

        /// <summary>
        /// Intercept�����I�u�W�F�N�g
        /// </summary>
        public object Target
        {
            get { return _target; }
        }

        /// <summary>
        /// Intercept����郁�\�b�h�̈���
        /// </summary>
        public object[] Arguments
        {
            get { return _arguments; }
        }

        /// <summary>
        /// ���\�b�h�̌Ăяo��
        /// </summary>
        /// <remarks>
        /// ���Ƀ`�F�[������Ă���Interceptor������΁AInterceptor���Ăяo���܂��i�ċA�I�ɌĂяo�����j�B
        /// ���Ƀ`�F�[������Ă���Interceptor��������΁AIntercept����Ă��郁�\�b�h�����s���܂��B
        /// <remarks>
        /// <returns>Intercept���ꂽ���\�b�h�̖߂�l</returns>
        public object Proceed()
        {
            while (_interceptorsIndex < _interceptors.Length)
            {
                // ����Interceptor������΁AInterceptor���Ăяo��
                return _interceptors[_interceptorsIndex++].Invoke(this);
            }

            try
            {
                // Intercept���ꂽ���\�b�h�����s����
                return _method.Invoke(_target, _arguments);
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
            get { return _target.GetType(); }
        }

        /// <summary>
        /// ���\�b�h�Ƃ��̃N���X�̃C���X�^���X��������S2�R���e�i�Ɋւ�����
        /// </summary>
        public object GetParameter(string name)
        {
            return _parameters[name];
        }

        #endregion
    }
}
