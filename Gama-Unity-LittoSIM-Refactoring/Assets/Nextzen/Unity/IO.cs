using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Nextzen.Unity
{
    public class IO
    {
        public class Response
        {
            // A "null" error value represents no error in the Response
            public string error;

            public byte[] data;

            public Response(string e, byte[] d)
            {
                error = e;
                data = d;
            }

            public bool hasError()
            {
                if (error == null)
                {
                    return false;
                }
                return true;
            }
        }

        public delegate void IORequestCallback(Response response);

        public IEnumerator FetchNetworkData(Uri uri, IORequestCallback callback)
        {
            Response response;

            if (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
            {
                response = new Response("Wrong Uri scheme provided", null);
            }
            else
            {
                UnityWebRequest request = UnityWebRequest.Get(uri.AbsoluteUri);
                yield return request.SendWebRequest();
                string requestError = null;

                if (request.isNetworkError)
                {
                    // only handles system errors
                    requestError = request.error;
                }
                else if (request.responseCode >= 400)
                {
                    // handle http errors
                    requestError = "HTTP Error with errorcode: " + request.responseCode;
                    // Following not available in 5.6.1 version
                    //requestError = UnityWebRequest.GetErrorDescription(request.responseCode);
                }
                response = new Response(requestError, request.downloadHandler.data);
            }
            callback(response);
        }

        public IEnumerator FetchAssetData(string assetPath, IORequestCallback callback)
        {
            Response response;

            ResourceRequest request = Resources.LoadAsync(assetPath);
            yield return request;

            TextAsset textAsset = request.asset as TextAsset;
            if (textAsset == null)
            {
                response = new Response("Missing asset at path: " + assetPath, null);
            }

            response = new Response(null, textAsset.bytes);
            callback(response);
        }
    }
}