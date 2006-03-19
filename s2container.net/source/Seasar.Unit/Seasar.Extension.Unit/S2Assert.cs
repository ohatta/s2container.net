using System;
using System.Collections;
using System.Data;
using MbUnit.Core.Exceptions;
using MbUnit.Framework;

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
		/// ���l�͑S��BigDecimal�Ƃ��Ĕ�r���܂��B
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
		/// ���l�͑S��BigDecimal�Ƃ��Ĕ�r���܂��B
		/// </summary>
		/// <param name="expected">�\���l</param>
		/// <param name="actual">���ےl</param>
		/// <param name="message">assert���s���̃��b�Z�[�W</param>
		public static void AreEqual(DataSet expected, DataSet actual, string message) 
		{
			message = message == null ? string.Empty : message;

			Assert.AreEqual(
				expected.Tables.Count,
				actual.Tables.Count,
				message + ":TableSize"
				);
			for (int i = 0; i < expected.Tables.Count; ++i) 
			{
				AreEqual(expected.Tables[i], actual.Tables[i], message);
			}
		}

		/// <summary>
		/// DataTable���m���r���܂��B
		/// 
		/// �J�����̕��я��͔�r�ɉe�����܂���B 
		/// ���l�͑S��BigDecimal�Ƃ��Ĕ�r���܂��B
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
		/// ���l�͑S��BigDecimal�Ƃ��Ĕ�r���܂��B
		/// </summary>
		/// <param name="expected">�\���l</param>
		/// <param name="actual">���ےl</param>
		/// <param name="message">assert���s���̃��b�Z�[�W</param>
		public static void AreEqual(DataTable expected, DataTable actual, string message) 
		{
			message = message == null ? string.Empty : message;
			message = message + ":TableName=" + expected.TableName;
			Assert.AreEqual(expected.Rows.Count, actual.Rows.Count, message + ":RowSize");
			for (int i = 0; i < expected.Rows.Count; ++i) 
			{
				DataRow expectedRow = expected.Rows[i];
				DataRow actualRow = actual.Rows[i];
				IList errorMessages = new ArrayList();
				for (int j = 0; j < expected.Columns.Count; ++j) 
				{
					try 
					{
						string columnName = expected.Columns[j].ColumnName;
						object expectedValue = expectedRow[columnName];
						object actualValue = actualRow[columnName];
						// TOOD ColumnType ct = ColumnTypes.getColumnType(expectedValue); impl
						if (!expectedValue.Equals(actualValue)) 
						{
							Assert.AreEqual(
								expectedValue,
								actualValue,
								message + ":Row=" + i + ":columnName=" + columnName
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
					Assert.Fail(message + errorMessages);
				}
			}
		}

		/// <summary>
		/// �I�u�W�F�N�g��DataSet�Ɣ�r���܂��B
		/// 
		/// �I�u�W�F�N�g�́ABean�AHashtable�ABean��IList�AHashtable��IList�̂����ꂩ �łȂ���΂Ȃ�܂���B
		/// 
		/// Bean�̏ꍇ�̓v���p�e�B�����AMap�̏ꍇ�̓L�[���J�������Ƃ��� ��r���܂��B
		/// �J�����̕��я��͔�r�ɉe�����܂���B 
		/// ���l�͑S��BigDecimal�Ƃ��Ĕ�r���܂��B
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

			if (actual is IList) 
			{
				IList actualList = (IList) actual;
				Assert.IsTrue(actualList.Count != 0);
				object actualItem = actualList[0];
				if (actualItem is IDictionary) 
				{
					AreDictionaryListEqual(expected, actualList, message);
				} 
				else 
				{
					AreBeanListEqual(expected, actualList, message);
				}
			} 
			else if (actual is object[]) 
			{
				AreEqual(expected, new ArrayList((object[]) actual), message);
			} 
			else 
			{
				if (actual is IDictionary) 
				{
					AreDictionaryEqual(expected, (IDictionary) actual, message);
				} 
				else 
				{
					AreBeanEqual(expected, actual, message);
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
		private static void AreDictionaryEqual(DataSet expected, IDictionary dictionary, string message) 
		{
			DictionaryReader reader = new DictionaryReader(dictionary);
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
		private static void AreDictionaryListEqual(DataSet expected, IList list, string message) 
		{
			DictionaryListReader reader = new DictionaryListReader(list);
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
		private static void AreBeanEqual(DataSet expected, object bean, string message) 
		{
			BeanReader reader = new BeanReader(bean);
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
		private static void AreBeanListEqual(DataSet expected, IList list, string message) 
		{
			BeanListReader reader = new BeanListReader(list);
			AreEqual(expected, reader.Read(), message);
		}

		#endregion
	}
}
