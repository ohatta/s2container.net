#region Copyright
/*
 * Copyright 2005-2009 the Seasar Foundation and the Others.
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

namespace Seasar.Framework.Aop
{
    /// <summary>
    /// IMethodInvocation�C���^�[�t�F�C�X��S2�Ǝ��̏���ǉ���������
    /// </summary>
    public interface IS2MethodInvocation : IMethodInvocation
    {
        /// <summary>
        /// ���\�b�h��������N���X�̌^���
        /// </summary>
        Type TargetType { get; }

        /// <summary>
        /// ���\�b�h�Ƃ��̃N���X�̃C���X�^���X��������S2�R���e�i�Ɋւ�����
        /// </summary>
        /// <param name="name">S2�R���e�i�̏��̖��O</param>
        object GetParameter(string name);
    }
}
