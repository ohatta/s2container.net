#region Copyright

/*
 * Copyright 2005-2010 the Seasar Foundation and the Others.
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
using System.Diagnostics;

namespace Seasar.Dxo.Converter.Impl
{
    /// <summary>
    /// �I�u�W�F�N�g��Enum�ɕϊ����邽�߂̃R���o�[�^�N���X
    /// </summary>
    public class EnumConverter : AbstractPropertyConverter
    {
        /// <summary>
        /// �I�u�W�F�N�g�̃v���p�e�B��C�ӂ̌^�ɕϊ����܂�
        /// (���ۃ��\�b�h�͔h���N���X�ŕK���I�[�o���C�h����܂�)
        /// </summary>
        /// <param name="source">�ϊ����̃I�u�W�F�N�g</param>
        /// <param name="dest">�ϊ���̃I�u�W�F�N�g</param>
        /// <param name="expectType">�ϊ���̃I�u�W�F�N�g�Ɋ��҂���Ă���^</param>
        /// <returns>bool �ϊ������������ꍇ�ɂ�true</returns>
        protected override bool DoConvert(object source, ref object dest, Type expectType)
        {
            Debug.Assert(expectType.IsEnum, String.Format(DxoMessages.EDXO1004, "expectType", "Enum"));
//            Debug.Assert(expectType.IsEnum, "expectType��Enum�ł���͂�");

            if (source.GetType().IsEnum)
            {
                if (Enum.IsDefined(expectType, source))
                {
                    dest = source;
                    return true;
                }
            }
            else if (source is string)
            {
                //Enum�̗ސ����s��   
                dest = _GetEnumValue(source as string, expectType);
                return true;
            }
            else if (source is IConvertible)
            {
                if (Enum.IsDefined(expectType, source))
                {
                    dest = Enum.ToObject(expectType, source);
                    return true;
                }
            }
            return false;
        }

        private static object _GetEnumValue(string strvalue, Type enumType)
        {
            string[] enumElements =
                strvalue.Split(new char[] {'.'}, StringSplitOptions.RemoveEmptyEntries);
            if (enumElements.Length > 1)
            {
                return Enum.Parse(enumType, enumElements[1], true);
            }
            return Enum.Parse(enumType, strvalue, true);
        }
    }
}
