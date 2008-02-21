#region Copyright
/*
 * Copyright 2005-2008 the Seasar Foundation and the Others.
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
using Seasar.Framework.Util;
using Seasar.Quill.Database.Tx;
using Seasar.Quill.Database.Tx.Impl;
using Seasar.Quill.Util;
using Seasar.Quill.Xml;

namespace Seasar.Quill.Attrs
{
    /// <summary>
    /// �g�����U�N�V�������E���w�肷�邽�߂̑���
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface |
       AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class TransactionAttribute : Attribute
    {
        private Type _handlerType = null;

        public virtual Type TransactionSettingType
        {
            get
            {
                return _handlerType;
            }
        }

        /// <summary>
        /// �f�t�H���g�R���X�g���N�^
        /// (QuillLocalRequiredTxInterceptor���g���܂�)
        /// </summary>
        public TransactionAttribute()
        {
            QuillSection section = QuillSectionHandler.GetQuillSection();
            if (section == null || string.IsNullOrEmpty(section.TransactionSetting))
            {
                //  ���������ɂ��w���app.config�ɂ��ݒ肪�Ȃ����
                //  �f�t�H���g�̃g�����U�N�V�����ݒ���g��
                SetHandlerType(typeof(TypicalTransactionSetting));
            }
            else
            {
                string typeName = section.TransactionSetting;
                if (TypeUtil.HasNamespace(typeName) == false)
                {
                    //  ���O��ԂȂ��̏ꍇ�͊���̖��O��Ԃ���
                    typeName = string.Format("{0}.{1}",
                        QuillConstants.NAMESPACE_TXSETTING, typeName);
                }
                Type settingType = ClassUtil.ForName(typeName);
                    SetHandlerType(settingType);
            }
        }

        /// <summary>
        /// �J�X�^���R���X�g���N�^
        /// (�w�肵��Interceptor���g���܂�)
        /// (AbstractQuillTransactionInterceptor�T�u�N���X�ł͂Ȃ��ꍇ���s����O�𓊂��܂��j
        /// </summary>
        /// <param name="handlerType"></param>
        public TransactionAttribute(Type handlerType)
        {
            SetHandlerType(handlerType);
        }

        /// <summary>
        /// �g�p����g�����U�N�V�����ݒ�N���X�̐ݒ�
        /// </summary>
        /// <param name="type"></param>
        /// <exception cref="">ITransactionSetting�����N���X�łȂ��Ƃ�</exception>
        protected virtual void SetHandlerType(Type type)
        {
            if(typeof(ITransactionSetting).IsAssignableFrom(type))
            {
                _handlerType = type;
            }
            else
            {
                throw new QuillApplicationException("EQLL0026", type.Name);
            }
        }
    }
}
