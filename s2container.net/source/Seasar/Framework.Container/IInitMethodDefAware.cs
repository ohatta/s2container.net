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
    /// ���̃C���^�[�t�F�[�X�́Ainit���\�b�h��`��o�^����ю擾���邱�Ƃ��ł���I�u�W�F�N�g��\���܂��B
    /// </summary>
    /// <remarks>
    /// <para>
    /// init���\�b�h��`�͕����o�^���邱�Ƃ��o���܂��B
    /// init���\�b�h��`�̎擾�̓C���f�b�N�X�ԍ����w�肵�čs���܂��B
    /// </para>
    /// </remarks>
    /// <seealso cref="IInitMethodDef"/>
    public interface IInitMethodDefAware
    {
        /// <summary>
        /// init���\�b�h��`��ǉ����܂��B
        /// </summary>
        /// <param name="methodDef">init���\�b�h��`</param>
        void AddInitMethodDef(IInitMethodDef methodDef);

        /// <summary>
        /// �o�^����Ă���<see cref="IInitMethodDef">init���\�b�h��`</see>�̐���Ԃ��܂��B
        /// </summary>
        /// <value>�o�^����Ă���init���\�b�h��`�̐�</value>
        int InitMethodDefSize { get; }

        /// <summary>
        /// �w�肳�ꂽ�C���f�b�N�X�ԍ�<code>index</code>��init���\�b�h��`��Ԃ��܂��B
        /// </summary>
        /// <remarks>
        /// <para>�C���f�b�N�X�ԍ��́A �o�^�������Ԃ� 0,1,2,�c �ƂȂ�܂��B</para>
        /// </remarks>
        /// <param name="index">init���\�b�h��`���w�肷��C���f�b�N�X�ԍ�</param>
        /// <returns>init���\�b�h��`</returns>
        IInitMethodDef GetInitMethodDef(int index);

    }
}
