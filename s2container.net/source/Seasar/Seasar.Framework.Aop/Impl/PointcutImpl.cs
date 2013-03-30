#region Copyright
/*
 * Copyright 2005-2013 the Seasar Foundation and the Others.
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
            SetMethodNames(GetMethodNames(targetType));
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
            SetMethodNames(methodNames);
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
            foreach (Regex regex in _regularExpressions)
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
        public string[] MethodNames
        {
            get { return _methodNames; }
        }

        private void SetMethodNames(string[] methodNames)
        {
            _methodNames = methodNames;
            _regularExpressions = new Regex[methodNames.Length];
            for (int i = 0; i < methodNames.Length; ++i)
            {
                string methodName = @"^" + methodNames[i].Trim() + "$";
                _regularExpressions[i] = new Regex(methodName, RegexOptions.Compiled);
            }
        }

        private static string[] GetMethodNames(Type targetType)
        {
            Hashtable methodNameList = new Hashtable();
            if (targetType.IsInterface)
            {
                AddInterfaceMethodNames(methodNameList, targetType);
            }
            for (Type type = targetType; type != typeof(object) && type != null; type = type.BaseType)
            {
                Type[] interfaces = type.GetInterfaces();
                foreach (Type interfaceTemp in interfaces)
                {
                    AddInterfaceMethodNames(methodNameList, interfaceTemp);
                }
            }
            string[] methodNames = new string[methodNameList.Count];
            IEnumerator enu = methodNameList.Keys.GetEnumerator();
            int i = 0;
            while (enu.MoveNext())
            {
                methodNames[i++] = (string) enu.Current;
            }
            return methodNames;
        }

        private static void AddInterfaceMethodNames(Hashtable methodNameList, Type interfaceType)
        {
            MethodInfo[] methods = interfaceType.GetMethods();
            foreach (MethodInfo method in methods)
            {
                if (!methodNameList.ContainsKey(method.Name))
                {
                    methodNameList.Add(method.Name, null);
                }
            }
            Type[] interfaces = interfaceType.GetInterfaces();
            foreach (Type interfaceTemp in interfaces)
            {
                AddInterfaceMethodNames(methodNameList, interfaceTemp);
            }
        }
    }
}
