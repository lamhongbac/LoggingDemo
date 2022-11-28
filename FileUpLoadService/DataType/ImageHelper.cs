using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MSA.FW.Utilities.Image;

namespace FileUpLoadService.DataType
{
    public class APIHelper
    {
        /// <summary>
        /// Save 1 image from IFormFile
        /// </summary>
        /// <param name="uploadResult">KQ cua viec luu tru</param>
        /// <param name="imageOutput">Cau hinh luu tru</param>
        /// <param name="postedFile">File can luu</param>
        /// <param name="rootImageFolder">ROOT path luu tru file image</param>
        public static void SaveImage(ImageResult uploadResult, ImageConfig imageOutput, ImageConfig thumbOutput,
            IFormFile postedFile, string rootImageFolder)
        {


            //using (Image image = Image.FromStream(new MemoryStream(
            //    postedFile)))
            //{
            string imagePath = Path.Combine(rootImageFolder , imageOutput.ImagePath);// + "/" + imageOutput.FileName;
                                                                                    //image.Save(savePath);
                                                                                    //image.Dispose();
            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }
            //new fileName: from config 
            string newImageFileName = postedFile.FileName;
            if (!string.IsNullOrEmpty(imageOutput.FileName))
                newImageFileName = imageOutput.FileName;

            string fileToBeSaveFullPath = Path.Combine(imagePath, newImageFileName);
            using (var fileStream = new FileStream(fileToBeSaveFullPath, FileMode.Create))
            {
                postedFile.CopyTo(fileStream);
                fileStream.Flush();
            }

            ImageHelper.ResizeImage(fileToBeSaveFullPath, imageOutput.MaxWidth,
                imageOutput.MaxHeight, imageOutput.MBytes);

            uploadResult.Path.Add(postedFile.FileName);
           string thumbFileName = newImageFileName;
            if (!string.IsNullOrWhiteSpace(thumbOutput.FileName))
                thumbFileName = thumbOutput.FileName;

          string thumbresult=  CreateImageThumb(Imagedirectory:imagePath,directoryThumb: thumbOutput.ImagePath,
                 imagefileName: newImageFileName,maxWidth: thumbOutput.MaxWidth, 
                  maxHeight:thumbOutput.MaxHeight, mBytes:thumbOutput.MBytes,thumbFileName: thumbFileName);
            uploadResult.Path.Add(thumbresult);
        }

        
        //===


        /// <summary>
        /// Thumb se copy image da save thanh cong sau do re-size
        /// Thumb ban chat la image duoc downsize
        /// </summary>
        /// <param name="Imagedirectory">duong dan luu tru imagefile</param>
        /// <param name="directoryThumb"> neu kg chi dinh se phat sinh thu muc con=THUMB</param>
        /// <param name="fileName">image fileName</param>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <param name="mBytes"></param>
        /// <param name="overwrite"></param>
        /// <returns></returns>
        public static string CreateImageThumb( string Imagedirectory,
            string directoryThumb, string imagefileName, 
            int maxWidth, int maxHeight, double mBytes,
            bool overwrite = true, string thumbFileName="")
        {

            if (String.IsNullOrWhiteSpace(directoryThumb))
                directoryThumb = "THUMB";
            if (String.IsNullOrWhiteSpace(thumbFileName))
                 thumbFileName = "Thumb_"+imagefileName;

            string thumbpath = Path.Combine(Imagedirectory , directoryThumb);
            if (!Directory.Exists(thumbpath))
            {
                Directory.CreateDirectory(thumbpath);
            }

            string ImageFile =Path.Combine(Imagedirectory , imagefileName);

            string destinationFile =Path.Combine(thumbpath, thumbFileName);
            File.Copy(ImageFile, destinationFile, overwrite);
            ImageHelper.ResizeImage(destinationFile, maxWidth, maxHeight,
                mBytes);
            return thumbFileName;
        }
        
    }
}
