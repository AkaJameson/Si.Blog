﻿using System.Diagnostics;

namespace Si.Framework.ToolKit
{
    /// <summary>
    /// 仅记录框架加载时的错误日志信息
    /// </summary>
    public class LogProvider
    {
        private static ReaderWriterLockSlim LogWriteLock = new ReaderWriterLockSlim();
        // 日志路径和文件名
        private static readonly string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "logs");
        private static readonly string logFileName = "SiWebApiInternal.txt";
        /// <summary>
        /// 输出 info 级别日志
        /// </summary>
        /// <param name="message">日志内容</param>
        public static void Info(string message)
        {
            WriteLog("INFO", message);
        }
        /// <summary>
        /// 输出 error 级别日志
        /// </summary>
        /// <param name="message">日志内容</param>
        public static void Error(string message)
        {
            WriteLog("ERROR", message);
        }

        private static void WriteLog(string level, string message)
        {
            string fullFilePath = Path.Combine(logDirectory, logFileName);

            try
            {
                LogWriteLock.EnterWriteLock();
                DateTime now = DateTime.Now;

                // 创建日志目录
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                    if (IsLinux())
                    {
                        SetFilePermissions(logDirectory, "777");
                    }
                }
                // 判断日志文件大小，超过 10MB 删除
                if (File.Exists(fullFilePath) && new FileInfo(fullFilePath).Length > 10 * 1024 * 1024)
                {
                    File.Delete(fullFilePath);
                }

                // 写入日志
                using (StreamWriter writer = File.AppendText(fullFilePath))
                {
                    writer.WriteLine($"{now:yyyy-MM-dd HH:mm:ss} >> {level} >> {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing log: {ex}");
            }
            finally
            {
                LogWriteLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 设置文件或目录权限（仅在 Linux 下使用）
        /// </summary>
        private static void SetFilePermissions(string filePath, string permissions)
        {
            if (!IsLinux()) return; // 仅在 Linux 下执行

            try
            {
                Process chmod = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "chmod",
                        Arguments = $"{permissions} {filePath}",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                chmod.Start();
                chmod.WaitForExit();

                if (chmod.ExitCode != 0)
                {
                    string error = chmod.StandardError.ReadToEnd();
                    Console.WriteLine($"Failed to set permissions: {error}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting file permissions: {ex}");
            }
        }

        /// <summary>
        /// 检查当前是否运行在 Linux 环境
        /// </summary>
        private static bool IsLinux()
        {
            return Environment.OSVersion.Platform == PlatformID.Unix ||
                   Environment.OSVersion.Platform == PlatformID.MacOSX;
        }
    }
}