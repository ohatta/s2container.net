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

using System.Collections;
using System.Text;

namespace Seasar.Framework.Util
{
	public class IListInspector
	{
		private IListInspector()
		{
		}

		public static string ToString(IList list)
		{
			StringBuilder buf = new StringBuilder();
			buf.Append("[");
			foreach (object o in list)
			{
				buf.Append(o.ToString());
				buf.Append(", ");
			}
			if (list.Count != 0)
			{
				buf.Length -= 2;
			}
			buf.Append("]");
			return buf.ToString();
		}
	}
}