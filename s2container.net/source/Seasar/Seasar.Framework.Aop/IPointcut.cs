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

using System.Reflection;

namespace Seasar.Framework.Aop
{
    /// <summary>
    /// Advice(Interceptor)��}�����邩��`���܂�
    /// </summary>
    /// <remarks>
    /// <p>���̃C���^�[�t�F�C�X�͈ꕔAOP�A���C�A���X�����B<p>
    /// <p>�����t���N�V������p����IsApplied���\�b�h��AOP�A���C�A���X�ɔ񏀋��ł��B</p>
    /// </remarks>
    /// <seealso href="http://aopalliance.sourceforge.net/doc/index.html">AOP Alliance</seealso>
    public interface IPointcut
    {
        /// <summary>
        /// �����œn���ꂽmethod��Advice��}�����邩�m�F���܂�
        /// </summary>
        /// <param name="method">MethodBase ���\�b�h�ƃR���X�g���N�^�Ɋւ�����������Ă��܂�</param>
        /// <returns>True�Ȃ�Advice��}������AFalse�Ȃ�Advice�͑}������Ȃ�</returns>
        bool IsApplied(MethodBase method);

        /// <summary>
        /// �����œn���ꂽ���\�b�h����Advice��}�����邩�m�F���܂�
        /// </summary>
        /// <param name="methodName">���\�b�h��</param>
        /// <returns>True�Ȃ�Advice��}������AFalse�Ȃ�Advice�͑}������Ȃ�</returns>
        bool IsApplied(string methodName);
    }
}
