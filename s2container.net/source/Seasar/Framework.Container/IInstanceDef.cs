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
    /// �R���|�[�l���g�̃C���X�^���X��S2�R���e�i��łǂ̂悤�ɊǗ�����̂����`���܂��B
    /// </summary>
    /// <remarks>
    /// <para>�C���X�^���X��`�̎�ނɂ́A�ȉ��̂��̂�����܂��B</para>
    /// <list type="bullet">
    /// <item>
    /// <term><code>singleton</code>(default)</term>
    /// <description>S2�R���e�i��ŗB��̃C���X�^���X�ɂȂ�܂��B</description>
    /// </item>
    /// <item>
    /// <term><code>prototype</code></term>
    /// <description>�R���|�[�l���g���K�v�Ƃ����x�ɈقȂ�C���X�^���X�ɂȂ�܂��B</description>
    /// </item>
    /// <item>
    /// <term><code>application</code></term>
    /// <description>�A�v���P�[�V�����R���e�L�X�g����1�̃C���X�^���X�ɂȂ�܂��B</description>
    /// </item>
    /// <item>
    /// <term><code>request</code></term>
    /// <description>���N�G�X�g�R���e�L�X�g����1�̃C���X�^���X�ɂȂ�܂��B</description>
    /// </item>
    /// <item>
    /// <term><code>session</code></term>
    /// <description>�Z�b�V�����R���e�L�X�g����1�̃C���X�^���X�ɂȂ�܂�</description>
    /// </item>
    /// <item>
    /// <term><code>outer</code></term>
    /// <description>
    /// �R���|�[�l���g�̃C���X�^���X��<see cref="IS2Container"/>�̊O�Ő������A
    /// �C���W�F�N�V�����������s�Ȃ��܂��B�A�X�y�N�g�A�R���X�g���N�^�E�C���W�F�N�V�����͓K�p�ł��܂���B
    /// </description>
    /// </item>
    /// </list>
    /// <para>
    /// ���ꂼ��A �C���X�^���X�����������^�C�~���O�́A���̃R���|�[�l���g���K�v�Ƃ���鎞�ɂȂ�܂��B
    /// �܂��A���̎��_�ő��݂���u�R���e�L�X�g�v�ɑ�����R���|�[�l���g�̂݃C���W�F�N�V�������\�ł��B
    /// </para>
    /// <para>�C���X�^���X��`���ȗ������ꍇ��<code>singleton</code>���w�肵�����ƂɂȂ�܂��B</para>
    /// </remarks>
    public interface IInstanceDef
    {
        /// <summary>
        /// �C���X�^���X��`�̕�����\����Ԃ��܂��B
        /// </summary>
        /// <value>�C���X�^���X��`��\��������</value>
        string Name { get; }

        /// <summary>
        /// �C���X�^���X��`�Ɋ�Â����A�R���|�[�l���g��`<code>componentDef</code>��
        /// <see cref="IComponentDeployer"/>��Ԃ��܂��B
        /// </summary>
        /// <param name="componentDef">�R���|�[�l���g��`</param>
        /// <returns><see cref="IComponentDeployer"/>�I�u�W�F�N�g</returns>
        IComponentDeployer CreateComponentDeployer(IComponentDef componentDef);
    }
}
