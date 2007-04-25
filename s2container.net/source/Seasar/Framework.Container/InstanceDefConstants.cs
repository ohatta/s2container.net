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
    /// �C���X�^���X��`�Ŏg�p�����萔���`����N���X�ł��B
    /// </summary>
    public class InstanceDefConstants
    {
        /// <summary>
        /// �C���X�^���X��`�u<code>singleton</code>�v��\���萔�ł��B
        /// </summary>
        public const string SINGLETON_NAME = "singleton";

        /// <summary>
        /// �C���X�^���X��`�u<code>prototype</code>�v��\���萔�ł��B
        /// </summary>
        public const string PROTOTYPE_NAME = "prototype";

        /// <summary>
        /// �C���X�^���X��`�u<code>application</code>�v��\���萔�ł��B
        /// </summary>
        public const string APPLICATION_NAME = "application";

        /// <summary>
        /// �C���X�^���X��`�u<code>request</code>�v��\���萔�ł��B
        /// </summary>
        public const string REQUEST_NAME = "request";

        /// <summary>
        /// �C���X�^���X��`�u<code>session</code>�v��\���萔�ł��B
        /// </summary>
        public const string SESSION_NAME = "session";

        /// <summary>
        /// �C���X�^���X��`�u<code>outer</code>�v��\���萔�ł��B
        /// </summary>
        public const string OUTER_NAME = "outer";
    }
}
