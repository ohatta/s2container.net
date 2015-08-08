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
using System.Text.RegularExpressions;
using Seasar.Framework.Exceptions;

namespace Seasar.Framework.Aop.Impl
{
    /// <summary>
    /// IPointcut�C���^�[�t�F�C�X�̎���
    /// </summary>
    [Serializable]
    public sealed class PointcutImpl : IPointcut
    {
        /// <summary>
        /// �R���p�C���ς݃��\�b�h���̐��K�\��
        /// </summary>
        private Regex[] _regularExpressions;

        /// <summary>
        /// ���\�b�h���̐��K�\��������
        /// </summary>
        private string[] _methodNames;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public PointcutImpl(Type targetType)
        {
            if (targetType == null)
            {
                throw new EmptyRuntimeException("targetType");
            }
            _SetMethodNames(_GetMethodNames(targetType));
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="methodNames">���\�b�h���̐��K�\��������̔z��</param>
        public PointcutImpl(string[] methodNames)
        {
            if (methodNames == null || methodNames.Length == 0)
            {
                throw new EmptyRuntimeException("methodNames");
            }
            _SetMethodNames(methodNames);
        }

        #region IPointcut �����o

        /// <summary>
        /// �����œn���ꂽmethod��Advice��}�����邩�m�F���܂�
        /// </summary>
        /// <param name="method">MethodBase ���\�b�h�ƃR���X�g���N�^�Ɋւ�����������Ă��܂�</param>
        /// <returns>True�Ȃ�Advice��}������AFalse�Ȃ�Advice�͑}������Ȃ�</returns>
        public bool IsApplied(MethodBase method)
        {
            return IsApplied(method.Name);
        }

        /// <summary>
        /// �����œn���ꂽ���\�b�h����Advice��}�����邩�m�F���܂�
        /// </summary>
        /// <param name="methodName">���\�b�h��</param>
        /// <returns>True�Ȃ�Advice��}������AFalse�Ȃ�Advice�͑}������Ȃ�</returns>
        public bool IsApplied(string methodName)
        {
            foreach (var regex in _regularExpressions)
            {
                if (regex.Match(methodName).Success)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        /// <summary>
        /// ���\�b�h���̐��K�\��������
        /// </summary>
        public string[] MethodNames => _methodNames;

        private void _SetMethodNames(string[] methodNames)
        {
            _methodNames = methodNames;
            _regularExpressions = new Regex[methodNames.Length];
            for (var i = 0; i < methodNames.Length; ++i)
            {
                var methodName = @"^" + methodNames[i].Trim() + "$";
                _regularExpressions[i] = new Regex(methodName, RegexOptions.Compiled);
            }
        }

        private static string[] _GetMethodNames(Type targetType)
        {
            var methodNameList = new Hashtable();
            if (targetType.IsInterface)
            {
                _AddInterfaceMethodNames(methodNameList, targetType);
            }
            for (var type = targetType; type != typeof(object) && type != null; type = type.BaseType)
            {
                var interfaces = type.GetInterfaces();
                foreach (var interfaceTemp in interfaces)
                {
                    _AddInterfaceMethodNames(methodNameList, interfaceTemp);
                }
            }
            var methodNames = new string[methodNameList.Count];
            var enu = methodNameList.Keys.GetEnumerator();
            var i = 0;
            while (enu.MoveNext())
            {
                methodNames[i++] = (string) enu.Current;
            }
            return methodNames;
        }

        private static void _AddInterfaceMethodNames(Hashtable methodNameList, Type interfaceType)
        {
            var methods = interfaceType.GetMethods();
            foreach (var method in methods)
            {
                if (!methodNameList.ContainsKey(method.Name))
                {
                    methodNameList.Add(method.Name, null);
                }
            }
            var interfaces = interfaceType.GetInterfaces();
            foreach (var interfaceTemp in interfaces)
            {
                _AddInterfaceMethodNames(methodNameList, interfaceTemp);
            }
        }
    }
}
