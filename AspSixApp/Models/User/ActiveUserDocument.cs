using System;

namespace AspSixApp.Models.User
{
    public class ActiveUserDocument : UserDocument
    {
        public double Progress { get; set; }
        public static ActiveUserDocument FromUserDocument(UserDocument document) =>
            new ActiveUserDocument
            {
                Content = document.Content,
                DateUploaded = document.DateUploaded,
                Name = document.Name,
                UserId = document.UserId,
                _id = document._id,
                Progress = 0
            };

    }
}