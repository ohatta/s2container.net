﻿#region Copyright
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

#if NET_4_0
namespace Seasar.Unit.Core
{
    /// <summary>
    /// 同名の属性と同等の動きをするメソッド名定義クラス
    /// </summary>
    public class S2SpecialMethodNameConst
    {
        public const string SET_UP = "SetUp";
        public const string TEAR_DOWN = "TearDown";
    }
}
#endif
