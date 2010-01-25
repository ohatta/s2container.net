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

namespace Seasar.Dxo.Converter.Impl
{
    /// <summary>
    /// �I�u�W�F�N�g��T[]�Ȕz��ɕϊ�����R���o�[�^�����N���X
    /// </summary>
    public class ArrayConverter<T> : AbstractPropertyConverter
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
            if (dest == null)
            {
                //�z��^�́Anull���Z�b�g����Ă��邱�Ƃ�����̂ŃC���X�^���X�𐶐����Ă��
                dest = new T[0];
            }
            T[] result = (dest as T[]);
            if (result != null)
                Array.Clear(result, 0, result.Length);
            else
                throw new ArgumentNullException("dest");

            if (source is ICollection<T>)
            {
                ICollection<T> sourceCollection = source as ICollection<T>;
                //�z���ReNew
                if (sourceCollection.Count > result.Length)
                {
                    result = new T[sourceCollection.Count];
                    dest = result; //�z�����蒼�����̂œ]�L���K�v
                }
                //�W�F�l���b�N�����ڃR�s�[�ł���
                sourceCollection.CopyTo(result, 0);
                return true;
            }
                //�R���N�V�������Ώ�
            else if (source is ICollection)
            {
                ICollection sourceCollection = source as ICollection;
                //�z���ReNew
                if (sourceCollection.Count > result.Length)
                {
                    result = new T[sourceCollection.Count];
                    dest = result; //�z�����蒼�����̂œ]�L���K�v
                }
                if (source.GetType().IsArray)
                {
                    if (source is T[])
                    {
                        //�^��v�̔z��Ȃ�Β��ڃR�s�[�ł���
                        (source as T[]).CopyTo(result, 0);
                        return true;
                    }
                    else
                    {
                        //�v�f�̌^�Ɍ݊��������邩���ׂĂ���o���N�R�s�[
                        Type elementType = source.GetType().GetElementType();
                        if (typeof (T).IsAssignableFrom(elementType))
                        {
                            if (source is Array)
                            {
                                (source as Array).CopyTo(result, 0);
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    //�w�e���W�j�A�X�ȃR���N�V�����̏ꍇ���l����1�A�C�e�����Ɍ^�`�F�b�N���K�v
                    int i = 0;
                    foreach (object item in sourceCollection)
                    {
                        //�v�f�̌^�Ɍ݊��������邩���ׂăR�s�[
                        if (item.GetType().IsAssignableFrom(typeof (T)))
                        {
                            result.SetValue(item, i);
                            i++;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
