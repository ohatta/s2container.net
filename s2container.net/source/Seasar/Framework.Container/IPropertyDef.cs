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
    /// �R���|�[�l���g�̃v���p�e�B�܂��̓t�B�[���h�ɃC���W�F�N�V����������@���`����C���^�[�t�F�[�X�ł��B
    /// </summary>
    /// <remarks>
    /// <para>
    /// �v���p�e�B��`�́Adicon�t�@�C���ɂ�����<code>&lt;property&gt;</code>�v�f�Ŏw�肳��܂��B
    /// <code>&lt;property&gt;</code>�v�f�ɂ�name������bindingType�������܂܂�Ă��܂��B
    /// </para>
    /// <list type="bullet">
    /// <item><term>name�����̓R���|�[�l���g�̃v���p�e�B���܂��̓t�B�[���h�����w�肵�܂��B</term></item>
    /// <item><term>bindingType������name�����ɂĎw�肳�ꂽ�v���p�e�B�܂��̓t�B�[���h�ɁA
    /// S2�R���e�i���Ɋi�[����Ă���R���|�[�l���g���o�C���f�B���O����ۂ̋������w�肵�܂��B</term></item>
    /// </list>
    /// <para>
    /// <code>&lt;property&gt;</code>�v�f�̓��e�Ɏw�肳�ꂽ���܂��̓R���|�[�l���g���A
    /// <code>&lt;property&gt;</code>�v�f��name�����Ŏw�肳�ꂽ�v���p�e�B�܂��̓t�B�[���h�ɐݒ肳��܂��B
    /// </para>
    /// <para>
    /// �v���p�e�B��`�����݂���ꍇ�̃v���p�e�B�C���W�F�N�V�����܂��̓t�B�[���h�C���W�F�N�V�����́A
    /// dicon�t�@�C���ɋL�q����Ă���v���p�e�B��`�ɏ]���čs���܂��B
    /// �v���p�e�B��`�����݂��Ȃ��ꍇ�A<see cref="IAutoBindingDef">�����o�C���f�B���O��`</see>��
    /// ��`�ɂ���Ď����o�C���f�B���O���s���鎖������܂��B
    /// </para>
    /// </remarks>
    public interface IPropertyDef : IArgDef
    {
        /// <summary>
        /// �C���W�F�N�V�����ΏۂƂȂ�v���p�e�B���܂��̓t�B�[���h����Ԃ��܂��B
        /// </summary>
        /// <value>�ݒ�ΏۂƂȂ�v���p�e�B��</value>
        string PropertyName { get; }

        /// <summary>
        /// �o�C���f�B���O�^�C�v��`���擾�E�ݒ肵�܂��B
        /// </summary>
        /// <value>�o�C���f�B���O�^�C�v��`</value>
        IBindingTypeDef BindingTypeDef { get; set; }

        /// <summary>
        /// �A�N�Z�X�^�C�v��`���擾�E�ݒ肵�܂��B
        /// </summary>
        /// <value>�A�N�Z�X�^�C�v��`</value>
        IAccessTypeDef AccessTypeDef { get; set; }
    }
}
