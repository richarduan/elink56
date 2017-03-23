using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;

public class PhotoUpFile
{

    public PhotoUpFile()
    {
    }


    #region 属性
    private String[] _allowedExtensions;
    /// <summary>
    /// 扩展名称
    /// </summary>
    public String[] allowedExtensions
    {
        get
        {
            return _allowedExtensions;
        }
        set
        {
            _allowedExtensions = value;
        }
    }

    private string _FileExt;
    /// <summary>
    /// 扩展名
    /// </summary>
    public string FileExt
    {
        get
        {
            return _FileExt.Replace(".", "");
        }
        set
        {
            _FileExt = value;
        }
    }

    private string _FileName;
    /// <summary>
    /// 文件名
    /// </summary>
    public string FileName
    {
        get
        {
            return _FileName;
        }
        set
        {
            _FileName = value;
        }
    }


    private int _FileLength;
    /// <summary>
    /// 上传文件大小
    /// </summary>
    public int FileLength
    {
        get
        {
            return _FileLength;
        }
        set
        {
            _FileLength = value;
        }
    }

    private string _savePathFile;
    /// <summary>
    /// 文件路径
    /// </summary>
    public string savePathFile
    {
        get
        {
            return _savePathFile;
        }
        set
        {
            _savePathFile = value;
        }
    }


    #endregion

    #region 文件头
    /// <summary>
    /// 文件头
    /// </summary>
    private enum FileExtension
    {
        JPG = 255216,
        GIF = 7173,
        BMP = 6677,
        PNG = 13780,
        DOC = 208207,
        DOCX = 8075,
        XLS = 208207,
        XLSX = 8075,
        //JS = 239187,
        //SWF = 6787,
        //TXT = 7067,
        //MP3 = 7368,
        //WMA = 4838,
        //MID = 7784,
        //RAR = 8297,
        //ZIP = 8075,
        //XML = 6063,
        //EXE = 7790,

        // 7790 exe dll,
        // 8297 rar
        // 6063 xml
        // 6033 html
        // 239187 aspx
        // 117115 cs
        // 119105 js
        // 210187 txt
        //255254 sql 
    }
    #endregion

    #region 头文件判断
    /// <summary>
    /// 头文件判断
    /// </summary>
    /// <param name="postfile"></param>
    /// <returns></returns>
    private bool isImage(HttpPostedFile file)
    {
        string[] exten = { "255216", "7173", "6677", "13780", "208207", "8075", "208207", "8075" };
        int contentLength = file.ContentLength;
        byte[] imgArray = new byte[contentLength];
        file.InputStream.Read(imgArray, 0, contentLength);
        System.IO.MemoryStream ms = new System.IO.MemoryStream(imgArray);
        System.IO.BinaryReader br = new System.IO.BinaryReader(ms);
        string filecess = "";
        byte buffer;
        try
        {
            buffer = br.ReadByte();
            filecess = buffer.ToString();
            buffer = br.ReadByte();
            filecess += buffer.ToString();
        }
        catch { }
        finally { br.Close(); ms.Dispose(); ms.Close(); }
        foreach (string s in exten)
        {
            if (s == filecess)
                return true;
        }
        return false;
    }
    #endregion



    #region 新目录
    /// <summary>
    /// 新目录
    /// </summary>
    /// <param name="dataLen">目录格式（时间）</param>
    /// <returns></returns>
    private string newFile(string dataLen)
    {
        if (dataLen == "")
        {
            dataLen = "yyyy-MM-dd";
        }
        return DateTime.Now.ToString(dataLen);
    }
    #endregion



    #region 图片上传处理过程
    /// <summary>
    /// 图片上传处理过程
    /// </summary>
    /// <param name="HttpPostedFile">文件</param>
    /// <param name="FilePath">路径</param>
    /// <returns></returns>
   // public string UpFileSave(System.Web.HttpPostedFile file, string FilePath, string FileName)
    public string UpFileSave(System.Web.HttpPostedFile file, string FilePath)
    {

        string response = "1";

        if (file == null)
        {
            response = "800400";
            return response;
        }

        //if(!this.isImage(file))
        //{
        //    response = "800411";
        //    return response;  
        //}

        //if(this._savePathFile==""||_savePathFile=null) this._savePathFile = this.newFile("yyyy-MM");//目录
        string SavafilePath = FilePath;//+ _savePathFile;//   初始化上传目录



        string thefiledir = file.FileName;//获取本地文件路径
        int theFileLength = file.ContentLength; //获取文件大小
        int thePosition = thefiledir.LastIndexOf('\\');//处理
        string thefilename = thefiledir.Substring(thePosition + 1);//获取文件名
        string fileExt = System.IO.Path.GetExtension(file.FileName).ToLower().Trim();//扩展名

        if (this._FileLength < theFileLength)//判断文件大小
        {
            response = "800403";
            return response;
        }
        this._FileLength = theFileLength;//文件大小


        if (!FileExtensions(fileExt))//判断文件名是否合法
        {
            response = "800404";
            return response;
        }



        string SavaFileName;//文件以时间+随即数方式生成
        System.DateTime currentTime = new DateTime();
        currentTime = System.DateTime.Now;
        SavaFileName = currentTime.ToString();
        SavaFileName = SavaFileName.Replace(" ", "");
        SavaFileName = SavaFileName.Replace("-", "");
        SavaFileName = SavaFileName.Replace(":", "");
        SavaFileName = SavaFileName.Replace("/", "");

        SavaFileName = SavaFileName + "-" + TypeParse.RandomNum(8).ToString();
        SavaFileName = SavaFileName + fileExt;

        if (!string.IsNullOrEmpty(FileName)) SavaFileName = FileName + fileExt;


        FileFSO fso = new FileFSO();
        fso.createDirectory(SavafilePath);//目录过程
        file.SaveAs(SavafilePath + "/" + SavaFileName);//保存

        this._FileExt = fileExt.Trim();
        this._FileName = SavaFileName.Trim();


        return response;

    }
    #endregion

    #region 判断文件扩展名
    /// <summary>
    /// 判断文件扩展名
    /// </summary>
    /// <param name="fileload"></param>
    /// <param name="TheFileName"></param>
    /// <returns></returns>
    private bool FileExtensions(string TheFileName)
    {

        bool fileOK = false;
        for (int i = 0; i < this._allowedExtensions.Length; i++)
        {
            if (TheFileName.ToString().Trim() == this._allowedExtensions[i])
            {
                return true;
            }
        }
        return fileOK;
    }
    #endregion




}

