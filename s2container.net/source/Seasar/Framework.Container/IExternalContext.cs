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
    /// S2�R���e�i��ŁA Web�R���e�i�Ȃǂ̊O���R���e�L�X�g���������߂̃C���^�[�t�F�[�X�ł��B
    /// </summary>
    /// <remarks>
    /// <para>
    /// <see cref="IInstanceDef">�C���X�^���X��`</see>�ŁA<code>application</code>�A
    /// <code>request</code>�A <code>session</code>���g���ꍇ�A
    /// <see cref="IS2Container.Init"/>���s���O��
    /// <code>IExternalContext</code>��S2�R���e�i�ɐݒ肷��K�v������܂��B
    /// </para>
    /// </remarks>
    public interface IExternalContext
    {
        /// <summary>
        /// ���N�G�X�g�R���e�L�X�g���擾�E�ݒ肵�܂��B
        /// </summary>
        /// <value>���N�G�X�g�R���e�L�X�g</value>
        /// <seealso cref="InstanceDefConstants.REQUEST_NAME"/>
        /// <seealso cref="Seasar.Framework.Container.Deployer.InstanceRequestDef"/>
        object Request { get; set; }

        /// <summary>
        /// ���X�|���X�R���e�L�X�g���擾�E�ݒ肵�܂��B
        /// </summary>
        /// <value>���X�|���X�R���e�L�X�g</value>
        object Response { get; set; }

        /// <summary>
        /// �Z�b�V�����R���e�L�X�g��Ԃ��܂��B
        /// </summary>
        /// <value>�Z�b�V�����R���e�L�X�g</value>
        /// <see cref="InstanceDefConstants.SESSION_NAME"/>
        /// <seealso cref="Seasar.Framework.Container.Deployer.InstanceSessionDef"/>
        object Session { get; }

        /// <summary>
        /// �A�v���P�[�V�����R���e�L�X�g���擾�E�ݒ肵�܂��B
        /// </summary>
        /// <value>�A�v���P�[�V�����R���e�L�X�g</value>
        /// <see cref="InstanceDefConstants.APPLICATION_NAME"/>
        /// <seealso cref="Seasar.Framework.Container.Deployer.InstanceApplicationDef"/>
        object Application { get; set; }

        /// <summary>
        /// �A�v���P�[�V�����R���e�L�X�g��<see cref="System.Collections.IDictionary"/>
        /// �C���^�[�t�F�[�X�ŕԂ��܂��B
        /// </summary>
        /// <value>�A�v���P�[�V�����R���e�L�X�g</value>
        /// <see cref="InstanceDefConstants.APPLICATION_NAME"/>
        /// <seealso cref="Seasar.Framework.Container.Deployer.InstanceApplicationDef"/>
        IDictionary ApplicationDictionary { get; }

        /// <summary>
        /// �����ݒ�l��<see cref="System.Collections.IDictionary"/>�C���^�[�t�F�[�X�ŕԂ��܂��B
        /// </summary>
        /// <value>�����ݒ�l</value>
        IDictionary InitParameterDictionary { get; }

        /// <summary>
        /// �Z�b�V�����R���e�L�X�g��<see cref="System.Collections.IDictionary"/>
        /// �C���^�[�t�F�[�X�ŕԂ��܂��B
        /// </summary>
        /// <value>�Z�b�V�����R���e�L�X�g</value>
        /// <see cref="InstanceDefConstants.SESSION_NAME"/>
        /// <seealso cref="Seasar.Framework.Container.Deployer.InstanceSessionDef"/>
        IDictionary SessionDictionary { get; }

        /// <summary>
        /// ���N�G�X�g�N�b�L�[��<see cref="System.Collections.IDictionary"/>�C���^�[�t�F�[�X�ŕԂ��܂��B
        /// </summary>
        /// <value>���N�G�X�g�N�b�L�[</value>
        IDictionary RequestCookieDictionary { get; }

        /// <summary>
        /// �L�[�ɑ΂���l��1�����N�G�X�g�w�b�_�[��
        /// <see cref="System.Collections.IDictionary"/>�C���^�[�t�F�[�X�ŕԂ��܂��B
        /// </summary>
        /// <value>�L�[�ɑ΂���l��1�����N�G�X�g�w�b�_�[</value>
        IDictionary RequestHeaderDictionary { get; }

        /// <summary>
        /// �L�[�ɑ΂���l�𕡐������N�G�X�g�w�b�_�[��
        /// <see cref="System.Collections.IDictionary"/>�C���^�[�t�F�[�X�ŕԂ��܂��B
        /// </summary>
        /// <value>�L�[�ɑ΂���l�𕡐������N�G�X�g�w�b�_�[</value>
        IDictionary RequestHeaderValuesDictionary { get; }

        /// <summary>
        /// ���N�G�X�g�R���e�L�X�g��<see cref="System.Collections.IDictionary"/>
        /// �C���^�[�t�F�[�X�ŕԂ��܂��B
        /// </summary>
        /// <value>���N�G�X�g�R���e�L�X�g</value>
        /// <seealso cref="InstanceDefConstants.REQUEST_NAME"/>
        /// <seealso cref="Seasar.Framework.Container.Deployer.InstanceRequestDef"/>
        IDictionary RequestDictionary { get; }

        /// <summary>
        /// �L�[�ɑ΂���l��1�����N�G�X�g�p�����[�^��
        /// <see cref="System.Collections.IDictionary"/>�C���^�[�t�F�[�X�ŕԂ��܂��B
        /// </summary>
        /// <value>�L�[�ɑ΂���l��1�����N�G�X�g�p�����[�^</value>
        IDictionary RequestParameterDictionary { get; }

        /// <summary>
        /// �L�[�ɑ΂���l�𕡐������N�G�X�g�p�����[�^��
        /// <see cref="System.Collections.IDictionary"/>�C���^�[�t�F�[�X�ŕԂ��܂��B
        /// </summary>
        /// <value>�L�[�ɑ΂���l�𕡐������N�G�X�g�p�����[�^</value>
        IDictionary RequestParameterValuesDictionary { get; }
    }
}
