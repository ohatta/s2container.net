using System;
using Seasar.Framework.Container;

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
        /// <param name="target">Aspect��D�荞�ޑΏۂ̃I�u�W�F�N�g</param>
        /// <param name="componentDef">Aspect��D�荞�ޑΏۂ̃R���|�[�l���g��`</param>
        void WeaveAspect(ref object target, IComponentDef componentDef);
	}
}
