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
using System.Text;

namespace Seasar.Windows.Utils
{
    /// <summary>
    /// ���̓`�F�b�N�p���[�e�B���e�B�N���X
    /// </summary>
    public sealed class Validator
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        private Validator()
        {
            ;
        }

        /// <summary>
        /// MS932�ł̕�����̒������擾����
        /// </summary>
        /// <param name="src">�������v�Z���镶����</param>
        /// <returns>����</returns>
        public static int GetLengthBySJIS(string src)
        {
            // Shift_JIS = 932
            Encoding enc = Encoding.GetEncoding(932);

            return (enc.GetByteCount(src));
        }

        /// <summary>
        /// ������̒������擾����
        /// </summary>
        /// <param name="src">�������v�Z���镶����</param>
        /// <param name="encoding">�����R�[�h</param>
        /// <returns>����</returns>
        public static int GetLength(string src, Encoding encoding)
        {
            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }

            return (encoding.GetByteCount(src));
        }

        /// <summary>
        /// ���������͈̔͂Ɏ��܂��Ă��邩���`�F�b�N����
        /// </summary>
        /// <param name="src">�`�F�b�N�Ώە�����</param>
        /// <param name="lower">�Œ�̒���</param>
        /// <param name="upper">�Œ��̒���</param>
        /// <returns></returns>
        public static bool IsInRangeBySJIS(string src, int lower, int upper)
        {
            int length = GetLengthBySJIS(src);
            if (length < lower || upper < length)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// ���������͈̔͂Ɏ��܂��Ă��邩���`�F�b�N����
        /// </summary>
        /// <param name="src">�`�F�b�N�Ώە�����</param>
        /// <param name="lower">�Œ�̒���</param>
        /// <param name="upper">�Œ��̒���</param>
        /// <returns></returns>
        /// <param name="encoding">�����G���R�[�h</param>
        public static bool IsInRange(string src, int lower, int upper, Encoding encoding)
        {
            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }

            int length = GetLength(src, encoding);
            if (length < lower || upper < length)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// �l���͈͓��Ɏ��܂��Ă��邩���`�F�b�N����
        /// </summary>
        /// <param name="value">�`�F�b�N�Ώې��l</param>
        /// <param name="lower">�ŏ��̒l</param>
        /// <param name="upper">�ő�̒l</param>
        /// <returns></returns>
        public static bool IsInRange(long value, long lower, long upper)
        {
            if (value < lower || upper < value)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// �l���͈͓��Ɏ��܂��Ă��邩���`�F�b�N����
        /// </summary>
        /// <param name="value">�`�F�b�N�Ώې��l</param>
        /// <param name="lower">�ŏ��̒l</param>
        /// <param name="upper">�ő�̒l</param>
        /// <returns></returns>
        public static bool IsInRange(int value, int lower, int upper)
        {
            if (value < lower || upper < value)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// �l���͈͓��Ɏ��܂��Ă��邩���`�F�b�N����
        /// </summary>
        /// <param name="value">�`�F�b�N�Ώې��l</param>
        /// <param name="lower">�ŏ��̒l</param>
        /// <param name="upper">�ő�̒l</param>
        /// <returns></returns>
        public static bool IsInRange(double value, double lower, double upper)
        {
            if (value < lower || upper < value)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// ���t���r����
        /// </summary>
        /// <param name="value">�`�F�b�N�Ώۓ��t</param>
        /// <param name="compare">��r���t</param>
        /// <returns>�������Ȃ�0�A�ŏ����t����r���t���ȑO�Ȃ�}�C�i�X�A�t�Ȃ琳</returns>
        public static int CompareDays(DateTime value, DateTime compare)
        {
            DateTime firstDate = new DateTime(value.Year, value.Month, value.Day, 0, 0, 0);
            DateTime secondDate = new DateTime(compare.Year, compare.Month, compare.Day, 0, 0, 0);
            if (firstDate.Ticks == secondDate.Ticks)
            {
                return 0;
            }
            else
            {
                return (secondDate.Subtract(firstDate).Days);
            }
        }

        /// <summary>
        /// �����r����
        /// </summary>
        /// <param name="value">�`�F�b�N�Ώۓ��t</param>
        /// <param name="compare">��r���t</param>
        /// <returns>�������Ȃ�0�A�ŏ����t����r���t���ȑO�Ȃ�}�C�i�X�A�t�Ȃ琳</returns>
        public static int CompareMonth(DateTime value, DateTime compare)
        {
            DateTime firstDate = new DateTime(value.Year, value.Month, value.Day, 0, 0, 0);
            DateTime secondDate = new DateTime(compare.Year, compare.Month, compare.Day, 0, 0, 0);
            if (firstDate.Year == secondDate.Year && firstDate.Month == secondDate.Month)
            {
                return 0;
            }
            else
            {
                return ((secondDate.Year - firstDate.Year) * 12 + (secondDate.Month - firstDate.Month));
            }
        }

        /// <summary>
        /// �N���r����
        /// </summary>
        /// <param name="value">�`�F�b�N�Ώۓ��t</param>
        /// <param name="compare">��r���t</param>
        /// <returns>�������Ȃ�0�A�ŏ����t����r���t���ȑO�Ȃ�}�C�i�X�A�t�Ȃ琳</returns>
        public static int CompareYear(DateTime value, DateTime compare)
        {
            DateTime firstDate = new DateTime(value.Year, value.Month, value.Day, 0, 0, 0);
            DateTime secondDate = new DateTime(compare.Year, compare.Month, compare.Day, 0, 0, 0);

            return (secondDate.Year - firstDate.Year);
        }

        /// <summary>
        /// ���m�ɓ��t���r����
        /// </summary>
        /// <param name="value">�`�F�b�N�Ώۓ��t</param>
        /// <param name="compare">��r���t</param>
        /// <returns>�����~���b�Ȃ�0�A�ŏ����t����r���t���ȑO�Ȃ�}�C�i�X�A�t�Ȃ琳</returns>
        public static long CompareStrict(DateTime value, DateTime compare)
        {
            return (value.Ticks - compare.Ticks);
        }
    }
}
