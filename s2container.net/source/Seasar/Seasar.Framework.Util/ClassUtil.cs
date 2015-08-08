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
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Seasar.Framework.Exceptions;

namespace Seasar.Framework.Util
{
    public static class ClassUtil
    {
        // ���؂̃L���b�V��
        private static readonly Dictionary<Type, Func<object>> _classCache = new Dictionary<Type, Func<object>>();

        public static ConstructorInfo GetConstructorInfo(Type type, Type[] argTypes)
        {
            var types = argTypes ?? Type.EmptyTypes;
            var constructor = type.GetConstructor(types);
            if (constructor == null)
            {
                throw new NoSuchConstructorRuntimeException(type, argTypes);
            }
            return constructor;
        }

        public static Type ForName(string className, Assembly[] assemblys)
        {
            var type = Type.GetType(className);
            if (type != null)
            {
                return type;
            }
            foreach (var assembly in assemblys)
            {
                type = assembly.GetType(className);
                if (type != null)
                {
                    return type;
                }
            }
            return null;
        }

        /// <summary>
        /// ���ݎg�p�\�ȃA�Z���u���̒�����A
        /// �N���X�����g���Č^���擾����
        /// </summary>
        /// <param name="className">���O��Ԃ��܂ރN���X��</param>
        /// <returns>�Y������^</returns>
        public static Type ForName(string className) => ForName(className, AppDomain.CurrentDomain.GetAssemblies());

        /// <summary>
        /// �C���X�^���X�𐶐�����
        /// </summary>
        /// <param name="type">��������^</param>
        /// <param name="nonPublic">�p�u���b�N�̊���R���X�g���N�^�[�B�p�u���b�N�łȂ�����R���X�g���N�^�[����v������ꍇ�́Atrue�B</param>
        /// <returns>�C���X�^���X</returns>
        public static object NewInstance(Type type, bool nonPublic = false)
        {
            Func<object> lambda;
            if (!_classCache.ContainsKey(type))
            {
                lambda = _CreateExpression(type, nonPublic);
                // �����������؂̓L���b�V���ɕۑ�
                _classCache.Add(type, lambda);
            }
            else
            {
                lambda = _classCache[type];
            }
            return lambda.DynamicInvoke(null);
//            return Activator.CreateInstance(type);
        }

        /// <summary>
        /// �C���X�^���X�𐶐�����
        /// </summary>
        /// <param name="info">�R���X�g���N�^���</param>
        /// <param name="type">��������^</param>
        /// <returns>�C���X�^���X</returns>
        public static object NewInstance(ConstructorInfo info, Type type)
        {
            Func<object> lambda;
            if (!_classCache.ContainsKey(type))
            {
                lambda = Expression.Lambda<Func<object>>(Expression.New(info)).Compile();
                // �����������؂̓L���b�V���ɕۑ�
                _classCache.Add(type, lambda);
            }
            else
            {
                lambda = _classCache[type];
            }
            return lambda.DynamicInvoke(null);
        }

        /// <summary>
        /// �C���X�^���X�𐶐�����
        /// </summary>
        /// <param name="className">���O��Ԃ��܂ރN���X��</param>
        /// <param name="assemblyName">�A�Z���u����</param>
        /// <returns>�C���X�^���X</returns>
        public static object NewInstance(string className, string assemblyName)
        {
            Assembly[] asms = {Assembly.LoadFrom(assemblyName)};
            return NewInstance(ForName(className, asms));
        }

        /// <summary>
        /// �C���X�^���X�����鎮��(Expression)���쐬����
        /// </summary>
        /// <param name="type">�C���X�^���X������^</param>
        /// <param name="nonPublic">�p�u���b�N�̊���R���X�g���N�^�[�B�p�u���b�N�łȂ�����R���X�g���N�^�[����v������ꍇ�́Atrue�B</param>
        /// <returns>�R���p�C����������</returns>
        private static Func<object> _CreateExpression(Type type, bool nonPublic)
        {
            ConstructorInfo info;
            if (nonPublic)
            {
                info = type.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[0], null);
            }
            else
            {
                info = type.GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
                    null, new Type[0], null);
            }
            return Expression.Lambda<Func<object>>(Expression.New(info)).Compile();
        }
    }
}