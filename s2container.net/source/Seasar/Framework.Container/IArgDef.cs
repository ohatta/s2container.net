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
    /// �R���|�[�l���g�̃R���X�g���N�^����у��\�b�h�ɗ^������
    /// ������`�̂��߂̃C���^�[�t�F�[�X�ł��B
    /// </summary>
    public interface IArgDef : IMetaDefAware
    {
        /// <summary>
        /// ������`�̒l
        /// </summary>
        /// <remarks>
        /// <para>
        /// ������`�̒l�Ƃ́Adicon�t�@�C���ɋL�q����<code>&lt;arg&gt;</code>�v�f�̓��e�ł��B
        /// �C���W�F�N�V��������ۂɁA�R���X�g���N�^�⏉�������\�b�h���̈����l�ɂȂ�܂��B
        /// </para>
        /// </remarks>
        object Value { get; set; }

        /// <summary>
        /// ������]������R���e�L�X�g�ƂȂ�S2�R���e�i
        /// </summary>
        IS2Container Container { get; set; }

        /// <summary>
        /// ������`�̒l�ƂȂ鎮
        /// </summary>
        IExpression Expression { get; set; }

        /// <summary>
        /// ������`�̒l�ƂȂ鎮�A������`�̒l�A������`�̒l�ƂȂ�R���|�[�l���g��`�̂����ꂩ�����݂��A
        /// �l�̎擾���\���ǂ���
        /// </summary>
        /// <remarks>
        /// <para>
        /// �l�̎擾���\�ȏꍇ�A<code>true</code>�A
        /// �����łȂ��ꍇ��<code>false</code>��Ԃ��܂��B
        /// </para>
        /// </remarks>
        bool ValueGettable { get; }

        /// <summary>
        /// ������`�̒l�ƂȂ�R���|�[�l���g��`
        /// </summary>
        IComponentDef ChildComponentDef { set; }
    }
}
