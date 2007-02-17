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
	/// DestroyMethodDefSupport �̊T�v�̐����ł��B
	/// </summary>
	public sealed class DestroyMethodDefSupport
	{
		private IList methodDefs_ = ArrayList.Synchronized(new ArrayList());
		private IS2Container container_;

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public DestroyMethodDefSupport()
		{
		}

		/// <summary>
		/// DestroyMethodDef��ǉ����܂��B
		/// </summary>
		/// <param name="methodDef">MethodDef</param>
		public void AddDestroyMethodDef(IDestroyMethodDef methodDef)
		{
			if(container_ != null)
			{
				methodDef.Container = container_;
			}
			methodDefs_.Add(methodDef);
		}

		/// <summary>
		/// DestroyMethodDef�̐�
		/// </summary>
		public int DestroyMethodDefSize
		{
			get { return methodDefs_.Count; }
		}

		/// <summary>
		/// �ԍ����w�肵��IDestroyMethodDef���擾���܂��B
		/// </summary>
		/// <param name="index">IDestroyMethodDef�̔ԍ�</param>
		/// <returns>IDestroyMethodDef</returns>
		public IDestroyMethodDef GetDestroyMethodDef(int index)
		{
			return (IDestroyMethodDef) methodDefs_[index];
		}

		/// <summary>
		/// S2Container
		/// </summary>
		public IS2Container Container
		{
			set
			{
				container_ = value;
				IEnumerator enu = methodDefs_.GetEnumerator();
				while(enu.MoveNext())
				{
					IDestroyMethodDef methodDef = (IDestroyMethodDef) enu.Current;
					methodDef.Container = value;
				}
			}
		}
	}
}
