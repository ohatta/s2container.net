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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace Seasar.Windows.Seasar.Windows.Utils
{
    /// <summary>
    /// GridView�R���g���[���Ƀo�C���h��������N���X
    /// </summary>
    public sealed class BindingUtilFactory
    {
        private static volatile BindingUtilFactory _factory;
        private static object _lockRoot = new object();

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        private BindingUtilFactory()
        {
            ;
        }

        /// <summary>
        /// �C���X�^���X
        /// </summary>
        public static BindingUtilFactory Factory
        {
            get
            {
                if (_factory == null)
                {
                    lock (_lockRoot)
                    {
                        if (_factory == null)
                            _factory = new BindingUtilFactory();
                    }
                }
                return _factory;
            }
        }

        /// <summary>
        /// GridView�R���g���[���Ƀo�C���h����N���X�𐶐�����
        /// </summary>
        /// <param name="propertyType">�v���p�e�BType</param>
        /// <returns>�o�C���h����N���X</returns>
        public IBindingUtil Create(Type propertyType)
        {
            if (propertyType == typeof (IList))
                return (new BindingListUtil());
            else if (propertyType.Name == typeof (IList<>).Name)
                return (new BindingGenericListUtil());
            else if (propertyType.Name == typeof (IBindingList).Name)
                return (new BindingBindingListUtil());
            else if (propertyType.Name == typeof (IEnumerable).Name)
                return (new BindingArrayUtil());
            else if (propertyType.Name == typeof (IEnumerable<>).Name)
                return (new BindingArrayUtil());
            else if (propertyType.IsArray)
                return (new BindingArrayUtil());
            else if (propertyType == typeof (DataTable))
                return (new BindingDataTableUtil());
            else
                throw new InvalidCastException(String.Format(SWFMessages.FSWF0006, propertyType.Name));
        }
    }
}