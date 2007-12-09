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

using System.Text.RegularExpressions;
using Seasar.Framework.Util;

namespace Seasar.Framework.Container.AutoRegister
{
    /// <summary>
    /// �����o�^�̑ΏہA��ΏۂƂȂ�N���X���̃p�^�[����ێ����܂��B
    /// </summary>
	public class ClassPattern
	{
        private string namespaceName;
        private Regex[] shortClassNamePatterns;

        /// <summary>
        /// �f�t�H���g�̃R���X�g���N�^�ł��B
        /// </summary>
        public ClassPattern()
        {
        }

        /// <summary>
        /// ���O��Ԗ��ƃN���X���̃p�^�[�����󂯎��R���X�g���N�^�ł��B
        /// </summary>
        /// <param name="namespaceName">���O��Ԗ�</param>
        /// <param name="shortClassNames">�N���X���̃p�^�[��</param>
        public ClassPattern(string namespaceName, string shortClassNames)
        {
            NamespaceName = namespaceName;
            ShortClassNames = shortClassNames;
        }

        /// <summary>
        /// ���O��Ԗ����擾�E�ݒ肵�܂��B
        /// </summary>
        public string NamespaceName
        {
            set { namespaceName = value; }
            get { return namespaceName; }
        }

        /// <summary>
        /// �i���O��Ԃ��܂܂Ȃ��j�N���X���̃p�^�[����ݒ肵�܂��B
        /// </summary>
        /// <remarks>
        /// �����̃p�^�[����ݒ肷��ꍇ�A','�ŋ�؂�܂��B
        /// </remarks>
        public string ShortClassNames
        {
            set
            {
                string[] classNames = value.Split(',');
                shortClassNamePatterns = new Regex[classNames.Length];

                for (int i = 0; i < classNames.Length; ++i)
                {
                    string className = classNames[i].Trim();
                    shortClassNamePatterns[i] = new Regex(className, RegexOptions.Compiled);
                }
            }
        }

        /// <summary>
        /// �i���O��Ԃ��܂܂Ȃ��j�N���X�����p�^�[���Ɉ�v���Ă��邩�ǂ�����Ԃ��܂��B
        /// </summary>
        /// <param name="shortClassName">�N���X��</param>
        /// <returns>��v���Ă���ꍇ��true, ��v���Ă��Ȃ��ꍇ��false</returns>
        public bool IsAppliedShortClassName(string shortClassName)
        {
            if (shortClassNamePatterns == null)
            {
                return true;
            }

            for (int i = 0; i < shortClassNamePatterns.Length; ++i)
            {
                if (shortClassNamePatterns[i].IsMatch(shortClassName))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// ���O��Ԗ����p�^�[���Ɉ�v���Ă��邩�ǂ�����Ԃ��܂��B
        /// </summary>
        /// <param name="namespaceName">���O��Ԗ�</param>
        /// <returns>��v���Ă���ꍇ��true, ��v���Ă��Ȃ��ꍇ��false</returns>
        public bool IsAppliedNamespaceName(string namespaceName)
        {
            if (!StringUtil.IsEmpty(namespaceName)
                && !StringUtil.IsEmpty(this.namespaceName))
            {
                return AppendDelimiter(namespaceName).StartsWith(
                    AppendDelimiter(this.namespaceName));
            }

            if (StringUtil.IsEmpty(namespaceName)
                && StringUtil.IsEmpty(this.namespaceName))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// �f���~�^��ǉ����܂��B
        /// </summary>
        /// <param name="name">���O��Ԗ�</param>
        /// <returns>���O��Ԗ��Ɍ��Ƀf���~�^('.')��ǉ���������</returns>
        protected static string AppendDelimiter(string name)
        {
            return name.EndsWith(".") ? name : name + ".";
        }
    }
}
