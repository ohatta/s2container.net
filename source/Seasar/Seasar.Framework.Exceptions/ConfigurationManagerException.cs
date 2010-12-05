#region Copyright
/*
 * Copyright 2005-2010 the Seasar Foundation and the Others.
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

namespace Seasar.Framework.Exceptions
{
    /// <summary>
    /// �A�v���P�[�V�����\���t�@�C���̃A�N�Z�X�ŃG���[��Ԃ����Ƃ��ɃX���[������O
    /// </summary>
    [Serializable]
    public class ConfigurationManagerException : SRuntimeException
    {
        private readonly string _section;
        private readonly string _key;

        public ConfigurationManagerException(string section, string key)
            : base("ESSR0005", new object[] { section, key })
        {
            _section = section;
            _key = key;
        }

        public ConfigurationManagerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _section = info.GetString("_section");
            _key = info.GetString("_key");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("_section", _section, typeof(string));
            info.AddValue("_key", _key, typeof(string));
            base.GetObjectData(info, context);
        }

        public string Section
        {
            get { return _section; }
        }

        public string Key
        {
            get { return _key; }
        }
    }
}