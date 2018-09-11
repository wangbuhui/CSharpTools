using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OpenZip
{
    public partial class openzip : System.Web.UI.Page
    {

        protected string Action = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Init_OpenZip();
        }

        public void Init_OpenZip()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["Action"]))//获取form的Action中的参数
            {
                Action = Request.QueryString["Action"].Trim().ToLower();//去掉空格并变小写
            }
            switch (Action)
            {
                case "openzip":
                    OpenZip();
                break;

            }
        }

        private void OpenZip()
        {
            var filepath = Request.Form["filepath"];
            var openpath = Request.Form["openpath"];
            if (string.IsNullOrEmpty(filepath) || string.IsNullOrEmpty(openpath))//获取form中的参数
            {
                Response.Write("<Script Language=JavaScript>alert('参数错误，请重试！');</Script>");
                return;
            }

            if (!File.Exists(filepath))
            {
                Response.Write("<Script Language=JavaScript>alert('该路径文件不存在！');</Script>");
                return;
            }

            if (!Directory.Exists(openpath))
            {
                Response.Write("<Script Language=JavaScript>alert('解压路径不存在！');</Script>");
                return;
            }
            //解压文件
            UnZip(filepath, openpath,null);

            Response.Write("<Script Language=JavaScript>alert('解压成功！');</Script>");

        }

        /// <summary>
        /// ZIP:解压一个zip文件
        /// add yuangang by 2016-06-13
        /// </summary>
        /// <param name="ZipFile">需要解压的Zip文件（绝对路径）</param>
        /// <param name="TargetDirectory">解压到的目录</param>
        /// <param name="Password">解压密码</param>
        /// <param name="OverWrite">是否覆盖已存在的文件</param>
        public static void UnZip(string ZipFile, string TargetDirectory, string Password, bool OverWrite = true)
        {
            //如果解压到的目录不存在，则报错
            if (!System.IO.Directory.Exists(TargetDirectory))
            {
                throw new System.IO.FileNotFoundException("指定的目录: " + TargetDirectory + " 不存在!");
            }
            //目录结尾
            if (!TargetDirectory.EndsWith("\\")) { TargetDirectory = TargetDirectory + "\\"; }

            using (ZipInputStream zipfiles = new ZipInputStream(File.OpenRead(ZipFile)))
            {
                zipfiles.Password = Password;
                ZipEntry theEntry;

                while ((theEntry = zipfiles.GetNextEntry()) != null)
                {
                    string directoryName = "";
                    string pathToZip = "";
                    pathToZip = theEntry.Name;

                    if (pathToZip != "")
                        directoryName = Path.GetDirectoryName(pathToZip) + "\\";

                    string fileName = Path.GetFileName(pathToZip);

                    Directory.CreateDirectory(TargetDirectory + directoryName);

                    if (fileName != "")
                    {
                        if ((File.Exists(TargetDirectory + directoryName + fileName) && OverWrite) || (!File.Exists(TargetDirectory + directoryName + fileName)))
                        {
                            using (FileStream streamWriter = File.Create(TargetDirectory + directoryName + fileName))
                            {
                                int size = 2048;
                                byte[] data = new byte[2048];
                                while (true)
                                {
                                    size = zipfiles.Read(data, 0, data.Length);

                                    if (size > 0)
                                        streamWriter.Write(data, 0, size);
                                    else
                                        break;
                                }
                                streamWriter.Close();
                            }
                        }
                    }
                }

                zipfiles.Close();
            }
        }
    }
}