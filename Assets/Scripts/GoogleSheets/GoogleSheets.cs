using System;
using UnityEngine;
using System.Collections.Generic;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;
using Google.Apis.Services;


public static class GoogleSheets
{


    static private String spreadsheetId = "1lK3D_AZ_wFdjKhaVovHmNxZPl7qG4QLDYyJLRYDHbm0";

    static private String serviceAccountID = "githubdeployment@pin-the-needle-46059362.iam.gserviceaccount.com";
    static private SheetsService service;
    // Start is called before the first frame update


    public static void InitilizeService()
    {

        Stream jsonCred = (Stream)File.Open("./Assets/Scripts/GoogleSheets/credentials.json", FileMode.Open);

        ServiceAccountCredential credential = ServiceAccountCredential.FromServiceAccountData(jsonCred);


        service = new SheetsService(
            new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
            }
        );
    }


    public static void WriteScore(string player, int score)
    {

        InitilizeService();

        IList<object> row = new List<object>();
        row.Add(player);
        row.Add(score.ToString());
        IList<IList<object>> data = new List<IList<object>>();
        data.Add(row);

        // Define request parameters.
        string spreadsheetId = "1lK3D_AZ_wFdjKhaVovHmNxZPl7qG4QLDYyJLRYDHbm0";

        string range = "Pin The Needle Scores!A1";
        //string valueInputOption = "USER_ENTERED";

        // The new values to apply to the spreadsheet.
        List<ValueRange> updateData = new List<ValueRange>();
        var dataValueRange = new ValueRange();
        dataValueRange.Range = range;
        dataValueRange.Values = data;
        updateData.Add(dataValueRange);

        // BatchUpdateValuesRequest requestBody = new BatchUpdateValuesRequest();
        // requestBody.ValueInputOption = valueInputOption;
        // requestBody.Data = updateData;

        // var request = service.Spreadsheets.Values.BatchUpdate(requestBody, spreadsheetId);






        // How the input data should be interpreted.
        SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum valueInputOption = (SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum)2;  // TODO: Update placeholder value.

        // How the input data should be inserted.
        SpreadsheetsResource.ValuesResource.AppendRequest.InsertDataOptionEnum insertDataOption = (SpreadsheetsResource.ValuesResource.AppendRequest.InsertDataOptionEnum)1;  // TODO: Update placeholder value.

        // TODO: Assign values to desired properties of `requestBody`:
        ValueRange requestBody = new ValueRange();
        requestBody.Range = range;
        requestBody.Values = data;

        SpreadsheetsResource.ValuesResource.AppendRequest request = service.Spreadsheets.Values.Append(requestBody, spreadsheetId, range);
        request.ValueInputOption = valueInputOption;
        request.InsertDataOption = insertDataOption;








        request.ExecuteAsync(); // For async 

        //return JsonConvert.SerializeObject(response);

    }


}
