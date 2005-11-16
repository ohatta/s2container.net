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

using Microsoft.JScript;
using System;
using System.Collections;
using System.Reflection;
using System.CodeDom;
using System.CodeDom.Compiler;
using Seasar.Framework.Exceptions;

namespace Seasar.Framework.Util
{
	/// <summary>
	/// CodeDom��JScript.NET��������悤�ɂ��܂��B
	/// </summary>
	public sealed class JScriptUtil
	{
		private static CodeDomProvider provider_ = new JScriptCodeProvider();
		private static Type evaluateType_;

		private const string EVAL_SOURCE = @"
			package Seasar.Framework.Util.JScript
			{
				class Evaluator
				{
					public static function Eval(expr : String,unsafe : boolean,
						self : Object,out : Object,err : Object, container : Object) : Object 
					{ 
						if(unsafe)
						{
							return eval(expr,'unsafe');
						}
						else
						{
							return eval(expr); 
						}
					}
				}
			}";

		private JScriptUtil()
		{
		}

		static JScriptUtil()
		{
			ICodeCompiler compiler = provider_.CreateCompiler();
			CompilerParameters parameters = new CompilerParameters();
			parameters.GenerateInMemory = true;
			CompilerResults results = compiler.CompileAssemblyFromSource(parameters,EVAL_SOURCE);
			Assembly assembly = results.CompiledAssembly;
			evaluateType_ = assembly.GetType("Seasar.Framework.Util.JScript.Evaluator");
		}

		public static object Evaluate(string exp,Hashtable ctx, object root)
		{
			try
			{
				return evaluateType_.InvokeMember("Eval",BindingFlags.InvokeMethod,
					null,null,new object[] {exp,true,ctx["self"],ctx["out"],ctx["err"],root});
			} 
			catch(Exception ex)
			{
				throw new JScriptEvaluateRuntimeException(exp,ex);
			}
		}

		public static object Evaluate(string exp, object root)
		{
			try
			{
				return evaluateType_.InvokeMember("Eval",BindingFlags.InvokeMethod,
					null,null,new object[] {exp,true,null,null,null,root});
			} 
			catch(Exception ex)
			{
				throw new JScriptEvaluateRuntimeException(exp,ex);
			}
		}
	}
}