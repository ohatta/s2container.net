#region Copyright
/*
 * Copyright 2005 the Seasar Foundation and the Others.
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
using System.Collections.Specialized;

namespace Seasar.Framework.Container.Util
{
	/// <summary>
	/// IPropertyDef�̐ݒ���T�|�[�g���܂��B
	/// </summary>
	public sealed class PropertyDefSupport
	{
		private Hashtable propertyDefs_ = Hashtable.Synchronized(CollectionsUtil.CreateCaseInsensitiveHashtable()); 
		private IS2Container container_;

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public PropertyDefSupport()
		{
		}

		/// <summary>
		/// PropertyDef��ǉ����܂��B
		/// </summary>
		/// <param name="propertyDef">IPropertyDef</param>
		public void AddPropertyDef(IPropertyDef propertyDef)
		{
			if(container_ != null)
			{
				propertyDef.Container = container_;
			}
			propertyDefs_.Add(propertyDef.PropertyName,propertyDef);
		}

		/// <summary>
		/// IPropertyDef�̐�
		/// </summary>
		public int PropertyDefSize
		{
			get{ return propertyDefs_.Count; }
		}

		/// <summary>
		/// �ԍ����w�肵��IPropertyDef���擾���܂��B
		/// </summary>
		/// <param name="index">IPropertyDef�̔ԍ�</param>
		/// <returns>IPropertyDef</returns>
		public IPropertyDef GetPropertyDef(int index)
		{
			int i = 0;
			IEnumerator enu = propertyDefs_.Values.GetEnumerator();
			while(enu.MoveNext())
			{
				if(i++ == index)
				{
					return (IPropertyDef) enu.Current;
				}
			}
			return null;
		}

		/// <summary>
		/// ���O���w�肵��IPropertyDef���擾���܂��B
		/// </summary>
		/// <param name="propertyName">IPropertyDef�̖��O</param>
		/// <returns>IPropertyDef</returns>
		public IPropertyDef GetPropertyDef(string propertyName)
		{
			return (IPropertyDef) propertyDefs_[propertyName];
		}

		/// <summary>
		/// �w�肳�ꂽ���O��IPropertyDef�������Ă��邩���肵�܂��B
		/// </summary>
		/// <param name="propertyName">IPropertyDef�̖��O</param>
		/// <returns>���݂���Ȃ�true</returns>
		public bool HasPropertyDef(string propertyName)
		{
			return propertyDefs_.ContainsKey(propertyName);
		}

		/// <summary>
		/// S2Container
		/// </summary>
		public IS2Container Container
		{
			set
			{
				container_ = value;
				IEnumerator enu = propertyDefs_.Values.GetEnumerator();
				while(enu.MoveNext())
				{
					IPropertyDef propertyDef = (IPropertyDef) enu.Current;
					propertyDef.Container = value;
				}
			}
		}

	}
}
