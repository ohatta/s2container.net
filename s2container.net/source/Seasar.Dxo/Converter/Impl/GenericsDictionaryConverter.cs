#region Copyright

/*
 * Copyright 2005-2013 the Seasar Foundation and the Others.
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

namespace Seasar.Dxo.Converter.Impl
{
    /// <summary>
    /// �I�u�W�F�N�g��IDictionary<typeparam name="K"><typeparam name="V">�֕ϊ�����R���o�[�^�����N���X
    /// </summary>
    public class GenericsDictionaryConverter<K, V> : AbstractPropertyConverter 
        where K: class
        where V: class
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
            Debug.Assert(typeof(IDictionary<K, V>).IsAssignableFrom(expectType)
                         , string.Format(DxoMessages.EDXO1003, "expectType", typeof(K).Name, typeof(V).Name));
//            Debug.Assert(typeof(IDictionary<K, V>).IsAssignableFrom(expectType)
//                         , string.Format("expectType��IDictionary<{0}{1}>�ƌ݊������Ȃ��Ă͂Ȃ�Ȃ�"
//                         , typeof(K).Name, typeof(V).Name));

            if (dest == null)
            {
                if (expectType.IsClass && !expectType.IsAbstract)
                    dest = Activator.CreateInstance(expectType);
                else
                    throw new DxoException(String.Format(DxoMessages.EDXO0001, "expectType"));
//                throw new DxoException("expectType�͋�ۃN���X�ł͂Ȃ��̂Ŏ��̉����邱�Ƃ��ł��Ȃ�");
            }
            IDictionary<K, V> result = dest as IDictionary<K, V>;
            if (result != null)
            {
                result.Clear();

                if (source is IDictionary<K, V>)
                {
                    foreach (KeyValuePair<K, V> pair in (source as IDictionary<K, V>))
                    {
                        result.Add(pair.Key, pair.Value);
                    }
                    return true;
                }
                else if (source is IDictionary)
                {
                    foreach ( object key in (source as IDictionary).Keys)
                    {
                        object value = (source as IDictionary)[key];
                        if (typeof (K).IsAssignableFrom(key.GetType())
                            && typeof (V).IsAssignableFrom(value.GetType()))
                            result.Add((K) key, (V) value);
                        else
                            return false;
                    }
                    return true;
                }
            }
            return false;
        }
    }

}
