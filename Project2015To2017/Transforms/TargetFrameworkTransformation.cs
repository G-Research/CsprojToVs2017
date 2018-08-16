using System;
using System.Collections.Generic;
using Project2015To2017.Definition;

namespace Project2015To2017.Transforms
{
	public sealed class TargetFrameworkTransformation : ITransformation
	{
		public TargetFrameworkTransformation(IReadOnlyList<string> targetFrameworks)
			: this(targetFrameworks, true)
		{
		}
		public TargetFrameworkTransformation(IReadOnlyList<string> targetFrameworks, bool appendTargetFrameworkToOutputPath)
		{
			this.TargetFrameworks = targetFrameworks;
			this.AppendTargetFrameworkToOutputPath = appendTargetFrameworkToOutputPath;
		}

		public void Transform(Project definition, IProgress<string> progress)
		{
			if (null == definition)
			{
				return;
			}

			if (this.TargetFrameworks != null && this.TargetFrameworks.Count > 0)
			{
				definition.TargetFrameworks.Clear();
				foreach (var targetFramework in this.TargetFrameworks)
				{
					definition.TargetFrameworks.Add(targetFramework);
				}
			}

			definition.AppendTargetFrameworkToOutputPath = AppendTargetFrameworkToOutputPath;
		}

		public IReadOnlyList<string> TargetFrameworks { get; }
		public bool AppendTargetFrameworkToOutputPath { get; }
	}
}
