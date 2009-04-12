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
 * governing permissions and limitations under the License.
 */

#endregion

using System;

namespace Seasar.Dxo.Converter.Impl
{
    /// <summary>
    /// Boolean�^�ϊ����邽�߂̃R���o�[�^�N���X
    /// </summary>
    public class BooleanConverter : AbstractPropertyConverter
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
            double val;
            string target = source.ToString();
            if (Double.TryParse(target, out val))
            {
                if (val * 10000 >= 10000)
                    dest = true;
                else
                    dest = false;
            }
            else
            {
                string convertedSource = source.ToString().ToLower();
                if (convertedSource == "yes" || convertedSource == "true"
                    || convertedSource == "y" || convertedSource == "on")
                    dest = true;
                else
                    dest = false;
            }

            return true;
        }
    }
}