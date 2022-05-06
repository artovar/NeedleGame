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

        //  Loading private key from resources as a TextAsset
        String key = "-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQDLwvMK1dqBoFaD\nyAs6XyImjM/EfY+fKUU8kEc7C+uQXs2aMsrgE7QHT28ZmlyqG/av7b1a1VG1mDnj\nn8TA3Sgyp6RhAZg6JA3P3Bh/uyCGzWD2PPMUK6LAmCH9kfMERlWvT9v67E6hGqlF\nL+oZ5dWIaXrxYkcKG6sJ5srmXEfgesexmY3Mp0CJwn2t5qqKpA1u31hx1PvL9+xu\nBwYj7sfywvh7GQ73uen4aDLGn4Uxe6lPhT7R2sC1wEOdJoe3PGIKPs09IDfC3ndX\n26arvfPKWOwaP/SQ8ZM7pVt4sV2MN6CUpO5WP1MKV6h2Z6/S9xz1KMfeRivHmyP0\ncuwV2mDhAgMBAAECggEAAwNB5rIcxffh75bgm59WeO1yEPyuR/MrQ+b6NqRJelv8\nfEAuAbwo2EbTZLSElDtPnXBNcW9gAuhaSsRM+3EJrB4QbyK/5nQsBBvD/Pd+Vp/o\nxBtImM7AXEqjjgU/a5vCnpH2tbISw40KXayEsJdWXer5V2JVnI+mKsJP07IfmfEQ\nBrCDLtPdnHtfx+12L+W+LcAnjV1aI722m05xiSgLXTQPihS4S6TZ8ACKCLXPRWll\nzXnTPB6F7lsZDjyDVUCc59OykntEMEXfVYpaXHwTA6/jKqBi1j7CMU717QPDbk57\n4HaN4ORZqpJgHsAJS9UCywzjgPbpAzD4zxQrgM01sQKBgQD2qKIa1N8i2R7Qfzsr\n6v6T95WrPoi74qrlMdr6aIcwjYVWZa31zzaLWOOR6iDQ9DNqyZBy5yWvmUGqlXFa\nilooXISjvXTuwjKVEv1WjxKS4UuWEtl8fvJyWy7PYlrPJj5YbvUqAFNdHYiKjQ6p\nk7XpgTPzzn6HJYePbAskMVSCHQKBgQDTem0MG7LdZmfmbootKIPm+XIEgpSBJEEM\nP9uCdYFYMEph2R1TtlgnLpVDRa8ORQf9TUD1rGwjG7G97aq4/maK7tKgVjviWr57\nIDqKinUWeMwerW4CEykE2t/lp3XgivuccBMDGE4Xf2xkJyB5u5tjhPLVKD16Vf5x\n6Rq/cjRelQKBgQDx/9NDtfTSsOj9sMayORXawL+USn9Gr4Xx5m8s3V898KuQb6Q0\nlj+yidyEbYwS1nSX4fiZ81TTKu71WTfkl/cKwHLWX2wZUGjmP8JUqXyUpPxO2mGN\nNtPSBMoxaBMmSCRFCV/5/GXN5sru/KQVFjoVRdFd9AvJtrWAroVj1g5OtQKBgFdK\n9bBFzrMtDj5D6q1xR+ETnkjWTc5w1Jjl0woqztVEOa/iGwTvA7xIsjwui2/sdrvM\nI8BFB+4GBHxNmmb6PAGlSzP75IQuyOmyB/gf5uz7t+YB2KLqncfvQPRfB31EYU/V\nw0NHyRRR+L/ulq0pD58oxcPO9HGHBZynh+72iT9xAoGAeOaTl+MFMaG3SnOH3uAZ\nPVIzxvNK6XmZqPLJExYLqE2eHx1Hh1XM/PTqmIQWTZg4eMFhVhXrm9o69cwnsMK6\nQNCg5T8j/HyR+b9TXMAuLrGKaxjvkulsWB4Kb5I1aDGQSu6GjOnkQN8FNJmaNWZl\n3l66YvGRGOqa1eqg0qNoYL8=\n-----END PRIVATE KEY-----\n";

        // Creating a  ServiceAccountCredential.Initializer
        // ref: https://googleapis.dev/dotnet/Google.Apis.Auth/latest/api/Google.Apis.Auth.OAuth2.ServiceAccountCredential.Initializer.html
        ServiceAccountCredential.Initializer initializer = new ServiceAccountCredential.Initializer(serviceAccountID);

        // Getting ServiceAccountCredential from the private key
        // ref: https://googleapis.dev/dotnet/Google.Apis.Auth/latest/api/Google.Apis.Auth.OAuth2.ServiceAccountCredential.html
        ServiceAccountCredential credential = new ServiceAccountCredential(
            initializer.FromPrivateKey(key)
        );

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
        List<ValueRange> updateData = new List<ValueRange>();
        var dataValueRange = new ValueRange();
        dataValueRange.Range = range;
        dataValueRange.Values = data;
        updateData.Add(dataValueRange);

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


        request.Execute(); // For async 


    }


}
