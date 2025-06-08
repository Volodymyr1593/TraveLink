namespace TraveLink.Services
{
    public class HotelParceService
    {

        public async Task<HtmlDocument> GethtmlHotelDocument(string link)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Виконайте GET запит до вказаного URL
                    HttpResponseMessage response = await client.GetAsync(link);

                    // Перевірте, чи відповідь успішна (код 200)
                    if (response.IsSuccessStatusCode)
                    {
                        // Отримайте HTML-код сторінки як рядок


                        HtmlDocument document = new HtmlDocument();

                        document.LoadHtml(await response.Content.ReadAsStringAsync());
                        return document;
                        ;



                    }
                    else
                    {
                        Console.WriteLine($"Отримано помилковий статус: {response.StatusCode}");
                        return null;
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Помилка при виконанні запиту: {e.Message}");
                    return null;
                }
            }
        }
        public FormUrlEncodedContent CreateSearchFormContent(HtmlDocument document, HotelViewModel? model)

        {

            if (document != null && model != null)
            {






                var formNode = document.DocumentNode.SelectSingleNode("//form [@class='b9a178a893']");

                if (formNode != null)
                {




                    var formAttributes = formNode.SelectNodes("//input [@type='hidden']");

                    var formData = new List<KeyValuePair<string, string>>();
                    foreach (var item in formAttributes)
                    {

                        string nameAttribute = item.GetAttributeValue("name", "");
                        string valueAttribute = item.GetAttributeValue("value", "");

                        formData.Add(new KeyValuePair<string, string>(nameAttribute, valueAttribute));

                    }






                    formData.Add(new KeyValuePair<string, string>("checkin", model.reserveModel.checkindate));
                    formData.Add(new KeyValuePair<string, string>("checkout", model.reserveModel.checkoutdate));
                    formData.Add(new KeyValuePair<string, string>("group_adults", model.reserveModel.adult));
                    formData.Add(new KeyValuePair<string, string>("group_children", model.reserveModel.children));
                    formData.Add(new KeyValuePair<string, string>("no_rooms", model.reserveModel.room));

                    var formContent = new FormUrlEncodedContent(formData);


                    return formContent;






                }
                else
                {

                    Console.WriteLine("Form not found");
                    return null;

                }


            }
            else
            {
                return null;
                Console.WriteLine("Didnt dawnload some paramer");
            }











        }
        public async Task<string> SentSearchForm(HtmlDocument document, FormUrlEncodedContent content, HotelViewModel model)
        {
            if (document != null && content != null)
            {
                var formNode = document.DocumentNode.SelectSingleNode("//form[@class='b9a178a893']");
                var formMethod = formNode.GetAttributeValue("method", "post").ToUpper();

                var formAction = model.url;


                using (HttpClient client = new HttpClient())
                {

                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36");
                    client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
                    client.DefaultRequestHeaders.Add("Accept-Language", "uk-UA,uk;q=0.9,en-US;q=0.8,en;q=0.7,ru;q=0.6");

                    client.DefaultRequestHeaders.Add("Priority", "u=0, i");
                    client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
                    client.DefaultRequestHeaders.Add("Sec-Ch-Ua", "\"Not/A)Brand\";v=\"8\", \"Chromium\";v=\"126\", \"Google Chrome\";v=\"126\"");
                    client.DefaultRequestHeaders.Add("Sec-Ch-Ua-Mobile", "?0");
                    client.DefaultRequestHeaders.Add("Sec-Ch-Ua-Platform", "\"Windows\"");
                    client.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "document");
                    client.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "navigate");
                    client.DefaultRequestHeaders.Add("Sec-Fetch-Site", "none");
                    client.DefaultRequestHeaders.Add("Sec-Fetch-User", "?1");
                    client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                    client.DefaultRequestHeaders.Add("Referer", "https://www.booking.com/");













                    HttpResponseMessage response;
                    if (formMethod == "POST")
                    {
                        response = await client.PostAsync(formAction, content);
                    }
                    else
                    {
                        var uriBuilder = new UriBuilder(formAction)
                        {
                            Query = await content.ReadAsStringAsync()
                        };
                        var url = uriBuilder.Uri.AbsoluteUri;

                        response = await client.GetAsync(url);

                    }

                    string responseContent = await response.Content.ReadAsStringAsync();

                    // string tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".html");
                    //File.WriteAllText(tempFilePath, responseContent);

                    // Відкриття файлу у браузері
                    // System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    // {
                    // FileName = tempFilePath,
                    //UseShellExecute = true
                    //});

                    // Видалення тимчасового файлу після перегляду
                    // await Task.Delay(5000); // Затримка на 10 секунд, щоб ви встигли переглянути сторінку
                    // File.Delete(tempFilePath);

                    return responseContent;



                }
            }

            else
            {


                return null;
            }
        }
        public HotelRoomData GetHotelObjekt(HtmlNode row)
        {






            if (row != null)
            {


                HotelRoomData data = new HotelRoomData();



                var roomtype = row.SelectSingleNode(".//span[@class='hprt-roomtype-icon-link ']")?.InnerText.ToString() ?? null;
                if (!string.IsNullOrEmpty(roomtype))
                {
                    data.roomtype = roomtype;
                }
                else
                {
                    Console.WriteLine(" roomtype not found");
                }







                var prices = row.SelectSingleNode(".//span[@class='prco-valign-middle-helper']")?.InnerText.ToString() ?? null;
                if (!string.IsNullOrEmpty(prices))
                {


                    data.todayprice = prices;



                }
                else
                {
                    Console.WriteLine(" prices not found");

                }




                var conditions = row.SelectNodes(".//td[@class= 'hprt-table-cell hprt-table-cell-conditions  droom_seperatorhprt-block-reposition-tooltip--container']") ?? null;
                if (conditions != null)
                {

                    List<string> conditionlist = new List<string>();
                    foreach (var condition in conditions)
                    {
                        var conditionbjekt = condition.InnerText.ToString();
                        conditionlist.Add(conditionbjekt);
                    }








                    data.conditions = conditionlist;



                }
                else
                {
                    Console.WriteLine(" conditions not found");
                }

                var gests = row.SelectNodes(".//td[@class='hprt-table-cell hprt-table-cell-occupancy  droom_seperator']") ?? null;
                if (gests != null)
                {
                    List<string> gestslist = new List<string>();
                    foreach (var gest in gests)
                    {
                        var gestobjekt = gest.InnerText.ToString();
                        gestslist.Add(gestobjekt);
                        data.gests = gestslist;

                    }
                }
                else
                {
                    Console.WriteLine("gests not found");
                }
                var selectrooms = row.SelectSingleNode(".//td[contains(@class, 'hprt-table-room-select')]") ?? null;

                if (selectrooms != null)
                {
                    var rooms = HttpUtility.HtmlDecode(selectrooms.InnerText.ToString());
                    data.selectrooms = rooms.Split(new[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                    Console.WriteLine("not found rooms for select");
                }


                if (data.selectrooms != null && data.todayprice != null)
                {
                    return data;
                }
                else
                {
                    return null;
                }

            }
            else
            {
                Console.WriteLine(" row is null");
                return null;
            }




        }
        public List<HotelRoomData> GetDataTableFromHotel(string response)
        {


            if (!string.IsNullOrEmpty(response))
            {
                HtmlDocument document = new HtmlDocument();

                document.LoadHtml(response);
                var hotelDataNode = document.DocumentNode.SelectSingleNode("//table[@id='hprt-table']");
                if (hotelDataNode != null)
                {
                    var bodytable = hotelDataNode.SelectSingleNode(".//tbody");
                    var rows = bodytable.SelectNodes(".//tr");
                    // Вибираємо всі рядки таблиці

                    List<HotelRoomData> data = new List<HotelRoomData>();

                    foreach (var row in rows)
                    {
                        var td = GetHotelObjekt(row);

                        if (td != null)
                        {
                            data.Add(td);
                        }
                        else
                        {
                            continue;
                        }






                    }

                    return data;






                }


                else
                {
                    Console.WriteLine("Node table not found");
                    return null;
                }



            }

            else
            {
                Console.WriteLine(" response is null or empty");
                return null;
            }

        }

        public async Task<List<HotelViewModel>> MainSearchFilter(List<HotelViewModel> models, HotelViewModel? searchData)
        {
            if (models != null)
            {
                List<HotelViewModel> hotels = new List<HotelViewModel>();

                foreach (var model in models)
                {

                    if (!string.IsNullOrEmpty(model.url))
                    {

                        var document = await GethtmlHotelDocument(model.url) ?? null;
                        var formContent = CreateSearchFormContent(document, searchData) ?? null;
                        var responseContent = await SentSearchForm(document, formContent, model) ?? null;
                        var orderData = GetDataTableFromHotel(responseContent) ?? null;
                        if (orderData != null)
                        {
                            model.roomDataModels = orderData;
                            hotels.Add(model);
                        }


                        else
                        {
                            continue;
                        }









                    }
                    else
                    {
                        break;
                    }















                }


                return hotels;


            }
            else
            {
                List<HotelViewModel> emptymodels = new List<HotelViewModel>();
                return emptymodels;
            }










        }
















    }
}
