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
using Seasar.Quill.Attrs;
using System.Reflection;

namespace Seasar.Quill.Util
{
    /// <summary>
    /// Quill�ŗp�ӂ���Ă��鑮���������N���X
    /// </summary>
    public static class AttributeUtil
    {
        /// <summary>
        /// �����N���X���w�肷��ׂɐݒ肳��Ă��鑮��
        /// (<see cref="Seasar.Quill.Attrs.ImplementationAttribute"/>)���擾����
        /// </summary>
        /// <param name="type">�������m�F����Type</param>
        /// <returns>�����N���X���w�肳�ꂽ����</returns>
        public static ImplementationAttribute GetImplementationAttr(Type type)
        {
            // �����N���X���w�肷�鑮�����擾����
            ImplementationAttribute implAttr =
                (ImplementationAttribute)Attribute.GetCustomAttribute(
                type, typeof(ImplementationAttribute));

            if(implAttr == null)
            {
                // Implementation�������w�肳��Ă��Ȃ��ꍇ��null��Ԃ�
                return null;
            }

            // Implementation�����Ɏw�肳�ꂽ�����N���X��Type
            Type implType = implAttr.ImplementationType;

            if (!type.IsInterface && implType != null)
            {
                // �N���X��Implementation�����Ɏ����N���X���w�肳��Ă���ꍇ��
                // ��O���X���[����
                throw new QuillApplicationException("EQLL0001",
                    new object[] { type.FullName });
            }

            if (implType != null && implType.IsInterface)
            {
                // Implementation�����̎����N���X�ɃC���^�[�t�F�[�X��
                // �w�肳��Ă���ꍇ�͗�O���X���[����
                throw new QuillApplicationException("EQLL0002", new object[] {
                    type.FullName, implType.FullName });
            }

            if (implType != null && implType.IsAbstract)
            {
                // Implementation�����̎����N���X�ɒ��ۃN���X��
                // �w�肳��Ă���ꍇ�͗�O���X���[����
                throw new QuillApplicationException("EQLL0003", new object[] {
                    type.FullName, implType.FullName });
            }

            if (implType != null && !type.IsAssignableFrom(implType))
            {
                // ����s�\�Ȏ����N���X���w�肳��Ă���ꍇ�͗�O���X���[����
                throw new QuillApplicationException("EQLL0004", new object[] {
                    type.FullName, implType.FullName });
            }

            // �����N���X���w�肷�鑮����Ԃ�
            return implAttr;
        }

        /// <summary>
        /// Aspect���w�肷��ׂɃN���X��C���^�[�t�F�[�X�ɐݒ肳��Ă��鑮��
        /// (<see cref="Seasar.Quill.Attrs.AspectAttribute"/>)���擾����
        /// </summary>
        /// <param name="type">�������m�F����Type</param>
        /// <returns>Aspect���w�肳�ꂽ�����̔z��</returns>
        public static AspectAttribute[] GetAspectAttrs(Type type)
        {
            // Aspect���w�肷�鑮�����擾���ĕԂ�
            return GetAspectAttrsByMember(type);
        }

        /// <summary>
        /// Aspect���w�肷��ׂɃ��\�b�h�ɐݒ肳��Ă��鑮��
        /// (<see cref="Seasar.Quill.Attrs.AspectAttribute"/>)���擾����
        /// </summary>
        /// <param name="method">�������m�F���郁�\�b�h</param>
        /// <returns>Aspect���w�肳�ꂽ�����̔z��</returns>
        public static AspectAttribute[] GetAspectAttrsByMethod(MethodInfo method)
        {
            // Aspect���w�肷�鑮�����擾����
            AspectAttribute[] attrs = GetAspectAttrsByMember(method);

            if (attrs.Length == 0)
            {
                // Aspect�������w�肳��Ă��Ȃ��ꍇ�͔z��T�C�Y0��Aspect�����̔z���Ԃ�
                return attrs;
            }

            if (method.IsStatic)
            {
                // ���\�b�h��static�̏ꍇ�͗�O���X���[����
                throw new QuillApplicationException("EQLL0005", new object[] {
                    method.DeclaringType.FullName, method.Name });
            }

            if (!method.IsPublic)
            {
                // ���\�b�h��public�ł͂Ȃ��ꍇ�͗�O���X���[����
                throw new QuillApplicationException("EQLL0006", new object[] {
                    method.DeclaringType.FullName, method.Name });
            }

            if (!method.IsVirtual)
            {
                // ���\�b�h��virtual���C���^�[�t�F�[�X�̃��\�b�h
                // �ł͂Ȃ��ꍇ�͗�O���X���[����
                throw new QuillApplicationException("EQLL0007", new object[] {
                    method.DeclaringType.FullName, method.Name });
            }

            // Aspect���w�肷�鑮����Ԃ�
            return attrs;
        }

        /// <summary>
        /// Aspect���w�肷��ׂɃ����o�ɐݒ肳��Ă��鑮��
        /// (<see cref="Seasar.Quill.Attrs.AspectAttribute"/>)���擾����
        /// </summary>
        /// <param name="member">�������m�F���郁���o</param>
        /// <returns>Aspect���w�肳�ꂽ�����̔z��</returns>
        private static AspectAttribute[] GetAspectAttrsByMember(MemberInfo member)
        {
            // Aspect���w�肷�鑮�����擾����
            AspectAttribute[] attrs =
                (AspectAttribute[])Attribute.GetCustomAttributes(
                member, typeof(AspectAttribute));

            // Aspect���w�肷�鑮����Ԃ�
            return attrs;
        }
    }
}
