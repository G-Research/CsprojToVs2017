using System;
using System.Linq;
using Project2015To2017.Definition;

namespace Project2015To2017
{
	/// <summary>
	/// Helper library to filter out unsupported project types
	/// </summary>
	public static class UnsupportedProjectTypes
	{
		/// <summary>
		/// Check for unsupported ProjectTypeGuids in project
		/// </summary>
		/// <param name="xmlDocument">source project document to check</param>
		/// <returns></returns>
		public static bool IsUnsupportedProjectType(Project project)
		{
			if (project == null) throw new ArgumentNullException(nameof(project));
			var xmlDocument = project.ProjectDocument;

			// try to get project type - may not exist
			var typeElement = xmlDocument.Descendants(project.XmlNamespace + "ProjectTypeGuids").FirstOrDefault();
			// no matching tag found, project should be okay to convert
			if (typeElement == null) return false;

			// parse the CSV list
			var guidTypes = typeElement.Value.Split(';').Select(x => x.Trim());

			// if any guid matches an unsupported type, return true
			return (from guid in guidTypes
					from unsupported in unsupportedGuids
					where guid.Equals(unsupported, StringComparison.CurrentCultureIgnoreCase)
					select unsupported).Any();
		}

		/// <summary>
		/// Guids that cannot be converted
		/// </summary>
		/// <remarks>
		/// Types of projects that are not supported:
		/// https://github.com/dotnet/project-system/blob/master/docs/feature-comparison.md
		/// The GUIDs taken from
		/// https://www.codeproject.com/Reference/720512/List-of-Visual-Studio-Project-Type-GUIDs
		/// Note that the list here is in upper case but project file guids are normally lower case
		/// This list does not include Windows Forms apps, these have no type guid
		/// </remarks>
		private static readonly string[] unsupportedGuids =
			{
			"{8BB2217D-0F2D-49D1-97BC-3654ED321F3B}",   // ASP.NET 5
			"{603C0E0B-DB56-11DC-BE95-000D561079B0}",   // ASP.NET MVC 1
			"{F85E285D-A4E0-4152-9332-AB1D724D3325}",   // ASP.NET MVC 2
			"{E53F8FEA-EAE0-44A6-8774-FFD645390401}",   // ASP.NET MVC 3
			"{E3E379DF-F4C6-4180-9B81-6769533ABE47}",   // ASP.NET MVC 4
			"{349C5851-65DF-11DA-9384-00065B846F21}",   // ASP.NET MVC 5
			};
	}
}
