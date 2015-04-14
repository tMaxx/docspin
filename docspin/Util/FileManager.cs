using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace docspin.Util
{
	public struct FileDescriptor
	{
		public string orig_name;
		public DateTime orig_ts;
		public DateTime ts;
		public long size;
		public string name;
	}


	public static class FileManager
	{
		private static string _fileFolder = "";

		public static string FileFolder 
		{
			get
			{
				if (_fileFolder == "")
					_fileFolder = HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data/repo_files");
				if (!Directory.Exists(_fileFolder))
					Directory.CreateDirectory(_fileFolder);
				return _fileFolder;
			}
		}


		private static string hashFile(FileDescriptor fd)
		{
			byte[] val, hashed;
			var ue = new UnicodeEncoding();
			var hashinstance = new SHA256Managed();

			val = ue.GetBytes(fd.orig_name + fd.ts + fd.size);		
			hashed = hashinstance.ComputeHash(val);

			return hashed.Aggregate("", (current, b) => current + String.Format("{0:x2}", b));
		}

		public static FileDescriptor writeFile(Stream input, string inputDetails)
		{
			var fd = new FileDescriptor {size = input.Length};

			string hash = hashFile(fd);
			fd.name = hash;

			return fd;
		}

		public static string getFilePath(string hash)
		{
			string ret = Path.Combine(FileFolder, hash);
			if (!File.Exists(ret))
				return null;
			return ret;
		}
	}
}