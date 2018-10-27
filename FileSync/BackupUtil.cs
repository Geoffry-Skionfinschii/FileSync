using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xxHashSharp;

namespace FileSync
{
    class BackupUtil
    {

        public static int BACKUP_PROGRESS_CURRENTFILE = 0;
        public static DateTime BACKUP_START_CURRENTFILE = DateTime.Now;
        public static long BACKUP_BYTESPROCESSED_CURRENTFILE = 0;

        public static DateTime FindFileAge(FileInfo file)
        {
            if(file.Exists)
            {
                return file.LastWriteTime;
            }
            return new DateTime(0);
        }

        public static FileInfo FindCorrespondingFile(BackupData dat, DriveBackupData drive)
        {
            string pathDir = drive.defaultBackupLocation + dat.path.Substring(2) + "\\" + dat.filename + "\\";
            DirectoryInfo dir = new DirectoryInfo(pathDir);
            if(dir.Exists == false)
            {
                Debug.WriteLine("FIND: NO DIR");
                return null;
            }
            var files = dir.EnumerateFiles().Where(x => x.Extension == ".filesync").OrderByDescending(x => x.LastWriteTime.Ticks).ToList();
            foreach(var file in files)
            {
                Debug.WriteLine("FIND: " + file.FullName);
            }
            return files.FirstOrDefault();
        }

        public static int COMPRESSION_BYTE_MIN = 150;

        public static bool BackupFile(BackupData dat, DriveBackupData drive)
        {
            try
            {
                string pathDir = drive.defaultBackupLocation + dat.path.Substring(2) + "\\" + dat.filename + "\\";
                DirectoryInfo inf = Directory.CreateDirectory(pathDir);
                //Debug.WriteLine(inf.FullName);
                string safeTime = GetSafeTime(DateTime.Now);
                byte[] buffer = new byte[16*4096];
                int inBuffer;
                long bytesRead = 0;
                long totalBytes;
                using (var inputStream = new FileStream(dat.path + "\\" + dat.filename, FileMode.Open,FileAccess.Read,FileShare.None,buffer.Length * 2))
                using (var outputStream = File.Create(pathDir + safeTime + ".filesync"))
                {
                    long fileSize = inputStream.Length;
                    CompressionLevel compMode = fileSize < COMPRESSION_BYTE_MIN ? CompressionLevel.NoCompression : CompressionLevel.Optimal;
                    Debug.WriteLineIf(compMode == CompressionLevel.NoCompression, "NOCOMPRESSION: " + fileSize);
                    using (GZipStream zipStream = new GZipStream(outputStream, compMode))
                    {
                        totalBytes = inputStream.Length;
                        while ((inBuffer = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bytesRead += inBuffer;
                            BACKUP_PROGRESS_CURRENTFILE = (int)((bytesRead * 100) / totalBytes);
                            zipStream.Write(buffer, 0, inBuffer);
                        }

                        zipStream.Close();

                        File.WriteAllBytes(pathDir + safeTime + ".filesync" + ".hash", BitConverter.GetBytes(dat.fileHash));

                    }
                }

                return true;
            }
            catch (FileNotFoundException e)
            {
                Program.ThrowErrorMessage(e);
                return false;
            }
            catch (DirectoryNotFoundException e)
            {
                Program.ThrowErrorMessage(e);
                return false;
            }
            catch (IOException e)
            {
                Program.ThrowErrorMessage(e);
                return false;
            }
        }

        public static uint FindUncompressedHash(FileInfo file)
        {
            try
            {
                byte[] dat = File.ReadAllBytes(file.FullName + ".hash");
                byte[] outDat = new byte[4];
                Array.Copy(dat, outDat, 4);
                return BitConverter.ToUInt32(outDat, 0);
            } catch (FileNotFoundException)
            {
                return 0;
            }
        }

        public static uint ComputeHash(FileInfo file)
        {
            xxHash hash = new xxHash();
            hash.Init();

            FileStream stream = new FileStream(file.FullName, FileMode.Open);
            long bytesToRead = stream.Length;
            long constSize = stream.Length;
            byte[] buffer = new byte[4096];
            long bytesRead = 0;
            while(bytesToRead > 0)
            {
                int len = stream.Read(buffer, 0, buffer.Length);
                bytesRead += len;
                bytesToRead -= len;
                if(len == 0 && bytesToRead > 0)
                {
                    //Unexpected IO
                    break;
                }
                BACKUP_PROGRESS_CURRENTFILE = (int)((bytesRead * 100) / constSize);
                hash.Update(buffer, len);
            }
            stream.Close();
            return hash.Digest();
        }

        public static string GetSafeTime(DateTime time)
        {
            return time.ToString("yyyyMMddHHmmss");
        }

        public static string ConvertRealPath(string path, DriveInfo info)
        {
            return ConvertRealPath(path, info.Name);
        }

        public static string ConvertRealPath(string path, string drive)
        {
            return path.Replace("*\\", drive);
        }

        public static string ConvertDrivePath(string path)
        {
            return "*" + path.Substring(2);
        }
    }
}
