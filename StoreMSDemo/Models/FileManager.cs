using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Aspose.BarCode;

namespace StoreMSDemo.Models
{
    public class FileManager
    {
        private string _barCodeFolderPath = "/Content/BarCode/";
        public string BarCodeFolder
        {
            get { return _barCodeFolderPath; }
        }

        public string CreateBarCode(string barcodeString, int albumID)
        {
            string barcodeSavePath = HttpContext.Current.Server.MapPath("~" + this._barCodeFolderPath + albumID.ToString());
            BarCodeBuilder barCodeBuilder = new BarCodeBuilder();
            barCodeBuilder.CodeText = barcodeString;
            barCodeBuilder.xDimension = 1f;

            barCodeBuilder.Save(barcodeSavePath, System.Drawing.Imaging.ImageFormat.Gif);

            return _barCodeFolderPath + albumID + ".gif";
        }
    }
}