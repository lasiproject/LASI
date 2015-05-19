using System;

namespace LASI.WebApp.Models.User
{
    using BsonIgnoreAttribute = MongoDB.Bson.Serialization.Attributes.BsonIgnoreAttribute;

    public class ActiveUserDocument : UserDocument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActiveUserDocument"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor is private and is used by the <see cref="FromUserDocument(UserDocument)"/> to create instances.
        /// Use the <see cref="FromUserDocument(UserDocument)"/> factory to create an instance of this class.
        /// </remarks>
        private ActiveUserDocument() { }
        /// <summary>
        /// Gets the progress, represented as the percentage of completion of the <see cref="ActiveUserDocument"/>.
        /// </summary>
        [BsonIgnore]
        public double Progress { get; private set; }
        public static ActiveUserDocument FromUserDocument(UserDocument document) =>
            new ActiveUserDocument
            {
                Content = document.Content,
                DateUploaded = document.DateUploaded,
                Name = document.Name,
                UserId = document.UserId,
                _id = document._id,
            };

    }
}