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
    /// �����o�C���f�B���O��K�p����͈͂�\�������o�C���f�B���O��`�̃C���^�[�t�F�[�X�ł��B
    /// </summary>
    /// <remarks>
    /// <para>�����o�C���f�B���O��`�ɂ́A �ȉ��̂��̂�����܂��B</para>
    /// <list type="bullet">
    /// <item>
    /// <term><code>auto</code></term>
    /// <description>�R���X�g���N�^�ƃv���p�e�B�̗����ŁA �����o�C���f�B���O��K�p���܂��B</description>
    /// </item>
    /// <item>
    /// <term><code>semiauto</code></term>
    /// <description>�����I�Ɏw�肵���v���p�e�B�ɑ΂��Ă̂ݎ����o�C���f�B���O��K�p���܂��B</description>
    /// </item>
    /// <item>
    /// <term><code>constructor</code></term>
    /// <description>�R���X�g���N�^�̎����o�C���f�B���O�̂ݓK�p���܂��B</description>
    /// </item>
    /// <item>
    /// <term><code>property</code></term>
    /// <description>�v���p�e�B�̎����o�C���f�B���O�̂ݓK�p���܂��B</description>
    /// </item>
    /// <item>
    /// <term><code>none</code></term>
    /// <description>���ׂĂ̎����o�C���f�B���O��K�p���܂���B</description>
    /// </item>
    /// </list>
    /// </remarks>
    /// <seealso cref="AutoBindingDefConstants"/>
    public interface IAutoBindingDef
    {
        /// <summary>
        /// �����o�C���f�B���O��`����Ԃ��܂��B
        /// </summary>
        /// <value>�����o�C���f�B���O��`��</value>
        string Name { get; }

        /// <summary>
        /// �����o�C���f�B���O��`�Ɋ�Â��A <code>componentDef</code>�ɑ΂���
        /// <see cref="IConstructorAssembler"/>��Ԃ��܂��B
        /// </summary>
        /// <param name="componentDef">�R���|�[�l���g��`</param>
        /// <returns>�����o�C���f�B���O�͈̔͂��ݒ肳�ꂽ<see cref="IConstructorAssembler"/></returns>
        IConstructorAssembler CreateConstructorAssembler(IComponentDef componentDef);

        /// <summary>
        /// �����o�C���f�B���O��`�Ɋ�Â��A <code>componentDef</code>�ɑ΂���
        /// <see cref="IPropertyAssembler"/>��Ԃ��܂��B
        /// </summary>
        /// <param name="componentDef">�R���|�[�l���g��`</param>
        /// <returns>�����o�C���f�B���O�͈̔͂��ݒ肳�ꂽ<see cref="IPropertyAssembler"/></returns>
        IPropertyAssembler CreatePropertyAssembler(IComponentDef componentDef);
    }
}
