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

namespace Seasar.Framework.Exceptions
{
    /// <summary>
    /// TargetException, ArgumentException, TargetParameterCountException,
    /// MethodAccessExceptionをラップする実行時例外です。
    /// メソッド・コンストラクタ・プロパティの呼び出しに関する例外です。
    /// 呼び出される前に例外は発生します。
    /// </summary>
    [Serializable]
    public class IllegalAccessRuntimeException : SRuntimeException
    {
        private Type targetType;

        public IllegalAccessRuntimeException(Type targetType, IllegalAccessRuntimeException cause)
            : base("ESSR0042", new object[] { targetType.FullName, cause }, cause)
        {
            this.targetType = targetType;
        }

        public Type TargetType
        {
            get { return targetType; }
        }
    }
}
