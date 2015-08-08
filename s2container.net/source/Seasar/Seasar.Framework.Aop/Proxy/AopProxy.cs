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

using System;
using System.Collections;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
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
        private object _target;

        /// <summary>
        /// �K�p����Aspect
        /// </summary>
        private readonly IAspect[] _aspects;

        /// <summary>
        /// ���\�b�h�Ƃ��̃N���X�̃C���X�^���X��������S2�R���e�i�Ɋւ�����
        /// </summary>
        private readonly Hashtable _parameters;

        public Type TargetType { get; }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="type">Aspect�����^�[�Q�b�g�̌^��Type</param>
        /// <param name="aspects">�K������Aspect</param>
        /// <param name="parameters">�p�����[�^</param>
        /// <param name="target">Aspect�����^�[�Q�b�g</param>
        public AopProxy(Type type, IAspect[] aspects, Hashtable parameters, object target)
            : base(type)
        {
            TargetType = type;
            _target = target;
            _aspects = aspects;
            _parameters = parameters;
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="type">Aspect�����^�[�Q�b�g�̌^��Type</param>
        /// <param name="aspects">�K������Aspect</param>
        /// <param name="parameters">�p�����[�^</param>
        public AopProxy(Type type, IAspect[] aspects, Hashtable parameters)
            : this(type, aspects, parameters, null)
        {
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="type">Aspect�����^�[�Q�b�g�̌^��Type</param>
        /// <param name="aspects">�K������Aspect</param>
        public AopProxy(Type type, IAspect[] aspects)
            : this(type, aspects, null)
        {
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="type">Aspect�����^�[�Q�b�g�̌^��Type</param>
        public AopProxy(Type type)
            : this(type, null)
        {
        }

        /// <summary>
        /// ���ߓI�v���N�V��Ԃ�
        /// </summary>
        /// <returns>���ߓI�v���N�V�̃C���X�^���X</returns>
        public object Create()
        {
            return GetTransparentProxy();
        }

        /// <summary>
        /// ���ߓI�v���N�V��Ԃ�
        /// </summary>
        /// <param name="argTypes">���ߓI�v���N�V�̑ΏۂƂȂ�N���X�̃R���X�g���N�^�̈����̌^�̃��X�g</param>
        /// <param name="args">���ߓI�v���N�V�̑ΏۂƂȂ�N���X�̃R���X�g���N�^�̈����̃��X�g</param>
        /// <returns>���ߓI�v���N�V�̃C���X�^���X</returns>
        public object Create(Type[] argTypes, object[] args)
        {
            var constructor = ClassUtil.GetConstructorInfo(TargetType, argTypes);
            _target = ConstructorUtil.NewInstance(constructor, args);
            return GetTransparentProxy();
        }

        /// <summary>
        /// ���ߓI�v���N�V��Ԃ�
        /// </summary>
        /// <param name="argTypes">���ߓI�v���N�V�̑ΏۂƂȂ�N���X�̃R���X�g���N�^�̈����̌^�̃��X�g</param>
        /// <param name="args">���ߓI�v���N�V�̑ΏۂƂȂ�N���X�̃R���X�g���N�^�̈����̃��X�g</param>
        /// <param name="targetType">���ߓI�v���N�V�̑ΏۂƂȂ�N���X�̌^</param>
        /// <returns>���ߓI�v���N�V�̃C���X�^���X</returns>
        public object Create(Type[] argTypes, object[] args, Type targetType)
        {
            var constructor = ClassUtil.GetConstructorInfo(targetType, argTypes);
            _target = ConstructorUtil.NewInstance(constructor, args);
            return GetTransparentProxy();
        }

        #region RealProxy �����o

        /// <summary>
        /// AopProxy��ʂ����I�u�W�F�N�g�̃��\�b�h�����s�����Ƃ��̃��\�b�h���Ă΂�܂�
        /// </summary>
        /// <param name="msg">IMessage</param>
        /// <returns>IMessage</returns>
        /// <seealso cref="System.Runtime.Remoting.Proxies">System.Runtime.Remoting.Proxies</seealso>
        public override IMessage Invoke(IMessage msg)
        {
            if (_target == null)
            {
//                if (!_type.IsInterface) _target = Activator.CreateInstance(_type);
                if (!TargetType.IsInterface) _target = ClassUtil.NewInstance(TargetType);
                if (_target == null) _target = new object();
            }

            var methodMessage = msg as IMethodMessage;
            if (methodMessage != null)
            {
                var method = methodMessage.MethodBase;

                var interceptorList = new ArrayList();

                if (_aspects != null)
                {
                    // ��`���ꂽAspect����Interceptor�̃��X�g�̍쐬
                    foreach (var aspect in _aspects)
                    {
                        var pointcut = aspect.Pointcut;
                        // IPointcut���Advice(Interceptor)��}�����邩�m�F
                        if (pointcut == null || pointcut.IsApplied(method))
                        {
                            // Aspect��K�p����ꍇ
                            interceptorList.Add(aspect.MethodInterceptor);
                        }
                    }
                }

                object ret;

                object[] methodArgs;

                if (interceptorList.Count == 0)
                {
                    methodArgs = methodMessage.Args;

                    try
                    {
                        //Interceptor��}�����Ȃ��ꍇ
                        ret = MethodUtil.Invoke((MethodInfo) method, _target, methodArgs);
//                    ret = method.Invoke(_target, methodArgs);
                    }
                    catch (TargetInvocationException ex)
                    {
                        // InnerException��StackTrace��ۑ�����
                        ExceptionUtil.SaveStackTraceToRemoteStackTraceString(ex.InnerException);

                        // InnerException��throw����
                        throw ex.InnerException;
                    }
                }
                else
                {
                    // Interceptor��}������ꍇ
                    var interceptors = (IMethodInterceptor[])
                        interceptorList.ToArray(typeof (IMethodInterceptor));

                    IMethodInvocation invocation = new MethodInvocationImpl(_target,
                        method, methodMessage.Args, interceptors, _parameters);

                    ret = interceptors[0].Invoke(invocation);

                    methodArgs = invocation.Arguments;
                }

                IMethodReturnMessage mrm = new ReturnMessage(ret, methodArgs, methodArgs.Length,
                    methodMessage.LogicalCallContext, (IMethodCallMessage) msg);

                return mrm;
            }
            else
            {
                return null;
            }
        }

        #endregion

    }
}
