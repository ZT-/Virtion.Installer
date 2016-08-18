using System;
using System.Text;
using System.IO;
using System.Windows;

namespace Virtion.Util
{
    class FileManager
    {
        public static byte[] ReadFileByte(String path)
        {
            if (File.Exists(path) == false)
            {
                MessageBox.Show("File is not found! " + path);
                return null;
            }

            FileStream fs = File.OpenRead(path); //OpenRead
            int filelength = 0;
            filelength = (int)fs.Length; //获得文件长度 
            Byte[] image = new Byte[filelength]; //建立一个字节数组 
            fs.Read(image, 0, filelength); //按字节流读取 
            fs.Close();
            return image;
        }

        public static String ReadFile(String path)
        {
            if (File.Exists(path) == false)
            {
                MessageBox.Show("File is not found! " + path);
                return null;
            }
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                StreamReader sr = new StreamReader(path, Encoding.Default);
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    //Console.WriteLine(line.ToString());
                    stringBuilder.Append(line);
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return stringBuilder.ToString();
        }

        public static void WriteFile(String path, String data)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                sw.Write(data);
                sw.Flush();
                sw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void WriteFile(String path, byte[] data)
        {
            try
            {
                path.Replace('*', 'x');
                FileStream fs = new FileStream(path, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(data);
                bw.Flush();
                bw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void Rename(string old, string newName)
        {
            if (old == newName)
            {
                return;
            }
            try
            {
                FileInfo fileInfo = new FileInfo(old);
                fileInfo.MoveTo(newName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }

}
