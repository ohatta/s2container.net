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

namespace Seasar.Framework.Container
{
    /// <summary>
    /// ����\�킷�C���^�[�t�F�[�X�ł��B
    /// </summary>
    /// <remarks>
    /// <para>
    /// ���Ƃ́A �I�u�W�F�N�g�̐����A �v���p�e�B�ւ̃A�N�Z�X�A ���\�b�h�̌Ăяo���A 
    /// ��`�ς݃I�u�W�F�N�g�̎w��A ���e�����̋L�q�A���Z�A�Ȃǂ��o����\�����@�ł��B
    /// �܂��A ���̎����ɂ���Ă�(JScript.NET�ł�)�ϐ��̎g�p�Ȃǂ��o���܂��B
    /// </para>
    /// <para>
    /// dicon�t�@�C����<code>&lt;property&gt;</code>�A <code>&lt;component&gt;</code>�A
    /// <code>&lt;initMethod&gt;</code>�A <code>&lt;destroyMethod&gt;</code>�A
    /// <code>&lt;arg&gt;</code>�A <code>&lt;meta&gt;</code>�A �Ɏ����L�q���邱�Ƃ��o���܂��B
    /// </para>
    /// <para>
    /// ��`�ς݃I�u�W�F�N�g�́A �ȉ��̂��̂�����܂��B
    /// </para>
    /// <list type="bullet">
    /// <item>
    /// <term><see cref="Seasar.Framework.Container.ContainerConstants.CONTAINER_NAME" /></term>
    /// <description>���݂�dicon�t�@�C�����������Ă���S2�R���e�i�ł��B</description>
    /// </item>
    /// <item>
    /// <term><see cref="Seasar.Framework.Container.ContainerConstants.REQUEST_NAME" /></term>
    /// <description>Web�R���e�i�ȂǂŎ��s����Ă���ꍇ�A 
    /// ���݂̃X���b�h�ŏ������Ă��郊�N�G�X�g�ł��B</description>
    /// </item>
    /// <item>
    /// <term><see cref="Seasar.Framework.Container.ContainerConstants.RESPONSE_NAME" /></term>
    /// <description>Web�R���e�i�ȂǂŎ��s����Ă���ꍇ�A
    /// ���݂̃X���b�h�ŏ������Ă��郌�X�|���X�ł��B</description>
    /// </item>
    /// <item>
    /// <term><see cref="Seasar.Framework.Container.ContainerConstants.SESSION_NAME" /></term>
    /// <description>Web�R���e�i�ȂǂŎ��s����Ă���ꍇ�A
    /// ���݂̃X���b�h�ŏ������Ă���Z�b�V�����ł��B</description>
    /// </item>
    /// <item>
    /// <term><see cref="Seasar.Framework.Container.ContainerConstants.APPLICATION_CONTEXT_NAME" /></term>
    /// <description>Web�R���e�i�ȂǂŎ��s����Ă���ꍇ�A
    /// ���݂�S2�R���e�i�Ɋ֘A�Â���ꂽ�A�v���P�[�V�����R���e�L�X�g�ł��B</description>
    /// </item>
    /// </list>
    /// <para>��`�ς݃I�u�W�F�N�g�̑��ɂ��A S2�R���e�i�ɓo�^����Ă���R���|�[�l���g��
    /// <code>name</code>�����ŎQ�Ƃ��邱�Ƃ��o���܂��B</para>
    /// </remarks>
    public interface IExpression
    {
        /// <summary>
        /// ����]���������ʂ�Ԃ��܂��B
        /// </summary>
        /// <param name="container">����]������R���e�L�X�g�ƂȂ�S2�R���e�i</param>
        /// <param name="context">����]������R���e�L�X�g�ɒǉ��ł���R���e�L�X�g</param>
        /// <returns>����]����������</returns>
        object Evaluate(IS2Container container, IDictionary context);
    }
}
