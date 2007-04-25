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
using Seasar.Framework.Aop;

namespace Seasar.Framework.Container
{
    /// <summary>
    /// �R���|�[�l���g�ɑg�ݍ��ރC���^�[�^�C�v���`����C���^�[�t�F�[�X�ł��B
    /// </summary>
    /// <remarks>
    /// <para>
    /// �C���^�[�^�C�v��`�́Adicon�t�@�C���ɂ�����<code>&lt;interType&gt;</code>�v�f�Ŏw�肳��܂��B
    /// <code>&lt;interType&gt;</code>�v�f�ɂ�class�������܂܂�Ă��܂��B
    /// </para>
    /// <para>
    /// class�����ɂ�<see cref="Seasar.Framework.Aop.IInterType">�C���^�[�^�C�v</see>
    /// �����������N���X���w�肵�܂��B
    /// </para>
    /// <para>
    /// InterType�́u�ÓI�ȍ\���̕ύX�v���������܂��B �u�ÓI�ȍ\���̕ύX�v�͉��L�̂��̂��܂݂܂��B
    /// </para>
    /// <list type="bullet">
    /// <item><term>�X�[�p�[�N���X�̕ύX</term></item>
    /// <item><term>�����C���^�[�t�F�[�X�̒ǉ�</term></item>
    /// <item><term>�t�B�[���h�̒ǉ�</term></item>
    /// <item><term>�R���X�g���N�^�̒ǉ�</term></item>
    /// <item><term>���\�b�h�̒ǉ�</term></item>
    /// </list>
    /// </remarks>
    public interface IInterTypeDef
    {
        /// <summary>
        /// �C���^�[�^�C�v��Ԃ��܂��B
        /// </summary>
        /// <value>�C���^�[�^�C�v</value>
        IInterType InterType { get; }
    }
}
