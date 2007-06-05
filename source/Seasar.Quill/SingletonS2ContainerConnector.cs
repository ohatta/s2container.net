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
                // S2Container����擾�����R���|�[�l���g��Ԃ�
                return container.GetComponent(componentName);
            }
            catch(Exception ex)
            {
                // �R���|�[�l���g�̎擾�ŗ�O�����������ꍇ�͗�O���X���[����
                throw new QuillApplicationException("EQLL0011", new string[] { }, ex);
            }
        }
    }
}
