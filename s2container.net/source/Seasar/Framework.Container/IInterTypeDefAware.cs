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

namespace Seasar.Framework.Container
{
    /// <summary>
    /// ���̃C���^�[�t�F�[�X�̓C���^�[�^�C�v��`��o�^����ю擾������@��
    /// ��`����I�u�W�F�N�g��\���܂��B
    /// </summary>
    /// <remarks>
    /// <para>
    /// �C���^�[�^�C�v��`�͕����o�^���邱�Ƃ��o���܂��B 
    /// �C���^�[�^�C�v��`�̎擾�̓C���f�b�N�X�ԍ����w�肵�čs���܂��B
    /// </para>
    /// </remarks>
    /// <seealso cref="IInterTypeDef"/>
    public interface IInterTypeDefAware
    {
        /// <summary>
        /// <see cref="IInterTypeDef">�C���^�[�^�C�v��`</see>��ǉ����܂��B
        /// </summary>
        /// <param name="interTypeDef">�C���^�[�^�C�v��`</param>
        void AddInterTypeDef(IInterTypeDef interTypeDef);

        /// <summary>
        /// �o�^����Ă���<see cref="IInterTypeDef">�C���^�[�^�C�v��`</see>�̐���Ԃ��܂��B
        /// </summary>
        /// <value>�o�^����Ă���C���^�[�^�C�v��`�̐�</value>
        int InterTypeDefSize { get; }

        /// <summary>
        /// �w�肳�ꂽ�C���f�b�N�X�ԍ�<code>index</code>��
        /// <see cref="IInterTypeDef">�C���^�[�^�C�v��`</see>��Ԃ��܂��B
        /// <see cref="IInterTypeDef">
        /// </summary>
        /// <param name="index">�C���^�[�^�C�v��`���w�肷��C���f�b�N�X�ԍ�</param>
        /// <returns>�C���^�[�^�C�v��`</returns>
        IInterTypeDef GetInterTypeDef(int index);
    }
}
