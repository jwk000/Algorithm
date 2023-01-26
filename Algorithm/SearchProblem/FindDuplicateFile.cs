using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    internal class FindDuplicateFile//查找某路径下重复名字的文件
    {
        public Dictionary<string, List<string>> duplicateFile = new Dictionary<string, List<string>>();
        public string startPath = "D:/project101/trunk/x_client/xProject/Assets/Lua";
        public void Search(string startPath)
        {   //遍历此路径下所有文件
            foreach (var file in Directory.EnumerateFiles(startPath))
            {
                if (file.EndsWith(".lua"))
                {
                    string excelName = Path.GetFileNameWithoutExtension(file);//得到此.lua文件的路径
                    if (duplicateFile.ContainsKey(file))
                    {
                        var t = duplicateFile[file];
                        t.Add(excelName);
                    }
                    else
                    {
                        List<string> list = new List<string>();
                        list.Add(excelName);
                        duplicateFile.Add(file, list);
                    }
                }
            }
            //遍历此路径下所有目录
            foreach (var dire in Directory.EnumerateDirectories(startPath))
            {
                Search(dire);
            }
        }

        public void Show()
        {
            foreach (var e in duplicateFile)
            {
                //if (e.Value.Count > 1)
                {
                    Console.WriteLine(e.Value[0]);
                }
            }
        }
    }
}
