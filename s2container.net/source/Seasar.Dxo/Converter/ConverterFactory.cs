#region Copyright
/*
 * Copyright 2005-2015 the Seasar Foundation and the Others.
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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using Seasar.Dxo.Converter.Impl;
using Seasar.Framework.Util;
using ImageConverter=Seasar.Dxo.Converter.Impl.ImageConverter;

namespace Seasar.Dxo.Converter
{
    /// <summary>
    /// ���f���𑊌ݕϊ����邽�߂̃R���o�[�^�𐶐����邽�߂̃t�@�N�g���N���X
    /// </summary>
    public static class ConverterFactory
    {
        private static readonly IPropertyConverter _assignableConverter = new AssignableConverter();
        private static readonly IPropertyConverter _listConverter = new ListConverter();
        private static readonly IPropertyConverter _dictionaryConverter = new DictionaryConverter();
        private static readonly IPropertyConverter _enumConverter = new EnumConverter();
        private static readonly IPropertyConverter _convertibleConverter = new ConvertibleConverter();
        private static readonly IPropertyConverter _typeConverterConverter = new TypeConverterConverter();
        private static readonly IPropertyConverter _imageConverter = new ImageConverter();
        private static readonly IPropertyConverter _datetimeConverter = new DateTimeConverter();
        private static readonly IPropertyConverter _stringConverter = new StringConverter();
        private static readonly IPropertyConverter _boolConverter = new BooleanConverter();
        private static readonly IPropertyConverter _charConverter = new CharConverter();

        private static readonly IDictionary<Type, IPropertyConverter> _converters;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        static ConverterFactory()
        {
            //�R���o�[�^����
            _converters = new Dictionary<Type, IPropertyConverter>();
        }

        /// <summary>
        /// �K�؂ȃR���o�[�^���擾���܂�
        /// </summary>
        /// <param name="source">�R���o�[�^��K�v�Ƃ��Ă���A���I�u�W�F�N�g���Z�b�g���܂�</param>
        /// <param name="destType">���I�u�W�F�N�g��ϊ�����^���Z�b�g���܂�</param>
        /// <returns>IPropertyConverter �K�؂ȃR���o�[�^���߂�܂�</returns>
        public static IPropertyConverter GetConverter(object source, Type destType)
        {
            Debug.Assert(destType != null, String.Format(DxoMessages.EDXO1001, "destType"));
//            Debug.Assert(destType != null, "destType�͔�null�̂͂�");
            if (destType == null)
                throw new ArgumentNullException(nameof(destType));

            if ((source != null && source.GetExType() == destType) // �����^��
                || (source != null && source.GetExType().IsSubclassOf(destType))) // �h���^��
            {
                return _assignableConverter;
            }
            else if (destType.IsGenericType) //�W�F�l���b�N��
            {
                //�^�p�����^���Ȃ����^���擾
                var openType = destType.GetGenericTypeDefinition();

                //�R���o�[�^�̃I�[�v���Ȍ^
                Type converterOpenType = null;

                //�ϊ���W�F�l���N�X�̌^�p�����^�z��
                var typeParams = destType.GetGenericArguments();

                //ICollection<>��
                if (typeof (ICollection<>).IsAssignableFrom(openType))
                {
                    converterOpenType = typeof (GenericsCollectionConverter<>);
                }
                    //IList<>��
                else if (typeof (IList<>).IsAssignableFrom(openType))
                {
                    converterOpenType = typeof (GenericsListConverter<>);
                }
                    //IDictionary<>��
                else if (typeof (IDictionary<,>).IsAssignableFrom(openType))
                {
                    converterOpenType = typeof (GenericsDictionaryConverter<,>);
                }

                //�W�F�l���N�X�R���o�[�^�𐶐��A���͎擾
                if (converterOpenType != null)
                    return (_GetGenericsConverter(converterOpenType, typeParams));
                else
                    return _typeConverterConverter;
            }
            else
            {
                if (destType.IsArray) //�z��
                {
                    Type[] typeParams = {destType.GetElementType()};
                    if (source != null && source.GetExType() == typeof (string))
                    {
                        return _charConverter;
                    }
                    else
                    {
                        //�W�F�l���N�X�R���o�[�^�𐶐��A���͎擾
                        return _GetGenericsConverter(typeof (ArrayConverter<>), typeParams);
                    }
                }
                else if (typeof (IList).IsAssignableFrom(destType)) //���X�g��
                {
                    return _listConverter;
                }
                else if (typeof (IDictionary).IsAssignableFrom(destType)) //������
                {
                    return _dictionaryConverter;
                }
                else if (destType == typeof (bool) || destType == typeof (Boolean))
                {
                    return _boolConverter;
                }
                else if (destType.IsPrimitive) //�v���~�e�B�u��
                {
                    return _convertibleConverter;
                }
                else if (destType.IsEnum) //�񋓂�
                {
                    return _enumConverter;
                }
                else if (typeof (Image).IsAssignableFrom(destType)) //�C���[�W��
                {
                    return _imageConverter;
                }
                else if (destType == typeof (DateTime)) // ���t
                {
                    return _datetimeConverter;
                }
                else if (destType == typeof (string)) // ����
                {
                    return _stringConverter;
                }
                else //�K�����Ȃ��ꍇ��TypeConvterter���g���Ă݂�
                {
                    return _typeConverterConverter;
                }
            }
        }

        /// <summary>
        /// Genrrics���g�p�����^�R���o�[�^���擾���܂�
        /// </summary>
        /// <param name="openType">Generics�̃I�[�v���^�C�v���Z�b�g���܂�</param>
        /// <param name="typeParams">Generics�̌^�p�����^���Z�b�g���܂�</param>
        /// <returns>IPropertyConverter ��������邩�A���͎�������擾�����R���o�[�^���߂�܂�</returns>
        private static IPropertyConverter _GetGenericsConverter(Type openType, Type[] typeParams)
        {
            var converterType = openType.MakeGenericType(typeParams);
            if (! _converters.ContainsKey(converterType))
            {
                //Generics�R���N�V�����p�R���o�[�^�̃C���X�^���X�𐶐�
                var propertyConverter = ClassUtil.NewInstance(converterType) as IPropertyConverter;
//                    Activator.CreateInstance(converterType) as IPropertyConverter;
                _converters[converterType] = propertyConverter;
            }
            return _converters[converterType];
        }
    }
}
