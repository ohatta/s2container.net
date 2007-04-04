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
    /// DI��AOP���T�|�[�g����S2�R���e�i�̃C���^�[�t�F�[�X�ł��B
    /// </summary>
    /// <remarks>
    /// <para>S2Container�̖����ɂ���</para>
    /// <para>
    /// �R���|�[�l���g�̊Ǘ����s���@�\��񋟂��܂��B 
    /// �R���|�[�l���g�Ƃ͂P���܂�����ȏ�̃N���X�ō\�������Java�I�u�W�F�N�g�ł��B
    /// S2�R���e�i�̓R���|�[�l���g�̐����A�R���|�[�l���g�̏������A�R���|�[�l���g�̎擾��񋟂��܂��B
    /// �R���|�[�l���g���擾����L�[�ɂ́A�R���|�[�l���g���A�R���|�[�l���g�̃N���X�A
    /// �܂��̓R���|�[�l���g����������C���^�[�t�F�[�X���w�肷�邱�Ƃ��ł��܂��B
    /// </para>
    /// <para>
    /// S2�R���e�i�̃C���X�^���X�K�w�ɂ���
    /// </para>
    /// <para>
    /// S2�R���e�i�S�͕̂����̃R���e�i�ɂ��K�w������Ă��܂��B
    /// ��̃R���e�i�͕����̃R���e�i���C���N���[�h���邱�Ƃ��ł��܂��B
    /// �����̃R���e�i������̃R���e�i���C���N���[�h���邱�Ƃ��ł��܂��B
    /// </para>
    /// <para>
    /// S2�R���e�i�̃C���W�F�N�V�����̎�ނɂ���
    /// </para>
    /// <para>
    /// S2�R���e�i��3��ނ̃C���W�F�N�V�������T�|�[�g���܂��B
    /// <list type="bullet">
    /// <item>
    /// <term><see cref="IConstructorAssembler">�R���X�g���N�^�E�C���W�F�N�V����</see></term>
    /// <description>�R���X�g���N�^�����𗘗p���ăR���|�[�l���g���Z�b�g���܂��B</description>
    /// </item>
    /// <item>
    /// <term><see cref="IPropertyAssembler">�v���p�e�B�E�C���W�F�N�V����</see></term>
    /// <description>�v���p�e�B�𗘗p���ăR���|�[�l���g���Z�b�g���܂��B </description>
    /// </item>
    /// <item>
    /// <term><see cref="IMethodAssembler">���\�b�h�E�C���W�F�N�V����</see></term>
    /// <description>�C�ӂ̃��\�b�h�𗘗p���ăR���|�[�l���g���Z�b�g���܂��B</description>
    /// </item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <seealso cref="http://s2container.seasar.org/ja/images/include_range_20040706.png">
    /// �C���N���[�h�̎Q�Ɣ͈͂ɂ��ẴC���[�W</seealso>
    /// <seealso cref="http://s2container.seasar.org/ja/images/include_search_20040706.png">
    /// �R���e�i�̌������ɂ��ẴC���[�W</seealso>
    public interface IS2Container : IMetaDefAware
    {
        /// <summary>
        /// �w�肳�ꂽ�L�[�ɑ΂���R���|�[�l���g��Ԃ��܂��B
        /// </summary>
        /// <remarks>
        /// <para>
        /// �L�[��������̏ꍇ�A���O����v����R���|�[�l���g��Ԃ��܂��B
        /// �L�[���N���X�܂��̓C���^�[�t�F�[�X�̏ꍇ�A�L�[�̌^�ɑ���\�ȃR���|�[�l���g��Ԃ��܂��B
        /// </para>
        /// </remarks>
        /// <param name="componentKey">�R���|�[�l���g���擾���邽�߂̃L�[</param>
        /// <returns>�R���|�[�l���g</returns>
        /// <exception cref="ComponentNotFoundRuntimeException">�R���|�[�l���g��������Ȃ��ꍇ</exception>
        /// <exception cref="TooManyRegistrationRuntimeException">
        /// �������O�A�܂��͓����N���X�ɕ����̃R���|�[�l���g���o�^����Ă���ꍇ
        /// </exception>
        /// <exception cref="CyclicReferenceRuntimeException">
        /// �R���X�g���N�^�E�C���W�F�N�V�����ŃR���|�[�l���g�̎Q�Ƃ��z���Ă���ꍇ
        /// </exception>
        object GetComponent(object componentKey);

        /// <summary>
        /// �w�肳�ꂽ�L�[�ɑΉ����镡���̃R���|�[�l���g���������ĕԂ��܂��B
        /// </summary>
        /// <remarks>
        /// �����͈̔͂͌��݂�S2�R���e�i����сA�C���N���[�h���Ă���S2�R���e�i�̊K�w�S�̂ł��B
        /// �L�[�ɑΉ�����R���|�[�l���g���ŏ��Ɍ�������S2�R���e�i��ΏۂƂ��܂��B
        /// ����S2�R���e�i����C�L�[�ɑΉ�����S�ẴR���|�[�l���g��z��ŕԂ��܂��B
        /// �Ԃ����z��Ɋ܂܂��R���|�[�l���g�͑S�ē����S2�R���e�i�ɓo�^���ꂽ���̂ł��B
        /// </remarks>
        /// <param name="componentKey">�R���|�[�l���g���擾���邽�߂̃L�[</param>
        /// <returns>
        /// �L�[�ɑΉ�����R���|�[�l���g�̔z���Ԃ��܂��B 
        /// �L�[�ɑΉ�����R���|�[�l���g�����݂��Ȃ��ꍇ�͒���0�̔z���Ԃ��܂��B
        /// </returns>
        /// <exception cref="CyclicReferenceRuntimeException">
        /// �R���X�g���N�^�E�C���W�F�N�V�����ŃR���|�[�l���g�̎Q�Ƃ��z���Ă���ꍇ
        /// </exception>
        /// <seealso cref="FindAllComponents"/>
        /// <seealso cref="FindLocalComponents"/>
        object[] FindComponents(object componentKey);

        /// <summary>
        /// �w�肳�ꂽ�L�[�ɑΉ����镡���̃R���|�[�l���g���������ĕԂ��܂��B
        /// </summary>
        /// <remarks>
        /// <para>
        /// �����͈̔͂͌��݂�S2�R���e�i����сA�C���N���[�h���Ă���S2�R���e�i�̊K�w�S�̂ł��B
        /// �L�[�ɑΉ�����R���|�[�l���g���ŏ��Ɍ�������S2�R���e�i�Ƃ��̎q���R���e�i�̑S�Ă�ΏۂƂ��܂��B
        /// �ΏۂɂȂ�S2�R���e�i�S�̂���A�L�[�ɑΉ�����S�ẴR���|�[�l���g��z��ŕԂ��܂��B
        /// </para>
        /// </remarks>
        /// <param name="componentKey">�R���|�[�l���g���擾���邽�߂̃L�[</param>
        /// <returns>
        /// �L�[�ɑΉ�����R���|�[�l���g�̔z���Ԃ��܂��B 
        /// �L�[�ɑΉ�����R���|�[�l���g�����݂��Ȃ��ꍇ�͒���0�̔z���Ԃ��܂��B
        /// </returns>
        /// <exception cref="CyclicReferenceRuntimeException">
        /// �R���X�g���N�^�E�C���W�F�N�V�����ŃR���|�[�l���g�̎Q�Ƃ��z���Ă���ꍇ
        /// </exception>
        /// <seealso cref="FindComponents"/>
        /// <seealso cref="FindLocalComponents"/>
        object[] FindAllComponents(object componentKey);

        /// <summary>
        /// �w�肳�ꂽ�L�[�ɑΉ����镡���̃R���|�[�l���g���������ĕԂ��܂��B
        /// </summary>
        /// <remarks>
        /// <para>
        /// �����͈̔͂͌��݂�S2�R���e�i�݂̂ł��B
        /// ���݂�S2�R���e�i����A�L�[�ɑΉ�����S�ẴR���|�[�l���g��z��ŕԂ��܂��B
        /// </para>
        /// </remarks>
        /// <param name="componentKey">�R���|�[�l���g���擾���邽�߂̃L�[</param>
        /// <returns>
        /// �L�[�ɑΉ�����R���|�[�l���g�̔z���Ԃ��܂��B 
        /// �L�[�ɑΉ�����R���|�[�l���g�����݂��Ȃ��ꍇ�͒���0�̔z���Ԃ��܂��B
        /// </returns>
        /// <exception cref="CyclicReferenceRuntimeException">
        /// �R���X�g���N�^�E�C���W�F�N�V�����ŃR���|�[�l���g�̎Q�Ƃ��z���Ă���ꍇ
        /// </exception>
        /// <seealso cref="FindComponents"/>
        /// <seealso cref="FindAllComponents"/>
        object[] FindLocalComponents(object componentKey);

        /// <summary>
        /// <code>outerComponent</code>�̃N���X���L�[�Ƃ��ēo�^���ꂽ
        /// <see cref="IComponentDef">�R���|�[�l���g��`</see>
        /// �ɏ]���āA�K�v�ȃR���|�[�l���g�̃C���W�F�N�V���������s���܂��B
        /// �A�X�y�N�g�A�R���X�g���N�^�E�C���W�F�N�V�����͓K�p�ł��܂���B
        /// </summary>
        /// <remarks>
        /// <para>
        /// <see cref="IComponentDef">�R���|�[�l���g��`</see>��
        /// <see cref="IInstanceDef">�C���X�^���X��`</see>��outer�łȂ��Ă͂Ȃ�܂���B
        /// </para>
        /// </remarks>
        /// <param name="outerComponent">�O���R���|�[�l���g</param>
        /// <exception cref="ClassUnmatchRuntimeException">
        /// �K������R���|�[�l���g��`��������Ȃ��ꍇ
        /// </exception>
        void InjectDependency(object outerComponent);

        /// <summary>
        /// <code>componentType</code>���L�[�Ƃ��ēo�^���ꂽ
        /// <see cref="IComponentDef">�R���|�[�l���g��`</see>�ɏ]���āA
        /// �K�v�ȃR���|�[�l���g�̃C���W�F�N�V���������s���܂��B
        /// �A�X�y�N�g�A�R���X�g���N�^�E�C���W�F�N�V�����͓K�p�ł��܂���B
        /// </summary>
        /// <remarks>
        /// <para>
        /// <see cref="IComponentDef">�R���|�[�l���g��`</see>��
        /// <see cref="IInstanceDef">�C���X�^���X��`</see>��outer�łȂ��Ă͂Ȃ�܂���B
        /// </para>
        /// </remarks>
        /// <param name="outerComponent">�O���R���|�[�l���g</param>
        /// <param name="componentType">�R���|�[�l���g��`�̃L�[ (Type)</param>
        /// <exception cref="ClassUnmatchRuntimeException">
        /// �K������R���|�[�l���g��`��������Ȃ��ꍇ
        /// </exception>
        void InjectDependency(object outerComponent, Type componentType);

        /// <summary>
        /// <code>componentName</code>���L�[�Ƃ��ēo�^���ꂽ 
        /// <see cref="IComponentDef">�R���|�[�l���g��`</see>�ɏ]���āA
        /// �K�v�ȃR���|�[�l���g�̃C���W�F�N�V���������s���܂��B
        /// �A�X�y�N�g�A�R���X�g���N�^�E�C���W�F�N�V�����͓K�p�ł��܂���B
        /// </summary>
        /// <remarks>
        /// <para>
        /// <see cref="IComponentDef">�R���|�[�l���g��`</see>��
        /// <see cref="IInstanceDef">�C���X�^���X��`</see>��outer�łȂ��Ă͂Ȃ�܂���B
        /// </para>
        /// </remarks>
        /// <param name="outerComponent">�O���R���|�[�l���g</param>
        /// <param name="componentName">�R���|�[�l���g��`�̃L�[ (���O)</param>
        /// <exception cref="ClassUnmatchRuntimeException">
        /// �K������R���|�[�l���g��`��������Ȃ��ꍇ
        /// </exception>
        void InjectDependency(object outerComponent, string componentName);

        /// <summary>
        /// �R���|�[�l���g��o�^���܂��B
        /// </summary>
        /// <remarks>
        /// <para>
        /// S2�R���e�i�ɖ����̃R���|�[�l���g�Ƃ��ēo�^���܂��B
        /// �o�^���ꂽ�R���|�[�l���g�̓C���W�F�N�V������A�X�y�N�g�̓K�p�Ȃǂ͏o���܂���B
        /// ���̃R���|�[�l���g�\�z���Ɉˑ��I�u�W�F�N�g�Ƃ��ė��p���邱�Ƃ��\�ł��B
        /// </para>
        /// </remarks>
        /// <param name="component">�R���|�[�l���g</param>
        void Register(object component);

        /// <summary>
        /// �w�肳�ꂽ���O�ŃR���|�[�l���g��o�^���܂��B
        /// </summary>
        /// <param name="component">�R���|�[�l���g</param>
        /// <param name="componentName">�R���|�[�l���g��</param>
        void Register(object component, string componentName);

        /// <summary>
        /// �N���X��Type���R���|�[�l���g��`�Ƃ��ēo�^���܂��B
        /// </summary>
        /// <remarks>
        /// <para>
        /// �o�^����R���|�[�l���g�͈ȉ��̂��̂ɂȂ�܂��B
        /// <list type="table">
        /// <item>
        /// <term><see cref="IInstanceDef">�C���X�^���X��`</see></term>
        /// <description><code>singleton</code></description>
        /// </item>
        /// <item>
        /// <term><see cref="IAutoBindingDef">�����o�C���f�B���O��`</see></term>
        /// <description><code>auto</code></description>
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <param name="componentType">�R���|�[�l���g�̃N���X��Type</param>
        void Register(Type componentType);

        /// <summary>
        /// �w�肳�ꂽ���O�ŃN���X��Type���R���|�[�l���g��`�Ƃ��ēo�^���܂��B
        /// </summary>
        /// <remarks>
        /// <para>
        /// �o�^����R���|�[�l���g�͈ȉ��̂��̂ɂȂ�܂��B
        /// <list type="table">
        /// <item>
        /// <term><see cref="IInstanceDef">�C���X�^���X��`</see></term>
        /// <description><code>singleton</code></description>
        /// </item>
        /// <item>
        /// <term><see cref="IAutoBindingDef">�����o�C���f�B���O��`</see></term>
        /// <description><code>auto</code></description>
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <param name="componentType">�R���|�[�l���g�̃N���X��Type</param>
        /// <param name="componentName">�R���|�[�l���g��</param>
        void Register(Type componentType, string componentName);

        /// <summary>
        /// �R���|�[�l���g��`��o�^���܂��B
        /// </summary>
        /// <param name="componentDef">�R���|�[�l���g��`</param>
        void Register(IComponentDef componentDef);

        /// <summary>
        /// �R���e�i�ɓo�^����Ă���R���|�[�l���g��`�̐���Ԃ��܂��B
        /// </summary>
        /// <value>�R���|�[�l���g��`�̐�</value>
        int ComponentDefSize { get; }

        /// <summary>
        /// �ԍ��Ŏw�肳�ꂽ�ʒu�̃R���|�[�l���g��`��Ԃ��܂��B
        /// </summary>
        /// <param name="index">�ԍ�</param>
        /// <returns>�R���|�[�l���g��`</returns>
        IComponentDef GetComponentDef(int index);

        /// <summary>
        /// �w�肳�ꂽ�L�[�ɑΉ�����R���|�[�l���g��`��Ԃ��܂��B
        /// </summary>
        /// <param name="componentKey">�R���|�[�l���g��`���擾���邽�߂̃L�[</param>
        /// <returns>�R���|�[�l���g��`</returns>
        /// <exception cref="ComponentNotFoundRuntimeException">
        /// �R���|�[�l���g��`��������Ȃ��ꍇ
        /// </exception>
        IComponentDef GetComponentDef(object componentKey);

        /// <summary>
        /// �w�肳�ꂽ�L�[�ɑΉ����镡���̃R���|�[�l���g��`���������ĕԂ��܂��B
        /// </summary>
        /// <remarks>
        /// <para>
        /// �����͈̔͂͌��݂�S2�R���e�i����сA�C���N���[�h���Ă���S2�R���e�i�̊K�w�S�̂ł��B
        /// �L�[�ɑΉ�����R���|�[�l���g���ŏ��Ɍ�������S2�R���e�i��ΏۂƂ��܂��B
        /// ����S2�R���e�i����C�L�[�ɑΉ�����S�ẴR���|�[�l���g��`��z��ŕԂ��܂��B
        /// �Ԃ����z��Ɋ܂܂��R���|�[�l���g��`�͑S�ē����S2�R���e�i�ɓo�^���ꂽ���̂ł��B
        /// </para>
        /// </remarks>
        /// <param name="componentKey">�R���|�[�l���g��`���擾���邽�߂̃L�[</param>
        /// <returns>
        /// �L�[�ɑΉ�����R���|�[�l���g��`�̔z���Ԃ��܂��B
        /// �L�[�ɑΉ�����R���|�[�l���g��`�����݂��Ȃ��ꍇ�͒���0�̔z���Ԃ��܂��B
        /// </returns>
        /// <seealso cref="FindAllComponentDefs"/>
        /// <seealso cref="FindLocalComponentDefs"/>
        IComponentDef[] FindComponentDefs(object componentKey);

        /// <summary>
        /// �w�肳�ꂽ�L�[�ɑΉ����镡���̃R���|�[�l���g��`���������ĕԂ��܂��B
        /// </summary>
        /// <remarks>
        /// <para>
        /// �����͈̔͂͌��݂�S2�R���e�i����сA�C���N���[�h���Ă���S2�R���e�i�̊K�w�S�̂ł��B
        /// �L�[�ɑΉ�����R���|�[�l���g���ŏ��Ɍ�������S2�R���e�i�Ƃ��̎q���R���e�i�̑S�Ă�ΏۂƂ��܂��B
        /// �ΏۂɂȂ�S2�R���e�i�S�̂���A�L�[�ɑΉ�����S�ẴR���|�[�l���g��`��z��ŕԂ��܂��B
        /// </para>
        /// </remarks>
        /// <param name="componentKey"�R���|�[�l���g��`���擾���邽�߂̃L�[></param>
        /// <returns>
        /// �L�[�ɑΉ�����R���|�[�l���g��`�̔z���Ԃ��܂��B
        /// �L�[�ɑΉ�����R���|�[�l���g��`�����݂��Ȃ��ꍇ�͒���0�̔z���Ԃ��܂��B
        /// </returns>
        /// <seealso cref="FindComponentDefs"/>
        /// <seealso cref="FindLocalComponentDefs"/>
        IComponentDef[] FindAllComponentDefs(object componentKey);

        /// <summary>
        /// �w�肳�ꂽ�L�[�ɑΉ����镡���̃R���|�[�l���g��`���������ĕԂ��܂��B
        /// </summary>
        /// <remarks>
        /// <para>
        /// �����͈̔͂͌��݂�S2�R���e�i�݂̂ł��B 
        /// ���݂�S2�R���e�i����A�L�[�ɑΉ�����S�ẴR���|�[�l���g��`��z��ŕԂ��܂��B
        /// </para>
        /// </remarks>
        /// <param name="componentKey">�R���|�[�l���g��`���擾���邽�߂̃L�[</param>
        /// <returns>
        /// �L�[�ɑΉ�����R���|�[�l���g��`�̔z���Ԃ��܂��B
        /// �L�[�ɑΉ�����R���|�[�l���g��`�����݂��Ȃ��ꍇ�͒���0�̔z���Ԃ��܂��B
        /// </returns>
        /// <seealso cref="FindComponentDefs"/>
        /// <seealso cref="FindAllComponentDefs"/>
        IComponentDef[] FindLocalComponentDefs(object componentKey);

        /// <summary>
        /// �w�肳�ꂽ�L�[�ɑΉ�����R���|�[�l���g��`�����݂���ꍇ<code>true</code>��Ԃ��܂��B
        /// </summary>
        /// <param name="componentKey">�L�[</param>
        /// <returns>
        /// �L�[�ɑΉ�����R���|�[�l���g��`�����݂���ꍇ<code>true</code>�A
        /// �����łȂ��ꍇ��<code>false</code>
        /// </returns>
        bool HasComponentDef(object componentKey);

        /// <summary>
        /// <code>path</code>��ǂݍ���S2�R���e�i�����݂���ꍇ<code>true</code>��Ԃ��܂��B
        /// </summary>
        /// <param name="path">�p�X</param>
        /// <returns>
        /// <code>path</code>��ǂݍ���S2�R���e�i�����݂���ꍇ<code>true</code>�A
        /// �����łȂ��ꍇ��<code>false</code>
        /// </returns>
        bool HasDescendant(string path);

        /// <summary>
        /// <code>path</code>��ǂݍ���S2�R���e�i��Ԃ��܂��B
        /// </summary>
        /// <param name="path">�p�X</param>
        /// <returns>S2�R���e�i</returns>
        /// <exception cref="ContainerNotRegisteredRuntimeException">
        /// S2�R���e�i��������Ȃ��ꍇ
        /// </exception>
        IS2Container GetDescendant(string path);

        /// <summary>
        /// <code>descendant</code>���q���R���e�i�Ƃ��ēo�^���܂��B
        /// </summary>
        /// <remarks>
        /// <para>
        /// �q���R���e�i�Ƃ́A���̃R���e�i�ɑ�����q�̃R���e�i��A���̎q�ł���R���e�i�ł��B
        /// </para>
        /// </remarks>
        /// <param name="descendant">�q���R���e�i</param>
        void RegisterDescendant(IS2Container descendant);

        /// <summary>
        /// �R���e�i���q�Ƃ��ăC���N���[�h���܂��B
        /// </summary>
        /// <param name="child">�C���N���[�h����S2�R���e�i</param>
        void Include(IS2Container child);

        /// <summary>
        /// �C���N���[�h���Ă���q�R���e�i�̐���Ԃ��܂��B
        /// </summary>
        /// <value>�q�R���e�i�̐�</value>
        int ChildSize { get; }

        /// <summary>
        /// �ԍ��Ŏw�肳�ꂽ�ʒu�̎q�R���e�i��Ԃ��܂��B
        /// </summary>
        /// <param name="index">�q�R���e�i�̔ԍ�</param>
        /// <returns>�q�R���e�i</returns>
        IS2Container GetChild(int index);

        /// <summary>
        /// ���̃R���e�i���C���N���[�h���Ă���e�R���e�i�̐���Ԃ��܂��B
        /// </summary>
        /// <value>�e�R���e�i�̐�</value>
        int ParentSize { get; }

        /// <summary>
        /// �ԍ��Ŏw�肳�ꂽ�ʒu�̐e�R���e�i��Ԃ��܂��B
        /// </summary>
        /// <param name="index">�e�R���e�i�̔ԍ�</param>
        /// <returns>�e�R���e�i</returns>
        IS2Container GetParent(int index);

        /// <summary>
        /// �e�R���e�i��ǉ����܂��B
        /// </summary>
        /// <param name="parent">�e�Ƃ��Ēǉ�����S2�R���e�i</param>
        void AddParent(IS2Container parent);

        /// <summary>
        /// �R���e�i�̏��������s���܂��B 
        /// �q�R���e�i�����ꍇ�A�q�R���e�i��S�ď�����������A�����̏��������s���܂��B
        /// </summary>
        void Init();

        /// <summary>
        /// �R���e�i�̏I�������������Ȃ��܂��B 
        /// �q�R���e�i�����ꍇ�A�����̏I�����������s������A�q�R���e�i�S�Ă̏I���������s���܂��B
        /// </summary>
        void Destroy();

        /// <summary>
        /// ���O��Ԃ��擾�������͐ݒ肵�܂��B
        /// </summary>
        /// <value>���O���</value>
        string Namespace { get; set; }

        /// <summary>
        /// �R���e�i�쐬���ɏ��������s�������擾�������͐ݒ肵�܂��B
        /// </summary>
        /// <value>
        /// <code>true</code>�̏ꍇ�̓R���e�i�쐬���ɏ��������s���܂��B
        /// <code>false</code>�̏ꍇ�̓R���e�i�쐬���ɏ��������s���܂���B
        /// </value>
        bool InitializeOnCreate { get; set; }

        /// <summary>
        /// �ݒ�t�@�C����<code>path</code>���擾�������͐ݒ肵�܂��B
        /// </summary>
        /// <value>�ݒ�t�@�C����<code>path</code></value>
        string Path { get; set; }

        /// <summary>
        /// ���[�g��S2�R���e�i���擾�������͐ݒ肵�܂��B
        /// </summary>
        /// <value>���[�g��S2�R���e�i</value>
        IS2Container Root { get; set; }

        /// <summary>
        /// �O���R���e�L�X�g���擾�������͐ݒ肵�܂��B
        /// </summary>
        /// <remarks>
        /// <para>
        /// <see cref="IExternalContext">�O���R���e�L�X�g</see>��
        /// <code>application</code>�A<code>request</code>�A <code>session</code>�Ȃ�
        /// �e<see cref="IInstanceDef">�C���X�^���X��`</see>��񋟂�����̂ł��B
        /// �����̃C���X�^���X��`���g�p����ɂ�
        /// <code>IExternalContext</code>��S2�R���e�i�ɐݒ肷��K�v������܂��B
        /// </para>
        /// </remarks>
        /// <value>�O���R���e�L�X�g</value>
        IExternalContext ExternalContext { get; set; }

        /// <summary>
        /// <see cref="IExternalContext">�O���R���e�L�X�g</see>
        /// ���񋟂���R���|�[�l���g��o�^����I�u�W�F�N�g���擾�������͐ݒ肵�܂��B
        /// </summary>
        /// <value>�O���R���e�L�X�g���񋟂���R���|�[�l���g��o�^����I�u�W�F�N�g</value>
        IExternalContextComponentDefRegister 
            ExternalContextComponentDefRegister { get; set; }

        /// <summary>
        /// �q�R���e�i�i<code>container</code>�j�ɓo�^���ꂽ
        /// �R���|�[�l���g��`�i<code>componentDef</code>�j�����̃R���e�i���猟���ł���悤
        /// �R���|�[�l���g��`���Ǘ�����}�b�v�ɓo�^���܂��B
        /// </summary>
        /// <param name="key">�L�[</param>
        /// <param name="componentDef">�R���|�[�l���g��`</param>
        /// <param name="container">S2�R���e�i</param>
        void RegisterMap(object key, IComponentDef componentDef, IS2Container container);
    }
}
