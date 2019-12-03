using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using IdentityCheck.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.Storage.Queue;
using System.Drawing;
using IdentityCheck.Utils;

namespace IdentityCheck.Services
{
    public class ImageService : IImageService
    {
        private readonly string blobContainerName = "rawimages";
        private readonly string queueContainerName = "imagequeue";
        private readonly string[] validExtensions = { "jpg", "jpeg", "png" };
        private string accessKey = string.Empty;
        private CloudStorageAccount account;
        private CloudBlobClient blobClient;
        private CloudBlobContainer blobContainer;
        private CloudQueueClient queueClient;

        public ImageService(IConfiguration configuration)
        {
            this.accessKey = configuration.GetConnectionString("AzureStorageKey");
            this.account = CloudStorageAccount.Parse(accessKey);
            this.blobClient = account.CreateCloudBlobClient();
            this.blobContainer = blobClient.GetContainerReference(blobContainerName);
            this.queueClient = account.CreateCloudQueueClient();
        }

        public async Task DeleteAllFileAsync(long postId)
        {
            var imageList = await GetImageListAsync(postId);
            foreach (var image in imageList)
            {
                await DeleteFileAsync(image.Path);
            }
        }

        public async Task DeleteFileAsync(string fileName)
        {
            var pathInBlob = fileName.Split(blobContainerName + "/")[1];
            var blob = blobContainer.GetBlockBlobReference(pathInBlob);
            await blob.DeleteIfExistsAsync();
        }

        public async Task<List<ImageDetails>> GetImageListAsync(long postId)
        {
            var imageList = new List<ImageDetails>();
            BlobContinuationToken blobContinuationToken = null;
            do
            {
                await blobContainer.CreateIfNotExistsAsync();
                var results = await blobContainer.ListBlobsSegmentedAsync(null, blobContinuationToken);
                blobContinuationToken = results.ContinuationToken;
                GetBlobDirectories(imageList, postId);
            } while (blobContinuationToken != null);
            return imageList;
        }

        public async Task<List<IFormFile>> UploadImagesAsync(List<IFormFile> files, long postId)
        {
            var wrongFiles = new List<IFormFile>();
            var filePath = Path.GetTempFileName();
            foreach (var file in files)
            {
                if (CheckImageExtension(file) && file.Length > 0)
                {
                    await SaveImagesIntoTempFolder(filePath, file);
                    using (var tempStream = new FileStream(filePath, FileMode.Open))
                    {
                        var path = ResizeImage(postId, tempStream);
                        using (var stream = new FileStream(path, FileMode.Open))
                        {
                            if (CheckImageSize(stream))
                            {
                                var azurePath = GenerateAzurePath(postId, file);
                                await UploadAsync(azurePath, stream);
                            }
                            else
                            {
                                wrongFiles.Add(file);
                            }
                        }
                    }
                }
                else
                {
                    wrongFiles.Add(file);
                }
            };
            return wrongFiles;
        }


        private string ResizeImage(long postId, Stream stream)
        {
            var size = 300;
            var fileName = postId.ToString() + ".jpg";
            stream.Seek(0, SeekOrigin.Begin);
            var bitmap = new Bitmap(Image.FromStream(stream));
            Bitmap resizedBitmap = ImageResizer.ResizeImage(bitmap, size, size);
            var tempPath = Path.GetTempPath();
            var absolutePath = tempPath + "/" + fileName;
            resizedBitmap.Save(absolutePath);
            return absolutePath;
        }

        public void GetBlobDirectories(List<ImageDetails> imageList, long postId)
        {
            foreach (IListBlobItem item in blobContainer.ListBlobs())
            {
                if (item is CloudBlobDirectory)
                {
                    GetImagesFromBlob(item, imageList, postId);
                }
            }
        }

        public void GetImagesFromBlob(IListBlobItem item, List<ImageDetails> imageList, long postId)
        {
            CloudBlobDirectory directory = (CloudBlobDirectory)item;
            IEnumerable<IListBlobItem> blobs = directory.ListBlobs(true);
            foreach (var blob in blobs)
            {
                var id = GetPostImageFolder(blob.Uri);
                if (id == postId)
                {
                    imageList.Add(new ImageDetails
                    {
                        Name = blob.Uri.Segments[blob.Uri.Segments.Length - 1],
                        Path = blob.Uri.ToString()
                    });
                }
            }
        }

        private async Task UploadAsync(string imageName, Stream stream)
        {
            await blobContainer.CreateIfNotExistsAsync();
            BlobContainerPermissions permissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            };
            await blobContainer.SetPermissionsAsync(permissions);

            var cloudBlockBlob = blobContainer.GetBlockBlobReference(imageName);
            await cloudBlockBlob.UploadFromStreamAsync(stream);


            var queueReference = queueClient.GetQueueReference(queueContainerName);
            await queueReference.CreateIfNotExistsAsync();
            await queueReference.AddMessageAsync(new CloudQueueMessage(imageName));

        }

        private static int GetPostImageFolder(Uri uri)
        {
            var pathSegments = uri.ToString().Split("/");
            var folder = pathSegments[pathSegments.Length - 2];
            return Convert.ToInt32(folder);
        }

        private static string GetExtension(IFormFile file)
        {
            if (file != null)
            {
                var fileNameSegments = file.FileName.Split(".");
                return fileNameSegments[fileNameSegments.Length - 1];
            }
            return null;
        }

        private static bool CheckImageSize(FileStream stream)
        {
            var fourMegaBytes = 4 * 1024 * 1024;
            return stream.Length < fourMegaBytes;
        }

        private bool CheckImageExtension(IFormFile file)
        {
            var extensions = new List<string>(validExtensions);
            return extensions.Contains(GetExtension(file));
        }

        private static string GenerateAzurePath(long postId, IFormFile file)
        {
            var timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            return postId.ToString() + "/" + timeStamp + "." + GetExtension(file);
        }

        private async static Task SaveImagesIntoTempFolder(string filePath, IFormFile file)
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }
    }
}
