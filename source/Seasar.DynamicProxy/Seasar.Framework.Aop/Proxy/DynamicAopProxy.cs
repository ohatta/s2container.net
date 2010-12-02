#region Copyright
/*
* Copyright 2005-2010 the Seasar Foundation and the Others.
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
using System.Collections.Generic;
using System.Reflection;
using Castle.DynamicProxy;
using Seasar.Framework.Aop.Impl;

#endregion

namespace Seasar.Framework.Aop.Proxy
{
    /// <summary>
    /// Castle.DynamicProxy���g�p�����AAspect���s�̂��߂̃v���L�V�N���X
    /// </summary>
    /// <author>Kazz
    /// </author>
    /// <remarks>edited Kazuya Sugimoto</remarks>
    /// <version>1.7.2 2006/07/24</version>
    ///
    [Serializable]
    public class DynamicAopProxy
    {
        #region fields

        private readonly ProxyGenerator _generator;
        private readonly Type _componentType;

        private readonly IInterceptor[] _interceptors;

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
        /// <param name="componentType">Aspect���K�p�����^</param>
        /// <param name="aspects">�K�p����Aspect�̔z��</param>
        /// <param name="parameters">�p�����[�^</param>
        /// <param name="target">Aspect���K�p�����^�[�Q�b�g</param>
        public DynamicAopProxy(Type componentType, IAspect[] aspects, Hashtable parameters, object target)
        {
            _componentType = componentType;
            _generator = new ProxyGenerator();

            var interceptorMap = CreateMethodInterceptors(componentType, aspects);
            _interceptors = new IInterceptor[] { new InterceptorAdapter(
                interceptorMap, componentType, parameters) };
        }

        #endregion

        #region public method

        /// <summary>
        /// �v���L�V�I�u�W�F�N�g�𐶐����܂�
        /// </summary>
        public object Create()
        {
            if (_componentType.IsInterface)
            {
                return _generator.CreateInterfaceProxyWithoutTarget(_componentType, _interceptors);
            }
            else
            {
                return _generator.CreateClassProxy(_componentType, _interceptors);
            }
        }

        public object Create(Type receiptType, object target)
        {
            if (receiptType.IsInterface && target.GetType() == typeof(object))
            {
                if (target.GetType() == typeof(object))
                {
                    return _generator.CreateInterfaceProxyWithoutTarget(receiptType, _interceptors);
                }
                else
                {
                    return _generator.CreateInterfaceProxyWithTarget(receiptType, target, _interceptors);
                }
            }
            else
            {
                return _generator.CreateClassProxy(_componentType, _interceptors);
            }
        }

        public object Create(Type[] types, object[] args)
        {            
            var interceptorTypes = new List<Type>();
            foreach(var interceptor in _interceptors)
            {
                interceptorTypes.Add(interceptor.GetType());
            }

            Type enhancedType;
            if (_componentType.IsInterface)
            {
                enhancedType = _generator.ProxyBuilder.CreateInterfaceProxyTypeWithoutTarget(_componentType, interceptorTypes.ToArray(), ProxyGenerationOptions.Default);
            }
            else
            {
                enhancedType = _generator.ProxyBuilder.CreateClassProxyType(_componentType, interceptorTypes.ToArray(), ProxyGenerationOptions.Default);
            }

            var newArgs = new ArrayList();
            newArgs.Add(_interceptors);
            newArgs.AddRange(args);
            return Activator.CreateInstance(enhancedType, newArgs.ToArray());
        }

        public T Create<T>(object target)
        {
            return (T)Create(typeof(T), target);
        }

        #endregion

        #region private methods

        /// <summary>
        /// �A�X�y�N�g���Z�b�g�A�b�v���܂�
        /// </summary>
        /// <param name="type"></param>
        /// <param name="aspects"></param>
        private IDictionary<MethodInfo, IMethodInterceptor[]> CreateMethodInterceptors(
            Type type, IAspect[] aspects)
        {
            var interceptorMap = new Dictionary<MethodInfo, IMethodInterceptor[]>();
            if (aspects != null)
            {
                var methodInfos = type.GetMethods();
                foreach (var method in methodInfos)
                {
                    // DynamicProxy��K�p�����邽�߂ɂ̓C���^�[�t�F�[�X��virtual���\�b�h
                    // �łȂ���΂Ȃ�Ȃ�
                    if (method.IsVirtual || type.IsInterface)
                    {
                        var interceptorList = new List<IMethodInterceptor>();
                        foreach (var aspect in aspects)
                        {
                            var pointcut = aspect.Pointcut;
                            if (pointcut == null || pointcut.IsApplied(method))
                            {
                                interceptorList.Add(aspect.MethodInterceptor);
                            }
                        }
                        if (interceptorList.Count > 0)
                        {
                            IMethodInterceptor[] interceptors = interceptorList.ToArray();
                            interceptorMap[method] = interceptors;
                        }
                    }
                }
            }
            
            return interceptorMap;
        }

        #endregion

    }
}
