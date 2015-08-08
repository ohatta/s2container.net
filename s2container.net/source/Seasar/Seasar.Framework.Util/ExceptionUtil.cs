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
using System.Reflection;

namespace Seasar.Framework.Util
{
    /// <summary>
    /// ��O�Ɋւ���Util�N���X
    /// </summary>
    public sealed class ExceptionUtil
    {
        private ExceptionUtil()
        {
        }

        /// <summary>
        /// StackTrace��ۑ�����
        /// </summary>
        /// <param name="ex">��O</param>
        /// <remarks>
        /// Exception��private field�ł���_remoteStackTraceString��
        /// InnerException��StackTrace��ۑ�����
        /// </remarks>
        public static void SaveStackTraceToRemoteStackTraceString(Exception ex)
        {
            // _remoteStackTraceString��FieldInfo
            var remoteStackTraceString =
                typeof(Exception).GetField("_remoteStackTraceString", BindingFlags.Instance | BindingFlags.NonPublic);

            // _remoteStackTraceString��InnerException��StackTrace���Z�b�g����
            remoteStackTraceString?.SetValue(ex, ex.StackTrace + Environment.NewLine);
        }
    }
}
