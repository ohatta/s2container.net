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
using System.Runtime.Serialization;
using System.Text;
using Seasar.Framework.Exceptions;

namespace Seasar.Framework.Container
{
    [Serializable]
    public sealed class TooManyRegistrationRuntimeException : SRuntimeException
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="key"></param>
        /// <param name="componentTypes"></param>
        public TooManyRegistrationRuntimeException(object key, Type[] componentTypes)
            : base("ESSR0045", new[] { key, GetTypeNames(componentTypes) })
        {
            Key = key;
            ComponentTypes = componentTypes;
        }

        public TooManyRegistrationRuntimeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Key = info.GetValue("_key", typeof(object));
            ComponentTypes = info.GetValue("_componentTypes", typeof(Type[])) as Type[];
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("_key", Key, typeof(object));
            info.AddValue("_componentTypes", ComponentTypes, typeof(Type[]));
            base.GetObjectData(info, context);
        }

        public object Key { get; }

        public Type[] ComponentTypes { get; }

        public static string GetTypeNames(Type[] componentTypes)
        {
            var buf = new StringBuilder(255);
            foreach (var componentType in componentTypes)
            {
                buf.Append(componentType.FullName);
                buf.Append(", ");
            }
            buf.Length -= 2;
            return buf.ToString();
        }
    }
}
