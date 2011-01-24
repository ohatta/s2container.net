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

using System.Collections;
using System.Configuration;
using System.Xml;
using Seasar.Framework.Container;
using Seasar.Framework.Util;

namespace Seasar.Framework.Xml
{
    /// <summary>
    /// S2Container.NET�̍\���Z�N�V�����n���h���N���X�ł��B
    /// </summary>
    public class S2SectionHandler : IConfigurationSectionHandler
    {
        public static S2Section GetS2Section()
        {
#if NET_1_1
            return (S2Section) ConfigurationSettings.GetConfig(
                ContainerConstants.SEASAR_CONFIG);
#else
            return (S2Section) ConfigurationManager.GetSection(
                ContainerConstants.SEASAR_CONFIG);
#endif
        }

        #region IConfigurationSectionHandler �����o

        public object Create(object parent, object configContext, XmlNode section)
        {
            return CreateS2Section(section);
        }

        #endregion

        /// <summary>
        /// �O���ݒ�t�@�C������Quill�ݒ�����擾
        /// </summary>
        /// <param name="section">XML�`���̐ݒ���</param>
        /// <returns>Quill�ݒ�</returns>
        private static S2Section CreateS2Section(XmlNode section)
        {
            S2Section S2Section = new S2Section();
            S2Section.ConfigPath = ConfigSectionUtil.GetElementValue(
                section, ContainerConstants.CONFIG_PATH_KEY);
            S2Section.Assemblys = GetAssemblyConfig(section);
            return S2Section;
        }

        #region CreateS2Section�֘A���\�b�h

        /// <summary>
        /// �A�Z���u���ݒ���̎擾
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        private static IList GetAssemblyConfig(XmlNode section)
        {
            return ConfigSectionUtil.GetListConfig(section, 
                ContainerConstants.CONFIG_ASSEMBLYS_KEY,
                ContainerConstants.CONFIG_ASSEMBLY_KEY, 
                Invoke_GetAssemblyConfig);
        }

        /// <summary>
        /// �A�Z���u���ݒ�擾�����f���Q�[�g
        /// </summary>
        /// <param name="list"></param>
        /// <param name="node"></param>
        private static void Invoke_GetAssemblyConfig(IList list, XmlNode node)
        {
            list.Add(node.InnerText);
        }

        #endregion
    }
}
