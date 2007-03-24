#region Copyright
/*
 * Copyright 2005-2007 the Seasar Foundation and the Others.
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
using System.Resources;
using System.Text;

namespace Seasar.Framework.Message
{
    /// <summary>
    /// ���b�Z�[�W�R�[�h�ƈ������v���p�e�B�ɓo�^����Ă���
    /// �p�^�[���ɓK�p���A���b�Z�[�W��g�ݗ��Ă܂��B
    /// ���b�Z�[�W�R�[�h�́A8���ō\������ŏ���1�������b�Z�[�W�̎�ʂŁA
    /// E:�G���[�AW:���[�j���O�AI:�C���t�H���[�V�����ō\������܂��B
    /// ����3�����V�X�e������Seasar�̏ꍇ�́ASSR�ɂȂ�܂��B
    /// �Ō��4���͘A�Ԃł��B
    /// ���b�Z�[�W��`�t�@�C���́A�V�X�e���� + Messages.resources�ɂȂ�܂��B
    /// SSRMessages.ja-JP.resources�Ȃǂ�p�ӂ��邱�Ƃő�����ɑΉ��ł��܂��B
    /// </summary>
    public sealed class MessageFormatter
    {
        private const string MESSAGES = "Messages";
        private static readonly object[] EMPTY_ARRAY = new object[0];
        private static readonly Hashtable _resourceManagers = new Hashtable();

        private MessageFormatter()
        {
        }

        public static string GetMessage(string messageCode, object[] args)
        {
            return GetMessage(messageCode, args, Assembly.GetExecutingAssembly());
        }

        public static string GetMessage(string messageCode, object[] args, Assembly assembly)
        {
            if (messageCode == null)
            {
                messageCode = string.Empty;
            }
            return "[" + messageCode + "] " + GetSimpleMessage(messageCode, args, assembly);
        }

        public static string GetSimpleMessage(string messageCode, object[] arguments)
        {
            return GetSimpleMessage(messageCode, arguments, Assembly.GetExecutingAssembly());
        }

        public static string GetSimpleMessage(string messageCode, object[] arguments, Assembly assembly)
        {
            try
            {
                string pattern = GetPattern(messageCode, assembly);
                if (pattern != null)
                {
                    if (arguments == null)
                    {
                        arguments = EMPTY_ARRAY;
                    }
                    return string.Format(pattern, arguments);
                }
            }
            catch
            {
            }
            return GetNoPatternMessage(arguments);
        }

        private static string GetPattern(string messageCode, Assembly assembly)
        {
            ResourceManager resourceManager = GetMessages(GetSystemName(messageCode), assembly);
            if (resourceManager != null)
            {
                return resourceManager.GetString(messageCode);
            }
            return null;
        }

        private static string GetSystemName(string messageCode)
        {
            return messageCode.Substring(1, Math.Min(3, messageCode.Length));
        }

        private static ResourceManager GetMessages(string systemName, Assembly assembly)
        {
            string key = systemName + assembly.FullName;
            if (_resourceManagers.ContainsKey(key))
            {
                return (ResourceManager) _resourceManagers[key];
            }
            else
            {
                ResourceManager rm = new ResourceManager(systemName + MESSAGES, assembly);
                _resourceManagers[key] = rm;
                return rm;
            }
        }

        private static string GetNoPatternMessage(object[] args)
        {
            if (args == null || args.Length == 0) return string.Empty;
            StringBuilder buffer = new StringBuilder();
            foreach (object arg in args)
            {
                buffer.Append(arg + ", ");
            }
            buffer.Length = buffer.Length - 2;
            return buffer.ToString();
        }
    }
}
