#region Copyright
/*
* Copyright 2005-2015 the Seasar Foundation and the Others.
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

#region using directives

using System;
using System.Collections;
using System.Reflection;
using Castle.DynamicProxy;

#endregion

namespace Seasar.Framework.Aop.Impl
{
    /// <summary>
    /// ������Advice(Interceptor)�ɂ��`�F�[���𒊏ۉ������C���^�t�F�[�X�̎����N���X�ł�
    /// </summary>
    /// <author>Kazz</author>
    /// <version>1.3 2006/05/23</version>
    ///
    public class DynamicProxyMethodInvocation : IS2MethodInvocation
    {
        #region fields

        private readonly IInvocation _invocation;
        private readonly IMethodInterceptor[] _interceptors;
        private int _interceptorsIndex = 1;
        private readonly Hashtable _parameters;

        #endregion

        #region constructors

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="target">�Ώۂ̃I�u�W�F�N�g���Z�b�g</param>
        /// <param name="targetType">�Ώۂ̌^���Z�b�g</param>
        /// <param name="invocation">IInvocation�C���^�t�F�[�X���Z�b�g</param>
        /// <param name="arguments">����</param>
        /// <param name="interceptors">�C���^�[�Z�v�^�̔z����Z�b�g</param>
        /// <param name="parameters">�p�����[�^</param>
        public DynamicProxyMethodInvocation(object target
                                            , Type targetType
                                            , IInvocation invocation
                                            , object[] arguments
                                            , IMethodInterceptor[] interceptors
                                            , Hashtable parameters)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (invocation == null) throw new ArgumentNullException(nameof(invocation));
            if (interceptors == null) throw new ArgumentNullException(nameof(interceptors));
            Target = target;
            TargetType = targetType;
            _invocation = invocation;
            Arguments = arguments;
            _interceptors = interceptors;
            _parameters = parameters;
        }

        #endregion

        #region IMethodInvocation member

        public MethodBase Method => _invocation.Method;

        public object Target { get; }

        public object[] Arguments { get; }

        public object Proceed()
        {
            // �֘A�t�����Ă���Interceptor�������Ăяo���Ă���
            while (_interceptorsIndex < _interceptors.Length)
            {
                return _interceptors[_interceptorsIndex++].Invoke(this);
            }

            // �Ăяo���I������猳�̃��\�b�h�̏������Ăяo��
#if NET_4_0
            _invocation.Proceed();
            return _invocation.ReturnValue;
#else
#region NET2.0
            return _invocation.Proceed(_arguments);
#endregion
#endif
        }

        #endregion

        #region IS2MethodInvocation �����o

        public Type TargetType { get; }

        public object GetParameter(string name) => _parameters[name];

        #endregion
    }
}
