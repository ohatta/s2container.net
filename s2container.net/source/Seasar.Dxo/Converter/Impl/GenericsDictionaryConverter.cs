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
using Seasar.Dxo.Exception;
using Seasar.Framework.Util;

namespace Seasar.Dxo.Converter.Impl
{
    /// <summary>
    /// �I�u�W�F�N�g��IDictionary<typeparam name="TK"/><typeparam name="TV"/>�֕ϊ�����R���o�[�^�����N���X
    /// </summary>
    public class GenericsDictionaryConverter<TK, TV> : AbstractPropertyConverter 
        where TK: class
        where TV: class
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
            Debug.Assert(typeof(IDictionary<TK, TV>).IsAssignableFrom(expectType)
                         , string.Format(DxoMessages.EDXO1003, "expectType", typeof(TK).Name, typeof(TV).Name));
//            Debug.Assert(typeof(IDictionary<K, V>).IsAssignableFrom(expectType)
//                         , string.Format("expectType��IDictionary<{0}{1}>�ƌ݊������Ȃ��Ă͂Ȃ�Ȃ�"
//                         , typeof(K).Name, typeof(V).Name));

            if (dest == null)
            {
                if (expectType.IsClass && !expectType.IsAbstract)
                    dest = ClassUtil.NewInstance(expectType);
//                    dest = Activator.CreateInstance(expectType);
                else
                    throw new DxoException(String.Format(DxoMessages.EDXO0001, "expectType"));
//                throw new DxoException("expectType�͋�ۃN���X�ł͂Ȃ��̂Ŏ��̉����邱�Ƃ��ł��Ȃ�");
            }
            var result = dest as IDictionary<TK, TV>;
            if (result != null)
            {
                result.Clear();

                var vs = source as IDictionary<TK, TV>;
                if (vs != null)
                {
                    foreach (var pair in vs)
                    {
                        result.Add(pair.Key, pair.Value);
                    }
                    return true;
                }
                else
                {
                    var dictionary = source as IDictionary;
                    if (dictionary != null)
                    {
                        foreach ( var key in dictionary.Keys)
                        {
                            var value = dictionary[key];
                            if (typeof (TK).IsAssignableFrom(key.GetExType())
                                && typeof (TV).IsAssignableFrom(value.GetExType()))
                                result.Add((TK) key, (TV) value);
                            else
                                return false;
                        }
                        return true;
                    }
                }
            }
            return false;
        }
    }

}
