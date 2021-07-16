using Azure.Storage.Blobs;
using Lab5.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab5.Models;
using Microsoft.AspNetCore.Http;
using Azure;
using System.IO;

namespace Lab5.Controllers
{
    public class AnswerImagesController : Controller
    {
        private readonly AnswerImageDataContext _context;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string earthContainerName = "earthimages";
        private readonly string computerContainerName = "computerimages";

        private string containerName = "answerType"; //created to be able to organize how user chooses answer image type

        public AnswerImagesController(AnswerImageDataContext context, BlobServiceClient blobServiceClient)
        {
            _context = context;
            _blobServiceClient = blobServiceClient;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.AnswerImages.ToListAsync());
        }

        //GET
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile answerImage, int answerType) 
        {
            BlobContainerClient containerClient;
            // to determine which type of image user want to upload and which container to store it in
            if (answerType == 1)
            {
                containerName = computerContainerName;
                
            }
            else if (answerType == 0)
            {
                containerName = earthContainerName;
            }
            try
            {
                containerClient = await _blobServiceClient.CreateBlobContainerAsync(containerName); //creating containers
                containerClient.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer); //giving public access
            }
            catch (RequestFailedException)
            {
                containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            }

            try
            {
                var blockBlob = containerClient.GetBlobClient(answerImage.FileName); 
                if (await blockBlob.ExistsAsync())
                {
                    await blockBlob.DeleteAsync();
                }
                using (var memoryStream = new MemoryStream())
                {
                    await answerImage.CopyToAsync(memoryStream); //copy file data into memory
                    memoryStream.Position = 0; //navigate back to start of memory stream
                    await blockBlob.UploadAsync(memoryStream); //send files to cloud
                    memoryStream.Close();
                }
                //add photo to DB if upload a success
                var image = new AnswerImage();
                image.Url = blockBlob.Uri.AbsoluteUri;
                image.FileName = answerImage.FileName;
                // setting the Question value to Computer or Earth by comparing the containerName to see if it matches the 
                // users radio select button which i gave each a value of 1 or 0. 
                if (answerType == 1)
                {
                    image.Question = Question.Computer;
                } 
                else if (answerType == 0)
                {
                    image.Question = Question.Earth;
                }
                _context.AnswerImages.Add(image);
                _context.SaveChanges();
            }
            catch (RequestFailedException)
            {
                View("Error");
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.AnswerImages.FirstOrDefaultAsync(m => m.AnswerImageId == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
            
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var image = await _context.AnswerImages.FindAsync(id);
            // confimring which container the image selected to delete is from (earth=0, else computer=1)
            if (image.Question == 0)
            {
                containerName = earthContainerName;
            }
            else
            {
                containerName = computerContainerName;
            }

            BlobContainerClient containerClient;
            // Get the container and return a container client object
            try
            {
                containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            }
            catch (RequestFailedException)
            {
                return View("Error");
            }

            try
            {
            // Get the blob that holds the data
                var blockBlob = containerClient.GetBlobClient(image.FileName);
                if (await blockBlob.ExistsAsync())
                {
                    await blockBlob.DeleteAsync();
                }

                _context.AnswerImages.Remove(image);
                await _context.SaveChangesAsync();

            }
            catch (RequestFailedException)
            {
                return View("Error");
            }

            return RedirectToAction("Index");
            
        }
    }
}


