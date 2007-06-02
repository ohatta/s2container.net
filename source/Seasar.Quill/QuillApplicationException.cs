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
using Seasar.Framework.Message;
using System.Reflection;
using Seasar.Quill.Util;

namespace Seasar.Quill
{
    /// <summary>
    /// Quill�ŃA�v���P�[�V�����G���[�����������ꍇ�ɃX���[������O�N���X
    /// </summary>
    [Serializable]
    public class QuillApplicationException : ApplicationException
    {
        /// <summary>
        /// ���b�Z�[�W�R�[�h
        /// </summary>
        protected string messageCode;

        /// <summary>
        /// ���b�Z�[�W�ɖ��ߍ��ޒl�̔z��
        /// </summary>
        protected object[] args;

        /// <summary>
        /// ���b�Z�[�W(���b�Z�[�W�R�[�h���܂�)
        /// </summary>
        protected string message;

        /// <summary>
        /// �ȒP�ȃ��b�Z�[�W(���b�Z�[�W�R�[�h���܂܂Ȃ�)
        /// </summary>
        protected string simpleMessage;

        /// <summary>
        /// ���b�Z�[�W�R�[�h���w�肵��QuillApplicationException������������
        /// </summary>
        /// <param name="messageCode">���b�Z�[�W�R�[�h</param>
        public QuillApplicationException(string messageCode)
			: this(messageCode,null,null)
		{
		}

        /// <summary>
        /// ���b�Z�[�W�R�[�h�E���b�Z�[�W���ɖ��ߍ��ޕ�����̔z����w�肵��
        /// QuillApplicationException������������
        /// </summary>
        /// <param name="messageCode">���b�Z�[�W�R�[�h</param>
        /// <param name="args">���b�Z�[�W���ɖ��ߍ��ޕ�����̔z��</param>
		public QuillApplicationException(string messageCode,object[] args)
			: this(messageCode,args,null)
		{
		}

        /// <summary>
        /// ���b�Z�[�W�R�[�h�E���b�Z�[�W���ɖ��ߍ��ޕ�����̔z��E���ƂȂ�����O
        /// ���w�肵��QuillApplicationException������������
        /// </summary>
        /// <param name="messageCode">���b�Z�[�W�R�[�h</param>
        /// <param name="args">���b�Z�[�W���ɖ��ߍ��ޒl�̔z��</param>
        /// <param name="cause">���ƂȂ�����O</param>
        public QuillApplicationException(
            string messageCode, object[] args, Exception cause) : base(null, cause)
		{
            // ���b�Z�[�W�R�[�h���Z�b�g����
            this.messageCode = messageCode;

            // ���b�Z�[�W���ɖ��ߍ��ޒl�̔z����Z�b�g����
            this.args = args;
            
            // ���b�Z�[�W���Z�b�g����
            simpleMessage = MessageUtil.GetSimpleMessage(messageCode, args);

            // ���b�Z�[�W�R�[�h�t���̃��b�Z�[�W���Z�b�g����
            message = "[" + messageCode + "]" + simpleMessage;
        }

        /// <summary>
        /// ���b�Z�[�W�R�[�h���擾����
        /// </summary>
        /// <value>���b�Z�[�W�R�[�h</value>
        public string MessageCode
        {
            get { return messageCode; }
        }

        /// <summary>
        /// ���b�Z�[�W�ɖ��ߍ��ޒl�̔z����擾����
        /// </summary>
        /// <value>���b�Z�[�W�ɖ��ߍ��ޒl�̔z��</value>
        public object[] Args
        {
            get { return args; }
        }

        /// <summary>
        /// ���b�Z�[�W(���b�Z�[�W�R�[�h���܂�)���擾����
        /// </summary>
        /// <value>���b�Z�[�W(���b�Z�[�W�R�[�h���܂�)</value>
        public override string Message
        {
            get { return message; }
        }

        /// <summary>
        /// �ȒP�ȃ��b�Z�[�W(���b�Z�[�W�R�[�h���܂܂Ȃ�)���擾����
        /// </summary>
        /// <value>�ȒP�ȃ��b�Z�[�W(���b�Z�[�W�R�[�h���܂܂Ȃ�)</value>
        public string SimpleMessage
        {
            get { return simpleMessage; }
        }
    }
}
