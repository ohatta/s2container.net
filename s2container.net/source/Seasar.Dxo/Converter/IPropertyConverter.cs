#region Copyright
/*
 * Copyright 2005-2012 the Seasar Foundation and the Others.
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

namespace Seasar.Dxo.Converter
{
    /// <summary>
    /// �v���p�e�B������^�ɕϊ����邽�߂̃R���o�[�^�C���^�t�F�[�X
    /// </summary>
    public interface IPropertyConverter
    {
        /// <summary>
        /// �I�u�W�F�N�g�̃v���p�e�B��C�ӂ̌^�ɕϊ����܂�
        /// </summary>
        /// <param name="propertyName">�v���p�e�B��</param>
        /// <param name="source">�ϊ����̃I�u�W�F�N�g</param>
        /// <param name="dest">�ϊ���̃I�u�W�F�N�g</param>
        /// <param name="expectType">�ϊ���̃I�u�W�F�N�g�Ɋ��҂���Ă���^</param>
        void Convert(string propertyName, object source, ref object dest, Type expectType);

        /// <summary>
        /// ����
        /// </summary>
        string Format { set; get; }

        /// <summary>
        /// �v���p�e�B�̃R���o�[�g���O�ɔ�������C�x���g
        /// </summary>
        event EventHandler<ConvertEventArgs> PrepareConvert;

        /// <summary>
        /// �R���o�[�g�����������ۂɔ�������C�x���g
        /// </summary>
        event EventHandler<ConvertEventArgs> ConvertCompleted;

        /// <summary>
        /// �R���o�[�g�����s�����ۂɔ�������C�x���g
        /// </summary>
        event EventHandler<ConvertEventArgs> ConvertFail;
    }
}
