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
using System.Diagnostics;

namespace Seasar.Dxo.Converter
{
    /// <summary>
    /// ���f���𑊌ݕϊ����邽�߂̃R���o�[�^���ۃN���X
    /// </summary>
    public abstract class AbstractPropertyConverter : IPropertyConverter
    {
        private string _formatString;

        public string Format
        {
            get { return _formatString; }
            set { _formatString = value; }
        }

        /// <summary>
        /// �v���p�e�B�̃R���o�[�g���O�ɔ�������C�x���g
        /// </summary>
        public event EventHandler<ConvertEventArgs> PrepareConvert;

        /// <summary>
        /// �R���o�[�g�����������ۂɔ�������C�x���g
        /// </summary>
        public event EventHandler<ConvertEventArgs> ConvertCompleted;

        /// <summary>
        /// �R���o�[�g�����s�����ۂɔ�������C�x���g
        /// </summary>
        public event EventHandler<ConvertEventArgs> ConvertFail;

        /// <summary>
        /// �I�u�W�F�N�g�̃v���p�e�B��C�ӂ̌^�ɕϊ����܂�
        /// </summary>
        /// <param name="propertyName">�v���p�e�B��</param>
        /// <param name="source">�ϊ����̃I�u�W�F�N�g</param>
        /// <param name="dest">�ϊ���̃I�u�W�F�N�g</param>
        /// <param name="expectType">�ϊ���̃I�u�W�F�N�g�Ɋ��҂���Ă���^</param>
        public void Convert(string propertyName, object source, ref object dest, Type expectType)
        {
            if (PrepareConvert != null)
            {
                PrepareConvert(this, new ConvertEventArgs(propertyName, source, ref dest, expectType));
            }

            if (this.DoConvert(source, ref dest, expectType))
            {
                //�R���o�[�g����
                if (ConvertCompleted != null)
                {
                    ConvertCompleted(this, new ConvertEventArgs(propertyName, source, ref dest, expectType));
                }
            }
            else
            {
                Debug.WriteLine("### Property Conversion fail!");
                Debug.WriteLine("         property PropertyConverter:" + this.GetType().Name);
                Debug.WriteLine("         property Name     :" + propertyName);
                Debug.WriteLine("             source Type   :" + ((source != null)
                                                                      ? source.GetType().Name
                                                                      : "null"));
                Debug.WriteLine("             source Value  :" + ((source != null)
                                                                      ? source.ToString()
                                                                      : "null"));
                Debug.WriteLine("            expected Type  :" + expectType.Name);
                //�R���o�[�g���s
                if (ConvertFail != null)
                {
                    ConvertFail(this, new ConvertEventArgs(propertyName, source, ref dest, expectType));
                }
            }
        }


        /// <summary>
        /// �I�u�W�F�N�g�̃v���p�e�B��C�ӂ̌^�ɕϊ����܂�
        /// (���ۃ��\�b�h�͔h���N���X�ŕK���I�[�o���C�h����܂�)
        /// </summary>
        /// <param name="source">�ϊ����̃I�u�W�F�N�g</param>
        /// <param name="dest">�ϊ���̃I�u�W�F�N�g</param>
        /// <param name="expectType">�ϊ���̃I�u�W�F�N�g�Ɋ��҂���Ă���^</param>
        /// <returns>bool �ϊ������������ꍇ�ɂ�true</returns>
        protected abstract bool DoConvert(object source, ref object  dest, Type expectType);
    }
}