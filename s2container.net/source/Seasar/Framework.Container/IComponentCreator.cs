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
    /// SMART deploy�ɂ����āA�����o�^�����R���|�[�l���g��<see cref="IComponentDef">�R���|�[�l���g��`</see>
    /// ���쐬���邽�߂̃C���^�[�t�F�[�X�ł��B
    /// </summary>
    /// <remarks>
    /// <para>
    /// �R���|�[�l���g��`��<see cref="Seasar.Framework.Convention.NamingConvention">�����K��</see>
    /// �Ɋ�Â��č쐬����A<see cref="IComponentCustomizer">�R���|�[�l���g��`�J�X�^�}�C�U</see>
    /// �ɂ���ăA�X�y�N�g��`�̒ǉ��Ȃǂ̃J�X�^�}�C�Y���{���Ă���ԋp����܂��B 
    /// </para>
    /// </remarks>
    public interface IComponentCreator
    {
        /// <summary>
        /// �w�肳�ꂽ�N���X����A<see cref="Seasar.Framework.Convention.NamingConvention">�����K��</see>
        /// �ɏ]���ăR���|�[�l���g��`���쐬���܂��B
        /// </summary>
        /// <param name="componentType">�R���|�[�l���g��`���쐬����Ώۂ̃N���X</param>
        /// <returns>
        /// �쐬���ꂽ�R���|�[�l���g��`�B �w�肳�ꂽ�N���X������Creator�̑ΏۂłȂ������ꍇ�́A
        /// <code>null</code>��Ԃ�
        /// </returns>
        IComponentDef CreateComponentDef(Type componentType);

        /// <summary>
        /// �w�肳�ꂽ�R���|�[�l���g������<see cref="Seasar.Framework.Convention.NamingConvention">�����K��</see>
        /// �ɏ]���ăR���|�[�l���g��`���쐬���܂��B
        /// </summary>
        /// <param name="componentName">�R���|�[�l���g��`���쐬����Ώۂ̃R���|�[�l���g��</param>
        /// <returns>
        /// �쐬���ꂽ�R���|�[�l���g��`�B �w�肳�ꂽ�N���X������Creator�̑ΏۂłȂ������ꍇ�A
        /// �܂��̓R���|�[�l���g���ɑΉ�����N���X�����݂��Ȃ������ꍇ�́A <code>null</code>��Ԃ�
        /// </returns>
        /// <exception cref="Seasar.Framework.Exceptions.EmptyRuntimeException">
        /// �R���|�[�l���g����<code>null</code>�܂��͋󕶎�����w�肵���ꍇ
        /// </exception>
        /// <seealso cref="Seasar.Framework.Convention.INamingConvention.FromComponentNameToType"/>
        IComponentDef CreateComponentDef(string componentName);
    }
}
