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
using System.Diagnostics;
using Seasar.Dxo.Exception;
using Seasar.Framework.Util;

namespace Seasar.Dxo.Converter.Impl
{
    /// <summary>
    /// �I�u�W�F�N�g��IDictionary�֕ϊ�����R���o�[�^�����N���X
    /// </summary>
    public class DictionaryConverter : AbstractPropertyConverter 
    {
        protected override bool DoConvert(object source, ref object dest, Type expectType)
        {
            Debug.Assert(typeof(IDictionary).IsAssignableFrom(expectType),
                        String.Format(DxoMessages.EDXO1002, "expectType", "IDictionary"));
//            Debug.Assert(typeof(IDictionary).IsAssignableFrom(expectType),
//                        "expectType��IDictionary�ƌ݊������Ȃ��Ă͂Ȃ�Ȃ�");
            
            if (dest == null)
            {
                if (expectType.IsClass && !expectType.IsAbstract)
                {
                    dest = ClassUtil.NewInstance(expectType);
//                    dest = Activator.CreateInstance(expectType);
                }
                else
                {
                    throw new DxoException(
                        String.Format(DxoMessages.EDXO0001, expectType.Name));
//                    throw new DxoException(
//                        expectType.Name + "�͋�ۃN���X�ł͂Ȃ��̂Ŏ��̉����邱�Ƃ��ł��Ȃ�");
                }
            }

            var target = dest as IDictionary;
            var src = source as IDictionary;
            if (src == null && target != null)
            {
                target.Clear();

                var properties = source.GetExType().GetProperties();
                foreach (var info in properties)
                {
                    target.Add(info.Name, info.GetValue(source, null));
                }

                return true;
            }
            return false;
        }
    }

}
