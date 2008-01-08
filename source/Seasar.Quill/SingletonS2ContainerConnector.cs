#region Copyright
/*
 * Copyright 2005-2008 the Seasar Foundation and the Others.
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
using Seasar.Framework.Container.Factory;
using Seasar.Framework.Container;

namespace Seasar.Quill
{
    /// <summary>
    /// S2Container�ƘA�g����ׂ̐ÓI�N���X�ł�
    /// </summary>
    /// <remarks>
    /// <see cref="Seasar.Framework.Container.Factory.SingletonS2ContainerFactory"/>
    /// �ō쐬���ꂽ<see cref="Seasar.Framework.Container.IS2Container"/>�������܂�
    /// </remarks>
    public static class SingletonS2ContainerConnector
    {
        /// <summary>
        /// S2Container�̃R���|�[�l���g���R���|�[�l���g�����w�肵�Ď擾���܂�
        /// </summary>
        /// <remarks>
        /// see cref="Seasar.Framework.Container.Factory.SingletonS2ContainerFactory"/>
        /// �ō쐬���ꂽ<see cref="Seasar.Framework.Container.IS2Container"/>
        /// ����R���|�[�l���g���擾���܂�
        /// </remarks>
        /// <param name="componentName">�R���|�[�l���g��</param>
        /// <returns>�R���|�[�l���g�̃C���X�^���X</returns>
        public static object GetComponent(string componentName)
        {
            // S2Container�̃R���|�[�l���g���R���|�[�l���g�����w�肵�Ď擾���܂�
            return GetComponent(componentName, null);
        }

        /// <summary>
        /// S2Container�̃R���|�[�l���g���R���|�[�l���g�����w�肵�Ď擾���܂�
        /// </summary>
        /// <remarks>
        /// see cref="Seasar.Framework.Container.Factory.SingletonS2ContainerFactory"/>
        /// �ō쐬���ꂽ<see cref="Seasar.Framework.Container.IS2Container"/>
        /// ����R���|�[�l���g���擾���܂�
        /// </remarks>
        /// <param name="componentName">�R���|�[�l���g��</param>
        /// <param name="receiptType">�󂯑���Type</param>
        /// <returns>�R���|�[�l���g�̃C���X�^���X</returns>
        public static object GetComponent(string componentName, Type receiptType)
        {
            if (!SingletonS2ContainerFactory.HasContainer)
            {
                // S2Container���쐬����Ă��Ȃ��ꍇ�͗�O���X���[���܂�
                throw new QuillApplicationException("EQLL0009");
            }

            // S2Container���擾����
            IS2Container container = SingletonS2ContainerFactory.Container;

            if (!container.HasComponentDef(componentName))
            {
                // S2Container�ɃR���|�[�l���g���o�^����Ă��Ȃ��ꍇ�͗�O���X���[����
                throw new QuillApplicationException("EQLL0010",
                    new string[] { componentName });
            }

            try
            {
                if (receiptType == null)
                {
                    // S2Container����擾�����R���|�[�l���g��Ԃ�
                    return container.GetComponent(componentName);
                }
                else
                {
                    // �󂯑��̌^���w�肵��S2Container����擾�����R���|�[�l���g��Ԃ�
                    return container.GetComponent(receiptType, componentName);
                }
            }
            catch (Exception ex)
            {
                // �R���|�[�l���g�̎擾�ŗ�O�����������ꍇ�͗�O���X���[����
                throw new QuillApplicationException("EQLL0011", new string[] { }, ex);
            }
        }
    }
}
