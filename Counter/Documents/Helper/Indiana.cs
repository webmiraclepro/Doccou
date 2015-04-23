﻿using System.IO;
using System.IO.Compression;

namespace Counter.Documents.Helper
{
	// we use Microsoft Compression
	public static class Indiana
	{
		public static string ReadArchive(Stream archiveStream, string path)
		{
			using (var archive = new ZipArchive(archiveStream, ZipArchiveMode.Read))
			{
				var zipArchiveEntry = archive.GetEntry(path);
				using (var stream = zipArchiveEntry.Open())
				using (var reader = new StreamReader(stream))
				{
					var content = reader.ReadToEnd();
					return content;
				}
			}
		}
	}

	// we cant use for know PCL Storage
	//private static async Task<Stream> OpenAsync(string fullName)
	//{
	//	var file = await FileSystem.Current.GetFileFromPathAsync(fullName);
	//	return await file.OpenAsync(FileAccess.Read);
	//}
}
