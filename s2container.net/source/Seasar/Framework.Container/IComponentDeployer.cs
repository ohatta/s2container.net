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
    /// �R���|�[�l���g�f�v���C���́A �R���|�[�l���g�𗘗p�\�ȏ�Ԃɂ��Ē񋟂��邽�߂̃C���^�[�t�F�[�X�ł��B
    /// </summary>
    public interface IComponentDeployer
    {
        /// <summary>
        /// �C���X�^���X��`�ɉ����ăC���X�^���X������O���R���e�L�X�g�ւ̔z���Ȃǂ��s������ɁA
        /// ���̃R���|�[�l���g�̃C���X�^���X��Ԃ��܂��B
        /// </summary>
        /// <returns>�R���|�[�l���g�̃C���X�^���X</returns>
        /// <seealso cref="Seasar.Framework.Container.Deployer.SingletonComponentDeployer.Deploy"/>
        /// <seealso cref="Seasar.Framework.Container.Deployer.PrototypeComponentDeployer.Deploy"/>
        /// <seealso cref="Seasar.Framework.Container.Deployer.ApplicationComponentDeployer.Deploy"/>
        /// <seealso cref="Seasar.Framework.Container.Deployer.RequestComponentDeployer.Deploy"/>
        /// <seealso cref="Seasar.Framework.Container.Deployer.SessionComponentDeployer.Deploy"/>
        object Deploy();

        /// <summary>
        /// �O���R���|�[�l���g<code>outerComponent</code>�ɑ΂��A
        /// ����<see cref="IComponentDeployer">�R���|�[�l���g�f�v���C��</see>��
        /// <see cref="IComponentDef">�R���|�[�l���g��`</see>�Ɋ�Â��āA
        /// S2�R���e�i��̃R���|�[�l���g���C���W�F�N�V�������܂��B
        /// </summary>
        /// <param name="outerComponent">�O���R���|�[�l���g</param>
        /// <seealso cref="Seasar.Framework.Container.Deployer.OuterComponentDeployer.InjectDependency"/>
        void InjectDependency(object outerComponent);

        /// <summary>
        /// �R���|�[�l���g�f�v���C�������������܂��B
        /// </summary>
        /// <remarks>
        /// <para>
        /// �f�v���C����R���|�[�l���g��<see cref="IInstanceDef">�C���X�^���X��`</see>��
        /// <code>singleton</code>�̏ꍇ�ɂ́A<see cref="IAspectDef">�A�X�y�N�g</see>��K�p����
        /// �C���X�^���X�̐����A �z���A �v���p�e�B�ݒ�̌�ɁA
        /// <see cref="IInitMethodDef.InitMethod"/>���Ă΂�܂��B
        /// </para>
        /// </remarks>
        /// <seealso cref="Seasar.Framework.Container.Deployer.SingletonComponentDeployer.Init"/>
        /// <seealso cref="Seasar.Framework.Container.Assembler.DefaultInitMethodAssembler.Assemble"/>
        void Init();

        /// <summary>
        /// �R���|�[�l���g�f�v���C����j�����܂��B
        /// </summary>
        /// <remarks>
        /// <para>
        /// �f�v���C����R���|�[�l���g��<see cref="IInstanceDef">�C���X�^���X��`</see>��
        /// <code>singleton</code>�̏ꍇ�ɂ́A<see cref="IDestroyMethodDef.DestroyMethod"/>���Ă΂�܂��B
        /// </para>
        /// </remarks>
        /// <seealso cref="Seasar.Framework.Container.Deployer.SingletonComponentDeployer.Destroy"/>
        /// <seealso cref="Seasar.Framework.Container.Assembler.DefaultDestroyMethodAssembler.Assemble"/>
        void Destroy();
    }
}
