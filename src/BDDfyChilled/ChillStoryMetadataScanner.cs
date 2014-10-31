using System;
using System.Reflection;
using TestStack.BDDfy;

namespace BDDfyChilled
{
    public class ChillStoryMetadataScanner : IStoryMetadataScanner
    {
        public virtual StoryMetadata Scan(object testObject, Type explicityStoryType = null)
        {
            string specificationTitle = GetSubject(testObject);
            var story = new StoryAttribute() { Title = specificationTitle, TitlePrefix = "Specifications For: " };
            return new StoryMetadata(testObject.GetType(), story);
        }

        private string GetSubject(object testObject)
        {
            return testObject
                .GetType()
                .GetProperty("Subject", BindingFlags.Instance | BindingFlags.NonPublic)
                .PropertyType
                .Name;
        }
    }
}
