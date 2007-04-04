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
    /// ���̃C���^�[�t�F�[�X�́A���^�f�[�^��`��ǉ�����ю擾���邱�Ƃ̂ł���I�u�W�F�N�g��\���܂��B 
    /// </summary>
    /// <remarks>
    /// <para>���^�f�[�^��`�͕����ǉ����邱�Ƃ��ł��܂��B ���^�f�[�^��`�̎擾�́A
    /// ���^�f�[�^��`���܂��̓C���f�b�N�X�ԍ����w�肵�čs���܂��B</para>
    /// </remarks>
    public interface IMetaDefAware
    {
        /// <summary>
        /// ���^�f�[�^��`��ǉ����܂��B
        /// </summary>
        /// <param name="metaDef">���^�f�[�^��`</param>
        void AddMetaDef(IMetaDef metaDef);

        /// <summary>
        /// ���^�f�[�^��`�̐���Ԃ��܂��B
        /// </summary>
        /// <returns>���^�f�[�^��`�̐�</returns>
        int MetaDefSize{ get; }

        /// <summary>
        /// �C���f�b�N�X�ԍ�<code>index</code>�Ŏw�肳�ꂽ���^�f�[�^��`��Ԃ��܂��B
        /// </summary>
        /// <remarks>
        /// <para>�C���f�b�N�X�ԍ��́A�ǉ���������0, 1, 2�c�ƂȂ�܂��B</para>
        /// </remarks>
        /// <param name="index">���^�f�[�^��`���w�肷��C���f�b�N�X�ԍ�</param>
        /// <returns>���^�f�[�^��`</returns>
        IMetaDef GetMetaDef(int index);

        /// <summary>
        /// �w�肵�����^�f�[�^��`���œo�^����Ă��郁�^�f�[�^��`���擾���܂��B
        /// </summary>
        /// <remarks>
        /// <para>���^�f�[�^��`���o�^����Ă��Ȃ��ꍇ�A<code>null</code>��Ԃ��܂��B</para>
        /// </remarks>
        /// <param name="name">���^�f�[�^��`��</param>
        /// <returns>���^�f�[�^��`</returns>
        IMetaDef GetMetaDef(string name);


        /// <summary>
        /// �w�肵�����^�f�[�^��`���œo�^����Ă��郁�^�f�[�^��`���擾���܂��B
        /// </summary>
        /// <remarks>
        /// <para>���^�f�[�^��`���o�^����Ă��Ȃ��ꍇ�A�v�f��0�̔z���Ԃ��܂��B</para>
        /// </remarks>
        /// <param name="name">���^�f�[�^��`��</param>
        /// <returns>���^�f�[�^��`���i�[�����z��</returns>
        IMetaDef[] GetMetaDefs(string name);
    }
}
