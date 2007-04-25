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
    /// ���̃C���^�[�t�F�[�X�̓v���p�e�B��`��o�^����ю擾������@��
    /// ��`����I�u�W�F�N�g��\���܂��B
    /// </summary>
    /// <remarks>
    /// <para>
    /// �v���p�e�B��`�͕����o�^���邱�Ƃ��o���܂��B
    /// �v���p�e�B��`�̎擾�̓C���f�b�N�X�ԍ����w�肵�čs���܂��B
    /// </para>
    /// </remarks>
    /// <seealso cref="IPropertyDef"/>
    public interface IPropertyDefAware
    {
        /// <summary>
        /// <see cref="IPropertyDef">�v���p�e�B��`</see>��ǉ����܂��B
        /// </summary>
        /// <param name="propertyDef">�v���p�e�B��`</param>
        void AddPropertyDef(IPropertyDef propertyDef);

        /// <summary>
        /// <see cref="IPropertyDef">�v���p�e�B��`</see>�̐���Ԃ��܂��B
        /// </summary>
        /// <value>�o�^����Ă���v���p�e�B��`�̐�</value>
        int PropertyDefSize { get; }

        /// <summary>
        /// �w�肳�ꂽ�C���f�b�N�X�ԍ�<code>index</code>��
        /// <see cref="IPropertyDef">�v���p�e�B��`</see>��Ԃ��܂��B
        /// </summary>
        /// <param name="index">�v���p�e�B��`���w�肷��C���f�b�N�X�ԍ�</param>
        /// <returns>�v���p�e�B��`</returns>
        IPropertyDef GetPropertyDef(int index);

        /// <summary>
        /// �w�肵���v���p�e�B���œo�^����Ă���
        /// <see cref="IPropertyDef">�v���p�e�B��`</see>��Ԃ��܂��B
        /// </summary>
        /// <param name="propertyName">�v���p�e�B��</param>
        /// <returns>�v���p�e�B��`</returns>
        IPropertyDef GetPropertyDef(string propertyName);

        /// <summary>
        /// �w�肵���v���p�e�B���̃v���p�e�B��`�������<code>true</code>��Ԃ��܂��B
        /// </summary>
        /// <param name="propertyName">�v���p�e�B��</param>
        /// <returns>
        /// �v���p�e�B��`�����݂��Ă����<code>true</code>�A���݂��Ă��Ȃ����<code>false</code>
        /// </returns>
        bool HasPropertyDef(string propertyName);
    }
}
