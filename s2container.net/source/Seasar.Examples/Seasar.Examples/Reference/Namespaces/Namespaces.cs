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

namespace Seasar.Examples.Reference.Namespaces
{
	public class Namespaces
	{
		private IHelloClient bbb;
		private IHelloClient ccc;

		public Namespaces() {}

		public IHelloClient Bbb
		{
			get { return this.bbb; }
			set { this.bbb = value; }
		}

		public IHelloClient Ccc
		{
			get { return this.ccc; }
			set { this.ccc = value; }
		}

		public void Main()
		{
			Console.WriteLine("bbb�̎��s����");
			bbb.ShowMessage();
			Console.WriteLine(" ------------------------- ");
			Console.WriteLine();
			
			Console.WriteLine("ccc�̎��s����");
			ccc.ShowMessage();
			Console.WriteLine(" ------------------------- ");
			Console.WriteLine();

		}
	}
}