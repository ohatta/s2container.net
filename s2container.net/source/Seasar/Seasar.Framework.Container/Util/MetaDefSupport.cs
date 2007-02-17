#region Copyright
/*
 * Copyright 2005-2007 the Seasar Foundation and the Others.
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

namespace Seasar.Framework.Container.Util
{
	/// <summary>
	/// IMetaDef�̐ݒ���T�|�[�g���܂�
	/// </summary>
	public sealed class MetaDefSupport
	{

		private IList metaDefs_ = ArrayList.Synchronized(new ArrayList());
		private IS2Container container_;

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public MetaDefSupport()
		{
		}

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		/// <param name="container">S2Container</param>
		public MetaDefSupport(IS2Container container)
		{
			this.Container = container;
		}

		/// <summary>
		/// IMetaDef��ǉ����܂��B
		/// </summary>
		/// <param name="metaDef"></param>
		public void AddMetaDef(IMetaDef metaDef)
		{
			if(container_ != null) 
			{
				metaDef.Container = container_;
			}
			metaDefs_.Add(metaDef);
		}

		/// <summary>
		/// IMetaDef�̐�
		/// </summary>
		public int MetaDefSize
		{
			get
			{
				return metaDefs_.Count;
			}
		}

		/// <summary>
		/// �ԍ����w�肵��IMetaDef���擾���܂��B
		/// </summary>
		/// <param name="index"></param>
		/// <returns>IMetaDef</returns>
		public IMetaDef GetMetaDef(int index)
		{
			return (IMetaDef) metaDefs_[index];
		}

		/// <summary>
		/// ���O���w�肵��IMetaDef���擾���܂��B
		/// </summary>
		/// <param name="name">IMetaDef�̖��O</param>
		/// <returns>IMetaDef</returns>
		public IMetaDef GetMetaDef(string name)
		{
			IEnumerator enu = metaDefs_.GetEnumerator();
			while(enu.MoveNext())
			{
				IMetaDef metaDef = (IMetaDef)enu.Current;
				if((name == null && metaDef.Name == null) || name != null
					&& name.ToLower().CompareTo(metaDef.Name.ToLower()) == 0)
				{
					return metaDef;
				}
			}
			return null;
		}
		
		/// <summary>
		/// ���O���w�肵��IMetaDef�̔z����擾���܂��B
		/// </summary>
		/// <param name="name">IMetaDef�̖��O</param>
		/// <returns>IMetaDef�̔z��</returns>
		public IMetaDef[] GetMetaDefs(string name)
		{
			ArrayList defs = new ArrayList();
			IEnumerator enu = metaDefs_.GetEnumerator();
			while(enu.MoveNext())
			{
				IMetaDef metaDef = (IMetaDef)enu.Current;
				if((name == null && metaDef.Name == null) || name != null
					&& name.ToLower().CompareTo(metaDef.Name.ToLower()) == 0)
				{
					defs.Add(metaDef);
				}
			}
			return (IMetaDef[]) defs.ToArray(typeof(IMetaDef));
		}

		/// <summary>
		/// S2Container
		/// </summary>
		public IS2Container Container
		{
			set
			{
				container_ = value;
				IEnumerator enu = metaDefs_.GetEnumerator();
				while(enu.MoveNext())
				{
					IMetaDef metaDef = (IMetaDef)enu.Current;
					metaDef.Container = value;
				}
			}
		}
	}
}
