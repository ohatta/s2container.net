#region Copyright

/*
 * Copyright 2005-2009 the Seasar Foundation and the Others.
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
 * governing permissions and limitations under the LicensSe.
 */

#endregion

using System;
using System.Globalization;

namespace Seasar.Dxo.Converter.Impl
{
    /// <summary>
    /// DateTime�^�ϊ����邽�߂̃R���o�[�^�N���X
    /// </summary>
    public class DateTimeConverter : AbstractPropertyConverter
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
            if (source is string)
            {
                DateTime target;
                CultureInfo format = new CultureInfo("ja-JP", false);
                target = DateTime.ParseExact(source as string, this.Format, format);
                dest = target;

                return true;
            }
            else if (source is bool)
            {
                if ((bool) source)
                    dest = new DateTime(1);
                else
                    dest = new DateTime(0);

                return true;
            }
            else
            {
                try
                {
                    string convertedSource = source.ToString();
                    long val = Int64.Parse(convertedSource);
                    dest = new DateTime(val);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}