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
using System.Reflection;
using Seasar.Quill.Attrs;
using Seasar.Quill.Util;

namespace Seasar.Quill.Unit
{
    /// <summary>
    /// Mock��Inject���邽�߂̃N���X
    /// </summary>
    public class MockInjector : QuillInjector
    {
        // MockInjector�̃C���X�^���X
        private static MockInjector mockInjector;

        /// <summary>
        /// MockInjector������������R���X�g���N�^
        /// </summary>
        /// <remarks>
        /// <see cref="GetInstance"/>����C���X�^���X�𐶐�����
        /// </remarks>
        /// <seealso cref="Seasar.Quill.Util.MockInjector.GetInstance"/>
        protected MockInjector() : base()
        {
        }

        /// <summary>
        /// MockInjector�̃C���X�^���X���擾����
        /// </summary>
        /// <remarks>
        /// <para>
        /// MockInjector�̃R���X�g���N�^�̃A�N�Z�X�C���q��protected�ɐݒ肳��Ă���ׁA
        /// ����MockInjector�̃C���X�^���X�𐶐����邱�Ƃ͂ł��Ȃ��B
        /// </para>
        /// <para>
        /// MockInjector�̃C���X�^���X���擾����ꍇ�͓����\�b�h���g�p����B
        /// </para>
        /// <para>
        /// ��{�I�ɓ����C���X�^���X��Ԃ����ADestroy���\�b�h�ɂ����Quill������
        /// �Q�Ƃ��j������Ă���ꍇ�͐V����MockInjector�̃C���X�^���X���쐬����B
        /// </para>
        /// </remarks>
        /// <returns>QuillInjector�̃C���X�^���X</returns>
        public new static MockInjector GetInstance()
        {
            if (mockInjector == null)
            {
                mockInjector = new MockInjector();
            }

            // MockInjector�̃C���X�^���X��Ԃ�
            return mockInjector;
        }

        /// <summary>
        /// QuillInjector�����Q�Ƃ�j������
        /// </summary>
        public override void Destroy()
        {
            if (container == null)
            {
                return;
            }

            // QuillContainer�����Q�Ƃ�j������
            container.Destroy();

            container = null;
            mockInjector = null;
        }

        /// <summary>
        /// �w�肷��t�B�[���h��DI����B
        /// </summary>
        /// <remarks>Mock�������w�肳��Ă���ꍇ��Mock�����Ŏw�肳��Ă���Mock�N���X��
        /// �D�悵��Inject����B</remarks>
        /// <param name="target">DI���s����I�u�W�F�N�g</param>
        /// <param name="field">DI���s����t�B�[���h���</param>
        protected override void InjectField(object target, FieldInfo field)
        {
            // �t�B�[���h�̌^�ɐݒ肳��Ă���Mock�������擾����
            MockAttribute mockAttr = AttributeUtil.GetMockAttr(field.FieldType);

            if (mockAttr != null)
            {
                // Mock��Inject����
                InjectField(target, field, mockAttr.MockType);
            }
            else
            {
                // Mock�������w�肳��Ă��Ȃ��ꍇ�͒ʏ�̏������s��
                base.InjectField(target, field);
            }
        }
    }
}
