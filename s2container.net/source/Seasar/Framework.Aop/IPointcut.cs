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

namespace Seasar.Framework.Aop
{
    /// <summary>
    /// �|�C���g�J�b�g
    /// </summary>
    /// <remarks>
    /// <para>�ǂ���Joinpoint��ݒ肷��̂����`���܂��B</para>
    /// </remarks>
    public interface IPointcut
    {
        /// <summary>
        /// ���\�b�h��Advice(MethodInterceptor)��}�����邩�m�F���܂�
        /// </summary>
        /// <param name="method">���\�b�h�ƃR���X�g���N�^�Ɋւ�����</param>
        /// <returns>
        /// Advice��}������ꍇ�A<code>true</code>�A
        /// �����łȂ��ꍇ��<code>false</code>��Ԃ��܂��B
        /// </returns>
        bool IsApplied(MethodBase method);
    }
}
