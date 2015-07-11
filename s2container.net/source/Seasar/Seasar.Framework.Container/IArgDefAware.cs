#region Copyright
/*
 * Copyright 2005-2015 the Seasar Foundation and the Others.
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

namespace Seasar.Framework.Container
{
    /// <summary>
    /// IArgDef�̐ݒ肪�\�ɂȂ�܂��B
    /// </summary>
    public interface IArgDefAware
    {
        /// <summary>
        /// IArgDef��ǉ����܂�
        /// </summary>
        /// <param name="argDef">IArgDef</param>
        void AddArgDef(IArgDef argDef);

        /// <summary>
        /// ArgDef�̐�
        /// </summary>
        int ArgDefSize { get; }

        /// <summary>
        /// �ԍ����w�肵��IArgDef���擾���܂�
        /// </summary>
        /// <param name="index">IArgDef�̔ԍ�</param>
        /// <returns>IArgDef</returns>
        IArgDef GetArgDef(int index);
    }
}
