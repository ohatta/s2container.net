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
    /// ���̃C���^�[�t�F�[�X�́A destroy���\�b�h��`��ǉ�����ю擾���邱�Ƃ��ł���I�u�W�F�N�g��\���܂��B
    /// </summary>
    /// <remarks>
    /// <para>
    /// destroy���\�b�h��`�͕����ǉ����邱�Ƃ��o���܂��B 
    /// destroy���\�b�h��`�̎擾�̓C���f�b�N�X�ԍ����w�肵�čs���܂��B
    /// </para>
    /// </remarks>
    /// <seealso cref="IDestroyMethodDef"/>
    public interface IDestroyMethodDefAware
    {
        /// <summary>
        /// destroy���\�b�h��`��ǉ����܂��B
        /// </summary>
        /// <param name="methodDef">destroy���\�b�h��`</param>
        void AddDestroyMethodDef(IDestroyMethodDef methodDef);

        /// <summary>
        /// <see cref="IDestroyMethodDef">destroy���\�b�h��`</see>�̐���Ԃ��܂��B
        /// </summary>
        /// <value>destroy���\�b�h��`�̐�</value>
        int DestroyMethodDefSize { get; }

        /// <summary>
        /// �w�肳�ꂽ�C���f�b�N�X�ԍ�<code>index</code>��destroy���\�b�h��`��Ԃ��܂��B
        /// </summary>
        /// <remarks>
        /// <para>
        /// �C���f�b�N�X�ԍ��́A �ǉ��������Ԃ� 0,1,2,�c �ƂȂ�܂��B
        /// </para>
        /// </remarks>
        /// <param name="index">destroy���\�b�h��`���w�肷��C���f�b�N�X�ԍ�</param>
        /// <returns>destroy���\�b�h��`</returns>
        IDestroyMethodDef GetDestroyMethodDef(int index);
    }
}
