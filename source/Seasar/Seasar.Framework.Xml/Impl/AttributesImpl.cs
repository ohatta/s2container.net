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

namespace Seasar.Framework.Xml.Impl
{
	/// <summary>
	/// AttributesImpl �̊T�v�̐����ł��B
	/// </summary>
	public class AttributesImpl : IAttributes
	{
		private Hashtable values_ = new Hashtable();
		private IList qNames_ = new ArrayList();

		public AttributesImpl()
		{
		}

		public void AddAttribute(string qName, string value)
		{
			qNames_.Add(qName);
			values_[qName] = value;
		}

		#region IAttributes �����o

		public string this[string qName]
		{
			get
			{
				return (string) values_[qName];
			}
		}

		string Seasar.Framework.Xml.IAttributes.this[int index]
		{
			get
			{
				return (string) values_[qNames_[index]];
			}
		}

		public int Count
		{
			get
			{
				return qNames_.Count;
			}
		}

		public string GetQName(int index)
		{
			return (string) qNames_[index];
		}

		#endregion
	}
}
