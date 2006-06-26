#region Copyright

/*
 * Copyright 2006 the Seasar Foundation and the Others.
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
 * 
 */

#endregion

using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace Seasar.Windows.Utils
{
    /// <summary>
    /// �ϊ��p���[�e�B���e�B�N���X
    /// </summary>
    public class Converter
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        private Converter()
        {
            ;
        }

        /// <summary>
        /// PONO��DataSet�ɕϊ�����N���X
        /// </summary>
        /// <param name="type">PONO�N���X</param>
        /// <param name="list"></param>
        /// <returns>�������ꂽDataSet</returns>
        /// <remarks>DataSet�Ɋ܂܂��DataTable�̖��̂�PONO�N���X��</remarks>
        public static DataSet ConvertPONOToDataSet(Type type, IList list)
        {
            if ( type == null )
                throw new ArgumentNullException("type");
            if ( list == null )
                throw new ArgumentNullException("list");

            DataTable dt = new DataTable(type.Name);

            PropertyInfo[] pis = type.GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                dt.Columns.Add(pi.Name, pi.GetType());
            }

            foreach (object bean in list)
            {
                DataRow row = dt.NewRow();

                foreach (PropertyInfo pi in pis)
                {
                    PropertyInfo p = bean.GetType().GetProperty(pi.Name);
                    row[pi.Name] = p.GetValue(bean, null);
                }

                dt.Rows.Add(row);
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            return ds;
        }
    }
}