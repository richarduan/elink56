using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;


    public class FileFSO
    {

        #region 判断路径是否存在
        /// <summary>
        /// 判断(路径)是否存在
        /// </summary>
        /// <param name="path">真实路径(绝对路径)</param>
        /// <returns></returns>
        public bool Exists(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 创建目录
        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="path">真实路径(绝对路径)</param>
        /// <returns></returns>
        public bool createDirectory(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 删除文件夹
        /// <summary>
        /// 删除文件夹 及文件
        /// </summary>
        /// <param name="path">真实路径(绝对路径)</param>
        /// <returns></returns>
        public bool DirDelete(string path)
        {
            try
            {
                if (Directory.Exists(path)) //如果存在这个文件夹删除    
                {
                    foreach (string d in Directory.GetFileSystemEntries(path))
                    {
                        if (File.Exists(d))
                        {
                            File.Delete(d); //直接删除其中的文件  
                        }
                        else
                        {
                            this.DirDelete(d); //递归删除子文件夹 
                        }
                    }
                    Directory.Delete(path); //删除已空文件夹    
                    return true;
                }
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }
        #endregion

        #region 判断(文件)是否存在
        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public bool FilesExist(string path)
        {
            try
            {
                return File.Exists(path);
            }
            catch
            {
                return false;
            }

        }
        #endregion

        #region 删除文件
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public bool FilesDelete(string path)
        {
            try
            {

                if (File.Exists(path))
                {
                    File.Delete(path);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 读取文件
        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string ReadFileText(string path)
        {
            string IncStr = "";

            if (File.Exists(path))
            {
                string s_readline = "";
                FileStream fsFile = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                StreamReader swMyFile = new StreamReader(fsFile, Encoding.GetEncoding("GB2312"));
                while (!swMyFile.EndOfStream)
                {
                    s_readline += swMyFile.ReadLine() + "\n";
                }
                IncStr = s_readline;
                fsFile.Dispose();
                fsFile.Close();

                swMyFile.Dispose();
                swMyFile.Close();

            }

            return IncStr;
        }
        #endregion 

        #region 写文件操作
        /// <summary>
        /// 写文件操作
        /// </summary>
        /// <param name="path"></param>
        /// <param name="Text"></param>
        /// <returns></returns>
        public string WriteFileText(string path, string Tmp)
        {
            try
            {

                this.FilesDelete(path);//清空文件

                //string filePath = System.Environment.CurrentDirectory + "\\" + path;
                //System.Windows.Forms.MessageBox.Show(filePath);

                //FileStream fsMyfile = new FileStream(path, FileMode.Append, FileAccess.Write);
                //StreamWriter swMyfile = new StreamWriter(fsMyfile, Encoding.GetEncoding("gb2312"));
                StreamWriter Writer = new StreamWriter(path, false, Encoding.GetEncoding("gb2312"));
                Writer.Write(Tmp);

                Writer.Flush();
                Writer.Close();
                Writer.Dispose();

                return "true";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }


        }
        #endregion 



    }

