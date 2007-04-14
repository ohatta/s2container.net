using System;

namespace Seasar.Framework.Container
{
    /// <summary>
    /// ���̃C���^�[�t�F�[�X�́A �R���|�[�l���g�̏�Ԃɑ΂���A�N�Z�X�^�C�v��`��\���܂��B
    /// </summary>
    /// <remarks>
    /// <para>
    /// �^����ꂽ�R���|�[�l���g�ɑ΂��A �A�N�Z�X�^�C�v��`�Ɋ�Â��āA
    /// S2�R���e�i��̃R���|�[�l���g���C���W�F�N�V��������@�\���񋟂��܂��B
    /// </para>
    /// <para>�A�N�Z�X�^�C�v��`�ɂ́A �ȉ��̂��̂�����܂��B</para>
    /// <list type="bullet">
    /// <item>
    /// <term><see cref="Seasar.Framework.Container.Assembler.IAccessTypePropertyDef">property</see></term>
    /// <description>�v���p�e�B�ɂ��A�N�Z�X��\���܂��B</description>
    /// </item>
    /// <item>
    /// <term><see cref="Seasar.Framework.Container.Assembler.IAccessTypeFieldDef">field</see></term>
    /// <description>�t�B�[���h�ւ̒��ڃA�N�Z�X��\���܂��B</description>
    /// </item>
    /// </list>
    /// <para>
    /// �A�N�Z�X�^�C�v��`�́A
    /// <see cref="Seasar.Framework.Container.Assembler.IAccessTypeDefFactory">�t�@�N�g��</see>
    /// �o�R�Ŏ擾���܂��B
    /// </para>
    /// <remarks>
    public interface IAccessTypeDef
    {
        /// <summary>
        /// �A�N�Z�X�^�C�v��`����Ԃ��܂��B
        /// </summary>
        /// <value>�A�N�Z�X�^�C�v��`��</value>
        /// <seealso cref="AccessTypeDefConstants.PROPERTY_NAME"/>
        /// <seealso cref="AccessTypeDefConstants.FIELD_NAME"/>
        string Name { get; }

        /// <summary>
        /// �A�N�Z�X�^�C�v��`�Ɋ�Â��āA <code>component</code>�̃v���p�e�B
        /// �܂��̓t�B�[���h��S2�R���e�i��̃R���|�[�l���g���C���W�F�N�V�������܂��B
        /// </summary>
        /// <param name="componentDef">�R���|�[�l���g��`</param>
        /// <param name="propertyDef">�v���p�e�B��`</param>
        /// <param name="component">�R���|�[�l���g</param>
        void Bind(IComponentDef componentDef, IPropertyDef propertyDef,
            object component);

        /// <summary>
        /// �A�N�Z�X�^�C�v��`�Ɋ�Â��āA <code>component</code>�̃v���p�e�B
        /// �܂��̓t�B�[���h��S2�R���e�i��̃R���|�[�l���g���C���W�F�N�V�������܂��B
        /// </summary>
        /// <param name="componentDef">�R���|�[�l���g��`</param>
        /// <param name="propertyDef">�v���p�e�B��`</param>
        /// <param name="bindingTypeDef">�o�C���f�B���O�^�C�v��`</param>
        /// <param name="component">�R���|�[�l���g</param>
        void Bind(IComponentDef componentDef, IPropertyDef propertyDef,
            IBindingTypeDef bindingTypeDef, object component);
    }
}
