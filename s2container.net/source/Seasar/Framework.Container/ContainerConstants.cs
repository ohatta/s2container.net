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
    /// S2�R���e�i�Ŏg�p�����萔���`����N���X�ł��B
    /// </summary>
    /// <remarks>
    /// <para>�Z�p���[�^�������`�ς݃R���|�[�l���g�L�[(�R���|�[�l���g��)
    /// �Ȃǂ̒萔���`���Ă��܂��B</para>
    /// </remarks>
    public class ContainerConstants
    {
        /// <summary>
        /// ���O��ԂƃR���|�[�l���g���̋�؂�(char)��\���萔�ł��B
        /// </summary>
        public const char NS_SEP = '.';

        /// <summary>
        /// �p�b�P�[�W��(Java�̃p�b�P�[�W�Ƃ͈قȂ�)�t���R���|�[�l���g���ɂ�����A
        /// �p�b�P�[�W���Ǝ����o�C���f�B���O�p�R���|�[�l���g���̋�؂�(char)��\���萔�ł��B
        /// </summary>
        public const char PACKAGE_SEP = '_';

        /// <summary>
        /// ���O��ԂƃR���|�[�l���g���̋�؂�(String)��\���萔�ł��B
        /// </summary>
        public const string NS_SEP_STR = ".";

        /// <summary>
        /// S2�R���e�i�̃R���|�[�l���g�L�[��\���萔�ł��B
        /// </summary>
        /// <seealso cref="Seasar.Framework.Container.Impl.S2Container"/>
        public const string CONTAINER_NAME = "container";

        /// <summary>
        /// �O���R���e�L�X�g���񋟂��郊�N�G�X�g�I�u�W�F�N�g���擾���邽�߂́A 
        /// �R���|�[�l���g�L�[��\���萔�ł��B
        /// </summary>
        /// <seealso cref="Seasar.Framework.Container.External.IRequestComponentDef"/>
        public const string REQUEST_NAME = "request";

        /// <summary>
        /// �O���R���e�L�X�g���񋟂��郌�X�|���X�I�u�W�F�N�g���擾���邽�߂́A
        /// �R���|�[�l���g�L�[��\���萔�ł��B
        /// </summary>
        public const string RESPONSE_NAME = "response";

        /// <summary>
        /// �O���R���e�L�X�g���񋟂���A�v���P�[�V�����R���e�L�X�g���擾���邽�߂́A 
        /// �R���|�[�l���g�L�[��\���萔�ł��B
        /// </summary>
        public const string SESSION_NAME = "session";

        /// <summary>
        /// �O���R���e�L�X�g���񋟂���A�v���P�[�V�����R���e�L�X�g���擾���邽�߂́A 
        /// �R���|�[�l���g�L�[��\���萔�ł��B
        /// </summary>
        public const string APPLICATION_CONTEXT_NAME = "application";

        /// <summary>
        /// �O���R���e�L�X�g���񋟂���A�v���P�[�V�����X�R�[�v��
        /// <see cref="System.Collections.IDictionary"/>
        /// �C���^�[�t�F�[�X�Ŏ擾���邽�߂́A�R���|�[�l���g�L�[��\���萔�ł��B
        /// </summary>
        /// <seealso cref="Seasar.Framework.Container.External.IApplicationComponentDef"/>
        /// <seealso cref="Seasar.Framework.Container.External.IApplicationMapComponentDef"/>
        public const string APPLICATION_SCOPE = "applicationScope";

        /// <summary>
        /// �O���R���e�L�X�g�̏������p�����[�^��<see cref="System.Collections.IDictionary"/>
        /// �C���^�[�t�F�[�X�Ŏ擾���邽�߂́A�R���|�[�l���g�L�[��\���萔�ł��B
        /// </summary>
        /// <seealso cref="Seasar.Framework.Container.External.IInitParameterMapComponentDef"/>
        public const string INIT_PARAM = "initParam";

        /// <summary>
        /// �O���R���e�L�X�g���񋟂���Z�b�V�����X�R�[�v��<see cref="System.Collections.IDictionary"/>
        /// �C���^�[�t�F�[�X�Ŏ擾���邽�߂́A�R���|�[�l���g�L�[��\���萔�ł��B
        /// </summary>
        /// <seealso cref="Seasar.Framework.Container.External.ISessionMapComponentDef"/>
        public const string SESSION_SCOPE = "sessionScope";

        /// <summary>
        /// �O���R���e�L�X�g���񋟂��郊�N�G�X�g�R�[�v��<see cref="System.Collections.IDictionary"/>
        /// �C���^�[�t�F�[�X�Ŏ擾���邽�߂́A�R���|�[�l���g�L�[��\���萔�ł��B
        /// </summary>
        /// <seealso cref="Seasar.Framework.Container.External.IRequestMapComponentDef"/>
        public const string REQUEST_SCOPE = "requestScope";
            
        /// <summary>
        /// �O���R���e�L�X�g���񋟂���N�b�L�[(cookie)�̓��e��
        /// <see cref="System.Collections.IDictionary"/>�C���^�[�t�F�[�X�Ŏ擾���邽�߂́A
        /// �R���|�[�l���g�L�[��\���萔�ł��B
        /// </summary>
        public const string COOKIE = "cookie";

        /// <summary>
        /// �O���R���e�L�X�g���񋟂��郊�N�G�X�g�w�b�_�̓��e��
        /// <see cref="System.Collections.IDictionary"/>�C���^�[�t�F�[�X�Ŏ擾���邽�߂́A
        /// �R���|�[�l���g�L�[��\���萔�ł��B
        /// </summary>
        /// <seealso cref="Seasar.Framework.Container.External.IRequestHeaderMapComponentDef"/>
        public const string HEADER = "header";

        /// <summary>
        /// �O���R���e�L�X�g���񋟂��郊�N�G�X�g�w�b�_�̓��e��l�̔z��Ŏ擾���邽�߂́A 
        /// �R���|�[�l���g�L�[��\���萔�ł��B
        /// </summary>
        /// <seealso cref="Seasar.Framework.Container.External.IRequestHeaderValuesMapComponentDef"/>
        public const string HEADER_VALUES = "headerValues";

        /// <summary>
        /// �O���R���e�L�X�g���񋟂��郊�N�G�X�g�p�����[�^�̓��e��
        /// <see cref="System.Collections.IDictionary"/>�C���^�[�t�F�[�X�Ŏ擾���邽�߂́A
        /// �R���|�[�l���g�L�[��\���萔�ł��B
        /// </summary>
        /// <seealso cref="Seasar.Framework.Container.External.IRequestParameterMapComponentDef"/>
        public const string PARAM = "param";

        /// <summary>
        /// �O���R���e�L�X�g���񋟂��郊�N�G�X�g�p�����[�^�̓��e��l�̔z��Ŏ擾���邽�߂́A 
        /// �R���|�[�l���g�L�[��\���萔�ł��B
        /// </summary>
        /// <seealso cref="Seasar.Framework.Container.External.IRequestParameterValuesMapComponentDef"/>
        public const string PARAM_VALUES = "paramValues";

        /// <summary>
        /// <see cref="Seasar.Framework.Container.IComponentDef"/>(�R���|�[�l���g��`)��
        /// <see cref="System.Collections.IDictionary"/>�ɕێ�����ꍇ�ȂǂɁA�L�[�Ƃ��Ďg�p����萔�ł��B
        /// </summary>
        public const string COMPONENT_DEF_NAME = "componentDef";

        /// <summary>
        /// �R���t�B�O���[�V�����I�u�W�F�N�g(�ݒ��������I�u�W�F�N�g)���擾���邽�߂́A 
        /// �R���|�[�l���g�L�[��\���萔�ł��B
        /// </summary>
        public const string CONFIG_NAME = "config";

        /// <summary>
        /// Properties�̖��O��Ԃ�\���萔�ł��B
        /// </summary>
        public const string PROPERTIES_NAMESPACE = "Seasar.Properties";
    }
}
