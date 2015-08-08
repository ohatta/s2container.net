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

using System.Collections;
using System.Data;
using MbUnit.Framework;
using Seasar.Extension.DataSets.Types;
using Seasar.Framework.Util;
#if NET_4_0
using Gallio.Framework.Assertions;
#else
#region NET2.0
using MbUnit.Core.Exceptions;
#endregion
#endif

namespace Seasar.Extension.Unit
{
    public sealed class S2Assert
    {
        private S2Assert()
        {
        }

        #region AreEqual

        /// <summary>
        /// DataSet���m���r���܂��B
        /// 
        /// �J�����̕��я��͔�r�ɉe�����܂���B 
        /// ���l�͑S��decimal�Ƃ��Ĕ�r���܂��B
        /// </summary>
        /// <param name="expected">�\���l</param>
        /// <param name="actual">���ےl</param>
        public static void AreEqual(DataSet expected, DataSet actual)
        {
            AreEqual(expected, actual, null);
        }

        /// <summary>
        /// DataSet���m���r���܂��B
        /// 
        /// �J�����̕��я��͔�r�ɉe�����܂���B 
        /// ���l�͑S��decimal�Ƃ��Ĕ�r���܂��B
        /// </summary>
        /// <param name="expected">�\���l</param>
        /// <param name="actual">���ےl</param>
        /// <param name="message">assert���s���̃��b�Z�[�W</param>
        public static void AreEqual(DataSet expected, DataSet actual, string message)
        {
            message = message ?? string.Empty;

            Assert.AreEqual(
                expected.Tables.Count,
                actual.Tables.Count,
                message + ":TableSize"
                );
            for (var i = 0; i < expected.Tables.Count; ++i)
            {
                AreEqual(expected.Tables[i], actual.Tables[i], message);
            }
        }

        /// <summary>
        /// DataTable���m���r���܂��B
        /// 
        /// �J�����̕��я��͔�r�ɉe�����܂���B 
        /// ���l�͑S��decimal�Ƃ��Ĕ�r���܂��B
        /// </summary>
        /// <param name="expected">�\���l</param>
        /// <param name="actual">���ےl</param>
        public static void AreEqual(DataTable expected, DataTable actual)
        {
            AreEqual(expected, actual, null);
        }

        /// <summary>
        /// DataTable���m���r���܂��B
        /// 
        /// �J�����̕��я��͔�r�ɉe�����܂���B 
        /// ���l�͑S��decimal�Ƃ��Ĕ�r���܂��B
        /// </summary>
        /// <param name="expected">�\���l</param>
        /// <param name="actual">���ےl</param>
        /// <param name="message">assert���s���̃��b�Z�[�W</param>
        public static void AreEqual(DataTable expected, DataTable actual, string message)
        {
            message = message ?? string.Empty;
            message = message + ":TableName=" + expected.TableName;
            Assert.AreEqual(expected.Rows.Count, actual.Rows.Count, message + ":RowSize");
            for (var i = 0; i < expected.Rows.Count; ++i)
            {
                var expectedRow = expected.Rows[i];
                var actualRow = actual.Rows[i];
                IList errorMessages = new ArrayList();
                for (var j = 0; j < expected.Columns.Count; ++j)
                {
                    try
                    {
                        var columnName = expected.Columns[j].ColumnName;
                        var expectedValue = expectedRow[columnName];
                        var ct = ColumnTypes.GetColumnType(expectedValue);
                        var actualValue = actualRow[DataTableUtil.GetColumn(actual, columnName)];
                        if (!ct.Equals1(expectedValue, actualValue))
                        {
                            Assert.AreEqual(
                                expectedValue,
                                actualValue,
                                message + ":Row=" + i + ":_columnName=" + columnName
                                );
                        }
                    }
                    catch (AssertionException e)
                    {
                        errorMessages.Add(e.Message);
                    }
                }
                if (errorMessages.Count != 0)
                {
                    Assert.Fail(message + ToStringUtil.ToString(errorMessages));
                }
            }
        }

        /// <summary>
        /// �I�u�W�F�N�g��DataSet�Ɣ�r���܂��B
        /// 
        /// �I�u�W�F�N�g�́ABean�AIDictionary�ABean��IList�AIDictionary��IList�̂����ꂩ �łȂ���΂Ȃ�܂���B
        /// 
        /// Bean�̏ꍇ�̓v���p�e�B�����AMap�̏ꍇ�̓L�[���J�������Ƃ��� ��r���܂��B
        /// �J�����̕��я��͔�r�ɉe�����܂���B 
        /// ���l�͑S��decimal�Ƃ��Ĕ�r���܂��B
        /// </summary>
        /// <param name="expected">�\���l</param>
        /// <param name="actual">���ےl</param>
        public static void AreEqual(DataSet expected, object actual)
        {
            AreEqual(expected, actual, null);
        }

        /// <summary>
        /// �I�u�W�F�N�g��DataSet�Ɣ�r���܂��B
        /// 
        /// �I�u�W�F�N�g�́Aobject�AIDictionary�Aobject��IList�AIDictionary��IList�̂����ꂩ �łȂ���΂Ȃ�܂���B
        /// 
        /// object�̏ꍇ�̓v���p�e�B�����AIDictionary�̏ꍇ�̓L�[���J�������Ƃ��� ��r���܂��B
        /// �J�����̕��я��͔�r�ɉe�����܂���B 
        /// </summary>
        /// <param name="expected">�\���l</param>
        /// <param name="actual">���ےl</param>
        /// <param name="message">assert���s���̃��b�Z�[�W</param>
        public static void AreEqual(DataSet expected, object actual, string message)
        {
            if (expected == null || actual == null)
            {
                Assert.AreEqual(expected, actual, message);
                return;
            }

            var objects = actual as object[];
            if (objects != null)
            {
                AreEqual(expected, new ArrayList(objects), message);
            }
            else if (actual is IList)
            {
                var actualList = (IList) actual;
                Assert.IsTrue(actualList.Count != 0);
                var actualItem = actualList[0];
                if (actualItem is IDictionary)
                {
                    _AreDictionaryListEqual(expected, actualList, message);
                }
                else
                {
                    _AreBeanListEqual(expected, actualList, message);
                }
            }
            else
            {
                var dictionary = actual as IDictionary;
                if (dictionary != null)
                {
                    _AreDictionaryEqual(expected, dictionary, message);
                }
                else
                {
                    _AreBeanEqual(expected, actual, message);
                }
            }
        }

        /// <summary>
        /// IDictionary��DataSet�Ɣ�r���܂��B
        /// 
        /// IDictionary�̃L�[���J�������Ƃ��Ĕ�r���܂��B
        /// �J�����̕��я��͔�r�ɉe�����܂���B
        /// </summary>
        /// <param name="expected">�\���l</param>
        /// <param name="dictionary">���ےl</param>
        /// <param name="message">assert���s���̃��b�Z�[�W</param>
        private static void _AreDictionaryEqual(DataSet expected, IDictionary dictionary, string message)
        {
            var reader = new DictionaryReader(dictionary);
            AreEqual(expected, reader.Read(), message);
        }

        /// <summary>
        /// IDictionary��IList��DataSet�Ɣ�r���܂��B
        /// 
        /// IDictionary�̃L�[���J�������Ƃ��Ĕ�r���܂��B
        /// �J�����̕��я��͔�r�ɉe�����܂���B
        /// </summary>
        /// <param name="expected">�\���l</param>
        /// <param name="list">���ےl</param>
        /// <param name="message">assert���s���̃��b�Z�[�W</param>
        private static void _AreDictionaryListEqual(DataSet expected, IList list, string message)
        {
            var reader = new DictionaryListReader(list);
            AreEqual(expected, reader.Read(), message);
        }

        /// <summary>
        /// object��DataSet�Ɣ�r���܂��B
        /// 
        /// object�̃v���p�e�B�����J�������Ƃ��Ĕ�r���܂��B
        /// �J�����̕��я��͔�r�ɉe�����܂���B
        /// </summary>
        /// <param name="expected">�\���l</param>
        /// <param name="bean">���ےl</param>
        /// <param name="message">assert���s���̃��b�Z�[�W</param>
        private static void _AreBeanEqual(DataSet expected, object bean, string message)
        {
            var reader = new BeanReader(bean);
            AreEqual(expected, reader.Read(), message);
        }

        /// <summary>
        /// object��IList��DataSet�Ɣ�r���܂��B
        /// 
        /// object�̃v���p�e�B�����J�������Ƃ��Ĕ�r���܂��B
        /// �J�����̕��я��͔�r�ɉe�����܂���B
        /// </summary>
        /// <param name="expected">�\���l</param>
        /// <param name="list">���ےl</param>
        /// <param name="message">assert���s���̃��b�Z�[�W</param>
        private static void _AreBeanListEqual(DataSet expected, IList list, string message)
        {
            var reader = new BeanListReader(list);
            AreEqual(expected, reader.Read(), message);
        }

        #endregion
    }
}
