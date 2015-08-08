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
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using Seasar.Framework.Util;
using Seasar.Windows.Attr;

namespace Seasar.Windows.Seasar.Windows.Utils
{
    /// <summary>
    /// GridView�R���g���[����DataTable���o�C���h��������N���X
    /// </summary>
    public class BindingDataTableUtil : IBindingUtil
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
        public int AddData(ref object source, PropertyInfo info, ref Control control, ControlAttribute attr, object data, int row)
        {
            var ret = 0;
            var target = info.GetValue(source, null);
            var dt = target as DataTable;
            if (dt != null)
            {
                if (row > dt.Rows.Count - 1)
                    throw new ArgumentOutOfRangeException(String.Format(SWFMessages.FSWF0003, "row"));

                var newTable = new DataTable(dt.TableName);
                // ��ݒ�
                var columns = dt.Columns;
                foreach (DataColumn column in columns)
                {
                    var newColumn = new DataColumn();

                    var type = newColumn.GetExType();
                    var pis = type.GetProperties();
                    foreach (var pi in pis)
                    {
                        var type2 = column.GetExType();
                        var pis2 = type2.GetProperties();
                        foreach (var pi2 in pis2)
                        {
                            if (pi.Name == pi2.Name)
                            {
                                var mis = pi.GetAccessors();
                                var isSetter = false;
                                foreach (var mi in mis)
                                {
                                    if (mi.Name.Substring(0, 3).ToLower() == "set")
                                        isSetter = true;
                                }
                                if (isSetter)
                                {
                                    var obj = pi2.GetValue(column, null);
                                    pi.SetValue(newColumn, obj, null);
                                }
                            }
                        }
                    }

                    newTable.Columns.Add(newColumn);
                }

                // �s�ǉ�
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    var addRow = newTable.NewRow();
                    var rowData = dt.Rows[i];
                    var newRow = data as DataRow;
                    if (newRow != null)
                    {
                        for (var j = 0; j < dt.Columns.Count; j++)
                        {
                            addRow[j] = newRow[j];
                        }
                    }

                    if (i == row)
                        newTable.Rows.Add(addRow);

                    addRow = newTable.NewRow();
                    for (var j = 0; j < dt.Columns.Count; j++)
                    {
                        addRow[j] = rowData[j];
                    }
                    newTable.Rows.Add(addRow);
                }
                info.SetValue(source, new DataTable(dt.TableName), null);
                control.DataBindings.Clear();

                info.SetValue(source, newTable, null);
                control.DataBindings.Add(
                    attr.ControlProperty, source, info.Name, attr.FormattingEnabled, attr.UpdateMode,
                    attr.NullValue, attr.FormatString);

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
            var dt = target as DataTable;
            if (dt != null)
            {
                if (row > dt.Rows.Count - 1)
                    throw new ArgumentOutOfRangeException(String.Format(SWFMessages.FSWF0003, "row"));

                dt.Rows.RemoveAt(row);
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

            var target = info.GetValue(source, null);
            var dt = target as DataTable;
            if (dt != null)
            {
                if (targetRow >= dt.Rows.Count)
                    throw new ArgumentOutOfRangeException(String.Format(SWFMessages.FSWF0003, "row"));

                var newTable = new DataTable(dt.TableName);
                // ��ݒ�
                var columns = dt.Columns;
                foreach (DataColumn column in columns)
                {
                    var newColumn = new DataColumn();

                    var type = newColumn.GetExType();
                    var pis = type.GetProperties();
                    foreach (var pi in pis)
                    {
                        var type2 = column.GetExType();
                        var pis2 = type2.GetProperties();
                        foreach (var pi2 in pis2)
                        {
                            if (pi.Name == pi2.Name)
                            {
                                var mis = pi.GetAccessors();
                                var isSetter = false;
                                foreach (var mi in mis)
                                {
                                    if (mi.Name.Substring(0, 3).ToLower() == "set")
                                        isSetter = true;
                                }
                                if (isSetter)
                                {
                                    var obj = pi2.GetValue(column, null);
                                    pi.SetValue(newColumn, obj, null);
                                }
                            }
                        }
                    }

                    newTable.Columns.Add(newColumn);
                }

                // �s�ړ�
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow rowData;
                    if (i == targetRow)
                        rowData = dt.Rows[row];
                    else if (i == row && direction == MovingDirection.Upper)
                        rowData = dt.Rows[row - 1];
                    else if (i == row && direction == MovingDirection.Lower)
                        rowData = dt.Rows[row + 1];
                    else
                        rowData = dt.Rows[i];

                    var addRow = newTable.NewRow();
                    for (var j = 0; j < dt.Columns.Count; j++)
                    {
                        addRow[j] = rowData[j];
                    }
                    newTable.Rows.Add(addRow);
                }
                info.SetValue(source, new DataTable(dt.TableName), null);
                control.DataBindings.Clear();

                info.SetValue(source, newTable, null);
                control.DataBindings.Add(
                    attr.ControlProperty, source, info.Name, attr.FormattingEnabled, attr.UpdateMode,
                    attr.NullValue, attr.FormatString);

            }
        }

        #endregion
    }
}
