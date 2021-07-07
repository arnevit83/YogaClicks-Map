namespace Clicks.Yoga.Media.Scanners
{
    public class TextWebMediaScanner : WebMediaScanner
    {
        protected override void ScanDocument()
        {
            Resource.Title =
                GetOpenGraphProperty("og:title") ??
                GetMetaProperty("title") ??
                GetTitle() ??
                Resource.Title;

            if (string.IsNullOrWhiteSpace(Resource.Title))
            {
                throw new UnsupportedMediaException();
            }

            Resource.Content =
                GetOpenGraphProperty("og:description") ??
                GetMetaProperty("description") ??
                Resource.Content;
        }

        private string GetTitle()
        {
            return GetElementValue("//title");
        }
    }
}