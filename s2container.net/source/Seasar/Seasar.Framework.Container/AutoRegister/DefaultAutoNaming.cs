#region Copyright
/*
 * Copyright 2005-2009 the Seasar Foundation and the Others.
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

namespace Seasar.Framework.Container.AutoRegister
{
    /// <summary>
    /// �R���|�[�l���g�Ɏ����I�ɖ��O��t����ׂ̕W���̃N���X�ł��B
    /// </summary>
    public class DefaultAutoNaming : IAutoNaming
    {
        #region IAutoNaming �����o

        /// <summary>
        /// �R���|�[�l���g�����`���܂��B
        /// </summary>
        /// <remarks>
        /// <para>�C���^�[�t�F�[�X�̏ꍇ�̓v���t�B�b�N�X��"I"����菜����
        /// �C���^�[�t�F�[�X���i���O��Ԃ͊܂܂Ȃ��j���R���|�[�l���g���Ƃ��܂��B
        /// �i������2�����ڂ��啶���̏ꍇ�̂�1�����ڂ��v���t�B�b�N�X�Ɣ��f���܂��j</para>
        /// <para>����ȊO�͖��O��Ԃ��܂܂Ȃ��ȈՖ����R���|�[�l���g���Ƃ��܂��B</para>
        /// </remarks>
        /// <param name="type">�R���|�[�l���g�����`������Type</param>
        /// <returns>�R���|�[�l���g��</returns>
        public string DefineName(Type type)
        {
            string name;

            if (type.IsInterface && type.Name.Length > 1 && type.Name[0] == 'I'
                && char.IsUpper(type.Name[1]))
            {
                // �C���^�[�t�F�[�X�̃v���t�B�b�N�X������Ɣ��f�����ꍇ��
                // �v���t�B�b�N�X"I"�����������̂��R���|�[�l���g���Ƃ���
                name = type.Name.Substring(1);
            }
            else
            {
                // ��L�ȊO�̏ꍇ�͊ȈՖ����R���|�[�l���g���Ƃ���
                name = type.Name;
            }

            return name;
        }

        #endregion
    }
}
