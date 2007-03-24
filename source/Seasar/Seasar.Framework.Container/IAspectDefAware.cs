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

namespace Seasar.Framework.Container
{
    /// <summary>
    /// IAspectDef�̐ݒ肪�\�ɂȂ�܂��B
    /// </summary>
    public interface IAspectDefAware
    {
        /// <summary>
        /// IAspectDef��ǉ����܂��B
        /// </summary>
        /// <param name="aspectDef">IAspectDef</param>
        void AddAspeceDef(IAspectDef aspectDef);

        /// <summary>
        /// IAspectDef�̐�
        /// </summary>
        int AspectDefSize { get; }

        /// <summary>
        /// �ԍ����w�肵��IAspectDef���擾���܂��B
        /// </summary>
        /// <param name="index">IAspectDef�̔ԍ�</param>
        /// <returns>IAspectDef</returns>
        IAspectDef GetAspectDef(int index);
    }
}
