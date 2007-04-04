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
    /// �O���R���e�L�X�g���񋟂���R���|�[�l���g��`���A S2�R���e�i�ɓo�^���܂��B
    /// </summary>
    /// <remarks>
    /// <para>
    /// <code>IExternalContextComponentDefRegister</code>���O���R���e�L�X�g��
    /// <see cref="IComponentDef">�R���|�[�l���g��`</see>��o�^���邱�Ƃɂ��A
    /// <see cref="IExternalContext"/>�C���^�[�t�F�[�X��ʂ��āA 
    /// �O���R���e�L�X�g�̃R���|�[�l���g���擾�ł���悤�ɂȂ�܂��B
    /// </para>
    /// <para>
    /// �R���|�[�l���g���擾�\�ȊO���R���e�L�X�g�̎�ނɂ��ẮA
    /// <see cref="IExternalContext"/>�C���^�[�t�F�[�X���Q�Ƃ��ĉ������B
    /// </para>
    /// </remarks>
    public interface IExternalContextComponentDefRegister
    {
        /// <summary>
        /// �w�肳�ꂽS2�R���e�i�ɁA �O���R���e�L�X�g�̃R���|�[�l���g��`��o�^���܂��B
        /// </summary>
        /// <param name="container">S2�R���e�i</param>
        void RegisterComponentDefs(IS2Container container);
    }
}
