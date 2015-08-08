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

namespace Seasar.Framework.Container.AutoRegister
{
    /// <summary>
    /// �����o�^�p�̒��ۃN���X�ł��B
    /// </summary>
    public abstract class AbstractAutoRegister
    {
        private readonly IList<ClassPattern> _classPatterns = new List<ClassPattern>();
        private readonly IList<ClassPattern> _ignoreClassPatterns = new List<ClassPattern>();

        /// <summary>
        /// �R���e�i���擾�E�ݒ肵�܂��B
        /// </summary>
        public IS2Container Container { set; get; }

        /// <summary>
        /// �ǉ�����Ă��� ClassPattern �̐����擾���܂��B
        /// </summary>
        public int ClassPatternSize => _classPatterns.Count;

        /// <summary>
        /// ClassPattern���擾���܂��B
        /// </summary>
        /// <param name="index">�擾����ClassPattern�̃C���f�b�N�X</param>
        /// <returns>ClassPattern</returns>
        public ClassPattern GetClassPattern(int index) => _classPatterns[index];

        /// <summary>
        /// �����o�^�œK�p����� ClassPattern ��ǉ����܂��B
        /// </summary>
        /// <param name="namespaceName">���O��Ԗ�</param>
        /// <param name="shortClassNames">�N���X���̃p�^�[��</param>
        public void AddClassPattern(string namespaceName, string shortClassNames)
        {
            AddClassPattern(new ClassPattern(namespaceName, shortClassNames));
        }

        /// <summary>
        /// �����o�^�œK�p����� ClassPattern ��ǉ����܂��B
        /// </summary>
        /// <param name="classPattern">ClassPattern</param>
        public void AddClassPattern(ClassPattern classPattern)
        {
            _classPatterns.Add(classPattern);
        }

        /// <summary>
        /// �����o�^����Ȃ� ClassPattern ��ǉ����܂��B
        /// </summary>
        /// <param name="namespaceName">���O��Ԗ�</param>
        /// <param name="shortClassNames">�N���X���̃p�^�[��</param>
        public void AddIgnoreClassPattern(string namespaceName, string shortClassNames)
        {
            AddIgnoreClassPattern(new ClassPattern(namespaceName, shortClassNames));
        }

        /// <summary>
        /// �����o�^����Ȃ� ClassPattern ��ǉ����܂��B
        /// </summary>
        /// <param name="classPattern">ClassPattern</param>
        public void AddIgnoreClassPattern(ClassPattern classPattern)
        {
            _ignoreClassPatterns.Add(classPattern);
        }

        /// <summary>
        /// �����o�^���s���܂��B
        /// </summary>
        /// <remarks>
        /// �����ɂ�鏉�������\�b�h�̎w�肪�ł��Ȃ��̂�Java�łƂ͈قȂ�
        /// ���̃��\�b�h��dicon�t�@�C���ŏ��������\�b�h�Ƃ��ČĂяo���K�v������܂��B
        /// </remarks>
        public abstract void RegisterAll();

        /// <summary>
        /// �������邩�ǂ�����Ԃ��܂��B
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns>�������邩�ǂ���</returns>
        protected bool IsIgnore(Type type)
        {
            if (_ignoreClassPatterns.Count == 0)
            {
                return false;
            }

            for (var i = 0; i < _ignoreClassPatterns.Count; ++i)
            {
                var cp = _ignoreClassPatterns[i];

                if (!cp.IsAppliedNamespaceName(type.Namespace))
                {
                    continue;
                }

                if (cp.IsAppliedShortClassName(type.Name))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
