using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Doccou.Pcl.Documents;
using Doccou.Pcl.Documents.Archives;

namespace Doccou.Pcl
{
	/// <summary>
	/// Document is the class wrapping every other class in this library.
	/// If you are an user, everything you need come from here.
	/// </summary>
	public class Document
	{
		public DocumentType ExtensionType { get; private set; }
		public string Extension { get; private set; }
		public string FullName { get; private set; }
		public string Name { get; private set; }
		public string NameWithoutExtension { get; private set; }
		public uint? Count { get; private set; }

		private readonly Dictionary<string, DocumentType> _extensionsSupported
			= new Dictionary<string, DocumentType>
		{
			{ ".pdf", DocumentType.Pdf },
			{ ".docx", DocumentType.Docx },
			{ ".pptx", DocumentType.Pptx },
			{ ".odt", DocumentType.Odt },
			{ ".bmp", DocumentType.Bmp },
			{ ".jpg", DocumentType.Jpeg },
			{ ".jpeg", DocumentType.Jpeg },
			{ ".gif", DocumentType.Gif },
			{ ".tiff", DocumentType.Tiff },
			{ ".png", DocumentType.Png },
		};

		/// <remarks>
		/// We can't open stream ourself.
		/// </remarks>
		private Document(string fullName)
		{
			FullName = fullName;
			Name = Path.GetFileName(fullName);
			NameWithoutExtension = Path.GetFileNameWithoutExtension(fullName);
			Extension = Path.GetExtension(fullName).ToLowerInvariant();
			ExtensionType = IsSupported(Extension)
				? _extensionsSupported[Extension]
				: DocumentType.Unknow;
		}

		public Document(string fullName, Stream stream)
			: this(fullName)
		{
			Count = !ExtensionType.Equals(DocumentType.Unknow)
				? BuildDocument(stream).Count
				: 0;
		}

		public bool IsSupported(string extension)
		{
			return _extensionsSupported.ContainsKey(extension);
		}

		// replace with a static dictionary ?
		private IDocument BuildDocument(Stream stream)
		{
			switch (ExtensionType)
			{
				case DocumentType.Pdf:	return new Pdf(stream);
				case DocumentType.Docx: return new Docx(stream);
				case DocumentType.Pptx: return new Pptx(stream);
				case DocumentType.Odt:	return new Odt(stream);
				case DocumentType.Bmp:	return new Bmp(stream);
				case DocumentType.Jpeg:	return new Jpeg(stream);
				case DocumentType.Gif:	return new Gif(stream);
				case DocumentType.Tiff:	return new Tiff(stream);
				case DocumentType.Png:	return new Png(stream);
				default:
					throw new NotImplementedException(String.Format(
						"{0} is a \"{1}\" and \"{1}\" is not implemented.",
						NameWithoutExtension, Extension));
			}
		}
	}
}