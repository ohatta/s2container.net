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
using System.Reflection;
using Seasar.Framework.Beans;

namespace Seasar.Framework.Container
{
    /// <summary>
    /// �R���|�[�l���g���C���W�F�N�V�������鎞�̓����\���o�C���f�B���O�^�C�v���`����C���^�[�t�F�[�X�ł��B
    /// </summary>
    /// <remarks>
    /// <para>�o�C���f�B���O�^�C�v��`�ɂ́A �ȉ��̂��̂�����܂��B</para>
    /// <list type="bullet">
    /// <item>
    /// <term><code>must</code></term>
    /// <description>�����o�C���f�B���O���K�p�ł��Ȃ������ꍇ�A ��O���������܂��B</description>
    /// </item>
    /// <item>
    /// <term><code>should</code></term>
    /// <description>�����o�C���f�B���O���K�p�ł��Ȃ������ꍇ�A �x����ʒm���܂��B</description>
    /// </item>
    /// <item>
    /// <term><code>may</code></term>
    /// <description>�����o�C���f�B���O���K�p�ł��Ȃ������ꍇ�ł����������܂���B</description>
    /// </item>
    /// <item>
    /// <term><code>none</code></term>
    /// <description><see cref="IAutoBindingDef">�����o�C���f�B���O��`</see>��
    /// <code>auto</code>��<code>property</code>�̏ꍇ�ł������o�C���f�B���O��K�p���܂���B
    /// </description>
    /// </item>
    /// </list>
    /// </remarks>
    public interface IBindingTypeDef
    {
        /// <summary>
        /// �o�C���f�B���O�^�C�v��`����Ԃ��܂��B
        /// </summary>
        /// <value>�o�C���f�B���O�^�C�v��`��</value>
        string Name { get; }

        /// <summary>
        /// �o�C���f�B���O�^�C�v��`�Ɋ�Â��āA <code>component</code>�ɑ΂���
        /// S2�R���e�i��̃R���|�[�l���g���v���p�e�B�o�R�ŃC���W�F�N�V�������܂��B
        /// </summary>
        /// <param name="componentDef">�R���|�[�l���g��`</param>
        /// <param name="propertyDef">�v���p�e�B�ɑ΂���ݒ���@��ݒ�l�̒�`</param>
        /// <param name="propertyDesc">�ΏۂƂȂ�R���|�[�l���g�̃v���p�e�B���</param>
        /// <param name="component">�ΏۂƂȂ�R���|�[�l���g�̃C���X�^���X</param>
        void Bind(IComponentDef componentDef, IPropertyDef propertyDef,
            IPropertyDesc propertyDesc, object component);

        /// <summary>
        /// �o�C���f�B���O�^�C�v��`�Ɋ�Â��āA <code>component</code>�ɑ΂���
        /// S2�R���e�i��̃R���|�[�l���g���t�B�[���h�ɒ��ڃC���W�F�N�V�������܂��B
        /// </summary>
        /// <param name="componentDef">�R���|�[�l���g��`</param>
        /// <param name="propertyDef">�v���p�e�B�ɑ΂���ݒ���@��ݒ�l�̒�`</param>
        /// <param name="field">�ΏۂƂȂ�R���|�[�l���g�̃t�B�[���h���</param>
        /// <param name="component">�ΏۂƂȂ�R���|�[�l���g�̃C���X�^���X</param>
        /// <exception cref="Seasar.Framework.Beans.IllegalPropertyRuntimeException">
        /// <code>propertyDef</code>�Ɏw�肳�ꂽ�R���|�[�l���g���擾�ł��Ȃ������ꍇ
        /// </exception>
        /// <exception cref="Seasar.Framework.Exceptions.IllegalAccessRuntimeException">
        /// �ΏۂƂȂ�R���|�[�l���g�̃t�B�[���h��<code>private</code>�ȂǂŃA�N�Z�X�ł��Ȃ������ꍇ
        /// </exception>
        void Bind(IComponentDef componentDef, IPropertyDef propertyDef, FieldInfo field,
            object component);
    }
}
