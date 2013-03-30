#region Copyright
/*
 * Copyright 2005-2013 the Seasar Foundation and the Others.
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

namespace Seasar.Framework.Container.Impl
{
    /// <summary>
    /// 1つのキーに複数のコンポーネントが登録された場合に利用されます。
    /// </summary>
    public class TooManyRegistrationComponentDef : SimpleComponentDef
    {
        private readonly object _key;
        private readonly ArrayList _componentTypes = new ArrayList();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="key">キー</param>
        public TooManyRegistrationComponentDef(object key)
        {
            _key = key;
        }

        /// <summary>
        /// コンポーネントのTypeを追加します。
        /// </summary>
        /// <param name="componentType">コンポーネントのType</param>
        public void AddComponentType(Type componentType)
        {
            _componentTypes.Add(componentType);
        }

        public override object GetComponent()
        {
            throw new TooManyRegistrationRuntimeException(
                _key, (Type[]) _componentTypes.ToArray(typeof(Type)));
        }

        public override object GetComponent(Type receiveType)
        {
            throw new TooManyRegistrationRuntimeException(
                _key, (Type[]) _componentTypes.ToArray(typeof(Type)));
        }
    }
}
