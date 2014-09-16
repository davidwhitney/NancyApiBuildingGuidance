using System.Collections.Generic;
using System.IO;
using Nancy;
using Nancy.IO;

namespace WebApp.Infrastructure.CustomMediaTypes.Application_FlatText
{
    public class FlatTextSerializer : ISerializer
    {
        public IEnumerable<string> Extensions
        {
            get { return new[] { FlatTextResponseProcessor .CustomContentExtension }; }
        }

        public bool CanSerialize(string contentType)
        {
            return contentType == FlatTextResponseProcessor.CustomContentType;
        }

        public void Serialize<TModel>(string contentType, TModel model, Stream outputStream)
        {
            using (var writer = new StreamWriter(new UnclosableStreamWrapper(outputStream)))
            {
                foreach (var property in model.GetType().GetProperties())
                {
                    writer.Write(property.Name + ": " + property.GetValue(model));
                }
            }
        }
    }
}
