using System;
using Seasar.Framework.Container;
using System.Reflection;

namespace Seasar.Framework.Aop.Impl
{
    /// <summary>
    /// Aspect��D�荞�ޏ����������ۃN���X
    /// </summary>
	public abstract class AbstractAspectWeaver : IAspectWeaver
	{
        /// <summary>
        /// Aspect��D�荞��
        /// </summary>
        /// <param name="componentDef">Aspect��D�荞�ޑΏۂ̃R���|�[�l���g��`</param>
        /// <param name="constructor">�R���X�g���N�^</param>
        /// <param name="args">�R���X�g���N�^�̈���</param>
        /// <returns>Aspect��D�荞�񂾃I�u�W�F�N�g</returns>
        public abstract object WeaveAspect(IComponentDef componentDef, 
            ConstructorInfo constructor, object[] args);

        /// <summary>
        /// �R���|�[�l���g��`�ɐݒ肳��Ă���Aspect���擾����
        /// </summary>
        /// <param name="componentDef">�R���|�[�l���g��`</param>
        /// <returns>Aspect�̔z��</returns>
        protected IAspect[] GetAspects(IComponentDef componentDef)
        {
            int size = componentDef.AspectDefSize;
            IAspect[] aspects = new IAspect[size];
            for (int i = 0; i < size; ++i)
            {
                aspects[i] = componentDef.GetAspectDef(i).Aspect;
            }
            return aspects;
        }

	}
}
