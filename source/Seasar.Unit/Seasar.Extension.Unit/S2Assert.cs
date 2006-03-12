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
		/// �I�u�W�F�N�g�́ABean�AHashtable�ABean��IList�AHashtable��IList�̂����ꂩ �łȂ���΂Ȃ�܂���B
		/// 
		/// Bean�̏ꍇ�̓v���p�e�B�����AMap�̏ꍇ�̓L�[���J�������Ƃ��� ��r���܂��B
		/// �J�����̕��я��͔�r�ɉe�����܂���B 
		/// ���l�͑S��BigDecimal�Ƃ��Ĕ�r���܂��B
		/// </summary>
		/// <param name="expected">�\���l</param>
		/// <param name="actual">���ےl</param>
		/// <param name="message">assert���s���̃��b�Z�[�W</param>
		public static void AreEqual(DataSet expected, object actual, string message) 
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Hashtable��DataSet�Ɣ�r���܂��B
		/// 
		/// Hashtable�̃L�[���J�������Ƃ��Ĕ�r���܂��B 
		/// �J�����̕��я��͔�r�ɉe�����܂���B 
		/// ���l�͑S��BigDecimal�Ƃ��Ĕ�r���܂��B
		/// </summary>
		/// <param name="expected">�\���l</param>
		/// <param name="actual">���ےl</param>
		/// <param name="message">assert���s���̃��b�Z�[�W</param>
		public static void AreEqual(DataSet expected, Hashtable actual, string message) 
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Hashtable��List��DataSet�Ɣ�r���܂��B
		/// 
		/// Hashtable�̃L�[���J�������Ƃ��Ĕ�r���܂��B 
		/// �J�����̕��я��͔�r�ɉe�����܂���B 
		/// ���l�͑S��BigDecimal�Ƃ��Ĕ�r���܂��B
		/// </summary>
		/// <param name="expected">�\���l</param>
		/// <param name="actual">���ےl</param>
		/// <param name="message">assert���s���̃��b�Z�[�W</param>
		public static void AreEqual(DataSet expected, IList actual, string message) 
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
