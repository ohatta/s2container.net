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

namespace Seasar.Framework.Util
{
    public sealed class StringUtil
    {
        private StringUtil()
        {
        }

        public static bool IsEmpty(string text)
        {
            return text == null || text.Length == 0 ? true : false;
        }

        public static string Decapitalize(string name)
        {
            if (IsEmpty(name))
            {
                return name;
            }
            char[] chars = name.ToCharArray();
            chars[0] = char.ToLower(chars[0]);
            return new string(chars);
        }

        public static bool StartWith(string text, string fragment)
        {
            if (text == null | fragment == null)
            {
                return false;
            }
            return text.Length > fragment.Length
                && text.Substring(0, fragment.Length).ToLower()
                .Equals(fragment.ToLower());
        }
    }
}
