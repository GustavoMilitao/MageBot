namespace BlueSheep.Core.Job
{
    public class Resource
    {
        public int ResourceId { get; set; }
        public string ResourceName { get; set; }

        public Resource()
        {

        }

        public Resource(int resourceId, string resourceName)
        {
            ResourceId = resourceId;
            ResourceName = resourceName;
        }

    }
}
