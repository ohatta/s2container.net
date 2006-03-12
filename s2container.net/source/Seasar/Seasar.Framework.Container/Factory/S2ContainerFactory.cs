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
using System.Globalization;
using System.Reflection;
using System.Resources;
using Seasar.Framework.Util;
using Seasar.Framework.Xml;

namespace Seasar.Framework.Container.Factory
{
	/// <summary>
	/// S2ContainerFactory の概要の説明です。
	/// </summary>
	public sealed class S2ContainerFactory
	{
		public const string PUBLIC_ID = "-//SEASAR//DTD S2Container//EN";
		public const string DTD_PATH = "components.dtd";
		public const string BUILDER_CONFIG_PATH = "s2containerbuilder";
		private static ResourceSet builderProps_;
		private static Hashtable builders_ = new Hashtable();
		private static IS2ContainerBuilder defaultBuilder_ = 
			new XmlS2ContainerBuilder();

		static S2ContainerFactory()
		{
			try
			{
				builderProps_ = new ResourceManager(BUILDER_CONFIG_PATH,
					Assembly.GetExecutingAssembly()).GetResourceSet(
					CultureInfo.CurrentCulture,true,true);
			}
			catch(MissingManifestResourceException)
			{
			}
			builders_.Add("xml",defaultBuilder_);
			builders_.Add("dicon",defaultBuilder_);
		}

		private S2ContainerFactory()
		{
		}

		public static IS2Container Create(string path)
		{
			string ext = GetExtension(path);
			S2Section config = S2SectionHandler.GetS2Section();
			if(config != null)
			{
				IList assemblys = config.Assemblys;
				foreach(string assembly in assemblys)
				{
					if(!StringUtil.IsEmpty(assembly)) AppDomain.CurrentDomain.Load(assembly);
				}
			}
			IS2Container container = GetBuilder(ext).Build(path);
			return container;
		}

		public static IS2Container Include(IS2Container parent,string path)
		{
			IS2Container root = parent.Root;
			IS2Container child = null;
			lock(root)
			{
				if(root.HasDescendant(path))
				{
					child = root.GetDescendant(path);
					parent.Include(child);
				}
				else
				{
					string ext = GetExtension(path);
					IS2ContainerBuilder builder = GetBuilder(ext);
					child = builder.Include(parent,path);
					root.RegisterDescendant(child);
				}
			}
			return child;
		}

		private static string GetExtension(string path)
		{
			string ext = ResourceUtil.GetExtension(path);
			if(ext == null) throw new ExtensionNotFoundRuntimeException(path);
			return ext;
		}

		private static IS2ContainerBuilder GetBuilder(string ext)
		{
			IS2ContainerBuilder builder = null;
			lock(builders_)
			{
				builder = (IS2ContainerBuilder) builders_[ext];
				if(builder != null) return builder;
				string className = builderProps_.GetString(ext);
				if(className != null)
				{
					Assembly[] asms = AppDomain.CurrentDomain.GetAssemblies();
					Type type = ClassUtil.ForName(className, asms);
					builder = (IS2ContainerBuilder) 
						ClassUtil.NewInstance(type);
					builders_[ext] = builder;
				}
				else
				{
					builder = defaultBuilder_;
				}
            }
			return builder;
		}
	}
}
