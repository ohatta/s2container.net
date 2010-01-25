#region Copyright

/*
 * Copyright 2005-2010 the Seasar Foundation and the Others.
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
    /// �I�u�W�F�N�g��IList<typeparam name="T">�֕ϊ�����R���o�[�^�����N���X
    /// </summary>
    public class GenericsListConverter<T> : AbstractPropertyConverter 
        where T:class
    {
        protected override bool DoConvert(object source, ref object dest, Type expectType)
        {
            Debug.Assert(typeof(IList<T>).IsAssignableFrom(expectType)
                         , String.Format(DxoMessages.EDXO1002, "expectType", "IList<" + typeof(T).Name + ">"));
            //            Debug.Assert(typeof(IList<T>).IsAssignableFrom(expectType)
//                         , "expectType��IList<" + typeof(T).Name + ">�ƌ݊������Ȃ��Ă͂Ȃ�Ȃ�");

            if (dest == null)
            {
                if (expectType.IsClass && !expectType.IsAbstract)
                    dest = Activator.CreateInstance(expectType);
                else
                    throw new DxoException(String.Format(DxoMessages.EDXO0001, "expectType"));
//                throw new DxoException("expectType�͋�ۃN���X�ł͂Ȃ��̂Ŏ��̉����邱�Ƃ��ł��Ȃ�");
            }
            IList<T> result = dest as IList<T>;
            if (result != null)
            {
                result.Clear();

                if (source is IEnumerable)
                {
                    if (source is IList<T>)
                    {
                        foreach (T item in (source as IList<T>))
                        {
                            result.Add(item);
                        }
                        return true;
                    }
                    else if (source.GetType().IsArray)
                    {
                        //�v�f�̌^�Ɍ݊��������邩
                        Type elementType = source.GetType().GetElementType();
                        if (typeof(T).IsAssignableFrom(elementType))
                        {
                            foreach (T item in (T[])source)
                            {
                                result.Add(item);
                            }
                            return true;
                        }
                    }
                    else
                    {
                        foreach (object item in source as IEnumerable)
                        {
                            if (typeof (T).IsAssignableFrom(item.GetType()))
                                result.Add(item as T);
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
