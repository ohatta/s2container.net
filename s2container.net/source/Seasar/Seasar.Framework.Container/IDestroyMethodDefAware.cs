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

namespace Seasar.Framework.Container
{
    /// <summary>
    /// IDestroyMethodDef�̐ݒ肪�\�ɂȂ�܂��B
    /// </summary>
    public interface IDestroyMethodDefAware
    {
        /// <summary>
        /// IDestroyMethodDef��ǉ����܂��B
        /// </summary>
        /// <param name="methodDef">IDestroyMethodDef</param>
        void AddDestroyMethodDef(IDestroyMethodDef methodDef);

        /// <summary>
        /// IDestroyMethodDef�̐�
        /// </summary>
        int DestroyMethodDefSize { get;}

        /// <summary>
        /// �ԍ����w�肵��IDestroyMethodDef���擾���܂��B
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        IDestroyMethodDef GetDestroyMethodDef(int index);
    }
}
