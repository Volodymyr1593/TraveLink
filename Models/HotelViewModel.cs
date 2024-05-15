namespace TraveLink.Models
{
    public class HotelViewModel
    {

        [Key] public int? hotels_id { get; set; }
        public string? title { get; set; }
        public string? url { get; set; }
        public string? hasmap { get; set; }
        public string? description { get; set; }
        public string? facilities { get; set; }
        public string? address { get; set; }
        public string? hoteltype { get; set; }
        public string? photoslink { get; set; }
        public byte? stars { get; set; }

        public string GetFotos()
        {


            StringBuilder sb = new StringBuilder();
            string key = @"thumb_url:\s*'([^']*)'";

            string Text = photoslink;



            Regex regex = new Regex(key);
            Match match = regex.Match(Text);



            if (match.Success)
            {

                sb.Append(match.Value);

                sb.ToString();

                string[] parts = sb.ToString().Split(new char[] { '\'', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string url = parts[1];

                return url;


            }

            else
            {

                return null;
            }

        }


        public List<string> GetFotosList()
        {


            StringBuilder sb = new StringBuilder();
            // string key = @"(?:large_url|thumb_url|highres_url):\s*'([^']*)'";
            string key = @"thumb_url:\s*'([^']*)'";




            List<string> Fotoslist = new List<string>();
            Regex regex = new Regex(key);
            MatchCollection matches = regex.Matches(photoslink);
            int count = -1;

            if (matches != null)
            {

                foreach (Match match in matches)
                {
                    if (match.Success)
                    {
                        // Console.WriteLine(match.Value );
                        sb.Append(match.Value);
                        count = count+2;
                        string[] parts = sb.ToString().Split(new char[] { '\'', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string url = parts[count];
                        Fotoslist.Add(url);
                    }
                    else
                    {
                        continue;
                    }

                }

                return Fotoslist;






            }


            else
            {
                return null;
            }


        }



    }





    







}
