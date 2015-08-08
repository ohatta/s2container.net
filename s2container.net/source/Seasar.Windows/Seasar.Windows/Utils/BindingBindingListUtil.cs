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
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using Seasar.Framework.Util;
using Seasar.Windows.Attr;

namespace Seasar.Windows.Seasar.Windows.Utils
{
    /// <summary>
    /// GridView�R���g���[����BindingList���o�C���h��������N���X
    /// </summary>
    public class BindingBindingListUtil : IBindingUtil
    {
        #region IBindingUtil Members

        /// <summary>
        /// �v���p�e�B�Ƀf�[�^��}������
        /// </summary>
        /// <param name="source">�f�[�^�\�[�X</param>
        /// <param name="info">�v���p�e�B���</param>
        /// <param name="control">�R���g���[��</param>
        /// <param name="attr">����</param>
        /// <param name="data">�}������f�[�^</param>
        /// <param name="row">�}���ʒu</param>
        /// <returns>�}������</returns>
        public int AddData(ref object source, PropertyInfo info, ref Control control, ControlAttribute attr, object data,
                           int row)
        {
            if (data == null)
                throw new ArgumentNullException(String.Format(SWFMessages.FSWF0001, "data"));

            var ret = 0;
            var target = info.GetValue(source, null);
            IList list = target as IBindingList;
            if (list != null)
            {
                if (row > list.Count)
                    throw new ArgumentOutOfRangeException(String.Format(SWFMessages.FSWF0003, "row"));

                list.Insert(row, data);

                var genericType = target.GetExType();
                var obj = ClassUtil.NewInstance(genericType);
                var obj2 = ClassUtil.NewInstance(genericType);
//                var obj = Activator.CreateInstance(genericType);
//                var obj2 = Activator.CreateInstance(genericType);

                PropertyUtil.SetValue(source, source.GetExType(), info.Name, info.PropertyType, obj);
//                info.SetValue(source, obj, null);
                control.DataBindings.Clear();

                var list2 = (IBindingList) obj2;
                foreach (var o in list)
                    list2.Add(o);

                PropertyUtil.SetValue(source, source.GetExType(), info.Name, info.PropertyType, obj2);
//                info.SetValue(source, obj2, null);
                control.DataBindings.Add(
                    attr.ControlProperty, source, info.Name, attr.FormattingEnabled, attr.UpdateMode, attr.NullValue,
                    attr.FormatString);

                ret = 1;
            }
            return ret;
        }

        /// <summary>
        /// �v���p�e�B�Ƀf�[�^���폜����
        /// </summary>
        /// <param name="source">�f�[�^�\�[�X</param>
        /// <param name="info">�v���p�e�B���</param>
        /// <param name="control">�R���g���[��</param>
        /// <param name="attr">����</param>
        /// <param name="row">�폜�ʒu</param>
        /// <returns>�폜����</returns>
        public int DeleteData(ref object source, PropertyInfo info, ref Control control, ControlAttribute attr, int row)
        {
            var ret = 0;
            var target = info.GetValue(source, null);
            var list = target as IBindingList;
            if (list != null)
            {
                if (row > list.Count - 1)
                    throw new ArgumentOutOfRangeException(String.Format(SWFMessages.FSWF0003, "row"));

                list.RemoveAt(row);

                var genericType = target.GetExType();

                var obj = ClassUtil.NewInstance(genericType);
                var obj2 = ClassUtil.NewInstance(genericType);
//                var obj = Activator.CreateInstance(genericType);
//                var obj2 = Activator.CreateInstance(genericType);

                var list2 = (IBindingList) obj2;
                foreach (var o in list)
                    list2.Add(o);
                PropertyUtil.SetValue(source, source.GetExType(), info.Name, info.PropertyType, obj);
//                info.SetValue(source, obj, null);
                control.DataBindings.Clear();


                PropertyUtil.SetValue(source, source.GetExType(), info.Name, info.PropertyType, obj2);
//                info.SetValue(source, obj2, null);
                control.DataBindings.Add(
                    attr.ControlProperty, source, info.Name, attr.FormattingEnabled, attr.UpdateMode,
                    attr.NullValue, attr.FormatString);

                ret = 1;
            }
            return ret;
        }

        /// <summary>
        /// �s���ړ�������
        /// </summary>
        /// <param name="source">�f�[�^�\�[�X</param>
        /// <param name="info">�v���p�e�B���</param>
        /// <param name="control">�R���g���[��</param>
        /// <param name="attr">�������</param>
        /// <param name="row">�Ώۍs</param>
        /// <param name="direction">�ړ��������</param>
        /// <returns>�폜�s��</returns>
        public void MoveRow(ref object source, PropertyInfo info, ref Control control, ControlAttribute attr, int row,
                            MovingDirection direction)
        {
            int targetRow;
            if (direction == MovingDirection.Upper)
                targetRow = row - 1;
            else
                targetRow = row + 1;

            var target = PropertyUtil.GetValue(source, source.GetExType(), info.Name);
//            var target = info.GetValue(source, null);
            IList list = target as IBindingList;
            if (list != null)
            {
                if (targetRow >= list.Count)
                    throw new ArgumentOutOfRangeException(String.Format(SWFMessages.FSWF0003, "row"));

                var bindingType = target.GetExType();
                var obj = ClassUtil.NewInstance(bindingType);
                var obj2 = ClassUtil.NewInstance(bindingType);
//                var obj = Activator.CreateInstance(bindingType);
//                var obj2 = Activator.CreateInstance(bindingType);

                PropertyUtil.SetValue(source, source.GetExType(), info.Name, info.PropertyType, obj);
//                info.SetValue(source, obj, null);
                control.DataBindings.Clear();

                var src = list[row];
                var list2 = (IBindingList) obj2;
                var pos = 0;
                foreach (var o in list)
                {
                    if (pos == targetRow)
                        list2.Add(src);
                    else if (pos != row && pos != targetRow)
                        list2.Add(o);
                    else if (pos == row && direction == MovingDirection.Upper)
                        list2.Add(list[row - 1]);
                    else if (pos == row && direction == MovingDirection.Lower)
                        list2.Add(list[row + 1]);

                    pos++;
                }

                PropertyUtil.SetValue(source, source.GetExType(), info.Name, info.PropertyType, obj2);
//                info.SetValue(source, obj2, null);
                control.DataBindings.Add(
                    attr.ControlProperty, source, info.Name, attr.FormattingEnabled, attr.UpdateMode, attr.NullValue,
                    attr.FormatString);
            }
        }

        #endregion
    }
}
