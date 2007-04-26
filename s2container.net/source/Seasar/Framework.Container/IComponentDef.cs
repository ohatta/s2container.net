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
    /// S2�R���e�i���Ǘ�����R���|�[�l���g�̒�`��\���C���^�[�t�F�[�X�ł��B
    /// </summary>
    /// <remarks>
    /// <para>�R���|�[�l���g��`�́A �R���|�[�l���g�̊Ǘ��ɕK�v�Ȉȉ��̏���ێ����܂��B</para>
    /// <list type="bullet">
    /// <item>
    /// <term>���C�t�T�C�N��</term>
    /// <description>
    /// �R���|�[�l���g�̃X�R�[�v��A�����Ə��łɂ��ẮA ���̃R���|�[�l���g��
    /// <see cref="IInstanceDef">�C���X�^���X��`</see>�Őݒ肵�܂��B�����ɂ��ẮA
    /// <see cref="IExpression">�R���|�[�l���g������</see>�ɂ��w�肷�邱�Ƃ��\�ł��B
    /// </description>
    /// </item>
    /// <item>
    /// <term>�ˑ�������(Dependency Injection)</term>
    /// <description>
    /// ���̃R���|�[�l���g���ˑ����鑼�̃R���|�[�l���g��p�����[�^�́A
    /// <see cref="IArgDef">������`</see>�Ȃǂɂ��ݒ肵�܂��B
    /// </description>
    /// </item>
    /// <item>
    /// <term>�A�X�y�N�g</term>
    /// <description>
    /// ���̃R���|�[�l���g��<see cref="IAspectDef">�A�X�y�N�g��`</see>�Ȃǂɂ��ݒ肵�܂��B
    /// </description>
    /// </item>
    /// <item>
    /// <term>���^�f�[�^</term>
    /// <description>
    /// <see cref="IMetaDef">���^�f�[�^��`</see>�ɂ��A�R���|�[�l���g�ɕt������ݒ�ł��܂��B
    /// ���^�f�[�^�́A����ȃR���|�[�l���g�ł��邱�Ƃ����ʂ���ꍇ�Ȃǂɗ��p���܂��B
    /// </description>
    /// </item>
    /// </list>
    /// </remarks>
    /// <seealso cref="IArgDef"/>
    /// <seealso cref="IInterTypeDef"/>
    /// <seealso cref="IPropertyDef"/>
    /// <seealso cref="IInitMethodDef"/>
    /// <seealso cref="IDestroyMethodDef"/>
    /// <seealso cref="IAspectDef"/>
    /// <seealso cref="IMetaDef"/>
    public interface IComponentDef : IArgDefAware, IInterTypeDefAware,
        IPropertyDefAware, IInitMethodDefAware, IDestroyMethodDefAware,
        IAspectDefAware, IMetaDefAware
    {
        /// <summary>
        /// ��`�Ɋ�Â��ăR���|�[�l���g��Ԃ��܂��B
        /// </summary>
        /// <value>�R���|�[�l���g</value>
        /// <exception cref="TooManyRegistrationRuntimeException">�R���|�[�l���g��`���d�����Ă���ꍇ</exception>
        /// <exception cref="CyclicReferenceRuntimeException">�R���|�[�l���g�Ԃɏz�Q�Ƃ�����ꍇ</exception>
        /// <seealso cref="ITooManyRegistrationComponentDef"/>
        object Component { get; }

        /// <summary>
        /// �O���R���|�[�l���g<code>outerComponent</code>�ɑ΂��A
        /// <see cref="IComponentDef">�R���|�[�l���g��`</see>�Ɋ�Â��āA
        /// S2�R���e�i��̃R���|�[�l���g���C���W�F�N�V�������܂��B
        /// </summary>
        /// <param name="outerComponent">�O���R���|�[�l���g</param>
        void InjectDependency(object outerComponent);

        /// <summary>
        /// ���̃R���|�[�l���g��`���܂�S2�R���e�i���擾�E�ݒ肵�܂��B
        /// </summary>
        /// <value>S2�R���e�i</value>
        IS2Container Container { get; set; }

        /// <summary>
        /// ��`��̃N���X��Type��Ԃ��܂��B
        /// </summary>
        /// <remarks>
        /// <para>
        /// dicon�t�@�C����<code>&lt;component&gt;</code>�^�O�ɂ�����A
        /// <code>class</code>�����Ŏw�肳�ꂽ�N���X��Type��\���܂��B
        /// �����o�C���f�B���O�����ۂɂ́A���̃N���X(�C���^�[�t�F�[�X)���g�p����܂��B
        /// </para>
        /// </remarks>
        /// <value>��`��̃N���X</value>
        Type ComponentType { get; }

        /// <summary>
        /// �R���|�[�l���g�����擾�E�ݒ肵�܂��B
        /// </summary>
        /// <value>�R���|�[�l���g��</value>
        string ComponentName { get; set; }

        /// <summary>
        /// �A�X�y�N�g�K�p��́A ���ۂɃC���X�^���X�������R���|�[�l���g��Type��Ԃ��܂��B
        /// </summary>
        /// <value>���ۂ̃N���X��Type</value>
        Type ConcreteType { get; }

        /// <summary>
        /// �����o�C���f�B���O��`���擾�E�ݒ肵�܂��B
        /// </summary>
        /// <value>�����o�C���f�B���O��`</value>
        IAutoBindingDef AutoBindingDef { get; set; }

        /// <summary>
        /// �C���X�^���X��`���擾�E�ݒ肵�܂��B
        /// </summary>
        /// <value>�C���X�^���X��`</value>
        IInstanceDef InstanceDef { get; set; }

        /// <summary>
        /// �R���|�[�l���g�𐶐����鎮���擾�E�ݒ肵�܂��B
        /// </summary>
        /// <value>�R���|�[�l���g�𐶐���</value>
        IExpression Expression { get; set; }

        /// <summary>
        /// �O���o�C���f�B���O���L���ł��邩�ǂ������擾�E�ݒ肵�܂��B
        /// </summary>
        /// <remarks>
        /// <para>
        /// �O���o�C���f�B���O�Ƃ́A �O���R���e�L�X�g�ɂ���I�u�W�F�N�g���A
        /// �w�肵���R���|�[�l���g�̑Ή�����v���p�e�B�Ƀo�C���f�B���O����@�\�ł��B
        /// </para>
        /// <para>
        /// Web�A�v���P�[�V�����ɂ����āA���N�G�X�g�R���e�L�X�g�ɓ��͂��ꂽ�l���A
        /// ���N�G�X�g�C���X�^���X��ʂ��Ď擾���A���N�G�X�g��(�y�[�W��)��
        /// ���ߓI�Ɉ����p���ꍇ�Ȃǂɗ��p����܂��B
        /// </para>
        /// </remarks>
        /// <value>�O���o�C���f�B���O���L���ȏꍇ<code>true</code></value>
        bool ExternalBinding { get; set; }

        /// <summary>
        /// �R���|�[�l���g��`�����������܂��B
        /// </summary>
        /// <remarks>
        /// <para>
        /// <see cref="IInstanceDef">�R���|�[�l���g�C���X�^���X��`</see>��<code>singleton</code>�̏ꍇ�ɂ́A
        /// <see cref="IAspectDef">�A�X�y�N�g</see>��K�p�����C���X�^���X�̐����A �z���A �v���p�e�B�ݒ�̌�ɁA
        /// <see cref="IInitMethodDef">InitMethod</see>���Ă΂�܂��B
        /// </para>
        /// </remarks>
        /// <seealso cref="Seasar.Framework.Container.Deployer.SingletonComponentDeployer.Init"/>
        void Init();

        /// <summary>
        /// �R���|�[�l���g��`��j�����܂��B
        /// </summary>
        /// <remarks>
        /// <para>
        /// <see cref="IInstanceDef">�R���|�[�l���g�C���X�^���X��`</see>��
        /// <code>singleton</code>�̏ꍇ�ɂ́A<see cref="IDestroyMethodDef">DestroyMethod</see>���Ă΂�܂��B
        /// </para>
        /// </remarks>
        /// <seealso cref="Seasar.Framework.Container.Deployer.SingletonComponentDeployer.Destroy"/>
        void Destroy();
    }
}
