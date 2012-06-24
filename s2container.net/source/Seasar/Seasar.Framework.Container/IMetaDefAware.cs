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

namespace Seasar.Framework.Container
{
    /// <summary>
    /// IMetaDef�̐ݒ肪�\�ɂȂ�܂��B
    /// </summary>
    public interface IMetaDefAware
    {
        /// <summary>
        /// IMetaDef��ǉ����܂��B
        /// </summary>
        /// <param name="metaDef">IMetaDef</param>
        void AddMetaDef(IMetaDef metaDef);

        /// <summary>
        /// IMetaDef�̐�
        /// </summary>
        int MetaDefSize { get;}

        /// <summary>
        /// �ԍ����w�肵��IMetaDef���擾���܂��B
        /// </summary>
        /// <param name="index">IMetaDef�̔ԍ�</param>
        /// <returns>IMetaDef</returns>
        IMetaDef GetMetaDef(int index);

        /// <summary>
        /// ���O���w�肵��IMetaDef���擾���܂��B
        /// </summary>
        /// <param name="name">IMetaDef�̖��O</param>
        /// <returns>IMetaDef</returns>
        IMetaDef GetMetaDef(string name);

        /// <summary>
        /// ���O���w�肵��IMetaDef�̔z����擾���܂��B
        /// </summary>
        /// <param name="name">IMetaDef�̖��O</param>
        /// <returns>IMetaDef�̔z��</returns>
        IMetaDef[] GetMetaDefs(string name);
    }
}
