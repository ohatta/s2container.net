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

using System;
using Seasar.Framework.Container.AutoRegister;

namespace Seasar.Windows
{
    /// <summary>
    /// �N��WindowsForm�w��p�N���X
    /// </summary>
    public class DefaultFormNaming : IAutoNaming
    {
        /// <summary>
        /// �N��WindowsForm
        /// </summary>
        public string MainFormName { get; set; }

        /// <summary>
        /// WindowsForm�w�胉�x��
        /// </summary>
        public string Label { get; set; } = "MainForm";

        #region IAutoNaming Members

        /// <summary>
        /// �R���|�[�l���g�����`���܂��B
        /// </summary>
        /// <param name="type">�R���|�[�l���g�����`������Type</param>
        /// <returns>�R���|�[�l���g��</returns>
        public string DefineName(Type type)
        {
            var name = type.Name;
            if (name == MainFormName)
            {
                return !String.IsNullOrEmpty(Label) ? Label : name;
            }
            else
            {
                return name;
            }
        }

        #endregion
    }
}
