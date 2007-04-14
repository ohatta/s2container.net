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
    /// バインディングタイプ定義で使用される定数を定義するクラスです。
    /// </summary>
    public class BindingTypeDefConstants
    {
        /// <summary>
        /// バインディングタイプ定義名「<code>must</code>」を表す定数です。
        /// </summary>
        public const string MUST_NAME = "must";

        /// <summary>
        /// バインディングタイプ定義名「<code>should</code>」を表す定数です。
        /// </summary>
        public const string SHOULD_NAME = "should";

        /// <summary>
        /// バインディングタイプ定義名「<code>may</code>」を表す定数です。
        /// </summary>
        public const string MAY_NAME = "may";

        /// <summary>
        /// バインディングタイプ定義名「<code>none</code>」を表す定数です。
        /// </summary>
        public const string NONE_NAME = "none";
    }
}
