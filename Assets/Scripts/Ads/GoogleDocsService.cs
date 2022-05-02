

using System;
using System.Net.Http;
using System.Threading.Tasks;
public class GoogleDocsService
{
    public static GoogleDocsService instance;

    public GoogleDocsService()
    {
        if (instance == null)
        {
            instance = this;
        }
    }



    // public static async Task uploadScore(int score)
    // {

    //     var content = new FormUrlEncodedContent(score);

    //     var response = await client.PostAsync("http://www.example.com/recepticle.aspx", content);

    //     var responseString = await response.Content.ReadAsStringAsync();






    // }


}