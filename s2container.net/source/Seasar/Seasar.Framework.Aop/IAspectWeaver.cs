using System;
using Seasar.Framework.Container;
using System.Reflection;

namespace Seasar.Framework.Aop
{
    /// <summary>
    /// Aspect��D�荞�ޏ���������Interface
    /// </summary>
	public interface IAspectWeaver
	{
        /// <summary>
        /// Aspect��D�荞��
        /// </summary>
        /// <param name="componentDef">Aspect��D�荞�ޑΏۂ̃R���|�[�l���g��`</param>
        /// <param name="constructor">�R���X�g���N�^</param>
        /// <param name="args">�R���X�g���N�^�̈���</param>
        /// <returns>Aspect��D�荞�񂾃I�u�W�F�N�g</returns>
        object WeaveAspect(IComponentDef componentDef, ConstructorInfo constructor, object[] args);
	}
}
