using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BinaryChecker
{
    public class FileSelect
    {

        public static string FileOpen() {

            var filepath = "";

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "default.msbt";
            ofd.InitialDirectory = @"C:\";
            ofd.Filter = "すべてのファイル(*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.Title = "開くファイルを選択してください";
            ofd.RestoreDirectory = true;
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName;
            }

            return filepath;
        }
    }
}
