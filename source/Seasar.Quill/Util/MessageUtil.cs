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

using System.Resources;
using System.Reflection;

namespace Seasar.Quill.Util
{
    /// <summary>
    /// Quill�ō쐬����郁�b�Z�[�W�������N���X
    /// </summary>
    public class MessageUtil
    {
        /// <summary>
        /// Quill�Ŏg�p���郁�b�Z�[�W���i�[���Ă���ResourceManager
        /// </summary>
        private static readonly ResourceManager MESSAGES_RESOURCE_MANAGER = 
            new ResourceManager("Seasar.Quill.QLLMessages", 
            Assembly.GetExecutingAssembly());

        // object�̋�̔z��
        private static readonly object[] EMPTY_ARRAY = new object[0];

        /// <summary>
        /// ���b�Z�[�W�R�[�h���܂܂Ȃ����b�Z�[�W���擾����
        /// </summary>
        /// <param name="messageCode">���b�Z�[�W�R�[�h</param>
        /// <param name="arguments">���b�Z�[�W���ɖ��ߍ��ޒl�̔z��</param>
        /// <returns>���b�Z�[�W�R�[�h���܂܂Ȃ����b�Z�[�W</returns>
        public static string GetSimpleMessage(string messageCode, object[] arguments)
        {
            // ���b�Z�[�W�̃t�H�[�}�b�g��ResourceManager����擾����
            string format = MESSAGES_RESOURCE_MANAGER.GetString(messageCode);

            if (format == null)
            {
                // ���b�Z�[�W��������Ȃ��ꍇ�͗�O���X���[����
                throw new QuillApplicationException(
                    "EQLL0000", "message not found.");
            }

            if (arguments == null)
            {
                // ���b�Z�[�W���ɖ��ߍ��ޒl��null�̏ꍇ�͋�̔z��ɕϊ�����
                arguments = EMPTY_ARRAY;
            }
            
            // �t�H�[�}�b�g�ɒl�𖄂ߍ��݃��b�Z�[�W���쐬����
            string message = string.Format(format, arguments);

            // ���b�Z�[�W��Ԃ�
            return message;
        }

        /// <summary>
        /// ���b�Z�[�W�R�[�h���܂ރ��b�Z�[�W���擾����
        /// </summary>
        /// <param name="messageCode">���b�Z�[�W�R�[�h</param>
        /// <param name="arguments">���b�Z�[�W���ɖ��ߍ��ޒl�̔z��</param>
        /// <returns>���b�Z�[�W�R�[�h���܂܂Ȃ����b�Z�[�W</returns>
        public static string GetMessage(string messageCode, object[] arguments)
        {
            // ���b�Z�[�W�R�[�h�t���̃��b�Z�[�W��Ԃ�
            return "[" + messageCode + "]" + GetSimpleMessage(messageCode, arguments);
        }
    }
}
